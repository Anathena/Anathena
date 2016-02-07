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
        private MemoryEditor MemoryEditor;
        private Snapshot<Null> Snapshot;

        // As far as I can tell, no valid pointers will end up being less than 0x10000 (UInt16.MaxValue). Huge gains by filtering these.
        private const UInt64 InvalidPointerMin = unchecked((UInt64)Int64.MaxValue); // !TODO RemotePtr.MaxValue
        private const UInt64 InvalidPointerMax = UInt16.MaxValue;

        private Int32 StartReadIndex;
        private Int32 EndReadIndex;
        private ConcurrentDictionary<Int32, String> IndexValueMap;

        private ConcurrentDictionary<UInt64, UInt64> PointerPool;
        private List<ConcurrentDictionary<UInt64, UInt64>> ConnectedPointers;
        private Snapshot<Null> AcceptedBases;


        private List<Tuple<UInt64, List<Int32>>> AcceptedPointers;

        // User parameters
        private Type ElementType;
        private UInt64 TargetAddress;
        private Int32 MaxPointerLevel;
        private UInt64 MaxPointerOffset;

        private enum ScanModeEnum
        {
            ReadValues,
            Scan,
            Rescan
        }

        private ScanModeEnum ScanMode;

        public PointerScanner()
        {
            IndexValueMap = new ConcurrentDictionary<Int32, String>();
            PointerPool = new ConcurrentDictionary<UInt64, UInt64>();
            ConnectedPointers = new List<ConcurrentDictionary<UInt64, UInt64>>();
            ScanMode = ScanModeEnum.ReadValues;

            InitializeProcessObserver();

            Begin();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemoryEditor MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }
        
        public override void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            this.StartReadIndex = StartReadIndex;
            this.EndReadIndex = EndReadIndex;
        }

        public override void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;
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

        private void UpdateDisplay()
        {
            PointerScannerEventArgs Args = new PointerScannerEventArgs();
            Args.ItemCount = AcceptedPointers.Count;
            Args.MaxPointerLevel = MaxPointerLevel;
            OnEventScanFinished(Args);
        }

        public override void AddSelectionToTable(Int32 MinIndex, Int32 MaxIndex)
        {
            const Int32 MaxAdd = 4096;

            if (MinIndex < 0)
                MinIndex = 0;

            if (MaxIndex > AcceptedPointers.Count)
                MaxIndex = AcceptedPointers.Count;

            Int32 Count = 0;
            for (Int32 Index = MinIndex; Index <= MaxIndex; Index++)
            {
                String Value = String.Empty;
                IndexValueMap.TryGetValue(Index, out Value);

                AddressTable.GetInstance().AddTableItem(AcceptedPointers[Index].Item1, ElementType, "Pointer", AcceptedPointers[Index].Item2.ToArray(), Value: Value);

                if (++Count >= MaxAdd)
                    break;
            }
        }

        public override String GetValueAtIndex(Int32 Index)
        {
            if (IndexValueMap.ContainsKey(Index))
                return IndexValueMap[Index];

            return "-";
        }

        public override String GetBaseAddress(Int32 Index)
        {
            return Conversions.ToAddress(AcceptedPointers[Index].Item1);
        }

        public override String[] GetOffsets(Int32 Index)
        {
            List<String> Offsets = new List<String>();
            AcceptedPointers[Index].Item2.ForEach(x => Offsets.Add((x < 0 ? "-" : "") + Math.Abs(x).ToString("X")));
            return Offsets.ToArray();
        }

        public override void Begin()
        {
            base.Begin();
        }

        public override void BeginPointerScan()
        {
            ScanMode = ScanModeEnum.Scan;
        }

        public override void BeginPointerRescan()
        {
            ScanMode = ScanModeEnum.Rescan;
        }

        protected override void Update()
        {
            base.Update();

            // Scan mode determines the action to make, such that the action always happens on this task thread
            switch (ScanMode)
            {
                case ScanModeEnum.ReadValues:

                    for (Int32 Index = StartReadIndex; Index <= EndReadIndex; Index++)
                    {
                        if (AcceptedPointers == null || MemoryEditor == null)
                            break;

                        if (Index < 0 || Index >= AcceptedPointers.Count)
                            continue;

                        Tuple<UInt64, List<Int32>> FullPointer = AcceptedPointers[Index];
                        UInt64 Pointer = FullPointer.Item1;
                        List<Int32> Offsets = FullPointer.Item2;

                        Boolean SuccessReading = true;

                        foreach (Int32 Offset in Offsets)
                        {
                            Pointer = MemoryEditor.Read<UInt64>((IntPtr)Pointer, out SuccessReading);
                            Pointer += (UInt64)Offset;

                            if (!SuccessReading)
                                break;
                        }
                        
                        String Value = MemoryEditor.Read(ElementType, (IntPtr)Pointer, out SuccessReading).ToString();

                        IndexValueMap[Index] = Value;
                    }

                    OnEventReadValues(new PointerScannerEventArgs());
                    break;
                case ScanModeEnum.Scan:

                    BuildPointerPool();
                    TracePointers();
                    BuildPointers();
                    UpdateDisplay();

                    ScanMode = ScanModeEnum.ReadValues;
                    break;
                case ScanModeEnum.Rescan:
                    PointerRescan();
                    ScanMode = ScanModeEnum.ReadValues;
                    break;
            }
        }

        public override void End()
        {
            base.End();
        }

        private void PointerRescan()
        {
            this.PrintDebugTag();
        }

        private void SetAcceptedBases()
        {
            this.PrintDebugTag();

            if (MemoryEditor == null)
                return;

            List<RemoteModule> Modules = MemoryEditor.Modules.RemoteModules.ToList();
            // List<RemoteModule> Modules = new List<RemoteModule>();
            // Modules.Add(MemoryEditor.Modules.MainModule);

            List<SnapshotRegion> AcceptedBaseRegions = new List<SnapshotRegion>();

            // Gather regions from every module as valid base addresses
            Modules.ForEach(x => AcceptedBaseRegions.Add(new SnapshotRegion<Null>(new RemoteRegion(MemoryEditor, x.BaseAddress, x.Size))));

            // Convert regions into a snapshot
            AcceptedBases = new Snapshot<Null>(AcceptedBaseRegions.ToArray());
        }

        private void BuildPointerPool()
        {
            this.PrintDebugTag();

            // Clear current pointer pool
            PointerPool.Clear();

            // Collect memory regions
            Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().SnapshotAllRegions(true));

            // Enforce 4-byte alignment of pointers
            Snapshot.SetAlignment(sizeof(Int32));

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

                    // Enforce 4-byte alignment of destination
                    if (Element.GetValue() % 4 != 0)
                        continue;

                    // Check if it is possible that this pointer is valid, if so keep it
                    if (Snapshot.ContainsAddress(Element.GetValue()))
                        PointerPool[unchecked((UInt64)Element.BaseAddress)] = unchecked((UInt64)Element.GetValue());
                }

                // Clear the saved values, we do not need them now
                Region.SetCurrentValues(null);
            });
        }

        private void TracePointers()
        {
            this.PrintDebugTag();

            ConcurrentBag<SnapshotRegion> PreviousLevelRegions = new ConcurrentBag<SnapshotRegion>();
            PreviousLevelRegions.Add(AddressToRegion(TargetAddress));

            ConnectedPointers.Clear();
            SetAcceptedBases();

            // Add the address we are looking for as the base
            ConnectedPointers.Add(new ConcurrentDictionary<UInt64, UInt64>());
            ConnectedPointers.Last()[TargetAddress] = 0;

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
                ConnectedPointers.Add(LevelPointers);

                PreviousLevelRegions = new ConcurrentBag<SnapshotRegion>();

                // Construct new target region list from this level of pointers
                Parallel.ForEach(LevelPointers, (Pointer) =>
                {
                    PreviousLevelRegions.Add(AddressToRegion(Pointer.Key));
                });
            }

            PointerPool.Clear();
        }

        private void BuildPointers()
        {
            this.PrintDebugTag();

            ConcurrentBag<Tuple<UInt64, List<Int32>>> DiscoveredPointers = new ConcurrentBag<Tuple<UInt64, List<Int32>>>();

            Parallel.ForEach(ConnectedPointers[MaxPointerLevel], (Base) =>
            {
                BuildPointers(DiscoveredPointers, MaxPointerLevel, Base.Key, Base.Value, new List<Int32>());
            });

            AcceptedPointers = DiscoveredPointers.ToList();
        }

        private void BuildPointers(ConcurrentBag<Tuple<UInt64, List<Int32>>> Pointers, Int32 Level, UInt64 Base, UInt64 PointerDestination, List<Int32> Offsets)
        {
            if (Level == 0)
            {
                Pointers.Add(new Tuple<UInt64, List<Int32>>(Base, Offsets));
                return;
            }

            Parallel.ForEach(ConnectedPointers[Level - 1], (Target) =>
            {
                //foreach (KeyValuePair<UInt64, UInt64> Target in ConnectedPointers[Level - 1])
                if (PointerDestination < unchecked(Target.Key - MaxPointerOffset))
                    return;

                if (PointerDestination > unchecked(Target.Key + MaxPointerOffset))
                    return;

                // Valid pointer, clone our current offset stack
                List<Int32> NewOffsets = new List<Int32>(Offsets);

                // Calculate the offset for this level
                NewOffsets.Add(unchecked((Int32)((Int64)Target.Key - (Int64)PointerDestination)));

                // Recurse
                BuildPointers(Pointers, Level - 1, Base, Target.Value, NewOffsets);
            });
        }

    } // End class

} // End namespace