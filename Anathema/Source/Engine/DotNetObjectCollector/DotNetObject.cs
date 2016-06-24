using Anathema.Source.Utils.Extensions;
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

        public DotNetObject(DotNetObject Parent, UInt64 ObjectReference, String Name)
        {
            this.Parent = Parent;
            this.ObjectReference = ObjectReference;

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