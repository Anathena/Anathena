using Anathema.Source.Utils;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Engine.DotNetObjectCollector
{
    class DotNetNameResolver : RepeatedTask
    {
        private static Lazy<DotNetNameResolver> DotNetNameResolverInstance = new Lazy<DotNetNameResolver>(() => { return new DotNetNameResolver(); });

        private const Int32 ResolveInterval = 5000;

        private Dictionary<String, DotNetObject> NameMap;

        private DotNetNameResolver()
        {
            NameMap = new Dictionary<String, DotNetObject>();

            this.Begin();
        }

        public static DotNetNameResolver GetInstance()
        {
            return DotNetNameResolverInstance.Value;
        }

        public IntPtr ResolveName(String Name)
        {
            DotNetObject Result;

            if (NameMap.TryGetValue(Name, out Result))
                return Result.GetAddress();

            return IntPtr.Zero;
        }

        public override void Begin()
        {
            base.Begin();

            this.UpdateInterval = ResolveInterval;
        }

        protected override void Update()
        {
            Dictionary<String, DotNetObject> NameMap = new Dictionary<String, DotNetObject>();
            List<DotNetObject> ObjectTrees = DotNetObjectCollector.GetInstance().GetObjectTrees();

            ObjectTrees.ForEach(X => BuildNameMap(NameMap, X));

            this.NameMap = NameMap;
        }

        private void BuildNameMap(Dictionary<String, DotNetObject> NameMap, DotNetObject CurrentObject)
        {
            NameMap.Add(CurrentObject.GetFullName(), CurrentObject);
            CurrentObject.GetChildren().ForEach(X => BuildNameMap(NameMap, X));
        }

        protected override void End() { }

    } // End class

} // End namespace