namespace Ana.Source.Engine.AddressResolver
{
    using ActionScheduler;
    using DotNet;
    using OperatingSystems;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Utils.Extensions;

    /// <summary>
    /// Singleton class to resolve the address of managed objects in an external process.
    /// </summary>
    internal class AddressResolver : ScheduledTask
    {
        /// <summary>
        /// Time in ms of how often to poll and resolve addresses initially.
        /// </summary>
        private const Int32 ResolveIntervalInitial = 200;

        /// <summary>
        /// Time in ms of how often to poll and re-resolve addresses.
        /// </summary>
        private const Int32 ResolveInterval = 5000;

        /// <summary>
        /// Singleton instance of the <see cref="AddressResolver" /> class.
        /// </summary>
        private static Lazy<AddressResolver> addressResolverInstance = new Lazy<AddressResolver>(
            () => { return new AddressResolver(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="AddressResolver" /> class from being created.
        /// </summary>
        private AddressResolver() : base("Address Resolver", isRepeated: true, trackProgress: false)
        {
            this.DotNetNameMap = new Dictionary<String, DotNetObject>();
        }

        /// <summary>
        /// The managed language, module, keyword, or other identifier to be used when resolving the provided object.
        /// </summary>
        public enum ResolveTypeEnum
        {
            /// <summary>
            /// A standard module in a native program.
            /// </summary>
            Module,

            /// <summary>
            /// A global keyword created by a script.
            /// </summary>
            GlobalKeyword,

            /// <summary>
            /// A .Net object.
            /// </summary>
            DotNet,

            /// <summary>
            /// A Java object.
            /// </summary>
            //// Java
        }

        /// <summary>
        /// Gets or sets the mapping of object identifiers to their object.
        /// </summary>
        private Dictionary<String, DotNetObject> DotNetNameMap { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="AddressResolver"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static AddressResolver GetInstance()
        {
            return AddressResolver.addressResolverInstance.Value;
        }

        /// <summary>
        /// Determines the base address of a module given a module name.
        /// </summary>
        /// <param name="identifier">The module identifier, or name.</param>
        /// <returns>The base address of the module.</returns>
        public IntPtr ResolveModule(String identifier)
        {
            IntPtr result = IntPtr.Zero;

            identifier = identifier?.RemoveSuffixes(".exe", ".dll");
            IEnumerable<NormalizedModule> modules = EngineCore.GetInstance().OperatingSystemAdapter.GetModules()
                .Select(x => x)?.Where(x => x.Name.RemoveSuffixes(".exe", ".dll").Equals(identifier, StringComparison.OrdinalIgnoreCase));

            if (modules.Count() > 0)
            {
                result = modules.First().BaseAddress;
            }

            return result;
        }

        /// <summary>
        /// Determines the base address of a module given a global keyword.
        /// </summary>
        /// <param name="identifier">The global keyword identifier defined by a script.</param>
        /// <returns>The base address as defined by the keyword.</returns>
        public IntPtr ResolveGlobalKeyword(String identifier)
        {
            ScriptEngine.ScriptEngine scriptEngine = new ScriptEngine.ScriptEngine();
            return scriptEngine.MemoryCore.GetGlobalKeyword(identifier)?.ToIntPtr();
        }

        /// <summary>
        /// Determines the base address of a .Net object given an object identifier.
        /// </summary>
        /// <param name="identifier">The .Net object identifier, which is the full namespace path to the object.</param>
        /// <returns>The base address of the .Net object.</returns>
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
                result = dotNetObject.ObjectReference.ToIntPtr();
            }

            return result;
        }

        /// <summary>
        /// Begins polling the external process for information needed to resolve addresses.
        /// </summary>
        protected override void OnBegin()
        {
            this.UpdateInterval = AddressResolver.ResolveIntervalInitial;
        }

        /// <summary>
        /// Polls the external process, gathering object information from the managed heap.
        /// </summary>
        protected override void OnUpdate()
        {
            Dictionary<String, DotNetObject> nameMap = new Dictionary<String, DotNetObject>();
            List<DotNetObject> objectTrees = DotNetObjectCollector.GetInstance().ObjectTrees;

            // Build .NET object list
            objectTrees?.ForEach(x => this.BuildNameMap(nameMap, x));
            this.DotNetNameMap = nameMap;

            // After we have successfully grabbed information from the process, slow the update interval
            if (objectTrees != null)
            {
                this.UpdateInterval = AddressResolver.ResolveInterval;
            }
        }

        /// <summary>
        /// Recursively updates the name map for a given object, mapping an identifier to a .Net object.
        /// </summary>
        /// <param name="nameMap">The name map being constructed</param>
        /// <param name="currentObject">The object to add</param>
        private void BuildNameMap(Dictionary<String, DotNetObject> nameMap, DotNetObject currentObject)
        {
            if (currentObject == null || currentObject.GetFullName() == null)
            {
                return;
            }

            nameMap[currentObject.GetFullName()] = currentObject;
            currentObject?.Children?.ForEach(x => this.BuildNameMap(nameMap, x));
        }
    }
    //// End class
}
//// End namespace