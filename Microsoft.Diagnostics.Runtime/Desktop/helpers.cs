// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal class AppDomainHeapWalker
    {
        #region Variables
        private enum InternalHeapTypes
        {
            IndcellHeap,
            LookupHeap,
            ResolveHeap,
            DispatchHeap,
            CacheEntryHeap
        }

        private List<MemoryRegion> _regions = new List<MemoryRegion>();
        private DesktopRuntimeBase.LoaderHeapTraverse _delegate;
        private ClrMemoryRegionType _type;
        private ulong _appDomain;
        private DesktopRuntimeBase _runtime;
        #endregion

        public AppDomainHeapWalker(DesktopRuntimeBase runtime)
        {
            _runtime = runtime;
            _delegate = new DesktopRuntimeBase.LoaderHeapTraverse(VisitOneHeap);
        }

        public IEnumerable<MemoryRegion> EnumerateHeaps(IAppDomainData appDomain)
        {
            Debug.Assert(appDomain != null);
            _appDomain = appDomain.Address;
            _regions.Clear();

            // Standard heaps.
            _type = ClrMemoryRegionType.LowFrequencyLoaderHeap;
            _runtime.TraverseHeap(appDomain.LowFrequencyHeap, _delegate);

            _type = ClrMemoryRegionType.HighFrequencyLoaderHeap;
            _runtime.TraverseHeap(appDomain.HighFrequencyHeap, _delegate);

            _type = ClrMemoryRegionType.StubHeap;
            _runtime.TraverseHeap(appDomain.StubHeap, _delegate);

            // Stub heaps.
            _type = ClrMemoryRegionType.IndcellHeap;
            _runtime.TraverseStubHeap(_appDomain, (int)InternalHeapTypes.IndcellHeap, _delegate);

            _type = ClrMemoryRegionType.LookupHeap;
            _runtime.TraverseStubHeap(_appDomain, (int)InternalHeapTypes.LookupHeap, _delegate);

            _type = ClrMemoryRegionType.ResolveHeap;
            _runtime.TraverseStubHeap(_appDomain, (int)InternalHeapTypes.ResolveHeap, _delegate);

            _type = ClrMemoryRegionType.DispatchHeap;
            _runtime.TraverseStubHeap(_appDomain, (int)InternalHeapTypes.DispatchHeap, _delegate);

            _type = ClrMemoryRegionType.CacheEntryHeap;
            _runtime.TraverseStubHeap(_appDomain, (int)InternalHeapTypes.CacheEntryHeap, _delegate);

            return _regions;
        }

        public IEnumerable<MemoryRegion> EnumerateModuleHeaps(IAppDomainData appDomain, ulong addr)
        {
            Debug.Assert(appDomain != null);
            _appDomain = appDomain.Address;
            _regions.Clear();

            if (addr == 0)
                return _regions;

            IModuleData module = _runtime.GetModuleData(addr);
            if (module != null)
            {
                _type = ClrMemoryRegionType.ModuleThunkHeap;
                _runtime.TraverseHeap(module.ThunkHeap, _delegate);

                _type = ClrMemoryRegionType.ModuleLookupTableHeap;
                _runtime.TraverseHeap(module.LookupTableHeap, _delegate);
            }

            return _regions;
        }

        public IEnumerable<MemoryRegion> EnumerateJitHeap(ulong heap)
        {
            _appDomain = 0;
            _regions.Clear();

            _type = ClrMemoryRegionType.JitLoaderCodeHeap;
            _runtime.TraverseHeap(heap, _delegate);

            return _regions;
        }

        #region Helper Functions
        private void VisitOneHeap(ulong address, IntPtr size, int isCurrent)
        {
            if (_appDomain == 0)
                _regions.Add(new MemoryRegion(_runtime, address, (ulong)size.ToInt64(), _type));
            else
                _regions.Add(new MemoryRegion(_runtime, address, (ulong)size.ToInt64(), _type, _appDomain));
        }
        #endregion

    }

    internal class HandleTableWalker
    {
        #region Variables
        private DesktopRuntimeBase _runtime;
        private ClrHeap _heap;
        private int _max = 10000;
        private VISITHANDLEV2 _mV2Delegate;
        private VISITHANDLEV4 _mV4Delegate;
        #endregion

        #region Properties
        public List<ClrHandle> Handles { get; private set; }
        public byte[] V4Request
        {
            get
            {
                // MULTITHREAD ISSUE
                if (_mV4Delegate == null)
                    _mV4Delegate = new VISITHANDLEV4(VisitHandleV4);

                IntPtr functionPtr = Marshal.GetFunctionPointerForDelegate(_mV4Delegate);
                byte[] request = new byte[IntPtr.Size * 2];
                FunctionPointerToByteArray(functionPtr, request, 0);

                return request;
            }
        }


        public byte[] V2Request
        {
            get
            {
                // MULTITHREAD ISSUE
                if (_mV2Delegate == null)
                    _mV2Delegate = new VISITHANDLEV2(VisitHandleV2);

                IntPtr functionPtr = Marshal.GetFunctionPointerForDelegate(_mV2Delegate);
                byte[] request = new byte[IntPtr.Size * 2];

                FunctionPointerToByteArray(functionPtr, request, 0);

                return request;
            }
        }
        #endregion

        #region Functions
        public HandleTableWalker(DesktopRuntimeBase dac)
        {
            _runtime = dac;
            _heap = dac.GetHeap();
            Handles = new List<ClrHandle>();
        }
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]

        private delegate int VISITHANDLEV4(ulong HandleAddr, ulong HandleValue, int HandleType, uint ulRefCount, ulong appDomainPtr, IntPtr token);

        private int VisitHandleV4(ulong addr, ulong obj, int hndType, uint refCnt, ulong appDomain, IntPtr unused)
        {
            Debug.Assert(unused == IntPtr.Zero);

            return AddHandle(addr, obj, hndType, refCnt, 0, appDomain);
        }
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]


        private delegate int VISITHANDLEV2(ulong HandleAddr, ulong HandleValue, int HandleType, ulong appDomainPtr, IntPtr token);

        private int VisitHandleV2(ulong addr, ulong obj, int hndType, ulong appDomain, IntPtr unused)
        {
            Debug.Assert(unused == IntPtr.Zero);

            // V2 cannot actually get the ref count from a handle.  We'll always report the RefCount as
            // 1 in this case so the user will treat this as a strong handle (which the majority of COM
            // handles are).
            uint refCnt = 0;
            if (hndType == (uint)HandleType.RefCount)
                refCnt = 1;

            return AddHandle(addr, obj, hndType, refCnt, 0, appDomain);
        }

        public int AddHandle(ulong addr, ulong obj, int hndType, uint refCnt, uint dependentTarget, ulong appDomain)
        {
            ulong mt;
            ulong cmt;

            // If we fail to get the MT of this object, just skip it and keep going
            if (!GetMethodTables(obj, out mt, out cmt))
                return _max-- > 0 ? 1 : 0;

            ClrHandle handle = new ClrHandle();
            handle.Address = addr;
            handle.Object = obj;
            handle.Type = _heap.GetObjectType(obj);
            handle.HandleType = (HandleType)hndType;
            handle.RefCount = refCnt;
            handle.AppDomain = _runtime.GetAppDomainByAddress(appDomain);
            handle.DependentTarget = dependentTarget;

            if (dependentTarget != 0)
                handle.DependentType = _heap.GetObjectType(dependentTarget);

            Handles.Add(handle);
            handle = handle.GetInteriorHandle();
            if (handle != null)
                Handles.Add(handle);

            // Stop if we have too many handles (likely infinite loop in dac due to
            // inconsistent data).
            return _max-- > 0 ? 1 : 0;
        }

        private bool GetMethodTables(ulong obj, out ulong mt, out ulong cmt)
        {
            mt = 0;
            cmt = 0;

            byte[] data = new byte[IntPtr.Size * 3];        // TODO assumes bitness same as dump
            int read = 0;
            if (!_runtime.ReadMemory(obj, data, data.Length, out read) || read != data.Length)
                return false;

            if (IntPtr.Size == 4)
                mt = BitConverter.ToUInt32(data, 0);
            else
                mt = BitConverter.ToUInt64(data, 0);

            if (mt == _runtime.ArrayMethodTable)
            {
                if (IntPtr.Size == 4)
                    cmt = BitConverter.ToUInt32(data, 2 * IntPtr.Size);
                else
                    cmt = BitConverter.ToUInt64(data, 2 * IntPtr.Size);
            }

            return true;
        }

        private static void FunctionPointerToByteArray(IntPtr functionPtr, byte[] request, int start)
        {
            long ptr = functionPtr.ToInt64();

            for (int i = start; i < start + sizeof(ulong); ++i)
            {
                request[i] = (byte)ptr;
                ptr >>= 8;
            }
        }
        #endregion
    }
}
