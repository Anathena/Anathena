// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime.Native
{
    internal class NativeStackRootWalker
    {
        private ClrHeap _heap;
        private ClrAppDomain _domain;
        private ClrThread _thread;
        public List<ClrRoot> Roots { get; set; }

        public NativeStackRootWalker(ClrHeap heap, ClrAppDomain domain, ClrThread thread)
        {
            _heap = heap;
            _domain = domain;
            _thread = thread;
            Roots = new List<ClrRoot>();
        }

        public void Callback(IntPtr token, ulong symbol, ulong addr, ulong obj, int pinned, int interior)
        {
            string name = "local variable";
            NativeStackRoot root = new NativeStackRoot(_thread, addr, obj, name, _heap.GetObjectType(obj), _domain, pinned != 0, interior != 0);
            Roots.Add(root);
        }
    }

    internal class NativeHandleRootWalker
    {
        public List<ClrRoot> Roots { get; set; }
        private ClrHeap _heap;
        private ClrAppDomain _domain;
        private bool _dependentSupport;

        public NativeHandleRootWalker(NativeRuntime runtime, bool dependentHandleSupport)
        {
            _heap = runtime.GetHeap();
            _domain = runtime.GetRhAppDomain();
            _dependentSupport = dependentHandleSupport;
        }

        public void RootCallback(IntPtr ptr, ulong addr, ulong obj, int hndType, uint refCount, int strong)
        {
            bool isDependent = hndType == (int)HandleType.Dependent;
            if ((isDependent && _dependentSupport) || strong != 0)
            {
                if (Roots == null)
                    Roots = new List<ClrRoot>(128);

                string name = Enum.GetName(typeof(HandleType), hndType) + " handle";
                if (isDependent)
                {
                    ulong dependentTarget = obj;
                    if (!_heap.ReadPointer(addr, out obj))
                        obj = 0;

                    ClrType type = _heap.GetObjectType(obj);
                    Roots.Add(new NativeHandleRoot(addr, obj, dependentTarget, type, hndType, _domain, name));
                }
                else
                {
                    ClrType type = _heap.GetObjectType(obj);
                    Roots.Add(new NativeHandleRoot(addr, obj, type, hndType, _domain, name));
                }
            }
        }
    }

    internal class NativeStaticRootWalker
    {
        public List<ClrRoot> Roots { get; set; }
        private NativeRuntime _runtime;
        private ClrHeap _heap;

        public NativeStaticRootWalker(NativeRuntime runtime, bool resolveStatics)
        {
            Roots = new List<ClrRoot>(128);
            _runtime = resolveStatics ? runtime : null;
            _heap = _runtime.GetHeap();
        }

        public void Callback(IntPtr token, ulong addr, ulong obj, int pinned, int interior)
        {
            // TODO:  Resolve name of addr.
            string name = "static var " + addr.ToString();
            var type = _heap.GetObjectType(obj);
            Roots.Add(new NativeStaticVar(_runtime, addr, obj, type, name, interior != 0, pinned != 0));
        }
    }

    internal class NativeStackRoot : ClrRoot
    {
        private string _name;
        private ClrType _type;
        private ClrAppDomain _appDomain;
        private ClrThread _thread;
        private bool _pinned;
        private bool _interior;

        public override GCRootKind Kind
        {
            get { return GCRootKind.LocalVar; }
        }

        public override ClrType Type
        {
            get { return _type; }
        }

        public override bool IsPinned
        {
            get
            {
                return _pinned;
            }
        }

        public override bool IsInterior
        {
            get
            {
                return _interior;
            }
        }

        public override ClrAppDomain AppDomain
        {
            get
            {
                return _appDomain;
            }
        }

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override ClrThread Thread
        {
            get
            {
                return _thread;
            }
        }

        public NativeStackRoot(ClrThread thread, ulong addr, ulong obj, string name, ClrType type, ClrAppDomain domain, bool pinned, bool interior)
        {
            Address = addr;
            Object = obj;
            _name = name;
            _type = type;
            _appDomain = domain;
            _pinned = pinned;
            _interior = interior;
            _thread = thread;
        }
    }

    internal class NativeHandleRoot : ClrRoot
    {
        private string _name;
        private ClrType _type;
        private ClrAppDomain _appDomain;
        private GCRootKind _kind;

        public override GCRootKind Kind
        {
            get { return _kind; }
        }

        public override ClrType Type
        {
            get { return _type; }
        }

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override ClrAppDomain AppDomain
        {
            get
            {
                return _appDomain;
            }
        }


        public NativeHandleRoot(Address addr, Address obj, Address dependentTarget, ClrType type, int hndType, ClrAppDomain domain, string name)
        {
            Init(addr, obj, dependentTarget, type, hndType, domain, name);
        }

        public NativeHandleRoot(Address addr, Address obj, ClrType type, int hndType, ClrAppDomain domain, string name)
        {
            Init(addr, obj, 0, type, hndType, domain, name);
        }

        private void Init(Address addr, Address obj, Address dependentTarget, ClrType type, int hndType, ClrAppDomain domain, string name)
        {
            HandleType htype = (HandleType)hndType;
            switch (htype)
            {
                case HandleType.AsyncPinned:
                    _kind = GCRootKind.AsyncPinning;
                    break;

                case HandleType.Pinned:
                    _kind = GCRootKind.Pinning;
                    break;

                case HandleType.WeakShort:
                case HandleType.WeakLong:
                    _kind = GCRootKind.Weak;
                    break;

                default:
                    _kind = GCRootKind.Strong;
                    break;
            }

            Address = addr;
            _name = name;
            _type = type;
            _appDomain = domain;

            if (htype == HandleType.Dependent && dependentTarget != 0)
                Object = dependentTarget;
            else
                Object = obj;
        }
    }


    internal class NativeStaticVar : ClrRoot
    {
        private string _name;
        private bool _pinned;
        private bool _interior;
        private ClrType _type;
        private ClrAppDomain _appDomain;

        public override GCRootKind Kind
        {
            get { return GCRootKind.StaticVar; }
        }

        public override ClrType Type
        {
            get { return _type; }
        }

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override bool IsPinned
        {
            get
            {
                return _pinned;
            }
        }

        public override bool IsInterior
        {
            get
            {
                return _interior;
            }
        }

        public override ClrAppDomain AppDomain
        {
            get
            {
                return _appDomain;
            }
        }

        public NativeStaticVar(NativeRuntime runtime, Address addr, Address obj, ClrType type, string name, bool pinned, bool interior)
        {
            Address = addr;
            Object = obj;
            _type = type;
            _name = name;
            _pinned = pinned;
            _interior = interior;
            _type = runtime.GetHeap().GetObjectType(obj);
            _appDomain = runtime.GetRhAppDomain();
        }
    }
}
