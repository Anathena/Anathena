using Anathena.Source.Engine.Processes;
using Anathena.Source.Engine.Proxy;
using Anathena.Source.Utils;
using Anathena.Source.Utils.Extensions;
using Anathena.Source.Utils.Validation;
using AnathenaProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Anathena.Source.Engine.AddressResolver.DotNet
{
    /// <summary>
    /// Class to walk through the managed heap of a .NET process, allowing for the easy retrieval
    /// of fully labeled objects.
    /// </summary>
    class DotNetObjectCollector : RepeatedTask, IProcessObserver
    {
        // Singleton instance of the .NET Object Collector
        private static Lazy<DotNetObjectCollector> dotNetObjectCollectorInstance = new Lazy<DotNetObjectCollector>(() => { return new DotNetObjectCollector(); }, LazyThreadSafetyMode.PublicationOnly);

        private static String[] excludedNameSpaces = new String[]
        {
            Assembly.GetExecutingAssembly().GetName().Name,
            "finalization handle", "strong handle", "pinned handle", "RefCount handle", "local var",
            "System.", "Microsoft.", "<CppImplementationDetails>.","<CrtImplementationDetails>.",
            "Newtonsoft.", "Ionic.", "SteamWorks.",
            "Terraria.Tile", "Terraria.Item", "Terraria.UI",  "Terraria.ObjectData", "Terraria.GameContent", "Terraria.Lighting",
            "Terraria.Graphics", "Terraria.Social", "Terraria.IO", "Terraria.DataStructures"
        };

        private static String[] excludedPrefixes = new String[]
        {
             "static var"
        };

        private const Int32 attachTimeout = 5000;
        private const Int32 initialPollingTime = 200;
        private const Int32 pollingTime = 10000;

        private EngineCore engineCore;

        private List<DotNetObject> objectTrees;

        private DotNetObjectCollector()
        {
            InitializeProcessObserver();

            this.Begin();
        }

        public static DotNetObjectCollector GetInstance()
        {
            return dotNetObjectCollectorInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.engineCore = EngineCore;

            Initialize();
        }

        public List<DotNetObject> GetObjectTrees()
        {
            return objectTrees;
        }

        private void Initialize()
        {
            if (engineCore == null || engineCore.Memory.GetProcess() == null)
                return;
        }

        public override void Begin()
        {
            base.Begin();

            this.UpdateInterval = initialPollingTime;
        }

        protected override void Update()
        {
            if (engineCore == null || engineCore.Memory.GetProcess() == null)
                return;

            this.UpdateInterval = pollingTime;

            ProxyCommunicator proxyCommunicator = ProxyCommunicator.GetInstance();
            IProxyService proxyService = proxyCommunicator.GetProxyService(engineCore.Memory.IsProcess32Bit());

            if (proxyService == null)
                return;

            if (!proxyService.RefreshHeap(engineCore.Memory.GetProcess().Id))
                return;

            List<DotNetObject> objectTrees = new List<DotNetObject>();
            HashSet<UInt64> visited = new HashSet<UInt64>();

            try
            {
                foreach (UInt64 rootRef in proxyService.GetRoots())
                {
                    String rootName = proxyService.GetRootName(rootRef);
                    Type rootType = Conversions.TypeCodeToType((TypeCode)proxyService.GetRootType(rootRef));

                    if (rootRef == 0 || rootName == null)
                        continue;

                    foreach (String ExcludedPrefix in excludedPrefixes)
                    {
                        if (rootName.StartsWith(ExcludedPrefix, StringComparison.OrdinalIgnoreCase))
                            rootName = rootName.Substring(ExcludedPrefix.Length, rootName.Length - ExcludedPrefix.Length).Trim();
                    }

                    if (excludedNameSpaces.Any(x => rootName.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
                        continue;

                    if (rootType != null)
                    {
                        if (excludedNameSpaces.Any(x => rootType.Name.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
                            continue;
                    }

                    if (visited.Contains(rootRef))
                        continue;

                    try
                    {
                        DotNetObject rootObject = new DotNetObject(null, rootRef, rootType, rootName);
                        visited.Add(rootRef);
                        objectTrees.Add(rootObject);

                        RecursiveBuild(proxyService, visited, rootObject, rootRef);
                    }
                    catch { }
                }
            }
            catch { }

            this.objectTrees = objectTrees;
        }

        private void RecursiveBuild(IProxyService proxyService, HashSet<UInt64> visited, DotNetObject parent, UInt64 parentRef)
        {
            // Add all fields
            foreach (UInt64 FieldRef in proxyService.GetObjectFields(parentRef))
            {
                DotNetObject childObject = new DotNetObject(parent, parent.GetAddress().Add(proxyService.GetFieldOffset(FieldRef)).ToUInt64(),
                    Conversions.TypeCodeToType((TypeCode)proxyService.GetFieldType(FieldRef)), proxyService.GetFieldName(FieldRef));
                parent.AddChild(childObject);
            }

            // Add all nested objects recursively
            foreach (UInt64 childObjectRef in proxyService.GetObjectChildren(parentRef))
            {
                if (childObjectRef == 0 || visited.Contains(childObjectRef))
                    return;

                visited.Add(childObjectRef);

                Type type = Conversions.TypeCodeToType((TypeCode)proxyService.GetObjectType(childObjectRef));

                if (type == null || excludedNameSpaces.Any(X => type.Name.StartsWith(X, StringComparison.OrdinalIgnoreCase)))
                    return;

                DotNetObject child = new DotNetObject(parent, childObjectRef, type, type.Name);
                parent.AddChild(child);
                RecursiveBuild(proxyService, visited, child, childObjectRef);
            }

            parent.SortChildren();
        }

        protected override void End()
        {
            base.End();
        }

    }
    // End class
}
// End namespace