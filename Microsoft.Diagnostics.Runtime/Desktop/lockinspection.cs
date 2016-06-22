// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal class LockInspection
    {
        private DesktopGCHeap _heap;
        private DesktopRuntimeBase _runtime;
        private ClrType _rwType, _rwsType;
        private Dictionary<ulong, DesktopBlockingObject> _monitors = new Dictionary<ulong, DesktopBlockingObject>();
        private Dictionary<ulong, DesktopBlockingObject> _locks = new Dictionary<ulong, DesktopBlockingObject>();
        private Dictionary<ClrThread, DesktopBlockingObject> _joinLocks = new Dictionary<ClrThread, DesktopBlockingObject>();
        private Dictionary<ulong, DesktopBlockingObject> _waitLocks = new Dictionary<ulong, DesktopBlockingObject>();
        private Dictionary<ulong, ulong> _syncblks = new Dictionary<ulong, ulong>();
        private DesktopBlockingObject[] _result = null;

        internal LockInspection(DesktopGCHeap heap, DesktopRuntimeBase runtime)
        {
            _heap = heap;
            _runtime = runtime;
        }

        internal DesktopBlockingObject[] InitLockInspection()
        {
            if (_result != null)
                return _result;

            // First, enumerate all thinlocks on the heap.
            foreach (var seg in _heap.Segments)
            {
                for (ulong obj = seg.FirstObject; obj != 0; obj = seg.NextObject(obj))
                {
                    ClrType type = _heap.GetObjectType(obj);
                    if (IsReaderWriterLock(obj, type))
                        _locks[obj] = CreateRWLObject(obj, type);
                    else if (IsReaderWriterSlim(obj, type))
                        _locks[obj] = CreateRWSObject(obj, type);

                    // Does this object have a syncblk with monitor associated with it?
                    uint header;
                    if (!_heap.GetObjectHeader(obj, out header))
                        continue;

                    if ((header & (BIT_SBLK_IS_HASH_OR_SYNCBLKINDEX | BIT_SBLK_SPIN_LOCK)) != 0)
                        continue;

                    uint threadId = header & SBLK_MASK_LOCK_THREADID;
                    if (threadId == 0)
                        continue;

                    ClrThread thread = _runtime.GetThreadFromThinlockID(threadId);
                    if (thread != null)
                    {
                        int recursion = ((int)header & SBLK_MASK_LOCK_RECLEVEL) >> SBLK_RECLEVEL_SHIFT;
                        _monitors[obj] = new DesktopBlockingObject(obj, true, recursion + 1, thread, BlockingReason.Monitor);
                    }
                }
            }

            // Enumerate syncblocks to find locks
            int syncblkCnt = _runtime.GetSyncblkCount();
            for (int i = 0; i < syncblkCnt; ++i)
            {
                ISyncBlkData data = _runtime.GetSyncblkData(i);
                if (data == null || data.Free)
                    continue;

                _syncblks[data.Address] = data.Object;
                _syncblks[data.Object] = data.Object;
                ClrThread thread = null;
                if (data.MonitorHeld)
                {
                    ulong threadAddr = data.OwningThread;
                    foreach (var clrThread in _runtime.Threads)
                    {
                        if (clrThread.Address == threadAddr)
                        {
                            thread = clrThread;
                            break;
                        }
                    }
                }

                _monitors[data.Object] = new DesktopBlockingObject(data.Object, data.MonitorHeld, (int)data.Recursion, thread, BlockingReason.Monitor);
            }

            SetThreadWaiters();

            int total = _monitors.Count + _locks.Count + _joinLocks.Count + _waitLocks.Count;
            _result = new DesktopBlockingObject[total];

            int j = 0;
            foreach (DesktopBlockingObject blocker in _monitors.Values)
                _result[j++] = blocker;

            foreach (DesktopBlockingObject blocker in _locks.Values)
                _result[j++] = blocker;

            foreach (DesktopBlockingObject blocker in _joinLocks.Values)
                _result[j++] = blocker;

            foreach (DesktopBlockingObject blocker in _waitLocks.Values)
                _result[j++] = blocker;

            Debug.Assert(j == _result.Length);

            // Free up some memory.
            _monitors = null;
            _locks = null;
            _joinLocks = null;
            _waitLocks = null;
            _syncblks = null;

            return _result;
        }

        private bool IsReaderWriterLock(ulong obj, ClrType type)
        {
            if (type == null)
                return false;

            if (_rwType == null)
            {
                if (type.Name != "System.Threading.ReaderWriterLock")
                    return false;

                _rwType = type;
                return true;
            }

            return _rwType == type;
        }
        private bool IsReaderWriterSlim(ulong obj, ClrType type)
        {
            if (type == null)
                return false;

            if (_rwsType == null)
            {
                if (type.Name != "System.Threading.ReaderWriterLockSlim")
                    return false;

                _rwsType = type;
                return true;
            }

            return _rwsType == type;
        }

        private void SetThreadWaiters()
        {
            HashSet<string> eventTypes = null;
            List<BlockingObject> blobjs = new List<BlockingObject>();

            foreach (DesktopThread thread in _runtime.Threads)
            {
                int max = thread.StackTrace.Count;
                if (max > 10)
                    max = 10;

                blobjs.Clear();
                for (int i = 0; i < max; ++i)
                {
                    DesktopBlockingObject blockingObj = null;
                    ClrMethod method = thread.StackTrace[i].Method;
                    if (method == null)
                        continue;

                    ClrType type = method.Type;
                    if (type == null)
                        continue;

                    switch (method.Name)
                    {
                        case "AcquireWriterLockInternal":
                        case "FCallUpgradeToWriterLock":
                        case "UpgradeToWriterLock":
                        case "AcquireReaderLockInternal":
                        case "AcquireReaderLock":
                            if (type.Name == "System.Threading.ReaderWriterLock")
                            {
                                blockingObj = FindLocks(thread.StackLimit, thread.StackTrace[i].StackPointer, IsReaderWriterLock);
                                if (blockingObj == null)
                                    blockingObj = FindLocks(thread.StackTrace[i].StackPointer, thread.StackBase, IsReaderWriterLock);

                                if (blockingObj != null && (blockingObj.Reason == BlockingReason.Unknown || blockingObj.Reason == BlockingReason.None))
                                {
                                    // This should have already been set correctly when the BlockingObject was created.  This is just a best-guess.
                                    if (method.Name == "AcquireReaderLockInternal" || method.Name == "AcquireReaderLock")
                                        blockingObj.Reason = BlockingReason.WriterAcquired;
                                    else
                                        blockingObj.Reason = BlockingReason.ReaderAcquired;
                                }
                            }
                            break;

                        case "TryEnterReadLockCore":
                        case "TryEnterReadLock":
                        case "TryEnterUpgradeableReadLock":
                        case "TryEnterUpgradeableReadLockCore":
                        case "TryEnterWriteLock":
                        case "TryEnterWriteLockCore":
                            if (type.Name == "System.Threading.ReaderWriterLockSlim")
                            {
                                blockingObj = FindLocks(thread.StackLimit, thread.StackTrace[i].StackPointer, IsReaderWriterSlim);
                                if (blockingObj == null)
                                    blockingObj = FindLocks(thread.StackTrace[i].StackPointer, thread.StackBase, IsReaderWriterSlim);


                                if (blockingObj != null && (blockingObj.Reason == BlockingReason.Unknown || blockingObj.Reason == BlockingReason.None))
                                {
                                    // This should have already been set correctly when the BlockingObject was created.  This is just a best-guess.
                                    if (method.Name == "TryEnterWriteLock" || method.Name == "TryEnterWriteLockCore")
                                        blockingObj.Reason = BlockingReason.ReaderAcquired;
                                    else
                                        blockingObj.Reason = BlockingReason.WriterAcquired;
                                }
                            }

                            break;

                        case "JoinInternal":
                        case "Join":
                            if (type.Name == "System.Threading.Thread")
                            {
                                ulong threadAddr;
                                ClrThread target;

                                if (FindThread(thread.StackLimit, thread.StackTrace[i].StackPointer, out threadAddr, out target) ||
                                    FindThread(thread.StackTrace[i].StackPointer, thread.StackBase, out threadAddr, out target))
                                {
                                    if (!_joinLocks.TryGetValue(target, out blockingObj))
                                        _joinLocks[target] = blockingObj = new DesktopBlockingObject(threadAddr, true, 0, target, BlockingReason.ThreadJoin);
                                }
                            }
                            break;

                        case "Wait":
                        case "ObjWait":
                            if (type.Name == "System.Threading.Monitor")
                            {
                                blockingObj = FindMonitor(thread.StackLimit, thread.StackTrace[i].StackPointer);
                                if (blockingObj == null)
                                    blockingObj = FindMonitor(thread.StackTrace[i].StackPointer, thread.StackBase);

                                blockingObj.Reason = BlockingReason.MonitorWait;
                            }
                            break;

                        case "WaitAny":
                        case "WaitAll":
                            if (type.Name == "System.Threading.WaitHandle")
                            {
                                ulong obj = FindWaitObjects(thread.StackLimit, thread.StackTrace[i].StackPointer, "System.Threading.WaitHandle[]");
                                if (obj == 0)
                                    obj = FindWaitObjects(thread.StackTrace[i].StackPointer, thread.StackBase, "System.Threading.WaitHandle[]");

                                if (obj != 0)
                                {
                                    BlockingReason reason = method.Name == "WaitAny" ? BlockingReason.WaitAny : BlockingReason.WaitAll;
                                    if (!_waitLocks.TryGetValue(obj, out blockingObj))
                                        _waitLocks[obj] = blockingObj = new DesktopBlockingObject(obj, true, 0, null, reason);
                                }
                            }
                            break;

                        case "WaitOne":
                        case "InternalWaitOne":
                        case "WaitOneNative":
                            if (type.Name == "System.Threading.WaitHandle")
                            {
                                if (eventTypes == null)
                                {
                                    eventTypes = new HashSet<string>();
                                    eventTypes.Add("System.Threading.Mutex");
                                    eventTypes.Add("System.Threading.Semaphore");
                                    eventTypes.Add("System.Threading.ManualResetEvent");
                                    eventTypes.Add("System.Threading.AutoResetEvent");
                                    eventTypes.Add("System.Threading.WaitHandle");
                                    eventTypes.Add("Microsoft.Win32.SafeHandles.SafeWaitHandle");
                                }

                                ulong obj = FindWaitHandle(thread.StackLimit, thread.StackTrace[i].StackPointer, eventTypes);
                                if (obj == 0)
                                    obj = FindWaitHandle(thread.StackTrace[i].StackPointer, thread.StackBase, eventTypes);

                                if (obj != 0)
                                {
                                    if (_waitLocks == null)
                                        _waitLocks = new Dictionary<ulong, DesktopBlockingObject>();

                                    if (!_waitLocks.TryGetValue(obj, out blockingObj))
                                        _waitLocks[obj] = blockingObj = new DesktopBlockingObject(obj, true, 0, null, BlockingReason.WaitOne);
                                }
                            }
                            break;


                        case "TryEnter":
                        case "ReliableEnterTimeout":
                        case "TryEnterTimeout":
                        case "Enter":
                            if (type.Name == "System.Threading.Monitor")
                            {
                                blockingObj = FindMonitor(thread.StackLimit, thread.StackTrace[i].StackPointer);
                                if (blockingObj != null)
                                    blockingObj.Reason = BlockingReason.Monitor;
                            }
                            break;
                    }


                    if (blockingObj != null)
                    {
                        bool alreadyEncountered = false;
                        foreach (var blobj in blobjs)
                        {
                            if (blobj.Object == blockingObj.Object)
                            {
                                alreadyEncountered = true;
                                break;
                            }
                        }

                        if (!alreadyEncountered)
                            blobjs.Add(blockingObj);
                    }
                }

                foreach (DesktopBlockingObject blobj in blobjs)
                    blobj.AddWaiter(thread);
                thread.SetBlockingObjects(blobjs.ToArray());
            }
        }


        private DesktopBlockingObject CreateRWLObject(ulong obj, ClrType type)
        {
            if (type == null)
                return new DesktopBlockingObject(obj, false, 0, null, BlockingReason.None);

            ClrInstanceField writerID = type.GetFieldByName("_dwWriterID");
            if (writerID != null && writerID.ElementType == ClrElementType.Int32)
            {
                int id = (int)writerID.GetValue(obj);
                if (id > 0)
                {
                    ClrThread thread = GetThreadById(id);
                    if (thread != null)
                        return new DesktopBlockingObject(obj, true, 0, thread, BlockingReason.ReaderAcquired);
                }
            }

            ClrInstanceField uLock = type.GetFieldByName("_dwULockID");
            ClrInstanceField lLock = type.GetFieldByName("_dwLLockID");

            if (uLock != null && uLock.ElementType == ClrElementType.Int32 && lLock != null && lLock.ElementType == ClrElementType.Int32)
            {
                int uId = (int)uLock.GetValue(obj);
                int lId = (int)lLock.GetValue(obj);


                List<ClrThread> threads = null;
                foreach (ClrThread thread in _runtime.Threads)
                {
                    foreach (IRWLockData l in _runtime.EnumerateLockData(thread.Address))
                    {
                        if (l.LLockID == lId && l.ULockID == uId && l.Level > 0)
                        {
                            if (threads == null)
                                threads = new List<ClrThread>();

                            threads.Add(thread);
                            break;
                        }
                    }
                }

                if (threads != null)
                    return new DesktopBlockingObject(obj, true, 0, BlockingReason.ReaderAcquired, threads.ToArray());
            }

            return new DesktopBlockingObject(obj, false, 0, null, BlockingReason.None);
        }

        private DesktopBlockingObject CreateRWSObject(ulong obj, ClrType type)
        {
            if (type == null)
                return new DesktopBlockingObject(obj, false, 0, null, BlockingReason.None);

            ClrInstanceField field = type.GetFieldByName("writeLockOwnerId");
            if (field != null && field.ElementType == ClrElementType.Int32)
            {
                int id = (int)field.GetValue(obj);
                ClrThread thread = GetThreadById(id);
                if (thread != null)
                    return new DesktopBlockingObject(obj, true, 0, thread, BlockingReason.WriterAcquired);
            }

            field = type.GetFieldByName("upgradeLockOwnerId");
            if (field != null && field.ElementType == ClrElementType.Int32)
            {
                int id = (int)field.GetValue(obj);
                ClrThread thread = GetThreadById(id);
                if (thread != null)
                    return new DesktopBlockingObject(obj, true, 0, thread, BlockingReason.WriterAcquired);
            }

            field = type.GetFieldByName("rwc");
            if (field != null)
            {
                List<ClrThread> threads = null;
                ulong rwc = (ulong)field.GetValue(obj);
                ClrType rwcArrayType = _heap.GetObjectType(rwc);
                if (rwcArrayType != null && rwcArrayType.IsArray && rwcArrayType.ComponentType != null)
                {
                    ClrType rwcType = rwcArrayType.ComponentType;
                    ClrInstanceField threadId = rwcType.GetFieldByName("threadid");
                    ClrInstanceField next = rwcType.GetFieldByName("next");
                    if (threadId != null && next != null)
                    {
                        int count = rwcArrayType.GetArrayLength(rwc);
                        for (int i = 0; i < count; ++i)
                        {
                            ulong entry = (ulong)rwcArrayType.GetArrayElementValue(rwc, i);
                            GetThreadEntry(ref threads, threadId, next, entry, false);
                        }
                    }
                }

                if (threads != null)
                    return new DesktopBlockingObject(obj, true, 0, BlockingReason.ReaderAcquired, threads.ToArray());
            }

            return new DesktopBlockingObject(obj, false, 0, null, BlockingReason.None);
        }

        private void GetThreadEntry(ref List<ClrThread> threads, ClrInstanceField threadId, ClrInstanceField next, ulong curr, bool interior)
        {
            if (curr == 0)
                return;

            int id = (int)threadId.GetValue(curr, interior);
            ClrThread thread = GetThreadById(id);
            if (thread != null)
            {
                if (threads == null)
                    threads = new List<ClrThread>();
                threads.Add(thread);
            }

            curr = (ulong)next.GetValue(curr, interior);
            if (curr != 0)
                GetThreadEntry(ref threads, threadId, next, curr, false);
        }

        private ulong FindWaitHandle(ulong start, ulong stop, HashSet<string> eventTypes)
        {
            ClrHeap heap = _runtime.GetHeap();
            foreach (ulong obj in EnumerateObjectsOfTypes(start, stop, eventTypes))
                return obj;

            return 0;
        }

        private ulong FindWaitObjects(ulong start, ulong stop, string typeName)
        {
            ClrHeap heap = _runtime.GetHeap();
            foreach (ulong obj in EnumerateObjectsOfType(start, stop, typeName))
                return obj;

            return 0;
        }

        private IEnumerable<ulong> EnumerateObjectsOfTypes(ulong start, ulong stop, HashSet<string> types)
        {
            ClrHeap heap = _runtime.GetHeap();
            foreach (ulong ptr in EnumeratePointersInRange(start, stop))
            {
                ulong obj;
                if (_runtime.ReadPointer(ptr, out obj))
                {
                    if (heap.IsInHeap(obj))
                    {
                        ClrType type = heap.GetObjectType(obj);

                        int sanity = 0;
                        while (type != null)
                        {
                            if (types.Contains(type.Name))
                            {
                                yield return obj;
                                break;
                            }

                            type = type.BaseType;

                            if (sanity++ == 16)
                                break;
                        }
                    }
                }
            }
        }


        private IEnumerable<ulong> EnumerateObjectsOfType(ulong start, ulong stop, string typeName)
        {
            ClrHeap heap = _runtime.GetHeap();
            foreach (ulong ptr in EnumeratePointersInRange(start, stop))
            {
                ulong obj;
                if (_runtime.ReadPointer(ptr, out obj))
                {
                    if (heap.IsInHeap(obj))
                    {
                        ClrType type = heap.GetObjectType(obj);


                        int sanity = 0;
                        while (type != null)
                        {
                            if (type.Name == typeName)
                            {
                                yield return obj;
                                break;
                            }

                            type = type.BaseType;

                            if (sanity++ == 16)
                                break;
                        }
                    }
                }
            }
        }

        private bool FindThread(ulong start, ulong stop, out ulong threadAddr, out ClrThread target)
        {
            ClrHeap heap = _runtime.GetHeap();
            foreach (ulong obj in EnumerateObjectsOfType(start, stop, "System.Threading.Thread"))
            {
                ClrType type = heap.GetObjectType(obj);
                ClrInstanceField threadIdField = type.GetFieldByName("m_ManagedThreadId");
                if (threadIdField != null && threadIdField.ElementType == ClrElementType.Int32)
                {
                    int id = (int)threadIdField.GetValue(obj);
                    ClrThread thread = GetThreadById(id);
                    if (thread != null)
                    {
                        threadAddr = obj;
                        target = thread;
                        return true;
                    }
                }
            }

            threadAddr = 0;
            target = null;
            return false;
        }

        private IEnumerable<ulong> EnumeratePointersInRange(ulong start, ulong stop)
        {
            uint diff = (uint)_runtime.PointerSize;

            if (start > stop)
                for (ulong ptr = stop; ptr <= start; ptr += diff)
                    yield return ptr;
            else
                for (ulong ptr = stop; ptr >= start; ptr -= diff)
                    yield return ptr;
        }



        private DesktopBlockingObject FindLocks(ulong start, ulong stop, Func<ulong, ClrType, bool> isCorrectType)
        {
            foreach (ulong ptr in EnumeratePointersInRange(start, stop))
            {
                ulong val = 0;
                if (_runtime.ReadPointer(ptr, out val))
                {
                    DesktopBlockingObject result = null;
                    if (_locks.TryGetValue(val, out result) && isCorrectType(val, _heap.GetObjectType(val)))
                        return result;
                }
            }

            return null;
        }

        private DesktopBlockingObject FindMonitor(ulong start, ulong stop)
        {
            ulong obj = 0;
            foreach (ulong ptr in EnumeratePointersInRange(start, stop))
            {
                ulong tmp = 0;
                if (_runtime.ReadPointer(ptr, out tmp))
                {
                    if (_syncblks.TryGetValue(tmp, out tmp))
                    {
                        obj = tmp;
                        break;
                    }
                }
            }

            DesktopBlockingObject result = null;
            if (obj != 0 && _monitors.TryGetValue(obj, out result))
                return result;

            return null;
        }

        private ClrThread GetThreadById(int id)
        {
            if (id < 0)
                return null;

            foreach (ClrThread thread in _runtime.Threads)
                if (thread.ManagedThreadId == id)
                    return thread;

            return null;
        }

        private const int HASHCODE_BITS = 25;
        private const int SYNCBLOCKINDEX_BITS = 26;
        private const uint BIT_SBLK_IS_HASH_OR_SYNCBLKINDEX = 0x08000000;
        private const uint BIT_SBLK_FINALIZER_RUN = 0x40000000;
        private const uint BIT_SBLK_SPIN_LOCK = 0x10000000;
        private const uint SBLK_MASK_LOCK_THREADID = 0x000003FF;   // special value of 0 + 1023 thread ids
        private const int SBLK_MASK_LOCK_RECLEVEL = 0x0000FC00;   // 64 recursion levels
        private const uint SBLK_APPDOMAIN_SHIFT = 16;           // shift right this much to get appdomain index
        private const uint SBLK_MASK_APPDOMAININDEX = 0x000007FF;   // 2048 appdomain indices
        private const int SBLK_RECLEVEL_SHIFT = 10;           // shift right this much to get recursion level
        private const uint BIT_SBLK_IS_HASHCODE = 0x04000000;
        private const uint MASK_HASHCODE = ((1 << HASHCODE_BITS) - 1);
        private const uint MASK_SYNCBLOCKINDEX = ((1 << SYNCBLOCKINDEX_BITS) - 1);
    }
}
