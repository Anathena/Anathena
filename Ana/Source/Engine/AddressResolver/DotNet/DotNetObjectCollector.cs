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
    using Utils.Validation;

    /// <summary>
    /// Class to walk through the managed heap of a .NET process, allowing for the easy retrieval
    /// of fully labeled objects.
    /// </summary>
    internal class DotNetObjectCollector : RepeatedTask
    {
        private const Int32 InitialPollingTime = 200;
        private const Int32 PollingTime = 10000;

        private static Lazy<DotNetObjectCollector> dotNetObjectCollectorInstance = new Lazy<DotNetObjectCollector>(
            () => { return new DotNetObjectCollector(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

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

        private static String[] excludedPrefixes = new String[]
        {
             "static var"
        };

        private List<DotNetObject> objectTrees;

        /// <summary>
        /// Prevents a default instance of the <see cref="DotNetObjectCollector" /> class from being created
        /// </summary>
        private DotNetObjectCollector()
        {
        }

        public static DotNetObjectCollector GetInstance()
        {
            return dotNetObjectCollectorInstance.Value;
        }

        public List<DotNetObject> GetObjectTrees()
        {
            return this.objectTrees;
        }

        public override void Begin()
        {
            base.Begin();

            this.UpdateInterval = DotNetObjectCollector.InitialPollingTime;
        }

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

            this.objectTrees = objectTrees;
        }

        protected override void OnEnd()
        {
            base.OnEnd();
        }

        private void RecursiveBuild(IProxyService proxyService, HashSet<UInt64> visited, DotNetObject parent, UInt64 parentRef)
        {
            // Add all fields
            foreach (UInt64 fieldRef in proxyService.GetObjectFields(parentRef))
            {
                DotNetObject childObject = new DotNetObject(
                    parent,
                    parent.GetAddress().Add(proxyService.GetFieldOffset(fieldRef)).ToUInt64(),
                    Conversions.TypeCodeToType((TypeCode)proxyService.GetFieldType(fieldRef)),
                    proxyService.GetFieldName(fieldRef));
                parent.AddChild(childObject);
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
                parent.AddChild(child);
                this.RecursiveBuild(proxyService, visited, child, childObjectRef);
            }

            parent.SortChildren();
        }
    }
    //// End class
}
//// End namespace