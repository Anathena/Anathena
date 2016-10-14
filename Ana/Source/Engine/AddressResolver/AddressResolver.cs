namespace Ana.Source.Engine.AddressResolver
{
    using DotNet;
    using OperatingSystems;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Utils;

    /// <summary>
    /// Singleton class to resolve the address of managed objects in an external process
    /// </summary>
    internal class AddressResolver : RepeatedTask
    {
        /// <summary>
        /// Singleton instance of the <see cref="AddressResolver" /> class
        /// </summary>
        private static Lazy<AddressResolver> addressResolverInstance = new Lazy<AddressResolver>(
            () => { return new AddressResolver(); },
            LazyThreadSafetyMode.PublicationOnly);

        private const Int32 ResolveIntervalInitial = 200;

        private const Int32 ResolveInterval = 5000;

        private Dictionary<String, DotNetObject> dotNetNameMap;

        private IEnumerable<NormalizedModule> modules;

        public enum ResolveTypeEnum
        {
            Module,
            DotNet,
            //// Java
        }

        private AddressResolver()
        {
            this.dotNetNameMap = new Dictionary<String, DotNetObject>();
            this.modules = new List<NormalizedModule>();
            this.Begin();
        }

        public static AddressResolver GetInstance()
        {
            return AddressResolver.addressResolverInstance.Value;
        }

        public IntPtr ResolveDotNetObject(String identifier)
        {
            IntPtr result = IntPtr.Zero;
            DotNetObject dotNetObject;

            if (dotNetNameMap.TryGetValue(identifier, out dotNetObject))
            {
                result = dotNetObject.GetAddress();
            }

            return result;
        }

        public override void Begin()
        {
            base.Begin();

            this.UpdateInterval = AddressResolver.ResolveIntervalInitial;
        }

        protected override void Update()
        {
            Dictionary<String, DotNetObject> nameMap = new Dictionary<String, DotNetObject>();
            List<DotNetObject> objectTrees = new List<DotNetObject>(); // DotNetObjectCollector.GetInstance().GetObjectTrees();

            // Build module list
            modules = EngineCore.GetInstance().OperatingSystemAdapter.GetModules();

            // Build .NET object list
            objectTrees?.ForEach(x => BuildNameMap(nameMap, x));
            this.dotNetNameMap = nameMap;

            // After we have successfully grabbed information from the process, slow the update interval
            if ((modules != null && modules.Count() != 0) || objectTrees != null)
            {
                this.UpdateInterval = ResolveInterval;
            }
        }

        private void BuildNameMap(Dictionary<String, DotNetObject> nameMap, DotNetObject currentObject)
        {
            if (currentObject == null || currentObject.GetFullName() == null)
            {
                return;
            }

            nameMap[currentObject.GetFullName()] = currentObject;
            currentObject?.GetChildren()?.ForEach(x => BuildNameMap(nameMap, x));
        }

        protected override void End()
        {
            base.End();
        }
    }
    //// End class
}
//// End namespace