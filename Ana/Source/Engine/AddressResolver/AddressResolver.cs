﻿namespace Ana.Source.Engine.AddressResolver
{
    using DotNet;
    using OperatingSystems;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Utils;
    using Utils.Extensions;
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

        /// <summary>
        /// Gets or sets the mapping of object identifiers to their object
        /// </summary>
        private Dictionary<String, DotNetObject> DotNetNameMap { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="AddressResolver"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
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

            IEnumerable<NormalizedModule> modules = EngineCore.GetInstance().OperatingSystemAdapter.GetModules()
                .Select(x => x)?.Where(x => x.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase));

            if (modules.Count() > 0)
            {
                result = modules.First().BaseAddress;
            }

            return result;
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
        public override void Begin()
        {
            base.Begin();

            this.UpdateInterval = AddressResolver.ResolveIntervalInitial;
        }

        /// <summary>
        /// Polls the external process, gathering object information from the managed heap.
        /// </summary>
        protected override void OnUpdate()
        {
            Dictionary<String, DotNetObject> nameMap = new Dictionary<String, DotNetObject>();
            List<DotNetObject> objectTrees = DotNetObjectCollector.GetInstance().GetObjectTrees();

            // Build .NET object list
            objectTrees?.ForEach(x => this.BuildNameMap(nameMap, x));
            this.DotNetNameMap = nameMap;

            // After we have successfully grabbed information from the process, slow the update interval
            if (objectTrees != null)
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