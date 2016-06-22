// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using Address = System.UInt64;
using System.Reflection;
using Microsoft.Diagnostics.Runtime.Utilities;
using System.Collections.Generic;

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal class DesktopStaticField : ClrStaticField
    {
        public DesktopStaticField(DesktopGCHeap heap, IFieldData field, BaseDesktopHeapType containingType, string name, FieldAttributes attributes, object defaultValue, IntPtr sig, int sigLen)
        {
            _field = field;
            _name = name;
            _attributes = attributes;
            _type = (BaseDesktopHeapType)heap.GetTypeByMethodTable(field.TypeMethodTable, 0);
            _defaultValue = defaultValue;
            _heap = heap;

            if (_type != null && ElementType != ClrElementType.Class)
                _type.ElementType = ElementType;

            _containingType = containingType;


            if (_type == null)
            {
                if (sig != IntPtr.Zero && sigLen > 0)
                {
                    SigParser sigParser = new SigParser(sig, sigLen);

                    bool res;
                    int sigType, etype = 0;

                    if (res = sigParser.GetCallingConvInfo(out sigType))
                        Debug.Assert(sigType == SigParser.IMAGE_CEE_CS_CALLCONV_FIELD);

                    res = res && sigParser.SkipCustomModifiers();
                    res = res && sigParser.GetElemType(out etype);

                    if (res)
                    {
                        ClrElementType type = (ClrElementType)etype;

                        if (type == ClrElementType.Array)
                        {
                            res = sigParser.PeekElemType(out etype);
                            res = res && sigParser.SkipExactlyOne();

                            int ranks = 0;
                            res = res && sigParser.GetData(out ranks);

                            if (res)
                                _type = heap.GetArrayType((ClrElementType)etype, ranks, null);
                        }
                        else if (type == ClrElementType.SZArray)
                        {
                            res = sigParser.PeekElemType(out etype);
                            type = (ClrElementType)etype;

                            if (DesktopRuntimeBase.IsObjectReference(type))
                                _type = (BaseDesktopHeapType)heap.GetBasicType(ClrElementType.SZArray);
                            else
                                _type = (BaseDesktopHeapType)heap.GetArrayType(type, -1, null);
                        }
                        else if (type == ClrElementType.Pointer)
                        {
                            // Only deal with single pointers for now and types that have already been constructed
                            res = sigParser.GetElemType(out etype);
                            type = (ClrElementType)etype;

                            int token;
                            sigParser.GetToken(out token);
                            BaseDesktopHeapType innerType = (BaseDesktopHeapType)heap.GetGCHeapTypeFromModuleAndToken(field.Module, Convert.ToUInt32(token));

                            if (innerType == null)
                            {
                                innerType = (BaseDesktopHeapType)heap.GetBasicType(type);
                            }

                            _type = heap.CreatePointerType(innerType, type, null);
                        }
                    }
                }

                if (_type == null)
                    _type = (BaseDesktopHeapType)TryBuildType(_heap);

                if (_type == null)
                    _type = (BaseDesktopHeapType)heap.GetBasicType(ElementType);
            }
        }

        override public bool HasDefaultValue { get { return _defaultValue != null; } }
        override public object GetDefaultValue() { return _defaultValue; }

        override public bool IsPublic
        {
            get
            {
                return (_attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
            }
        }

        override public bool IsPrivate
        {
            get
            {
                return (_attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
            }
        }

        override public bool IsInternal
        {
            get
            {
                return (_attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
            }
        }

        override public bool IsProtected
        {
            get
            {
                return (_attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
            }
        }

        public override ClrElementType ElementType
        {
            get { return (ClrElementType)_field.CorElementType; }
        }

        public override string Name { get { return _name; } }

        public override ClrType Type
        {
            get
            {
                if (_type == null)
                    _type = (BaseDesktopHeapType)TryBuildType(_heap);
                return _type;
            }
        }

        private ClrType TryBuildType(ClrHeap heap)
        {
            var runtime = heap.Runtime;
            var domains = runtime.AppDomains;
            ClrType[] types = new ClrType[domains.Count];

            ClrElementType elType = ElementType;
            if (ClrRuntime.IsPrimitive(elType) || elType == ClrElementType.String)
                return ((DesktopGCHeap)heap).GetBasicType(elType);

            int count = 0;
            foreach (var domain in domains)
            {
                object value = GetValue(domain);
                if (value != null && value is ulong && ((ulong)value != 0))
                {
                    types[count++] = heap.GetObjectType((ulong)value);
                }
            }

            int depth = int.MaxValue;
            ClrType result = null;
            for (int i = 0; i < count; ++i)
            {
                ClrType curr = types[i];
                if (curr == result || curr == null)
                    continue;

                int nextDepth = GetDepth(curr);
                if (nextDepth < depth)
                {
                    result = curr;
                    depth = nextDepth;
                }
            }

            return result;
        }

        private int GetDepth(ClrType curr)
        {
            int depth = 0;
            while (curr != null)
            {
                curr = curr.BaseType;
                depth++;
            }

            return depth;
        }

        // these are optional.  
        /// <summary>
        /// If the field has a well defined offset from the base of the object, return it (otherwise -1). 
        /// </summary>
        public override int Offset { get { return (int)_field.Offset; } }

        /// <summary>
        /// Given an object reference, fetch the address of the field. 
        /// </summary>

        public override bool HasSimpleValue
        {
            get { return _containingType != null; }
        }
        public override int Size
        {
            get
            {
                if (_type == null)
                    _type = (BaseDesktopHeapType)TryBuildType(_heap);
                return DesktopInstanceField.GetSize(_type, ElementType);
            }
        }

        public override object GetValue(ClrAppDomain appDomain, bool convertStrings = true)
        {
            if (!HasSimpleValue)
                return null;

            Address addr = GetAddress(appDomain);

            if (ElementType == ClrElementType.String)
            {
                object val = _containingType.DesktopHeap.GetValueAtAddress(ClrElementType.Object, addr);

                Debug.Assert(val == null || val is ulong);
                if (val == null || !(val is ulong))
                    return convertStrings ? null : (object)(ulong)0;

                addr = (ulong)val;
                if (!convertStrings)
                    return addr;
            }

            // Structs are stored as objects.
            var elementType = ElementType;
            if (elementType == ClrElementType.Struct)
                elementType = ClrElementType.Object;

            if (elementType == ClrElementType.Object && addr == 0)
                return (ulong)0;

            return _containingType.DesktopHeap.GetValueAtAddress(elementType, addr);
        }

        public override Address GetAddress(ClrAppDomain appDomain)
        {
            if (_containingType == null)
                return 0;

            bool shared = _containingType.Shared;

            IDomainLocalModuleData data = null;
            if (shared)
            {
                Address id = _containingType.DesktopModule.ModuleId;
                data = _containingType.DesktopHeap.DesktopRuntime.GetDomainLocalModule(appDomain.Address, id);
                if (!IsInitialized(data))
                    return 0;
            }
            else
            {
                Address modAddr = _containingType.GetModuleAddress(appDomain);
                if (modAddr != 0)
                    data = _containingType.DesktopHeap.DesktopRuntime.GetDomainLocalModule(modAddr);
            }

            if (data == null)
                return 0;

            Address addr;
            if (DesktopRuntimeBase.IsPrimitive(ElementType))
                addr = data.NonGCStaticDataStart + _field.Offset;
            else
                addr = data.GCStaticDataStart + _field.Offset;

            return addr;
        }

        public override bool IsInitialized(ClrAppDomain appDomain)
        {
            if (_containingType == null)
                return false;

            if (!_containingType.Shared)
                return true;

            Address id = _containingType.DesktopModule.ModuleId;
            IDomainLocalModuleData data = _containingType.DesktopHeap.DesktopRuntime.GetDomainLocalModule(appDomain.Address, id);
            if (data == null)
                return false;

            return IsInitialized(data);
        }

        private bool IsInitialized(IDomainLocalModuleData data)
        {
            if (data == null || _containingType == null)
                return false;

            byte flags = 0;
            ulong flagsAddr = data.ClassData + (_containingType.MetadataToken & ~0x02000000u) - 1;
            if (!_heap.DesktopRuntime.ReadByte(flagsAddr, out flags))
                return false;

            return (flags & 1) != 0;
        }

        private IFieldData _field;
        private string _name;
        private BaseDesktopHeapType _type, _containingType;
        private FieldAttributes _attributes;
        private object _defaultValue;
        private DesktopGCHeap _heap;
    }

    internal class DesktopThreadStaticField : ClrThreadStaticField
    {
        public DesktopThreadStaticField(DesktopGCHeap heap, IFieldData field, string name)
        {
            _field = field;
            _name = name;
            _type = (BaseDesktopHeapType)heap.GetTypeByMethodTable(field.TypeMethodTable, 0);
        }

        public override object GetValue(ClrAppDomain appDomain, ClrThread thread, bool convertStrings = true)
        {
            if (!HasSimpleValue)
                return null;

            Address addr = GetAddress(appDomain, thread);
            if (addr == 0)
                return null;

            if (ElementType == ClrElementType.String)
            {
                object val = _type.DesktopHeap.GetValueAtAddress(ClrElementType.Object, addr);

                Debug.Assert(val == null || val is ulong);
                if (val == null || !(val is ulong))
                    return convertStrings ? null : (object)(ulong)0;

                addr = (ulong)val;
                if (!convertStrings)
                    return addr;
            }

            return _type.DesktopHeap.GetValueAtAddress(ElementType, addr);
        }

        public override Address GetAddress(ClrAppDomain appDomain, ClrThread thread)
        {
            if (_type == null)
                return 0;

            DesktopRuntimeBase runtime = _type.DesktopHeap.DesktopRuntime;
            IModuleData moduleData = runtime.GetModuleData(_field.Module);

            return runtime.GetThreadStaticPointer(thread.Address, (ClrElementType)_field.CorElementType, (uint)Offset, (uint)moduleData.ModuleId, _type.Shared);
        }


        public override ClrElementType ElementType
        {
            get { return (ClrElementType)_field.CorElementType; }
        }

        public override string Name { get { return _name; } }

        public override ClrType Type { get { return _type; } }

        // these are optional.  
        /// <summary>
        /// If the field has a well defined offset from the base of the object, return it (otherwise -1). 
        /// </summary>
        public override int Offset { get { return (int)_field.Offset; } }

        /// <summary>
        /// Given an object reference, fetch the address of the field. 
        /// </summary>

        public override bool HasSimpleValue
        {
            get { return _type != null && !DesktopRuntimeBase.IsValueClass(ElementType); }
        }
        public override int Size
        {
            get
            {
                return DesktopInstanceField.GetSize(_type, ElementType);
            }
        }

        public override bool IsPublic
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsPrivate
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsInternal
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsProtected
        {
            get { throw new NotImplementedException(); }
        }

        private IFieldData _field;
        private string _name;
        private BaseDesktopHeapType _type;
    }

    internal class DesktopInstanceField : ClrInstanceField
    {
        override public bool IsPublic
        {
            get
            {
                return (_attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
            }
        }

        override public bool IsPrivate
        {
            get
            {
                return (_attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
            }
        }

        override public bool IsInternal
        {
            get
            {
                return (_attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
            }
        }

        override public bool IsProtected
        {
            get
            {
                return (_attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
            }
        }

        public DesktopInstanceField(DesktopGCHeap heap, IFieldData data, string name, FieldAttributes attributes, IntPtr sig, int sigLen)
        {
            _name = name;
            _field = data;
            _attributes = attributes;

            ulong mt = data.TypeMethodTable;
            if (mt != 0)
                _type = (BaseDesktopHeapType)heap.GetTypeByMethodTable(mt, 0);

            if (_type == null)
            {
                if (sig != IntPtr.Zero && sigLen > 0)
                {
                    SigParser sigParser = new SigParser(sig, sigLen);

                    bool res;
                    int sigType, etype = 0;

                    if (res = sigParser.GetCallingConvInfo(out sigType))
                        Debug.Assert(sigType == SigParser.IMAGE_CEE_CS_CALLCONV_FIELD);

                    res = res && sigParser.SkipCustomModifiers();
                    res = res && sigParser.GetElemType(out etype);


                    // Generic instantiation
                    if (etype == 0x15)
                        res = res && sigParser.GetElemType(out etype);

                    if (res)
                    {
                        ClrElementType type = (ClrElementType)etype;

                        if (type == ClrElementType.Array)
                        {
                            res = sigParser.PeekElemType(out etype);
                            res = res && sigParser.SkipExactlyOne();

                            int ranks = 0;
                            res = res && sigParser.GetData(out ranks);

                            if (res)
                                _type = heap.GetArrayType((ClrElementType)etype, ranks, null);
                        }
                        else if (type == ClrElementType.SZArray)
                        {
                            res = sigParser.PeekElemType(out etype);
                            type = (ClrElementType)etype;

                            if (DesktopRuntimeBase.IsObjectReference(type))
                                _type = (BaseDesktopHeapType)heap.GetBasicType(ClrElementType.SZArray);
                            else
                                _type = (BaseDesktopHeapType)heap.GetArrayType(type, -1, null);
                        }
                        else if (type == ClrElementType.Pointer)
                        {
                            // Only deal with single pointers for now and types that have already been constructed
                            res = sigParser.GetElemType(out etype);
                            type = (ClrElementType)etype;

                            int token;
                            sigParser.GetToken(out token);
                            BaseDesktopHeapType innerType = (BaseDesktopHeapType)heap.GetGCHeapTypeFromModuleAndToken(data.Module, Convert.ToUInt32(token));

                            if (innerType == null)
                            {
                                innerType = (BaseDesktopHeapType)heap.GetBasicType(type);
                            }

                            _type = heap.CreatePointerType(innerType, type, null);
                        }
                        else
                        {
                            // If it's a class or struct, then try to get the token
                            int token = 0;
                            if (etype == 0x11 || etype == 0x12)
                                res = res && sigParser.GetToken(out token);

                            if (token != 0)
                                _type = (BaseDesktopHeapType)heap.GetGCHeapTypeFromModuleAndToken(data.Module, (uint)token);

                            if (_type == null)
                                if ((_type = (BaseDesktopHeapType)heap.GetBasicType((ClrElementType)etype)) == null)
                                    _type = heap.ErrorType;
                        }
                    }
                }

                if (_type == null)
                    _type = (BaseDesktopHeapType)heap.GetBasicType(ElementType);
            }
            else if (ElementType != ClrElementType.Class)
            {
                _type.ElementType = ElementType;
            }

            if (_type.IsArray && _type.ComponentType == null)
            {
                if (sig != IntPtr.Zero && sigLen > 0)
                {
                    SigParser sigParser = new SigParser(sig, sigLen);

                    bool res;
                    int sigType, etype = 0;

                    if (res = sigParser.GetCallingConvInfo(out sigType))
                        Debug.Assert(sigType == SigParser.IMAGE_CEE_CS_CALLCONV_FIELD);

                    res = res && sigParser.SkipCustomModifiers();
                    res = res && sigParser.GetElemType(out etype);
                    
                    res = res && sigParser.GetElemType(out etype);

                    // Generic instantiation
                    if (etype == 0x15)
                        res = res && sigParser.GetElemType(out etype);

                    // If it's a class or struct, then try to get the token
                    int token = 0;
                    if (etype == 0x11 || etype == 0x12)
                        res = res && sigParser.GetToken(out token);
                    
                    if (token != 0)
                        _type.ComponentType = heap.GetGCHeapTypeFromModuleAndToken(data.Module, (uint)token);
                    
                    else if (_type.ComponentType == null)
                        if ((_type.ComponentType = heap.GetBasicType((ClrElementType)etype)) == null)
                            _type.ComponentType = heap.ErrorType;
                }
            }
        }


        public override ClrElementType ElementType
        {
            get
            {
                if (_elementType != ClrElementType.Unknown)
                    return _elementType;

                if (_type == null)
                    _elementType = (ClrElementType)_field.CorElementType;

                else if (_type.IsEnum)
                    _elementType = _type.GetEnumElementType();

                else
                    _elementType = _type.ElementType;

                return _elementType;
            }
        }

        public override string Name { get { return _name; } }

        public override ClrType Type { get { return _type; } }

        // these are optional.  
        /// <summary>
        /// If the field has a well defined offset from the base of the object, return it (otherwise -1). 
        /// </summary>
        public override int Offset { get { return (int)_field.Offset; } }

        /// <summary>
        /// Given an object reference, fetch the address of the field. 
        /// </summary>

        public override bool HasSimpleValue
        {
            get { return _type != null && !DesktopRuntimeBase.IsValueClass(ElementType); }
        }
        public override int Size
        {
            get
            {
                return GetSize(_type, ElementType);
            }
        }


        #region Fields
        private string _name;
        private BaseDesktopHeapType _type;
        private IFieldData _field;
        private FieldAttributes _attributes;
        private ClrElementType _elementType = ClrElementType.Unknown;
        #endregion

        public override object GetValue(Address objRef, bool interior = false, bool convertStrings = true)
        {
            if (!HasSimpleValue)
                return null;

            Address addr = GetAddress(objRef, interior);

            if (ElementType == ClrElementType.String)
            {
                object val = _type.DesktopHeap.GetValueAtAddress(ClrElementType.Object, addr);

                Debug.Assert(val == null || val is ulong);
                if (val == null || !(val is ulong))
                    return convertStrings ? null : (object)(ulong)0;

                addr = (ulong)val;
                if (!convertStrings)
                    return addr;
            }

            return _type.DesktopHeap.GetValueAtAddress(ElementType, addr);
        }

        public override Address GetAddress(Address objRef, bool interior = false)
        {
            if (interior)
                return objRef + (Address)Offset;

            // TODO:  Type really shouldn't be null here, but due to the dac it can be.  We still need
            //        to respect m_heap.PointerSize, so there needs to be a way to track this when m_type is null.
            if (_type == null)
                return objRef + (Address)(Offset + IntPtr.Size);

            return objRef + (Address)(Offset + _type.DesktopHeap.PointerSize);
        }


        internal static int GetSize(BaseDesktopHeapType type, ClrElementType cet)
        {
            // todo:  What if we have a struct which is not fully constructed (null MT,
            //        null type) and need to get the size of the field?
            switch (cet)
            {
                case ClrElementType.Struct:
                    if (type == null)
                        return 1;
                    return type.BaseSize;

                case ClrElementType.Int8:
                case ClrElementType.UInt8:
                case ClrElementType.Boolean:
                    return 1;

                case ClrElementType.Float:
                case ClrElementType.Int32:
                case ClrElementType.UInt32:
                    return 4;

                case ClrElementType.Double: // double
                case ClrElementType.Int64:
                case ClrElementType.UInt64:
                    return 8;

                case ClrElementType.String:
                case ClrElementType.Class:
                case ClrElementType.Array:
                case ClrElementType.SZArray:
                case ClrElementType.Object:
                case ClrElementType.NativeInt:  // native int
                case ClrElementType.NativeUInt:  // native unsigned int
                case ClrElementType.Pointer:
                case ClrElementType.FunctionPointer:
                    if (type == null)
                        return IntPtr.Size;  // todo: fixme
                    return (int)type.DesktopHeap.PointerSize;


                case ClrElementType.UInt16:
                case ClrElementType.Int16:
                case ClrElementType.Char:  // u2
                    return 2;
            }

            throw new Exception("Unexpected element type.");
        }
    }

    class ErrorType : BaseDesktopHeapType
    {
        public ErrorType(DesktopGCHeap heap)
            : base(heap, heap.DesktopRuntime.ErrorModule, 0)
        {
        }

        public override int BaseSize
        {
            get
            {
                return 0;
            }
        }

        public override ClrType BaseType
        {
            get
            {
                return DesktopHeap.ObjectType;
            }
        }

        public override int ElementSize
        {
            get
            {
                return 0;
            }
        }

        public override ClrHeap Heap
        {
            get
            {
                return DesktopHeap;
            }
        }

        public override IList<ClrInterface> Interfaces
        {
            get
            {
                return new ClrInterface[0];
            }
        }

        public override bool IsAbstract
        {
            get
            {
                return false;
            }
        }

        public override bool IsFinalizable
        {
            get
            {
                return false;
            }
        }

        public override bool IsInterface
        {
            get
            {
                return false;
            }
        }

        public override bool IsInternal
        {
            get
            {
                return false;
            }
        }

        public override bool IsPrivate
        {
            get
            {
                return false;
            }
        }

        public override bool IsProtected
        {
            get
            {
                return false;
            }
        }

        public override bool IsPublic
        {
            get
            {
                return false;
            }
        }

        public override bool IsSealed
        {
            get
            {
                return false;
            }
        }

        public override uint MetadataToken
        {
            get
            {
                return 0;
            }
        }

        public override ulong MethodTable
        {
            get
            {
                return 0;
            }
        }

        public override string Name
        {
            get
            {
                return "ERROR";
            }
        }

        public override IEnumerable<ulong> EnumerateMethodTables()
        {
            return new ulong[0];
        }

        public override void EnumerateRefsOfObject(ulong objRef, Action<ulong, int> action)
        {
        }

        public override void EnumerateRefsOfObjectCarefully(ulong objRef, Action<ulong, int> action)
        {
        }

        public override ulong GetArrayElementAddress(ulong objRef, int index)
        {
            throw new InvalidOperationException();
        }

        public override object GetArrayElementValue(ulong objRef, int index)
        {
            throw new InvalidOperationException();
        }

        public override int GetArrayLength(ulong objRef)
        {
            throw new InvalidOperationException();
        }

        public override ClrInstanceField GetFieldByName(string name)
        {
            return null;
        }

        public override bool GetFieldForOffset(int fieldOffset, bool inner, out ClrInstanceField childField, out int childFieldOffset)
        {
            childField = null;
            childFieldOffset = 0;
            return false;
        }

        public override ulong GetSize(ulong objRef)
        {
            return 0;
        }

        public override ClrStaticField GetStaticFieldByName(string name)
        {
            return null;
        }

        internal override ulong GetModuleAddress(ClrAppDomain domain)
        {
            return 0;
        }
    }
}
