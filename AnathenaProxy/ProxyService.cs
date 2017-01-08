namespace AnathenaProxy
{
    using Anathena.Assemblers.Fasm;
    using Microsoft.Diagnostics.Runtime;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

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

        public ProxyService()
        {
        }

        public Byte[] Assemble(Boolean isProcess32Bit, String assembly, UInt64 baseAddress, out String logs)
        {
            logs = "Starting instruction assembly" + Environment.NewLine;

            if (assembly == null)
            {
                logs += "No assembly code given" + Environment.NewLine;
                return null;
            }

            // Add header information about process
            if (isProcess32Bit)
            {
                assembly = String.Format("use32\n" + "org 0x{0:X8}\n", baseAddress) + assembly;
            }
            else
            {
                assembly = String.Format("use64\n" + "org 0x{0:X16}\n", baseAddress) + assembly;
            }

            logs += assembly + Environment.NewLine;

            Byte[] result;
            try
            {
                // Call C++ FASM wrapper which will call the 32-bit FASM library which can assemble all x86/x64 instructions
                result = FasmNet.Assemble(assembly);

                logs += "Assembled byte results:" + Environment.NewLine;

                foreach (Byte next in result)
                {
                    logs += next.ToString("X") + " ";
                }

                logs += Environment.NewLine;
            }
            catch (Exception ex)
            {
                logs += "Error:" + ex.ToString() + Environment.NewLine;
                result = null;
            }

            return result;
        }

        #region Clr

        private ClrHeap Heap { get; set; }

        private Dictionary<UInt64, ClrRoot> Roots { get; set; }

        private Dictionary<UInt64, ClrField> Fields { get; set; }

        public Boolean RefreshHeap(Int32 ProcessId)
        {
            // Clear all variables on a heap read
            this.Heap = null;
            this.Roots = new Dictionary<UInt64, ClrRoot>();
            this.Fields = new Dictionary<UInt64, ClrField>();

            try
            {
                DataTarget dataTarget = DataTarget.AttachToProcess(ProcessId, AttachTimeout, AttachFlag.Passive);

                if (dataTarget.ClrVersions.Count <= 0)
                {
                    return false;
                }

                ClrInfo version = dataTarget.ClrVersions[0];
                ClrRuntime runtime = version.CreateRuntime();
                this.Heap = runtime.GetHeap();
            }
            catch
            {
            }

            return this.Heap == null ? false : true;
        }

        private TypeCode TranslateType(ClrElementType? elementType)
        {
            Type result;

            if (elementType == null)
            {
                return TypeCode.Empty;
            }

            switch (elementType)
            {
                case ClrElementType.Boolean:
                    result = typeof(Boolean);
                    break;
                case ClrElementType.Int8:
                    result = typeof(SByte);
                    break;
                case ClrElementType.UInt8:
                    result = typeof(Byte);
                    break;
                case ClrElementType.Char:
                    result = typeof(Byte);
                    break;
                case ClrElementType.UInt16:
                    result = typeof(UInt16);
                    break;
                case ClrElementType.UInt32:
                    result = typeof(UInt32);
                    break;
                case ClrElementType.NativeUInt:
                    result = typeof(UInt32);
                    break;
                case ClrElementType.UInt64:
                    result = typeof(UInt64);
                    break;
                case ClrElementType.Int16:
                    result = typeof(Int16);
                    break;
                case ClrElementType.Int32:
                    result = typeof(Int32);
                    break;
                case ClrElementType.NativeInt:
                    result = typeof(Int32);
                    break;
                case ClrElementType.Int64:
                    result = typeof(Int64);
                    break;
                case ClrElementType.Float:
                    result = typeof(Single);
                    break;
                case ClrElementType.Double:
                    result = typeof(Double);
                    break;
                default:
                    result = null;
                    break;
            }

            if (result == null)
            {
                return TypeCode.Empty;
            }

            return Type.GetTypeCode(result);
        }

        public IEnumerable<UInt64> GetRoots()
        {
            foreach (ClrRoot root in this.Heap?.EnumerateRoots())
            {
                // Prefilter bad roots
                if (root == null || root.Type == null || root.Name == null)
                {
                    continue;
                }

                this.Roots[root == null ? 0 : root.Object] = root;
            }

            return this.Roots?.Keys?.ToList();
        }

        public IEnumerable<UInt64> GetObjectChildren(UInt64 objectRef)
        {
            List<UInt64> children = new List<UInt64>();

            if (this.Roots.ContainsKey(objectRef))
            {
                this.Heap?.GetObjectType(objectRef)?.EnumerateRefsOfObject(objectRef, delegate (UInt64 childObjectRef, Int32 Offset)
                {
                    children.Add(childObjectRef);
                });
            }

            return children;
        }

        public Int32 GetRootType(UInt64 rootRef)
        {
            if (this.Roots.ContainsKey(rootRef))
            {
                return (Int32)this.TranslateType(this.Roots[rootRef]?.Type?.ElementType);
            }

            return (Int32)TypeCode.Empty;
        }

        public String GetRootName(UInt64 rootRef)
        {
            if (this.Roots.ContainsKey(rootRef))
            {
                return this.Roots[rootRef].Name;
            }

            return null;
        }

        public IEnumerable<UInt64> GetObjectFields(UInt64 objectRef)
        {
            List<UInt64> fieldReferences = new List<UInt64>();

            IEnumerable<ClrField> objectFields = this.Heap?.GetObjectType(objectRef)?.Fields;

            if (objectFields == null)
            {
                return fieldReferences;
            }

            foreach (ClrField field in objectFields)
            {
                UInt64 fieldReference = unchecked((UInt64)field.GetHashCode());
                this.Fields[fieldReference] = field;
                fieldReferences.Add(fieldReference);
            }

            return fieldReferences.ToArray();
        }

        public Int32 GetObjectType(UInt64 objectRef)
        {
            return (Int32)this.TranslateType(this.Heap?.GetObjectType(objectRef)?.ElementType);
        }

        public String GetObjectName(UInt64 objectRef)
        {
            return this.Heap?.GetObjectType(objectRef)?.Name;
        }

        public String GetFieldName(UInt64 fieldRef)
        {
            if (this.Fields.ContainsKey(fieldRef))
            {
                return this.Fields[fieldRef].Name;
            }

            return null;
        }

        public Int32 GetFieldType(UInt64 fieldRef)
        {
            if (this.Fields.ContainsKey(fieldRef))
            {
                return (Int32)this.TranslateType(this.Fields[fieldRef].ElementType);
            }

            return (Int32)TypeCode.Empty;
        }

        public Int32 GetFieldOffset(UInt64 fieldRef)
        {
            if (this.Fields.ContainsKey(fieldRef))
            {
                return this.Fields[fieldRef].Offset + IntPtr.Size;
            }

            return 0;
        }
        #endregion
    }
    //// End class
}
//// End namespace