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

    class PointerScanner : IPointerScannerModel, IProcessObserver
    {
        private MemorySharp MemoryEditor;
        private Snapshot<Null> Snapshot;

        // As far as I can tell, no valid pointers will end up being less than 0x10000 (UInt16.MaxValue). Huge gains by filtering these.
        private const UInt64 InvalidPointerMin = unchecked((UInt64)Int64.MaxValue); // !TODO RemotePtr.MaxValue
        private const UInt64 InvalidPointerMax = UInt16.MaxValue;


        private ConcurrentDictionary<UInt64, UInt64> PointerPool;
        private List<ConcurrentDictionary<UInt64, UInt64>> AcceptedPointers;
        private Snapshot<Null> AcceptedBases;

        // User parameters
        private UInt64 TargetAddress;
        private Int32 MaxPointerLevel;
        private UInt64 MaxPointerOffset;

        public enum UpdateModeEnum
        {
            ReadValues,
            Scan,
            Rescan
        }

        private UpdateModeEnum UpdateMode;

        public PointerScanner()
        {
            PointerPool = new ConcurrentDictionary<UInt64, UInt64>();
            AcceptedPointers = new List<ConcurrentDictionary<UInt64, UInt64>>();
            UpdateMode = UpdateModeEnum.ReadValues;

            InitializeProcessObserver();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
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

            switch(UpdateMode)
            {
                case UpdateModeEnum.ReadValues:
                    // Request display refresh pointers
                    OnEventReadValues(new PointerScannerEventArgs());
                    break;
                case UpdateModeEnum.Scan:
                    // Scan initiated
                    CollectPointers();
                    UpdateMode = UpdateModeEnum.ReadValues;
                    break;
                case UpdateModeEnum.Rescan:
                    // Rescan initiated
                    RebuildPointers();
                    UpdateMode = UpdateModeEnum.ReadValues;
                    break;

            }
        }

        private void CollectPointers()
        {
            // Clear current pointer pool
            PointerPool.Clear();

            // Collect memory regions
            Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().SnapshotAllRegions(true));

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
        }

        public override void End()
        {
            base.End();

            TracePointers();
            BuildPointers();

            PointerScannerEventArgs Args = new PointerScannerEventArgs();
            Args.ItemCount = AcceptedPointers.Count;
            OnEventUpdateItemCount(Args);
        }

        private void SetAcceptedBases()
        {
            if (MemoryEditor == null)
                return;

            //List<RemoteModule> Modules = MemoryEditor.Modules.RemoteModules.ToList();
            List<RemoteModule> Modules = new List<RemoteModule>();
            Modules.Add(MemoryEditor.Modules.MainModule);

            List<SnapshotRegion> AcceptedBaseRegions = new List<SnapshotRegion>();

            // Gather regions from every module as valid base addresses
            Modules.ForEach(x => AcceptedBaseRegions.Add(new SnapshotRegion<Null>(new RemoteRegion(MemoryEditor, x.BaseAddress, x.Size))));

            // Convert regions into a snapshot
            AcceptedBases = new Snapshot<Null>(AcceptedBaseRegions.ToArray());
        }

        private void TracePointers()
        {
            List<SnapshotRegion> PreviousLevelRegions = new List<SnapshotRegion>();
            PreviousLevelRegions.Add(AddressToRegion(TargetAddress));

            AcceptedPointers.Clear();
            SetAcceptedBases();

            // Add the address we are looking for as the base
            AcceptedPointers.Add(new ConcurrentDictionary<UInt64, UInt64>());
            AcceptedPointers.Last()[TargetAddress] = 0;

            for (Int32 Level = 1; Level <= MaxPointerLevel; Level++)
            {
                // Create snapshot from previous level regions to leverage the merging and sorting capabilities of a snapshot
                Snapshot PreviousLevel = new Snapshot<Null>(PreviousLevelRegions.ToArray());
                ConcurrentDictionary<UInt64, UInt64> LevelPointers = new ConcurrentDictionary<UInt64, UInt64>();

                Parallel.ForEach(PointerPool, (Pointer) =>
                {
                    // Ensure if this is a max level pointer that it is from an acceptable base address (ie static)
                    if (Level == MaxPointerLevel && !AcceptedBases.ContainsAddress(Pointer.Key))
                        return;

                    // Accept this pointer if it is points to the previous level snapshot
                    if (PreviousLevel.ContainsAddress(Pointer.Value))
                        LevelPointers[Pointer.Key] = Pointer.Value;
                });

                // Add the pointers for this level to the global accepted list
                AcceptedPointers.Add(LevelPointers);

                PreviousLevelRegions.Clear();

                // Construct new target region list from this level of pointers
                foreach (KeyValuePair<UInt64, UInt64> Pointer in LevelPointers)
                    PreviousLevelRegions.Add(AddressToRegion(Pointer.Key));
            }

            PointerPool.Clear();
        }

        private void RebuildPointers()
        {
            ///
            /// Reread values
            ///

            BuildPointers();
        }

        private void BuildPointers()
        {
            List<Tuple<UInt64, Stack<Int32>>> Pointers = new List<Tuple<UInt64, Stack<Int32>>>();

            foreach (KeyValuePair<UInt64, UInt64> Base in AcceptedPointers[MaxPointerLevel])
                BuildPointers(Pointers, MaxPointerLevel, Base.Key, Base.Value, new Stack<Int32>());
        }

        private void BuildPointers(List<Tuple<UInt64, Stack<Int32>>> Pointers, Int32 Level, UInt64 Base, UInt64 PointerDestination, Stack<Int32> Offsets)
        {
            if (Level == 0)
            {
                Pointers.Add(new Tuple<UInt64, Stack<Int32>>(Base, Offsets));
                return;
            }

            foreach (KeyValuePair<UInt64, UInt64> Target in AcceptedPointers[Level - 1])
            {
                if (PointerDestination < unchecked(Target.Key - MaxPointerOffset))
                    continue;

                if (PointerDestination > unchecked(Target.Key + MaxPointerOffset))
                    continue;

                // Valid pointer, clone our current offset stack
                Stack<Int32> NewOffsets = new Stack<Int32>(Offsets.Reverse());

                // Calculate the offset for this level
                NewOffsets.Push(unchecked((Int32)((Int64)Target.Key - (Int64)PointerDestination)));

                // Recurse
                BuildPointers(Pointers, Level - 1, Base, Target.Value, NewOffsets);
            }
        }

    } // End class

} // End namespace