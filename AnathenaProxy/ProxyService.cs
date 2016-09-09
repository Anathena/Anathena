using Anathena.Assemblers.Fasm;
using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace AnathenaProxy
{
    /// <summary>
    /// Proxy service to be contained by a 32 and 64 bit service, with services exposed via IPC. Useful for certain things that
    /// Anathena requires, such as:
    /// - FASM Compiler, which can only be run in 32 bit mode
    /// - Microsoft.Diagnostics.Runtime, which can only be used on processes of the same bitness
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ProxyService : IProxyService
    {
        private const Int32 AttachTimeout = 5000;

        public ProxyService() { }

        public Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, UInt64 BaseAddress)
        {
            if (Assembly == null)
                return null;

            // Add header information about process
            if (IsProcess32Bit)
                Assembly = String.Format("use32\n" + "org 0x{0:X8}\n", BaseAddress) + Assembly;
            else
                Assembly = String.Format("use64\n" + "org 0x{0:X16}\n", BaseAddress) + Assembly;

            // Print fully assembly to console
            Console.WriteLine("\n" + Assembly + "\n");

            Byte[] Result;
            try
            {
                // Call C++ FASM wrapper which will call the 32-bit FASM library which can assemble all x86/x64 instructions
                Result = FasmNet.Assemble(Assembly);

                // Print bytes to console
                Array.ForEach(Result, (X => Console.Write(X.ToString() + " ")));
            }
            catch
            {
                Result = null;
            }
            return Result;
        }

        #region Clr

        private ClrHeap Heap;
        private Dictionary<UInt64, ClrRoot> Roots;
        private Dictionary<UInt64, ClrField> Fields;

        public Boolean RefreshHeap(Int32 ProcessId)
        {
            // Clear all variables on a heap read
            Heap = null;
            Roots = new Dictionary<UInt64, ClrRoot>();
            Fields = new Dictionary<UInt64, ClrField>();

            try
            {
                DataTarget DataTarget = DataTarget.AttachToProcess(ProcessId, AttachTimeout, AttachFlag.Passive);

                if (DataTarget.ClrVersions.Count <= 0)
                    return false;

                ClrInfo Version = DataTarget.ClrVersions[0];
                ClrRuntime Runtime = Version.CreateRuntime();
                Heap = Runtime.GetHeap();
            }
            catch { }

            return Heap == null ? false : true;
        }

        private TypeCode TranslateType(ClrElementType? ElementType)
        {
            Type Result;

            if (ElementType == null)
                return TypeCode.Empty;

            switch (ElementType)
            {
                case ClrElementType.Boolean: Result = typeof(Boolean); break;
                case ClrElementType.Int8: Result = typeof(SByte); break;
                case ClrElementType.UInt8: Result = typeof(Byte); break;
                case ClrElementType.Char: Result = typeof(Byte); break;
                case ClrElementType.UInt16: Result = typeof(UInt16); break;
                case ClrElementType.UInt32: Result = typeof(UInt32); break;
                case ClrElementType.NativeUInt: Result = typeof(UInt32); break;
                case ClrElementType.UInt64: Result = typeof(UInt64); break;
                case ClrElementType.Int16: Result = typeof(Int16); break;
                case ClrElementType.Int32: Result = typeof(Int32); break;
                case ClrElementType.NativeInt: Result = typeof(Int32); break;
                case ClrElementType.Int64: Result = typeof(Int64); break;
                case ClrElementType.Float: Result = typeof(Single); break;
                case ClrElementType.Double: Result = typeof(Double); break;
                default: Result = null; break;
            }

            if (Result == null)
                return TypeCode.Empty;

            return Type.GetTypeCode(Result);
        }

        public IEnumerable<UInt64> GetRoots()
        {
            foreach (ClrRoot Root in Heap?.EnumerateRoots())
            {
                // Prefilter bad roots
                if (Root == null || Root.Type == null || Root.Name == null)
                    continue;

                Roots[Root == null ? 0 : Root.Object] = Root;
            }

            return Roots?.Keys?.ToList();
        }

        public IEnumerable<UInt64> GetObjectChildren(UInt64 ObjectRef)
        {
            List<UInt64> Children = new List<UInt64>();

            if (Roots.ContainsKey(ObjectRef))
            {
                Heap?.GetObjectType(ObjectRef)?.EnumerateRefsOfObject(ObjectRef, delegate (UInt64 ChildObjectRef, Int32 Offset)
                {
                    Children.Add(ChildObjectRef);
                });
            }

            return Children;
        }

        public Int32 GetRootType(UInt64 RootRef)
        {
            if (Roots.ContainsKey(RootRef))
                return (Int32)TranslateType(Roots[RootRef]?.Type?.ElementType);

            return (Int32)TypeCode.Empty;
        }

        public String GetRootName(UInt64 RootRef)
        {
            if (Roots.ContainsKey(RootRef))
                return Roots[RootRef].Name;

            return null;
        }

        public IEnumerable<UInt64> GetObjectFields(UInt64 ObjectRef)
        {
            List<UInt64> FieldReferences = new List<UInt64>();

            IEnumerable<ClrField> ObjectFields = Heap?.GetObjectType(ObjectRef)?.Fields;

            if (ObjectFields == null)
                return FieldReferences;

            foreach (ClrField Field in ObjectFields)
            {
                UInt64 FieldReference = unchecked((UInt64)Field.GetHashCode());
                Fields[FieldReference] = Field;

                FieldReferences.Add(FieldReference);
            }

            return FieldReferences.ToArray();
        }

        public Int32 GetObjectType(UInt64 ObjectRef)
        {
            return (Int32)TranslateType(Heap?.GetObjectType(ObjectRef)?.ElementType);
        }

        public String GetObjectName(UInt64 ObjectRef)
        {
            return Heap?.GetObjectType(ObjectRef)?.Name;
        }

        public String GetFieldName(UInt64 FieldRef)
        {
            if (Fields.ContainsKey(FieldRef))
                return Fields[FieldRef].Name;

            return null;
        }

        public Int32 GetFieldType(UInt64 FieldRef)
        {
            if (Fields.ContainsKey(FieldRef))
                return (Int32)TranslateType(Fields[FieldRef].ElementType);

            return (Int32)TypeCode.Empty;
        }

        public Int32 GetFieldOffset(UInt64 FieldRef)
        {
            if (Fields.ContainsKey(FieldRef))
                return Fields[FieldRef].Offset + IntPtr.Size;

            return 0;
        }

        #endregion

    } // End class

} // End namespace