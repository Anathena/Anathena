// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Address = System.UInt64;
using Microsoft.Diagnostics.Runtime.Desktop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace Microsoft.Diagnostics.Runtime
{

    /// <summary>
    /// Represents a single runtime in a target process or crash dump.  This serves as the primary
    /// entry point for getting diagnostic information.
    /// </summary>
    public abstract class ClrRuntime
    {
        /// <summary>
        /// In .NET native crash dumps, we have a list of serialized exceptions objects. This property expose them as ClrException objects.
        /// </summary>
        public abstract IEnumerable<ClrException> EnumerateSerializedExceptions();

        /// <summary>
        /// The ClrInfo of the current runtime.
        /// </summary>
        public ClrInfo ClrInfo { get; protected set; }

        /// <summary>
        /// Returns the DataTarget associated with this runtime.
        /// </summary>
        public abstract DataTarget DataTarget { get; }

        /// <summary>
        /// Whether or not the process is running in server GC mode or not.
        /// </summary>
        public bool ServerGC { get; protected set; }

        /// <summary>
        /// Enumerates the OS thread ID of GC threads in the runtime.  
        /// </summary>
        public abstract IEnumerable<int> EnumerateGCThreads();

        /// <summary>
        /// The number of logical GC heaps in the process.  This is always 1 for a workstation
        /// GC, and usually it's the number of logical processors in a server GC application.
        /// </summary>
        public int HeapCount { get; protected set; }

        /// <summary>
        /// Returns the pointer size of the target process.
        /// </summary>
        abstract public int PointerSize { get; }

        /// <summary>
        /// Enumerates the list of appdomains in the process.  Note the System appdomain and Shared
        /// AppDomain are omitted.
        /// </summary>
        abstract public IList<ClrAppDomain> AppDomains { get; }

        /// <summary>
        /// Give access to the System AppDomain
        /// </summary>
        abstract public ClrAppDomain SystemDomain { get; }

        /// <summary>
        /// Give access to the Shared AppDomain
        /// </summary>
        abstract public ClrAppDomain SharedDomain { get; }

        /// <summary>
        /// Enumerates all managed threads in the process.  Only threads which have previously run managed
        /// code will be enumerated.
        /// </summary>
        abstract public IList<ClrThread> Threads { get; }

        /// <summary>
        /// Enumerates all objects currently on the finalizer queue.  (Not finalizable objects, but objects
        /// which have been collected and will be imminently finalized.)
        /// </summary>
        abstract public IEnumerable<Address> EnumerateFinalizerQueueObjectAddresses();

        /// <summary>
        /// Returns a ClrMethod by its internal runtime handle (on desktop CLR this is a MethodDesc).
        /// </summary>
        /// <param name="methodHandle">The method handle (MethodDesc) to look up.</param>
        /// <returns>The ClrMethod for the given method handle, or null if no method was found.</returns>
        abstract public ClrMethod GetMethodByHandle(Address methodHandle);

        /// <summary>
        /// Returns the CCW data associated with the given address.  This is used when looking at stowed
        /// exceptions in CLR.
        /// </summary>
        /// <param name="addr">The address of the CCW obtained from stowed exception data.</param>
        /// <returns>The CcwData describing the given CCW, or null.</returns>
        public abstract CcwData GetCcwDataByAddress(ulong addr);


        /// <summary>
        /// Read data out of the target process.
        /// </summary>
        /// <param name="address">The address to start the read from.</param>
        /// <param name="buffer">The buffer to write memory to.</param>
        /// <param name="bytesRequested">How many bytes to read (must be less than/equal to buffer.Length)</param>
        /// <param name="bytesRead">The number of bytes actually read out of the process.  This will be less than
        /// bytes requested if the request falls off the end of an allocation.</param>
        /// <returns>False if the memory is not readable (free or no read permission), true if *some* memory was read.</returns>
        abstract public bool ReadMemory(Address address, byte[] buffer, int bytesRequested, out int bytesRead);


        /// <summary>
        /// Reads a pointer value out of the target process.  This function reads only the target's pointer size,
        /// so if this is used on an x86 target, only 4 bytes is read and written to val.
        /// </summary>
        /// <param name="address">The address to read from.</param>
        /// <param name="value">The value at that address.</param>
        /// <returns>True if the read was successful, false otherwise.</returns>
        abstract public bool ReadPointer(Address address, out Address value);

        /// <summary>
        /// Enumerates a list of GC handles currently in the process.  Note that this list may be incomplete
        /// depending on the state of the process when we attempt to walk the handle table.
        /// </summary>
        /// <returns>The list of GC handles in the process, NULL on catastrophic error.</returns>
        public abstract IEnumerable<ClrHandle> EnumerateHandles();

        /// <summary>
        /// Gets the GC heap of the process.
        /// </summary>
        abstract public ClrHeap GetHeap();

        /// <summary>
        /// Returns data on the CLR thread pool for this runtime.
        /// </summary>
        virtual public ClrThreadPool GetThreadPool() { throw new NotImplementedException(); }

        /// <summary>
        /// Enumerates regions of memory which CLR has allocated with a description of what data
        /// resides at that location.  Note that this does not return every chunk of address space
        /// that CLR allocates.
        /// </summary>
        /// <returns>An enumeration of memory regions in the process.</returns>
        abstract public IEnumerable<ClrMemoryRegion> EnumerateMemoryRegions();

        /// <summary>
        /// Attempts to get a ClrMethod for the given instruction pointer.  This will return NULL if the
        /// given instruction pointer is not within any managed method.
        /// </summary>
        abstract public ClrMethod GetMethodByAddress(Address ip);


        /// <summary>
        /// A list of all modules loaded into the process.
        /// </summary>
        public abstract IList<ClrModule> Modules { get; }

        /// <summary>
        /// Flushes the dac cache.  This function MUST be called any time you expect to call the same function
        /// but expect different results.  For example, after walking the heap, you need to call Flush before
        /// attempting to walk the heap again.  After calling this function, you must discard ALL ClrMD objects
        /// you have cached other than DataTarget and ClrRuntime and re-request the objects and data you need.
        /// (E.G. if you want to use the ClrHeap object after calling flush, you must call ClrRuntime.GetHeap
        /// again after Flush to get a new instance.)
        /// </summary>
        abstract public void Flush();

        /// <summary>
        /// Delegate called when the RuntimeFlushed event is triggered.
        /// </summary>
        /// <param name="runtime">Which runtime was flushed.</param>
        public delegate void RuntimeFlushedCallback(ClrRuntime runtime);

        /// <summary>
        /// Called whenever the runtime is being flushed.  All references to ClrMD objects need to be released
        /// and not used for the given runtime after this call.
        /// </summary>
        public event RuntimeFlushedCallback RuntimeFlushed;

        /// <summary>
        /// Call when flushing the runtime.
        /// </summary>
        protected void OnRuntimeFlushed()
        {
            var evt = RuntimeFlushed;
            if (evt != null)
                evt(this);
        }

        /// <summary>
        /// Whether or not the runtime has component method tables for arrays.  This is an extra field in
        /// array objects on the heap, which was removed in v4.6 of desktop clr.
        /// </summary>
        internal bool HasArrayComponentMethodTables
        {
            get
            {
                if (ClrInfo.Flavor == ClrFlavor.Desktop)
                {
                    VersionInfo version = ClrInfo.Version;
                    if (version.Major > 4)
                        return false;

                    if (version.Major == 4 && version.Minor >= 6)
                        return false;
                }

                return true;
            }
        }

        internal static bool IsPrimitive(ClrElementType cet)
        {
            return cet >= ClrElementType.Boolean && cet <= ClrElementType.Double
                || cet == ClrElementType.NativeInt || cet == ClrElementType.NativeUInt
                || cet == ClrElementType.Pointer || cet == ClrElementType.FunctionPointer;
        }

        internal static bool IsValueClass(ClrElementType cet)
        {
            return cet == ClrElementType.Struct;
        }

        internal static bool IsObjectReference(ClrElementType cet)
        {
            return cet == ClrElementType.String || cet == ClrElementType.Class
                || cet == ClrElementType.Array || cet == ClrElementType.SZArray
                || cet == ClrElementType.Object;
        }

        internal static Type GetTypeForElementType(ClrElementType type)
        {
            switch (type)
            {
                case ClrElementType.Boolean:
                    return typeof(bool);

                case ClrElementType.Char:
                    return typeof(char);

                case ClrElementType.Double:
                    return typeof(double);

                case ClrElementType.Float:
                    return typeof(float);

                case ClrElementType.Pointer:
                case ClrElementType.NativeInt:
                case ClrElementType.FunctionPointer:
                    return typeof(IntPtr);

                case ClrElementType.NativeUInt:
                    return typeof(UIntPtr);

                case ClrElementType.Int16:
                    return typeof(short);

                case ClrElementType.Int32:
                    return typeof(int);

                case ClrElementType.Int64:
                    return typeof(long);

                case ClrElementType.Int8:
                    return typeof(sbyte);

                case ClrElementType.UInt16:
                    return typeof(ushort);

                case ClrElementType.UInt32:
                    return typeof(uint);

                case ClrElementType.UInt64:
                    return typeof(ulong);

                case ClrElementType.UInt8:
                    return typeof(byte);

                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Provides information about CLR's threadpool.
    /// </summary>
    public abstract class ClrThreadPool
    {
        /// <summary>
        /// The total number of threadpool worker threads in the process.
        /// </summary>
        abstract public int TotalThreads { get; }

        /// <summary>
        /// The number of running threadpool threads in the process.
        /// </summary>
        abstract public int RunningThreads { get; }

        /// <summary>
        /// The number of idle threadpool threads in the process.
        /// </summary>
        abstract public int IdleThreads { get; }

        /// <summary>
        /// The minimum number of threadpool threads allowable.
        /// </summary>
        abstract public int MinThreads { get; }

        /// <summary>
        /// The maximum number of threadpool threads allowable.
        /// </summary>
        abstract public int MaxThreads { get; }

        /// <summary>
        /// Returns the minimum number of completion ports (if any).
        /// </summary>
        abstract public int MinCompletionPorts { get; }

        /// <summary>
        /// Returns the maximum number of completion ports.
        /// </summary>
        abstract public int MaxCompletionPorts { get; }

        /// <summary>
        /// Returns the CPU utilization of the threadpool (as a percentage out of 100).
        /// </summary>
        abstract public int CpuUtilization { get; }

        /// <summary>
        /// The number of free completion port threads.
        /// </summary>
        abstract public int FreeCompletionPortCount { get; }

        /// <summary>
        /// The maximum number of free completion port threads.
        /// </summary>
        abstract public int MaxFreeCompletionPorts { get; }

        /// <summary>
        /// Enumerates the work items on the threadpool (native side).
        /// </summary>
        abstract public IEnumerable<NativeWorkItem> EnumerateNativeWorkItems();

        /// <summary>
        /// Enumerates work items on the thread pool (managed side).
        /// </summary>
        /// <returns></returns>
        abstract public IEnumerable<ManagedWorkItem> EnumerateManagedWorkItems();
    }

    /// <summary>
    /// A managed threadpool object.
    /// </summary>
    public abstract class ManagedWorkItem
    {
        /// <summary>
        /// The object address of this entry.
        /// </summary>
        public abstract ulong Object { get; }

        /// <summary>
        /// The type of Object.
        /// </summary>
        public abstract ClrType Type { get; }
    }

    /// <summary>
    /// The type of work item this is.
    /// </summary>
    public enum WorkItemKind
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Callback for an async timer.
        /// </summary>
        AsyncTimer,

        /// <summary>
        /// Async callback.
        /// </summary>
        AsyncCallback,

        /// <summary>
        /// From ThreadPool.QueueUserWorkItem.
        /// </summary>
        QueueUserWorkItem,

        /// <summary>
        /// Timer delete callback.
        /// </summary>
        TimerDelete
    }

    /// <summary>
    /// Represents a work item on CLR's thread pool (native side).
    /// </summary>
    public abstract class NativeWorkItem
    {
        /// <summary>
        /// The type of work item this is.
        /// </summary>
        public abstract WorkItemKind Kind { get; }

        /// <summary>
        /// Returns the callback's address.
        /// </summary>
        public abstract Address Callback { get; }

        /// <summary>
        /// Returns the pointer to the user's data.
        /// </summary>
        public abstract Address Data { get; }
    }

    /// <summary>
    /// Types of Clr handles.
    /// </summary>
    public enum HandleType
    {
        /// <summary>
        /// Weak, short lived handle.
        /// </summary>
        WeakShort = 0,

        /// <summary>
        /// Weak, long lived handle.
        /// </summary>
        WeakLong = 1,

        /// <summary>
        /// Strong handle.
        /// </summary>
        Strong = 2,

        /// <summary>
        /// Strong handle, prevents relocation of target object.
        /// </summary>
        Pinned = 3,

        /// <summary>
        /// RefCounted handle (strong when the reference count is greater than 0).
        /// </summary>
        RefCount = 5,

        /// <summary>
        /// A weak handle which may keep its "secondary" object alive if the "target" object is also alive.
        /// </summary>
        Dependent = 6,

        /// <summary>
        /// A strong, pinned handle (keeps the target object from being relocated), used for async IO operations.
        /// </summary>
        AsyncPinned = 7,

        /// <summary>
        /// Strong handle used internally for book keeping.
        /// </summary>
        SizedRef = 8
    }

    /// <summary>
    /// Represents a Clr handle in the target process.
    /// </summary>
    public class ClrHandle
    {
        /// <summary>
        /// The address of the handle itself.  That is, *Address == Object.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The Object the handle roots.
        /// </summary>
        public Address Object { get; set; }

        /// <summary>
        /// The the type of the Object.
        /// </summary>
        public ClrType Type { get; set; }

        /// <summary>
        /// Whether the handle is strong (roots the object) or not.
        /// </summary>
        public virtual bool IsStrong
        {
            get
            {
                switch (HandleType)
                {
                    case HandleType.RefCount:
                        return RefCount > 0;

                    case HandleType.WeakLong:
                    case HandleType.WeakShort:
                    case HandleType.Dependent:
                        return false;

                    default:
                        return true;
                }
            }
        }

        /// <summary>
        /// Whether or not the handle pins the object (doesn't allow the GC to
        /// relocate it) or not.
        /// </summary>
        public virtual bool IsPinned
        {
            get
            {
                return HandleType == Runtime.HandleType.AsyncPinned || HandleType == Runtime.HandleType.Pinned;
            }
        }

        /// <summary>
        /// Gets the type of handle.
        /// </summary>
        public HandleType HandleType { get; set; }

        /// <summary>
        /// If this handle is a RefCount handle, this returns the reference count.
        /// RefCount handles with a RefCount > 0 are strong.
        /// NOTE: v2 CLR CANNOT determine the RefCount.  We always set the RefCount
        ///       to 1 in a v2 query since a strong RefCount handle is the common case.
        /// </summary>
        public uint RefCount { get; set; }


        /// <summary>
        /// Set only if the handle type is a DependentHandle.  Dependent handles add
        /// an extra edge to the object graph.  Meaning, this.Object now roots the
        /// dependent target, but only if this.Object is alive itself.
        /// NOTE: CLRs prior to v4.5 cannot obtain the dependent target.  This field will
        ///       be 0 for any CLR prior to v4.5.
        /// </summary>
        public Address DependentTarget { get; set; }

        /// <summary>
        /// The type of the dependent target, if non 0.
        /// </summary>
        public ClrType DependentType { get; set; }

        /// <summary>
        /// The AppDomain the handle resides in.
        /// </summary>
        public ClrAppDomain AppDomain { get; set; }

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return HandleType.ToString() + " " + (Type != null ? Type.Name : "");
        }

        #region Internal
        internal ClrHandle()
        {
        }

        internal ClrHandle(Microsoft.Diagnostics.Runtime.Desktop.V45Runtime clr, ClrHeap heap, Microsoft.Diagnostics.Runtime.Desktop.HandleData handleData)
        {
            Address obj;
            Address = handleData.Handle;
            clr.ReadPointer(Address, out obj);

            Object = obj;
            Type = heap.GetObjectType(obj);

            uint refCount = 0;

            if (handleData.Type == (int)HandleType.RefCount)
            {
                if (handleData.IsPegged != 0)
                    refCount = handleData.JupiterRefCount;

                if (refCount < handleData.RefCount)
                    refCount = handleData.RefCount;

                if (Type != null)
                {
                    if (Type.IsCCW(obj))
                    {
                        CcwData data = Type.GetCCWData(obj);
                        if (data != null && refCount < data.RefCount)
                            refCount = (uint)data.RefCount;
                    }
                    else if (Type.IsRCW(obj))
                    {
                        RcwData data = Type.GetRCWData(obj);
                        if (data != null && refCount < data.RefCount)
                            refCount = (uint)data.RefCount;
                    }
                }

                RefCount = refCount;
            }


            HandleType = (HandleType)handleData.Type;
            AppDomain = clr.GetAppDomainByAddress(handleData.AppDomain);

            if (HandleType == HandleType.Dependent)
            {
                DependentTarget = handleData.Secondary;
                DependentType = heap.GetObjectType(handleData.Secondary);
            }
        }
        #endregion

        internal ClrHandle GetInteriorHandle()
        {
            if (this.HandleType != HandleType.AsyncPinned)
                return null;

            if (this.Type == null)
                return null;

            var field = this.Type.GetFieldByName("m_userObject");
            if (field == null)
                return null;

            ulong obj;
            object tmp = field.GetValue(this.Object);
            if (!(tmp is ulong) || (obj = (ulong)tmp) == 0)
                return null;

            ClrType type = this.Type.Heap.GetObjectType(obj);
            if (type == null)
                return null;

            ClrHandle result = new ClrHandle();
            result.Object = obj;
            result.Type = type;
            result.Address = this.Address;
            result.AppDomain = this.AppDomain;
            result.HandleType = this.HandleType;

            return result;
        }
    }

    /// <summary>
    /// Types of memory regions in a Clr process.
    /// </summary>
    public enum ClrMemoryRegionType
    {
        // Loader heaps
        /// <summary>
        /// Data on the loader heap.
        /// </summary>
        LowFrequencyLoaderHeap,

        /// <summary>
        /// Data on the loader heap.
        /// </summary>
        HighFrequencyLoaderHeap,

        /// <summary>
        /// Data on the stub heap.
        /// </summary>
        StubHeap,

        // Virtual Call Stub heaps
        /// <summary>
        /// Clr implementation detail (this is here to allow you to distinguish from other
        /// heap types).
        /// </summary>
        IndcellHeap,
        /// <summary>
        /// Clr implementation detail (this is here to allow you to distinguish from other
        /// heap types).
        /// </summary>
        LookupHeap,
        /// <summary>
        /// Clr implementation detail (this is here to allow you to distinguish from other
        /// heap types).
        /// </summary>
        ResolveHeap,
        /// <summary>
        /// Clr implementation detail (this is here to allow you to distinguish from other
        /// heap types).
        /// </summary>
        DispatchHeap,
        /// <summary>
        /// Clr implementation detail (this is here to allow you to distinguish from other
        /// heap types).
        /// </summary>
        CacheEntryHeap,

        // Other regions
        /// <summary>
        /// Heap for JIT code data.
        /// </summary>
        JitHostCodeHeap,
        /// <summary>
        /// Heap for JIT loader data.
        /// </summary>
        JitLoaderCodeHeap,
        /// <summary>
        /// Heap for module jump thunks.
        /// </summary>
        ModuleThunkHeap,
        /// <summary>
        /// Heap for module lookup tables.
        /// </summary>
        ModuleLookupTableHeap,

        /// <summary>
        /// A segment on the GC heap (committed memory).
        /// </summary>
        GCSegment,

        /// <summary>
        /// A segment on the GC heap (reserved, but not committed, memory).
        /// </summary>
        ReservedGCSegment,

        /// <summary>
        /// A portion of Clr's handle table.
        /// </summary>
        HandleTableChunk
    }

    /// <summary>
    /// Represents a region of memory in the process which Clr allocated and controls.
    /// </summary>
    public abstract class ClrMemoryRegion
    {
        /// <summary>
        /// The start address of the memory region.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The size of the memory region in bytes.
        /// </summary>
        public ulong Size { get; set; }

        /// <summary>
        /// The type of heap/memory that the region contains.
        /// </summary>
        public ClrMemoryRegionType Type { get; set; }

        /// <summary>
        /// The AppDomain pointer that corresponds to this heap.  You can obtain the
        /// name of the AppDomain index or name by calling the appropriate function
        /// on RuntimeBase.
        /// Note:  HasAppDomainData must be true before getting this property.
        /// </summary>
        abstract public ClrAppDomain AppDomain { get; }

        /// <summary>
        /// The Module pointer that corresponds to this heap.  You can obtain the
        /// filename of the module with this property.
        /// Note:  HasModuleData must be true or this property will be null.
        /// </summary>
        abstract public string Module { get; }

        /// <summary>
        /// Returns the heap number associated with this data.  Returns -1 if no
        /// GC heap is associated with this memory region.
        /// </summary>
        abstract public int HeapNumber { get; set; }

        /// <summary>
        /// Returns the gc segment type associated with this data.  Only callable if
        /// HasGCHeapData is true.
        /// </summary>
        abstract public GCSegmentType GCSegmentType { get; set; }

        /// <summary>
        /// Returns a string describing the region of memory (for example "JIT Code Heap"
        /// or "GC Segment").
        /// </summary>
        /// <param name="detailed">Whether or not to include additional data such as the module,
        /// AppDomain, or GC Heap associaed with it.</param>
        abstract public string ToString(bool detailed);

        /// <summary>
        /// Equivalent to GetDisplayString(false).
        /// </summary>
        public override string ToString()
        {
            return ToString(false);
        }
    }

    internal abstract class RuntimeBase : ClrRuntime
    {
        private static ulong[] s_emptyPointerArray = new ulong[0];
        protected DacLibrary _library;
        protected IXCLRDataProcess _dacInterface;
        private MemoryReader _cache;
        protected IDataReader _dataReader;
        protected DataTargetImpl _dataTarget;

        protected ICorDebug.ICorDebugProcess _corDebugProcess;
        internal ICorDebug.ICorDebugProcess CorDebugProcess
        {
            get
            {
                if (_corDebugProcess == null)
                    _corDebugProcess = ICorDebug.CLRDebugging.CreateICorDebugProcess(ClrInfo.ModuleInfo.ImageBase, _library.DacDataTarget, _dataTarget.FileLoader);

                return _corDebugProcess;
            }
        }

        public RuntimeBase(ClrInfo info, DataTargetImpl dataTarget, DacLibrary lib)
        {
            Debug.Assert(lib != null);
            Debug.Assert(lib.DacInterface != null);

            ClrInfo = info;
            _dataTarget = dataTarget;
            _library = lib;
            _dacInterface = _library.DacInterface;
            InitApi();

            _dacInterface.Flush();

            IGCInfo data = GetGCInfo();
            if (data != null)
            {
                ServerGC = data.ServerMode;
                HeapCount = data.HeapCount;
                CanWalkHeap = data.GCStructuresValid && !dataTarget.DataReader.IsMinidump;
            }
            _dataReader = dataTarget.DataReader;
        }

        public override DataTarget DataTarget
        {
            get { return _dataTarget; }
        }

        public void RegisterForRelease(object o)
        {
            if (o != null)
                _library.AddToReleaseList(o);
        }

        public void RegisterForRelease(IModuleData module)
        {
            RegisterForRelease(module?.LegacyMetaDataImport);
        }

        public IDataReader DataReader
        {
            get { return _dataReader; }
        }

        protected abstract void InitApi();

        public override int PointerSize
        {
            get { return IntPtr.Size; }
        }

        internal bool CanWalkHeap { get; private set; }

        internal MemoryReader MemoryReader
        {
            get
            {
                if (_cache == null)
                    _cache = new MemoryReader(DataReader, 0x200);
                return _cache;
            }
            set
            {
                _cache = value;
            }
        }

        internal bool GetHeaps(out SubHeap[] heaps)
        {
            heaps = new SubHeap[HeapCount];
            Dictionary<ulong, ulong> allocContexts = GetAllocContexts();
            if (ServerGC)
            {
                ulong[] heapList = GetServerHeapList();
                if (heapList == null)
                    return false;

                bool succeeded = false;
                for (int i = 0; i < heapList.Length; ++i)
                {
                    IHeapDetails heap = GetSvrHeapDetails(heapList[i]);
                    if (heap == null)
                        continue;

                    heaps[i] = new SubHeap(heap, i);
                    heaps[i].AllocPointers = new Dictionary<ulong, ulong>(allocContexts);
                    if (heap.EphemeralAllocContextPtr != 0)
                        heaps[i].AllocPointers[heap.EphemeralAllocContextPtr] = heap.EphemeralAllocContextLimit;

                    succeeded = true;
                }

                return succeeded;
            }
            else
            {
                IHeapDetails heap = GetWksHeapDetails();
                if (heap == null)
                    return false;

                heaps[0] = new SubHeap(heap, 0);
                heaps[0].AllocPointers = allocContexts;
                heaps[0].AllocPointers[heap.EphemeralAllocContextPtr] = heap.EphemeralAllocContextLimit;

                return true;
            }
        }

        internal Dictionary<ulong, ulong> GetAllocContexts()
        {
            Dictionary<ulong, ulong> ret = new Dictionary<ulong, ulong>();

            // Give a max number of threads to walk to ensure no infinite loops due to data
            // inconsistency.
            int max = 1024;

            IThreadData thread = GetThread(GetFirstThread());

            while (max-- > 0 && thread != null)
            {
                if (thread.AllocPtr != 0)
                    ret[thread.AllocPtr] = thread.AllocLimit;

                if (thread.Next == 0)
                    break;
                thread = GetThread(thread.Next);
            }

            return ret;
        }

        private struct StackRef
        {
            public ulong Address;
            public ulong Object;

            public StackRef(ulong stackPtr, ulong objRef)
            {
                Address = stackPtr;
                Object = objRef;
            }
        }

        public override IEnumerable<Address> EnumerateFinalizerQueueObjectAddresses()
        {
            SubHeap[] heaps;
            if (GetHeaps(out heaps))
            {
                foreach (SubHeap heap in heaps)
                {
                    foreach (Address objAddr in GetPointersInRange(heap.FQStart, heap.FQStop))
                    {
                        if (objAddr != 0)
                            yield return objAddr;
                    }
                }
            }
        }

        internal virtual IEnumerable<ClrRoot> EnumerateStackReferences(ClrThread thread, bool includeDead)
        {
            Address stackBase = thread.StackBase;
            Address stackLimit = thread.StackLimit;
            if (stackLimit <= stackBase)
            {
                Address tmp = stackLimit;
                stackLimit = stackBase;
                stackBase = tmp;
            }

            ClrAppDomain domain = GetAppDomainByAddress(thread.AppDomain);
            ClrHeap heap = GetHeap();
            var mask = ((ulong)(PointerSize - 1));
            var cache = MemoryReader;
            cache.EnsureRangeInCache(stackBase);
            for (Address stackPtr = stackBase; stackPtr < stackLimit; stackPtr += (uint)PointerSize)
            {
                Address objRef;
                if (cache.ReadPtr(stackPtr, out objRef))
                {
                    // If the value isn't pointer aligned, it cannot be a managed pointer.
                    if (heap.IsInHeap(objRef))
                    {
                        ulong mt;
                        if (heap.ReadPointer(objRef, out mt))
                        {
                            ClrType type = null;

                            if (mt > 1024)
                                type = heap.GetObjectType(objRef);

                            if (type != null && !type.IsFree)
                                yield return new LocalVarRoot(stackPtr, objRef, type, domain, thread, false, true, false);
                        }
                    }
                }
            }
        }

        #region Abstract
        internal abstract ulong GetFirstThread();
        internal abstract IThreadData GetThread(ulong addr);
        internal abstract IHeapDetails GetSvrHeapDetails(ulong addr);
        internal abstract IHeapDetails GetWksHeapDetails();
        internal abstract ulong[] GetServerHeapList();
        internal abstract IThreadStoreData GetThreadStoreData();
        internal abstract ISegmentData GetSegmentData(ulong addr);
        internal abstract IGCInfo GetGCInfo();
        internal abstract IMethodTableData GetMethodTableData(ulong addr);
        internal abstract uint GetTlsSlot();
        internal abstract uint GetThreadTypeIndex();

        internal abstract ClrAppDomain GetAppDomainByAddress(ulong addr);
        #endregion

        #region Helpers
        #region Request Helpers
        protected bool Request(uint id, ulong param, byte[] output)
        {
            byte[] input = BitConverter.GetBytes(param);

            return Request(id, input, output);
        }

        protected bool Request(uint id, uint param, byte[] output)
        {
            byte[] input = BitConverter.GetBytes(param);

            return Request(id, input, output);
        }

        protected bool Request(uint id, byte[] input, byte[] output)
        {
            uint inSize = 0;
            if (input != null)
                inSize = (uint)input.Length;

            uint outSize = 0;
            if (output != null)
                outSize = (uint)output.Length;

            int result = _dacInterface.Request(id, inSize, input, outSize, output);

            return result >= 0;
        }

        protected I Request<I, T>(uint id, byte[] input)
            where T : struct, I
            where I : class
        {
            byte[] output = GetByteArrayForStruct<T>();

            if (!Request(id, input, output))
                return null;

            return ConvertStruct<I, T>(output);
        }

        protected I Request<I, T>(uint id, ulong param)
            where T : struct, I
            where I : class
        {
            byte[] output = GetByteArrayForStruct<T>();

            if (!Request(id, param, output))
                return null;

            return ConvertStruct<I, T>(output);
        }

        protected I Request<I, T>(uint id, uint param)
            where T : struct, I
            where I : class
        {
            byte[] output = GetByteArrayForStruct<T>();

            if (!Request(id, param, output))
                return null;

            return ConvertStruct<I, T>(output);
        }

        protected I Request<I, T>(uint id)
            where T : struct, I
            where I : class
        {
            byte[] output = GetByteArrayForStruct<T>();

            if (!Request(id, null, output))
                return null;

            return ConvertStruct<I, T>(output);
        }

        protected bool RequestStruct<T>(uint id, ref T t)
            where T : struct
        {
            byte[] output = GetByteArrayForStruct<T>();

            if (!Request(id, null, output))
                return false;

            GCHandle handle = GCHandle.Alloc(output, GCHandleType.Pinned);
            t = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return true;
        }

        protected bool RequestStruct<T>(uint id, ulong addr, ref T t)
            where T : struct
        {
            byte[] input = new byte[sizeof(ulong)];
            byte[] output = GetByteArrayForStruct<T>();

            WriteValueToBuffer(addr, input, 0);

            if (!Request(id, input, output))
                return false;

            GCHandle handle = GCHandle.Alloc(output, GCHandleType.Pinned);
            t = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return true;
        }

        protected ulong[] RequestAddrList(uint id, int length)
        {
            byte[] bytes = new byte[length * sizeof(ulong)];
            if (!Request(id, null, bytes))
                return null;

            ulong[] result = new ulong[length];
            for (uint i = 0; i < length; ++i)
                result[i] = BitConverter.ToUInt64(bytes, (int)(i * sizeof(ulong)));

            return result;
        }


        protected ulong[] RequestAddrList(uint id, ulong param, int length)
        {
            byte[] bytes = new byte[length * sizeof(ulong)];
            if (!Request(id, param, bytes))
                return null;

            ulong[] result = new ulong[length];
            for (uint i = 0; i < length; ++i)
                result[i] = BitConverter.ToUInt64(bytes, (int)(i * sizeof(ulong)));

            return result;
        }
        #endregion

        #region Marshalling Helpers
        protected static string BytesToString(byte[] output)
        {
            int len = 0;
            while (len < output.Length && (output[len] != 0 || output[len + 1] != 0))
                len += 2;

            if (len > output.Length)
                len = output.Length;

            return Encoding.Unicode.GetString(output, 0, len);
        }

        protected byte[] GetByteArrayForStruct<T>() where T : struct
        {
            return new byte[Marshal.SizeOf(typeof(T))];
        }

        protected I ConvertStruct<I, T>(byte[] bytes)
            where I : class
            where T : I
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            I result = (I)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return result;
        }

        protected int WriteValueToBuffer(IntPtr ptr, byte[] buffer, int offset)
        {
            ulong value = (ulong)ptr.ToInt64();
            for (int i = offset; i < offset + IntPtr.Size; ++i)
            {
                buffer[i] = (byte)value;
                value >>= 8;
            }

            return offset + IntPtr.Size;
        }

        protected int WriteValueToBuffer(int value, byte[] buffer, int offset)
        {
            for (int i = offset; i < offset + sizeof(int); ++i)
            {
                buffer[i] = (byte)value;
                value >>= 8;
            }

            return offset + sizeof(int);
        }

        protected int WriteValueToBuffer(uint value, byte[] buffer, int offset)
        {
            for (int i = offset; i < offset + sizeof(int); ++i)
            {
                buffer[i] = (byte)value;
                value >>= 8;
            }

            return offset + sizeof(int);
        }

        protected int WriteValueToBuffer(ulong value, byte[] buffer, int offset)
        {
            for (int i = offset; i < offset + sizeof(ulong); ++i)
            {
                buffer[i] = (byte)value;
                value >>= 8;
            }

            return offset + sizeof(ulong);
        }
        #endregion

        #region Data Read


        public override bool ReadMemory(Address address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            return _dataReader.ReadMemory(address, buffer, bytesRequested, out bytesRead);
        }

        private byte[] _dataBuffer = new byte[8];
        public bool ReadByte(Address addr, out byte value)
        {
            // todo: There's probably a more efficient way to implement this if ReadVirtual accepted an "out byte"
            //       "out dword", "out long", etc.
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, 1, out read))
                return false;

            Debug.Assert(read == 1);

            value = _dataBuffer[0];
            return true;
        }

        public bool ReadByte(Address addr, out sbyte value)
        {
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, 1, out read))
                return false;

            Debug.Assert(read == 1);

            value = (sbyte)_dataBuffer[0];
            return true;
        }

        public bool ReadDword(ulong addr, out int value)
        {
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, sizeof(int), out read))
                return false;

            Debug.Assert(read == 4);

            value = BitConverter.ToInt32(_dataBuffer, 0);
            return true;
        }

        public bool ReadDword(ulong addr, out uint value)
        {
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, sizeof(uint), out read))
                return false;

            Debug.Assert(read == 4);

            value = BitConverter.ToUInt32(_dataBuffer, 0);
            return true;
        }

        public bool ReadFloat(ulong addr, out float value)
        {
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, sizeof(float), out read))
                return false;

            Debug.Assert(read == sizeof(float));

            value = BitConverter.ToSingle(_dataBuffer, 0);
            return true;
        }

        public bool ReadFloat(ulong addr, out double value)
        {
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, sizeof(double), out read))
                return false;

            Debug.Assert(read == sizeof(double));

            value = BitConverter.ToDouble(_dataBuffer, 0);
            return true;
        }


        public bool ReadShort(ulong addr, out short value)
        {
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, sizeof(short), out read))
                return false;

            Debug.Assert(read == sizeof(short));

            value = BitConverter.ToInt16(_dataBuffer, 0);
            return true;
        }

        public bool ReadShort(ulong addr, out ushort value)
        {
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, sizeof(ushort), out read))
                return false;

            Debug.Assert(read == sizeof(ushort));

            value = BitConverter.ToUInt16(_dataBuffer, 0);
            return true;
        }

        public bool ReadQword(ulong addr, out ulong value)
        {
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, sizeof(ulong), out read))
                return false;

            Debug.Assert(read == sizeof(ulong));

            value = BitConverter.ToUInt64(_dataBuffer, 0);
            return true;
        }

        public bool ReadQword(ulong addr, out long value)
        {
            value = 0;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, sizeof(long), out read))
                return false;

            Debug.Assert(read == sizeof(long));

            value = BitConverter.ToInt64(_dataBuffer, 0);
            return true;
        }

        public override bool ReadPointer(ulong addr, out ulong value)
        {
            int ptrSize = (int)PointerSize;
            int read = 0;
            if (!ReadMemory(addr, _dataBuffer, ptrSize, out read))
            {
                value = 0xcccccccc;
                return false;
            }

            Debug.Assert(read == ptrSize);

            if (ptrSize == 4)
                value = (ulong)BitConverter.ToUInt32(_dataBuffer, 0);
            else
                value = (ulong)BitConverter.ToUInt64(_dataBuffer, 0);

            return true;
        }

        internal IEnumerable<ulong> GetPointersInRange(ulong start, ulong stop)
        {
            // Possible we have empty list, or inconsistent data.
            if (start >= stop)
                return s_emptyPointerArray;

            // Enumerate individually if we have too many.
            ulong count = (stop - start) / (ulong)IntPtr.Size;
            if (count > 4096)
                return EnumeratePointersInRange(start, stop);

            ulong[] array = new ulong[count];
            byte[] tmp = new byte[(int)count * IntPtr.Size];
            int read;
            if (!ReadMemory(start, tmp, tmp.Length, out read))
                return s_emptyPointerArray;

            if (IntPtr.Size == 4)
                for (uint i = 0; i < array.Length; ++i)
                    array[i] = BitConverter.ToUInt32(tmp, (int)(i * IntPtr.Size));
            else
                for (uint i = 0; i < array.Length; ++i)
                    array[i] = BitConverter.ToUInt64(tmp, (int)(i * IntPtr.Size));

            return array;
        }

        private IEnumerable<Address> EnumeratePointersInRange(ulong start, ulong stop)
        {
            ulong obj;
            for (ulong ptr = start; ptr < stop; ptr += (uint)IntPtr.Size)
            {
                if (!ReadPointer(ptr, out obj))
                    break;

                yield return obj;
            }
        }
        #endregion
        #endregion
    }
}
