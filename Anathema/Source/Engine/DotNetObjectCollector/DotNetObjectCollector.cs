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

        private const Int32 AttachTimeout = 2000;
        private const Int32 RescanTime = 4000;

        private EngineCore EngineCore;
        private ClrHeap Heap;

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

        private void Initialize()
        {
            if (EngineCore == null || EngineCore.Memory.GetProcess() == null)
                return;

            try
            {
                DataTarget DataTarget = DataTarget.AttachToProcess(EngineCore.Memory.GetProcess().Id, AttachTimeout);
                ClrInfo Version = DataTarget?.ClrVersions[0]; // TODO: Handle case where multiple CLR versions may be loaded
                ClrRuntime Runtime = Version.CreateRuntime();
                Heap = Runtime.GetHeap();
            }
            catch
            {

            }
        }

        public override void Begin()
        {
            base.Begin();

            this.UpdateInterval = RescanTime;
        }

        protected override void Update()
        {
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

                        AddChildren(Visited, RootObject, Root.Object);
                    }
                    catch { }
                }
            }
            catch { }

            this.ObjectTrees = ObjectTrees;
        }
        private void AddChildren(HashSet<UInt64> Visited, DotNetObject Parent, UInt64 ParentRef)
        {
            ClrType Type = Heap.GetObjectType(ParentRef);

            if (Type == null)
                return;

            Type.EnumerateRefsOfObject(ParentRef, delegate (UInt64 ChildObjectRef, Int32 Offset)
            {
                if (ChildObjectRef == 0 || Visited.Contains(ChildObjectRef))
                    return;

                DotNetObject Child = new DotNetObject(ChildObjectRef, Heap.GetObjectType(ChildObjectRef)?.ToString());
                Visited.Add(ChildObjectRef);
                Parent.AddChild(Child);
                AddChildren(Visited, Child, ChildObjectRef);
            });
        }

        protected override void End() { }

    } // End class

} // End namespace