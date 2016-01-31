using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Modules;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /*
    Trace-Retrace Algorithm:
    0) Potential pre-processing -- no idea how many valid pointers exist in a process, but we may be able to:
        - Store all pointers to use
        - Store all regions that contain a pointer

    1) Start with a base address. Convert this to a range that spans 1024 in each direction, add this to the target list
    2) REPEAT FOR N LEVELS:
        - Search for all pointer values that fall in in the target list
        - Convert these pointers to spanning regions, and add them to the target list, clearing the old list

    3) Retrace pointers. We will not trace pointers with invalid bases. Loop from last level to first level:
        - Compare pointer to all pointers in the previous level. Store offsets from current level to all pointers in previous level.
    */

    class PointerScanner : IPointerScannerModel
    {
        private MemorySharp MemoryEditor;
        private Snapshot<Null> Snapshot;

        public event PointerScannerEventHandler EventUpdateProcessTitle;

        private Int32 MaxPointerLevel;
        private List<RemoteModule> Modules;
        private List<RemoteRegion> AcceptedBases;

        private ConcurrentDictionary<UInt64, UInt64> PointerPool;
        private List<RemoteRegion> TargetRegions;
        // private Dictionary<Int32, List<SnapshotElement>> Pointers;

        public PointerScanner()
        {
            PointerPool = new ConcurrentDictionary<UInt64, UInt64>();
            Modules = new List<RemoteModule>();
            AcceptedBases = new List<RemoteRegion>();
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public void SetMaxPointerLevel(Int32 MaxPointerLevel)
        {
            this.MaxPointerLevel = MaxPointerLevel;
        }

        public override void SetTargetAddress(UInt64 Address)
        {
            TargetRegions.Clear();

            //TargetRegions.Add()
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            // Clear current pointer pool
            PointerPool.Clear();

            // Collect memory regions
            Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().SnapshotAllRegions());

            // Set to type of a pointer
            Snapshot.SetElementType(typeof(UInt64));

            Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
            {
                SnapshotRegion Region = (SnapshotRegion)RegionObject;

                // Read the memory of this region
                Region.ReadAllSnapshotMemory(Snapshot.GetMemoryEditor(), true);

                if (!Region.HasValues())
                    return;

                foreach (SnapshotElement Element in Region)
                {
                    foreach (RemoteRegion TargetRegion in TargetRegions)
                    {
                        // Check if outside of target bounds
                        if (Element.LessThanValue(unchecked((UInt64)TargetRegion.BaseAddress)) ||
                            Element.GreaterThanValue(unchecked((UInt64)TargetRegion.EndAddress)))
                        {
                            continue;
                        }

                        // Valid pointer -- lets keep it
                        PointerPool.TryAdd(unchecked((UInt64)Element.BaseAddress), unchecked((UInt64)Element.GetValue()));
                    }
                }

                // Clear the saved values, we do not need them now
                Region.SetCurrentValues(null);

            });

            CancelFlag = true;
        }

        public override void End()
        {
            base.End();
            
            SetAcceptedBases();
        }

        private void SetAcceptedBases()
        {
            if (MemoryEditor == null)
                return;

            Modules = MemoryEditor.Modules.RemoteModules.ToList();

            // Gather regions from every module as valid base addresses
            Modules.ForEach(x => AcceptedBases.Add(new RemoteRegion(MemoryEditor, x.BaseAddress, x.Size)));
        }

        private void Trace()
        {

        }

        private void Retrace()
        {

        }

    } // End class

} // End namespace