namespace Ana.Source.Snapshots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Utils;
    using Utils.Extensions;

    /// <summary>
    /// Empty struct for unlabeled snapshots
    /// </summary>
    internal struct Null
    {
    }

    /// <summary>
    /// Defines data contained in a single snapshot
    /// </summary>
    internal abstract class Snapshot : IEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        private Type elementType;

        /// <summary>
        /// Memory alignment of the regions contained in the snapshot
        /// </summary>
        private Int32 alignment;

        /// <summary>
        /// Gets or sets a string indicating from where this snapshot was generated
        /// </summary>
        public String ScanMethod { get; set; }

        /// <summary>
        /// Gets or sets the time stamp of most recent scan for a given snapshot
        /// </summary>
        public DateTime TimeStamp { get; protected set; }

        /// <summary>
        /// Gets or sets the memory alignment of the regions contained in the snapshot
        /// </summary>
        public Int32 Alignment
        {
            get
            {
                return this.alignment;
            }

            set
            {
                this.alignment = value;
                if (this.SnapshotRegions == null)
                {
                    return;
                }

                foreach (SnapshotRegion region in this)
                {
                    region.SetAlignment(this.alignment);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value type of each element in this snapshot
        /// </summary>
        public Type ElementType
        {
            get
            {
                return this.elementType;
            }

            set
            {
                this.elementType = value;

                if (this.SnapshotRegions == null || this.SnapshotRegions.Count() <= 0)
                {
                    return;
                }

                foreach (SnapshotRegion region in this)
                {
                    region.SetElementType(elementType);
                }
            }
        }

        /// <summary>
        /// Gets or sets the snapshot regions contained in this snapshot
        /// </summary>
        protected IEnumerable<SnapshotRegion> SnapshotRegions { get; set; }

        /// <summary>
        /// Gets or sets the collection of deallocated regions found when reading memory
        /// </summary>
        protected List<SnapshotRegion> DeallocatedRegions { get; set; }

        /// <summary>
        /// Gets or sets the lock for accessing deallocated regions
        /// </summary>
        protected Object DeallocatedRegionLock { get; set; }

        /// <summary>
        /// Indexer to allow the retrieval of the element at the specified index. Note that this does NOT index into a region
        /// </summary>
        /// <param name="index">The index of the snapshot element</param>
        /// <returns>Returns the snapshot element at the specified index</returns>
        [Obfuscation(Exclude = true)]
        public SnapshotElement this[Int32 index]
        {
            get
            {
                foreach (SnapshotRegion region in this)
                {
                    if (index - (region.RegionSize / this.Alignment) >= 0)
                    {
                        index -= region.RegionSize / this.Alignment;
                    }
                    else
                    {
                        return region[index * this.Alignment];
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract Snapshot Clone();

        /// <summary>
        /// 
        /// </summary>
        public abstract Snapshot<NewLabelType> CloneAs<NewLabelType>() where NewLabelType : struct;

        /// <summary>
        /// Reads all memory for every region contained in this snapshot
        /// </summary>
        public abstract void ReadAllSnapshotMemory();

        /// <summary>
        /// Sets a valid bit for each element in this snapshot
        /// </summary>
        public abstract void MarkAllValid();

        /// <summary>
        /// Sets an invalid bit for each element in this snapshot
        /// </summary>
        public abstract void MarkAllInvalid();

        /// <summary>
        /// Discards all sections of memory marked with an invalid bit
        /// </summary>
        public abstract void DiscardInvalidRegions();

        /// <summary>
        /// Gets all snapshot regions contained in this snapshot
        /// </summary>
        /// <returns>The snapshot regions contained in this snapshot</returns>
        public IEnumerable<SnapshotRegion> GetSnapshotRegions()
        {
            return this.SnapshotRegions;
        }

        /// <summary>
        /// Gets the number of regions contained in this snapshot
        /// </summary>
        /// <returns>The number of regions contained in this snapshot</returns>
        public Int32 GetRegionCount()
        {
            return this.SnapshotRegions == null ? 0 : this.SnapshotRegions.Count();
        }

        /// <summary>
        /// Gets the number of individual elements contaiend in this snapshot
        /// </summary>
        /// <returns>The number of individual elements contaiend in this snapshot</returns>
        public UInt64 GetElementCount()
        {
            return this.SnapshotRegions == null ? 0 : (UInt64)this.SnapshotRegions.AsEnumerable().Sum(x => (Int64)(x.RegionSize / this.Alignment));
        }

        /// <summary>
        /// Gets the total number of bytes contained in this snapshot
        /// </summary>
        /// <returns>The total number of bytes contained in this snapshot</returns>
        public UInt64 GetMemorySize()
        {
            return this.SnapshotRegions == null ? 0 : (UInt64)this.SnapshotRegions.AsEnumerable().Sum(x => ((Int64)x.RegionSize / this.Alignment) + (Int64)x.GetRegionExtension());
        }

        /// <summary>
        /// Sets the underlying data type of the element to an arbitrary data type of the specified size
        /// </summary>
        /// <param name="variableSize">The size of the data contained at each element in this snapshot</param>
        public void SetVariableSize(Int32 variableSize)
        {
            switch (variableSize)
            {
                case sizeof(SByte):
                    this.ElementType = typeof(SByte);
                    break;
                case sizeof(Int16):
                    this.ElementType = typeof(Int16);
                    break;
                case sizeof(Int32):
                    this.ElementType = typeof(Int32);
                    break;
                case sizeof(Int64):
                    this.ElementType = typeof(Int64);
                    break;
                default:
                    throw new Exception("Unsupported variable size");
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.SnapshotRegions.GetEnumerator();
        }

        public Boolean ContainsAddress(IntPtr address)
        {
            if (this.SnapshotRegions == null || this.SnapshotRegions.Count() == 0)
            {
                return false;
            }

            return this.ContainsAddress(address, this.SnapshotRegions.Count() / 2, 0, this.SnapshotRegions.Count());
        }

        private Boolean ContainsAddress(IntPtr address, Int32 middle, Int32 min, Int32 max)
        {
            if (middle < 0 || middle == this.SnapshotRegions.Count() || max < min)
            {
                return false;
            }

            if (address.ToUInt64() < this.SnapshotRegions.ElementAt(middle).BaseAddress.ToUInt64())
            {
                return this.ContainsAddress(address, (min + middle - 1) / 2, min, middle - 1);
            }
            else if (address.ToUInt64() > this.SnapshotRegions.ElementAt(middle).EndAddress.ToUInt64())
            {
                return this.ContainsAddress(address, (middle + 1 + max) / 2, middle + 1, max);
            }
            else
            {
                return true;
            }
        }
    }
    //// End class

    /// <summary>
    /// Defines data contained in a single labeled snapshot
    /// </summary>
    /// <typeparam name="LabelType">The type of the labels for each snapshot element</typeparam>
    internal class Snapshot<LabelType> : Snapshot where LabelType : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Snapshot{LabelType}"/> class
        /// </summary>
        public Snapshot()
        {
            this.SnapshotRegions = null;
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Snapshot{LabelType}"/> class
        /// </summary>
        /// <param name="snapshotRegions">The regions with which to initialize this snapshot</param>
        public Snapshot(IEnumerable<SnapshotRegion> snapshotRegions)
        {
            this.SnapshotRegions = snapshotRegions == null ? null : snapshotRegions.Select(x => (SnapshotRegion<LabelType>)x);
            this.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public override Snapshot Clone()
        {
            List<SnapshotRegion<LabelType>> regions = new List<SnapshotRegion<LabelType>>();

            if (this.GetRegionCount() > 0)
            {
                foreach (SnapshotRegion region in this.GetSnapshotRegions())
                {
                    regions.Add(new SnapshotRegion<LabelType>(region));
                    regions.Last().SetCurrentValues(region.GetCurrentValues());
                }
            }

            Snapshot<LabelType> clonedSnapshot = new Snapshot<LabelType>(regions);
            clonedSnapshot.Alignment = this.Alignment;
            clonedSnapshot.ElementType = this.GetElementType();

            return clonedSnapshot;
        }

        public override Snapshot<NewLabelType> CloneAs<NewLabelType>()
        {
            return new Snapshot<NewLabelType>(this.Clone().GetSnapshotRegions());
        }

        public void Initialize()
        {
            this.DeallocatedRegions = new List<SnapshotRegion>();
            this.DeallocatedRegionLock = new Object();

            this.MergeRegions();
        }

        public override void MarkAllValid()
        {
            foreach (SnapshotRegion snapshotRegion in this)
            {
                snapshotRegion.MarkAllValid();
            }
        }

        public override void MarkAllInvalid()
        {
            foreach (SnapshotRegion snapshotRegion in this)
            {
                snapshotRegion.MarkAllInvalid();
            }
        }

        /// <summary>
        /// Expands all memory regions in both directions based on the size of the current element type.
        /// Useful for filtering methods that isolate changing bytes (ie 1 byte of an 8 byte integer), where we would want to grow to recover the other 7 bytes.
        /// </summary>
        /// <param name="expandSize">The size of the region expansion</param>
        public void ExpandAllRegionsOutward(Int32 expandSize)
        {
            foreach (SnapshotRegion snapshotRegion in this)
            {
                snapshotRegion.ExpandRegion(expandSize);
            }
        }

        /// <summary>
        /// Reads memory for every snapshot, with each region storing the current and previous read values.
        /// Handles ScanFailedExceptions by automatically masking deallocated regions against the current virtual memory space
        /// </summary>
        public override void ReadAllSnapshotMemory()
        {
            this.TimeStamp = DateTime.Now;

            if (this.SnapshotRegions == null || this.SnapshotRegions.Count() <= 0)
            {
                return;
            }

            //// Mask this snapshot regions against active virtual pages in the target
            //// TODO: Debug this shit, apparently it isn't working correctly
            //// Snapshot<LabelType> Mask = new Snapshot<LabelType>(SnapshotManager.GetInstance().CollectSnapshot(UseSettings: false, UsePrefilter: false));
            //// SnapshotRegions = MaskRegions(Mask, this.GetSnapshotRegions());

            Parallel.ForEach(
                this.SnapshotRegions,
                (snapshotRegion) =>
            {
                Boolean readSuccess;

                snapshotRegion.ReadAllRegionMemory(out readSuccess, keepValues: true);

                if (!readSuccess)
                {
                    using (TimedLock.Lock(this.DeallocatedRegionLock))
                    {
                        if (!this.DeallocatedRegions.Contains(snapshotRegion))
                        {
                            this.DeallocatedRegions.Add(snapshotRegion);
                        }
                    }
                }
            });

            // Mask deallocated regions
            IEnumerable<SnapshotRegion> newRegions = this.MaskDeallocatedRegions();

            if (newRegions == null || newRegions.Count() <= 0)
            {
                return;
            }

            // Attempt to collect values for the recovered regions
            foreach (SnapshotRegion snapshotRegion in newRegions)
            {
                Boolean readSuccess;
                snapshotRegion.ReadAllRegionMemory(out readSuccess);
            }
        }

        /// <summary>
        /// Discards all elements marked as invalid, and updates the snapshot regions to contain only valid regions
        /// </summary>
        public override void DiscardInvalidRegions()
        {
            List<SnapshotRegion<LabelType>> candidateRegions = new List<SnapshotRegion<LabelType>>();

            if (this.SnapshotRegions == null || this.SnapshotRegions.Count() <= 0)
            {
                return;
            }

            // Collect valid element regions
            foreach (SnapshotRegion<LabelType> snapshotRegion in this)
            {
                candidateRegions.AddRange(snapshotRegion.GetValidRegions());
            }

            // Mask the regions against the original snapshot (this snapshot)
            IEnumerable<SnapshotRegion<LabelType>> validRegions = this.MaskRegions(this, candidateRegions);

            // Shrink the regions based on their element type
            if (validRegions != null && validRegions.Count() > 0)
            {
                foreach (SnapshotRegion<LabelType> region in validRegions)
                {
                    region.RelaxRegion();
                }
            }

            this.SnapshotRegions = validRegions;
        }

        /// <summary>
        /// Masks the given memory regions against the memory regions of a given snapshot, keeping the common elements of the two in O(n)
        /// </summary>
        /// <param name="mask">The snapshot to mask the target regions against</param>
        /// <param name="targetRegions">The regions we are performing a mask against</param>
        /// <returns>A new collection of regions created from the masking operation</returns>
        public IEnumerable<SnapshotRegion<LabelType>> MaskRegions(Snapshot<LabelType> mask, IEnumerable<SnapshotRegion> targetRegions)
        {
            List<SnapshotRegion<LabelType>> resultRegions = new List<SnapshotRegion<LabelType>>();

            // Initialize stacks with regions and masking regions
            Queue<SnapshotRegion<LabelType>> candidateRegions = new Queue<SnapshotRegion<LabelType>>();
            Queue<SnapshotRegion<LabelType>> maskingRegions = new Queue<SnapshotRegion<LabelType>>();

            if (targetRegions == null || targetRegions.Count() < 0)
            {
                return null;
            }

            if (mask == null || mask.GetRegionCount() <= 0)
            {
                return null;
            }

            // Build candidate region queue from target region array
            foreach (SnapshotRegion<LabelType> region in targetRegions.OrderBy(x => x.BaseAddress.ToUInt64()))
            {
                candidateRegions.Enqueue(region);
            }

            // Build masking region queue from snapshot
            foreach (SnapshotRegion<LabelType> maskRegion in mask.GetSnapshotRegions().OrderBy(x => x.BaseAddress.ToUInt64()))
            {
                maskingRegions.Enqueue(maskRegion);
            }

            if (candidateRegions.Count <= 0 || maskingRegions.Count <= 0)
            {
                return null;
            }

            SnapshotRegion<LabelType> currentRegion;
            SnapshotRegion<LabelType> currentMask = maskingRegions.Dequeue();

            while (candidateRegions.Count > 0)
            {
                // Grab next region
                currentRegion = candidateRegions.Dequeue();

                // Grab the next mask following the current region
                while (currentMask.EndAddress.ToUInt64() < currentRegion.BaseAddress.ToUInt64() && maskingRegions.Count > 0)
                {
                    currentMask = maskingRegions.Dequeue();
                }

                // Check for mask completely removing this region
                if (currentMask.EndAddress.ToUInt64() < currentRegion.BaseAddress.ToUInt64() || currentMask.BaseAddress.ToUInt64() > currentRegion.EndAddress.ToUInt64())
                {
                    continue;
                }

                // Mask completely overlaps, just use the original region
                if (currentMask.BaseAddress == currentRegion.BaseAddress && currentMask.EndAddress == currentRegion.EndAddress)
                {
                    resultRegions.Add(currentRegion);
                    continue;
                }

                // Mask is within bounds; Grab the masked portion of this region
                Int32 baseOffset = 0;
                if (currentMask.BaseAddress.ToUInt64() > currentRegion.BaseAddress.ToUInt64())
                {
                    baseOffset = currentMask.BaseAddress.Subtract(currentRegion.BaseAddress).ToInt32();
                }

                SnapshotRegion<LabelType> newRegion = new SnapshotRegion<LabelType>(currentRegion);
                newRegion.BaseAddress = currentRegion.BaseAddress + baseOffset;
                newRegion.EndAddress = Math.Min(currentMask.EndAddress.ToUInt64(), currentRegion.EndAddress.ToUInt64()).ToIntPtr();
                newRegion.SetCurrentValues(currentRegion.GetCurrentValues().LargestSubArray(baseOffset, newRegion.RegionSize + newRegion.GetRegionExtension()));
                newRegion.SetPreviousValues(currentRegion.GetPreviousValues().LargestSubArray(baseOffset, newRegion.RegionSize + newRegion.GetRegionExtension()));
                newRegion.SetElementLabels(currentRegion.GetElementLabels().LargestSubArray(baseOffset, newRegion.RegionSize + newRegion.GetRegionExtension()));
                newRegion.SetElementType(currentRegion.GetElementType());
                newRegion.SetAlignment(currentRegion.GetAlignment());
                resultRegions.Add(newRegion);
            }

            return resultRegions.Count == 0 ? null : resultRegions;
        }

        public void ClearSnapshotRegions()
        {
            this.SnapshotRegions = null;
        }

        public void AddSnapshotRegions(IEnumerable<SnapshotRegion<LabelType>> snapshotRegions)
        {
            List<SnapshotRegion<LabelType>> newRegions = this.SnapshotRegions == null ? new List<SnapshotRegion<LabelType>>() : ((IEnumerable<SnapshotRegion<LabelType>>)this.SnapshotRegions).ToList();
            newRegions.AddRange(snapshotRegions);
            this.SnapshotRegions = newRegions.ToArray();
            Array.Sort((SnapshotRegion<LabelType>[])this.SnapshotRegions, (x, y) => x.BaseAddress.ToUInt64().CompareTo(y.BaseAddress.ToUInt64()));

            this.MergeRegions();
        }

        public void SetElementLabels(LabelType label)
        {
            foreach (SnapshotRegion<LabelType> region in this)
            {
                region.SetElementLabels(label);
            }
        }

        /// <summary>
        /// Merges regions in the current list of memory regions using a fast stack based algorithm O(nlogn + n)
        /// </summary>
        private void MergeRegions()
        {
            if (this.SnapshotRegions == null)
            {
                return;
            }

            SnapshotRegion<LabelType>[] snapshotRegionArray = ((IEnumerable<SnapshotRegion<LabelType>>)this.SnapshotRegions).ToArray();

            if (snapshotRegionArray == null || snapshotRegionArray.Length <= 0)
            {
                return;
            }

            // First, sort by start address
            Array.Sort(snapshotRegionArray, (x, y) => x.BaseAddress.ToUInt64().CompareTo(y.BaseAddress.ToUInt64()));

            // Create and initialize the stack with the first region
            Stack<SnapshotRegion<LabelType>> combinedRegions = new Stack<SnapshotRegion<LabelType>>();
            combinedRegions.Push(snapshotRegionArray[0]);

            // Build the remaining regions
            for (Int32 index = combinedRegions.Count; index < snapshotRegionArray.Length; index++)
            {
                SnapshotRegion<LabelType> top = combinedRegions.Peek();

                if (top.EndAddress.ToUInt64() < snapshotRegionArray[index].BaseAddress.ToUInt64())
                {
                    // If the interval does not overlap, put it on the top of the stack
                    combinedRegions.Push(snapshotRegionArray[index]);
                }
                else if (top.EndAddress.ToUInt64() == snapshotRegionArray[index].BaseAddress.ToUInt64())
                {
                    // The regions are adjacent; merge them
                    top.RegionSize = snapshotRegionArray[index].EndAddress.Subtract(top.BaseAddress).ToInt32();
                    top.SetElementLabels(top.GetElementLabels().Concat(snapshotRegionArray[index].GetElementLabels()));
                }
                else if (top.EndAddress.ToUInt64() <= snapshotRegionArray[index].EndAddress.ToUInt64())
                {
                    // The regions overlap
                    top.RegionSize = snapshotRegionArray[index].EndAddress.Subtract(top.BaseAddress).ToInt32();
                }
            }

            // Replace memory regions with merged memory regions
            this.SnapshotRegions = combinedRegions.ToArray();
            Array.Sort((SnapshotRegion<LabelType>[])this.SnapshotRegions, (x, y) => x.BaseAddress.ToUInt64().CompareTo(y.BaseAddress.ToUInt64()));
        }

        /// <summary>
        /// Removes deallocated regions and recovers the remaining regions
        /// </summary>
        /// <returns>A collection of snapshot regions after performing a mask operation against deallocated regions</returns>
        private IEnumerable<SnapshotRegion<LabelType>> MaskDeallocatedRegions()
        {
            if (this.DeallocatedRegions == null || this.DeallocatedRegions.Count <= 0 || this.SnapshotRegions == null || this.SnapshotRegions.Count() <= 0)
            {
                return null;
            }

            List<SnapshotRegion<LabelType>> newSnapshotRegions = this.SnapshotRegions.Select(x => (SnapshotRegion<LabelType>)x).ToList();

            // Remove invalid items from collection
            foreach (SnapshotRegion<LabelType> region in this.DeallocatedRegions)
            {
                newSnapshotRegions.Remove(region);
            }

            // Get current memory regions
            Snapshot<LabelType> mask = SnapshotManager.GetInstance().CollectSnapshot(useSettings: false, usePrefilter: false).CloneAs<LabelType>();

            // Mask each region against the current virtual memory regions
            IEnumerable<SnapshotRegion<LabelType>> maskedRegions = this.MaskRegions(mask, this.DeallocatedRegions);

            // Merge split regions back with the main list
            if (maskedRegions != null && maskedRegions.Count() > 0)
            {
                newSnapshotRegions.AddRange(maskedRegions);
            }

            // Clear invalid items
            this.DeallocatedRegions = new List<SnapshotRegion>();

            // Store result as main snapshot array
            this.SnapshotRegions = newSnapshotRegions;

            return maskedRegions;
        }
    }
    //// End class
}
//// End namespace