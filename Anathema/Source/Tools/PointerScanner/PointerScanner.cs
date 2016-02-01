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

        // As far as I can tell, no valid pointers will end up being less than 0x10000 (UInt16.MaxValue). Huge gains by filtering these.
        private const UInt64 InvalidPointerMin = unchecked((UInt64)Int64.MaxValue); // !TODO RemotePtr.MaxValue
        private const UInt64 InvalidPointerMax = UInt16.MaxValue;

        private List<RemoteModule> Modules;
        private List<RemoteRegion> AcceptedBases;

        private ConcurrentDictionary<UInt64, UInt64> PointerPool;

        // User parameters
        private UInt64 TargetAddress;
        private Int32 MaxPointerLevel;
        private UInt64 MaxPointerOffset;

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

        public override void SetTargetAddress(UInt64 Address)
        {
            TargetAddress = Address;
        }

        public override void SetMaxPointerLevel(Int32 MaxPointerLevel)
        {
            this.MaxPointerLevel = MaxPointerLevel;
        }

        public override void SetMaxPointerOffset(UInt64 MaxPointerOffset)
        {
            this.MaxPointerOffset = MaxPointerOffset;
        }

        private SnapshotRegion AddressToRegion(UInt64 Address)
        {
            return new SnapshotRegion<Null>(new RemoteRegion(null, unchecked((IntPtr)(Address - MaxPointerOffset)), unchecked((Int32)MaxPointerOffset * 2)));
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
                try { Region.ReadAllSnapshotMemory(Snapshot.GetMemoryEditor(), true); }
                catch (ScanFailedException) { return; }

                if (!Region.HasValues())
                    return;

                foreach (SnapshotElement Element in Region)
                {
                    if (Element.LessThanValue(InvalidPointerMax))
                        continue;

                    if (Element.GreaterThanValue(InvalidPointerMin))
                        continue;

                    if (unchecked((UInt64)Element.BaseAddress) % 4 != 0)
                        continue;

                    if (Element.GetValue() % 4 != 0)
                        continue;

                    if (Snapshot.ContainsAddress(Element.GetValue()))
                        PointerPool[unchecked((UInt64)Element.BaseAddress)] = unchecked((UInt64)Element.GetValue());
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
            Trace();
            Retrace();
        }

        private void SetAcceptedBases()
        {
            if (MemoryEditor == null)
                return;

            Modules = MemoryEditor.Modules.RemoteModules.ToList();

            // Gather regions from every module as valid base addresses
            Modules.ForEach(x => AcceptedBases.Add(new SnapshotRegion<Null>(new RemoteRegion(MemoryEditor, x.BaseAddress, x.Size))));
        }

        private void Trace()
        {
            List<SnapshotRegion> TargetRegions = new List<SnapshotRegion>();
            TargetRegions.Add(AddressToRegion(TargetAddress));
            
            ConcurrentDictionary<UInt64, UInt64> AcceptedPointers = new ConcurrentDictionary<UInt64, UInt64>();

            for (Int32 Level = 0; Level < MaxPointerLevel; Level++)
            {
                ConcurrentDictionary<UInt64, UInt64> LevelPointers = new ConcurrentDictionary<UInt64, UInt64>();

                // Create snapshot from target regions to leverage the internal merging and sorting
                Snapshot<Null> TargetSnapshot = new Snapshot<Null>(TargetRegions.ToArray());

                foreach (KeyValuePair<UInt64, UInt64> Pointer in PointerPool)
                {
                    // Accept this pointer if it is contained in the target snapshot
                    if (TargetSnapshot.ContainsAddress(Pointer.Value))
                        LevelPointers[Pointer.Key] = Pointer.Value;
                }

                // Add the pointers for this level to the global accepted list
                List<KeyValuePair<UInt64, UInt64>> LevelPointerList = LevelPointers.ToList();
                LevelPointerList.ForEach(x => AcceptedPointers[x.Key] = x.Value);

                TargetRegions.Clear();

                // Construct new target region list from this level of pointers
                foreach (KeyValuePair<UInt64, UInt64> Pointer in LevelPointerList)
                    TargetRegions.Add(AddressToRegion(Pointer.Key));
            }
        }

        private void Retrace()
        {
            return;
            List<SnapshotRegion> TargetRegions = new List<SnapshotRegion>();
            TargetRegions.Add(AddressToRegion(TargetAddress));

            // Create snapshot from target regions to leverage the merging and sorting capabilities of a snapshot
            Snapshot<Null> TargetSnapshot = new Snapshot<Null>(TargetRegions.ToArray());

            ConcurrentDictionary<UInt64, UInt64> AcceptedPointers = new ConcurrentDictionary<UInt64, UInt64>();

            for (Int32 Level = 0; Level < MaxPointerLevel; Level++)
            {
                ConcurrentDictionary<UInt64, UInt64> LevelPointers = new ConcurrentDictionary<UInt64, UInt64>();

                foreach (KeyValuePair<UInt64, UInt64> Pointer in PointerPool)
                {
                    // Accept this pointer if it is contained in the target snapshot
                    if (TargetSnapshot.ContainsAddress(Pointer.Value))
                        LevelPointers[Pointer.Key] = Pointer.Value;
                }

                // Add the pointers for this level to the global accepted list
                List<KeyValuePair<UInt64, UInt64>> LevelPointerList = LevelPointers.ToList();
                LevelPointerList.ForEach(x => AcceptedPointers[x.Key] = x.Value);

                TargetRegions.Clear();

                // Construct new target region list from this level of pointers
                foreach (KeyValuePair<UInt64, UInt64> Pointer in LevelPointerList)
                    TargetRegions.Add(AddressToRegion(Pointer.Key));
            }
        }

    } // End class

} // End namespace