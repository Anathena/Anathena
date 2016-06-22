// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal class DesktopInterfaceData : ComInterfaceData
    {
        public override ClrType Type
        {
            get { return _type; }
        }

        public override Address InterfacePointer
        {
            get { return _interface; }
        }

        public DesktopInterfaceData(ClrType type, Address ptr)
        {
            _type = type;
            _interface = ptr;
        }

        private Address _interface;
        private ClrType _type;
    }


    internal class DesktopCCWData : CcwData
    {
        public override Address IUnknown { get { return _ccw.IUnknown; } }
        public override Address Object { get { return _ccw.Object; } }
        public override Address Handle { get { return _ccw.Handle; } }
        public override int RefCount { get { return _ccw.RefCount + _ccw.JupiterRefCount; } }

        public override IList<ComInterfaceData> Interfaces
        {
            get
            {
                if (_interfaces != null)
                    return _interfaces;

                _heap.LoadAllTypes();

                _interfaces = new List<ComInterfaceData>();

                COMInterfacePointerData[] interfaces = _heap.DesktopRuntime.GetCCWInterfaces(_addr, _ccw.InterfaceCount);
                for (int i = 0; i < interfaces.Length; ++i)
                {
                    ClrType type = null;
                    if (interfaces[i].MethodTable != 0)
                        type = _heap.GetTypeByMethodTable(interfaces[i].MethodTable, 0);

                    _interfaces.Add(new DesktopInterfaceData(type, interfaces[i].InterfacePtr));
                }

                return _interfaces;
            }
        }

        internal DesktopCCWData(DesktopGCHeap heap, Address ccw, ICCWData data)
        {
            _addr = ccw;
            _ccw = data;
            _heap = heap;
        }

        private Address _addr;
        private ICCWData _ccw;
        private DesktopGCHeap _heap;
        private List<ComInterfaceData> _interfaces;
    }

    internal class DesktopRCWData : RcwData
    {
        //public ulong IdentityPointer { get; }
        public override Address IUnknown { get { return _rcw.UnknownPointer; } }
        public override Address VTablePointer { get { return _rcw.VTablePtr; } }
        public override int RefCount { get { return _rcw.RefCount; } }
        public override Address Object { get { return _rcw.ManagedObject; } }
        public override bool Disconnected { get { return _rcw.IsDisconnected; } }
        public override Address WinRTObject
        {
            get { return _rcw.JupiterObject; }
        }
        public override uint CreatorThread
        {
            get
            {
                if (_osThreadID == uint.MaxValue)
                {
                    IThreadData data = _heap.DesktopRuntime.GetThread(_rcw.CreatorThread);
                    if (data == null || data.OSThreadID == uint.MaxValue)
                        _osThreadID = 0;
                    else
                        _osThreadID = data.OSThreadID;
                }

                return _osThreadID;
            }
        }

        public override IList<ComInterfaceData> Interfaces
        {
            get
            {
                if (_interfaces != null)
                    return _interfaces;

                _heap.LoadAllTypes();

                _interfaces = new List<ComInterfaceData>();

                COMInterfacePointerData[] interfaces = _heap.DesktopRuntime.GetRCWInterfaces(_addr, _rcw.InterfaceCount);
                for (int i = 0; i < interfaces.Length; ++i)
                {
                    ClrType type = null;
                    if (interfaces[i].MethodTable != 0)
                        type = _heap.GetTypeByMethodTable(interfaces[i].MethodTable, 0);

                    _interfaces.Add(new DesktopInterfaceData(type, interfaces[i].InterfacePtr));
                }

                return _interfaces;
            }
        }

        internal DesktopRCWData(DesktopGCHeap heap, Address rcw, IRCWData data)
        {
            _addr = rcw;
            _rcw = data;
            _heap = heap;
            _osThreadID = uint.MaxValue;
        }

        private IRCWData _rcw;
        private DesktopGCHeap _heap;
        private uint _osThreadID;
        private List<ComInterfaceData> _interfaces;
        private Address _addr;
    }
}
