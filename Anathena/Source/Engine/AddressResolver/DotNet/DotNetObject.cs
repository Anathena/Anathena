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
        [Browsable(false)]
        private DotNetObject Parent { get; set; }

        [Browsable(false)]
        private List<DotNetObject> Children { get; set; }

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

        public DotNetObject(DotNetObject Parent, UInt64 ObjectReference, Type ElementType, String Name)
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