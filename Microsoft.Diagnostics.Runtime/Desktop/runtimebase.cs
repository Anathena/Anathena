// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Address = System.UInt64;
using System.Linq;
using Microsoft.Diagnostics.Runtime.ICorDebug;

#pragma warning disable 649

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal enum DesktopVersion
    {
        v2,
        v4,
        v45
    }

    abstract internal class DesktopRuntimeBase : RuntimeBase
    {
        #region Variables
        protected CommonMethodTables _commonMTs;
        private Dictionary<uint, ICorDebug.ICorDebugThread> _corDebugThreads;
        private Dictionary<Address, DesktopModule> _modules = new Dictionary<Address, DesktopModule>();
        private Dictionary<ulong, uint> _moduleSizes = null;
        private ClrModule[] _moduleList = null;
        private Dictionary<string, DesktopModule> _moduleFiles = null;
        private DesktopAppDomain _system, _shared;
        private List<ClrAppDomain> _domains;
        private List<ClrThread> _threads;
        private DesktopGCHeap _heap;
        private DesktopThreadPool _threadpool;
        private ErrorModule _errorModule;
        #endregion

        internal int Revision { get; set; }

        public ErrorModule ErrorModule
        {
            get
            {
                if (_errorModule == null)
                    _errorModule = new ErrorModule(this);

                return _errorModule;
            }
        }

        internal override IGCInfo GetGCInfo()
        {
            IGCInfo data = GetGCInfoImpl();
            if (data == null)
            {
                throw new ClrDiagnosticsException("This runtime is not initialized and contains no data.", ClrDiagnosticsException.HR.RuntimeUninitialized);
            }

            return data;
        }

        public override IEnumerable<ClrException> EnumerateSerializedExceptions()
        {
            return new ClrException[0];
        }

        public override IEnumerable<int> EnumerateGCThreads()
        {
            foreach (uint thread in _dataReader.EnumerateAllThreads())
            {
                ulong teb = _dataReader.GetThreadTeb(thread);
                int threadType = DesktopThread.GetTlsSlotForThread(this, teb);
                if ((threadType & (int)DesktopThread.TlsThreadType.ThreadType_GC) == (int)DesktopThread.TlsThreadType.ThreadType_GC)
                    yield return (int)thread;
            }
        }

        internal DesktopGCHeap TryGetHeap()
        {
            return _heap;
        }


        /// <summary>
        /// Returns the version of the target process (v2, v4, v45)
        /// </summary>
        internal abstract DesktopVersion CLRVersion { get; }

        internal abstract IGCInfo GetGCInfoImpl();

        /// <summary>
        /// Returns the pointer size of the target process.
        /// </summary>
        public override int PointerSize
        {
            get
            {
                return IntPtr.Size;
            }
        }

        /// <summary>
        /// Returns the MethodTable for an array of objects.
        /// </summary>
        public ulong ArrayMethodTable
        {
            get
            {
                return _commonMTs.ArrayMethodTable;
            }
        }

        internal DesktopModule GetModule(Address module)
        {
            if (module == 0)
                return null;

            DesktopModule res;
            if (_modules.TryGetValue(module, out res))
                return res;

            IModuleData moduleData = GetModuleData(module);
            if (moduleData == null)
                return null;

            string peFile = GetPEFileName(moduleData.PEFile);
            string assemblyName = GetAssemblyName(moduleData.Assembly);

            if (_moduleSizes == null)
            {
                _moduleSizes = new Dictionary<Address, uint>();
                foreach (var native in _dataReader.EnumerateModules())
                    _moduleSizes[native.ImageBase] = native.FileSize;
            }

            if (_moduleFiles == null)
                _moduleFiles = new Dictionary<string, DesktopModule>();

            uint size = 0;
            _moduleSizes.TryGetValue(moduleData.ImageBase, out size);
            if (peFile == null)
            {
                res = new DesktopModule(this, module, moduleData, peFile, assemblyName, size);
            }
            else if (!_moduleFiles.TryGetValue(peFile, out res))
            {
                res = new DesktopModule(this, module, moduleData, peFile, assemblyName, size);
                _moduleFiles[peFile] = res;
            }

            _modules[module] = res;
            return res;
        }


        public override CcwData GetCcwDataByAddress(Address addr)
        {
            var ccw = GetCCWData(addr);
            if (ccw == null)
                return null;

            return new DesktopCCWData((DesktopGCHeap)GetHeap(), addr, ccw);
        }

        internal ICorDebugThread GetCorDebugThread(uint osid)
        {
            if (_corDebugThreads == null)
            {
                _corDebugThreads = new Dictionary<uint, ICorDebugThread>();

                ICorDebugProcess process = CorDebugProcess;
                if (process == null)
                    return null;

                ICorDebugThreadEnum threadEnum;
                process.EnumerateThreads(out threadEnum);

                uint fetched;
                ICorDebugThread[] threads = new ICorDebugThread[1];
                while (threadEnum.Next(1, threads, out fetched) == 0 && fetched == 1)
                {
                    try
                    {
                        uint id;
                        threads[0].GetID(out id);
                        _corDebugThreads[id] = threads[0];
                    }
                    catch
                    {
                    }
                }
            }

            ICorDebugThread result;
            _corDebugThreads.TryGetValue(osid, out result);
            return result;
        }

        public override IList<ClrAppDomain> AppDomains
        {
            get
            {
                if (_domains == null)
                    InitDomains();

                return _domains;
            }
        }

        /// <summary>
        /// Enumerates all managed threads in the process.  Only threads which have previously run managed
        /// code will be enumerated.
        /// </summary>
        public override IList<ClrThread> Threads
        {
            get
            {
                if (_threads == null)
                    InitThreads();

                return _threads;
            }
        }

        private void InitThreads()
        {
            if (_threads == null)
            {
                IThreadStoreData threadStore = GetThreadStoreData();
                ulong finalizer = ulong.MaxValue - 1;
                if (threadStore != null)
                    finalizer = threadStore.Finalizer;

                List<ClrThread> threads = new List<ClrThread>();

                // Give a max number of threads to walk to ensure no infinite loops due to data
                // inconsistency.
                int max = 4098;
                ulong addr = GetFirstThread();
                IThreadData thread = GetThread(addr);

                while (max-- > 0 && thread != null)
                {
                    threads.Add(new DesktopThread(this, thread, addr, addr == finalizer));
                    addr = thread.Next;
                    thread = GetThread(addr);
                }

                _threads = threads;
            }
        }

        public Address ExceptionMethodTable { get { return _commonMTs.ExceptionMethodTable; } }
        public ulong ObjectMethodTable
        {
            get
            {
                return _commonMTs.ObjectMethodTable;
            }
        }

        /// <summary>
        /// Returns the MethodTable for string objects.
        /// </summary>
        public ulong StringMethodTable
        {
            get
            {
                return _commonMTs.StringMethodTable;
            }
        }

        /// <summary>
        /// Returns the MethodTable for free space markers.
        /// </summary>
        public ulong FreeMethodTable
        {
            get
            {
                return _commonMTs.FreeMethodTable;
            }
        }

        public override ClrHeap GetHeap()
        {
            if (_heap == null)
            {
                if (HasArrayComponentMethodTables)
                    _heap = new LegacyGCHeap(this);
                else
                    _heap = new V46GCHeap(this);
            }

            return _heap;
        }

        public override ClrThreadPool GetThreadPool()
        {
            if (_threadpool != null)
                return _threadpool;

            IThreadPoolData data = GetThreadPoolData();
            if (data == null)
                return null;

            _threadpool = new DesktopThreadPool(this, data);
            return _threadpool;
        }


        /// <summary>
        /// The address of the system domain in CLR.
        /// </summary>
        public ulong SystemDomainAddress
        {
            get
            {
                if (_domains == null)
                    InitDomains();

                if (_system == null)
                    return 0;

                return _system.Address;
            }
        }

        /// <summary>
        /// The address of the shared domain in CLR.
        /// </summary>
        public ulong SharedDomainAddress
        {
            get
            {
                if (_domains == null)
                    InitDomains();

                if (_shared == null)
                    return 0;

                return _shared.Address;
            }
        }

        /// <summary>
        /// The address of the system domain in CLR.
        /// </summary>
        public override ClrAppDomain SystemDomain
        {
            get
            {
                if (_domains == null)
                    InitDomains();

                return _system;
            }
        }

        /// <summary>
        /// The address of the shared domain in CLR.
        /// </summary>
        public override ClrAppDomain SharedDomain
        {
            get
            {
                if (_domains == null)
                    InitDomains();

                return _shared;
            }
        }

        public bool IsSingleDomain
        {
            get
            {
                if (_domains == null)
                    InitDomains();

                return _domains.Count == 1;
            }
        }

        public override ClrMethod GetMethodByHandle(ulong methodHandle)
        {
            if (methodHandle == 0)
                return null;

            IMethodDescData methodDesc = GetMethodDescData(methodHandle);
            if (methodDesc == null)
                return null;

            ClrType type = GetHeap().GetTypeByMethodTable(methodDesc.MethodTable);
            if (type == null)
                return null;

            return type.GetMethod(methodDesc.MDToken);
        }

        /// <summary>
        /// Enumerates regions of memory which CLR has allocated with a description of what data
        /// resides at that location.  Note that this does not return every chunk of address space
        /// that CLR allocates.
        /// </summary>
        /// <returns>An enumeration of memory regions in the process.</returns>
        public override IEnumerable<ClrMemoryRegion> EnumerateMemoryRegions()
        {
            // Enumerate GC Segment regions.
            IHeapDetails[] heaps;
            if (ServerGC)
            {
                heaps = new IHeapDetails[HeapCount];
                int i = 0;
                Address[] heapList = GetServerHeapList();
                if (heapList != null)
                {
                    foreach (ulong addr in heapList)
                    {
                        heaps[i++] = GetSvrHeapDetails(addr);
                        if (i == heaps.Length)
                            break;
                    }
                }
                else
                {
                    heaps = new IHeapDetails[0];
                }
            }
            else
            {
                Debug.Assert(HeapCount == 1);
                heaps = new IHeapDetails[1];
                heaps[0] = GetWksHeapDetails();
            }

            int max = 2048;  // Max number of segments in case of inconsistent data.
            for (int i = 0; i < heaps.Length; ++i)
            {
                // Small heap
                ISegmentData segment = GetSegmentData(heaps[i].FirstHeapSegment);
                while (segment != null && max-- > 0)
                {
                    Debug.Assert(segment.Start < segment.Committed);
                    Debug.Assert(segment.Committed <= segment.Reserved);

                    GCSegmentType type = (segment.Address == heaps[i].EphemeralSegment) ? GCSegmentType.Ephemeral : GCSegmentType.Regular;
                    yield return new MemoryRegion(this, segment.Start, segment.Committed - segment.Start, ClrMemoryRegionType.GCSegment, (uint)i, type);
                    yield return new MemoryRegion(this, segment.Committed, segment.Reserved - segment.Committed, ClrMemoryRegionType.ReservedGCSegment, (uint)i, type);

                    if (segment.Address == segment.Next || segment.Address == 0)
                        segment = null;
                    else
                        segment = GetSegmentData(segment.Next);
                }

                segment = GetSegmentData(heaps[i].FirstLargeHeapSegment);
                while (segment != null && max-- > 0)
                {
                    Debug.Assert(segment.Start < segment.Committed);
                    Debug.Assert(segment.Committed <= segment.Reserved);

                    yield return new MemoryRegion(this, segment.Start, segment.Committed - segment.Start, ClrMemoryRegionType.GCSegment, (uint)i, GCSegmentType.LargeObject);
                    yield return new MemoryRegion(this, segment.Committed, segment.Reserved - segment.Committed, ClrMemoryRegionType.ReservedGCSegment, (uint)i, GCSegmentType.LargeObject);

                    if (segment.Address == segment.Next || segment.Address == 0)
                        segment = null;
                    else
                        segment = GetSegmentData(segment.Next);
                }
            }

            // Enumerate handle table regions.
            HashSet<ulong> regions = new HashSet<ulong>();
            foreach (ClrHandle handle in EnumerateHandles())
            {
                VirtualQueryData vq;
                if (!_dataReader.VirtualQuery(handle.Address, out vq))
                    continue;

                if (regions.Contains(vq.BaseAddress))
                    continue;

                regions.Add(vq.BaseAddress);
                yield return new MemoryRegion(this, vq.BaseAddress, vq.Size, ClrMemoryRegionType.HandleTableChunk, handle.AppDomain);
            }

            // Enumerate each AppDomain and Module specific heap.
            AppDomainHeapWalker adhw = new AppDomainHeapWalker(this);
            IAppDomainData ad = GetAppDomainData(SystemDomainAddress);
            foreach (MemoryRegion region in adhw.EnumerateHeaps(ad))
                yield return region;

            foreach (ulong module in EnumerateModules(ad))
                foreach (MemoryRegion region in adhw.EnumerateModuleHeaps(ad, module))
                    yield return region;

            ad = GetAppDomainData(SharedDomainAddress);
            foreach (MemoryRegion region in adhw.EnumerateHeaps(ad))
                yield return region;

            foreach (ulong module in EnumerateModules(ad))
                foreach (MemoryRegion region in adhw.EnumerateModuleHeaps(ad, module))
                    yield return region;

            IAppDomainStoreData ads = GetAppDomainStoreData();
            if (ads != null)
            {
                IList<ulong> appDomains = GetAppDomainList(ads.Count);
                if (appDomains != null)
                {
                    foreach (ulong addr in appDomains)
                    {
                        ad = GetAppDomainData(addr);
                        foreach (MemoryRegion region in adhw.EnumerateHeaps(ad))
                            yield return region;

                        foreach (ulong module in EnumerateModules(ad))
                            foreach (MemoryRegion region in adhw.EnumerateModuleHeaps(ad, module))
                                yield return region;
                    }
                }
            }


            // Enumerate each JIT code heap.
            regions.Clear();
            foreach (ICodeHeap jitHeap in EnumerateJitHeaps())
            {
                if (jitHeap.Type == CodeHeapType.Host)
                {
                    VirtualQueryData vq;
                    if (_dataReader.VirtualQuery(jitHeap.Address, out vq))
                        yield return new MemoryRegion(this, vq.BaseAddress, vq.Size, ClrMemoryRegionType.JitHostCodeHeap);
                    else
                        yield return new MemoryRegion(this, jitHeap.Address, 0, ClrMemoryRegionType.JitHostCodeHeap);
                }
                else if (jitHeap.Type == CodeHeapType.Loader)
                {
                    foreach (MemoryRegion region in adhw.EnumerateJitHeap(jitHeap.Address))
                        yield return region;
                }
            }
        }

        /// <summary>
        /// Converts an address into an AppDomain.
        /// </summary>
        internal override ClrAppDomain GetAppDomainByAddress(ulong address)
        {
            foreach (var ad in AppDomains)
                if (ad.Address == address)
                    return ad;

            return null;
        }

        /// <summary>
        /// Flushes the dac cache.  This function MUST be called any time you expect to call the same function
        /// but expect different results.  For example, after walking the heap, you need to call Flush before
        /// attempting to walk the heap again.
        /// </summary>
        public override void Flush()
        {
            OnRuntimeFlushed();

            Revision++;
            _dacInterface.Flush();

            _modules.Clear();
            _moduleFiles = null;
            _moduleSizes = null;
            _domains = null;
            _system = null;
            _shared = null;
            _threads = null;
            MemoryReader = null;
            _heap = null;
            _threadpool = null;
        }

        public override ClrMethod GetMethodByAddress(Address ip)
        {
            IMethodDescData mdData = GetMDForIP(ip);
            if (mdData == null)
                return null;

            return DesktopMethod.Create(this, mdData);
        }

        internal IEnumerable<IRWLockData> EnumerateLockData(ulong thread)
        {
            // add offset of the m_pHead (tagLockEntry) field
            thread += GetRWLockDataOffset();
            ulong firstEntry;
            if (ReadPointer(thread, out firstEntry))
            {
                ulong lockEntry = firstEntry;
                byte[] output = GetByteArrayForStruct<RWLockData>();
                do
                {
                    int read;
                    if (!ReadMemory(lockEntry, output, output.Length, out read) || read != output.Length)
                        break;

                    IRWLockData result = ConvertStruct<IRWLockData, RWLockData>(output);
                    if (result != null)
                        yield return result;

                    if (result.Next == lockEntry)
                        break;

                    lockEntry = result.Next;
                } while (lockEntry != firstEntry);
            }
        }

        #region Internal Functions
        protected ClrThread GetThreadByStackAddress(ulong address)
        {
            Debug.Assert(address != 0);
            foreach (ClrThread thread in _threads)
            {
                ulong min = thread.StackBase;
                ulong max = thread.StackLimit;

                if (min > max)
                {
                    ulong tmp = min;
                    min = max;
                    max = tmp;
                }

                if (min <= address && address <= max)
                    return thread;
            }

            return null;
        }

        internal uint GetExceptionMessageOffset()
        {
            if (PointerSize == 8)
                return 0x20;

            return 0x10;
        }

        internal uint GetStackTraceOffset()
        {
            if (PointerSize == 8)
                return 0x40;

            return 0x20;
        }

        internal ClrThread GetThreadFromThinlockID(uint threadId)
        {
            Address thread = GetThreadFromThinlock(threadId);
            if (thread == 0)
                return null;

            if (_threads == null)
                InitThreads();

            foreach (var clrThread in _threads)
                if (clrThread.Address == thread)
                    return clrThread;

            return null;
        }

        public override IList<ClrModule> Modules
        {
            get
            {
                if (_domains == null)
                    InitDomains();

                if (_moduleList == null)
                {
                    HashSet<ClrModule> modules = new HashSet<ClrModule>(_modules.Values.Select(p => (ClrModule)p));
                    _moduleList = modules.ToArray();
                }

                return _moduleList;
            }
        }

        internal IEnumerable<ulong> EnumerateModules(IAppDomainData appDomain)
        {
            if (appDomain != null)
            {
                ulong[] assemblies = GetAssemblyList(appDomain.Address, appDomain.AssemblyCount);
                if (assemblies != null)
                {
                    foreach (ulong assembly in assemblies)
                    {
                        IAssemblyData data = GetAssemblyData(appDomain.Address, assembly);
                        if (data == null)
                            continue;

                        Address[] moduleList = GetModuleList(assembly, (int)data.ModuleCount);
                        if (moduleList != null)
                            foreach (ulong module in moduleList)
                                yield return module;
                    }
                }
            }
        }

        internal DesktopRuntimeBase(ClrInfo info, DataTargetImpl dt, DacLibrary lib)
            : base(info, dt, lib)
        {
        }

        internal void InitDomains()
        {
            if (_domains != null)
                return;

            _modules.Clear();
            _domains = new List<ClrAppDomain>();

            IAppDomainStoreData ads = GetAppDomainStoreData();
            if (ads == null)
                return;

            IList<ulong> domains = GetAppDomainList(ads.Count);
            foreach (ulong domain in domains)
            {
                DesktopAppDomain appDomain = InitDomain(domain);
                if (appDomain != null)
                    _domains.Add(appDomain);
            }

            _system = InitDomain(ads.SystemDomain, "System Domain");
            _shared = InitDomain(ads.SharedDomain, "Shared Domain");

            _moduleFiles = null;
            _moduleSizes = null;
        }

        private DesktopAppDomain InitDomain(ulong domain, string name = null)
        {
            ulong[] bases = new ulong[1];
            IAppDomainData domainData = GetAppDomainData(domain);
            if (domainData == null)
                return null;

            DesktopAppDomain appDomain = new DesktopAppDomain(this, domainData, name ?? GetAppDomaminName(domain));

            if (domainData.AssemblyCount > 0)
            {
                foreach (ulong assembly in GetAssemblyList(domain, domainData.AssemblyCount))
                {
                    IAssemblyData assemblyData = GetAssemblyData(domain, assembly);
                    if (assemblyData == null)
                        continue;

                    if (assemblyData.ModuleCount > 0)
                    {
                        foreach (ulong module in GetModuleList(assembly, assemblyData.ModuleCount))
                        {
                            DesktopModule clrModule = GetModule(module);
                            if (clrModule != null)
                            {
                                clrModule.AddMapping(appDomain, module);
                                appDomain.AddModule(clrModule);
                            }
                        }
                    }
                }
            }

            return appDomain;
        }

        private IEnumerable<DesktopModule> EnumerateImages()
        {
            InitDomains();
            foreach (var module in _modules.Values)
                if (module.ImageBase != 0)
                    yield return module;
        }

        private IEnumerable<ulong> EnumerateImageBases(IEnumerable<DesktopModule> modules)
        {
            foreach (var module in modules)
                yield return module.ImageBase;
        }

        /// <summary>
        /// 
        /// Returns the name of the type as specified by the TypeHandle.  Note this returns the name as specified by the
        /// metadata, NOT as you would expect to see it in a C# program.  For example, generics are denoted with a ` and
        /// the number of params.  Thus a Dictionary (with two type params) would look like:
        ///     System.Collections.Generics.Dictionary`2
        /// </summary>
        /// <param name="id">The TypeHandle to get the name of.</param>
        /// <returns>The name of the type, or null on error.</returns>
        internal string GetTypeName(TypeHandle id)
        {
            if (id.MethodTable == FreeMethodTable)
                return "Free";

            if (id.MethodTable == ArrayMethodTable && id.ComponentMethodTable != 0)
            {
                string name = GetNameForMT(id.ComponentMethodTable);
                if (name != null)
                    return name + "[]";
            }

            return GetNameForMT(id.MethodTable);
        }

        protected IXCLRDataProcess GetClrDataProcess()
        {
            return _dacInterface;
        }


        internal IEnumerable<ClrStackFrame> EnumerateStackFrames(DesktopThread thread)
        {
            IXCLRDataProcess proc = GetClrDataProcess();
            object tmp;

            int res = proc.GetTaskByOSThreadID(thread.OSThreadId, out tmp);
            if (res < 0)
                yield break;

            IXCLRDataTask task = null;
            IXCLRDataStackWalk stackwalk = null;

            try
            {
                task = (IXCLRDataTask)tmp;
                res = task.CreateStackWalk(0xf, out tmp);
                if (res < 0)
                    yield break;

                stackwalk = (IXCLRDataStackWalk)tmp;
                byte[] ulongBuffer = new byte[8];
                byte[] context = ContextHelper.Context;
                do
                {
                    uint size;
                    res = stackwalk.GetContext(ContextHelper.ContextFlags, ContextHelper.Length, out size, context);
                    if (res < 0 || res == 1)
                        break;

                    ulong ip = BitConverter.ToUInt32(context, ContextHelper.InstructionPointerOffset);
                    ulong sp = BitConverter.ToUInt32(context, ContextHelper.StackPointerOffset);

                    res = stackwalk.Request(0xf0000000, 0, null, (uint)ulongBuffer.Length, ulongBuffer);

                    ulong frameVtbl = 0;
                    if (res >= 0)
                    {
                        frameVtbl = BitConverter.ToUInt64(ulongBuffer, 0);
                        if (frameVtbl != 0)
                        {
                            sp = frameVtbl;
                            ReadPointer(sp, out frameVtbl);
                        }
                    }

                    DesktopStackFrame frame = GetStackFrame(thread, res, ip, sp, frameVtbl);
                    yield return frame;
                } while (stackwalk.Next() == 0);
            }
            finally
            {
                if (task != null)
                    Marshal.FinalReleaseComObject(task);

                if (stackwalk != null)
                    Marshal.FinalReleaseComObject(stackwalk);
            }
        }

        internal ILToNativeMap[] GetILMap(Address ip)
        {
            List<ILToNativeMap> list = null;
            ILToNativeMap[] tmp = null;

            ulong handle;
            int res = _dacInterface.StartEnumMethodInstancesByAddress(ip, null, out handle);
            if (res < 0)
                return null;

            object objMethod;
            res = _dacInterface.EnumMethodInstanceByAddress(ref handle, out objMethod);

            while (res == 0)
            {
                IXCLRDataMethodInstance method = (IXCLRDataMethodInstance)objMethod;

                uint needed = 0;
                res = method.GetILAddressMap(0, out needed, null);

                if (res == 0)
                {
                    tmp = new ILToNativeMap[needed];
                    res = method.GetILAddressMap(needed, out needed, tmp);

                    for (int i = 0; i < tmp.Length; i++)
                    {
                        // There seems to be a bug in IL to native mappings where a throw statement
                        // may end up with an end address lower than the start address.  This is a
                        // workaround for that issue.
                        if (tmp[i].StartAddress > tmp[i].EndAddress)
                        {
                            if (i + 1 == tmp.Length)
                                tmp[i].EndAddress = tmp[i].StartAddress + 0x20;
                            else
                                tmp[i].EndAddress = tmp[i + 1].StartAddress - 1;
                        }
                    }

                    if (res != 0)
                        tmp = null;
                }

                res = _dacInterface.EnumMethodInstanceByAddress(ref handle, out objMethod);
                if (res == 0 && tmp != null)
                {
                    if (list == null)
                        list = new List<ILToNativeMap>();

                    list.AddRange(tmp);
                }
            }

            if (list != null)
            {
                list.AddRange(tmp);
                return list.ToArray();
            }

            _dacInterface.EndEnumMethodInstancesByAddress(handle);
            return tmp;
        }

        #endregion

        #region Abstract Functions
        internal abstract uint GetExceptionHROffset();
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void LoaderHeapTraverse(ulong address, IntPtr size, int isCurrent);
        internal abstract IList<ulong> GetAppDomainList(int count);
        internal abstract ulong[] GetAssemblyList(ulong appDomain, int count);
        internal abstract ulong[] GetModuleList(ulong assembly, int count);
        internal abstract IAssemblyData GetAssemblyData(ulong domain, ulong assembly);
        internal abstract IAppDomainStoreData GetAppDomainStoreData();
        internal abstract bool GetCommonMethodTables(ref CommonMethodTables mCommonMTs);
        internal abstract string GetNameForMT(ulong mt);
        internal abstract string GetPEFileName(ulong addr);
        internal abstract IModuleData GetModuleData(ulong addr);
        internal abstract IAppDomainData GetAppDomainData(ulong addr);
        internal abstract string GetAppDomaminName(ulong addr);
        internal abstract bool TraverseHeap(ulong heap, LoaderHeapTraverse callback);
        internal abstract bool TraverseStubHeap(ulong appDomain, int type, LoaderHeapTraverse callback);
        internal abstract IEnumerable<ICodeHeap> EnumerateJitHeaps();
        internal abstract ulong GetModuleForMT(ulong mt);
        internal abstract IFieldInfo GetFieldInfo(Address mt);
        internal abstract IFieldData GetFieldData(Address fieldDesc);
        internal abstract ICorDebug.IMetadataImport GetMetadataImport(Address module);
        internal abstract IObjectData GetObjectData(Address objRef);
        internal abstract ulong GetMethodTableByEEClass(ulong eeclass);
        internal abstract IList<MethodTableTokenPair> GetMethodTableList(Address module);
        internal abstract IDomainLocalModuleData GetDomainLocalModule(Address appDomain, Address id);
        internal abstract ICCWData GetCCWData(Address ccw);
        internal abstract IRCWData GetRCWData(Address rcw);
        internal abstract COMInterfacePointerData[] GetCCWInterfaces(Address ccw, int count);
        internal abstract COMInterfacePointerData[] GetRCWInterfaces(Address rcw, int count);
        internal abstract ulong GetThreadStaticPointer(ulong thread, ClrElementType type, uint offset, uint moduleId, bool shared);
        internal abstract IDomainLocalModuleData GetDomainLocalModule(Address module);
        internal abstract IList<Address> GetMethodDescList(Address methodTable);
        internal abstract string GetNameForMD(Address md);
        internal abstract IMethodDescData GetMethodDescData(Address md);
        internal abstract uint GetMetadataToken(Address mt);
        protected abstract DesktopStackFrame GetStackFrame(DesktopThread thread, int res, ulong ip, ulong sp, ulong frameVtbl);
        internal abstract IList<ClrStackFrame> GetExceptionStackTrace(Address obj, ClrType type);
        internal abstract string GetAssemblyName(Address assembly);
        internal abstract string GetAppBase(Address appDomain);
        internal abstract string GetConfigFile(Address appDomain);
        internal abstract IMethodDescData GetMDForIP(ulong ip);
        protected abstract Address GetThreadFromThinlock(uint threadId);
        internal abstract int GetSyncblkCount();
        internal abstract ISyncBlkData GetSyncblkData(int index);
        internal abstract IThreadPoolData GetThreadPoolData();
        protected abstract uint GetRWLockDataOffset();
        internal abstract IEnumerable<NativeWorkItem> EnumerateWorkItems();
        internal abstract uint GetStringFirstCharOffset();
        internal abstract uint GetStringLengthOffset();
        #endregion


    }


    internal struct MethodTableTokenPair
    {
        public ulong MethodTable { get; set; }
        public uint Token { get; set; }

        public MethodTableTokenPair(ulong methodTable, uint token)
        {
            MethodTable = methodTable;
            Token = token;
        }
    }

    internal class MemoryRegion : ClrMemoryRegion
    {
        #region Private Variables
        private DesktopRuntimeBase _runtime;
        private ulong _domainModuleHeap;
        private GCSegmentType _segmentType;
        #endregion

        private bool HasAppDomainData
        {
            get
            {
                return Type <= ClrMemoryRegionType.CacheEntryHeap || Type == ClrMemoryRegionType.HandleTableChunk;
            }
        }

        private bool HasModuleData
        {
            get
            {
                return Type == ClrMemoryRegionType.ModuleThunkHeap || Type == ClrMemoryRegionType.ModuleLookupTableHeap;
            }
        }

        private bool HasGCHeapData
        {
            get
            {
                return Type == ClrMemoryRegionType.GCSegment || Type == ClrMemoryRegionType.ReservedGCSegment;
            }
        }


        public override ClrAppDomain AppDomain
        {
            get
            {
                if (!HasAppDomainData)
                    return null;
                return _runtime.GetAppDomainByAddress(_domainModuleHeap);
            }
        }

        public override string Module
        {
            get
            {
                if (!HasModuleData)
                    return null;

                return _runtime.GetModule(_domainModuleHeap).FileName;
            }
        }

        public override int HeapNumber
        {
            get
            {
                if (!HasGCHeapData)
                    return -1;

                Debug.Assert(_domainModuleHeap < uint.MaxValue);
                return (int)_domainModuleHeap;
            }
            set
            {
                _domainModuleHeap = (ulong)value;
            }
        }

        public override GCSegmentType GCSegmentType
        {
            get
            {
                if (!HasGCHeapData)
                    throw new NotSupportedException();

                return _segmentType;
            }
            set
            {
                _segmentType = value;
            }
        }

        public override string ToString(bool detailed)
        {
            string value = null;

            switch (Type)
            {
                case ClrMemoryRegionType.LowFrequencyLoaderHeap:
                    value = "Low Frequency Loader Heap";
                    break;

                case ClrMemoryRegionType.HighFrequencyLoaderHeap:
                    value = "High Frequency Loader Heap";
                    break;

                case ClrMemoryRegionType.StubHeap:
                    value = "Stub Heap";
                    break;

                // Virtual Call Stub heaps
                case ClrMemoryRegionType.IndcellHeap:
                    value = "Indirection Cell Heap";
                    break;

                case ClrMemoryRegionType.LookupHeap:
                    value = "Loopup Heap";
                    break;

                case ClrMemoryRegionType.ResolveHeap:
                    value = "Resolver Heap";
                    break;

                case ClrMemoryRegionType.DispatchHeap:
                    value = "Dispatch Heap";
                    break;

                case ClrMemoryRegionType.CacheEntryHeap:
                    value = "Cache Entry Heap";
                    break;

                // Other regions
                case ClrMemoryRegionType.JitHostCodeHeap:
                    value = "JIT Host Code Heap";
                    break;

                case ClrMemoryRegionType.JitLoaderCodeHeap:
                    value = "JIT Loader Code Heap";
                    break;

                case ClrMemoryRegionType.ModuleThunkHeap:
                    value = "Thunk Heap";
                    break;

                case ClrMemoryRegionType.ModuleLookupTableHeap:
                    value = "Lookup Table Heap";
                    break;

                case ClrMemoryRegionType.HandleTableChunk:
                    value = "GC Handle Table Chunk";
                    break;

                case ClrMemoryRegionType.ReservedGCSegment:
                case ClrMemoryRegionType.GCSegment:
                    if (_segmentType == GCSegmentType.Ephemeral)
                        value = "Ephemeral Segment";
                    else if (_segmentType == GCSegmentType.LargeObject)
                        value = "Large Object Segment";
                    else
                        value = "GC Segment";

                    if (Type == ClrMemoryRegionType.ReservedGCSegment)
                        value += " (Reserved)";
                    break;

                default:
                    // should never happen.
                    value = "<unknown>";
                    break;
            }

            if (detailed)
            {
                if (HasAppDomainData)
                {
                    if (_domainModuleHeap == _runtime.SharedDomainAddress)
                    {
                        value = string.Format("{0} for Shared AppDomain", value);
                    }
                    else if (_domainModuleHeap == _runtime.SystemDomainAddress)
                    {
                        value = string.Format("{0} for System AppDomain", value);
                    }
                    else
                    {
                        ClrAppDomain domain = AppDomain;
                        value = string.Format("{0} for AppDomain {1}: {2}", value, domain.Id, domain.Name);
                    }
                }
                else if (HasModuleData)
                {
                    string fn = _runtime.GetModule(_domainModuleHeap).FileName;
                    value = string.Format("{0} for Module: {1}", value, Path.GetFileName(fn));
                }
                else if (HasGCHeapData)
                {
                    value = string.Format("{0} for Heap {1}", value, HeapNumber);
                }
            }

            return value;
        }

        /// <summary>
        /// Equivalent to GetDisplayString(false).
        /// </summary>
        public override string ToString()
        {
            return ToString(false);
        }

        #region Constructors
        internal MemoryRegion(DesktopRuntimeBase clr, ulong addr, ulong size, ClrMemoryRegionType type, ulong moduleOrAppDomain)
        {
            Address = addr;
            Size = size;
            _runtime = clr;
            Type = type;
            _domainModuleHeap = moduleOrAppDomain;
        }

        internal MemoryRegion(DesktopRuntimeBase clr, ulong addr, ulong size, ClrMemoryRegionType type, ClrAppDomain domain)
        {
            Address = addr;
            Size = size;
            _runtime = clr;
            Type = type;
            _domainModuleHeap = domain.Address;
        }

        internal MemoryRegion(DesktopRuntimeBase clr, ulong addr, ulong size, ClrMemoryRegionType type)
        {
            Address = addr;
            Size = size;
            _runtime = clr;
            Type = type;
        }

        internal MemoryRegion(DesktopRuntimeBase clr, ulong addr, ulong size, ClrMemoryRegionType type, uint heap, GCSegmentType seg)
        {
            Address = addr;
            Size = size;
            _runtime = clr;
            Type = type;
            _domainModuleHeap = heap;
            _segmentType = seg;
        }
        #endregion
    }

    internal class SubHeap
    {
        internal int HeapNum { get; private set; }
        private IHeapDetails ActualHeap { get; set; }

        /// <summary>
        /// The allocation context pointers/limits for this heap.  The keys of this
        /// dictionary are the allocation pointers, the values of this dictionary are
        /// the limits.  If an allocation pointer is ever reached while walking a
        /// segment, you must "skip" past the allocation limit.  That is:
        ///     if (curr_obj is in AllocPointers)
        ///         curr_obj = AllocPointers[curr_obj] + min_object_size;
        /// </summary>
        internal Dictionary<ulong, ulong> AllocPointers { get; set; }

        /// <summary>
        /// Returns the address of the ephemeral segment.  Users of this API should use
        /// HeapSegment.Ephemeral instead of this property.
        /// </summary>
        internal ulong EphemeralSegment { get { return ActualHeap.EphemeralSegment; } }

        /// <summary>
        /// Returns the actual end of the ephemeral segment.
        /// </summary>
        internal ulong EphemeralEnd { get { return ActualHeap.EphemeralEnd; } }

        internal ulong Gen0Start { get { return ActualHeap.Gen0Start; } }
        internal ulong Gen1Start { get { return ActualHeap.Gen1Start; } }
        internal ulong Gen2Start { get { return ActualHeap.Gen2Start; } }
        internal ulong FirstLargeSegment { get { return ActualHeap.FirstLargeHeapSegment; } }
        internal ulong FirstSegment { get { return ActualHeap.FirstHeapSegment; } }
        internal ulong FQStart { get { return ActualHeap.FQAllObjectsStart; } }
        internal ulong FQStop { get { return ActualHeap.FQAllObjectsStop; } }
        internal ulong FQLiveStart { get { return ActualHeap.FQRootsStart; } }
        internal ulong FQLiveStop { get { return ActualHeap.FQRootsEnd; } }

        internal SubHeap(IHeapDetails heap, int heapNum)
        {
            ActualHeap = heap;
            HeapNum = heapNum;
        }
    }


    #region Data Interfaces
    internal enum CodeHeapType
    {
        Loader,
        Host,
        Unknown
    }

    internal interface ICodeHeap
    {
        CodeHeapType Type { get; }
        ulong Address { get; }
    }

    internal interface IThreadPoolData
    {
        int TotalThreads { get; }
        int RunningThreads { get; }
        int IdleThreads { get; }
        int MinThreads { get; }
        int MaxThreads { get; }
        ulong FirstWorkRequest { get; }
        ulong QueueUserWorkItemCallbackFPtr { get; }
        ulong AsyncCallbackCompletionFPtr { get; }
        ulong AsyncTimerCallbackCompletionFPtr { get; }
        int MinCP { get; }
        int MaxCP { get; }
        int CPU { get; }
        int NumFreeCP { get; }
        int MaxFreeCP { get; }
    }

    internal interface IAssemblyData
    {
        ulong Address { get; }
        ulong ParentDomain { get; }
        ulong AppDomain { get; }
        bool IsDynamic { get; }
        bool IsDomainNeutral { get; }
        int ModuleCount { get; }
    }

    internal interface IAppDomainData
    {
        int Id { get; }
        ulong Address { get; }
        ulong LowFrequencyHeap { get; }
        ulong HighFrequencyHeap { get; }
        ulong StubHeap { get; }
        int AssemblyCount { get; }
    }

    internal interface IThreadStoreData
    {
        ulong Finalizer { get; }
        ulong FirstThread { get; }
        int Count { get; }
    }

    internal interface IThreadData
    {
        ulong Next { get; }
        ulong AllocPtr { get; }
        ulong AllocLimit { get; }
        uint OSThreadID { get; }
        uint ManagedThreadID { get; }
        ulong Teb { get; }
        ulong AppDomain { get; }
        uint LockCount { get; }
        int State { get; }
        ulong ExceptionPtr { get; }
        bool Preemptive { get; }
    }

    internal interface ISegmentData
    {
        ulong Address { get; }
        ulong Next { get; }
        ulong Start { get; }
        ulong End { get; }
        ulong Committed { get; }
        ulong Reserved { get; }
    }

    internal interface IHeapDetails
    {
        ulong FirstHeapSegment { get; }
        ulong FirstLargeHeapSegment { get; }
        ulong EphemeralSegment { get; }
        ulong EphemeralEnd { get; }
        ulong EphemeralAllocContextPtr { get; }
        ulong EphemeralAllocContextLimit { get; }

        ulong Gen0Start { get; }
        ulong Gen0Stop { get; }
        ulong Gen1Start { get; }
        ulong Gen1Stop { get; }
        ulong Gen2Start { get; }
        ulong Gen2Stop { get; }

        ulong FQAllObjectsStart { get; }
        ulong FQAllObjectsStop { get; }
        ulong FQRootsStart { get; }
        ulong FQRootsEnd { get; }
    }

    internal interface IGCInfo
    {
        bool ServerMode { get; }
        int HeapCount { get; }
        int MaxGeneration { get; }
        bool GCStructuresValid { get; }
    }

    internal interface IMethodTableData
    {
        bool Shared { get; }
        bool Free { get; }
        bool ContainsPointers { get; }
        uint BaseSize { get; }
        uint ComponentSize { get; }
        ulong EEClass { get; }
        ulong Parent { get; }
        uint NumMethods { get; }
        ulong ElementTypeHandle { get; }
    }

    internal interface IFieldInfo
    {
        uint InstanceFields { get; }
        uint StaticFields { get; }
        uint ThreadStaticFields { get; }
        ulong FirstField { get; }
    }

    internal interface IFieldData
    {
        uint CorElementType { get; }
        uint SigType { get; }
        ulong TypeMethodTable { get; }

        ulong Module { get; }
        uint TypeToken { get; }

        uint FieldToken { get; }
        ulong EnclosingMethodTable { get; }
        uint Offset { get; }
        bool IsThreadLocal { get; }
        bool bIsContextLocal { get; }
        bool bIsStatic { get; }
        ulong nextField { get; }
    }

    internal interface IEEClassData
    {
        ulong MethodTable { get; }
        ulong Module { get; }
    }

    internal interface IDomainLocalModuleData
    {
        ulong AppDomainAddr { get; }
        ulong ModuleID { get; }

        ulong ClassData { get; }
        ulong DynamicClassTable { get; }
        ulong GCStaticDataStart { get; }
        ulong NonGCStaticDataStart { get; }
    }

    internal interface IModuleData
    {
        ulong ImageBase { get; }
        ulong PEFile { get; }
        ulong LookupTableHeap { get; }
        ulong ThunkHeap { get; }
        object LegacyMetaDataImport { get; }
        ulong ModuleId { get; }
        ulong ModuleIndex { get; }
        ulong Assembly { get; }
        bool IsReflection { get; }
        bool IsPEFile { get; }
        ulong MetdataStart { get; }
        ulong MetadataLength { get; }
    }

    internal interface IMethodDescData
    {
        ulong MethodDesc { get; }
        ulong Module { get; }
        uint MDToken { get; }
        ulong NativeCodeAddr { get; }
        ulong MethodTable { get; }
        MethodCompilationType JITType { get; }
    }

    internal interface ICCWData
    {
        ulong IUnknown { get; }
        ulong Object { get; }
        ulong Handle { get; }
        ulong CCWAddress { get; }
        int RefCount { get; }
        int JupiterRefCount { get; }
        int InterfaceCount { get; }
    }

    internal interface IRCWData
    {
        ulong IdentityPointer { get; }
        ulong UnknownPointer { get; }
        ulong ManagedObject { get; }
        ulong JupiterObject { get; }
        ulong VTablePtr { get; }
        ulong CreatorThread { get; }

        int RefCount { get; }
        int InterfaceCount { get; }

        bool IsJupiterObject { get; }
        bool IsDisconnected { get; }
    }

    internal interface IAppDomainStoreData
    {
        ulong SharedDomain { get; }
        ulong SystemDomain { get; }
        int Count { get; }
    }

    internal interface IObjectData
    {
        ulong DataPointer { get; }
        ulong ElementTypeHandle { get; }
        ClrElementType ElementType { get; }
        ulong RCW { get; }
        ulong CCW { get; }
    }

    internal interface ISyncBlkData
    {
        bool Free { get; }
        ulong Address { get; }
        ulong Object { get; }
        ulong OwningThread { get; }
        bool MonitorHeld { get; }
        uint Recursion { get; }
        uint TotalCount { get; }
    }

    // This is consistent across all dac versions.  No need for interface.
    internal struct CommonMethodTables
    {
        public ulong ArrayMethodTable;
        public ulong StringMethodTable;
        public ulong ObjectMethodTable;
        public ulong ExceptionMethodTable;
        public ulong FreeMethodTable;
    };


    internal interface IRWLockData
    {
        ulong Next { get; }
        int ULockID { get; }
        int LLockID { get; }
        int Level { get; }
    }

    internal struct RWLockData : IRWLockData
    {
        public IntPtr pNext;
        public IntPtr pPrev;
        public int _uLockID;
        public int _lLockID;
        public Int16 wReaderLevel;

        public ulong Next
        {
            get { return (ulong)pNext.ToInt64(); }
        }

        public int ULockID
        {
            get { return _uLockID; }
        }

        public int LLockID
        {
            get { return _lLockID; }
        }


        public int Level
        {
            get { return wReaderLevel; }
        }
    }
    #endregion
}
