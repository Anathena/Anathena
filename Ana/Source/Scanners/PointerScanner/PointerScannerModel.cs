namespace Ana.Source.Scanners.PointerScanner
{
    using Engine;
    using Engine.AddressResolver;
    using Engine.OperatingSystems;
    using Project;
    using Project.ProjectItems;
    using ScanConstraints;
    using Snapshots;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using UserSettings;
    using Utils.Extensions;
    using Utils.Validation;

    /// <summary>
    /// Trace-Retrace Algorithm:
    /// 0) Potential pre-processing -- no idea how many valid pointers exist in a process, but we may be able to:
    ///     - Store all pointers to use
    ///     - Store all regions that contain a pointer
    ///  1) Start with a base address. Convert this to a range that spans 1024 in each direction, add this to the target list
    ///  2) REPEAT FOR N LEVELS:
    ///     - Search for all pointer values that fall in in the target list
    ///     - Convert these pointers to spanning regions, and add them to the target list, clearing the old list
    ///  3) Retrace pointers. We will not trace pointers with invalid bases. Loop from last level to first level:
    ///    - Compare pointer to all pointers in the previous level. Store offsets from current level to all pointers in previous level.
    /// </summary>
    internal class PointerScannerModel : ScannerBase
    {
        private const Int32 MaxAdd = 4096;

        public PointerScannerModel() : base("Pointer Scanner")
        {
            this.IndexValueMap = new ConcurrentDictionary<Int32, String>();
            this.PointerPool = new ConcurrentDictionary<IntPtr, IntPtr>();
            this.ConnectedPointers = new List<ConcurrentDictionary<IntPtr, IntPtr>>();
            this.ScanMode = ScanModeEnum.ReadValues;

            this.Begin();
        }

        private enum ScanModeEnum
        {
            ReadValues,
            Scan,
            Rescan
        }

        private Snapshot Snapshot { get; set; }

        private ScanModeEnum ScanMode { get; set; }

        private Int32 StartReadIndex { get; set; }

        private Int32 EndReadIndex { get; set; }

        private ConcurrentDictionary<Int32, String> IndexValueMap { get; set; }

        private ConcurrentDictionary<IntPtr, IntPtr> PointerPool { get; set; }

        private List<ConcurrentDictionary<IntPtr, IntPtr>> ConnectedPointers { get; set; }

        private Snapshot<Null> AcceptedBases { get; set; }

        private ScanConstraintManager ScanConstraintManager { get; set; }

        private Boolean IsAddressMode { get; set; }

        private List<Tuple<IntPtr, List<Int32>>> AcceptedPointers { get; set; }

        private Type ElementType { get; set; }

        private IntPtr TargetAddress { get; set; }

        private Int32 MaxPointerLevel { get; set; }

        private Int32 MaxPointerOffset { get; set; }

        public void UpdateReadBounds(Int32 startReadIndex, Int32 endReadIndex)
        {
            this.StartReadIndex = startReadIndex;
            this.EndReadIndex = endReadIndex;
        }

        public void SetElementType(Type elementType)
        {
            this.ElementType = elementType;
        }

        public void SetRescanMode(Boolean isAddressMode)
        {
            this.IsAddressMode = isAddressMode;
        }

        public void SetTargetAddress(IntPtr address)
        {
            this.TargetAddress = address;
        }

        public void SetScanConstraintManager(ScanConstraintManager scanConstraintManager)
        {
            this.ScanConstraintManager = scanConstraintManager;
        }

        public void SetMaxPointerLevel(Int32 maxPointerLevel)
        {
            this.MaxPointerLevel = maxPointerLevel;
        }

        public void SetMaxPointerOffset(Int32 maxPointerOffset)
        {
            this.MaxPointerOffset = maxPointerOffset;
        }

        public void AddSelectionToTable(Int32 minIndex, Int32 maxIndex)
        {
            if (minIndex < 0)
            {
                minIndex = 0;
            }

            if (maxIndex > this.AcceptedPointers.Count)
            {
                maxIndex = this.AcceptedPointers.Count;
            }

            Int32 count = 0;

            for (Int32 index = minIndex; index <= maxIndex; index++)
            {
                String pointerValue = String.Empty;
                this.IndexValueMap.TryGetValue(index, out pointerValue);

                AddressItem newPointer = new AddressItem(
                    this.AcceptedPointers[index].Item1,
                    this.ElementType,
                    "New Pointer",
                    AddressResolver.ResolveTypeEnum.Module,
                    String.Empty,
                    this.AcceptedPointers[index].Item2,
                    false,
                    pointerValue);

                ProjectExplorerViewModel.GetInstance().AddNewProjectItem(newPointer);

                if (++count >= PointerScannerModel.MaxAdd)
                {
                    break;
                }
            }
        }

        public String GetValueAtIndex(Int32 index)
        {
            if (this.IndexValueMap.ContainsKey(index))
            {
                return this.IndexValueMap[index];
            }

            return "-";
        }

        public String GetAddressAtIndex(Int32 index)
        {
            return Conversions.ToAddress(this.AcceptedPointers[index].Item1);
        }

        public IEnumerable<String> GetOffsetsAtIndex(Int32 index)
        {
            List<String> offsets = new List<String>();
            this.AcceptedPointers[index].Item2.ForEach(x => offsets.Add((x < 0 ? "-" : String.Empty) + Math.Abs(x).ToString("X")));
            return offsets;
        }

        public Int32 GetMaxPointerLevel()
        {
            return this.MaxPointerLevel;
        }

        public Int32 GetMaxPointerOffset()
        {
            return this.MaxPointerOffset;
        }

        public override void Begin()
        {
            base.Begin();
        }

        public void BeginPointerScan()
        {
            this.ScanMode = ScanModeEnum.Scan;
        }

        public void BeginPointerRescan()
        {
            this.ScanMode = ScanModeEnum.Rescan;
        }

        protected override void OnUpdate()
        {
            // Scan mode determines the action to make, such that the action always happens on this task thread
            switch (this.ScanMode)
            {
                case ScanModeEnum.ReadValues:

                    for (Int32 index = this.StartReadIndex; index <= this.EndReadIndex; index++)
                    {
                        if (this.AcceptedPointers == null)
                        {
                            break;
                        }

                        if (index < 0 || index >= this.AcceptedPointers.Count)
                        {
                            continue;
                        }

                        IntPtr pointer = this.ResolvePointer(this.AcceptedPointers[index]);

                        Boolean successReading;
                        String value = EngineCore.GetInstance().OperatingSystemAdapter.Read(this.ElementType, pointer, out successReading).ToString();

                        this.IndexValueMap[index] = value;
                    }

                    break;
                case ScanModeEnum.Scan:
                    this.BuildPointerPool();
                    this.TracePointers();
                    this.BuildPointers();
                    this.ScanMode = ScanModeEnum.ReadValues;
                    break;
                case ScanModeEnum.Rescan:
                    this.PointerRescan();
                    this.ScanMode = ScanModeEnum.ReadValues;
                    break;
            }

            base.OnUpdate();
        }

        /// <summary>
        /// Called when the repeated task completes
        /// </summary>
        protected override void OnEnd()
        {
            base.OnEnd();
        }

        private IntPtr ResolvePointer(Tuple<IntPtr, List<Int32>> fullPointer)
        {
            IntPtr pointer = fullPointer.Item1;
            List<Int32> offsets = fullPointer.Item2;

            if (offsets == null || offsets.Count == 0)
            {
                return pointer;
            }

            Boolean successReading = true;

            foreach (Int32 offset in offsets)
            {
                pointer = EngineCore.GetInstance().OperatingSystemAdapter.Read<IntPtr>(pointer, out successReading);
                pointer = pointer.Add(offset);

                if (!successReading)
                {
                    break;
                }
            }

            return pointer;
        }

        private void PointerRescan()
        {
            this.PrintDebugTag();

            if (this.IsAddressMode)
            {
                this.RescanAddresses();
            }
            else
            {
                this.RescanValues();
            }
        }

        private void RescanAddresses()
        {
            this.PrintDebugTag();

            List<Tuple<IntPtr, List<Int32>>> retainedPointers = new List<Tuple<IntPtr, List<Int32>>>();

            foreach (Tuple<IntPtr, List<Int32>> fullPointer in this.AcceptedPointers)
            {
                if (this.ResolvePointer(fullPointer) == this.TargetAddress)
                {
                    retainedPointers.Add(fullPointer);
                }
            }

            this.AcceptedPointers = retainedPointers;
        }

        private void RescanValues()
        {
            this.PrintDebugTag();

            if (this.ScanConstraintManager == null || this.ScanConstraintManager.GetCount() <= 0)
            {
                return;
            }

            if (this.AcceptedPointers == null || this.AcceptedPointers.Count == 0)
            {
                return;
            }

            List<IntPtr> resolvedAddresses = new List<IntPtr>();
            List<SnapshotRegion> regions = new List<SnapshotRegion>();

            // Resolve addresses
            foreach (Tuple<IntPtr, List<Int32>> fullPointer in this.AcceptedPointers)
            {
                resolvedAddresses.Add(this.ResolvePointer(fullPointer));
            }

            // Build regions from resolved address
            foreach (IntPtr pointer in resolvedAddresses)
            {
                regions.Add(new SnapshotRegion<Null>(pointer, Marshal.SizeOf(this.ScanConstraintManager.ElementType)));
            }

            // Create a snapshot from regions
            Snapshot<Null> pointerSnapshot = new Snapshot<Null>(regions);

            // Read the memory (collecting values)
            pointerSnapshot.ReadAllSnapshotMemory();
            pointerSnapshot.ElementType = this.ScanConstraintManager.ElementType;
            this.Snapshot.Alignment = sizeof(Int32);
            pointerSnapshot.MarkAllValid();

            if (pointerSnapshot.GetRegionCount() <= 0)
            {
                this.AcceptedPointers = new List<Tuple<IntPtr, List<Int32>>>();
                return;
            }

            // Note there are likely only a few regions that span <= 8 bytes, we do not need to parallelize this
            foreach (SnapshotRegion region in pointerSnapshot)
            {
                if (!region.HasValues())
                {
                    region.MarkAllInvalid();
                    continue;
                }

                foreach (SnapshotElement element in region)
                {
                    // Enforce each value constraint on the element
                    foreach (ScanConstraint scanConstraint in this.ScanConstraintManager)
                    {
                        switch (scanConstraint.Constraint)
                        {
                            case ConstraintsEnum.Equal:
                                if (!element.EqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.NotEqual:
                                if (!element.NotEqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.GreaterThan:
                                if (!element.GreaterThanValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.GreaterThanOrEqual:
                                if (!element.GreaterThanOrEqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.LessThan:
                                if (!element.LessThanValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.LessThanOrEqual:
                                if (!element.LessThanOrEqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.NotScientificNotation:
                                if (element.IsScientificNotation())
                                {
                                    element.Valid = false;
                                }

                                break;
                            default:
                                throw new Exception("Invalid Constraint");
                        }
                    }
                    //// End foreach Constraint
                }
                //// End foreach Element
            }
            //// End foreach Region

            pointerSnapshot.DiscardInvalidRegions();

            List<Tuple<IntPtr, List<Int32>>> retainedPointers = new List<Tuple<IntPtr, List<Int32>>>();

            if (pointerSnapshot.GetRegionCount() <= 0)
            {
                this.AcceptedPointers = retainedPointers;
                return;
            }

            // Keep all remaining pointers
            foreach (SnapshotRegion region in pointerSnapshot)
            {
                foreach (SnapshotElement element in region)
                {
                    for (Int32 addressIndex = 0; addressIndex < resolvedAddresses.Count; addressIndex++)
                    {
                        if (resolvedAddresses[addressIndex] != element.BaseAddress)
                        {
                            continue;
                        }

                        retainedPointers.Add(this.AcceptedPointers[addressIndex]);
                    }
                }
            }

            this.AcceptedPointers = retainedPointers;
        }

        private void SetAcceptedBases()
        {
            this.PrintDebugTag();

            IEnumerable<NormalizedModule> modules = EngineCore.GetInstance().OperatingSystemAdapter.GetModules();
            List<SnapshotRegion> acceptedBaseRegions = new List<SnapshotRegion>();

            // Gather regions from every module as valid base addresses
            modules.ForEach(x => acceptedBaseRegions.Add(new SnapshotRegion<Null>(new NormalizedRegion(x.BaseAddress, x.RegionSize))));

            // Convert regions into a snapshot
            this.AcceptedBases = new Snapshot<Null>(acceptedBaseRegions);
        }

        private void BuildPointerPool()
        {
            this.PrintDebugTag();

            // Clear current pointer pool
            this.PointerPool.Clear();

            // Collect memory regions
            this.Snapshot = SnapshotManager.GetInstance().CollectSnapshot(useSettings: false, usePrefilter: false).Clone();

            // Set to type of a pointer
            if (EngineCore.GetInstance().Processes.IsOpenedProcess32Bit())
            {
                this.Snapshot.ElementType = typeof(UInt32);
            }
            else
            {
                this.Snapshot.ElementType = typeof(UInt64);
            }

            // As far as I can tell, no valid pointers will end up being less than 0x10000 (UInt16.MaxValue), nor higher than usermode space.
            dynamic invalidPointerMin = EngineCore.GetInstance().Processes.IsOpenedProcess32Bit() ? (Int32)UInt16.MaxValue : (Int64)UInt16.MaxValue;
            dynamic invalidPointerMax = EngineCore.GetInstance().Processes.IsOpenedProcess32Bit() ? Int32.MaxValue : Int64.MaxValue;

            // Enforce 4-byte alignment of pointers
            this.Snapshot.Alignment = sizeof(Int32);

            Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                SettingsViewModel.GetInstance().ParallelSettings,
                (regionObject) =>
            {
                SnapshotRegion region = (SnapshotRegion)regionObject;
                Boolean readSuccess;

                // Read the memory of this region
                region.ReadAllRegionMemory(out readSuccess, true);

                if (!readSuccess)
                {
                    return;
                }

                if (!region.HasValues())
                {
                    return;
                }

                foreach (SnapshotElement element in region)
                {
                    if (element.LessThanValue(invalidPointerMin))
                    {
                        continue;
                    }

                    if (element.GreaterThanValue(invalidPointerMax))
                    {
                        continue;
                    }

                    // Enforce 4-byte alignment of destination
                    if (element.GetCurrentValue() % 4 != 0)
                    {
                        continue;
                    }

                    IntPtr addressValue = new IntPtr(element.GetCurrentValue());

                    // Check if it is possible that this pointer is valid, if so keep it
                    if (Snapshot.ContainsAddress(addressValue))
                    {
                        PointerPool[element.BaseAddress] = addressValue;
                    }
                }

                // Clear the saved values, we do not need them now
                region.SetCurrentValues(null);
            });
        }

        private SnapshotRegion AddressToRegion(IntPtr address)
        {
            return new SnapshotRegion<Null>(new NormalizedRegion(address.Subtract(this.MaxPointerOffset), this.MaxPointerOffset * 2));
        }

        private void TracePointers()
        {
            this.PrintDebugTag();

            ConcurrentBag<SnapshotRegion> previousLevelRegions = new ConcurrentBag<SnapshotRegion>();
            previousLevelRegions.Add(this.AddressToRegion(this.TargetAddress));

            this.ConnectedPointers.Clear();
            this.SetAcceptedBases();

            // Add the address we are looking for as the base
            this.ConnectedPointers.Add(new ConcurrentDictionary<IntPtr, IntPtr>());
            this.ConnectedPointers.Last()[this.TargetAddress] = IntPtr.Zero;

            for (Int32 level = 1; level <= this.MaxPointerLevel; level++)
            {
                // Create snapshot from previous level regions to leverage the merging and sorting capabilities of a snapshot
                Snapshot previousLevel = new Snapshot<Null>(previousLevelRegions);
                ConcurrentDictionary<IntPtr, IntPtr> levelPointers = new ConcurrentDictionary<IntPtr, IntPtr>();

                Parallel.ForEach(
                    this.PointerPool,
                    SettingsViewModel.GetInstance().ParallelSettings,
                    (pointer) =>
                {
                    // Ensure if this is a max level pointer that it is from an acceptable base address (ie static)
                    if (level == MaxPointerLevel && !AcceptedBases.ContainsAddress(pointer.Key))
                    {
                        return;
                    }

                    // Accept this pointer if it is points to the previous level snapshot
                    if (previousLevel.ContainsAddress(pointer.Value))
                    {
                        levelPointers[pointer.Key] = pointer.Value;
                    }
                });

                // Add the pointers for this level to the global accepted list
                this.ConnectedPointers.Add(levelPointers);

                previousLevelRegions = new ConcurrentBag<SnapshotRegion>();

                // Construct new target region list from this level of pointers
                Parallel.ForEach(
                    levelPointers,
                    SettingsViewModel.GetInstance().ParallelSettings,
                    (pointer) =>
                {
                    previousLevelRegions.Add(AddressToRegion(pointer.Key));
                });
            }

            this.PointerPool.Clear();
        }

        private void BuildPointers()
        {
            this.PrintDebugTag();

            ConcurrentBag<Tuple<IntPtr, List<Int32>>> discoveredPointers = new ConcurrentBag<Tuple<IntPtr, List<Int32>>>();

            // Iterate incrementally towards the maximum, allowing for the discovery of all pointer levels
            for (Int32 currentMaximum = 0; currentMaximum <= this.MaxPointerLevel; currentMaximum++)
            {
                Parallel.ForEach(
                    this.ConnectedPointers[currentMaximum],
                    SettingsViewModel.GetInstance().ParallelSettings,
                    (baseAddress) =>
                {
                    // Enforce static base constraint. Maxlevel pointers were already prefitlered, but not other levels.
                    if (!this.AcceptedBases.ContainsAddress(baseAddress.Key))
                    {
                        return;
                    }

                    // Recursively build the pointers
                    this.BuildPointers(discoveredPointers, currentMaximum, baseAddress.Key, baseAddress.Value, new List<Int32>());
                });
            }

            this.AcceptedPointers = discoveredPointers.ToList();
        }

        private void BuildPointers(ConcurrentBag<Tuple<IntPtr, List<Int32>>> pointers, Int32 level, IntPtr baseAddress, IntPtr pointerDestination, List<Int32> offsets)
        {
            if (level == 0)
            {
                pointers.Add(new Tuple<IntPtr, List<Int32>>(baseAddress, offsets));
                return;
            }

            Parallel.ForEach(
                this.ConnectedPointers[level - 1],
                SettingsViewModel.GetInstance().ParallelSettings,
                (target) =>
            {
                if (pointerDestination.ToUInt64() < target.Key.Subtract(this.MaxPointerOffset).ToUInt64())
                {
                    return;
                }

                if (pointerDestination.ToUInt64() > target.Key.Add(this.MaxPointerOffset).ToUInt64())
                {
                    return;
                }

                // Valid pointer, clone our current offset stack
                List<Int32> newOffsets = new List<Int32>(offsets);

                // Calculate the offset for this level
                newOffsets.Add(unchecked((Int32)(target.Key.ToInt64() - pointerDestination.ToInt64())));

                // Recurse
                this.BuildPointers(pointers, level - 1, baseAddress, target.Value, newOffsets);
            });
        }
    }
    //// End class
}
//// End namespace