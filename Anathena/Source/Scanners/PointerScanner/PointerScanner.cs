using Anathena.Source.Engine;
using Anathena.Source.Engine.AddressResolver;
using Anathena.Source.Engine.OperatingSystems;
using Anathena.Source.Engine.Processes;
using Anathena.Source.Project;
using Anathena.Source.Project.ProjectItems;
using Anathena.Source.Scanners.ScanConstraints;
using Anathena.Source.Snapshots;
using Anathena.Source.Utils.Extensions;
using Anathena.Source.Utils.Validation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Anathena.Source.Scanners.PointerScanner
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
        private EngineCore EngineCore;
        private Snapshot<Null> Snapshot;

        private Int32 StartReadIndex;
        private Int32 EndReadIndex;
        private ConcurrentDictionary<Int32, String> IndexValueMap;

        private ConcurrentDictionary<IntPtr, IntPtr> PointerPool;
        private List<ConcurrentDictionary<IntPtr, IntPtr>> ConnectedPointers;
        private Snapshot<Null> AcceptedBases;

        ScanConstraintManager ScanConstraintManager;
        private Boolean IsAddressMode;

        private List<Tuple<IntPtr, List<Int32>>> AcceptedPointers;

        // User parameters
        private Type ElementType;
        private IntPtr TargetAddress;
        private Int32 MaxPointerLevel;
        private Int32 MaxPointerOffset;

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
            PointerPool = new ConcurrentDictionary<IntPtr, IntPtr>();
            ConnectedPointers = new List<ConcurrentDictionary<IntPtr, IntPtr>>();
            ScanMode = ScanModeEnum.ReadValues;

            InitializeProcessObserver();

            Begin();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
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

        public override void SetRescanMode(Boolean IsAddressMode)
        {
            this.IsAddressMode = IsAddressMode;
        }

        public override void SetTargetAddress(IntPtr Address)
        {
            TargetAddress = Address;
        }

        public override void SetScanConstraintManager(ScanConstraintManager ScanConstraintManager)
        {
            this.ScanConstraintManager = ScanConstraintManager;
        }

        public override void SetMaxPointerLevel(Int32 MaxPointerLevel)
        {
            this.MaxPointerLevel = MaxPointerLevel;
        }

        public override void SetMaxPointerOffset(Int32 MaxPointerOffset)
        {
            this.MaxPointerOffset = MaxPointerOffset;
        }

        private SnapshotRegion AddressToRegion(IntPtr Address)
        {
            return new SnapshotRegion<Null>(new NormalizedRegion(Address.Subtract(MaxPointerOffset), MaxPointerOffset * 2));
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

                AddressItem NewPointer = new AddressItem(AcceptedPointers[Index].Item1, ElementType, "New Pointer",
                    AddressResolver.ResolveTypeEnum.Module, String.Empty, AcceptedPointers[Index].Item2, false, Value);
                ProjectExplorer.GetInstance().AddProjectItem(NewPointer);

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

        public override String GetAddressAtIndex(Int32 Index)
        {
            return Conversions.ToAddress(AcceptedPointers[Index].Item1);
        }

        public override IEnumerable<String> GetOffsetsAtIndex(Int32 Index)
        {
            List<String> Offsets = new List<String>();
            AcceptedPointers[Index].Item2.ForEach(x => Offsets.Add((x < 0 ? "-" : "") + Math.Abs(x).ToString("X")));
            return Offsets;
        }

        public override Int32 GetMaxPointerLevel()
        {
            return MaxPointerLevel;
        }

        public override Int32 GetMaxPointerOffset()
        {
            return MaxPointerOffset;
        }

        private IntPtr ResolvePointer(Tuple<IntPtr, List<Int32>> FullPointer)
        {
            IntPtr Pointer = FullPointer.Item1;
            List<Int32> Offsets = FullPointer.Item2;

            if (Offsets == null || Offsets.Count == 0)
                return Pointer;

            Boolean SuccessReading = true;

            foreach (Int32 Offset in Offsets)
            {
                Pointer = EngineCore.Memory.Read<IntPtr>(Pointer, out SuccessReading);
                Pointer = Pointer.Add(Offset);

                if (!SuccessReading)
                    break;
            }

            return Pointer;
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
                        if (AcceptedPointers == null || EngineCore == null)
                            break;

                        if (Index < 0 || Index >= AcceptedPointers.Count)
                            continue;

                        IntPtr Pointer = ResolvePointer(AcceptedPointers[Index]);

                        Boolean SuccessReading;
                        String Value = EngineCore.Memory.Read(ElementType, Pointer, out SuccessReading).ToString();

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
                    UpdateDisplay();
                    ScanMode = ScanModeEnum.ReadValues;
                    break;
            }
        }

        protected override void End()
        {
            base.End();
        }

        private void PointerRescan()
        {
            this.PrintDebugTag();

            if (IsAddressMode)
            {
                RescanAddresses();
            }
            else
            {
                RescanValues();
            }
        }

        private void RescanAddresses()
        {
            this.PrintDebugTag();

            List<Tuple<IntPtr, List<Int32>>> RetainedPointers = new List<Tuple<IntPtr, List<Int32>>>();

            foreach (Tuple<IntPtr, List<Int32>> FullPointer in AcceptedPointers)
            {
                if (ResolvePointer(FullPointer) == TargetAddress)
                    RetainedPointers.Add(FullPointer);
            }

            AcceptedPointers = RetainedPointers;
        }

        private void RescanValues()
        {
            this.PrintDebugTag();

            if (ScanConstraintManager == null || ScanConstraintManager.GetCount() <= 0)
                return;

            if (AcceptedPointers == null || AcceptedPointers.Count == 0)
                return;

            List<IntPtr> ResolvedAddresses = new List<IntPtr>();
            List<SnapshotRegion> Regions = new List<SnapshotRegion>();

            // Resolve addresses
            foreach (Tuple<IntPtr, List<Int32>> FullPointer in AcceptedPointers)
            {
                ResolvedAddresses.Add(ResolvePointer(FullPointer));
            }

            // Build regions from resolved address
            foreach (IntPtr Pointer in ResolvedAddresses)
            {
                Regions.Add(new SnapshotRegion<Null>(Pointer, Marshal.SizeOf(ScanConstraintManager.GetElementType())));
            }

            // Create a snapshot from regions
            Snapshot<Null> PointerSnapshot = new Snapshot<Null>(Regions);

            // Read the memory (collecting values)
            PointerSnapshot.ReadAllSnapshotMemory();
            PointerSnapshot.SetElementType(ScanConstraintManager.GetElementType());
            Snapshot.SetAlignment(sizeof(Int32));
            PointerSnapshot.MarkAllValid();

            if (PointerSnapshot.GetRegionCount() <= 0)
            {
                AcceptedPointers = new List<Tuple<IntPtr, List<Int32>>>();
                return;
            }

            // Note there are likely only a few regions that span <= 8 bytes, we do not need to parallelize this
            foreach (SnapshotRegion Region in PointerSnapshot)
            {
                if (!Region.HasValues())
                {
                    Region.MarkAllInvalid();
                    continue;
                }

                foreach (SnapshotElement Element in Region)
                {
                    // Enforce each value constraint on the element
                    foreach (ScanConstraint ScanConstraint in ScanConstraintManager)
                    {
                        switch (ScanConstraint.Constraint)
                        {
                            case ConstraintsEnum.Equal:
                                if (!Element.EqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.NotEqual:
                                if (!Element.NotEqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.GreaterThan:
                                if (!Element.GreaterThanValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.GreaterThanOrEqual:
                                if (!Element.GreaterThanOrEqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.LessThan:
                                if (!Element.LessThanValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.LessThanOrEqual:
                                if (!Element.LessThanOrEqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.NotScientificNotation:
                                if (Element.IsScientificNotation())
                                    Element.Valid = false;
                                break;
                            default:
                                throw new Exception("Invalid Constraint");
                        }

                    } // End foreach Constraint

                } // End foreach Element

            } // End foreach Region

            PointerSnapshot.DiscardInvalidRegions();

            List<Tuple<IntPtr, List<Int32>>> RetainedPointers = new List<Tuple<IntPtr, List<Int32>>>();

            if (PointerSnapshot.GetRegionCount() <= 0)
            {
                AcceptedPointers = RetainedPointers;
                return;
            }

            // Keep all remaining pointers
            foreach (SnapshotRegion Region in PointerSnapshot)
            {
                foreach (SnapshotElement Element in Region)
                {
                    for (Int32 AddressIndex = 0; AddressIndex < ResolvedAddresses.Count; AddressIndex++)
                    {
                        if (ResolvedAddresses[AddressIndex] != Element.BaseAddress)
                            continue;

                        RetainedPointers.Add(AcceptedPointers[AddressIndex]);
                    }
                }
            }

            AcceptedPointers = RetainedPointers;
        }

        private void SetAcceptedBases()
        {
            this.PrintDebugTag();

            if (EngineCore == null)
                return;

            IEnumerable<NormalizedModule> Modules = EngineCore.Memory.GetModules();

            List<SnapshotRegion> AcceptedBaseRegions = new List<SnapshotRegion>();

            // Gather regions from every module as valid base addresses
            Modules.ForEach(x => AcceptedBaseRegions.Add(new SnapshotRegion<Null>(new NormalizedRegion(x.BaseAddress, x.RegionSize))));

            // Convert regions into a snapshot
            AcceptedBases = new Snapshot<Null>(AcceptedBaseRegions);
        }

        private void BuildPointerPool()
        {
            this.PrintDebugTag();

            // Clear current pointer pool
            PointerPool.Clear();

            // Collect memory regions
            Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().CollectSnapshot(UseSettings: false, UsePrefilter: false));

            // Set to type of a pointer
            if (EngineCore.Memory.IsProcess32Bit())
                Snapshot.SetElementType(typeof(UInt32));
            else
                Snapshot.SetElementType(typeof(UInt64));


            // As far as I can tell, no valid pointers will end up being less than 0x10000 (UInt16.MaxValue), nor higher than usermode space.
            dynamic InvalidPointerMin = EngineCore.Memory.IsProcess32Bit() ? (Int32)UInt16.MaxValue : (Int64)UInt16.MaxValue;
            dynamic InvalidPointerMax = EngineCore.Memory.IsProcess32Bit() ? Int32.MaxValue : Int64.MaxValue;

            // Enforce 4-byte alignment of pointers
            Snapshot.SetAlignment(sizeof(Int32));

            Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
            {
                SnapshotRegion Region = (SnapshotRegion)RegionObject;
                Boolean Success;

                // Read the memory of this region
                Region.ReadAllRegionMemory(Snapshot.GetEngineCore(), out Success, true);

                if (!Success)
                    return;

                if (!Region.HasValues())
                    return;

                foreach (SnapshotElement Element in Region)
                {
                    if (Element.LessThanValue(InvalidPointerMin))
                        continue;

                    if (Element.GreaterThanValue(InvalidPointerMax))
                        continue;

                    // Enforce 4-byte alignment of destination
                    if (Element.GetValue() % 4 != 0)
                        continue;

                    IntPtr Value = new IntPtr(Element.GetValue());

                    // Check if it is possible that this pointer is valid, if so keep it
                    if (Snapshot.ContainsAddress(Value))
                        PointerPool[Element.BaseAddress] = Value;
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
            ConnectedPointers.Add(new ConcurrentDictionary<IntPtr, IntPtr>());
            ConnectedPointers.Last()[TargetAddress] = IntPtr.Zero;

            for (Int32 Level = 1; Level <= MaxPointerLevel; Level++)
            {
                // Create snapshot from previous level regions to leverage the merging and sorting capabilities of a snapshot
                Snapshot PreviousLevel = new Snapshot<Null>(PreviousLevelRegions);
                ConcurrentDictionary<IntPtr, IntPtr> LevelPointers = new ConcurrentDictionary<IntPtr, IntPtr>();

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

            ConcurrentBag<Tuple<IntPtr, List<Int32>>> DiscoveredPointers = new ConcurrentBag<Tuple<IntPtr, List<Int32>>>();

            // Iterate incrementally towards the maximum, allowing for the discovery of all pointer levels
            for (Int32 CurrentMaximum = 0; CurrentMaximum <= MaxPointerLevel; CurrentMaximum++)
            {
                Parallel.ForEach(ConnectedPointers[CurrentMaximum], (Base) =>
                {
                    // Enforce static base constraint. Maxlevel pointers were already prefitlered, but not other levels.
                    if (!AcceptedBases.ContainsAddress(Base.Key))
                        return;

                    // Recursively build the pointers
                    BuildPointers(DiscoveredPointers, CurrentMaximum, Base.Key, Base.Value, new List<Int32>());
                });
            }

            AcceptedPointers = DiscoveredPointers.ToList();
        }

        private void BuildPointers(ConcurrentBag<Tuple<IntPtr, List<Int32>>> Pointers, Int32 Level, IntPtr Base, IntPtr PointerDestination, List<Int32> Offsets)
        {
            if (Level == 0)
            {
                Pointers.Add(new Tuple<IntPtr, List<Int32>>(Base, Offsets));
                return;
            }

            Parallel.ForEach(ConnectedPointers[Level - 1], (Target) =>
            {
                if (PointerDestination.ToUInt64() < Target.Key.Subtract(MaxPointerOffset).ToUInt64())
                    return;

                if (PointerDestination.ToUInt64() > Target.Key.Add(MaxPointerOffset).ToUInt64())
                    return;

                // Valid pointer, clone our current offset stack
                List<Int32> NewOffsets = new List<Int32>(Offsets);

                // Calculate the offset for this level
                NewOffsets.Add(unchecked((Int32)(Target.Key.ToInt64() - PointerDestination.ToInt64())));

                // Recurse
                BuildPointers(Pointers, Level - 1, Base, Target.Value, NewOffsets);
            });
        }

    } // End class

} // End namespace