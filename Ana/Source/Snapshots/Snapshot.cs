using Ana.Source.Utils;
using Ana.Source.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ana.Source.Snapshots
{
    /// <summary>
    /// Defines data contained in a single snapshot
    /// </summary>
    abstract class Snapshot : IEnumerable
    {
        protected IEnumerable<SnapshotRegion> SnapshotRegions;
        protected List<SnapshotRegion> DeallocatedRegions;
        protected Object DeallocatedRegionLock;

        protected Type ElementType; // Type to consider each element of this snapshot
        protected Int32 Alignment;  // Memory alignment constraint
        private String ScanMethod;  // String indicating most recent scan method used
        private DateTime TimeStamp; // Time stamp of most recent scan for a given snapshot

        /// <summary>
        /// Indexer to allow the retrieval of the element at the specified index. Note that this does NOT index into a region.
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        [Obfuscation(Exclude = true)]
        public SnapshotElement this[Int32 Index]
        {
            get
            {
                foreach (SnapshotRegion MemoryRegion in this)
                {
                    if (Index - MemoryRegion.RegionSize / Alignment >= 0)
                        Index -= MemoryRegion.RegionSize / Alignment;
                    else
                        return MemoryRegion[Index * Alignment];
                }
                return null;
            }
        }

        public abstract void ReadAllSnapshotMemory();
        public abstract void MarkAllValid();
        public abstract void MarkAllInvalid();
        public abstract void DiscardInvalidRegions();

        public void SetScanMethod(String ScanMethod)
        {
            this.ScanMethod = ScanMethod;
        }

        public String GetScanMethod()
        {
            return ScanMethod;
        }

        public IEnumerable<SnapshotRegion> GetSnapshotRegions()
        {
            return SnapshotRegions;
        }

        public void SetTimeStampToNow()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime GetTimeStamp()
        {
            return TimeStamp;
        }

        public Type GetElementType()
        {
            return ElementType;
        }

        public Int32 GetRegionCount()
        {
            return SnapshotRegions == null ? 0 : SnapshotRegions.Count();
        }

        public UInt64 GetElementCount()
        {
            return SnapshotRegions == null ? 0 : (UInt64)SnapshotRegions.AsEnumerable().Sum(x => (Int64)(x.RegionSize / Alignment));
        }

        public UInt64 GetMemorySize()
        {
            return SnapshotRegions == null ? 0 : (UInt64)SnapshotRegions.AsEnumerable().Sum(x => (Int64)x.RegionSize / Alignment + (Int64)x.GetRegionExtension());
        }

        /// <summary>
        /// Sets the underlying data type of the element to an arbitrary data type of the specified size
        /// </summary>
        /// <param name="VariableSize"></param>
        public void SetVariableSize(Int32 VariableSize)
        {
            switch (VariableSize)
            {
                case sizeof(SByte): SetElementType(typeof(SByte)); break;
                case sizeof(Int16): SetElementType(typeof(Int16)); break;
                case sizeof(Int32): SetElementType(typeof(Int32)); break;
                case sizeof(Int64): SetElementType(typeof(Int64)); break;
            }
        }

        /// <summary>
        /// Updates type of every element with the specified type
        /// </summary>
        /// <param name="ElementType"></param>
        public void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;

            if (SnapshotRegions == null || SnapshotRegions.Count() <= 0)
                return;

            foreach (SnapshotRegion Region in this)
                Region.SetElementType(ElementType);
        }

        public void SetAlignment(Int32 Alignment)
        {
            this.Alignment = Alignment;

            if (SnapshotRegions == null)
                return;

            foreach (SnapshotRegion Region in this)
                Region.SetAlignment(Alignment);
        }

        public Boolean ContainsAddress(IntPtr Address)
        {
            if (SnapshotRegions == null || SnapshotRegions.Count() == 0)
                return false;

            return ContainsAddress(Address, SnapshotRegions.Count() / 2, 0, SnapshotRegions.Count());
        }

        private Boolean ContainsAddress(IntPtr Address, Int32 Middle, Int32 Min, Int32 Max)
        {
            if (Middle < 0 || Middle == SnapshotRegions.Count() || Max < Min)
                return false;

            if (Address.ToUInt64() < SnapshotRegions.ElementAt(Middle).BaseAddress.ToUInt64())
                return (ContainsAddress(Address, (Min + Middle - 1) / 2, Min, Middle - 1));
            else if (Address.ToUInt64() > SnapshotRegions.ElementAt(Middle).EndAddress.ToUInt64())
                return (ContainsAddress(Address, (Middle + 1 + Max) / 2, Middle + 1, Max));
            else
                return true;
        }

        public IEnumerator GetEnumerator()
        {
            return SnapshotRegions.GetEnumerator();
        }

    } // End class

    /// <summary>
    /// Empty struct for unlabeled snapshots
    /// </summary>
    struct Null { }

    /// <summary>
    /// Defines data contained in a single snapshot
    /// </summary>
    class Snapshot<LabelType> : Snapshot where LabelType : struct
    {
        #region Constructors

        /// <summary>
        /// Constructor for creating an empty snapshot
        /// </summary>
        public Snapshot()
        {
            this.SnapshotRegions = null;
            Initialize();
        }

        /// <summary>
        /// Constructor to clone a snapshot from another snapshot
        /// </summary>
        /// <param name="BaseSnapshot"></param>
        public Snapshot(Snapshot BaseSnapshot)
        {
            List<SnapshotRegion<LabelType>> Regions = new List<SnapshotRegion<LabelType>>();

            if (BaseSnapshot != null && BaseSnapshot.GetRegionCount() > 0)
            {
                foreach (SnapshotRegion Region in BaseSnapshot.GetSnapshotRegions())
                {
                    Regions.Add(new SnapshotRegion<LabelType>(Region));
                    Regions.Last().SetCurrentValues(Region.GetCurrentValues());
                }
            }

            SnapshotRegions = Regions;

            if (BaseSnapshot != null)
                SetElementType(BaseSnapshot.GetElementType());

            Initialize();
        }

        /// <summary>
        /// Constructor to create a snapshot from various regions
        /// </summary>
        /// <param name="SnapshotRegions"></param>
        public Snapshot(IEnumerable<SnapshotRegion> SnapshotRegions)
        {
            this.SnapshotRegions = SnapshotRegions == null ? null : SnapshotRegions.Select(X => (SnapshotRegion<LabelType>)X);
            Initialize();
        }

        #endregion

        #region Initialization

        public void Initialize()
        {
            this.DeallocatedRegions = new List<SnapshotRegion>();
            DeallocatedRegionLock = new Object();

            MergeRegions();
        }

        #endregion

        public override void MarkAllValid()
        {
            foreach (SnapshotRegion SnapshotRegion in this)
                SnapshotRegion.MarkAllValid();
        }

        public override void MarkAllInvalid()
        {
            foreach (SnapshotRegion SnapshotRegion in this)
                SnapshotRegion.MarkAllInvalid();
        }

        /// <summary>
        /// Expands all memory regions in both directions based on the size of the current element type.
        /// Useful for filtering methods that isolate changing bytes (ie 1 byte of an 8 byte integer), where we would want to grow to recover the other 7 bytes.
        /// </summary>
        /// <param name="GrowAmount"></param>
        public void ExpandAllRegionsOutward(Int32 ExpandSize)
        {
            foreach (SnapshotRegion SnapshotRegion in this)
                SnapshotRegion.ExpandRegion(ExpandSize);
        }

        /// <summary>
        /// Reads memory for every snapshot, with each region storing the current and previous read values.
        /// Handles ScanFailedExceptions by automatically masking deallocated regions against the current virtual memory space
        /// </summary>
        public override void ReadAllSnapshotMemory()
        {
            SetTimeStampToNow();

            if (SnapshotRegions == null || SnapshotRegions.Count() <= 0)
                return;

            // Mask this snapshot regions against active virtual pages in the target
            // TODO: Debug this shit, apparently it isn't working correctly
            // Snapshot<LabelType> Mask = new Snapshot<LabelType>(SnapshotManager.GetInstance().CollectSnapshot(UseSettings: false, UsePrefilter: false));
            // SnapshotRegions = MaskRegions(Mask, this.GetSnapshotRegions());

            Parallel.ForEach(SnapshotRegions, (SnapshotRegion) =>
            {
                Boolean Success;

                SnapshotRegion.ReadAllRegionMemory(out Success);

                if (!Success)
                {
                    using (TimedLock.Lock(DeallocatedRegionLock))
                    {
                        if (!DeallocatedRegions.Contains(SnapshotRegion))
                            DeallocatedRegions.Add(SnapshotRegion);
                    }
                }
            });

            // Mask deallocated regions
            IEnumerable<SnapshotRegion> NewRegions = MaskDeallocatedRegions();

            if (NewRegions == null || NewRegions.Count() <= 0)
                return;

            // Attempt to collect values for the recovered regions
            foreach (SnapshotRegion SnapshotRegion in NewRegions)
            {
                Boolean Success;
                SnapshotRegion.ReadAllRegionMemory(out Success);
            }
        }

        /// <summary>
        /// Discards all elements marked as invalid, and updates the snapshot regions to contain only valid regions
        /// </summary>
        public override void DiscardInvalidRegions()
        {
            List<SnapshotRegion<LabelType>> CandidateRegions = new List<SnapshotRegion<LabelType>>();

            if (SnapshotRegions == null || SnapshotRegions.Count() <= 0)
                return;

            // Collect valid element regions
            foreach (SnapshotRegion<LabelType> SnapshotRegion in this)
                CandidateRegions.AddRange(SnapshotRegion.GetValidRegions());

            // Mask the regions against the original snapshot (this snapshot)
            IEnumerable<SnapshotRegion<LabelType>> ValidRegions = MaskRegions(this, CandidateRegions);

            // Shrink the regions based on their element type
            if (ValidRegions != null && ValidRegions.Count() > 0)
            {
                foreach (SnapshotRegion<LabelType> Region in ValidRegions)
                    Region.RelaxRegion();
            }

            this.SnapshotRegions = ValidRegions;
        }

        /// <summary>
        /// Masks the given memory regions against the memory regions of a given snapshot, keeping the common elements of the two. O(n).
        /// </summary>
        /// <param name="Mask"></param>
        public IEnumerable<SnapshotRegion<LabelType>> MaskRegions(Snapshot<LabelType> Mask, IEnumerable<SnapshotRegion> TargetRegions)
        {
            List<SnapshotRegion<LabelType>> ResultRegions = new List<SnapshotRegion<LabelType>>();

            // Initialize stacks with regions and masking regions
            Queue<SnapshotRegion<LabelType>> CandidateRegions = new Queue<SnapshotRegion<LabelType>>();
            Queue<SnapshotRegion<LabelType>> MaskingRegions = new Queue<SnapshotRegion<LabelType>>();

            if (TargetRegions == null || TargetRegions.Count() < 0)
                return null;

            if (Mask == null || Mask.GetRegionCount() <= 0)
                return null;

            // Build candidate region queue from target region array
            foreach (SnapshotRegion<LabelType> Region in TargetRegions.OrderBy(X => X.BaseAddress.ToUInt64()))
                CandidateRegions.Enqueue(Region);

            // Build masking region queue from snapshot
            foreach (SnapshotRegion<LabelType> MaskRegion in Mask.GetSnapshotRegions().OrderBy(X => X.BaseAddress.ToUInt64()))
                MaskingRegions.Enqueue(MaskRegion);

            if (CandidateRegions.Count <= 0 || MaskingRegions.Count <= 0)
                return null;

            SnapshotRegion<LabelType> CurrentRegion;
            SnapshotRegion<LabelType> CurrentMask = MaskingRegions.Dequeue();

            while (CandidateRegions.Count > 0)
            {
                // Grab next region
                CurrentRegion = CandidateRegions.Dequeue();

                // Grab the next mask following the current region
                while (CurrentMask.EndAddress.ToUInt64() < CurrentRegion.BaseAddress.ToUInt64() && MaskingRegions.Count > 0)
                    CurrentMask = MaskingRegions.Dequeue();

                // Check for mask completely removing this region
                if (CurrentMask.EndAddress.ToUInt64() < CurrentRegion.BaseAddress.ToUInt64() || CurrentMask.BaseAddress.ToUInt64() > CurrentRegion.EndAddress.ToUInt64())
                    continue;

                // Mask completely overlaps, just use the original region
                if (CurrentMask.BaseAddress == CurrentRegion.BaseAddress && CurrentMask.EndAddress == CurrentRegion.EndAddress)
                {
                    ResultRegions.Add(CurrentRegion);
                    continue;
                }

                // Mask is within bounds; Grab the masked portion of this region
                Int32 BaseOffset = 0;
                if (CurrentMask.BaseAddress.ToUInt64() > CurrentRegion.BaseAddress.ToUInt64())
                    BaseOffset = CurrentMask.BaseAddress.Subtract(CurrentRegion.BaseAddress).ToInt32();

                SnapshotRegion<LabelType> NewRegion = new SnapshotRegion<LabelType>(CurrentRegion);
                NewRegion.BaseAddress = CurrentRegion.BaseAddress + BaseOffset;
                NewRegion.EndAddress = Math.Min(CurrentMask.EndAddress.ToUInt64(), CurrentRegion.EndAddress.ToUInt64()).ToIntPtr();
                NewRegion.SetCurrentValues(CurrentRegion.GetCurrentValues().LargestSubArray(BaseOffset, NewRegion.RegionSize + NewRegion.GetRegionExtension()));
                NewRegion.SetPreviousValues(CurrentRegion.GetPreviousValues().LargestSubArray(BaseOffset, NewRegion.RegionSize + NewRegion.GetRegionExtension()));
                NewRegion.SetElementLabels(CurrentRegion.GetElementLabels().LargestSubArray(BaseOffset, NewRegion.RegionSize + NewRegion.GetRegionExtension()));
                NewRegion.SetElementType(CurrentRegion.GetElementType());
                NewRegion.SetAlignment(CurrentRegion.GetAlignment());
                ResultRegions.Add(NewRegion);
            }

            return ResultRegions.Count == 0 ? null : ResultRegions;
        }

        public void ClearSnapshotRegions()
        {
            this.SnapshotRegions = null;
        }

        public void AddSnapshotRegions(IEnumerable<SnapshotRegion<LabelType>> SnapshotRegions)
        {
            List<SnapshotRegion<LabelType>> NewRegions = this.SnapshotRegions == null ? new List<SnapshotRegion<LabelType>>() : ((IEnumerable<SnapshotRegion<LabelType>>)this.SnapshotRegions).ToList();
            NewRegions.AddRange(SnapshotRegions);
            this.SnapshotRegions = NewRegions.ToArray();
            Array.Sort((SnapshotRegion<LabelType>[])this.SnapshotRegions, (X, Y) => (X.BaseAddress.ToUInt64()).CompareTo(Y.BaseAddress.ToUInt64()));

            MergeRegions();

            if (this.ContainsAddress(new IntPtr(0x00805468)))
            {
                int i = 0;
                i++;
            }
        }

        /// <summary>
        /// Merges regions in the current list of memory regions using a fast stack based algorithm O(nlogn + n)
        /// </summary>
        private void MergeRegions()
        {
            if (this.SnapshotRegions == null)
                return;

            SnapshotRegion<LabelType>[] SnapshotRegionArray = ((IEnumerable<SnapshotRegion<LabelType>>)this.SnapshotRegions).ToArray();

            if (SnapshotRegionArray == null || SnapshotRegionArray.Length <= 0)
                return;

            // First, sort by start address
            Array.Sort(SnapshotRegionArray, (X, Y) => (X.BaseAddress.ToUInt64()).CompareTo(Y.BaseAddress.ToUInt64()));

            // Create and initialize the stack with the first region
            Stack<SnapshotRegion<LabelType>> CombinedRegions = new Stack<SnapshotRegion<LabelType>>();
            CombinedRegions.Push(SnapshotRegionArray[0]);

            // Build the remaining regions
            for (Int32 Index = CombinedRegions.Count; Index < SnapshotRegionArray.Length; Index++)
            {
                SnapshotRegion<LabelType> Top = CombinedRegions.Peek();

                // If the interval does not overlap, put it on the top of the stack
                if (Top.EndAddress.ToUInt64() < SnapshotRegionArray[Index].BaseAddress.ToUInt64())
                {
                    CombinedRegions.Push(SnapshotRegionArray[Index]);
                }
                // The regions are adjacent; merge them
                else if (Top.EndAddress.ToUInt64() == SnapshotRegionArray[Index].BaseAddress.ToUInt64())
                {
                    Top.RegionSize = SnapshotRegionArray[Index].EndAddress.Subtract(Top.BaseAddress).ToInt32();
                    Top.SetElementLabels(Top.GetElementLabels().Concat(SnapshotRegionArray[Index].GetElementLabels()));
                }
                // The regions overlap.
                else if (Top.EndAddress.ToUInt64() <= SnapshotRegionArray[Index].EndAddress.ToUInt64())
                {
                    Top.RegionSize = SnapshotRegionArray[Index].EndAddress.Subtract(Top.BaseAddress).ToInt32();
                }
            }

            // Replace memory regions with merged memory regions
            this.SnapshotRegions = CombinedRegions.ToArray();
            Array.Sort((SnapshotRegion<LabelType>[])this.SnapshotRegions, (X, Y) => (X.BaseAddress.ToUInt64()).CompareTo(Y.BaseAddress.ToUInt64()));
        }

        /// <summary>
        /// Removes deallocated regions and recovers the remaining regions
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SnapshotRegion<LabelType>> MaskDeallocatedRegions()
        {
            if (DeallocatedRegions == null || DeallocatedRegions.Count <= 0 || SnapshotRegions == null || SnapshotRegions.Count() <= 0)
                return null;

            List<SnapshotRegion<LabelType>> NewSnapshotRegions = SnapshotRegions.Select(X => (SnapshotRegion<LabelType>)X).ToList();

            // Remove invalid items from collection
            foreach (SnapshotRegion<LabelType> Region in DeallocatedRegions)
                NewSnapshotRegions.Remove(Region);

            // Get current memory regions
            Snapshot<LabelType> Mask = new Snapshot<LabelType>(SnapshotManager.GetInstance().CollectSnapshot(useSettings: false, usePrefilter: false));

            // Mask each region against the current virtual memory regions
            IEnumerable<SnapshotRegion<LabelType>> MaskedRegions = MaskRegions(Mask, DeallocatedRegions);

            // Merge split regions back with the main list
            if (MaskedRegions != null && MaskedRegions.Count() > 0)
                NewSnapshotRegions.AddRange(MaskedRegions);

            // Clear invalid items
            DeallocatedRegions = new List<SnapshotRegion>();

            // Store result as main snapshot array
            this.SnapshotRegions = NewSnapshotRegions;

            return MaskedRegions;
        }

        public void SetElementLabels(LabelType Value)
        {
            foreach (SnapshotRegion<LabelType> Region in this)
                Region.SetElementLabels(Value);
        }

    } // End class

} // End namespace