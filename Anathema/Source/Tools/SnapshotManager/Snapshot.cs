using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace Anathema
{
    /// <summary>
    /// Defines data contained in a single snapshot
    /// </summary>
    abstract class Snapshot : IProcessObserver, IEnumerable
    {
        protected const Int32 NoAlignment = 1;

        protected MemoryEditor MemoryEditor;

        protected SnapshotRegion[] SnapshotRegions;
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

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemoryEditor MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public MemoryEditor GetMemoryEditor()
        {
            return MemoryEditor;
        }

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

        public SnapshotRegion[] GetSnapshotRegions()
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
            return SnapshotRegions == null ? 0 : SnapshotRegions.Length;
        }

        public UInt64 GetElementCount()
        {
            return SnapshotRegions == null ? 0 : (UInt64)SnapshotRegions.AsEnumerable().Sum(x => (Int64)(x.RegionSize / Alignment));
        }

        public UInt64 GetMemorySize()
        {
            return SnapshotRegions == null ? 0 : (UInt64)SnapshotRegions.AsEnumerable().Sum(x => (Int64)x.RegionSize + (Int64)x.GetRegionExtension());
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

            if (SnapshotRegions == null || SnapshotRegions.Length <= 0)
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

        public Boolean ContainsAddress(UInt64 Address)
        {
            if (SnapshotRegions == null || SnapshotRegions.Length == 0)
                return false;

            return ContainsAddress(Address, SnapshotRegions.Length / 2, 0, SnapshotRegions.Length);
        }

        private Boolean ContainsAddress(UInt64 Address, Int32 Middle, Int32 Min, Int32 Max)
        {
            if (Middle < 0 || Middle == SnapshotRegions.Length || Max < Min)
                return false;

            if (Address < unchecked((UInt64)SnapshotRegions[Middle].BaseAddress))
                return (ContainsAddress(Address, (Min + Middle - 1) / 2, Min, Middle - 1));
            else if (Address > unchecked((UInt64)SnapshotRegions[Middle].EndAddress))
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
            //SetElementType(BaseSnapshot.GetElementType());

            if (BaseSnapshot != null && BaseSnapshot.GetRegionCount() > 0)
            {
                foreach (SnapshotRegion Region in BaseSnapshot.GetSnapshotRegions())
                {
                    Regions.Add(new SnapshotRegion<LabelType>(Region));
                    Regions.Last().SetCurrentValues(Region.GetCurrentValues());
                }
            }
            SnapshotRegions = Regions.ToArray();

            Initialize();
        }

        public Snapshot(SnapshotRegion<LabelType>[] SnapshotData)
        {
            this.SnapshotRegions = SnapshotData;
            Initialize();
        }

        /// <summary>
        /// Constructor to create a snapshot from various regions
        /// </summary>
        /// <param name="SnapshotRegions"></param>
        public Snapshot(SnapshotRegion[] SnapshotRegions)
        {
            this.SnapshotRegions = SnapshotRegions.Select(x => (SnapshotRegion<LabelType>)x).ToArray();
            Initialize();
        }

        #endregion

        #region Initialization

        public void Initialize()
        {
            this.DeallocatedRegions = new List<SnapshotRegion>();
            DeallocatedRegionLock = new Object();
            SetAlignment(NoAlignment);
            InitializeProcessObserver();
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
        public void ReadAllSnapshotMemory()
        {
            SetTimeStampToNow();

            if (SnapshotRegions == null || SnapshotRegions.Length <= 0)
                return;
            
            //foreach (SnapshotRegion SnapshotRegion in SnapshotRegions)
            Parallel.ForEach(SnapshotRegions, (SnapshotRegion) =>
            {
                try
                {
                    SnapshotRegion.ReadAllSnapshotMemory(MemoryEditor);
                }
                catch (ScanFailedException)
                {
                    lock (DeallocatedRegionLock)
                    {
                        if (!DeallocatedRegions.Contains(SnapshotRegion))
                            DeallocatedRegions.Add(SnapshotRegion);
                    }
                }
            });
            
            // Mask deallocated regions
            SnapshotRegion[] NewRegions = MaskDeallocatedRegions();

            if (NewRegions == null || NewRegions.Length <= 0)
                return;
            
            // Attempt to collect values for the recovered regions
            foreach (SnapshotRegion SnapshotRegion in NewRegions)
            {
                try
                {
                    SnapshotRegion.ReadAllSnapshotMemory(MemoryEditor);
                }
                catch (ScanFailedException)
                {

                }
            }
        }

        /// <summary>
        /// Discards all elements marked as invalid, and updates the snapshot regions to contain only valid regions
        /// </summary>
        public override void DiscardInvalidRegions()
        {
            List<SnapshotRegion<LabelType>> CandidateRegions = new List<SnapshotRegion<LabelType>>();

            if (SnapshotRegions == null || SnapshotRegions.Length <= 0)
                return;

            // Collect valid element regions
            foreach (SnapshotRegion<LabelType> SnapshotRegion in this)
                CandidateRegions.AddRange(SnapshotRegion.GetValidRegions());

            // Mask the regions against the original snapshot (this snapshot)
            SnapshotRegion<LabelType>[] ValidRegions = MaskRegions(this, CandidateRegions.ToArray());

            // Shrink the regions based on their element type
            if (ValidRegions != null)
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
        public SnapshotRegion<LabelType>[] MaskRegions(Snapshot<LabelType> Mask, SnapshotRegion[] TargetRegions)
        {
            List<SnapshotRegion<LabelType>> ResultRegions = new List<SnapshotRegion<LabelType>>();

            // Initialize stacks with regions and masking regions
            Queue<SnapshotRegion<LabelType>> CandidateRegions = new Queue<SnapshotRegion<LabelType>>();
            Queue<SnapshotRegion<LabelType>> MaskingRegions = new Queue<SnapshotRegion<LabelType>>();
            
            if (TargetRegions == null || TargetRegions.Length < 0)
                return null;

            if (Mask == null || Mask.GetRegionCount() <= 0)
                return null;
            
            // Build candidate region queue from target region array
            foreach (SnapshotRegion<LabelType> Region in TargetRegions)
                CandidateRegions.Enqueue(Region);

            // Build masking region queue from snapshot
            foreach (SnapshotRegion<LabelType> MaskRegion in Mask)
                MaskingRegions.Enqueue(MaskRegion);
            
            if (CandidateRegions.Count == 0 || MaskingRegions.Count == 0)
                return null;

            SnapshotRegion<LabelType> CurrentRegion;
            SnapshotRegion<LabelType> CurrentMask = MaskingRegions.Dequeue();
            
            while (CandidateRegions.Count > 0)
            {
                // Grab next region
                CurrentRegion = CandidateRegions.Dequeue();

                // Grab the next mask following the current region
                while ((UInt64)CurrentMask.EndAddress < (UInt64)CurrentRegion.BaseAddress && MaskingRegions.Count > 0)
                    CurrentMask = MaskingRegions.Dequeue();

                // Check for mask completely removing this region
                if ((UInt64)CurrentMask.EndAddress < (UInt64)CurrentRegion.BaseAddress || (UInt64)CurrentMask.BaseAddress > (UInt64)CurrentRegion.EndAddress)
                    continue;

                // Mask completely overlaps, just use the original region
                if (CurrentMask.BaseAddress == CurrentRegion.BaseAddress && CurrentMask.EndAddress == CurrentRegion.EndAddress)
                {
                    ResultRegions.Add(CurrentRegion);
                    continue;
                }

                // Mask is within bounds; Grab the masked portion of this region
                Int32 BaseOffset = 0;
                if ((UInt64)CurrentMask.BaseAddress > (UInt64)CurrentRegion.BaseAddress)
                    BaseOffset = (Int32)((UInt64)CurrentMask.BaseAddress - (UInt64)CurrentRegion.BaseAddress);

                var NewRegion = new SnapshotRegion<LabelType>(CurrentRegion);
                ResultRegions.Add(NewRegion);
                NewRegion.BaseAddress = CurrentRegion.BaseAddress + BaseOffset;
                NewRegion.EndAddress = (IntPtr)Math.Min((UInt64)CurrentMask.EndAddress, (UInt64)CurrentRegion.EndAddress);
                NewRegion.SetCurrentValues(CurrentRegion.GetCurrentValues().LargestSubArray(BaseOffset, ResultRegions.Last().RegionSize + NewRegion.GetRegionExtension()));
                NewRegion.SetPreviousValues(CurrentRegion.GetPreviousValues().LargestSubArray(BaseOffset, ResultRegions.Last().RegionSize + NewRegion.GetRegionExtension()));
                NewRegion.SetElementLabels(CurrentRegion.GetElementLabels().LargestSubArray(BaseOffset, ResultRegions.Last().RegionSize + NewRegion.GetRegionExtension()));
                NewRegion.SetElementType(CurrentRegion.GetElementType());
                NewRegion.SetAlignment(CurrentRegion.GetAlignment());
            }
            
            return ResultRegions.Count == 0 ? null : ResultRegions.ToArray();
        }

        /// <summary>
        /// Merges labeled, non-overlapping regions in the current list of memory regions using a fast stack based algorithm O(nlogn + n)
        /// </summary>
        private void MergeRegions()
        {
            SnapshotRegion<LabelType>[] SnapshotRegions = (SnapshotRegion<LabelType>[])this.SnapshotRegions;

            if (SnapshotRegions == null || SnapshotRegions.Length == 0)
                return;

            // First, sort by start address
            Array.Sort(SnapshotRegions, (x, y) => ((UInt64)x.BaseAddress).CompareTo((UInt64)y.BaseAddress));

            // Create and initialize the stack with the first region
            Stack<SnapshotRegion<LabelType>> CombinedRegions = new Stack<SnapshotRegion<LabelType>>();
            CombinedRegions.Push(SnapshotRegions[0]);

            // Build the remaining regions
            for (Int32 Index = CombinedRegions.Count; Index < SnapshotRegions.Length; Index++)
            {
                SnapshotRegion<LabelType> Top = CombinedRegions.Peek();

                // If the interval does not overlap, put it on the top of the stack
                if ((UInt64)Top.EndAddress < (UInt64)SnapshotRegions[Index].BaseAddress)
                {
                    CombinedRegions.Push(SnapshotRegions[Index]);
                }
                // The regions are adjacent; merge them
                else if ((UInt64)Top.EndAddress == (UInt64)SnapshotRegions[Index].BaseAddress)
                {
                    Top.RegionSize = (Int32)((UInt64)SnapshotRegions[Index].EndAddress - (UInt64)Top.BaseAddress);
                    Top.SetElementLabels(Top.GetElementLabels().Concat(SnapshotRegions[Index].GetElementLabels()));
                }
                // The regions overlap.
                else if ((UInt64)Top.EndAddress <= (UInt64)SnapshotRegions[Index].EndAddress)
                {
                    Top.RegionSize = (Int32)((UInt64)SnapshotRegions[Index].EndAddress - (UInt64)Top.BaseAddress);
                    // throw new Exception("The labeled regions overlap and can not be merged.");
                }
            }

            // Replace memory regions with merged memory regions
            this.SnapshotRegions = CombinedRegions.ToArray();
            Array.Sort(this.SnapshotRegions, (x, y) => ((UInt64)x.BaseAddress).CompareTo((UInt64)y.BaseAddress));
        }

        /// <summary>
        /// Removes deallocated regions and recovers the remaining regions
        /// </summary>
        /// <returns></returns>
        private SnapshotRegion<LabelType>[] MaskDeallocatedRegions()
        {
            if (DeallocatedRegions == null || DeallocatedRegions.Count <= 0 || SnapshotRegions == null || SnapshotRegions.Length <= 0)
                return null;
            
            List<SnapshotRegion<LabelType>> NewSnapshotRegions = SnapshotRegions.Select(x => (SnapshotRegion<LabelType>)x).ToList();

            // Remove invalid items from collection
            foreach (SnapshotRegion<LabelType> Region in DeallocatedRegions)
                NewSnapshotRegions.Remove(Region);

            // Get current memory regions
            Snapshot<LabelType> Mask = new Snapshot<LabelType>(SnapshotManager.GetInstance().SnapshotAllRegions(true));

            // Mask each region against the current virtual memory regions
            SnapshotRegion<LabelType>[] MaskRegionResult = MaskRegions(Mask, DeallocatedRegions.ToArray());
            SnapshotRegion<LabelType>[] MaskedRegions = (MaskRegionResult == null ? null : MaskRegionResult.ToArray());

            // Merge split regions back with the main list
            if (MaskedRegions != null && MaskedRegions.Length > 0)
                NewSnapshotRegions.AddRange(MaskedRegions);

            // Clear invalid items
            DeallocatedRegions = new List<SnapshotRegion>();

            // Store result as main snapshot array
            this.SnapshotRegions = NewSnapshotRegions.ToArray();

            return MaskedRegions;
        }

        public void SetElementLabels(LabelType Value)
        {
            foreach (SnapshotRegion<LabelType> Region in this)
                Region.SetElementLabels(Value);
        }

    } // End class

    /// <summary>
    /// Indicates a scan failed, likely due to scanning deallocated memory or memory with certain virtual page flags
    /// </summary>
    public class ScanFailedException : Exception
    {
        public SnapshotRegion[] NewRegions { get; set; }
        public ScanFailedException() { }
        public ScanFailedException(SnapshotRegion[] NewRegions)
        {
            this.NewRegions = NewRegions;
        }

    } // End class

} // End namespace