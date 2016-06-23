using Anathema.Source.Utils.Extensions;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Engine.DotNetObjectCollector
{
    public class DotNetObject
    {
        private List<DotNetObject> Children;
        private UInt64 ObjectReference;
        private String ObjectType;

        public DotNetObject(UInt64 ObjectReference, String ObjectType)
        {
            this.ObjectReference = ObjectReference;
            this.ObjectType = ObjectType;

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

        public IntPtr GetObjectAddress()
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

        public String GetObjectType()
        {
            return ObjectType;
        }

    } // End class

} // End namespace