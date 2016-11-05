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
        /// Time in ms of how often to poll and resolve addresses initially
        /// </summary>
        private const Int32 ResolveIntervalInitial = 200;

        /// <summary>
        /// Time in ms of how often to poll and re-resolve addresses
        /// </summary>
        private const Int32 ResolveInterval = 5000;

        /// <summary>
        /// Singleton instance of the <see cref="AddressResolver" /> class
        /// </summary>
        private static Lazy<AddressResolver> addressResolverInstance = new Lazy<AddressResolver>(
            () => { return new AddressResolver(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="AddressResolver" /> class from being created
        /// </summary>
        private AddressResolver()
        {
            this.DotNetNameMap = new Dictionary<String, DotNetObject>();
            this.Modules = new List<NormalizedModule>();
            this.Begin();
        }

        /// <summary>
        /// The managed language to be used when resolving the provided object
        /// </summary>
        public enum ResolveTypeEnum
        {
            /// <summary>
            /// A standard module in a native program
            /// </summary>
            Module,

            /// <summary>
            /// A .Net object
            /// </summary>
            DotNet,

            /// <summary>
            /// A Java object
            /// </summary>
            //// Java
        }

        private Dictionary<String, DotNetObject> DotNetNameMap { get; set; }

        private IEnumerable<NormalizedModule> Modules { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="AddressResolver"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static AddressResolver GetInstance()
        {
            return AddressResolver.addressResolverInstance.Value;
        }

        public IntPtr ResolveDotNetObject(String identifier)
        {
            IntPtr result = IntPtr.Zero;
            DotNetObject dotNetObject;

            if (identifier == null)
            {
                return result;
            }

            if (this.DotNetNameMap.TryGetValue(identifier, out dotNetObject))
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

        protected override void OnUpdate()
        {
            Dictionary<String, DotNetObject> nameMap = new Dictionary<String, DotNetObject>();
            List<DotNetObject> objectTrees = new List<DotNetObject>(); // DotNetObjectCollector.GetInstance().GetObjectTrees();

            // Build module list
            this.Modules = EngineCore.GetInstance().OperatingSystemAdapter.GetModules();

            // Build .NET object list
            objectTrees?.ForEach(x => this.BuildNameMap(nameMap, x));
            this.DotNetNameMap = nameMap;

            // After we have successfully grabbed information from the process, slow the update interval
            if ((this.Modules != null && this.Modules.Count() != 0) || objectTrees != null)
            {
                this.UpdateInterval = ResolveInterval;
            }
        }

        /// <summary>
        /// Called when the repeated task completes
        /// </summary>
        protected override void OnEnd()
        {
            base.OnEnd();
        }

        private void BuildNameMap(Dictionary<String, DotNetObject> nameMap, DotNetObject currentObject)
        {
            if (currentObject == null || currentObject.GetFullName() == null)
            {
                return;
            }

            nameMap[currentObject.GetFullName()] = currentObject;
            currentObject?.GetChildren()?.ForEach(x => this.BuildNameMap(nameMap, x));
        }
    }
    //// End class
}
//// End namespace