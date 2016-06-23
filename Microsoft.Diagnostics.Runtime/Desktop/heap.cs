// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal abstract class DesktopGCHeap : HeapBase
    {
        public DesktopGCHeap(DesktopRuntimeBase runtime)
            : base(runtime)
        {
            DesktopRuntime = runtime;
            _types = new List<ClrType>(1000);
            Revision = runtime.Revision;

            // Prepopulate a few important method tables.
            // UPDATE: Threw everything in try/catch blocks since microsoft was boning us in this method
            try
            {
                FreeType = GetTypeByMethodTable(DesktopRuntime.FreeMethodTable, 0, 0);
                ((DesktopHeapType)FreeType).Shared = true;
            }
            catch { }
            try
            {
                ObjectType = GetTypeByMethodTable(DesktopRuntime.ObjectMethodTable, 0, 0);
            }
            catch { }
            try
            {
                ArrayType = GetTypeByMethodTable(DesktopRuntime.ArrayMethodTable, DesktopRuntime.ObjectMethodTable, 0);
                ArrayType.ComponentType = ObjectType;
            }
            catch { }
            try
            {
                ((BaseDesktopHeapType)FreeType).DesktopModule = (DesktopModule)ObjectType.Module;
            }
            catch { }

            try
            {
                StringType = GetTypeByMethodTable(DesktopRuntime.StringMethodTable, 0, 0);
            }
            catch { }
            try
            {
                ExceptionType = GetTypeByMethodTable(DesktopRuntime.ExceptionMethodTable, 0, 0);
            }
            catch { }
            try
            {
                ErrorType = new ErrorType(this);
            }
            catch { }
            try
            {
                InitSegments(runtime);
            }
            catch { }
        }

        protected override int GetRuntimeRevision()
        {
            return DesktopRuntime.Revision;
        }

        public override ClrRuntime Runtime
        {
            get
            {
                return DesktopRuntime;
            }
        }

        public override ClrException GetExceptionObject(Address objRef)
        {
            ClrType type = GetObjectType(objRef);
            if (type == null)
                return null;

            // It's possible for the exception handle to go stale on a dead thread.  In this
            // case we will simply return null if we have a valid object at the address, but
            // that object isn't actually an exception.
            if (!type.IsException)
                return null;

            return new DesktopException(objRef, (BaseDesktopHeapType)type);
        }


        public override ulong GetEEClassByMethodTable(ulong methodTable)
        {
            if (methodTable == 0)
                return 0;

            IMethodTableData mtData = DesktopRuntime.GetMethodTableData(methodTable);
            if (mtData == null)
                return 0;

            return mtData.EEClass;
        }

        public override ulong GetMethodTableByEEClass(ulong eeclass)
        {
            if (eeclass == 0)
                return 0;

            return DesktopRuntime.GetMethodTableByEEClass(eeclass);
        }

        public override bool TryGetMethodTable(ulong obj, out ulong methodTable, out ulong componentMethodTable)
        {
            componentMethodTable = 0;
            if (!ReadPointer(obj, out methodTable))
                return false;

            if (methodTable == DesktopRuntime.ArrayMethodTable)
                if (!ReadPointer(obj + (ulong)(IntPtr.Size * 2), out componentMethodTable))
                    return false;

            return true;
        }


        internal ClrType GetGCHeapTypeFromModuleAndToken(ulong moduleAddr, uint token)
        {
            DesktopModule module = DesktopRuntime.GetModule(moduleAddr);
            ModuleEntry modEnt = new ModuleEntry(module, token);
            int index;

            if (_typeEntry.TryGetValue(modEnt, out index))
            {
                BaseDesktopHeapType match = (BaseDesktopHeapType)_types[index];
                if (match.MetadataToken == token)
                    return match;
            }
            else
            {
                // TODO: TypeRefTokens do not work with this code
                foreach (ClrType type in module.EnumerateTypes())
                {
                    if (type.MetadataToken == token)
                        return type;
                }
            }

            return null;
        }

        internal abstract ClrType GetTypeByMethodTable(ulong mt, ulong cmt, ulong obj);


        protected ClrType TryGetComponentType(ulong obj, ulong cmt)
        {
            ClrType result = null;
            IObjectData data = GetObjectData(obj);
            if (data != null)
            {
                if (data.ElementTypeHandle != 0)
                    result = GetTypeByMethodTable(data.ElementTypeHandle, 0, 0);

                if (result == null && data.ElementType != ClrElementType.Unknown)
                    result = GetBasicType(data.ElementType);
            }
            else if (cmt != 0)
            {
                result = GetTypeByMethodTable(cmt, 0);
            }

            return result;
        }

        protected static StringBuilder GetTypeNameFromToken(DesktopModule module, uint token)
        {
            if (module == null)
                return null;

            ICorDebug.IMetadataImport meta = module.GetMetadataImport();
            if (meta == null)
                return null;

            // Get type name.
            int ptkExtends;
            int typeDefLen;
            System.Reflection.TypeAttributes typeAttrs;
            StringBuilder typeBuilder = new StringBuilder(256);
            int res = meta.GetTypeDefProps((int)token, typeBuilder, typeBuilder.Capacity, out typeDefLen, out typeAttrs, out ptkExtends);
            if (res < 0)
                return null;

            int enclosing = 0;
            res = meta.GetNestedClassProps((int)token, out enclosing);
            if (res == 0 && token != enclosing)
            {
                StringBuilder inner = GetTypeNameFromToken(module, (uint)enclosing);
                if (inner == null)
                {
                    inner = new StringBuilder(typeBuilder.Capacity + 16);
                    inner.Append("<UNKNOWN>");
                }

                inner.Append('+');
                inner.Append(typeBuilder);
                return inner;
            }

            return typeBuilder;
        }



        public override IEnumerable<ulong> EnumerateFinalizableObjectAddresses()
        {
            SubHeap[] heaps;
            if (DesktopRuntime.GetHeaps(out heaps))
            {
                foreach (SubHeap heap in heaps)
                {
                    foreach (Address obj in DesktopRuntime.GetPointersInRange(heap.FQLiveStart, heap.FQLiveStop))
                    {
                        if (obj == 0)
                            continue;

                        var type = GetObjectType(obj);
                        if (type != null && !type.IsFinalizeSuppressed(obj))
                            yield return obj;
                    }
                }
            }
        }

        private BlockingObject[] _managedLocks;

        public override IEnumerable<BlockingObject> EnumerateBlockingObjects()
        {
            InitLockInspection();
            return _managedLocks;
        }

        internal void InitLockInspection()
        {
            if (_managedLocks != null)
                return;

            LockInspection li = new LockInspection(this, DesktopRuntime);
            _managedLocks = li.InitLockInspection();
        }

        public override IEnumerable<ClrRoot> EnumerateRoots()
        {
            return EnumerateRoots(true);
        }

        public override IEnumerable<ClrRoot> EnumerateRoots(bool enumerateStatics)
        {
            if (enumerateStatics)
            {
                // Statics
                foreach (var type in EnumerateTypes())
                {
                    // Statics
                    foreach (var staticField in type.StaticFields)
                    {
                        if (!ClrRuntime.IsPrimitive(staticField.ElementType))
                        {
                            foreach (var ad in DesktopRuntime.AppDomains)
                            {
                                ulong addr = 0;
                                ulong value = 0;
                                // We must manually get the value, as strings will not be returned as an object address.
                                try // If this fails for whatever reasion, don't fail completely.  
                                {
                                    addr = staticField.GetAddress(ad);
                                }
                                catch (Exception e)
                                {
                                    Trace.WriteLine(string.Format("Error getting stack field {0}.{1}: {2}", type.Name, staticField.Name, e.Message));
                                    goto NextStatic;
                                }

                                if (DesktopRuntime.ReadPointer(addr, out value) && value != 0)
                                {
                                    ClrType objType = GetObjectType(value);
                                    if (objType != null)
                                        yield return new StaticVarRoot(addr, value, objType, type.Name, staticField.Name, ad);
                                }
                            }
                        }
                    NextStatic:;
                    }

                    // Thread statics
                    foreach (var tsf in type.ThreadStaticFields)
                    {
                        if (ClrRuntime.IsObjectReference(tsf.ElementType))
                        {
                            foreach (var ad in DesktopRuntime.AppDomains)
                            {
                                foreach (var thread in DesktopRuntime.Threads)
                                {
                                    // We must manually get the value, as strings will not be returned as an object address.
                                    ulong addr = tsf.GetAddress(ad, thread);
                                    ulong value = 0;

                                    if (DesktopRuntime.ReadPointer(addr, out value) && value != 0)
                                    {
                                        ClrType objType = GetObjectType(value);
                                        if (objType != null)
                                            yield return new ThreadStaticVarRoot(addr, value, objType, type.Name, tsf.Name, ad);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Handles
            var handles = DesktopRuntime.EnumerateHandles();
            if (handles != null)
            {
                foreach (ClrHandle handle in handles)
                {
                    Address objAddr = handle.Object;
                    GCRootKind kind = GCRootKind.Strong;
                    if (objAddr != 0)
                    {
                        ClrType type = GetObjectType(objAddr);
                        if (type != null)
                        {
                            switch (handle.HandleType)
                            {
                                case HandleType.WeakShort:
                                case HandleType.WeakLong:
                                    break;
                                case HandleType.RefCount:
                                    if (handle.RefCount <= 0)
                                        break;
                                    goto case HandleType.Strong;
                                case HandleType.Dependent:
                                    if (objAddr == 0)
                                        continue;
                                    objAddr = handle.DependentTarget;
                                    goto case HandleType.Strong;
                                case HandleType.Pinned:
                                    kind = GCRootKind.Pinning;
                                    goto case HandleType.Strong;
                                case HandleType.AsyncPinned:
                                    kind = GCRootKind.AsyncPinning;
                                    goto case HandleType.Strong;
                                case HandleType.Strong:
                                case HandleType.SizedRef:
                                    yield return new HandleRoot(handle.Address, objAddr, type, handle.HandleType, kind, handle.AppDomain);

                                    // Async pinned handles keep 1 or more "sub objects" alive.  I will report them here as their own pinned handle.
                                    if (handle.HandleType == HandleType.AsyncPinned)
                                    {
                                        ClrInstanceField userObjectField = type.GetFieldByName("m_userObject");
                                        if (userObjectField != null)
                                        {
                                            ulong _userObjAddr = userObjectField.GetAddress(objAddr);
                                            ulong _userObj = (ulong)userObjectField.GetValue(objAddr);
                                            var _userObjType = GetObjectType(_userObj);
                                            if (_userObjType != null)
                                            {
                                                if (_userObjType.IsArray)
                                                {
                                                    if (_userObjType.ComponentType != null)
                                                    {
                                                        if (_userObjType.ComponentType.ElementType == ClrElementType.Object)
                                                        {
                                                            // report elements
                                                            int len = _userObjType.GetArrayLength(_userObj);
                                                            for (int i = 0; i < len; ++i)
                                                            {
                                                                ulong indexAddr = _userObjType.GetArrayElementAddress(_userObj, i);
                                                                ulong indexObj = (ulong)_userObjType.GetArrayElementValue(_userObj, i);
                                                                ClrType indexObjType = GetObjectType(indexObj);

                                                                if (indexObj != 0 && indexObjType != null)
                                                                    yield return new HandleRoot(indexAddr, indexObj, indexObjType, HandleType.AsyncPinned, GCRootKind.AsyncPinning, handle.AppDomain);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            yield return new HandleRoot(_userObjAddr, _userObj, _userObjType, HandleType.AsyncPinned, GCRootKind.AsyncPinning, handle.AppDomain);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    yield return new HandleRoot(_userObjAddr, _userObj, _userObjType, HandleType.AsyncPinned, GCRootKind.AsyncPinning, handle.AppDomain);
                                                }
                                            }
                                        }
                                    }

                                    break;
                                default:
                                    Debug.WriteLine("Warning, unknown handle type {0} ignored", Enum.GetName(typeof(HandleType), handle.HandleType));
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                Trace.WriteLine("Warning, GetHandles() return null!");
            }

            // Finalization Queue
            foreach (Address objAddr in DesktopRuntime.EnumerateFinalizerQueueObjectAddresses())
                if (objAddr != 0)
                {
                    ClrType type = GetObjectType(objAddr);
                    if (type != null)
                        yield return new FinalizerRoot(objAddr, type);
                }

            // Threads
            foreach (ClrThread thread in DesktopRuntime.Threads)
                if (thread.IsAlive)
                    foreach (var root in thread.EnumerateStackObjects(false))
                        yield return root;
        }

        internal string GetStringContents(Address strAddr)
        {
            if (strAddr == 0)
                return null;

            if (!_initializedStringFields)
            {
                _firstChar = StringType.GetFieldByName("m_firstChar");
                _stringLength = StringType.GetFieldByName("m_stringLength");

                // .Type being null can happen in minidumps.  In that case we will fall back to
                // hardcoded values and hope they don't get out of date.
                if (_firstChar.Type == ErrorType)
                    _firstChar = null;

                if (_stringLength.Type == ErrorType)
                    _stringLength = null;

                _initializedStringFields = true;
            }

            int length = 0;
            if (_stringLength != null)
                length = (int)_stringLength.GetValue(strAddr);
            else if (!DesktopRuntime.ReadDword(strAddr + DesktopRuntime.GetStringLengthOffset(), out length))
                return null;


            if (length == 0)
                return "";

            Address data = 0;
            if (_firstChar != null)
                data = _firstChar.GetAddress(strAddr);
            else
                data = strAddr + DesktopRuntime.GetStringFirstCharOffset();

            byte[] buffer = new byte[length * 2];
            int read;
            if (!DesktopRuntime.ReadMemory(data, buffer, buffer.Length, out read))
                return null;

            return UnicodeEncoding.Unicode.GetString(buffer);
        }

        public override int ReadMemory(Address address, byte[] buffer, int offset, int count)
        {
            if (offset != 0)
                throw new NotImplementedException("Non-zero offsets not supported (yet)");

            int bytesRead = 0;
            if (!DesktopRuntime.ReadMemory(address, buffer, count, out bytesRead))
                return 0;
            return (int)bytesRead;
        }

        public override IEnumerable<ClrType> EnumerateTypes()
        {
            LoadAllTypes();

            for (int i = 0; i < _types.Count; ++i)
                yield return _types[i];
        }


        internal bool TypesLoaded { get { return _loadedTypes; } }

        internal void LoadAllTypes()
        {
            if (_loadedTypes)
                return;

            _loadedTypes = true;

            // Walking a module is sloooow.  Ensure we only walk each module once.
            HashSet<Address> modules = new HashSet<Address>();

            foreach (Address module in DesktopRuntime.EnumerateModules(DesktopRuntime.GetAppDomainData(DesktopRuntime.SystemDomainAddress)))
                modules.Add(module);

            foreach (Address module in DesktopRuntime.EnumerateModules(DesktopRuntime.GetAppDomainData(DesktopRuntime.SharedDomainAddress)))
                modules.Add(module);

            IAppDomainStoreData ads = DesktopRuntime.GetAppDomainStoreData();
            if (ads == null)
                return;

            IList<Address> appDomains = DesktopRuntime.GetAppDomainList(ads.Count);
            if (appDomains == null)
                return;

            foreach (Address ad in appDomains)
            {
                var adData = DesktopRuntime.GetAppDomainData(ad);
                if (adData != null)
                {
                    foreach (Address module in DesktopRuntime.EnumerateModules(adData))
                        modules.Add(module);
                }
            }

            ulong arrayMt = DesktopRuntime.ArrayMethodTable;
            foreach (var module in modules)
            {
                var mtList = DesktopRuntime.GetMethodTableList(module);
                if (mtList != null)
                {
                    foreach (var pair in mtList)
                    {
                        if (pair.MethodTable != arrayMt)
                        {
                            // prefetch element type, as this also can load types
                            var type = GetTypeByMethodTable(pair.MethodTable, 0, 0);
                            if (type != null)
                            {
                                ClrElementType cet = type.ElementType;
                            }
                        }
                    }
                }
            }
        }

        internal bool GetObjectHeader(ulong obj, out uint value)
        {
            return MemoryReader.TryReadDword(obj - 4, out value);
        }

        internal IObjectData GetObjectData(Address address)
        {
            LastObjectData last = _lastObjData;

            if (_lastObjData != null && _lastObjData.Address == address)
                return _lastObjData.Data;

            last = new LastObjectData(address, DesktopRuntime.GetObjectData(address));
            _lastObjData = last;

            return last.Data;
        }

        internal object GetValueAtAddress(ClrElementType cet, Address addr)
        {
            switch (cet)
            {
                case ClrElementType.String:
                    return GetStringContents(addr);

                case ClrElementType.Class:
                case ClrElementType.Array:
                case ClrElementType.SZArray:
                case ClrElementType.Object:
                    {
                        Address val;
                        if (!MemoryReader.TryReadPtr(addr, out val))
                            return null;

                        return val;
                    }

                case ClrElementType.Boolean:
                    {
                        byte val;
                        if (!DesktopRuntime.ReadByte(addr, out val))
                            return null;
                        return val != 0;
                    }

                case ClrElementType.Int32:
                    {
                        int val;
                        if (!DesktopRuntime.ReadDword(addr, out val))
                            return null;

                        return val;
                    }

                case ClrElementType.UInt32:
                    {
                        uint val;
                        if (!DesktopRuntime.ReadDword(addr, out val))
                            return null;

                        return val;
                    }

                case ClrElementType.Int64:
                    {
                        long val;
                        if (!DesktopRuntime.ReadQword(addr, out val))
                            return long.MaxValue;

                        return val;
                    }

                case ClrElementType.UInt64:
                    {
                        ulong val;
                        if (!DesktopRuntime.ReadQword(addr, out val))
                            return long.MaxValue;

                        return val;
                    }

                case ClrElementType.NativeUInt:  // native unsigned int
                case ClrElementType.Pointer:
                case ClrElementType.FunctionPointer:
                    {
                        ulong val;
                        if (!MemoryReader.TryReadPtr(addr, out val))
                            return null;

                        return val;
                    }

                case ClrElementType.NativeInt:  // native int
                    {
                        ulong val;
                        if (!MemoryReader.TryReadPtr(addr, out val))
                            return null;

                        return (long)val;
                    }

                case ClrElementType.Int8:
                    {
                        sbyte val;
                        if (!DesktopRuntime.ReadByte(addr, out val))
                            return null;
                        return val;
                    }

                case ClrElementType.UInt8:
                    {
                        byte val;
                        if (!DesktopRuntime.ReadByte(addr, out val))
                            return null;
                        return val;
                    }

                case ClrElementType.Float:
                    {
                        float val;
                        if (!DesktopRuntime.ReadFloat(addr, out val))
                            return null;
                        return val;
                    }

                case ClrElementType.Double: // double
                    {
                        double val;
                        if (!DesktopRuntime.ReadFloat(addr, out val))
                            return null;
                        return val;
                    }

                case ClrElementType.Int16:
                    {
                        short val;
                        if (!DesktopRuntime.ReadShort(addr, out val))
                            return null;
                        return val;
                    }

                case ClrElementType.Char:  // u2
                    {
                        ushort val;
                        if (!DesktopRuntime.ReadShort(addr, out val))
                            return null;
                        return (char)val;
                    }

                case ClrElementType.UInt16:
                    {
                        ushort val;
                        if (!DesktopRuntime.ReadShort(addr, out val))
                            return null;
                        return val;
                    }
            }

            throw new Exception("Unexpected element type.");
        }

        internal ClrElementType GetElementType(BaseDesktopHeapType type, int depth)
        {
            // Max recursion.
            if (depth >= 32)
                return ClrElementType.Object;

            if (type == ObjectType)
                return ClrElementType.Object;
            else if (type == StringType)
                return ClrElementType.String;
            else if (type.ElementSize > 0)
                return ClrElementType.SZArray;

            BaseDesktopHeapType baseType = (BaseDesktopHeapType)type.BaseType;
            if (baseType == null || baseType == ObjectType)
                return ClrElementType.Object;

            bool vc = false;
            if (ValueType == null)
            {
                if (baseType.Name == "System.ValueType")
                {
                    ValueType = baseType;
                    vc = true;
                }
            }
            else if (baseType == ValueType)
            {
                vc = true;
            }

            if (!vc)
            {
                ClrElementType et = baseType.ElementType;
                if (et == ClrElementType.Unknown)
                {
                    et = GetElementType(baseType, depth + 1);
                    baseType.ElementType = et;
                }

                return et;
            }

            switch (type.Name)
            {
                case "System.Int32":
                    return ClrElementType.Int32;
                case "System.Int16":
                    return ClrElementType.Int16;
                case "System.Int64":
                    return ClrElementType.Int64;
                case "System.IntPtr":
                    return ClrElementType.NativeInt;
                case "System.UInt16":
                    return ClrElementType.UInt16;
                case "System.UInt32":
                    return ClrElementType.UInt32;
                case "System.UInt64":
                    return ClrElementType.UInt64;
                case "System.UIntPtr":
                    return ClrElementType.NativeUInt;
                case "System.Boolean":
                    return ClrElementType.Boolean;
                case "System.Single":
                    return ClrElementType.Float;
                case "System.Double":
                    return ClrElementType.Double;
                case "System.Byte":
                    return ClrElementType.UInt8;
                case "System.Char":
                    return ClrElementType.Char;
                case "System.SByte":
                    return ClrElementType.Int8;
                case "System.Enum":
                    return ClrElementType.Int32;
            }

            return ClrElementType.Struct;
        }


        #region private
        protected List<ClrType> _types;
        protected Dictionary<ModuleEntry, int> _typeEntry = new Dictionary<ModuleEntry, int>(new ModuleEntryCompare());
        private Dictionary<ArrayRankHandle, BaseDesktopHeapType> _arrayTypes;
        private ClrModule _mscorlib;

        private ClrInstanceField _firstChar, _stringLength;
        private bool _initializedStringFields = false;
        private LastObjectData _lastObjData;
        private ClrType[] _basicTypes;
        private bool _loadedTypes = false;
        #endregion

        internal readonly ClrInterface[] EmptyInterfaceList = new ClrInterface[0];
        internal Dictionary<string, ClrInterface> Interfaces = new Dictionary<string, ClrInterface>();
        internal DesktopRuntimeBase DesktopRuntime { get; private set; }
        internal BaseDesktopHeapType ErrorType { get; private set; }
        internal ClrType ObjectType { get; private set; }
        internal ClrType StringType { get; private set; }
        internal ClrType ValueType { get; private set; }
        internal ClrType FreeType { get; private set; }
        internal ClrType ExceptionType { get; private set; }
        internal ClrType EnumType { get; set; }
        internal ClrType ArrayType { get; private set; }

        private class LastObjectData
        {
            public IObjectData Data;
            public Address Address;
            public LastObjectData(Address addr, IObjectData data)
            {
                Address = addr;
                Data = data;
            }
        }

        internal struct LastObjectType
        {
            public Address Address;
            public ClrType Type;
        }

        private class ModuleEntryCompare : IEqualityComparer<ModuleEntry>
        {
            public bool Equals(ModuleEntry mx, ModuleEntry my)
            {
                return mx.Token == my.Token && mx.Module == my.Module;
            }

            public int GetHashCode(ModuleEntry obj)
            {
                return (int)obj.Token;
            }
        }

        internal ClrType GetBasicType(ClrElementType elType)
        {
            // Early out without having to construct the array.
            if (_basicTypes == null)
            {
                switch (elType)
                {
                    case ClrElementType.String:
                        return StringType;

                    case ClrElementType.Array:
                    case ClrElementType.SZArray:
                        return ArrayType;

                    case ClrElementType.Object:
                    case ClrElementType.Class:
                        return ObjectType;

                    case ClrElementType.Struct:
                        if (ValueType != null)
                            return ValueType;
                        break;
                }
            }

            if (_basicTypes == null)
                InitBasicTypes();


            return _basicTypes[(int)elType];
        }

        private void InitBasicTypes()
        {
            const int max = (int)ClrElementType.SZArray + 1;

            _basicTypes = new ClrType[max];
            _basicTypes[(int)ClrElementType.Unknown] = null;  // ???
            _basicTypes[(int)ClrElementType.String] = StringType;
            _basicTypes[(int)ClrElementType.Array] = ArrayType;
            _basicTypes[(int)ClrElementType.SZArray] = ArrayType;
            _basicTypes[(int)ClrElementType.Object] = ObjectType;
            _basicTypes[(int)ClrElementType.Class] = ObjectType;

            ClrModule mscorlib = Mscorlib;
            if (mscorlib == null)
                return;

            int count = 0;
            foreach (ClrType type in mscorlib.EnumerateTypes())
            {
                if (count == 14)
                    break;

                switch (type.Name)
                {
                    case "System.ValueType":
                        Debug.Assert(_basicTypes[(int)ClrElementType.Struct] == null);
                        _basicTypes[(int)ClrElementType.Struct] = type;
                        count++;
                        break;

                    case "System.Boolean":
                        Debug.Assert(_basicTypes[(int)ClrElementType.Boolean] == null);
                        _basicTypes[(int)ClrElementType.Boolean] = type;
                        count++;
                        break;

                    case "System.Char":
                        Debug.Assert(_basicTypes[(int)ClrElementType.Char] == null);
                        _basicTypes[(int)ClrElementType.Char] = type;
                        count++;
                        break;

                    case "System.SByte":
                        Debug.Assert(_basicTypes[(int)ClrElementType.Int8] == null);
                        _basicTypes[(int)ClrElementType.Int8] = type;
                        count++;
                        break;

                    case "System.Byte":
                        Debug.Assert(_basicTypes[(int)ClrElementType.UInt8] == null);
                        _basicTypes[(int)ClrElementType.UInt8] = type;
                        count++;
                        break;

                    case "System.Int16":
                        Debug.Assert(_basicTypes[(int)ClrElementType.Int16] == null);
                        _basicTypes[(int)ClrElementType.Int16] = type;
                        count++;
                        break;

                    case "System.UInt16":
                        Debug.Assert(_basicTypes[(int)ClrElementType.UInt16] == null);
                        _basicTypes[(int)ClrElementType.UInt16] = type;
                        count++;
                        break;

                    case "System.Int32":
                        Debug.Assert(_basicTypes[(int)ClrElementType.Int32] == null);
                        _basicTypes[(int)ClrElementType.Int32] = type;
                        count++;
                        break;

                    case "System.UInt32":
                        Debug.Assert(_basicTypes[(int)ClrElementType.UInt32] == null);
                        _basicTypes[(int)ClrElementType.UInt32] = type;
                        count++;
                        break;

                    case "System.Int64":
                        Debug.Assert(_basicTypes[(int)ClrElementType.Int64] == null);
                        _basicTypes[(int)ClrElementType.Int64] = type;
                        count++;
                        break;

                    case "System.UInt64":
                        Debug.Assert(_basicTypes[(int)ClrElementType.UInt64] == null);
                        _basicTypes[(int)ClrElementType.UInt64] = type;
                        count++;
                        break;

                    case "System.Single":
                        Debug.Assert(_basicTypes[(int)ClrElementType.Float] == null);
                        _basicTypes[(int)ClrElementType.Float] = type;
                        count++;
                        break;

                    case "System.Double":
                        Debug.Assert(_basicTypes[(int)ClrElementType.Double] == null);
                        _basicTypes[(int)ClrElementType.Double] = type;
                        count++;
                        break;

                    case "System.IntPtr":
                        Debug.Assert(_basicTypes[(int)ClrElementType.NativeInt] == null);
                        _basicTypes[(int)ClrElementType.NativeInt] = type;
                        count++;
                        break;

                    case "System.UIntPtr":
                        Debug.Assert(_basicTypes[(int)ClrElementType.NativeUInt] == null);
                        _basicTypes[(int)ClrElementType.NativeUInt] = type;
                        count++;
                        break;
                }
            }

            Debug.Assert(DesktopRuntime.DataReader.IsMinidump || count == 14);
        }

        internal BaseDesktopHeapType CreatePointerType(BaseDesktopHeapType innerType, ClrElementType clrElementType, string nameHint)
        {
            BaseDesktopHeapType result = new DesktopPointerType(this, (DesktopBaseModule)Mscorlib, clrElementType, 0, nameHint);
            result.ComponentType = innerType;
            return result;
        }

        internal BaseDesktopHeapType GetArrayType(ClrElementType clrElementType, int ranks, string nameHint)
        {
            if (_arrayTypes == null)
                _arrayTypes = new Dictionary<ArrayRankHandle, BaseDesktopHeapType>();

            var handle = new ArrayRankHandle(clrElementType, ranks);
            BaseDesktopHeapType result;
            if (!_arrayTypes.TryGetValue(handle, out result))
                _arrayTypes[handle] = result = new DesktopArrayType(this, (DesktopBaseModule)Mscorlib, clrElementType, ranks, ArrayType.MetadataToken, nameHint);

            return result;
        }

        internal ClrModule Mscorlib
        {
            get
            {
                if (_mscorlib == null)
                {
                    foreach (ClrModule module in DesktopRuntime.Modules)
                    {
                        if (module.Name.Contains("mscorlib"))
                        {
                            _mscorlib = module;
                            break;
                        }
                    }
                }

                return _mscorlib;
            }
        }
    }


    internal class FinalizerRoot : ClrRoot
    {
        private ClrType _type;
        public FinalizerRoot(ulong obj, ClrType type)
        {
            Object = obj;
            _type = type;
        }

        public override GCRootKind Kind
        {
            get { return GCRootKind.Finalizer; }
        }

        public override string Name
        {
            get
            {
                return "finalization handle";
            }
        }

        public override ClrType Type
        {
            get { return _type; }
        }
    }

    internal class HandleRoot : ClrRoot
    {
        private GCRootKind _kind;
        private string _name;
        private ClrType _type;
        private ClrAppDomain _domain;

        public HandleRoot(ulong addr, ulong obj, ClrType type, HandleType hndType, GCRootKind kind, ClrAppDomain domain)
        {
            _name = Enum.GetName(typeof(HandleType), hndType) + " handle";
            Address = addr;
            Object = obj;
            _kind = kind;
            _type = type;
            _domain = domain;
        }

        public override ClrAppDomain AppDomain
        {
            get
            {
                return _domain;
            }
        }

        public override bool IsPinned
        {
            get
            {
                return Kind == GCRootKind.Pinning || Kind == GCRootKind.AsyncPinning;
            }
        }

        public override GCRootKind Kind
        {
            get { return _kind; }
        }

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override ClrType Type
        {
            get { return _type; }
        }
    }

    internal class StaticVarRoot : ClrRoot
    {
        private string _name;
        private ClrAppDomain _domain;
        private ClrType _type;

        public StaticVarRoot(ulong addr, ulong obj, ClrType type, string typeName, string variableName, ClrAppDomain appDomain)
        {
            Address = addr;
            Object = obj;
            _name = string.Format("static var {0}.{1}", typeName, variableName);
            _domain = appDomain;
            _type = type;
        }

        public override ClrAppDomain AppDomain
        {
            get
            {
                return _domain;
            }
        }

        public override GCRootKind Kind
        {
            get { return GCRootKind.StaticVar; }
        }

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override ClrType Type
        {
            get { return _type; }
        }
    }

    internal class ThreadStaticVarRoot : ClrRoot
    {
        private string _name;
        private ClrAppDomain _domain;
        private ClrType _type;

        public ThreadStaticVarRoot(ulong addr, ulong obj, ClrType type, string typeName, string variableName, ClrAppDomain appDomain)
        {
            Address = addr;
            Object = obj;
            _name = string.Format("thread static var {0}.{1}", typeName, variableName);
            _domain = appDomain;
            _type = type;
        }

        public override ClrAppDomain AppDomain
        {
            get
            {
                return _domain;
            }
        }

        public override GCRootKind Kind
        {
            get { return GCRootKind.ThreadStaticVar; }
        }

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override ClrType Type
        {
            get { return _type; }
        }
    }

    internal class DesktopBlockingObject : BlockingObject
    {
        private Address _obj;
        private bool _locked;
        private int _recursion;
        private IList<ClrThread> _waiters;
        private BlockingReason _reason;
        private ClrThread[] _owners;

        private static readonly ClrThread[] s_emptyWaiters = new ClrThread[0];

        internal void SetOwners(ClrThread[] owners)
        {
            _owners = owners;
        }

        internal void SetOwner(ClrThread owner)
        {
            _owners = new ClrThread[0];
            _owners[0] = owner;
        }

        public DesktopBlockingObject(Address obj, bool locked, int recursion, ClrThread owner, BlockingReason reason)
        {
            _obj = obj;
            _locked = locked;
            _recursion = recursion;
            _reason = reason;
            _owners = new ClrThread[1];
            _owners[0] = owner;
        }

        public DesktopBlockingObject(Address obj, bool locked, int recursion, BlockingReason reason, ClrThread[] owners)
        {
            _obj = obj;
            _locked = locked;
            _recursion = recursion;
            _reason = reason;
            _owners = owners;
        }

        public DesktopBlockingObject(Address obj, bool locked, int recursion, BlockingReason reason)
        {
            _obj = obj;
            _locked = locked;
            _recursion = recursion;
            _reason = reason;
        }

        public override Address Object
        {
            get { return _obj; }
        }

        public override bool Taken
        {
            get { return _locked; }
        }

        public void SetTaken(bool status)
        {
            _locked = status;
        }

        public override int RecursionCount
        {
            get { return _recursion; }
        }


        public override IList<ClrThread> Waiters
        {
            get
            {
                if (_waiters == null)
                    return s_emptyWaiters;

                return _waiters;
            }
        }

        internal void AddWaiter(ClrThread thread)
        {
            if (thread == null)
                return;

            if (_waiters == null)
                _waiters = new List<ClrThread>();

            _waiters.Add(thread);
            _locked = true;
        }

        public override BlockingReason Reason
        {
            get { return _reason; }
            internal set { _reason = value; }
        }

        public override ClrThread Owner
        {
            get
            {
                if (!HasSingleOwner)
                    throw new InvalidOperationException("BlockingObject has more than one owner.");

                return _owners[0];
            }
        }

        public override bool HasSingleOwner
        {
            get { return _owners.Length == 1; }
        }

        public override IList<ClrThread> Owners
        {
            get
            {
                return _owners ?? new ClrThread[0];
            }
        }
    }

    internal class DesktopException : ClrException
    {
        public DesktopException(Address objRef, BaseDesktopHeapType type)
        {
            _object = objRef;
            _type = type;
        }

        public override ClrType Type
        {
            get { return _type; }
        }

        public override string Message
        {
            get
            {
                var field = _type.GetFieldByName("_message");
                if (field != null)
                    return (string)field.GetValue(_object);

                var runtime = _type.DesktopHeap.DesktopRuntime;
                uint offset = runtime.GetExceptionMessageOffset();
                Debug.Assert(offset > 0);

                ulong message = _object + offset;
                if (!runtime.ReadPointer(message, out message))
                    return null;

                return _type.DesktopHeap.GetStringContents(message);
            }
        }

        public override Address Address
        {
            get { return _object; }
        }

        public override ClrException Inner
        {
            get
            {
                // TODO:  This needs to get the field offset by runtime instead.
                var field = _type.GetFieldByName("_innerException");
                if (field == null)
                    return null;
                object inner = field.GetValue(_object);
                if (inner == null || !(inner is ulong) || ((ulong)inner == 0))
                    return null;

                ulong ex = (ulong)inner;
                BaseDesktopHeapType type = (BaseDesktopHeapType)_type.DesktopHeap.GetObjectType(ex);

                return new DesktopException(ex, type);
            }
        }

        public override IList<ClrStackFrame> StackTrace
        {
            get
            {
                if (_stackTrace == null)
                    _stackTrace = _type.DesktopHeap.DesktopRuntime.GetExceptionStackTrace(_object, _type);

                return _stackTrace;
            }
        }

        public override int HResult
        {
            get
            {
                var field = _type.GetFieldByName("_HResult");
                if (field != null)
                    return (int)field.GetValue(_object);

                int hr = 0;
                var runtime = _type.DesktopHeap.DesktopRuntime;
                uint offset = runtime.GetExceptionHROffset();
                runtime.ReadDword(_object + offset, out hr);

                return hr;
            }
        }

        #region Private
        private Address _object;
        private BaseDesktopHeapType _type;
        private IList<ClrStackFrame> _stackTrace;
        #endregion
    }

    internal struct ArrayRankHandle : IEquatable<ArrayRankHandle>
    {
        private ClrElementType _type;
        private int _ranks;

        public ArrayRankHandle(ClrElementType eltype, int ranks)
        {
            _type = eltype;
            _ranks = ranks;
        }

        public bool Equals(ArrayRankHandle other)
        {
            return _type == other._type && _ranks == other._ranks;
        }
    }

    internal struct TypeHandle : IEquatable<TypeHandle>
    {
        public Address MethodTable;
        public Address ComponentMethodTable;

        #region Constructors
        public TypeHandle(ulong mt)
        {
            MethodTable = mt;
            ComponentMethodTable = 0;
        }

        public TypeHandle(ulong mt, ulong cmt)
        {
            MethodTable = mt;
            ComponentMethodTable = cmt;
        }
        #endregion

        public override int GetHashCode()
        {
            return ((int)MethodTable + (int)ComponentMethodTable) >> 3;
        }

        bool IEquatable<TypeHandle>.Equals(TypeHandle other)
        {
            return (MethodTable == other.MethodTable) && (ComponentMethodTable == other.ComponentMethodTable);
        }

        #region Compare Helpers
        // TODO should not be needed.   IEquatable should cover it.  
        public static IEqualityComparer<TypeHandle> EqualityComparer = new HeapTypeEqualityComparer();
        private class HeapTypeEqualityComparer : IEqualityComparer<TypeHandle>
        {
            public bool Equals(TypeHandle x, TypeHandle y)
            {
                return (x.MethodTable == y.MethodTable) && (x.ComponentMethodTable == y.ComponentMethodTable);
            }
            public int GetHashCode(TypeHandle obj)
            {
                return ((int)obj.MethodTable + (int)obj.ComponentMethodTable) >> 3;
            }
        }
        #endregion
    }

    struct ModuleEntry
    {
        public ClrModule Module;
        public uint Token;
        public ModuleEntry(ClrModule module, uint token)
        {
            Module = module;
            Token = token;
        }
    }

    internal class LegacyGCHeap : DesktopGCHeap
    {
        private LastObjectType _lastObjType = new LastObjectType();
        private Dictionary<TypeHandle, int> _indices = new Dictionary<TypeHandle, int>(TypeHandle.EqualityComparer);

        public LegacyGCHeap(DesktopRuntimeBase runtime)
            : base(runtime)
        {
        }

        public override bool HasComponentMethodTables
        {
            get
            {
                return true;
            }
        }

        public override ClrType GetTypeByMethodTable(ulong mt, ulong cmt)
        {
            return GetTypeByMethodTable(mt, cmt, 0);
        }

        internal override ClrType GetTypeByMethodTable(ulong mt, ulong cmt, ulong obj)
        {
            if (mt == 0)
                return null;

            ClrType componentType = null;
            if (mt == DesktopRuntime.ArrayMethodTable)
            {
                if (cmt != 0)
                {
                    componentType = GetTypeByMethodTable(cmt, 0);
                    if (componentType != null)
                    {
                        cmt = componentType.MethodTable;
                    }
                    else if (obj != 0)
                    {
                        componentType = TryGetComponentType(obj, cmt);
                        if (componentType != null)
                            cmt = componentType.MethodTable;
                    }
                }
                else
                {
                    componentType = ObjectType;
                    cmt = ObjectType.MethodTable;
                }
            }
            else
            {
                cmt = 0;
            }

            TypeHandle hnd = new TypeHandle(mt, cmt);
            ClrType ret = null;

            // See if we already have the type.
            int index;
            if (_indices.TryGetValue(hnd, out index))
            {
                ret = _types[index];
            }
            else if (mt == DesktopRuntime.ArrayMethodTable && cmt == 0)
            {
                // Handle the case where the methodtable is an array, but the component method table
                // was not specified.  (This happens with fields.)  In this case, return System.Object[],
                // with an ArrayComponentType set to System.Object.
                uint token = DesktopRuntime.GetMetadataToken(mt);
                if (token == 0xffffffff)
                    return null;

                ModuleEntry modEnt = new ModuleEntry(ArrayType.Module, token);

                ret = ArrayType;
                index = _types.Count;

                _indices[hnd] = index;
                _typeEntry[modEnt] = index;
                _types.Add(ret);

                Debug.Assert(_types[index] == ret);
            }
            else
            {
                // No, so we'll have to construct it.
                var moduleAddr = DesktopRuntime.GetModuleForMT(hnd.MethodTable);
                DesktopModule module = DesktopRuntime.GetModule(moduleAddr);
                uint token = DesktopRuntime.GetMetadataToken(mt);

                bool isFree = mt == DesktopRuntime.FreeMethodTable;
                if (token == 0xffffffff && !isFree)
                    return null;

                // Dynamic functions/modules
                uint tokenEnt = token;
                if (!isFree && (module == null || module.IsDynamic))
                    tokenEnt = (uint)mt;

                ModuleEntry modEnt = new ModuleEntry(module, tokenEnt);

                // We key the dictionary on a Module/Token pair.  If names do not match, then
                // do not treat these as the same type (happens with generics).
                string typeName = DesktopRuntime.GetTypeName(hnd);
                if (typeName == null || typeName == "<Unloaded Type>")
                {
                    var builder = GetTypeNameFromToken(module, token);
                    typeName = (builder != null) ? builder.ToString() : "<UNKNOWN>";
                }
                else
                {
                    typeName = DesktopHeapType.FixGenerics(typeName);
                }

                if (_typeEntry.TryGetValue(modEnt, out index))
                {
                    BaseDesktopHeapType match = (BaseDesktopHeapType)_types[index];
                    if (match.Name == typeName)
                    {
                        _indices[hnd] = index;
                        ret = match;
                    }
                }

                if (ret == null)
                {
                    IMethodTableData mtData = DesktopRuntime.GetMethodTableData(mt);
                    if (mtData == null)
                        return null;

                    ret = new DesktopHeapType(typeName, module, token, mt, mtData, this);
                    ret.ComponentType = componentType;

                    index = _types.Count;
                    ((DesktopHeapType)ret).SetIndex(index);
                    _indices[hnd] = index;
                    _typeEntry[modEnt] = index;
                    _types.Add(ret);

                    Debug.Assert(_types[index] == ret);
                }
            }

            if (obj != 0 && ret.ComponentType == null && ret.IsArray)
                ret.ComponentType = TryGetComponentType(obj, cmt);

            return ret;
        }


        public override ClrType GetObjectType(Address objRef)
        {
            ulong mt, cmt = 0;

            if (_lastObjType.Address == objRef)
                return _lastObjType.Type;

            var cache = MemoryReader;
            if (cache.Contains(objRef))
            {
                if (!cache.ReadPtr(objRef, out mt))
                    return null;
            }
            else if (DesktopRuntime.MemoryReader.Contains(objRef))
            {
                cache = DesktopRuntime.MemoryReader;
                if (!cache.ReadPtr(objRef, out mt))
                    return null;
            }
            else
            {
                cache = null;
                mt = DesktopRuntime.DataReader.ReadPointerUnsafe(objRef);
            }

            if ((((int)mt) & 3) != 0)
                mt &= ~3UL;

            if (mt == DesktopRuntime.ArrayMethodTable)
            {
                uint elemenTypeOffset = (uint)PointerSize * 2;
                if (cache == null)
                    cmt = DesktopRuntime.DataReader.ReadPointerUnsafe(objRef + elemenTypeOffset);
                else if (!cache.ReadPtr(objRef + elemenTypeOffset, out cmt))
                    return null;
            }
            else
            {
                cmt = 0;
            }

            ClrType type = GetTypeByMethodTable(mt, cmt, objRef);
            _lastObjType.Address = objRef;
            _lastObjType.Type = type;

            return type;
        }
    }

    internal class V46GCHeap : DesktopGCHeap
    {
        private LastObjectType _lastObjType = new LastObjectType();
        private Dictionary<Address, int> _indices = new Dictionary<Address, int>();

        public V46GCHeap(DesktopRuntimeBase runtime)
            : base(runtime)
        {

        }

        public override ClrType GetObjectType(Address objRef)
        {
            ulong mt;

            if (_lastObjType.Address == objRef)
                return _lastObjType.Type;

            var cache = MemoryReader;
            if (cache.Contains(objRef))
            {
                if (!cache.ReadPtr(objRef, out mt))
                    return null;
            }
            else if (DesktopRuntime.MemoryReader.Contains(objRef))
            {
                cache = DesktopRuntime.MemoryReader;
                if (!cache.ReadPtr(objRef, out mt))
                    return null;
            }
            else
            {
                cache = null;
                mt = DesktopRuntime.DataReader.ReadPointerUnsafe(objRef);
            }

            if ((((int)mt) & 3) != 0)
                mt &= ~3UL;

            ClrType type = GetTypeByMethodTable(mt, 0, objRef);
            _lastObjType.Address = objRef;
            _lastObjType.Type = type;

            return type;
        }

        public override ClrType GetTypeByMethodTable(ulong mt, ulong cmt)
        {
            return GetTypeByMethodTable(mt, 0, 0);
        }

        internal override ClrType GetTypeByMethodTable(ulong mt, ulong _, ulong obj)
        {
            if (mt == 0)
                return null;

            ClrType ret = null;

            // See if we already have the type.
            int index;
            if (_indices.TryGetValue(mt, out index))
            {
                ret = _types[index];
            }
            else
            {
                // No, so we'll have to construct it.
                var moduleAddr = DesktopRuntime.GetModuleForMT(mt);
                DesktopModule module = DesktopRuntime.GetModule(moduleAddr);
                uint token = DesktopRuntime.GetMetadataToken(mt);

                bool isFree = mt == DesktopRuntime.FreeMethodTable;
                if (token == 0xffffffff && !isFree)
                    return null;

                // Dynamic functions/modules
                uint tokenEnt = token;
                if (!isFree && (module == null || module.IsDynamic))
                    tokenEnt = (uint)mt;

                ModuleEntry modEnt = new ModuleEntry(module, tokenEnt);

                // We key the dictionary on a Module/Token pair.  If names do not match, then
                // do not treat these as the same type (happens with generics).
                string typeName = DesktopRuntime.GetNameForMT(mt);
                if (typeName == null || typeName == "<Unloaded Type>")
                {
                    var builder = GetTypeNameFromToken(module, token);
                    typeName = (builder != null) ? builder.ToString() : "<UNKNOWN>";
                }
                else
                {
                    typeName = DesktopHeapType.FixGenerics(typeName);
                }

                if (_typeEntry.TryGetValue(modEnt, out index))
                {
                    BaseDesktopHeapType match = (BaseDesktopHeapType)_types[index];
                    if (match.Name == typeName)
                    {
                        _indices[mt] = index;
                        ret = match;
                    }
                }

                if (ret == null)
                {
                    IMethodTableData mtData = DesktopRuntime.GetMethodTableData(mt);
                    if (mtData == null)
                        return null;

                    ret = new DesktopHeapType(typeName, module, token, mt, mtData, this);

                    index = _types.Count;
                    ((DesktopHeapType)ret).SetIndex(index);
                    _indices[mt] = index;

                    // Arrays share a common token, so it's not helpful to look them up here.
                    if (!ret.IsArray)
                        _typeEntry[modEnt] = index;

                    _types.Add(ret);
                    Debug.Assert(_types[index] == ret);
                }
            }

            if (obj != 0 && ret.ComponentType == null && ret.IsArray)
                ret.ComponentType = TryGetComponentType(obj, 0);

            return ret;
        }
    }
}