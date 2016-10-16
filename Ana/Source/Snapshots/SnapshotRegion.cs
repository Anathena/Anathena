namespace Ana.Source.Snapshots
{
    using Engine;
    using Engine.OperatingSystems;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Utils.Extensions;

    /// <summary>
    /// Defines a snapshot of memory in an external process.
    /// </summary>
    internal abstract class SnapshotRegion : NormalizedRegion, IEnumerable
    {
        public SnapshotRegion(IntPtr baseAddress, Int32 regionSize) : base(baseAddress, regionSize)
        {
            this.RegionExtension = 0;
        }

        public SnapshotRegion(NormalizedRegion remoteRegion) : base(remoteRegion.BaseAddress, remoteRegion.RegionSize)
        {
            this.RegionExtension = 0;
        }

        public SnapshotRegion(SnapshotRegion snapshotRegion) : base(snapshotRegion.BaseAddress, snapshotRegion.RegionSize)
        {
            this.RegionExtension = snapshotRegion.RegionExtension;
        }

        /// <summary>
        /// Gets or sets the element type of this region
        /// [Public as an access optimization, otherwise use getters and setters] 
        /// </summary>
        public Type ElementType { get; set; }

        /// <summary>
        /// Gets or sets the valid bits for use in filtering scans
        /// [Public as an access optimization, otherwise use getters and setters]
        /// </summary>
        public BitArray Valid { get; set; }

        /// <summary>
        /// Gets or sets the regions only access one element at a time, so it is held here to avoid uneccessary memory usage
        /// [Public as an access optimization, otherwise use getters and setters]
        /// </summary>
        public SnapshotElement CurrentSnapshotElement { get; set; }

        /// <summary>
        /// Gets the time since the last read was performed on this region
        /// </summary>
        public DateTime TimeSinceLastRead { get; private set; }

        /// <summary>
        /// Gets or sets a value indicatating a safe number of read-over bytes
        /// </summary>
        protected Int32 RegionExtension { get; set; }

        /// <summary>
        /// Gets or sets the most recently read values
        /// </summary>
        protected unsafe Byte[] CurrentValues { get; set; }

        /// <summary>
        /// Gets or sets the previously read values
        /// </summary>
        protected unsafe Byte[] PreviousValues { get; set; }

        /// <summary>
        /// Gets or sets the memory alignment, typically aligned with native platform word size
        /// </summary>
        protected Int32 Alignment { get; set; }

        [Obfuscation(Exclude = true)]
        public unsafe abstract SnapshotElement this[Int32 index] { get; }

        /// <summary>
        /// Expands a region by a given size in both directions (default is element type size) unconditionally
        /// </summary>
        public void ExpandRegion()
        {
            this.ExpandRegion(this.GetElementReadOverSize());
        }

        public void ExpandRegion(Int32 expandSize)
        {
            // Expand with negative overflow protection
            if (this.BaseAddress.ToUInt64() > checked((UInt32)expandSize))
            {
                this.BaseAddress = this.BaseAddress.Subtract(expandSize);
            }
            else
            {
                this.BaseAddress = IntPtr.Zero;
            }

            // Expand with overflow protection
            Int32 newRegionSize = unchecked(this.RegionSize + (expandSize * 2));
            this.RegionSize = Math.Max(this.RegionSize, newRegionSize);
        }

        /// <summary>
        /// Fills a region into the available extension space
        /// </summary>
        public void FillRegion()
        {
            Int32 expandSize = this.RegionExtension;
            this.RegionExtension = 0;

            // Expand with overflow protection
            Int32 newRegionSize = unchecked(this.RegionSize + expandSize);
            this.RegionSize = Math.Max(this.RegionSize, newRegionSize);
        }

        /// <summary>
        /// Shrinks a region by the current element size. The old space is marked as extension space.
        /// This is important for elements with values that span several bytes, which need to read past the region size.
        /// </summary>
        public void RelaxRegion()
        {
            Int32 relaxSize = this.GetElementReadOverSize();

            this.FillRegion();

            if (this.RegionSize >= relaxSize)
            {
                this.RegionSize -= relaxSize;
                this.RegionExtension = relaxSize;
            }
            else
            {
                this.RegionExtension = this.RegionSize;
                this.RegionSize = 0;
            }
        }

        public void MarkAllValid()
        {
            this.Valid = new BitArray(this.RegionSize, true);
        }

        public void MarkAllInvalid()
        {
            this.Valid = new BitArray(this.RegionSize, false);
        }

        public void SetElementType(Type elementType)
        {
            this.ElementType = elementType;

            if (elementType == null)
            {
                return;
            }

            // Adjust extension space accordingly
            this.RelaxRegion();
        }

        public void SetAlignment(Int32 alignment)
        {
            this.Alignment = alignment;

            if (alignment == 0)
            {
                return;
            }

            // Enforce alignment constraint on base address
            if (this.BaseAddress.Mod(alignment).ToUInt64() != 0)
            {
                unchecked
                {
                    this.BaseAddress = this.BaseAddress.Subtract(this.BaseAddress.Mod(alignment));
                    this.BaseAddress = this.BaseAddress.Add(alignment);

                    this.RegionSize -= alignment - this.BaseAddress.Mod(alignment).ToInt32();
                    if (this.RegionSize < 0)
                    {
                        this.RegionSize = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Determines how many extra bytes an element will need to read to determine it's value
        /// </summary>
        /// <returns>The readover size of this region</returns>
        public Int32 GetElementReadOverSize()
        {
            return Marshal.SizeOf(this.ElementType) - 1;
        }

        public Int32 GetRegionExtension()
        {
            return this.RegionExtension;
        }

        public void SetCurrentValues(Byte[] newValues)
        {
            this.PreviousValues = this.CurrentValues;
            this.CurrentValues = newValues;
            this.CurrentSnapshotElement.InitializePointers();
        }

        public void SetPreviousValues(Byte[] newValues)
        {
            this.PreviousValues = newValues;
            this.CurrentSnapshotElement.InitializePointers();
        }

        public Byte[] GetCurrentValues()
        {
            return this.CurrentValues;
        }

        public Byte[] GetPreviousValues()
        {
            return this.PreviousValues;
        }

        public Type GetElementType()
        {
            return this.ElementType;
        }

        public Int32 GetAlignment()
        {
            return this.Alignment;
        }

        /// <summary>
        /// Determines if the values of this region can be compared with itself, ie previous and current values are initialized
        /// </summary>
        /// <returns>Returns true if an region can be compared with itself</returns>
        public Boolean CanCompare()
        {
            if (this.PreviousValues == null || this.CurrentValues == null || this.PreviousValues.Length != this.CurrentValues.Length)
            {
                return false;
            }

            return true;
        }

        public Boolean HasValues()
        {
            if (this.CurrentValues == null)
            {
                return false;
            }

            return true;
        }

        public Byte[] ReadAllRegionMemory(out Boolean readSuccess, Boolean keepValues = true)
        {
            this.TimeSinceLastRead = DateTime.Now;

            readSuccess = false;
            Byte[] currentValues = EngineCore.GetInstance().OperatingSystemAdapter.ReadBytes(this.BaseAddress, this.RegionSize + this.RegionExtension, out readSuccess);

            if (!readSuccess)
            {
                return null;
            }

            if (keepValues)
            {
                this.SetCurrentValues(currentValues);
            }

            return currentValues;
        }

        public IEnumerator GetEnumerator()
        {
            if (this.RegionSize <= 0)
            {
                yield break;
            }

            // Prevent the GC from moving buffers around
            GCHandle currentValuesHandle = GCHandle.Alloc(this.CurrentValues, GCHandleType.Pinned);
            GCHandle previousValuesHandle = GCHandle.Alloc(this.PreviousValues, GCHandleType.Pinned);

            this.CurrentSnapshotElement.InitializePointers();

            // Return the first element. This allows us to call IncrementPointers each loop unconditionally, with small speed gains.
            yield return this.CurrentSnapshotElement;

            if (this.Alignment == 1)
            {
                // Utilizes ++ operators; fast but we check every address
                for (Int32 index = 1; index < this.RegionSize; index++)
                {
                    this.CurrentSnapshotElement.IncrementPointers();
                    yield return this.CurrentSnapshotElement;
                }
            }
            else
            {
                // Utilizes += operators; faster because we access far less addresses
                for (Int32 index = this.Alignment; index < this.RegionSize; index += this.Alignment)
                {
                    this.CurrentSnapshotElement.AddPointers(this.Alignment);
                    yield return this.CurrentSnapshotElement;
                }
            }

            // Let the GC do what it wants now
            currentValuesHandle.Free();
            previousValuesHandle.Free();
        }
    }
    //// End class

    /// <summary>
    /// Defines a snapshot of memory in an external process, as well as assigned labels to this memory
    /// </summary>
    /// <typeparam name="LabelType">The label type of this snapshot region</typeparam>
    internal class SnapshotRegion<LabelType> : SnapshotRegion where LabelType : struct
    {
        public SnapshotRegion(IntPtr baseAddress, Int32 regionSize) : base(baseAddress, regionSize)
        {
            this.CurrentSnapshotElement = new SnapshotElement<LabelType>(this);
        }

        public SnapshotRegion(NormalizedRegion remoteRegion) : base(remoteRegion)
        {
            this.CurrentSnapshotElement = new SnapshotElement<LabelType>(this);
        }

        public SnapshotRegion(SnapshotRegion snapshotRegion) : base(snapshotRegion)
        {
            this.CurrentSnapshotElement = new SnapshotElement<LabelType>(this);
            this.SetCurrentValues(snapshotRegion.GetCurrentValues() == null ? null : (Byte[])snapshotRegion.GetCurrentValues().Clone());
            this.SetPreviousValues(snapshotRegion.GetPreviousValues() == null ? null : (Byte[])snapshotRegion.GetPreviousValues().Clone());
        }

        /// <summary>
        /// Gets or sets the labels for individual elements
        /// </summary>
        public LabelType?[] ElementLabels { get; set; }

        /// <summary>
        /// Indexer to access a labeled unified snapshot element at the specified index
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The snapshot at the specified index</returns>
        [Obfuscation(Exclude = true)]
        public unsafe override SnapshotElement this[Int32 index]
        {
            get
            {
                SnapshotElement<LabelType> element = new SnapshotElement<LabelType>(this);
                element.InitializePointers(index);
                return element;
            }
        }

        public LabelType?[] GetElementLabels()
        {
            return this.ElementLabels;
        }

        public void SetElementLabels(LabelType? elementLabel)
        {
            this.ElementLabels = Enumerable.Repeat(elementLabel, this.RegionSize).ToArray();
        }

        public void SetElementLabels(LabelType?[] elementLabels)
        {
            this.ElementLabels = elementLabels;
        }

        /// <summary>
        /// Retrieves a subset of snapshot regions where valid bits are true. Valid bits will be cleared in the new regions
        /// </summary>
        /// <returns>A subset of the snapshot regions where the valid bits are true</returns>
        public IEnumerable<SnapshotRegion<LabelType>> GetValidRegions()
        {
            List<SnapshotRegion<LabelType>> validRegions = new List<SnapshotRegion<LabelType>>();
            for (Int32 startIndex = 0; startIndex < (this.Valid == null ? 0 : this.Valid.Length); startIndex += this.Alignment)
            {
                if (!this.Valid[startIndex])
                {
                    continue;
                }

                // Get the length of this valid region
                Int32 validRegionSize = 0;
                do
                {
                    // We only care if the aligned elements are valid
                    validRegionSize += this.Alignment;
                }
                while (startIndex + validRegionSize < this.Valid.Length && this.Valid[startIndex + validRegionSize]);

                // Create new subregion from this valid region
                SnapshotRegion<LabelType> subRegion = new SnapshotRegion<LabelType>(this.BaseAddress + startIndex, validRegionSize);

                // Collect the current values. Attempts to collect out of bounds into extended space.
                subRegion.SetCurrentValues(this.CurrentValues.LargestSubArray(startIndex, validRegionSize + this.GetElementReadOverSize()));

                // Collect the element labels. Attempts to collect out of bounds into extended space.
                subRegion.SetElementLabels(this.ElementLabels.LargestSubArray(startIndex, validRegionSize + this.GetElementReadOverSize()));

                // If we were able to grab values into the extended space, update the extension size.
                if (subRegion.GetCurrentValues() != null)
                {
                    subRegion.RegionExtension = subRegion.GetCurrentValues().Length - validRegionSize;
                }
                else
                {
                    subRegion.RegionExtension = this.GetElementReadOverSize();
                }

                subRegion.SetElementType(this.ElementType);
                subRegion.SetAlignment(this.Alignment);

                validRegions.Add(subRegion);

                startIndex += validRegionSize;
            }

            // Free memory for GC on valid array
            this.Valid = null;

            return validRegions;
        }
    }
    //// End class
}
//// End namespace