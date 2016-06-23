using Anathema.Source.Engine.Processes;
using Anathema.Source.Utils;
using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Engine.DotNetObjectCollector
{
    /// <summary>
    /// Class to walk through the managed heap of a .NET process, allowing for the easy retrieval
    /// of fully labeled objects.
    /// </summary>
    class DotNetObjectCollector : RepeatedTask, IProcessObserver
    {
        private static Lazy<DotNetObjectCollector> DotNetObjectCollectorInstance = new Lazy<DotNetObjectCollector>(() => { return new DotNetObjectCollector(); });

        private const Int32 AttachTimeout = 4000;
        private const Int32 RescanTime = 5000;

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

            this.UpdateInterval = RescanTime;
        }

        protected override void Update()
        {
            if (EngineCore == null || EngineCore.Memory.GetProcess() == null)
                return;

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
                    if (Root == null || Root.Name == null)
                        continue;

                    if (Root.Name.StartsWith("System.") || Root.Name.StartsWith("Microsoft."))
                        continue;

                    if (Root.Type.Name.StartsWith("System.") || Root.Type.Name.StartsWith("Microsoft."))
                        continue;

                    if (Visited.Contains(Root.Object))
                        continue;

                    try
                    {
                        DotNetObject RootObject = new DotNetObject(Root.Object, Root.Type?.ToString());
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
            unchecked
            {
                Heap?.GetObjectType(ParentRef)?.EnumerateRefsOfObject(ParentRef, delegate (UInt64 ChildObjectRef, Int32 Offset)
                {
                    if (ChildObjectRef == 0 || Visited.Contains(ChildObjectRef))
                        return;

                    DotNetObject Child = new DotNetObject(ChildObjectRef, Heap.GetObjectType(ChildObjectRef)?.ToString());
                    Visited.Add(ChildObjectRef);
                    Parent.AddChild(Child);
                    RecursiveBuild(Heap, Visited, Child, ChildObjectRef);
                });
            }
        }

        protected override void End() { }

    } // End class

} // End namespace