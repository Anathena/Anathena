using Anathema.Source.Engine.Processes;
using Anathema.Source.Engine.Proxy;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.Validation;
using AnathenaProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Anathema.Source.Engine.AddressResolver.DotNet
{
    /// <summary>
    /// Class to walk through the managed heap of a .NET process, allowing for the easy retrieval
    /// of fully labeled objects.
    /// </summary>
    class DotNetObjectCollector : RepeatedTask, IProcessObserver
    {
        // Singleton instance of the .NET Object Collector
        private static Lazy<DotNetObjectCollector> DotNetObjectCollectorInstance = new Lazy<DotNetObjectCollector>(() => { return new DotNetObjectCollector(); }, LazyThreadSafetyMode.PublicationOnly);

        private static String[] ExcludedNameSpaces = new String[]
        {
            Assembly.GetExecutingAssembly().GetName().Name,
            "finalization handle", "strong handle", "pinned handle", "RefCount handle", "local var",
            "System.", "Microsoft.", "<CppImplementationDetails>.","<CrtImplementationDetails>.",
            "Newtonsoft.", "Ionic.", "SteamWorks.",
            "Terraria.Tile", "Terraria.Item", "Terraria.UI",  "Terraria.ObjectData", "Terraria.GameContent", "Terraria.Lighting",
            "Terraria.Graphics", "Terraria.Social", "Terraria.IO", "Terraria.DataStructures"
        };

        private static String[] ExcludedPrefixes = new String[]
        {
             "static var"
        };

        private const Int32 AttachTimeout = 5000;
        private const Int32 InitialPollingTime = 200;
        private const Int32 PollingTime = 10000;

        private EngineCore EngineCore;

        private List<DotNetObject> ObjectTrees;

        private DotNetObjectCollector()
        {
            InitializeProcessObserver();

            this.Begin();
        }

        public static DotNetObjectCollector GetInstance()
        {
            return DotNetObjectCollectorInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;

            Initialize();
        }

        public List<DotNetObject> GetObjectTrees()
        {
            return ObjectTrees;
        }

        private void Initialize()
        {
            if (EngineCore == null || EngineCore.Memory.GetProcess() == null)
                return;
        }

        public override void Begin()
        {
            base.Begin();

            this.UpdateInterval = InitialPollingTime;
        }

        protected override void Update()
        {
            if (EngineCore == null || EngineCore.Memory.GetProcess() == null)
                return;

            this.UpdateInterval = PollingTime;

            ProxyCommunicator ProxyCommunicator = ProxyCommunicator.GetInstance();
            IProxyService ProxyService = ProxyCommunicator.GetProxyService(EngineCore.Memory.IsProcess32Bit());

            if (ProxyService == null)
                return;

            if (!ProxyService.RefreshHeap(EngineCore.Memory.GetProcess().Id))
                return;

            List<DotNetObject> ObjectTrees = new List<DotNetObject>();
            HashSet<UInt64> Visited = new HashSet<UInt64>();

            try
            {
                foreach (UInt64 RootRef in ProxyService.GetRoots())
                {
                    String RootName = ProxyService.GetRootName(RootRef);
                    Type RootType = Conversions.TypeCodeToType((TypeCode)ProxyService.GetRootType(RootRef));

                    if (RootRef == 0 || RootName == null)
                        continue;

                    foreach (String ExcludedPrefix in ExcludedPrefixes)
                    {
                        if (RootName.StartsWith(ExcludedPrefix, StringComparison.OrdinalIgnoreCase))
                            RootName = RootName.Substring(ExcludedPrefix.Length, RootName.Length - ExcludedPrefix.Length).Trim();
                    }

                    if (ExcludedNameSpaces.Any(X => RootName.StartsWith(X, StringComparison.OrdinalIgnoreCase)))
                        continue;

                    if (RootType != null)
                    {
                        if (ExcludedNameSpaces.Any(X => RootType.Name.StartsWith(X, StringComparison.OrdinalIgnoreCase)))
                            continue;
                    }

                    if (Visited.Contains(RootRef))
                        continue;

                    try
                    {
                        DotNetObject RootObject = new DotNetObject(null, RootRef, RootType, RootName);
                        Visited.Add(RootRef);
                        ObjectTrees.Add(RootObject);

                        RecursiveBuild(ProxyService, Visited, RootObject, RootRef);
                    }
                    catch { }
                }
            }
            catch { }

            this.ObjectTrees = ObjectTrees;
        }

        private void RecursiveBuild(IProxyService ProxyService, HashSet<UInt64> Visited, DotNetObject Parent, UInt64 ParentRef)
        {
            // Add all fields
            foreach (UInt64 FieldRef in ProxyService.GetObjectFields(ParentRef))
            {
                DotNetObject ChildObject = new DotNetObject(Parent, Parent.GetAddress().Add(ProxyService.GetFieldOffset(FieldRef)).ToUInt64(),
                    Conversions.TypeCodeToType((TypeCode)ProxyService.GetFieldType(FieldRef)), ProxyService.GetFieldName(FieldRef));
                Parent.AddChild(ChildObject);
            }

            // Add all nested objects recursively
            foreach (UInt64 ChildObjectRef in ProxyService.GetObjectChildren(ParentRef))
            {
                if (ChildObjectRef == 0 || Visited.Contains(ChildObjectRef))
                    return;

                Visited.Add(ChildObjectRef);

                Type Type = Conversions.TypeCodeToType((TypeCode)ProxyService.GetObjectType(ChildObjectRef));

                if (Type == null || ExcludedNameSpaces.Any(X => Type.Name.StartsWith(X, StringComparison.OrdinalIgnoreCase)))
                    return;

                DotNetObject Child = new DotNetObject(Parent, ChildObjectRef, Type, Type.Name);
                Parent.AddChild(Child);
                RecursiveBuild(ProxyService, Visited, Child, ChildObjectRef);
            }

            Parent.SortChildren();
        }

        protected override void End()
        {
            base.End();
        }

    } // End class

} // End namespace