using Anathema.Source.Engine.Processes;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anathema.Source.Engine.DotNetObjectCollector
{
    /// <summary>
    /// Class to walk through the managed heap of a .NET process, allowing for the easy retrieval
    /// of fully labeled objects.
    /// </summary>
    class DotNetObjectCollector : RepeatedTask, IProcessObserver
    {
        private static Lazy<DotNetObjectCollector> DotNetObjectCollectorInstance = new Lazy<DotNetObjectCollector>(() => { return new DotNetObjectCollector(); });
        private static String[] ExcludedNameSpaces = new String[]
        {
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            "System.", "Microsoft.", "<CppImplementationDetails>.","<CrtImplementationDetails>.",
            "Newtonsoft.", "Ionic.", "SteamWorks.",
            "Terraria.Tile", "Terraria.Item", "Terraria.UI",  "Terraria.ObjectData", "Terraria.GameContent", "Terraria.Lighting",
            "Terraria.Graphics", "Terraria.Social", "Terraria.IO", "Terraria.DataStructures"
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

            ClrHeap Heap;
            try
            {
                DataTarget DataTarget = DataTarget.AttachToProcess(EngineCore.Memory.GetProcess().Id, AttachTimeout, AttachFlag.Passive);
                ClrInfo Version = DataTarget.ClrVersions[0]; // TODO: Handle case where multiple CLR versions may be loaded
                ClrRuntime Runtime = Version.CreateRuntime();
                Heap = Runtime.GetHeap();
            }
            catch
            {
                return;
            }

            if (Heap == null)
                return;

            List<DotNetObject> ObjectTrees = new List<DotNetObject>();
            HashSet<UInt64> Visited = new HashSet<UInt64>();

            try
            {
                foreach (ClrRoot Root in Heap.EnumerateRoots())
                {
                    // Ignore root system namespaces
                    if (Root == null || Root.Type == null || Root.Name == null)
                        continue;

                    if (ExcludedNameSpaces.Any(X => Root.Name.StartsWith(X, StringComparison.OrdinalIgnoreCase)))
                        continue;

                    if (ExcludedNameSpaces.Any(X => Root.Type.Name.StartsWith(X, StringComparison.OrdinalIgnoreCase)))
                        continue;

                    if (Visited.Contains(Root.Object))
                        continue;

                    try
                    {
                        DotNetObject RootObject = new DotNetObject(null, Root.Object, Root.Type?.ToString());
                        Visited.Add(Root.Object);
                        ObjectTrees.Add(RootObject);

                        RecursiveBuild(Heap, Visited, RootObject, Root.Object);
                    }
                    catch { }
                }
            }
            catch { }

            this.ObjectTrees = ObjectTrees;
        }

        private void RecursiveBuild(ClrHeap Heap, HashSet<UInt64> Visited, DotNetObject Parent, UInt64 ParentRef)
        {
            // Add all fields
            foreach (ClrField Field in Heap.GetObjectType(ParentRef).Fields)
            {
                DotNetObject ChildObject = new DotNetObject(Parent, Parent.GetAddress().Add(Field.Offset + 4).ToUInt64(), Field?.Name);
                Parent.AddChild(ChildObject);
            }

            // Add all nested objects recursively
            Heap?.GetObjectType(ParentRef)?.EnumerateRefsOfObject(ParentRef, delegate (UInt64 ChildObjectRef, Int32 Offset)
            {
                if (ChildObjectRef == 0 || Visited.Contains(ChildObjectRef))
                    return;

                Visited.Add(ChildObjectRef);

                ClrType Type = Heap.GetObjectType(ChildObjectRef);

                if (Type == null || ExcludedNameSpaces.Any(X => Type.Name.StartsWith(X, StringComparison.OrdinalIgnoreCase)))
                    return;

                DotNetObject Child = new DotNetObject(Parent, ChildObjectRef, Type.Name);
                Parent.AddChild(Child);
                RecursiveBuild(Heap, Visited, Child, ChildObjectRef);
            });

            Parent.SortChildren();
        }

        protected override void End() { }

    } // End class

} // End namespace