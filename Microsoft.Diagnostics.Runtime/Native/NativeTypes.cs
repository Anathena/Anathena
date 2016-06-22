// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Microsoft.Diagnostics.Runtime.Native
{
    internal class NativeType : ClrType
    {
        private string _name;
        private ulong _eeType;
        private NativeHeap _heap;
        private NativeModule _module;
        private uint _baseSize;
        private uint _componentSize;
        private GCDesc _gcDesc;
        private bool _containsPointers;
        private int _index;

        public NativeType(NativeHeap heap, int index, NativeModule module, string name, ulong eeType, Microsoft.Diagnostics.Runtime.Desktop.IMethodTableData mtData)
        {
            _heap = heap;
            _module = module;
            _name = name;
            _eeType = eeType;
            _index = index;

            if (mtData != null)
            {
                _baseSize = mtData.BaseSize;
                _componentSize = mtData.ComponentSize;
                _containsPointers = mtData.ContainsPointers;
            }
        }

        internal override ClrMethod GetMethod(uint token)
        {
            return null;
        }

        public override ulong MethodTable
        {
            get
            {
                return _eeType;
            }
        }

        public override IEnumerable<ulong> EnumerateMethodTables()
        {
            return new ulong[] { _eeType };
        }
        
        public override ClrModule Module
        {
            get
            {
                return _module;
            }
        }

        public override string Name
        {
            get { return _name; }
        }

        public override ulong GetSize(ulong objRef)
        {
            ulong size;
            uint pointerSize = (uint)_heap.PointerSize;
            if (_componentSize == 0)
            {
                size = _baseSize;
            }
            else
            {
                uint count = 0;
                uint countOffset = pointerSize;
                ulong loc = objRef + countOffset;

                var cache = _heap.MemoryReader;
                if (!cache.Contains(loc))
                    cache = _heap.NativeRuntime.MemoryReader;

                if (!cache.ReadDword(loc, out count))
                    throw new Exception("Could not read from heap at " + objRef.ToString("x"));

                // TODO:  Strings in v4+ contain a trailing null terminator not accounted for.


                size = count * (ulong)_componentSize + _baseSize;
            }

            uint minSize = pointerSize * 3;
            if (size < minSize)
                size = minSize;
            return size;
        }

        public override void EnumerateRefsOfObject(ulong objRef, Action<ulong, int> action)
        {
            if (!_containsPointers)
                return;

            if (_gcDesc == null)
                if (!FillGCDesc() || _gcDesc == null)
                    return;

            var size = GetSize(objRef);
            var cache = _heap.MemoryReader;
            if (!cache.Contains(objRef))
                cache = _heap.NativeRuntime.MemoryReader;

            _gcDesc.WalkObject(objRef, (ulong)size, cache, action);
        }


        private bool FillGCDesc()
        {
            NativeRuntime runtime = _heap.NativeRuntime;

            int entries;
            if (!runtime.MemoryReader.TryReadDword(_eeType - (ulong)IntPtr.Size, out entries))
                return false;

            // Get entries in map
            if (entries < 0)
                entries = -entries;

            int read;
            int slots = 1 + entries * 2;
            byte[] buffer = new byte[slots * IntPtr.Size];
            if (!runtime.ReadMemory(_eeType - (ulong)(slots * IntPtr.Size), buffer, buffer.Length, out read) || read != buffer.Length)
                return false;

            // Construct the gc desc
            _gcDesc = new GCDesc(buffer);
            return true;
        }

        public override ClrHeap Heap
        {
            get { throw new NotImplementedException(); }
        }

        public override IList<ClrInterface> Interfaces
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsFinalizable
        {
            get { throw new NotImplementedException(); }
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

        public override bool IsAbstract
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsSealed
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsInterface
        {
            get { throw new NotImplementedException(); }
        }

        public override bool GetFieldForOffset(int fieldOffset, bool inner, out ClrInstanceField childField, out int childFieldOffset)
        {
            throw new NotImplementedException();
        }

        public override ClrInstanceField GetFieldByName(string name)
        {
            throw new NotImplementedException();
        }

        public override ClrType BaseType
        {
            get { throw new NotImplementedException(); }
        }

        public override int GetArrayLength(ulong objRef)
        {
            throw new NotImplementedException();
        }

        public override ulong GetArrayElementAddress(ulong objRef, int index)
        {
            throw new NotImplementedException();
        }

        public override object GetArrayElementValue(ulong objRef, int index)
        {
            throw new NotImplementedException();
        }

        public override int ElementSize
        {
            get { throw new NotImplementedException(); }
        }

        public override int BaseSize
        {
            get { throw new NotImplementedException(); }
        }

        public override ClrStaticField GetStaticFieldByName(string name)
        {
            throw new NotImplementedException();
        }

        public override void EnumerateRefsOfObjectCarefully(ulong objRef, Action<ulong, int> action)
        {
            EnumerateRefsOfObject(objRef, action);
        }

        public override uint MetadataToken
        {
            get { throw new NotImplementedException(); }
        }
    }
}
