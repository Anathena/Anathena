using Anathena.Source.Project.PropertyView.TypeConverters;
using Anathena.Source.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Anathena.Source.Engine.AddressResolver.DotNet
{
    public class DotNetObject : IEnumerable, IComparable<DotNetObject>
    {
        [ReadOnly(true)]
        [TypeConverter(typeof(AddressConverter))]
        [Category("Properties"), DisplayName("Address"), Description("Address of the object. The CLR can change this at any time")]
        public UInt64 ObjectReference { get; set; }

        [ReadOnly(true)]
        [Category("Properties"), DisplayName("Name"), Description("Name of the .NET object")]
        public String Name { get; set; }

        [ReadOnly(true)]
        [TypeConverter(typeof(ValueTypeConverter))]
        [Category("Properties"), DisplayName("Value Type"), Description("Data type of the address")]
        public Type ElementType { get; set; }

        private DotNetObject parent { get; set; }
        private List<DotNetObject> children { get; set; }

        public DotNetObject(DotNetObject parent, UInt64 objectReference, Type elementType, String name)
        {
            this.parent = parent;
            this.ObjectReference = unchecked(objectReference);
            this.ElementType = elementType;

            // Trim root name from all objects if applicable
            String rootName = GetRootName();
            if (name.StartsWith(rootName))
                name = name.Remove(0, rootName.Length);

            this.Name = name;

            children = new List<DotNetObject>();
        }

        public void AddChild(DotNetObject objectNode)
        {
            if (!children.Contains(objectNode))
                children.Add(objectNode);
        }

        public List<DotNetObject> GetChildren()
        {
            return children;
        }

        public void SortChildren()
        {
            children.Sort();
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
            return ElementType == null ? typeof(Int32) : ElementType;
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
            if (parent == null)
                return GetName();

            return parent.GetRootName();
        }

        private String GetFullNamespace(String CurrentNamespace)
        {
            if (parent == null)
                return GetRootName();

            return parent.GetFullNamespace(CurrentNamespace) + "." + Name;
        }

        public Int32 CompareTo(DotNetObject other)
        {
            if (this.ObjectReference > other.ObjectReference)
                return 1;

            if (this.ObjectReference == other.ObjectReference)
                return 0;

            return -1;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)children).GetEnumerator();
        }

    } // End class

} // End namespace