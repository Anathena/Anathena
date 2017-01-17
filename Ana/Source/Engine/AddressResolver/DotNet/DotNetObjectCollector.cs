namespace Ana.Source.Engine.AddressResolver.DotNet
{
    using AnathenaProxy;
    using Processes;
    using Proxy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using Utils;
    using Utils.Extensions;
    using Utils.Tasks;

    /// <summary>
    /// Class to walk through the managed heap of a .NET process, allowing for the easy retrieval.
    /// of fully labeled objects.
    /// </summary>
    internal class DotNetObjectCollector : ScheduledTask
    {
        /// <summary>
        /// Duration in ms to poll the target process for .Net objects initially.
        /// </summary>
        private const Int32 InitialPollingTime = 200;

        /// <summary>
        /// Duration in ms to poll the target process for .Net objects after the initial polling.
        /// </summary>
        private const Int32 PollingTime = 10000;

        /// <summary>
        /// Singleton instance of the <see cref="DotNetObjectCollector" /> class.
        /// </summary>
        private static Lazy<DotNetObjectCollector> dotNetObjectCollectorInstance = new Lazy<DotNetObjectCollector>(
            () => { return new DotNetObjectCollector(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Namespaces excluded from being collected in the external process.
        /// </summary>
        private static String[] excludedNameSpaces = new String[]
        {
            Assembly.GetExecutingAssembly().GetName().Name,
            "finalization handle",
            "strong handle",
            "pinned handle",
            "RefCount handle",
            "local var",
            "System.",
            "Microsoft.",
            "<CppImplementationDetails>.",
            "<CrtImplementationDetails>.",
            "Newtonsoft.",
            "Ionic.",
            "SteamWorks.",
            "Terraria.Tile",
            "Terraria.Item",
            "Terraria.UI",
            "Terraria.ObjectData",
            "Terraria.GameContent",
            "Terraria.Lighting",
            "Terraria.Graphics",
            "Terraria.Social",
            "Terraria.IO",
            "Terraria.DataStructures"
        };

        /// <summary>
        /// Prefixes that are trimmed from the root name string.
        /// </summary>
        private static String[] excludedPrefixes = new String[]
        {
             "static var"
        };

        /// <summary>
        /// Prevents a default instance of the <see cref="DotNetObjectCollector" /> class from being created.
        /// </summary>
        private DotNetObjectCollector() : base(isRepeated: true)
        {
        }

        /// <summary>
        /// Gets a collection of all .Net object heirarchies in an external process.
        /// </summary>
        public List<DotNetObject> ObjectTrees { get; private set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="DotNetObjectCollector"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static DotNetObjectCollector GetInstance()
        {
            return dotNetObjectCollectorInstance.Value;
        }

        /// <summary>
        /// Called before the collection of .Net objects.
        /// </summary>
        protected override void OnBegin()
        {
            this.UpdateInterval = DotNetObjectCollector.InitialPollingTime;
        }

        /// <summary>
        /// Collects .Net objects in the external process.
        /// </summary>
        protected override void OnUpdate()
        {
            this.UpdateInterval = DotNetObjectCollector.PollingTime;

            ProxyCommunicator proxyCommunicator = ProxyCommunicator.GetInstance();
            NormalizedProcess process = EngineCore.GetInstance()?.Processes?.GetOpenedProcess();
            IProxyService proxyService = proxyCommunicator.GetProxyService(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit());

            if (proxyService == null)
            {
                return;
            }

            if (process == null || !proxyService.RefreshHeap(process.ProcessId))
            {
                return;
            }

            List<DotNetObject> objectTrees = new List<DotNetObject>();
            HashSet<UInt64> visited = new HashSet<UInt64>();

            try
            {
                foreach (UInt64 rootRef in proxyService.GetRoots())
                {
                    String rootName = proxyService.GetRootName(rootRef);
                    Type rootType = Conversions.TypeCodeToType((TypeCode)proxyService.GetRootType(rootRef));

                    if (rootRef == 0 || rootName == null)
                    {
                        continue;
                    }

                    foreach (String excludedPrefix in excludedPrefixes)
                    {
                        if (rootName.StartsWith(excludedPrefix, StringComparison.OrdinalIgnoreCase))
                        {
                            rootName = rootName.Substring(excludedPrefix.Length, rootName.Length - excludedPrefix.Length).Trim();
                        }
                    }

                    if (excludedNameSpaces.Any(x => rootName.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    if (rootType != null)
                    {
                        if (excludedNameSpaces.Any(x => rootType.Name.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }
                    }

                    if (visited.Contains(rootRef))
                    {
                        continue;
                    }

                    try
                    {
                        DotNetObject rootObject = new DotNetObject(null, rootRef, rootType, rootName);
                        visited.Add(rootRef);
                        objectTrees.Add(rootObject);

                        this.RecursiveBuild(proxyService, visited, rootObject, rootRef);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }

            this.ObjectTrees = objectTrees;
        }

        /// <summary>
        /// Recursively build the heirarchy of .Net objects in the external process.
        /// </summary>
        /// <param name="proxyService">The proxy service collecting the .Net objects.</param>
        /// <param name="visited">A set of visited object addresses.</param>
        /// <param name="parent">The parent of the current object.</param>
        /// <param name="parentRef">The address of the parent of the current object.</param>
        private void RecursiveBuild(IProxyService proxyService, HashSet<UInt64> visited, DotNetObject parent, UInt64 parentRef)
        {
            // Add all fields
            foreach (UInt64 fieldRef in proxyService.GetObjectFields(parentRef))
            {
                DotNetObject childObject = new DotNetObject(
                    parent,
                    parent.ObjectReference.ToIntPtr().Add(proxyService.GetFieldOffset(fieldRef)).ToUInt64(),
                    Conversions.TypeCodeToType((TypeCode)proxyService.GetFieldType(fieldRef)),
                    proxyService.GetFieldName(fieldRef));
                parent.Children.Add(childObject);
            }

            // Add all nested objects recursively
            foreach (UInt64 childObjectRef in proxyService.GetObjectChildren(parentRef))
            {
                if (childObjectRef == 0 || visited.Contains(childObjectRef))
                {
                    return;
                }

                visited.Add(childObjectRef);

                Type type = Conversions.TypeCodeToType((TypeCode)proxyService.GetObjectType(childObjectRef));

                if (type == null || excludedNameSpaces.Any(X => type.Name.StartsWith(X, StringComparison.OrdinalIgnoreCase)))
                {
                    return;
                }

                DotNetObject child = new DotNetObject(parent, childObjectRef, type, type.Name);
                parent.Children.Add(child);
                this.RecursiveBuild(proxyService, visited, child, childObjectRef);
            }

            parent.Children.Sort();
        }
    }
    //// End class
}
//// End namespace