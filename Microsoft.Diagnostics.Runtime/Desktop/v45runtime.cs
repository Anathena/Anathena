// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal class V45Runtime : DesktopRuntimeBase
    {
        private ISOSDac _sos;

        #region Constructor
        public V45Runtime(ClrInfo info, DataTargetImpl dt, DacLibrary lib)
            : base(info, dt, lib)
        {
            if (!GetCommonMethodTables(ref _commonMTs))
                throw new ClrDiagnosticsException("Could not request common MethodTable list.", ClrDiagnosticsException.HR.DacError);

            // Ensure the version of the dac API matches the one we expect.  (Same for both
            // v2 and v4 rtm.)
            byte[] tmp = new byte[sizeof(int)];

            if (!Request(DacRequests.VERSION, null, tmp))
                throw new ClrDiagnosticsException("Failed to request dac version.", ClrDiagnosticsException.HR.DacError);

            int v = BitConverter.ToInt32(tmp, 0);
            if (v != 9)
                throw new ClrDiagnosticsException("Unsupported dac version.", ClrDiagnosticsException.HR.DacError);
        }
        #endregion

        #region Overrides
        protected override void InitApi()
        {
            if (_sos == null)
                _sos = _library.SOSInterface;

            Debug.Assert(_sos != null);
        }
        internal override DesktopVersion CLRVersion
        {
            get { return DesktopVersion.v45; }
        }

        private ISOSHandleEnum _handleEnum;
        private List<ClrHandle> _handles;

        public override IEnumerable<ClrHandle> EnumerateHandles()
        {
            if (_handles != null && _handleEnum == null)
                return _handles;

            return EnumerateHandleWorker();
        }

        private IEnumerable<ClrHandle> EnumerateHandleWorker()
        {
            // handles was fully populated already
            if (_handles != null && _handleEnum == null)
                yield break;

            // Create _handleEnum if it's not already created.
            object tmp;
            if (_handleEnum == null)
            {
                if (_sos.GetHandleEnum(out tmp) < 0)
                    yield break;

                _handleEnum = tmp as ISOSHandleEnum;
                if (_handleEnum == null)
                    yield break;

                _handles = new List<ClrHandle>();
            }

            // We already partially enumerated handles before, start with them.
            foreach (var handle in _handles)
                yield return handle;

            HandleData[] handles = new HandleData[8];
            uint fetched = 0;
            do
            {
                if (_handleEnum.Next((uint)handles.Length, handles, out fetched) < 0 || fetched <= 0)
                    break;

                int curr = _handles.Count;
                for (int i = 0; i < fetched; i++)
                {
                    ClrHandle handle = new ClrHandle(this, GetHeap(), handles[i]);
                    _handles.Add(handle);

                    handle = handle.GetInteriorHandle();
                    if (handle != null)
                    {
                        _handles.Add(handle);
                        yield return handle;
                    }
                }

                for (int i = curr; i < _handles.Count; i++)
                    yield return _handles[i];
            } while (fetched > 0);

            _handleEnum = null;
        }

        internal override IEnumerable<ClrRoot> EnumerateStackReferences(ClrThread thread, bool includeDead)
        {
            if (includeDead)
                return base.EnumerateStackReferences(thread, includeDead);

            return EnumerateStackReferencesWorker(thread);
        }

        private IEnumerable<ClrRoot> EnumerateStackReferencesWorker(ClrThread thread)
        {
            ISOSStackRefEnum handleEnum = null;
            object tmp;
            if (_sos.GetStackReferences(thread.OSThreadId, out tmp) >= 0)
                handleEnum = tmp as ISOSStackRefEnum;

            ClrAppDomain domain = GetAppDomainByAddress(thread.AppDomain);
            if (handleEnum != null)
            {
                var heap = GetHeap();
                StackRefData[] refs = new StackRefData[1024];

                const int GCInteriorFlag = 1;
                const int GCPinnedFlag = 2;
                uint fetched = 0;
                do
                {
                    if (handleEnum.Next((uint)refs.Length, refs, out fetched) < 0)
                        break;

                    for (uint i = 0; i < fetched && i < refs.Length; ++i)
                    {
                        if (refs[i].Object == 0)
                            continue;

                        bool pinned = (refs[i].Flags & GCPinnedFlag) == GCPinnedFlag;
                        bool interior = (refs[i].Flags & GCInteriorFlag) == GCInteriorFlag;

                        ClrType type = null;

                        if (!interior)
                            type = heap.GetObjectType(refs[i].Object);

                        if (interior || type != null)
                            yield return new LocalVarRoot(refs[i].Address, refs[i].Object, type, domain, thread, pinned, false, interior);
                    }
                } while (fetched == refs.Length);
            }
        }

        internal override ulong GetFirstThread()
        {
            IThreadStoreData threadStore = GetThreadStoreData();
            return threadStore != null ? threadStore.FirstThread : 0;
        }

        internal override IThreadData GetThread(ulong addr)
        {
            if (addr == 0)
                return null;

            V4ThreadData data;
            if (_sos.GetThreadData(addr, out data) < 0)
                return null;

            return data;
        }

        internal override IHeapDetails GetSvrHeapDetails(ulong addr)
        {
            V4HeapDetails data;
            if (_sos.GetGCHeapDetails(addr, out data) < 0)
                return null;
            return data;
        }

        internal override IHeapDetails GetWksHeapDetails()
        {
            V4HeapDetails data;
            if (_sos.GetGCHeapStaticData(out data) < 0)
                return null;
            return data;
        }

        internal override ulong[] GetServerHeapList()
        {
            uint needed;
            ulong[] refs = new ulong[HeapCount];
            if (_sos.GetGCHeapList((uint)HeapCount, refs, out needed) < 0)
                return null;

            return refs;
        }

        internal override IList<ulong> GetAppDomainList(int count)
        {
            ulong[] data = new ulong[1024];
            uint needed;
            if (_sos.GetAppDomainList((uint)data.Length, data, out needed) < 0)
                return null;

            List<ulong> list = new List<ulong>((int)needed);

            for (uint i = 0; i < needed; ++i)
                list.Add(data[i]);

            return list;
        }

        internal override ulong[] GetAssemblyList(ulong appDomain, int count)
        {
            // It's not valid to request an assembly list for the system domain in v4.5.
            if (appDomain == SystemDomainAddress)
                return new ulong[0];

            int needed;
            if (count <= 0)
            {
                if (_sos.GetAssemblyList(appDomain, 0, null, out needed) < 0)
                    return new ulong[0];

                count = needed;
            }

            // We ignore the return value here since modules might be partially
            // filled even if GetAssemblyList hits an error.
            ulong[] modules = new ulong[count];
            _sos.GetAssemblyList(appDomain, modules.Length, modules, out needed);

            return modules;
        }

        internal override ulong[] GetModuleList(ulong assembly, int count)
        {
            uint needed = (uint)count;

            if (count <= 0)
            {
                if (_sos.GetAssemblyModuleList(assembly, 0, null, out needed) < 0)
                    return new ulong[0];
            }

            // We ignore the return value here since modules might be partially
            // filled even if GetAssemblyList hits an error.
            ulong[] modules = new ulong[needed];
            _sos.GetAssemblyModuleList(assembly, needed, modules, out needed);
            return modules;
        }

        internal override IAssemblyData GetAssemblyData(ulong domain, ulong assembly)
        {
            LegacyAssemblyData data;
            if (_sos.GetAssemblyData(domain, assembly, out data) < 0)
            {
                // The dac seems to have an issue where the assembly data can be filled in for a minidump.
                // If the data is partially filled in, we'll use it.
                if (data.Address != assembly)
                    return null;
            }

            return data;
        }

        internal override IAppDomainStoreData GetAppDomainStoreData()
        {
            LegacyAppDomainStoreData data;
            if (_sos.GetAppDomainStoreData(out data) < 0)
                return null;

            return data;
        }

        internal override IMethodTableData GetMethodTableData(ulong addr)
        {
            V45MethodTableData data;
            if (_sos.GetMethodTableData(addr, out data) < 0)
                return null;

            return data;
        }

        internal override ulong GetMethodTableByEEClass(ulong eeclass)
        {
            ulong value;
            if (_sos.GetMethodTableForEEClass(eeclass, out value) != 0)
                return 0;

            return value;
        }

        internal override IGCInfo GetGCInfoImpl()
        {
            LegacyGCInfo gcInfo;
            return (_sos.GetGCHeapData(out gcInfo) >= 0) ? (IGCInfo)gcInfo : null;
        }

        internal override bool GetCommonMethodTables(ref CommonMethodTables mCommonMTs)
        {
            return _sos.GetUsefulGlobals(out mCommonMTs) >= 0;
        }

        internal override string GetNameForMT(ulong mt)
        {
            uint count;
            if (_sos.GetMethodTableName(mt, 0, null, out count) < 0)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.Capacity = (int)count;

            if (_sos.GetMethodTableName(mt, count, sb, out count) < 0)
                return null;

            return sb.ToString();
        }

        internal override string GetPEFileName(ulong addr)
        {
            uint needed;
            if (_sos.GetPEFileName(addr, 0, null, out needed) < 0)
                return null;

            StringBuilder sb = new StringBuilder((int)needed);
            if (_sos.GetPEFileName(addr, needed, sb, out needed) < 0)
                return null;

            return sb.ToString();
        }

        internal override IModuleData GetModuleData(ulong addr)
        {
            V45ModuleData data;
            return _sos.GetModuleData(addr, out data) >= 0 ? (IModuleData)data : null;
        }

        internal override ulong GetModuleForMT(ulong addr)
        {
            V45MethodTableData data;
            if (_sos.GetMethodTableData(addr, out data) < 0)
                return 0;

            return data.module;
        }

        internal override ISegmentData GetSegmentData(ulong addr)
        {
            V4SegmentData seg;
            if (_sos.GetHeapSegmentData(addr, out seg) < 0)
                return null;
            return seg;
        }

        internal override IAppDomainData GetAppDomainData(ulong addr)
        {
            LegacyAppDomainData data = new LegacyAppDomainData(); ;
            if (_sos.GetAppDomainData(addr, out data) < 0)
            {
                // We can face an exception while walking domain data if we catch the process
                // at a bad state.  As a workaround we will return partial data if data.Address
                // and data.StubHeap are set.
                if (data.Address != addr && data.StubHeap != 0)
                    return null;
            }

            return data;
        }

        internal override string GetAppDomaminName(ulong addr)
        {
            uint count;
            if (_sos.GetAppDomainName(addr, 0, null, out count) < 0)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.Capacity = (int)count;

            if (_sos.GetAppDomainName(addr, count, sb, out count) < 0)
                return null;

            return sb.ToString();
        }

        internal override string GetAssemblyName(ulong addr)
        {
            uint count;
            if (_sos.GetAssemblyName(addr, 0, null, out count) < 0)
                return null;

            StringBuilder sb = new StringBuilder();
            sb.Capacity = (int)count;

            if (_sos.GetAssemblyName(addr, count, sb, out count) < 0)
                return null;

            return sb.ToString();
        }

        internal override bool TraverseHeap(ulong heap, DesktopRuntimeBase.LoaderHeapTraverse callback)
        {
            bool res = _sos.TraverseLoaderHeap(heap, Marshal.GetFunctionPointerForDelegate(callback)) >= 0;
            GC.KeepAlive(callback);
            return res;
        }

        internal override bool TraverseStubHeap(ulong appDomain, int type, DesktopRuntimeBase.LoaderHeapTraverse callback)
        {
            bool res = _sos.TraverseVirtCallStubHeap(appDomain, (uint)type, Marshal.GetFunctionPointerForDelegate(callback)) >= 0;
            GC.KeepAlive(callback);
            return res;
        }

        internal override IEnumerable<ICodeHeap> EnumerateJitHeaps()
        {
            LegacyJitManagerInfo[] jitManagers = null;

            uint needed = 0;
            int res = _sos.GetJitManagerList(0, null, out needed);
            if (res >= 0)
            {
                jitManagers = new LegacyJitManagerInfo[needed];
                res = _sos.GetJitManagerList(needed, jitManagers, out needed);
            }

            if (res >= 0 && jitManagers != null)
            {
                for (int i = 0; i < jitManagers.Length; ++i)
                {
                    if (jitManagers[i].type != CodeHeapType.Unknown)
                        continue;

                    res = _sos.GetCodeHeapList(jitManagers[i].addr, 0, null, out needed);
                    if (res >= 0 && needed > 0)
                    {
                        LegacyJitCodeHeapInfo[] heapInfo = new LegacyJitCodeHeapInfo[needed];
                        res = _sos.GetCodeHeapList(jitManagers[i].addr, needed, heapInfo, out needed);

                        if (res >= 0)
                        {
                            for (int j = 0; j < heapInfo.Length; ++j)
                            {
                                yield return (ICodeHeap)heapInfo[i];
                            }
                        }
                    }
                }
            }
        }

        internal override IFieldInfo GetFieldInfo(ulong mt)
        {
            V4FieldInfo fieldInfo;
            if (_sos.GetMethodTableFieldData(mt, out fieldInfo) < 0)
                return null;

            return fieldInfo;
        }

        internal override IFieldData GetFieldData(ulong fieldDesc)
        {
            LegacyFieldData data;
            if (_sos.GetFieldDescData(fieldDesc, out data) < 0)
                return null;

            return data;
        }

        internal override ICorDebug.IMetadataImport GetMetadataImport(ulong module)
        {
            object obj = null;
            if (module == 0 || _sos.GetModule(module, out obj) < 0)
                return null;

            RegisterForRelease(obj);
            return obj as ICorDebug.IMetadataImport;
        }

        internal override IObjectData GetObjectData(ulong objRef)
        {
            V45ObjectData data;
            if (_sos.GetObjectData(objRef, out data) < 0)
                return null;
            return data;
        }

        internal override IList<MethodTableTokenPair> GetMethodTableList(ulong module)
        {
            List<MethodTableTokenPair> mts = new List<MethodTableTokenPair>();
            int res = _sos.TraverseModuleMap(0, module, new ModuleMapTraverse(delegate (uint index, ulong mt, IntPtr token)
                { mts.Add(new MethodTableTokenPair(mt, index)); }),
                IntPtr.Zero);

            return mts;
        }

        internal override IDomainLocalModuleData GetDomainLocalModule(ulong appDomain, ulong id)
        {
            V45DomainLocalModuleData data;
            int res = _sos.GetDomainLocalModuleDataFromAppDomain(appDomain, (int)id, out data);
            if (res < 0)
                return null;

            return data;
        }

        internal override COMInterfacePointerData[] GetCCWInterfaces(ulong ccw, int count)
        {
            COMInterfacePointerData[] data = new COMInterfacePointerData[count];
            uint pNeeded;
            if (_sos.GetCCWInterfaces(ccw, (uint)count, data, out pNeeded) >= 0)
                return data;

            return null;
        }

        internal override COMInterfacePointerData[] GetRCWInterfaces(ulong rcw, int count)
        {
            COMInterfacePointerData[] data = new COMInterfacePointerData[count];
            uint pNeeded;
            if (_sos.GetRCWInterfaces(rcw, (uint)count, data, out pNeeded) >= 0)
                return data;

            return null;
        }
        internal override ICCWData GetCCWData(ulong ccw)
        {
            V45CCWData data;
            if (ccw != 0 && _sos.GetCCWData(ccw, out data) >= 0)
                return data;

            return null;
        }

        internal override IRCWData GetRCWData(ulong rcw)
        {
            V45RCWData data;
            if (rcw != 0 && _sos.GetRCWData(rcw, out data) >= 0)
                return data;

            return null;
        }
        #endregion

        internal override ulong GetThreadStaticPointer(ulong thread, ClrElementType type, uint offset, uint moduleId, bool shared)
        {
            ulong addr = offset;

            V45ThreadLocalModuleData data;
            if (_sos.GetThreadLocalModuleData(thread, moduleId, out data) < 0)
                return 0;

            if (IsObjectReference(type) || IsValueClass(type))
                addr += data.pGCStaticDataStart;
            else
                addr += data.pNonGCStaticDataStart;

            return addr;
        }

        internal override IDomainLocalModuleData GetDomainLocalModule(ulong module)
        {
            V45DomainLocalModuleData data;
            if (_sos.GetDomainLocalModuleDataFromModule(module, out data) < 0)
                return null;

            return data;
        }

        internal override IList<ulong> GetMethodDescList(ulong methodTable)
        {
            V45MethodTableData mtData;
            if (_sos.GetMethodTableData(methodTable, out mtData) < 0)
                return null;

            List<ulong> mds = new List<ulong>((int)mtData.wNumMethods);

            CodeHeaderData header;
            ulong ip = 0;
            for (uint i = 0; i < mtData.wNumMethods; ++i)
                if (_sos.GetMethodTableSlot(methodTable, i, out ip) >= 0)
                {
                    if (_sos.GetCodeHeaderData(ip, out header) >= 0)
                        mds.Add(header.MethodDescPtr);
                }

            return mds;
        }

        internal override string GetNameForMD(ulong md)
        {
            StringBuilder sb = new StringBuilder();
            uint needed = 0;
            if (_sos.GetMethodDescName(md, 0, null, out needed) < 0)
                return "UNKNOWN";

            sb.Capacity = (int)needed;
            if (_sos.GetMethodDescName(md, (uint)sb.Capacity, sb, out needed) < 0)
                return "UNKNOWN";

            return sb.ToString();
        }

        internal override IMethodDescData GetMethodDescData(ulong md)
        {
            V45MethodDescDataWrapper wrapper = new V45MethodDescDataWrapper();
            if (!wrapper.Init(_sos, md))
                return null;

            return wrapper;
        }

        internal override uint GetMetadataToken(ulong mt)
        {
            V45MethodTableData data;
            if (_sos.GetMethodTableData(mt, out data) < 0)
                return uint.MaxValue;

            return data.token;
        }

        protected override DesktopStackFrame GetStackFrame(DesktopThread thread, int res, ulong ip, ulong framePtr, ulong frameVtbl)
        {
            DesktopStackFrame frame;
            StringBuilder sb = new StringBuilder();
            sb.Capacity = 256;
            uint needed;
            if (res >= 0 && frameVtbl != 0)
            {
                ClrMethod innerMethod = null;
                string frameName = "Unknown Frame";
                if (_sos.GetFrameName(frameVtbl, (uint)sb.Capacity, sb, out needed) >= 0)
                    frameName = sb.ToString();

                ulong md = 0;
                if (_sos.GetMethodDescPtrFromFrame(framePtr, out md) == 0)
                {
                    V45MethodDescDataWrapper mdData = new V45MethodDescDataWrapper();
                    if (mdData.Init(_sos, md))
                        innerMethod = DesktopMethod.Create(this, mdData);
                }

                frame = new DesktopStackFrame(this, thread, framePtr, frameName, innerMethod);
            }
            else
            {
                ulong md;
                if (_sos.GetMethodDescPtrFromIP(ip, out md) >= 0)
                {
                    frame = new DesktopStackFrame(this, thread, ip, framePtr, md);
                }
                else
                {
                    frame = new DesktopStackFrame(this, thread, ip, framePtr, 0);
                }
            }

            return frame;
        }

        private bool GetStackTraceFromField(ClrType type, ulong obj, out ulong stackTrace)
        {
            stackTrace = 0;
            var field = type.GetFieldByName("_stackTrace");
            if (field == null)
                return false;

            object tmp = field.GetValue(obj);
            if (tmp == null || !(tmp is ulong))
                return false;

            stackTrace = (ulong)tmp;
            return true;
        }


        internal override IList<ClrStackFrame> GetExceptionStackTrace(ulong obj, ClrType type)
        {
            // TODO: Review this and if it works on v4.5, merge the two implementations back into RuntimeBase.
            List<ClrStackFrame> result = new List<ClrStackFrame>();
            if (type == null)
                return result;

            ulong _stackTrace;
            if (!GetStackTraceFromField(type, obj, out _stackTrace))
            {
                if (!ReadPointer(obj + GetStackTraceOffset(), out _stackTrace))
                    return result;
            }

            if (_stackTrace == 0)
                return result;

            ClrHeap heap = GetHeap();
            ClrType stackTraceType = heap.GetObjectType(_stackTrace);
            if (stackTraceType == null || !stackTraceType.IsArray)
                return result;

            int len = stackTraceType.GetArrayLength(_stackTrace);
            if (len == 0)
                return result;

            int elementSize = IntPtr.Size * 4;
            ulong dataPtr = _stackTrace + (ulong)(IntPtr.Size * 2);
            ulong count = 0;
            if (!ReadPointer(dataPtr, out count))
                return result;

            // Skip size and header
            dataPtr += (ulong)(IntPtr.Size * 2);

            DesktopThread thread = null;
            for (int i = 0; i < (int)count; ++i)
            {
                ulong ip, sp, md;
                if (!ReadPointer(dataPtr, out ip))
                    break;
                if (!ReadPointer(dataPtr + (ulong)IntPtr.Size, out sp))
                    break;
                if (!ReadPointer(dataPtr + (ulong)(2 * IntPtr.Size), out md))
                    break;

                if (i == 0)
                    thread = (DesktopThread)GetThreadByStackAddress(sp);

                result.Add(new DesktopStackFrame(this, thread, ip, sp, md));

                dataPtr += (ulong)elementSize;
            }

            return result;
        }

        internal override IThreadStoreData GetThreadStoreData()
        {
            LegacyThreadStoreData data;
            if (_sos.GetThreadStoreData(out data) < 0)
                return null;

            return data;
        }

        internal override string GetAppBase(ulong appDomain)
        {
            uint needed;
            if (_sos.GetApplicationBase(appDomain, 0, null, out needed) < 0)
                return null;

            StringBuilder builder = new StringBuilder((int)needed);
            if (_sos.GetApplicationBase(appDomain, (int)needed, builder, out needed) < 0)
                return null;

            return builder.ToString();
        }

        internal override string GetConfigFile(ulong appDomain)
        {
            uint needed;
            if (_sos.GetAppDomainConfigFile(appDomain, 0, null, out needed) < 0)
                return null;

            StringBuilder builder = new StringBuilder((int)needed);
            if (_sos.GetAppDomainConfigFile(appDomain, (int)needed, builder, out needed) < 0)
                return null;

            return builder.ToString();
        }

        internal override IMethodDescData GetMDForIP(ulong ip)
        {
            ulong md;
            if (_sos.GetMethodDescPtrFromIP(ip, out md) < 0 || md == 0)
            {
                CodeHeaderData codeHeaderData;
                if (_sos.GetCodeHeaderData(ip, out codeHeaderData) < 0)
                {
                    return null;
                }
                if ((md = codeHeaderData.MethodDescPtr) == 0)
                    return null;
            }

            V45MethodDescDataWrapper mdWrapper = new V45MethodDescDataWrapper();
            if (!mdWrapper.Init(_sos, md))
                return null;

            return mdWrapper;
        }

        protected override ulong GetThreadFromThinlock(uint threadId)
        {
            ulong thread;
            if (_sos.GetThreadFromThinlockID(threadId, out thread) < 0)
                return 0;

            return thread;
        }

        internal override int GetSyncblkCount()
        {
            LegacySyncBlkData data;
            if (_sos.GetSyncBlockData(1, out data) < 0)
                return 0;

            return (int)data.TotalCount;
        }

        internal override ISyncBlkData GetSyncblkData(int index)
        {
            LegacySyncBlkData data;
            if (_sos.GetSyncBlockData((uint)index + 1, out data) < 0)
                return null;

            return data;
        }

        internal override IThreadPoolData GetThreadPoolData()
        {
            V45ThreadPoolData data;
            if (_sos.GetThreadpoolData(out data) < 0)
                return null;

            return data;
        }

        internal override uint GetTlsSlot()
        {
            uint result = 0;
            if (_sos.GetTLSIndex(out result) < 0)
                return uint.MaxValue;

            return result;
        }

        internal override uint GetThreadTypeIndex()
        {
            return 11;
        }

        protected override uint GetRWLockDataOffset()
        {
            if (PointerSize == 8)
                return 0x30;
            else
                return 0x18;
        }

        internal override IEnumerable<NativeWorkItem> EnumerateWorkItems()
        {
            V45ThreadPoolData data;
            if (_sos.GetThreadpoolData(out data) == 0)
            {
                ulong request = data.FirstWorkRequest;
                while (request != 0)
                {
                    V45WorkRequestData requestData;
                    if (_sos.GetWorkRequestData(request, out requestData) != 0)
                        break;

                    yield return new DesktopNativeWorkItem(requestData);
                    request = requestData.NextWorkRequest;
                }
            }
        }

        internal override uint GetStringFirstCharOffset()
        {
            if (PointerSize == 8)
                return 0xc;

            return 8;
        }

        internal override uint GetStringLengthOffset()
        {
            if (PointerSize == 8)
                return 0x8;

            return 0x4;
        }

        internal override uint GetExceptionHROffset()
        {
            return PointerSize == 8 ? 0x8cu : 0x40u;
        }
    }
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("3E269830-4A2B-4301-8EE2-D6805B29B2FA")]



    #region V45 Dac Interface

    internal interface ISOSHandleEnum
    {
        void Skip(uint count);
        void Reset();
        void GetCount(out uint count);
        [PreserveSig]
        int Next(uint count, [Out, MarshalAs(UnmanagedType.LPArray)] HandleData[] handles, out uint pNeeded);
    }
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("8FA642BD-9F10-4799-9AA3-512AE78C77EE")]

    internal interface ISOSStackRefEnum
    {
        void Skip(uint count);
        void Reset();
        void GetCount(out uint count);
        [PreserveSig]
        int Next(uint count, [Out, MarshalAs(UnmanagedType.LPArray)] StackRefData[] handles, out uint pNeeded);
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void ModuleMapTraverse(uint index, ulong methodTable, IntPtr token);
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("436f00f2-b42a-4b9f-870c-e73db66ae930")]

    internal interface ISOSDac
    {
        // ThreadStore
        [PreserveSig]
        int GetThreadStoreData(out LegacyThreadStoreData data);

        // AppDomains
        [PreserveSig]
        int GetAppDomainStoreData(out LegacyAppDomainStoreData data);
        [PreserveSig]
        int GetAppDomainList(uint count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]ulong[] values, out uint pNeeded);
        [PreserveSig]
        int GetAppDomainData(ulong addr, out LegacyAppDomainData data);
        [PreserveSig]
        int GetAppDomainName(ulong addr, uint count, [Out]StringBuilder lpFilename, out uint pNeeded);
        [PreserveSig]
        int GetDomainFromContext(ulong context, out ulong domain);

        // Assemblies
        [PreserveSig]
        int GetAssemblyList(ulong appDomain, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] ulong[] values, out int pNeeded);
        [PreserveSig]
        int GetAssemblyData(ulong baseDomainPtr, ulong assembly, out LegacyAssemblyData data);
        [PreserveSig]
        int GetAssemblyName(ulong assembly, uint count, [Out] StringBuilder name, out uint pNeeded);

        // Modules
        [PreserveSig]
        int GetModule(ulong addr, [Out, MarshalAs(UnmanagedType.IUnknown)] out object module);
        [PreserveSig]
        int GetModuleData(ulong moduleAddr, out V45ModuleData data);
        [PreserveSig]
        int TraverseModuleMap(int mmt, ulong moduleAddr, [In, MarshalAs(UnmanagedType.FunctionPtr)] ModuleMapTraverse pCallback, IntPtr token);
        [PreserveSig]
        int GetAssemblyModuleList(ulong assembly, uint count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] ulong[] modules, out uint pNeeded);
        [PreserveSig]
        int GetILForModule(ulong moduleAddr, uint rva, out ulong il);

        // Threads
        [PreserveSig]
        int GetThreadData(ulong thread, out V4ThreadData data);
        [PreserveSig]
        int GetThreadFromThinlockID(uint thinLockId, out ulong pThread);
        [PreserveSig]
        int GetStackLimits(ulong threadPtr, out ulong lower, out ulong upper, out ulong fp);

        // MethodDescs
        [PreserveSig]
        int GetMethodDescData(ulong methodDesc, ulong ip, out V45MethodDescData data, uint cRevertedRejitVersions, V45ReJitData[] rgRevertedRejitData, out ulong pcNeededRevertedRejitData);
        [PreserveSig]
        int GetMethodDescPtrFromIP(ulong ip, out ulong ppMD);
        [PreserveSig]
        int GetMethodDescName(ulong methodDesc, uint count, [Out] StringBuilder name, out uint pNeeded);
        [PreserveSig]
        int GetMethodDescPtrFromFrame(ulong frameAddr, out ulong ppMD);
        [PreserveSig]
        int GetMethodDescFromToken(ulong moduleAddr, uint token, out ulong methodDesc);
        [PreserveSig]
        int GetMethodDescTransparencyData_do_not_use();//(ulong methodDesc, out DacpMethodDescTransparencyData data);

        // JIT Data
        [PreserveSig]
        int GetCodeHeaderData(ulong ip, out CodeHeaderData data);
        [PreserveSig]
        int GetJitManagerList(uint count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] LegacyJitManagerInfo[] jitManagers, out uint pNeeded);
        [PreserveSig]
        int GetJitHelperFunctionName(ulong ip, uint count, char name, out uint pNeeded);
        [PreserveSig]
        int GetJumpThunkTarget_do_not_use(uint ctx, out ulong targetIP, out ulong targetMD);

        // ThreadPool
        [PreserveSig]
        int GetThreadpoolData(out V45ThreadPoolData data);
        [PreserveSig]
        int GetWorkRequestData(ulong addrWorkRequest, out V45WorkRequestData data);
        [PreserveSig]
        int GetHillClimbingLogEntry_do_not_use(); //(ulong addr, out DacpHillClimbingLogEntry data);

        // Objects
        [PreserveSig]
        int GetObjectData(ulong objAddr, out V45ObjectData data);
        [PreserveSig]
        int GetObjectStringData(ulong obj, uint count, [Out] StringBuilder stringData, out uint pNeeded);
        [PreserveSig]
        int GetObjectClassName(ulong obj, uint count, [Out] StringBuilder className, out uint pNeeded);

        // MethodTable
        [PreserveSig]
        int GetMethodTableName(ulong mt, uint count, [Out] StringBuilder mtName, out uint pNeeded);
        [PreserveSig]
        int GetMethodTableData(ulong mt, out V45MethodTableData data);
        [PreserveSig]
        int GetMethodTableSlot(ulong mt, uint slot, out ulong value);
        [PreserveSig]
        int GetMethodTableFieldData(ulong mt, out V4FieldInfo data);
        [PreserveSig]
        int GetMethodTableTransparencyData_do_not_use(); //(ulong mt, out DacpMethodTableTransparencyData data);

        // EEClass
        [PreserveSig]
        int GetMethodTableForEEClass(ulong eeClass, out ulong value);

        // FieldDesc
        [PreserveSig]
        int GetFieldDescData(ulong fieldDesc, out LegacyFieldData data);

        // Frames
        [PreserveSig]
        int GetFrameName(ulong vtable, uint count, [Out] StringBuilder frameName, out uint pNeeded);


        // PEFiles
        [PreserveSig]
        int GetPEFileBase(ulong addr, [Out] out ulong baseAddr);

        [PreserveSig]
        int GetPEFileName(ulong addr, uint count, [Out] StringBuilder ptr, [Out] out uint pNeeded);

        // GC
        [PreserveSig]
        int GetGCHeapData(out LegacyGCInfo data);
        [PreserveSig]
        int GetGCHeapList(uint count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ulong[] heaps, out uint pNeeded); // svr only
        [PreserveSig]
        int GetGCHeapDetails(ulong heap, out V4HeapDetails details); // wks only
        [PreserveSig]
        int GetGCHeapStaticData(out V4HeapDetails data);
        [PreserveSig]
        int GetHeapSegmentData(ulong seg, out V4SegmentData data);
        [PreserveSig]
        int GetOOMData_do_not_use(); //(ulong oomAddr, out DacpOomData data);
        [PreserveSig]
        int GetOOMStaticData_do_not_use(); //(out DacpOomData data);
        [PreserveSig]
        int GetHeapAnalyzeData_do_not_use(); //(ulong addr, out  DacpGcHeapAnalyzeData data);
        [PreserveSig]
        int GetHeapAnalyzeStaticData_do_not_use(); //(out DacpGcHeapAnalyzeData data);

        // DomainLocal
        [PreserveSig]
        int GetDomainLocalModuleData_do_not_use(); //(ulong addr, out DacpDomainLocalModuleData data);
        [PreserveSig]
        int GetDomainLocalModuleDataFromAppDomain(ulong appDomainAddr, int moduleID, out V45DomainLocalModuleData data);
        [PreserveSig]
        int GetDomainLocalModuleDataFromModule(ulong moduleAddr, out V45DomainLocalModuleData data);

        // ThreadLocal
        [PreserveSig]
        int GetThreadLocalModuleData(ulong thread, uint index, out V45ThreadLocalModuleData data);

        // SyncBlock
        [PreserveSig]
        int GetSyncBlockData(uint number, out LegacySyncBlkData data);
        [PreserveSig]
        int GetSyncBlockCleanupData_do_not_use(); //(ulong addr, out DacpSyncBlockCleanupData data);

        // Handles
        [PreserveSig]
        int GetHandleEnum([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppHandleEnum);
        [PreserveSig]
        int GetHandleEnumForTypes([In] uint[] types, uint count, [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppHandleEnum);
        [PreserveSig]
        int GetHandleEnumForGC(uint gen, [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppHandleEnum);

        // EH
        [PreserveSig]
        int TraverseEHInfo_do_not_use(); //(ulong ip, DUMPEHINFO pCallback, IntPtr token);
        [PreserveSig]
        int GetNestedExceptionData(ulong exception, out ulong exceptionObject, out ulong nextNestedException);

        // StressLog
        [PreserveSig]
        int GetStressLogAddress(out ulong stressLog);

        // Heaps
        [PreserveSig]
        int TraverseLoaderHeap(ulong loaderHeapAddr, IntPtr pCallback);
        [PreserveSig]
        int GetCodeHeapList(ulong jitManager, uint count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] LegacyJitCodeHeapInfo[] codeHeaps, out uint pNeeded);
        [PreserveSig]
        int TraverseVirtCallStubHeap(ulong pAppDomain, uint heaptype, IntPtr pCallback);

        // Other
        [PreserveSig]
        int GetUsefulGlobals(out CommonMethodTables data);
        [PreserveSig]
        int GetClrWatsonBuckets(ulong thread, out IntPtr pGenericModeBlock);
        [PreserveSig]
        int GetTLSIndex(out uint pIndex);
        [PreserveSig]
        int GetDacModuleHandle(out IntPtr phModule);

        // COM
        [PreserveSig]
        int GetRCWData(ulong addr, out V45RCWData data);
        [PreserveSig]
        int GetRCWInterfaces(ulong rcw, uint count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] COMInterfacePointerData[] interfaces, out uint pNeeded);
        [PreserveSig]
        int GetCCWData(ulong ccw, out V45CCWData data);
        [PreserveSig]
        int GetCCWInterfaces(ulong ccw, uint count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] COMInterfacePointerData[] interfaces, out uint pNeeded);
        [PreserveSig]
        int TraverseRCWCleanupList_do_not_use(); //(ulong cleanupListPtr, VISITRCWFORCLEANUP pCallback, LPVOID token);

        // GC Reference Functions
        [PreserveSig]
        int GetStackReferences(uint osThreadID, [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppEnum);
        [PreserveSig]
        int GetRegisterName(int regName, uint count, [Out] StringBuilder buffer, out uint pNeeded);


        [PreserveSig]
        int GetThreadAllocData(ulong thread, ref V45AllocData data);
        [PreserveSig]
        int GetHeapAllocData(uint count, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] V45GenerationAllocData[] data, out uint pNeeded);

        // For BindingDisplay plugin
        [PreserveSig]
        int GetFailedAssemblyList(ulong appDomain, int count, ulong[] values, out uint pNeeded);
        [PreserveSig]
        int GetPrivateBinPaths(ulong appDomain, int count, [Out] StringBuilder paths, out uint pNeeded);
        [PreserveSig]
        int GetAssemblyLocation(ulong assembly, int count, [Out] StringBuilder location, out uint pNeeded);
        [PreserveSig]
        int GetAppDomainConfigFile(ulong appDomain, int count, [Out] StringBuilder configFile, out uint pNeeded);
        [PreserveSig]
        int GetApplicationBase(ulong appDomain, int count, [Out] StringBuilder appBase, out uint pNeeded);
        [PreserveSig]
        int GetFailedAssemblyData(ulong assembly, out uint pContext, out int pResult);
        [PreserveSig]
        int GetFailedAssemblyLocation(ulong assesmbly, uint count, [Out] StringBuilder location, out uint pNeeded);
        [PreserveSig]
        int GetFailedAssemblyDisplayName(ulong assembly, uint count, [Out] StringBuilder name, out uint pNeeded);
    }
    #endregion

#pragma warning disable 0649
#pragma warning disable 0169


    #region V45 Structs
    internal struct V45ThreadPoolData : IThreadPoolData
    {
        private int _cpuUtilization;
        private int _numIdleWorkerThreads;
        private int _numWorkingWorkerThreads;
        private int _numRetiredWorkerThreads;
        private int _minLimitTotalWorkerThreads;
        private int _maxLimitTotalWorkerThreads;

        private ulong _firstUnmanagedWorkRequest;

        private ulong _hillClimbingLog;
        private int _hillClimbingLogFirstIndex;
        private int _hillClimbingLogSize;

        private int _numTimers;

        private int _numCPThreads;
        private int _numFreeCPThreads;
        private int _maxFreeCPThreads;
        private int _numRetiredCPThreads;
        private int _maxLimitTotalCPThreads;
        private int _currentLimitTotalCPThreads;
        private int _minLimitTotalCPThreads;

        private ulong _asyncTimerCallbackCompletionFPtr;

        public int MinCP
        {
            get { return _minLimitTotalCPThreads; }
        }

        public int MaxCP
        {
            get { return _maxLimitTotalCPThreads; }
        }

        public int CPU
        {
            get { return _cpuUtilization; }
        }

        public int NumFreeCP
        {
            get { return _numFreeCPThreads; }
        }

        public int MaxFreeCP
        {
            get { return _maxFreeCPThreads; }
        }

        public int TotalThreads
        {
            get { return _numIdleWorkerThreads + _numWorkingWorkerThreads + _numRetiredWorkerThreads; }
        }

        public int RunningThreads
        {
            get { return _numWorkingWorkerThreads; }
        }

        public int IdleThreads
        {
            get { return _numIdleWorkerThreads; }
        }

        public int MinThreads
        {
            get { return _minLimitTotalWorkerThreads; }
        }

        public int MaxThreads
        {
            get { return _maxLimitTotalWorkerThreads; }
        }


        public ulong FirstWorkRequest
        {
            get { return _firstUnmanagedWorkRequest; }
        }


        public ulong QueueUserWorkItemCallbackFPtr
        {
            get { return ulong.MaxValue; }
        }

        public ulong AsyncCallbackCompletionFPtr
        {
            get { return ulong.MaxValue; }
        }

        ulong IThreadPoolData.AsyncTimerCallbackCompletionFPtr
        {
            get { return ulong.MaxValue; }
        }
    }

    internal struct V45ThreadLocalModuleData
    {
        private ulong _threadAddr;
        private ulong _moduleIndex;

        private ulong _pClassData;
        private ulong _pDynamicClassTable;
        public ulong pGCStaticDataStart;
        public ulong pNonGCStaticDataStart;
    }

    internal struct V45DomainLocalModuleData : IDomainLocalModuleData
    {
        private ulong _appDomainAddr;
        private ulong _moduleID;

        private ulong _pClassData;
        private ulong _pDynamicClassTable;
        private ulong _pGCStaticDataStart;
        private ulong _pNonGCStaticDataStart;

        public ulong AppDomainAddr
        {
            get { return _appDomainAddr; }
        }

        public ulong ModuleID
        {
            get { return _moduleID; }
        }

        public ulong ClassData
        {
            get { return _pClassData; }
        }

        public ulong DynamicClassTable
        {
            get { return _pDynamicClassTable; }
        }

        public ulong GCStaticDataStart
        {
            get { return _pGCStaticDataStart; }
        }

        public ulong NonGCStaticDataStart
        {
            get { return _pNonGCStaticDataStart; }
        }
    }

    internal struct StackRefData
    {
        public uint HasRegisterInformation;
        public int Register;
        public int Offset;
        public ulong Address;
        public ulong Object;
        public uint Flags;

        public uint SourceType;
        public ulong Source;
        public ulong StackPointer;
    }
    [StructLayout(LayoutKind.Sequential)]

    internal struct HandleData
    {
        public ulong AppDomain;
        public ulong Handle;
        public ulong Secondary;
        public uint Type;
        public uint StrongReference;

        // For RefCounted Handles
        public uint RefCount;
        public uint JupiterRefCount;
        public uint IsPegged;
    }

    internal struct V45ReJitData
    {
        private ulong _rejitID;
        private uint _flags;
        private ulong _nativeCodeAddr;
    }

    internal class V45MethodDescDataWrapper : IMethodDescData
    {
        public bool Init(ISOSDac sos, ulong md)
        {
            ulong count = 0;
            V45MethodDescData data = new V45MethodDescData();
            if (sos.GetMethodDescData(md, 0, out data, 0, null, out count) < 0)
                return false;


            _md = data.MethodDescPtr;
            _ip = data.NativeCodeAddr;
            _module = data.ModulePtr;
            _token = data.MDToken;
            _mt = data.MethodTablePtr;

            CodeHeaderData header;
            if (sos.GetCodeHeaderData(data.NativeCodeAddr, out header) >= 0)
            {
                if (header.JITType == 1)
                    _jitType = MethodCompilationType.Jit;
                else if (header.JITType == 2)
                    _jitType = MethodCompilationType.Ngen;
                else
                    _jitType = MethodCompilationType.None;
            }
            else
            {
                _jitType = MethodCompilationType.None;
            }

            return true;
        }

        private MethodCompilationType _jitType;
        private ulong _md, _module, _ip;
        private uint _token;
        private ulong _mt;

        public ulong MethodDesc
        {
            get { return _md; }
        }

        public ulong Module
        {
            get { return _module; }
        }

        public uint MDToken
        {
            get { return _token; }
        }

        public ulong NativeCodeAddr
        {
            get { return _ip; }
        }

        public MethodCompilationType JITType
        {
            get { return _jitType; }
        }


        public ulong MethodTable
        {
            get { return _mt; }
        }
    }

    internal struct V45MethodDescData
    {
        private uint _bHasNativeCode;
        private uint _bIsDynamic;
        private short _wSlotNumber;
        internal ulong NativeCodeAddr;
        // Useful for breaking when a method is jitted.
        private ulong _addressOfNativeCodeSlot;

        internal ulong MethodDescPtr;
        internal ulong MethodTablePtr;
        internal ulong ModulePtr;

        internal uint MDToken;
        private ulong _GCInfo;
        private ulong _GCStressCodeCopy;

        // This is only valid if bIsDynamic is true
        private ulong _managedDynamicMethodObject;

        private ulong _requestedIP;

        // Gives info for the single currently active version of a method
        private V45ReJitData _rejitDataCurrent;

        // Gives info corresponding to requestedIP (for !ip2md)
        private V45ReJitData _rejitDataRequested;

        // Total number of rejit versions that have been jitted
        private uint _cJittedRejitVersions;
    }

    internal struct CodeHeaderData
    {
        public ulong GCInfo;
        public uint JITType;
        public ulong MethodDescPtr;
        public ulong MethodStart;
        public uint MethodSize;
        public ulong ColdRegionStart;
        public uint ColdRegionSize;
        public uint HotRegionSize;
    }

    internal struct V45ModuleData : IModuleData
    {
        public ulong address;
        public ulong peFile;
        public ulong ilBase;
        public ulong metadataStart;
        public ulong metadataSize;
        public ulong assembly;
        public uint bIsReflection;
        public uint bIsPEFile;
        public ulong dwBaseClassIndex;
        public ulong dwModuleID;
        public uint dwTransientFlags;
        public ulong TypeDefToMethodTableMap;
        public ulong TypeRefToMethodTableMap;
        public ulong MethodDefToDescMap;
        public ulong FieldDefToDescMap;
        public ulong MemberRefToDescMap;
        public ulong FileReferencesMap;
        public ulong ManifestModuleReferencesMap;
        public ulong pLookupTableHeap;
        public ulong pThunkHeap;
        public ulong dwModuleIndex;

        #region IModuleData
        public ulong Assembly
        {
            get
            {
                return assembly;
            }
        }

        public ulong PEFile
        {
            get
            {
                return (bIsPEFile == 0) ? ilBase : peFile;
            }
        }
        public ulong LookupTableHeap
        {
            get { return pLookupTableHeap; }
        }
        public ulong ThunkHeap
        {
            get { return pThunkHeap; }
        }


        public object LegacyMetaDataImport
        {
            get { return null; }
        }


        public ulong ModuleId
        {
            get { return dwModuleID; }
        }

        public ulong ModuleIndex
        {
            get { return dwModuleIndex; }
        }

        public bool IsReflection
        {
            get { return bIsReflection != 0; }
        }

        public bool IsPEFile
        {
            get { return bIsPEFile != 0; }
        }
        public ulong ImageBase
        {
            get { return ilBase; }
        }
        public ulong MetdataStart
        {
            get { return metadataStart; }
        }

        public ulong MetadataLength
        {
            get { return metadataSize; }
        }
        #endregion
    }

    internal struct V45ObjectData : IObjectData
    {
        private ulong _methodTable;
        private uint _objectType;
        private ulong _size;
        private ulong _elementTypeHandle;
        private uint _elementType;
        private uint _dwRank;
        private ulong _dwNumComponents;
        private ulong _dwComponentSize;
        private ulong _arrayDataPtr;
        private ulong _arrayBoundsPtr;
        private ulong _arrayLowerBoundsPtr;

        private ulong _rcw;
        private ulong _ccw;

        public ClrElementType ElementType { get { return (ClrElementType)_elementType; } }
        public ulong ElementTypeHandle { get { return _elementTypeHandle; } }
        public ulong RCW { get { return _rcw; } }
        public ulong CCW { get { return _ccw; } }

        public ulong DataPointer
        {
            get { return _arrayDataPtr; }
        }
    }

    internal struct V45MethodTableData : IMethodTableData
    {
        public uint bIsFree; // everything else is NULL if this is true.
        public ulong module;
        public ulong eeClass;
        public ulong parentMethodTable;
        public ushort wNumInterfaces;
        public ushort wNumMethods;
        public ushort wNumVtableSlots;
        public ushort wNumVirtuals;
        public uint baseSize;
        public uint componentSize;
        public uint token;
        public uint dwAttrClass;
        public uint isShared; // flags & enum_flag_DomainNeutral
        public uint isDynamic;
        public uint containsPointers;

        public bool ContainsPointers
        {
            get { return containsPointers != 0; }
        }

        public uint BaseSize
        {
            get { return baseSize; }
        }

        public uint ComponentSize
        {
            get { return componentSize; }
        }

        public ulong EEClass
        {
            get { return eeClass; }
        }

        public bool Free
        {
            get { return bIsFree != 0; }
        }

        public ulong Parent
        {
            get { return parentMethodTable; }
        }

        public bool Shared
        {
            get { return isShared != 0; }
        }


        public uint NumMethods
        {
            get { return wNumMethods; }
        }


        public ulong ElementTypeHandle
        {
            get { throw new NotImplementedException(); }
        }
    }

    internal struct V45CCWData : ICCWData
    {
        private ulong _outerIUnknown;
        private ulong _managedObject;
        private ulong _handle;
        private ulong _ccwAddress;

        private int _refCount;
        private int _interfaceCount;
        private uint _isNeutered;

        private int _jupiterRefCount;
        private uint _isPegged;
        private uint _isGlobalPegged;
        private uint _hasStrongRef;
        private uint _isExtendsCOMObject;
        private uint _hasWeakReference;
        private uint _isAggregated;

        public ulong IUnknown
        {
            get { return _outerIUnknown; }
        }

        public ulong Object
        {
            get { return _managedObject; }
        }

        public ulong Handle
        {
            get { return _handle; }
        }

        public ulong CCWAddress
        {
            get { return _ccwAddress; }
        }

        public int RefCount
        {
            get { return _refCount; }
        }

        public int JupiterRefCount
        {
            get { return _jupiterRefCount; }
        }

        public int InterfaceCount
        {
            get { return _interfaceCount; }
        }
    }

    internal struct COMInterfacePointerData
    {
        public ulong MethodTable;
        public ulong InterfacePtr;
        public ulong ComContext;
    }

    internal struct V45RCWData : IRCWData
    {
        private ulong _identityPointer;
        private ulong _unknownPointer;
        private ulong _managedObject;
        private ulong _jupiterObject;
        private ulong _vtablePtr;
        private ulong _creatorThread;
        private ulong _ctxCookie;

        private int _refCount;
        private int _interfaceCount;

        private uint _isJupiterObject;
        private uint _supportsIInspectable;
        private uint _isAggregated;
        private uint _isContained;
        private uint _isFreeThreaded;
        private uint _isDisconnected;

        public ulong IdentityPointer
        {
            get { return _identityPointer; }
        }

        public ulong UnknownPointer
        {
            get { return _unknownPointer; }
        }

        public ulong ManagedObject
        {
            get { return _managedObject; }
        }

        public ulong JupiterObject
        {
            get { return _jupiterObject; }
        }

        public ulong VTablePtr
        {
            get { return _vtablePtr; }
        }

        public ulong CreatorThread
        {
            get { return _creatorThread; }
        }

        public int RefCount
        {
            get { return _refCount; }
        }

        public int InterfaceCount
        {
            get { return _interfaceCount; }
        }

        public bool IsJupiterObject
        {
            get { return _isJupiterObject != 0; }
        }

        public bool IsDisconnected
        {
            get { return _isDisconnected != 0; }
        }
    }


    internal struct V45WorkRequestData
    {
        public ulong Function;
        public ulong Context;
        public ulong NextWorkRequest;
    }

    #endregion

#pragma warning restore 0169
#pragma warning restore 0649
}
