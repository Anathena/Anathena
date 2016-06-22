using Anathema.Source.Engine.Processes;
using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Engine.DotNetProfiler
{
    /// <summary>
    /// Class to walk through the managed heap of a .NET process, allowing for the easy retrieval
    /// of fully labeled objects.
    /// </summary>
    class DotNetProfiler : IProcessObserver
    {
        private const Int32 AttachTimeout = 2000;
        private EngineCore EngineCore;
        private ClrRuntime Runtime;

        public DotNetProfiler()
        {
            InitializeProcessObserver();
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

            DataTarget DataTarget = DataTarget.AttachToProcess(EngineCore.Memory.GetProcess().Id, AttachTimeout);
            ClrInfo Version = DataTarget?.ClrVersions[0]; // TODO: Handle case where multiple CLR versions may be loaded
            Runtime = Version.CreateRuntime();

            Update();
        }

        private void Update()
        {
            if (Runtime == null)
                return;

            ClrHeap Heap = Runtime.GetHeap();

            Stack<UInt64> Roots = new Stack<UInt64>();

            foreach (ClrRoot Root in Heap.EnumerateRoots())
            {
                // Ignore system namespaces
                if (Root.Name.StartsWith("System.") || Root.Name.StartsWith("Microsoft."))
                    continue;

                ClrType ObjectType = Heap.GetObjectType(Root.Address);
                Roots.Push(Root.Object);
            }

            foreach (ClrSegment Segment in Heap.Segments)
            {
                for (UInt64 SegmentObject = Segment.FirstObject; SegmentObject != 0; SegmentObject = Segment.NextObject(SegmentObject))
                {
                    // This gets the type of the object.
                    ClrType ObjectType = Heap.GetObjectType(SegmentObject);
                }
            }
        }

    } // End class

} // End namespace