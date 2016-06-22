// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal class DesktopThreadPool : ClrThreadPool
    {
        private DesktopRuntimeBase _runtime;
        private ClrHeap _heap;
        private int _totalThreads;
        private int _runningThreads;
        private int _idleThreads;
        private int _minThreads;
        private int _maxThreads;
        private int _minCP;
        private int _maxCP;
        private int _cpu;
        private int _freeCP;
        private int _maxFreeCP;

        public DesktopThreadPool(DesktopRuntimeBase runtime, IThreadPoolData data)
        {
            _runtime = runtime;
            _totalThreads = data.TotalThreads;
            _runningThreads = data.RunningThreads;
            _idleThreads = data.IdleThreads;
            _minThreads = data.MinThreads;
            _maxThreads = data.MaxThreads;
            _minCP = data.MinCP;
            _maxCP = data.MaxCP;
            _cpu = data.CPU;
            _freeCP = data.NumFreeCP;
            _maxFreeCP = data.MaxFreeCP;
        }

        public override int TotalThreads
        {
            get { return _totalThreads; }
        }

        public override int RunningThreads
        {
            get { return _runningThreads; }
        }

        public override int IdleThreads
        {
            get { return _idleThreads; }
        }

        public override int MinThreads
        {
            get { return _minThreads; }
        }

        public override int MaxThreads
        {
            get { return _maxThreads; }
        }

        public override IEnumerable<NativeWorkItem> EnumerateNativeWorkItems()
        {
            return _runtime.EnumerateWorkItems();
        }

        public override IEnumerable<ManagedWorkItem> EnumerateManagedWorkItems()
        {
            foreach (ulong obj in EnumerateManagedThreadpoolObjects())
            {
                if (obj != 0)
                {
                    ClrType type = _heap.GetObjectType(obj);
                    if (type != null)
                        yield return new DesktopManagedWorkItem(type, obj);
                }
            }
        }

        private IEnumerable<ulong> EnumerateManagedThreadpoolObjects()
        {
            _heap = _runtime.GetHeap();

            ClrModule mscorlib = GetMscorlib();
            if (mscorlib != null)
            {
                ClrType queueType = mscorlib.GetTypeByName("System.Threading.ThreadPoolGlobals");
                if (queueType != null)
                {
                    ClrStaticField workQueueField = queueType.GetStaticFieldByName("workQueue");
                    if (workQueueField != null)
                    {
                        foreach (var appDomain in _runtime.AppDomains)
                        {
                            object workQueueValue = workQueueField.GetValue(appDomain);
                            ulong workQueue = workQueueValue == null ? 0L : (ulong)workQueueValue;
                            ClrType workQueueType = _heap.GetObjectType(workQueue);

                            if (workQueue == 0 || workQueueType == null)
                                continue;

                            ulong queueHead;
                            ClrType queueHeadType;
                            do
                            {
                                if (!GetFieldObject(workQueueType, workQueue, "queueHead", out queueHeadType, out queueHead))
                                    break;

                                ulong nodes;
                                ClrType nodesType;
                                if (GetFieldObject(queueHeadType, queueHead, "nodes", out nodesType, out nodes) && nodesType.IsArray)
                                {
                                    int len = nodesType.GetArrayLength(nodes);
                                    for (int i = 0; i < len; ++i)
                                    {
                                        ulong addr = (ulong)nodesType.GetArrayElementValue(nodes, i);
                                        if (addr != 0)
                                            yield return addr;
                                    }
                                }

                                if (!GetFieldObject(queueHeadType, queueHead, "Next", out queueHeadType, out queueHead))
                                    break;
                            } while (queueHead != 0);
                        }
                    }
                }


                queueType = mscorlib.GetTypeByName("System.Threading.ThreadPoolWorkQueue");
                if (queueType != null)
                {
                    ClrStaticField threadQueuesField = queueType.GetStaticFieldByName("allThreadQueues");
                    if (threadQueuesField != null)
                    {
                        foreach (ClrAppDomain domain in _runtime.AppDomains)
                        {
                            ulong? threadQueue = (ulong?)threadQueuesField.GetValue(domain);
                            if (!threadQueue.HasValue || threadQueue.Value == 0)
                                continue;

                            ClrType threadQueueType = _heap.GetObjectType(threadQueue.Value);
                            if (threadQueueType == null)
                                continue;

                            ulong outerArray = 0;
                            ClrType outerArrayType = null;
                            if (!GetFieldObject(threadQueueType, threadQueue.Value, "m_array", out outerArrayType, out outerArray) || !outerArrayType.IsArray)
                                continue;

                            int outerLen = outerArrayType.GetArrayLength(outerArray);
                            for (int i = 0; i < outerLen; ++i)
                            {
                                ulong entry = (ulong)outerArrayType.GetArrayElementValue(outerArray, i);
                                if (entry == 0)
                                    continue;

                                ClrType entryType = _heap.GetObjectType(entry);
                                if (entryType == null)
                                    continue;

                                ulong array;
                                ClrType arrayType;
                                if (!GetFieldObject(entryType, entry, "m_array", out arrayType, out array) || !arrayType.IsArray)
                                    continue;

                                int len = arrayType.GetArrayLength(array);
                                for (int j = 0; j < len; ++j)
                                {
                                    ulong addr = (ulong)arrayType.GetArrayElementValue(array, i);
                                    if (addr != 0)
                                        yield return addr;
                                }
                            }
                        }
                    }
                }
            }
        }

        private ClrModule GetMscorlib()
        {
            foreach (ClrModule module in _runtime.Modules)
                if (module.AssemblyName.Contains("mscorlib.dll"))
                    return module;

            // Uh oh, this shouldn't have happened.  Let's look more carefully (slowly).
            foreach (ClrModule module in _runtime.Modules)
                if (module.AssemblyName.ToLower().Contains("mscorlib"))
                    return module;

            // Ok...not sure why we couldn't find it.
            return null;
        }

        private bool GetFieldObject(ClrType type, ulong obj, string fieldName, out ClrType valueType, out ulong value)
        {
            value = 0;
            valueType = null;

            ClrInstanceField field = type.GetFieldByName(fieldName);
            if (field == null)
                return false;

            value = (ulong)field.GetValue(obj);
            if (value == 0)
                return false;

            valueType = _heap.GetObjectType(value);
            return valueType != null;
        }

        public override int MinCompletionPorts
        {
            get { return _minCP; }
        }

        public override int MaxCompletionPorts
        {
            get { return _maxCP; }
        }

        public override int CpuUtilization
        {
            get { return _cpu; }
        }

        public override int FreeCompletionPortCount
        {
            get { return _freeCP; }
        }

        public override int MaxFreeCompletionPorts
        {
            get { return _maxFreeCP; }
        }
    }

    internal class DesktopManagedWorkItem : ManagedWorkItem
    {
        private ClrType _type;
        private Address _addr;

        public DesktopManagedWorkItem(ClrType type, ulong addr)
        {
            _type = type;
            _addr = addr;
        }

        public override Address Object
        {
            get { return _addr; }
        }

        public override ClrType Type
        {
            get { return _type; }
        }
    }

    internal class DesktopNativeWorkItem : NativeWorkItem
    {
        private WorkItemKind _kind;
        private ulong _callback, _data;

        public DesktopNativeWorkItem(DacpWorkRequestData result)
        {
            _callback = result.Function;
            _data = result.Context;

            switch (result.FunctionType)
            {
                default:
                case WorkRequestFunctionTypes.UNKNOWNWORKITEM:
                    _kind = WorkItemKind.Unknown;
                    break;

                case WorkRequestFunctionTypes.TIMERDELETEWORKITEM:
                    _kind = WorkItemKind.TimerDelete;
                    break;

                case WorkRequestFunctionTypes.QUEUEUSERWORKITEM:
                    _kind = WorkItemKind.QueueUserWorkItem;
                    break;

                case WorkRequestFunctionTypes.ASYNCTIMERCALLBACKCOMPLETION:
                    _kind = WorkItemKind.AsyncTimer;
                    break;

                case WorkRequestFunctionTypes.ASYNCCALLBACKCOMPLETION:
                    _kind = WorkItemKind.AsyncCallback;
                    break;
            }
        }


        public DesktopNativeWorkItem(V45WorkRequestData result)
        {
            _callback = result.Function;
            _data = result.Context;
            _kind = WorkItemKind.Unknown;
        }

        public override WorkItemKind Kind
        {
            get { return _kind; }
        }

        public override Address Callback
        {
            get { return _callback; }
        }

        public override Address Data
        {
            get { return _data; }
        }
    }
}
