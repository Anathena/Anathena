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
    class Snapshot : IEnumerable, IProcessObserver
    {
        private MemorySharp MemoryEditor;

        protected SnapshotRegion[] SnapshotRegions;
        protected ConcurrentBag<SnapshotRegion> DeallocatedRegions;

        // Variables to send to the display when displaying this snapshot
        private String ScanMethod;
        private DateTime TimeStamp;
        protected Type ElementType;

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
            List<SnapshotRegion> Regions = new List<SnapshotRegion>();

            if (BaseSnapshot.GetSnapshotRegions() != null)
            {
                foreach (SnapshotRegion Region in BaseSnapshot.GetSnapshotRegions())
                {
                    Regions.Add(new SnapshotRegion(Region));
                    Regions.Last().SetCurrentValues(Region.GetCurrentValues());
                    Regions.Last().SetElementType(Region.GetElementType());
                }
            }
            SnapshotRegions = Regions.ToArray();

            Initialize();
        }

        /// <summary>
        /// Constructor to create a snapshot from various regions
        /// </summary>
        /// <param name="SnapshotRegions"></param>
        public Snapshot(SnapshotRegion[] SnapshotRegions)
        {
            this.SnapshotRegions = SnapshotRegions;

            Initialize();
        }

        #endregion

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
                    if (Index - MemoryRegion.RegionSize >= 0)
                        Index -= MemoryRegion.RegionSize;
                    else
                        return MemoryRegion[Index];
                }
                return null;
            }
        }

        #region Initialization

        public void Initialize()
        {
            this.DeallocatedRegions = new ConcurrentBag<SnapshotRegion>();
            InitializeObserver();
            MergeRegions();
        }

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public MemorySharp GetMemoryEditor()
        {
            return MemoryEditor;
        }

        #endregion

        #region Property Accessors

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

        /// <summary>
        /// Updates type of every element with the specified type
        /// </summary>
        /// <param name="ElementType"></param>
        public void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;
            foreach (SnapshotRegion Region in this)
                Region.SetElementType(ElementType);
        }

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

        public Int32 GetRegionCount()
        {
            return SnapshotRegions.Length;
        }

        public UInt64 GetMemorySize()
        {
            return SnapshotRegions == null ? 0 : (UInt64)SnapshotRegions.AsEnumerable().Sum(x => (Int64)x.RegionSize);
        }

        #endregion

        /// <summary>
        /// Reads memory for every snapshot, with each region storing the current and previous read values.
        /// 
        /// Handles ScanFailedExceptions
        /// </summary>
        public void ReadAllSnapshotMemory()
        {
            Parallel.ForEach(SnapshotRegions, (SnapshotRegion) =>
            {
                try
                {
                    SnapshotRegion.ReadAllSnapshotMemory(MemoryEditor);
                }
                catch (ScanFailedException)
                {
                    if (!DeallocatedRegions.Contains(SnapshotRegion))
                        DeallocatedRegions.Add(SnapshotRegion);
                }
            });

            // Handle invalid reads
            if (!DeallocatedRegions.IsEmpty)
            {
                // Mask deallocated regions
                SnapshotRegion[] NewRegions = MaskDeallocatedRegions();

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

            SetTimeStampToNow();
        }

        protected virtual SnapshotRegion[] MaskDeallocatedRegions()
        {
            List<SnapshotRegion> NewSnapshotRegions = new List<SnapshotRegion>(SnapshotRegions);

            // Remove invalid items from collection
            foreach (SnapshotRegion Region in DeallocatedRegions)
                NewSnapshotRegions.Remove(Region);

            // Get current memory regions
            Snapshot Mask = SnapshotManager.GetInstance().SnapshotAllRegions();

            // Mask each region against the current virtual memory regions
            SnapshotRegion[] MaskedRegions = MaskRegions(Mask, DeallocatedRegions.ToArray());

            // Merge split regions back with the main list
            NewSnapshotRegions.AddRange(MaskedRegions);

            // Clear invalid items
            DeallocatedRegions = new ConcurrentBag<SnapshotRegion>();

            // Store result as main snapshot array
            this.SnapshotRegions = NewSnapshotRegions.ToArray();

            return MaskedRegions;
        }

        /// <summary>
        /// Returns regions containing elements marked as valid
        /// </summary>
        /// <returns></returns>
        public virtual SnapshotRegion[] GetValidRegions()
        {
            List<SnapshotRegion> CandidateRegions = new List<SnapshotRegion>();

            // Collect valid element regions
            foreach (SnapshotRegion Region in this)
                CandidateRegions.AddRange(Region.GetValidRegions());

            // Expand these by the size of their element type
            foreach (SnapshotRegion SnapshotRegion in CandidateRegions)
                SnapshotRegion.ExpandRegion();

            // Mask the expansions against the original snapshot
            SnapshotRegion[] ValidRegions = MaskRegions(this, CandidateRegions.ToArray());

            // Shrink the regions by the size of their element type
            if (ValidRegions != null)
                foreach (SnapshotRegion SnapshotRegion in CandidateRegions)
                    SnapshotRegion.ShrinkRegion();

            return ValidRegions;
        }

        public void MarkAllValid()
        {
            foreach (SnapshotRegion SnapshotRegion in this)
                SnapshotRegion.MarkAllValid();
        }

        public void MarkAllInvalid()
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
                SnapshotRegion.ExpandRegionBidirectional(ExpandSize);
        }

        /// <summary>
        /// Masks the given memory regions against the memory regions of a given snapshot, keeping the common elements of the two.
        /// </summary>
        /// <param name="Mask"></param>
        public SnapshotRegion[] MaskRegions(Snapshot Mask, SnapshotRegion[] TargetRegions)
        {
            List<SnapshotRegion> ResultRegions = new List<SnapshotRegion>();

            // Initialize stacks with regions and masking regions
            Queue<SnapshotRegion> CandidateRegions = new Queue<SnapshotRegion>();
            Queue<SnapshotRegion> MaskingRegions = new Queue<SnapshotRegion>();

            foreach (SnapshotRegion Region in TargetRegions)
                CandidateRegions.Enqueue(Region);

            foreach (SnapshotRegion MaskRegion in Mask)
                MaskingRegions.Enqueue(MaskRegion);

            if (CandidateRegions.Count == 0 || MaskingRegions.Count == 0)
                return null;

            SnapshotRegion CurrentRegion;
            SnapshotRegion CurrentMask = MaskingRegions.Dequeue();

            while (CandidateRegions.Count > 0)
            {
                // Grab next region
                CurrentRegion = CandidateRegions.Dequeue();

                // Grab the next mask following the current region
                while ((UInt64)CurrentMask.EndAddress < (UInt64)CurrentRegion.BaseAddress)
                    CurrentMask = MaskingRegions.Dequeue();

                // Check for mask completely removing this region
                if ((UInt64)CurrentMask.BaseAddress > (UInt64)CurrentRegion.EndAddress)
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

                ResultRegions.Add(new SnapshotRegion(CurrentRegion));
                ResultRegions.Last().BaseAddress = CurrentRegion.BaseAddress + BaseOffset;
                ResultRegions.Last().EndAddress = (IntPtr)Math.Min((UInt64)CurrentMask.EndAddress, (UInt64)CurrentRegion.EndAddress);
                ResultRegions.Last().SetCurrentValues(CurrentRegion.GetCurrentValues().LargestSubArray(BaseOffset, ResultRegions.Last().RegionSize + (ElementType == null ? 0 : Marshal.SizeOf(ElementType))));
                ResultRegions.Last().SetPreviousValues(CurrentRegion.GetPreviousValues().LargestSubArray(BaseOffset, ResultRegions.Last().RegionSize + (ElementType == null ? 0 : Marshal.SizeOf(ElementType))));
                ResultRegions.Last().SetElementType(CurrentRegion.GetElementType());
            }

            return ResultRegions.ToArray();
        }

        /// <summary>
        /// Merges continguous regions in the current list of memory regions using a fast stack based algorithm O(nlogn + n)
        /// </summary>
        protected virtual void MergeRegions()
        {
            if (SnapshotRegions == null || SnapshotRegions.Length == 0)
                return;

            // First, sort by start address
            Array.Sort(SnapshotRegions, (x, y) => ((UInt64)x.BaseAddress).CompareTo((UInt64)y.BaseAddress));

            // Create and initialize the stack with the first region
            Stack<SnapshotRegion> CombinedRegions = new Stack<SnapshotRegion>();
            CombinedRegions.Push(SnapshotRegions[0]);

            // Build the remaining regions
            for (Int32 Index = CombinedRegions.Count; Index < SnapshotRegions.Length; Index++)
            {
                SnapshotRegion Top = CombinedRegions.Peek();

                // If the interval does not overlap, put it on the top of the stack
                if ((UInt64)Top.EndAddress < (UInt64)SnapshotRegions[Index].BaseAddress)
                {
                    CombinedRegions.Push(SnapshotRegions[Index]);
                }
                // The interval overlaps; just merge it with the current top of the stack
                else if ((UInt64)Top.EndAddress <= (UInt64)SnapshotRegions[Index].EndAddress)
                {
                    Top.RegionSize = (Int32)((UInt64)SnapshotRegions[Index].EndAddress - (UInt64)Top.BaseAddress);
                }
            }

            // Replace memory regions with merged memory regions
            SnapshotRegions = CombinedRegions.ToArray();
            Array.Sort(SnapshotRegions, (x, y) => ((UInt64)x.BaseAddress).CompareTo((UInt64)y.BaseAddress));
        }

        public IEnumerator GetEnumerator()
        {
            return SnapshotRegions.GetEnumerator();
        }
    }

    /// <summary>
    /// Defines labeled data contained in a single snapshot
    /// </summary>
    class Snapshot<LabelType> : Snapshot where LabelType : struct
    {
        public Snapshot() : base()
        {

        }

        public Snapshot(Snapshot BaseSnapshot)
        {
            // Copy and convert the snapshot data to a labeled format
            SnapshotRegions = new SnapshotRegion<LabelType>[BaseSnapshot.GetRegionCount()];
            for (Int32 RegionIndex = 0; RegionIndex < SnapshotRegions.Length; RegionIndex++)
                SnapshotRegions[RegionIndex] = new SnapshotRegion<LabelType>(BaseSnapshot.GetSnapshotRegions()[RegionIndex]);

            Initialize();
        }

        public Snapshot(SnapshotRegion<LabelType>[] SnapshotData)
        {
            this.SnapshotRegions = SnapshotData;
            Initialize();
        }

        /// <summary>
        /// Indexer to allow the retrieval of the element at the specified index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public new SnapshotElement<LabelType> this[Int32 Index]
        {
            get
            {
                foreach (SnapshotRegion<LabelType> MemoryRegion in this)
                {
                    if (Index - MemoryRegion.RegionSize >= 0)
                        Index -= MemoryRegion.RegionSize;
                    else
                        return MemoryRegion[Index];
                }
                return null;
            }
        }

        public new SnapshotRegion<LabelType>[] GetValidRegions()
        {
            List<SnapshotRegion<LabelType>> CandidateRegions = new List<SnapshotRegion<LabelType>>();

            // Collect valid element regions
            foreach (SnapshotRegion<LabelType> Region in this)
                CandidateRegions.AddRange(Region.GetValidRegions());

            // Expand these by the size of their element type
            foreach (SnapshotRegion<LabelType> Region in CandidateRegions)
                Region.ExpandRegion();

            // Mask the expansions against the original snapshot
            SnapshotRegion<LabelType>[] ValidRegions = MaskRegions(this, CandidateRegions.ToArray());

            // Shrink the regions by the size of their element type
            if (ValidRegions != null)
                foreach (SnapshotRegion<LabelType> Region in ValidRegions)
                    Region.ShrinkRegion();

            return ValidRegions;
        }

        public void SetMemoryLabels(LabelType Value)
        {
            foreach (SnapshotRegion<LabelType> Region in this)
                Region.SetElementLabels(Value);
        }

        protected override SnapshotRegion[] MaskDeallocatedRegions()
        {
            List<SnapshotRegion<LabelType>> NewSnapshotRegions = SnapshotRegions.Select(x => (SnapshotRegion<LabelType>)x).ToList();

            // Remove invalid items from collection
            foreach (SnapshotRegion<LabelType> Region in DeallocatedRegions)
                NewSnapshotRegions.Remove(Region);

            // Get current memory regions
            Snapshot<LabelType> Mask = new Snapshot<LabelType>(SnapshotManager.GetInstance().SnapshotAllRegions());

            // Mask each region against the current virtual memory regions
            SnapshotRegion<LabelType>[] MaskedRegions = MaskRegions(Mask, DeallocatedRegions.Select(x => (SnapshotRegion<LabelType>)x).ToArray());

            // Merge split regions back with the main list
            NewSnapshotRegions.AddRange(MaskedRegions);

            // Clear invalid items
            DeallocatedRegions = new ConcurrentBag<SnapshotRegion>();

            // Store result as main snapshot array
            this.SnapshotRegions = NewSnapshotRegions.ToArray();

            return MaskedRegions;
        }

        /// <summary>
        /// Masks the given memory regions against the memory regions of a given snapshot, keeping the common elements of the two.
        /// </summary>
        /// <param name="Mask"></param>
        public SnapshotRegion<LabelType>[] MaskRegions(Snapshot<LabelType> Mask, SnapshotRegion<LabelType>[] TargetRegions)
        {
            List<SnapshotRegion<LabelType>> ResultRegions = new List<SnapshotRegion<LabelType>>();

            // Initialize stacks with regions and masking regions
            Queue<SnapshotRegion<LabelType>> CandidateRegions = new Queue<SnapshotRegion<LabelType>>();
            Queue<SnapshotRegion<LabelType>> MaskingRegions = new Queue<SnapshotRegion<LabelType>>();

            foreach (SnapshotRegion<LabelType> Region in TargetRegions)
                CandidateRegions.Enqueue(Region);

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
                while ((UInt64)CurrentMask.EndAddress < (UInt64)CurrentRegion.BaseAddress)
                    CurrentMask = MaskingRegions.Dequeue();

                // Check for mask completely removing this region
                if ((UInt64)CurrentMask.BaseAddress > (UInt64)CurrentRegion.EndAddress)
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

                ResultRegions.Add(new SnapshotRegion<LabelType>(CurrentRegion));
                ResultRegions.Last().BaseAddress = CurrentRegion.BaseAddress + BaseOffset;
                ResultRegions.Last().EndAddress = (IntPtr)Math.Min((UInt64)CurrentMask.EndAddress, (UInt64)CurrentRegion.EndAddress);
                ResultRegions.Last().SetCurrentValues(CurrentRegion.GetCurrentValues().LargestSubArray(BaseOffset, ResultRegions.Last().RegionSize + (ElementType == null ? 0 : Marshal.SizeOf(ElementType))));
                ResultRegions.Last().SetPreviousValues(CurrentRegion.GetPreviousValues().LargestSubArray(BaseOffset, ResultRegions.Last().RegionSize + (ElementType == null ? 0 : Marshal.SizeOf(ElementType))));
                ResultRegions.Last().SetElementLabels(CurrentRegion.GetElementLabels().LargestSubArray(BaseOffset, ResultRegions.Last().RegionSize + (ElementType == null ? 0 : Marshal.SizeOf(ElementType))));
                ResultRegions.Last().SetElementType(CurrentRegion.GetElementType());
            }

            return ResultRegions.ToArray();
        }

        /// <summary>
        /// Merges labeled, non-overlapping regions in the current list of memory regions using a fast stack based algorithm O(nlogn + n)
        /// </summary>
        protected override void MergeRegions()
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
                else if ((UInt64)Top.EndAddress > (UInt64)SnapshotRegions[Index].BaseAddress)
                {
                    throw new Exception("The labeled regions overlap and can not be merged.");
                }
            }

            // Replace memory regions with merged memory regions
            this.SnapshotRegions = CombinedRegions.ToArray();
            Array.Sort(this.SnapshotRegions, (x, y) => ((UInt64)x.BaseAddress).CompareTo((UInt64)y.BaseAddress));
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
    }

    /// <summary>
    /// Indicates a scan failed when attempting to hash all values
    /// </summary>
    public class HashingFailedException : Exception
    {
        public SnapshotRegion[] NewRegions { get; set; }
        public ConcurrentDictionary<SnapshotRegion, UInt64?> HashDictionary { get; set; }

        public HashingFailedException(SnapshotRegion[] NewRegions, ConcurrentDictionary<SnapshotRegion, UInt64?> HashDictionary)
        {
            this.NewRegions = NewRegions;
            this.HashDictionary = HashDictionary;
        }
    }

} // End namespace