namespace Ana.Source.Engine.AddressResolver.DotNet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Utils.Extensions;

    /// <summary>
    /// An object representing a .Net object in an external process
    /// </summary>
    internal class DotNetObject : IEnumerable, IComparable<DotNetObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetObject" /> class
        /// </summary>
        /// <param name="parent">The parent object of this object</param>
        /// <param name="objectReference">The address of this object</param>
        /// <param name="elementType">The data type this object represents</param>
        /// <param name="name">The name of this object as a full .Net namespace path</param>
        public DotNetObject(DotNetObject parent, UInt64 objectReference, Type elementType, String name)
        {
            this.Parent = parent;
            this.ObjectReference = unchecked(objectReference);
            this.ElementType = elementType;

            // Trim root name from all objects if applicable
            String rootName = this.GetRootName();

            if (name.StartsWith(rootName))
            {
                name = name.Remove(0, rootName.Length);
            }

            this.Name = name;

            this.Children = new List<DotNetObject>();
        }

        [ReadOnly(true)]
        [Category("Properties"), DisplayName("Address"), Description("Address of the object. The CLR can change this at any time")]
        public UInt64 ObjectReference { get; set; }

        [ReadOnly(true)]
        [Category("Properties"), DisplayName("Name"), Description("Name of the .NET object")]
        public String Name { get; set; }

        [ReadOnly(true)]
        [Category("Properties"), DisplayName("Value Type"), Description("Data type of the address")]
        public Type ElementType { get; set; }

        private DotNetObject Parent { get; set; }

        private List<DotNetObject> Children { get; set; }

        public void AddChild(DotNetObject objectNode)
        {
            if (!this.Children.Contains(objectNode))
            {
                this.Children.Add(objectNode);
            }
        }

        public List<DotNetObject> GetChildren()
        {
            return this.Children;
        }

        public void SortChildren()
        {
            this.Children.Sort();
        }

        public IntPtr GetAddress()
        {
            try
            {
                return this.ObjectReference.ToIntPtr();
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        public Type GetElementType()
        {
            return this.ElementType == null ? typeof(Int32) : this.ElementType;
        }

        public String GetName()
        {
            return this.Name == null ? String.Empty : this.Name;
        }

        public String GetFullName()
        {
            return this.GetFullNamespace(String.Empty);
        }

        public Int32 CompareTo(DotNetObject other)
        {
            if (this.ObjectReference > other.ObjectReference)
            {
                return 1;
            }

            if (this.ObjectReference == other.ObjectReference)
            {
                return 0;
            }

            return -1;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)this.Children).GetEnumerator();
        }

        private String GetRootName()
        {
            if (this.Parent == null)
            {
                return this.GetName();
            }

            return this.Parent.GetRootName();
        }

        private String GetFullNamespace(String currentNamespace)
        {
            if (this.Parent == null)
            {
                return this.GetRootName();
            }

            return this.Parent.GetFullNamespace(currentNamespace) + "." + this.Name;
        }
    }
    //// End class
}
//// End namespace