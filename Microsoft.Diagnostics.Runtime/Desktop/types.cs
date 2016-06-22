// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Diagnostics.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal class DesktopHeapInterface : ClrInterface
    {
        private string _name;
        private ClrInterface _base;
        public DesktopHeapInterface(string name, ClrInterface baseInterface)
        {
            _name = name;
            _base = baseInterface;
        }

        public override string Name
        {
            get { return _name; }
        }

        public override ClrInterface BaseInterface
        {
            get { return _base; }
        }
    }

    internal abstract class BaseDesktopHeapType : ClrType
    {
        protected ClrElementType _elementType;
        protected uint _token;
        private IList<ClrInterface> _interfaces;
        public bool Shared { get; internal set; }
        internal abstract ulong GetModuleAddress(ClrAppDomain domain);


        public BaseDesktopHeapType(DesktopGCHeap heap, DesktopBaseModule module, uint token)
        {
            DesktopHeap = heap;
            DesktopModule = module;
            _token = token;
        }

        internal override ClrMethod GetMethod(uint token)
        {
            return null;
        }

        internal DesktopGCHeap DesktopHeap { get; set; }
        internal DesktopBaseModule DesktopModule { get; set; }

        public override ClrElementType ElementType { get { return _elementType; } internal set { _elementType = value; } }

        public override uint MetadataToken
        {
            get { return _token; }
        }

        public override IList<ClrInterface> Interfaces
        {
            get
            {
                if (_interfaces == null)
                    InitInterfaces();

                Debug.Assert(_interfaces != null);
                return _interfaces;
            }
        }

        public List<ClrInterface> InitInterfaces()
        {
            if (DesktopModule == null)
            {
                _interfaces = DesktopHeap.EmptyInterfaceList;
                return null;
            }

            BaseDesktopHeapType baseType = BaseType as BaseDesktopHeapType;
            List<ClrInterface> interfaces = baseType != null ? new List<ClrInterface>(baseType.Interfaces) : null;
            ICorDebug.IMetadataImport import = DesktopModule.GetMetadataImport();
            if (import == null)
            {
                _interfaces = DesktopHeap.EmptyInterfaceList;
                return null;
            }

            IntPtr hnd = IntPtr.Zero;
            int[] mdTokens = new int[32];
            int count;

            do
            {
                int res = import.EnumInterfaceImpls(ref hnd, (int)_token, mdTokens, mdTokens.Length, out count);
                if (res < 0)
                    break;

                for (int i = 0; i < count; ++i)
                {
                    int mdClass, mdIFace;
                    res = import.GetInterfaceImplProps(mdTokens[i], out mdClass, out mdIFace);

                    if (interfaces == null)
                        interfaces = new List<ClrInterface>(count == mdTokens.Length ? 64 : count);

                    var result = GetInterface(import, mdIFace);
                    if (result != null && !interfaces.Contains(result))
                        interfaces.Add(result);
                }
            } while (count == mdTokens.Length);

            import.CloseEnum(hnd);

            if (interfaces == null)
                _interfaces = DesktopHeap.EmptyInterfaceList;
            else
                _interfaces = interfaces.ToArray();

            return interfaces;
        }



        private ClrInterface GetInterface(ICorDebug.IMetadataImport import, int mdIFace)
        {
            StringBuilder builder = new StringBuilder(1024);
            int extends, cnt;
            System.Reflection.TypeAttributes attr;
            int res = import.GetTypeDefProps(mdIFace, builder, builder.Capacity, out cnt, out attr, out extends);
            int scope;

            string name = null;
            ClrInterface result = null;
            if (res == 0)
            {
                name = builder.ToString();
            }
            else if (res == 1)
            {
                res = import.GetTypeRefProps(mdIFace, out scope, builder, builder.Capacity, out cnt);
                if (res == 0)
                {
                    name = builder.ToString();
                }
                else if (res == 1)
                {
                }
            }

            // TODO:  Handle typespec case.

            if (name != null && !DesktopHeap.Interfaces.TryGetValue(name, out result))
            {
                ClrInterface type = null;
                if (extends != 0 && extends != 0x01000000)
                    type = GetInterface(import, extends);

                result = new DesktopHeapInterface(name, type);
                DesktopHeap.Interfaces[name] = result;
            }

            return result;
        }
    }

    internal class DesktopPointerType : BaseDesktopHeapType
    {
        private ClrElementType _pointerElement;
        private ClrType _pointerElementType;
        private string _name;

        public DesktopPointerType(DesktopGCHeap heap, DesktopBaseModule module, ClrElementType eltype, uint token, string nameHint)
            : base(heap, module, token)
        {
            ElementType = ClrElementType.Pointer;
            _pointerElement = eltype;
            if (nameHint != null)
                BuildName(nameHint);
        }

        public override ClrModule Module { get { return DesktopModule; } }

        public override ulong MethodTable
        {
            get
            {
                // We have no good way of finding this value, unfortunately
                return 0;
            }
        }

        public override IEnumerable<ulong> EnumerateMethodTables()
        {
            return new ulong[] { MethodTable };
        }

        internal override Address GetModuleAddress(ClrAppDomain domain)
        {
            return 0;
        }
        
        public override string Name
        {
            get
            {
                if (_name == null)
                    BuildName(null);

                return _name;
            }
        }

        private void BuildName(string hint)
        {
            StringBuilder builder = new StringBuilder();
            ClrType inner = ComponentType;

            builder.Append(inner != null ? inner.Name : GetElementTypeName(hint));
            builder.Append("*");
            _name = builder.ToString();
        }

        private string GetElementTypeName(string hint)
        {
            switch (_pointerElement)
            {
                case ClrElementType.Boolean:
                    return "System.Boolean";

                case ClrElementType.Char:
                    return "System.Char";

                case ClrElementType.Int8:
                    return "System.SByte";

                case ClrElementType.UInt8:
                    return "System.Byte";

                case ClrElementType.Int16:
                    return "System.Int16";

                case ClrElementType.UInt16:
                    return "ClrElementType.UInt16";

                case ClrElementType.Int32:
                    return "System.Int32";

                case ClrElementType.UInt32:
                    return "System.UInt32";

                case ClrElementType.Int64:
                    return "System.Int64";

                case ClrElementType.UInt64:
                    return "System.UInt64";

                case ClrElementType.Float:
                    return "System.Single";

                case ClrElementType.Double:
                    return "System.Double";

                case ClrElementType.NativeInt:
                    return "System.IntPtr";

                case ClrElementType.NativeUInt:
                    return "System.UIntPtr";

                case ClrElementType.Struct:
                    return "Sytem.ValueType";
            }

            if (hint != null)
                return hint;

            return "POINTER";
        }

        public override bool IsFinalizeSuppressed(Address obj)
        {
            return false;
        }

        public override ClrType ComponentType
        {
            get
            {
                if (_pointerElementType == null)
                    _pointerElementType = DesktopHeap.GetBasicType(_pointerElement);

                return _pointerElementType;
            }
            internal set
            {
                if (value != null)
                    _pointerElementType = value;
            }
        }

        override public bool IsPointer { get { return true; } }

        override public IList<ClrInstanceField> Fields { get { return new ClrInstanceField[0]; } }

        override public IList<ClrStaticField> StaticFields { get { return new ClrStaticField[0]; } }

        override public IList<ClrThreadStaticField> ThreadStaticFields { get { return new ClrThreadStaticField[0]; } }

        override public IList<ClrMethod> Methods { get { return new ClrMethod[0]; } }

        public override Address GetSize(Address objRef)
        {
            ClrType realType = DesktopHeap.GetObjectType(objRef);
            return realType.GetSize(objRef);
        }

        public override void EnumerateRefsOfObject(Address objRef, Action<Address, int> action)
        {
            ClrType realType = DesktopHeap.GetObjectType(objRef);
            realType.EnumerateRefsOfObject(objRef, action);
        }

        public override ClrHeap Heap
        {
            get { return DesktopHeap; }
        }

        public override IList<ClrInterface> Interfaces
        {
            get { return new ClrInterface[0]; }
        }

        public override bool IsFinalizable
        {
            get { return false; }
        }

        public override bool IsPublic
        {
            get { return true; }
        }

        public override bool IsPrivate
        {
            get { return false; }
        }

        public override bool IsInternal
        {
            get { return false; }
        }

        public override bool IsProtected
        {
            get { return false; }
        }

        public override bool IsAbstract
        {
            get { return false; }
        }

        public override bool IsSealed
        {
            get { return false; }
        }

        public override bool IsInterface
        {
            get { return false; }
        }

        public override bool GetFieldForOffset(int fieldOffset, bool inner, out ClrInstanceField childField, out int childFieldOffset)
        {
            childField = null;
            childFieldOffset = 0;
            return false;
        }

        public override ClrInstanceField GetFieldByName(string name)
        {
            return null;
        }

        public override ClrStaticField GetStaticFieldByName(string name)
        {
            return null;
        }

        public override ClrType BaseType
        {
            get { return null; } // TODO: Determine if null should be the correct return value
        }

        public override int GetArrayLength(Address objRef)
        {
            return 0;
        }

        public override Address GetArrayElementAddress(Address objRef, int index)
        {
            return 0;
        }

        public override object GetArrayElementValue(Address objRef, int index)
        {
            return null;
        }

        public override int ElementSize
        {
            get { return DesktopInstanceField.GetSize(null, _pointerElement); } //TODO GET HELP
        }

        public override int BaseSize
        {
            get { return IntPtr.Size * 8; } //TODO GET HELP
        }

        public override void EnumerateRefsOfObjectCarefully(Address objRef, Action<Address, int> action) // TODO GET HELP
        {
            ClrType realType = DesktopHeap.GetObjectType(objRef);
            realType.EnumerateRefsOfObjectCarefully(objRef, action);
        }
    }


    internal class DesktopArrayType : BaseDesktopHeapType
    {
        private ClrElementType _arrayElement;
        private ClrType _arrayElementType;
        private int _ranks;
        private string _name;

        public DesktopArrayType(DesktopGCHeap heap, DesktopBaseModule module, ClrElementType eltype, int ranks, uint token, string nameHint)
            : base(heap, module, token)
        {
            ElementType = ClrElementType.Array;
            _arrayElement = eltype;
            _ranks = ranks;
            if (nameHint != null)
                BuildName(nameHint);
        }

        internal override ClrMethod GetMethod(uint token)
        {
            return null;
        }

        public override ulong MethodTable
        {
            get
            {
                // Unfortunately this is a "fake" type (we constructed it because we could not
                // get the real type from the dac APIs).  So we have nothing we can return here.
                return 0;
            }
        }

        public override IEnumerable<ulong> EnumerateMethodTables()
        {
            return new ulong[] { MethodTable };
        }

        public override ClrModule Module { get { return DesktopModule; } }


        internal override Address GetModuleAddress(ClrAppDomain domain)
        {
            return 0;
        }
        
        public override string Name
        {
            get
            {
                if (_name == null)
                    BuildName(null);

                return _name;
            }
        }

        private void BuildName(string hint)
        {
            StringBuilder builder = new StringBuilder();
            ClrType inner = ComponentType;

            builder.Append(inner != null ? inner.Name : GetElementTypeName(hint));
            builder.Append("[");

            for (int i = 0; i < _ranks - 1; ++i)
                builder.Append(",");

            builder.Append("]");
            _name = builder.ToString();
        }

        private string GetElementTypeName(string hint)
        {
            switch (_arrayElement)
            {
                case ClrElementType.Boolean:
                    return "System.Boolean";

                case ClrElementType.Char:
                    return "System.Char";

                case ClrElementType.Int8:
                    return "System.SByte";

                case ClrElementType.UInt8:
                    return "System.Byte";

                case ClrElementType.Int16:
                    return "System.Int16";

                case ClrElementType.UInt16:
                    return "ClrElementType.UInt16";

                case ClrElementType.Int32:
                    return "System.Int32";

                case ClrElementType.UInt32:
                    return "System.UInt32";

                case ClrElementType.Int64:
                    return "System.Int64";

                case ClrElementType.UInt64:
                    return "System.UInt64";

                case ClrElementType.Float:
                    return "System.Single";

                case ClrElementType.Double:
                    return "System.Double";

                case ClrElementType.NativeInt:
                    return "System.IntPtr";

                case ClrElementType.NativeUInt:
                    return "System.UIntPtr";

                case ClrElementType.Struct:
                    return "Sytem.ValueType";
            }

            if (hint != null)
                return hint;

            return "ARRAY";
        }

        public override bool IsFinalizeSuppressed(Address obj)
        {
            return false;
        }

        public override ClrType ComponentType
        {
            get
            {
                if (_arrayElementType == null)
                    _arrayElementType = DesktopHeap.GetBasicType(_arrayElement);

                return _arrayElementType;
            }
            internal set
            {
                if (value != null)
                    _arrayElementType = value;
            }
        }

        override public bool IsArray { get { return true; } }

        override public IList<ClrInstanceField> Fields { get { return new ClrInstanceField[0]; } }

        override public IList<ClrStaticField> StaticFields { get { return new ClrStaticField[0]; } }

        override public IList<ClrThreadStaticField> ThreadStaticFields { get { return new ClrThreadStaticField[0]; } }

        override public IList<ClrMethod> Methods { get { return new ClrMethod[0]; } }

        public override Address GetSize(Address objRef)
        {
            ClrType realType = DesktopHeap.GetObjectType(objRef);
            return realType.GetSize(objRef);
        }

        public override void EnumerateRefsOfObject(Address objRef, Action<Address, int> action)
        {
            ClrType realType = DesktopHeap.GetObjectType(objRef);
            realType.EnumerateRefsOfObject(objRef, action);
        }

        public override ClrHeap Heap
        {
            get { return DesktopHeap; }
        }

        public override IList<ClrInterface> Interfaces
        { // todo
            get { return new ClrInterface[0]; }
        }

        public override bool IsFinalizable
        {
            get { return false; }
        }

        public override bool IsPublic
        {
            get { return true; }
        }

        public override bool IsPrivate
        {
            get { return false; }
        }

        public override bool IsInternal
        {
            get { return false; }
        }

        public override bool IsProtected
        {
            get { return false; }
        }

        public override bool IsAbstract
        {
            get { return false; }
        }

        public override bool IsSealed
        {
            get { return false; }
        }

        public override bool IsInterface
        {
            get { return false; }
        }

        public override bool GetFieldForOffset(int fieldOffset, bool inner, out ClrInstanceField childField, out int childFieldOffset)
        {
            childField = null;
            childFieldOffset = 0;
            return false;
        }

        public override ClrInstanceField GetFieldByName(string name)
        {
            return null;
        }

        public override ClrStaticField GetStaticFieldByName(string name)
        {
            return null;
        }

        public override ClrType BaseType
        {
            get { return DesktopHeap.ArrayType; }
        }

        public override int GetArrayLength(Address objRef)
        {
            //todo
            throw new NotImplementedException();
        }

        public override Address GetArrayElementAddress(Address objRef, int index)
        {
            throw new NotImplementedException();
        }

        public override object GetArrayElementValue(Address objRef, int index)
        {
            throw new NotImplementedException();
        }

        public override int ElementSize
        {
            get { return DesktopInstanceField.GetSize(null, _arrayElement); }
        }

        public override int BaseSize
        {
            get { return IntPtr.Size * 8; }
        }

        public override void EnumerateRefsOfObjectCarefully(Address objRef, Action<Address, int> action)
        {
            ClrType realType = DesktopHeap.GetObjectType(objRef);
            realType.EnumerateRefsOfObjectCarefully(objRef, action);
        }
    }


    internal class DesktopHeapType : BaseDesktopHeapType
    {
        ulong _cachedMethodTable;
        ulong[] _methodTables;

        public override ulong MethodTable
        {
            get
            {
                if (_cachedMethodTable != 0)
                    return _cachedMethodTable;

                if (Shared || ((DesktopRuntimeBase)Heap.Runtime).IsSingleDomain)
                    _cachedMethodTable = _constructedMT;
                else
                {
                    _cachedMethodTable = EnumerateMethodTables().FirstOrDefault();
                    if (_cachedMethodTable == 0)
                        _cachedMethodTable = _constructedMT;
                }

                Debug.Assert(_cachedMethodTable != 0);
                return _cachedMethodTable;
            }
        }

        public override IEnumerable<ulong> EnumerateMethodTables()
        {
            if (_methodTables == null && (Shared || ((DesktopRuntimeBase)Heap.Runtime).IsSingleDomain))
            {
                if (_cachedMethodTable == 0)
                {
                    // This should never happen, but we'll check to make sure.
                    Debug.Assert(_constructedMT != 0);

                    if (_constructedMT != 0)
                    {
                        _cachedMethodTable = _constructedMT;
                        _methodTables = new ulong[] { _cachedMethodTable };
                        return _methodTables;
                    }
                }
            }

            return FillAndEnumerateTypeHandles();
        }

        private IEnumerable<ulong> FillAndEnumerateTypeHandles()
        {
            IList<ClrAppDomain> domains = null;
            if (_methodTables == null)
            {
                domains = Module.AppDomains;
                _methodTables = new ulong[domains.Count];
            }
            
            for (int i = 0; i < _methodTables.Length; i++)
            {
                if (_methodTables[i] == 0)
                {
                    if (domains == null)
                        domains = Module.AppDomains;

                    ulong value = ((DesktopModule)DesktopModule).GetMTForDomain(domains[i], this);
                    _methodTables[i] = value != 0 ? value : ulong.MaxValue;
                }

                if (_methodTables[i] == ulong.MaxValue)
                    continue;

                yield return _methodTables[i];
            }
        }

        public override ClrElementType ElementType
        {
            get
            {
                if (_elementType == ClrElementType.Unknown)
                    _elementType = DesktopHeap.GetElementType(this, 0);

                return _elementType;
            }

            internal set
            {
                _elementType = value;
            }
        }

        public override string Name { get { return _name; } }
        public override ClrModule Module
        {
            get
            {
                if (DesktopModule == null)
                    return DesktopHeap.DesktopRuntime.ErrorModule;

                return DesktopModule;
            }
        }
        public override ulong GetSize(Address objRef)
        {
            ulong size;
            uint pointerSize = (uint)DesktopHeap.PointerSize;
            if (_componentSize == 0)
            {
                size = _baseSize;
            }
            else
            {
                uint count = 0;
                uint countOffset = pointerSize;
                ulong loc = objRef + countOffset;

                var cache = DesktopHeap.MemoryReader;
                if (!cache.Contains(loc))
                {
                    var runtimeCache = DesktopHeap.DesktopRuntime.MemoryReader;
                    if (runtimeCache.Contains(loc))
                        cache = DesktopHeap.DesktopRuntime.MemoryReader;
                }

                if (!cache.ReadDword(loc, out count))
                    throw new Exception("Could not read from heap at " + objRef.ToString("x"));

                // Strings in v4+ contain a trailing null terminator not accounted for.
                if (DesktopHeap.StringType == this && DesktopHeap.DesktopRuntime.CLRVersion != DesktopVersion.v2)
                    count++;

                size = count * (ulong)_componentSize + _baseSize;
            }

            uint minSize = pointerSize * 3;
            if (size < minSize)
                size = minSize;
            return size;
        }

        public override void EnumerateRefsOfObjectCarefully(Address objRef, Action<Address, int> action)
        {
            if (!_containsPointers)
                return;

            if (_gcDesc == null)
                if (!FillGCDesc() || _gcDesc == null)
                    return;

            ulong size = GetSize(objRef);

            ClrSegment seg = DesktopHeap.GetSegmentByAddress(objRef);
            if (seg == null || objRef + size > seg.End)
                return;

            var cache = DesktopHeap.MemoryReader;
            if (!cache.Contains(objRef))
                cache = DesktopHeap.DesktopRuntime.MemoryReader;

            _gcDesc.WalkObject(objRef, (ulong)size, cache, action);
        }

        public override void EnumerateRefsOfObject(Address objRef, Action<Address, int> action)
        {
            if (!_containsPointers)
                return;

            if (_gcDesc == null)
                if (!FillGCDesc() || _gcDesc == null)
                    return;

            var size = GetSize(objRef);
            var cache = DesktopHeap.MemoryReader;
            if (!cache.Contains(objRef))
                cache = DesktopHeap.DesktopRuntime.MemoryReader;

            _gcDesc.WalkObject(objRef, (ulong)size, cache, action);
        }


        private bool FillGCDesc()
        {
            DesktopRuntimeBase runtime = DesktopHeap.DesktopRuntime;

            int entries;
            if (!runtime.ReadDword(_constructedMT - (ulong)IntPtr.Size, out entries))
                return false;

            // Get entries in map
            if (entries < 0)
                entries = -entries;

            int read;
            int slots = 1 + entries * 2;
            byte[] buffer = new byte[slots * IntPtr.Size];
            if (!runtime.ReadMemory(_constructedMT - (ulong)(slots * IntPtr.Size), buffer, buffer.Length, out read) || read != buffer.Length)
                return false;

            // Construct the gc desc
            _gcDesc = new GCDesc(buffer);
            return true;
        }

        public override ClrHeap Heap { get { return DesktopHeap; } }
        public override string ToString()
        {
            return Name;
        }

        public override bool HasSimpleValue { get { return ElementType != ClrElementType.Struct; } }
        public override object GetValue(Address address)
        {
            if (IsPrimitive)
                address += (ulong)DesktopHeap.PointerSize;

            return DesktopHeap.GetValueAtAddress(ElementType, address);
        }



        public override bool IsException
        {
            get
            {
                ClrType type = this;
                while (type != null)
                    if (type == DesktopHeap.ExceptionType)
                        return true;
                    else
                        type = type.BaseType;

                return false;
            }
        }


        public override bool IsCCW(Address obj)
        {
            if (_checkedIfIsCCW)
                return !_notCCW;

            // The dac cannot report this information prior to v4.5.
            if (DesktopHeap.DesktopRuntime.CLRVersion != DesktopVersion.v45)
                return false;

            IObjectData data = DesktopHeap.GetObjectData(obj);
            _notCCW = !(data != null && data.CCW != 0);
            _checkedIfIsCCW = true;

            return !_notCCW;
        }

        public override CcwData GetCCWData(Address obj)
        {
            if (_notCCW)
                return null;

            // The dac cannot report this information prior to v4.5.
            if (DesktopHeap.DesktopRuntime.CLRVersion != DesktopVersion.v45)
                return null;

            DesktopCCWData result = null;
            IObjectData data = DesktopHeap.GetObjectData(obj);

            if (data != null && data.CCW != 0)
            {
                ICCWData ccw = DesktopHeap.DesktopRuntime.GetCCWData(data.CCW);
                if (ccw != null)
                    result = new DesktopCCWData(DesktopHeap, data.CCW, ccw);
            }
            else if (!_checkedIfIsCCW)
            {
                _notCCW = true;
            }

            _checkedIfIsCCW = true;
            return result;
        }

        public override bool IsRCW(Address obj)
        {
            if (_checkedIfIsRCW)
                return !_notRCW;

            // The dac cannot report this information prior to v4.5.
            if (DesktopHeap.DesktopRuntime.CLRVersion != DesktopVersion.v45)
                return false;

            IObjectData data = DesktopHeap.GetObjectData(obj);
            _notRCW = !(data != null && data.RCW != 0);
            _checkedIfIsRCW = true;

            return !_notRCW;
        }

        public override RcwData GetRCWData(Address obj)
        {
            // Most types can't possibly be RCWs.  
            if (_notRCW)
                return null;

            // The dac cannot report this information prior to v4.5.
            if (DesktopHeap.DesktopRuntime.CLRVersion != DesktopVersion.v45)
            {
                _notRCW = true;
                return null;
            }

            DesktopRCWData result = null;
            IObjectData data = DesktopHeap.GetObjectData(obj);

            if (data != null && data.RCW != 0)
            {
                IRCWData rcw = DesktopHeap.DesktopRuntime.GetRCWData(data.RCW);
                if (rcw != null)
                    result = new DesktopRCWData(DesktopHeap, data.RCW, rcw);
            }
            else if (!_checkedIfIsRCW)     // If the first time fails, we assume that all instances of this type can't be RCWs.
            {
                _notRCW = true;            // TODO FIX NOW review.  We really want to simply ask the runtime... 
            }

            _checkedIfIsRCW = true;
            return result;
        }

        private class EnumData
        {
            internal ClrElementType ElementType;
            internal Dictionary<string, object> NameToValue = new Dictionary<string, object>();
            internal Dictionary<object, string> ValueToName = new Dictionary<object, string>();
        }

        public override ClrElementType GetEnumElementType()
        {
            if (_enumData == null)
                InitEnumData();

            return _enumData.ElementType;
        }

        public override bool TryGetEnumValue(string name, out int value)
        {
            object val = null;
            if (TryGetEnumValue(name, out val))
            {
                value = (int)val;
                return true;
            }

            value = int.MinValue;
            return false;
        }


        public override bool TryGetEnumValue(string name, out object value)
        {
            if (_enumData == null)
                InitEnumData();

            return _enumData.NameToValue.TryGetValue(name, out value);
        }

        override public string GetEnumName(object value)
        {
            if (_enumData == null)
                InitEnumData();

            string result = null;
            _enumData.ValueToName.TryGetValue(value, out result);
            return result;
        }

        override public string GetEnumName(int value)
        {
            return GetEnumName((object)value);
        }



        public override IEnumerable<string> GetEnumNames()
        {
            if (_enumData == null)
                InitEnumData();

            return _enumData.NameToValue.Keys;
        }

        private void InitEnumData()
        {
            if (!IsEnum)
                throw new InvalidOperationException("Type is not an Enum.");

            _enumData = new EnumData();
            ICorDebug.IMetadataImport import = null;
            if (DesktopModule != null)
                import = DesktopModule.GetMetadataImport();

            if (import == null)
            {
                _enumData = new EnumData();
                return;
            }

            IntPtr hnd = IntPtr.Zero;
            int tokens;

            List<string> names = new List<string>();
            int[] fields = new int[64];
            do
            {
                int res = import.EnumFields(ref hnd, (int)_token, fields, fields.Length, out tokens);
                for (int i = 0; i < tokens; ++i)
                {
                    FieldAttributes attr;
                    int mdTypeDef, pchField, pcbSigBlob, pdwCPlusTypeFlag, pcchValue;
                    IntPtr ppvSigBlob, ppValue = IntPtr.Zero;
                    StringBuilder builder = new StringBuilder(256);

                    res = import.GetFieldProps(fields[i], out mdTypeDef, builder, builder.Capacity, out pchField, out attr, out ppvSigBlob, out pcbSigBlob, out pdwCPlusTypeFlag, out ppValue, out pcchValue);

                    if ((int)attr == 0x606 && builder.ToString() == "value__")
                    {
                        SigParser parser = new SigParser(ppvSigBlob, pcbSigBlob);
                        int sigType, elemType;

                        if (parser.GetCallingConvInfo(out sigType) && parser.GetElemType(out elemType))
                            _enumData.ElementType = (ClrElementType)elemType;
                    }

                    // public, static, literal, has default
                    int intAttr = (int)attr;
                    if ((int)attr == 0x8056)
                    {
                        string name = builder.ToString();
                        names.Add(name);

                        int ccinfo;
                        SigParser parser = new SigParser(ppvSigBlob, pcbSigBlob);
                        parser.GetCallingConvInfo(out ccinfo);
                        int elemType;
                        parser.GetElemType(out elemType);

                        Type type = ClrRuntime.GetTypeForElementType((ClrElementType)pdwCPlusTypeFlag);
                        if (type != null)
                        {
                            object o = System.Runtime.InteropServices.Marshal.PtrToStructure(ppValue, type);
                            _enumData.NameToValue[name] = o;
                            _enumData.ValueToName[o] = name;
                        }
                    }
                }
            } while (fields.Length == tokens);

            import.CloseEnum(hnd);
        }

        public override bool IsEnum
        {
            get
            {
                ClrType type = this;

                ClrType enumType = DesktopHeap.EnumType;
                while (type != null)
                {
                    if (enumType == null && type.Name == "System.Enum")
                    {
                        DesktopHeap.EnumType = type;
                        return true;
                    }
                    else if (type == enumType)
                    {
                        return true;
                    }
                    else
                    {
                        type = type.BaseType;
                    }
                }

                return false;
            }
        }


        public override bool IsFree
        {
            get
            {
                return this == DesktopHeap.FreeType;
            }
        }

        private const uint FinalizationSuppressedFlag = 0x40000000;
        public override bool IsFinalizeSuppressed(Address obj)
        {
            uint value;
            bool result = DesktopHeap.GetObjectHeader(obj, out value);

            return result && (value & FinalizationSuppressedFlag) == FinalizationSuppressedFlag;
        }

        public override bool IsFinalizable
        {
            get
            {
                if (_finalizable == 0)
                {
                    foreach (var method in Methods)
                    {
                        if (method.IsVirtual && method.Name == "Finalize")
                        {
                            _finalizable = 1;
                            break;
                        }
                    }

                    if (_finalizable == 0)
                        _finalizable = 2;
                }

                return _finalizable == 1;
            }
        }

        public override bool IsArray { get { return _componentSize != 0 && this != DesktopHeap.StringType && this != DesktopHeap.FreeType; } }
        public override bool ContainsPointers { get { return _containsPointers; } }
        public override bool IsString { get { return this == DesktopHeap.StringType; } }

        public override bool GetFieldForOffset(int fieldOffset, bool inner, out ClrInstanceField childField, out int childFieldOffset)
        {
            int ps = (int)DesktopHeap.PointerSize;
            int offset = fieldOffset;

            if (!IsArray)
            {
                if (!inner)
                    offset -= ps;

                foreach (ClrInstanceField field in Fields)
                {
                    if (field.Offset <= offset)
                    {
                        int size = field.Size;

                        if (offset < field.Offset + size)
                        {
                            childField = field;
                            childFieldOffset = offset - field.Offset;
                            return true;
                        }
                    }
                }
            }

            if (BaseType != null)
                return BaseType.GetFieldForOffset(fieldOffset, inner, out childField, out childFieldOffset);

            childField = null;
            childFieldOffset = 0;
            return false;
        }

        public override int ElementSize { get { return (int)_componentSize; } }
        public override IList<ClrInstanceField> Fields
        {
            get
            {
                if (_fields == null)
                    InitFields();

                return _fields;
            }
        }


        public override IList<ClrStaticField> StaticFields
        {
            get
            {
                if (_fields == null)
                    InitFields();

                if (_statics == null)
                    return s_emptyStatics;

                return _statics;
            }
        }

        public override IList<ClrThreadStaticField> ThreadStaticFields
        {
            get
            {
                if (_fields == null)
                    InitFields();

                if (_threadStatics == null)
                    return s_emptyThreadStatics;

                return _threadStatics;
            }
        }

        private void InitFields()
        {
            if (_fields != null)
                return;

            DesktopRuntimeBase runtime = DesktopHeap.DesktopRuntime;
            IFieldInfo fieldInfo = runtime.GetFieldInfo(_constructedMT);

            if (fieldInfo == null)
            {
                // Fill fields so we don't repeatedly try to init these fields on error.
                _fields = new List<ClrInstanceField>();
                return;
            }

            _fields = new List<ClrInstanceField>((int)fieldInfo.InstanceFields);

            // Add base type's fields.
            if (BaseType != null)
            {
                foreach (var field in BaseType.Fields)
                    _fields.Add(field);
            }

            int count = (int)(fieldInfo.InstanceFields + fieldInfo.StaticFields) - _fields.Count;
            ulong nextField = fieldInfo.FirstField;
            int i = 0;

            ICorDebug.IMetadataImport import = null;
            if (nextField != 0 && DesktopModule != null)
                import = DesktopModule.GetMetadataImport();

            while (i < count && nextField != 0)
            {
                IFieldData field = runtime.GetFieldData(nextField);
                if (field == null)
                    break;

                // We don't handle context statics.
                if (field.bIsContextLocal)
                {
                    nextField = field.nextField;
                    continue;
                }

                // Get the name of the field.
                string name = null;
                FieldAttributes attr = FieldAttributes.PrivateScope;
                int pcchValue = 0, sigLen = 0;
                IntPtr ppValue = IntPtr.Zero;
                IntPtr fieldSig = IntPtr.Zero;

                if (import != null)
                {
                    int mdTypeDef, pchField, pdwCPlusTypeFlab;
                    StringBuilder builder = new StringBuilder(256);

                    int res = import.GetFieldProps((int)field.FieldToken, out mdTypeDef, builder, builder.Capacity, out pchField, out attr, out fieldSig, out sigLen, out pdwCPlusTypeFlab, out ppValue, out pcchValue);
                    if (res >= 0)
                        name = builder.ToString();
                    else
                        fieldSig = IntPtr.Zero;
                }

                // If we couldn't figure out the name, at least give the token.
                if (import == null || name == null)
                {
                    name = string.Format("<ERROR:{0:X}>", field.FieldToken);
                }

                // construct the appropriate type of field.
                if (field.IsThreadLocal)
                {
                    if (_threadStatics == null)
                        _threadStatics = new List<ClrThreadStaticField>((int)fieldInfo.ThreadStaticFields);

                    // TODO:  Renable when thread statics are fixed.
                    //m_threadStatics.Add(new RealTimeMemThreadStaticField(m_heap, field, name));
                }
                else if (field.bIsStatic)
                {
                    if (_statics == null)
                        _statics = new List<ClrStaticField>();

                    // TODO:  Enable default values.
                    /*
                    object defaultValue = null;


                    FieldAttributes sdl = FieldAttributes.Static | FieldAttributes.HasDefault | FieldAttributes.Literal;
                    if ((attr & sdl) == sdl)
                        Debugger.Break();
                    */
                    _statics.Add(new DesktopStaticField(DesktopHeap, field, this, name, attr, null, fieldSig, sigLen));
                }
                else // instance variable
                {
                    _fields.Add(new DesktopInstanceField(DesktopHeap, field, name, attr, fieldSig, sigLen));
                }

                i++;
                nextField = field.nextField;
            }

            _fields.Sort((a, b) => a.Offset.CompareTo(b.Offset));
        }
        
        internal override ClrMethod GetMethod(uint token)
        {
            return Methods.Where(m => m.MetadataToken == token).FirstOrDefault();
        }

        public override IList<ClrMethod> Methods
        {
            get
            {
                if (_methods != null)
                    return _methods;

                ICorDebug.IMetadataImport metadata = null;
                if (DesktopModule != null)
                    metadata = DesktopModule.GetMetadataImport();

                DesktopRuntimeBase runtime = DesktopHeap.DesktopRuntime;
                IList<ulong> mdList = runtime.GetMethodDescList(_constructedMT);

                if (mdList != null)
                {
                    _methods = new List<ClrMethod>(mdList.Count);
                    foreach (ulong md in mdList)
                    {
                        if (md == 0)
                            continue;

                        IMethodDescData mdData = runtime.GetMethodDescData(md);
                        DesktopMethod method = DesktopMethod.Create(runtime, metadata, mdData);
                        if (method != null)
                            _methods.Add(method);
                    }
                }
                else
                {
                    _methods = new ClrMethod[0];
                }

                return _methods;
            }
        }



        public override ClrStaticField GetStaticFieldByName(string name)
        {
            foreach (var field in StaticFields)
                if (field.Name == name)
                    return field;

            return null;
        }

        private IList<ClrMethod> _methods;

        public override ClrInstanceField GetFieldByName(string name)
        {
            if (_fields == null)
                InitFields();

            if (_fields.Count == 0)
                return null;

            if (_fieldNameMap == null)
            {
                _fieldNameMap = new int[_fields.Count];
                for (int j = 0; j < _fieldNameMap.Length; ++j)
                    _fieldNameMap[j] = j;

                Array.Sort(_fieldNameMap, (x, y) => { return _fields[x].Name.CompareTo(_fields[y].Name); });
            }

            int min = 0, max = _fieldNameMap.Length - 1;

            while (max >= min)
            {
                int mid = (max + min) / 2;

                ClrInstanceField field = _fields[_fieldNameMap[mid]];
                int comp = field.Name.CompareTo(name);
                if (comp < 0)
                    min = mid + 1;
                else if (comp > 0)
                    max = mid - 1;
                else
                    return _fields[_fieldNameMap[mid]];
            }

            return null;
        }

        public override ClrType BaseType
        {
            get
            {
                if (_parent == 0)
                    return null;

                return DesktopHeap.GetTypeByMethodTable(_parent, 0, 0);
            }
        }

        public override int GetArrayLength(Address objRef)
        {
            Debug.Assert(IsArray);

            uint res;
            if (!DesktopHeap.DesktopRuntime.ReadDword(objRef + (uint)DesktopHeap.DesktopRuntime.PointerSize, out res))
                res = 0;

            return (int)res;
        }

        public override Address GetArrayElementAddress(Address objRef, int index)
        {
            if (_baseArrayOffset == 0)
            {
                var componentType = ComponentType;

                IObjectData data = DesktopHeap.DesktopRuntime.GetObjectData(objRef);
                if (data != null)
                {
                    _baseArrayOffset = (int)(data.DataPointer - objRef);
                    Debug.Assert(_baseArrayOffset >= 0);
                }
                else if (componentType != null)
                {
                    if (!componentType.IsObjectReference || !Heap.Runtime.HasArrayComponentMethodTables)
                        _baseArrayOffset = IntPtr.Size * 2;
                    else
                        _baseArrayOffset = IntPtr.Size * 3;
                }
                else
                {
                    return 0;
                }
            }

            return objRef + (Address)(_baseArrayOffset + index * _componentSize);
        }

        public override object GetArrayElementValue(Address objRef, int index)
        {
            ulong addr = GetArrayElementAddress(objRef, index);
            if (addr == 0)
                return null;

            ClrElementType cet = ClrElementType.Unknown;
            var componentType = this.ComponentType;
            if (componentType != null)
            {
                cet = componentType.ElementType;
            }
            else
            {
                // Slow path, we need to get the element type of the array.
                IObjectData data = DesktopHeap.DesktopRuntime.GetObjectData(objRef);
                if (data == null)
                    return null;

                cet = data.ElementType;
            }

            if (cet == ClrElementType.Unknown)
                return null;

            if (cet == ClrElementType.String && !DesktopHeap.MemoryReader.ReadPtr(addr, out addr))
                return null;

            return DesktopHeap.GetValueAtAddress(cet, addr);
        }

        public override int BaseSize
        {
            get { return (int)_baseSize; }
        }

        #region private

        /// <summary>
        /// A messy version with better performance that doesn't use regular expression.
        /// </summary>
        internal static int FixGenericsWorker(string name, int start, int end, StringBuilder sb)
        {
            int parenCount = 0;
            while (start < end)
            {
                char c = name[start];
                if (c == '`')
                    break;

                if (c == '[')
                    parenCount++;

                if (c == ']')
                    parenCount--;

                if (parenCount < 0)
                    return start + 1;

                if (c == ',' && parenCount == 0)
                    return start;

                sb.Append(c);
                start++;
            }

            if (start >= end)
                return start;

            start++;

            bool hasSubtypeAirity = false;
            int paramCount = 0;
            do
            {
                int currParamCount = 0;
                hasSubtypeAirity = false;
                // Skip airity.
                while (start < end)
                {
                    char c = name[start];
                    if (c < '0' || c > '9')
                        break;

                    currParamCount = (currParamCount * 10) + c - '0';
                    start++;
                }

                paramCount += currParamCount;
                if (start >= end)
                    return start;

                if (name[start] == '+')
                {
                    while (start < end && name[start] != '[')
                    {
                        if (name[start] == '`')
                        {
                            start++;
                            hasSubtypeAirity = true;
                            break;
                        }

                        sb.Append(name[start]);
                        start++;
                    }

                    if (start >= end)
                        return start;
                }
            } while (hasSubtypeAirity);

            if (name[start] == '[')
            {
                sb.Append('<');
                start++;
                while (paramCount-- > 0)
                {
                    if (start >= end)
                        return start;

                    bool withModule = false;
                    if (name[start] == '[')
                    {
                        withModule = true;
                        start++;
                    }

                    start = FixGenericsWorker(name, start, end, sb);

                    if (start < end && name[start] == '[')
                    {
                        start++;
                        if (start >= end)
                            return start;

                        sb.Append('[');

                        while (start < end && name[start] == ',')
                        {
                            sb.Append(',');
                            start++;
                        }

                        if (start >= end)
                            return start;

                        if (name[start] == ']')
                        {
                            sb.Append(']');
                            start++;
                        }
                    }

                    if (withModule)
                    {
                        while (start < end && name[start] != ']')
                            start++;
                        start++;
                    }

                    if (paramCount > 0)
                    {
                        if (start >= end)
                            return start;

                        //Debug.Assert(name[start] == ',');
                        sb.Append(',');
                        start++;

                        if (start >= end)
                            return start;

                        if (name[start] == ' ')
                            start++;
                    }
                }

                sb.Append('>');
                start++;
            }

            if (start + 1 >= end)
                return start;

            if (name[start] == '[' && name[start + 1] == ']')
                sb.Append("[]");

            return start;
        }

        internal static string FixGenerics(string name)
        {
            StringBuilder builder = new StringBuilder();
            FixGenericsWorker(name, 0, name.Length, builder);
            return builder.ToString();
        }

        internal DesktopHeapType(string typeName, DesktopModule module, uint token, ulong mt, IMethodTableData mtData, DesktopGCHeap heap)
            : base(heap, module, token)
        {
            _name = typeName;

            _constructedMT = mt;
            Shared = mtData.Shared;
            _parent = mtData.Parent;
            _baseSize = mtData.BaseSize;
            _componentSize = mtData.ComponentSize;
            _containsPointers = mtData.ContainsPointers;
            _hasMethods = mtData.NumMethods > 0;
        }
        
        internal void SetIndex(int index)
        {
            _index = index;
        }

        private void InitFlags()
        {
            if ((int)_attributes != 0 || DesktopModule == null)
                return;

            ICorDebug.IMetadataImport import = DesktopModule.GetMetadataImport();
            if (import == null)
            {
                _attributes = (TypeAttributes)0x70000000;
                return;
            }

            int tdef;
            int extends;
            int i = import.GetTypeDefProps((int)_token, null, 0, out tdef, out _attributes, out extends);
            if (i < 0 || (int)_attributes == 0)
                _attributes = (TypeAttributes)0x70000000;
        }


        override public bool IsInternal
        {
            get
            {
                if ((int)_attributes == 0)
                    InitFlags();

                TypeAttributes visibility = (_attributes & TypeAttributes.VisibilityMask);
                return visibility == TypeAttributes.NestedAssembly || visibility == TypeAttributes.NotPublic;
            }
        }

        override public bool IsPublic
        {
            get
            {
                if ((int)_attributes == 0)
                    InitFlags();

                TypeAttributes visibility = (_attributes & TypeAttributes.VisibilityMask);
                return visibility == TypeAttributes.Public || visibility == TypeAttributes.NestedPublic;
            }
        }

        override public bool IsPrivate
        {
            get
            {
                if ((int)_attributes == 0)
                    InitFlags();

                TypeAttributes visibility = (_attributes & TypeAttributes.VisibilityMask);
                return visibility == TypeAttributes.NestedPrivate;
            }
        }

        override public bool IsProtected
        {
            get
            {
                if ((int)_attributes == 0)
                    InitFlags();

                TypeAttributes visibility = (_attributes & TypeAttributes.VisibilityMask);
                return visibility == TypeAttributes.NestedFamily;
            }
        }

        override public bool IsAbstract
        {
            get
            {
                if ((int)_attributes == 0)
                    InitFlags();

                return (_attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract;
            }
        }

        override public bool IsSealed
        {
            get
            {
                if ((int)_attributes == 0)
                    InitFlags();

                return (_attributes & TypeAttributes.Sealed) == TypeAttributes.Sealed;
            }
        }

        override public bool IsInterface
        {
            get
            {
                if ((int)_attributes == 0)
                    InitFlags();
                return (_attributes & TypeAttributes.Interface) == TypeAttributes.Interface;
            }
        }


        internal override Address GetModuleAddress(ClrAppDomain appDomain)
        {
            if (DesktopModule == null)
                return 0;
            return DesktopModule.GetDomainModule(appDomain);
        }

        public override bool IsRuntimeType
        {
            get
            {
                if (_runtimeType == null)
                    _runtimeType = Name == "System.RuntimeType";

                return (bool)_runtimeType;
            }
        }

        public override ClrType GetRuntimeType(ulong obj)
        {
            if (!IsRuntimeType)
                return null;

            ClrInstanceField field = GetFieldByName("m_handle");
            if (field == null)
                return null;

            ulong methodTable = 0;
            if (field.ElementType == ClrElementType.NativeInt)
            {
                methodTable = (ulong)(long)field.GetValue(obj);
            }
            else if (field.ElementType == ClrElementType.Struct)
            {
                ClrInstanceField ptrField = field.Type.GetFieldByName("m_ptr");
                methodTable = (ulong)(long)ptrField.GetValue(field.GetAddress(obj, false), true);
            }

            return DesktopHeap.GetTypeByMethodTable(methodTable, 0, obj);
        }

        internal void InitMethodHandles()
        {
            var runtime = DesktopHeap.DesktopRuntime;
            foreach (ulong methodTable in EnumerateMethodTables())
            {
                foreach (ulong methodDesc in runtime.GetMethodDescList(methodTable))
                {
                    IMethodDescData data = runtime.GetMethodDescData(methodDesc);
                    DesktopMethod method = (DesktopMethod)GetMethod(data.MDToken);
                    if (method.Type != this)
                        continue;
                    method.AddMethodHandle(methodDesc);
                }
            }
        }

        private string _name;
        private int _index;

        private TypeAttributes _attributes;
        private GCDesc _gcDesc;
        private ulong _constructedMT;
        private ulong _parent;
        private uint _baseSize;
        private uint _componentSize;
        private bool _containsPointers;
        private byte _finalizable;

        private List<ClrInstanceField> _fields;
        private List<ClrStaticField> _statics;
        private List<ClrThreadStaticField> _threadStatics;
        private int[] _fieldNameMap;

        private int _baseArrayOffset;
        private bool _hasMethods;
        private bool? _runtimeType;
        private EnumData _enumData;
        private bool _notRCW;
        private bool _checkedIfIsRCW;
        private bool _checkedIfIsCCW;
        private bool _notCCW;

        private static ClrStaticField[] s_emptyStatics = new ClrStaticField[0];
        private static ClrThreadStaticField[] s_emptyThreadStatics = new ClrThreadStaticField[0];
        #endregion
    }
}
