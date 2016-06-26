using Anathema.Source.Utils.Extensions;
using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Anathema.Source.Engine.DotNetObjectCollector
{
    public class DotNetObject : IEnumerable, IComparable<DotNetObject>
    {
        private DotNetObject Parent;
        private List<DotNetObject> Children;
        private UInt64 ObjectReference;
        private String Name;
        private ClrElementType ElementType;

        public DotNetObject(DotNetObject Parent, UInt64 ObjectReference, ClrElementType ElementType, String Name)
        {
            this.Parent = Parent;
            this.ObjectReference = unchecked(ObjectReference);
            this.ElementType = ElementType;

            // Trim root name from all objects if applicable
            String RootName = GetRootName();
            if (Name.StartsWith(RootName))
                Name = Name.Remove(0, RootName.Length);

            this.Name = Name;

            Children = new List<DotNetObject>();
        }

        public void AddChild(DotNetObject ObjectNode)
        {
            if (!Children.Contains(ObjectNode))
                Children.Add(ObjectNode);
        }

        public List<DotNetObject> GetChildren()
        {
            return Children;
        }

        public void SortChildren()
        {
            Children.Sort();
        }

        public IntPtr GetAddress()
        {
            try
            {
                return ObjectReference.ToIntPtr();
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        public Type GetElementType()
        {
            switch (ElementType)
            {
                case ClrElementType.Boolean:
                    return typeof(Byte);
                case ClrElementType.Int8:
                    return typeof(SByte);
                case ClrElementType.UInt8:
                case ClrElementType.Char:
                    return typeof(Byte);
                case ClrElementType.UInt16:
                    return typeof(UInt16);
                case ClrElementType.UInt32:
                case ClrElementType.NativeUInt:
                    return typeof(UInt32);
                case ClrElementType.UInt64:
                    return typeof(UInt64);
                case ClrElementType.Int16:
                    return typeof(Int16);
                case ClrElementType.Int32:
                case ClrElementType.NativeInt:
                    return typeof(Int32);
                case ClrElementType.Int64:
                    return typeof(Int64);
                case ClrElementType.Float:
                    return typeof(Single);
                case ClrElementType.Double:
                    return typeof(Double);
                default:
                    return typeof(Int32);
            }
        }

        public String GetName()
        {
            return Name == null ? String.Empty : Name;
        }

        public String GetFullName()
        {
            return GetFullNamespace(String.Empty);
        }

        private String GetRootName()
        {
            if (Parent == null)
                return GetName();

            return Parent.GetRootName();
        }

        private String GetFullNamespace(String CurrentNamespace)
        {
            if (Parent == null)
                return GetRootName();

            return Parent.GetFullNamespace(CurrentNamespace) + "." + Name;
        }

        public Int32 CompareTo(DotNetObject Other)
        {
            if (this.ObjectReference > Other.ObjectReference)
                return 1;

            if (this.ObjectReference == Other.ObjectReference)
                return 0;

            return -1;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Children).GetEnumerator();
        }

    } // End class

} // End namespace