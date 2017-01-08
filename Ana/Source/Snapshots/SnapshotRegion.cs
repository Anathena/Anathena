namespace Ana.Source.Snapshots
{
    using Engine;
    using Engine.OperatingSystems;
    using Results.ScanResults;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UserSettings;
    using Utils;
    using Utils.Extensions;

    /// <summary>
    /// Defines a region of memory in an external process.
    /// </summary>
    internal class SnapshotRegion : NormalizedRegion, IEnumerable
    {
        /// <summary>
        /// The memory alignment of this region.
        /// </summary>
        private Int32 alignment;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotRegion" /> class.
        /// </summary>
        public SnapshotRegion() : this(IntPtr.Zero, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotRegion" /> class.
        /// </summary>
        /// <param name="normalizedRegion">The region on which to base this snapshot region.</param>
        public SnapshotRegion(NormalizedRegion normalizedRegion) :
            this(normalizedRegion == null ? IntPtr.Zero : normalizedRegion.BaseAddress, normalizedRegion == null ? 0 : normalizedRegion.RegionSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotRegion" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of this snapshot region.</param>
        /// <param name="regionSize">The size of this snapshot region.</param>
        public SnapshotRegion(IntPtr baseAddress, Int32 regionSize) : base(baseAddress, regionSize)
        {
            this.TimeSinceLastRead = DateTime.MinValue;
            this.Alignment = SettingsViewModel.GetInstance().Alignment;
            this.ElementType = ScanResultsViewModel.GetInstance().ActiveType;
        }

        /// <summary>
        /// Gets or sets the data type of the elements of this region.
        /// </summary>
        public Type ElementType { get; set; }

        /// <summary>
        /// Gets or sets the data type of the labels of this region.
        /// </summary>
        public Type LabelType { get; set; }

        /// <summary>
        /// Gets or sets the memory alignment, typically aligned with external process pointer size.
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

                // Enforce alignment constraint on base address
                if (this.BaseAddress.Mod(value).ToUInt64() == 0)
                {
                    return;
                }

                unchecked
                {
                    this.BaseAddress = this.BaseAddress.Subtract(this.BaseAddress.Mod(value));
                    this.BaseAddress = this.BaseAddress.Add(value);
                    this.RegionSize -= value - this.BaseAddress.Mod(value).ToInt32();
                }
            }
        }

        /// <summary>
        /// Gets or sets the most recently read values.
        /// </summary>
        private unsafe Byte[] CurrentValues { get; set; }

        /// <summary>
        /// Gets or sets the previously read values.
        /// </summary>
        private unsafe Byte[] PreviousValues { get; set; }

        /// <summary>
        /// Gets or sets the previously read values.
        /// </summary>
        private unsafe Object[] ElementLabels { get; set; }

        /// <summary>
        /// Gets or sets the valid bits for use in filtering scans.
        /// </summary>
        private BitArray ValidBits { get; set; }

        /// <summary>
        /// Gets or sets the time since the last read was performed on this region.
        /// </summary>
        private DateTime TimeSinceLastRead { get; set; }

        /// <summary>
        /// Gets or sets the reference to the snapshot element being iterated over.
        /// </summary>
        private SnapshotElementRef SnapshotElementRef { get; set; }

        /// <summary>
        /// Indexer to allow the retrieval of the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the snapshot element.</param>
        /// <returns>Returns the snapshot element at the specified index.</returns>
        public SnapshotElementRef this[Int32 index]
        {
            get
            {
                SnapshotElementRef element = new SnapshotElementRef(this);
                element.InitializePointers(index);
                return element;
            }
        }

        /// <summary>
        /// Sets all valid bits to the specified boolean value.
        /// </summary>
        /// <param name="isValid">Value indicating if valid bits will be marked as valid or invalid.</param>
        public void SetAllValidBits(Boolean isValid)
        {
            this.ValidBits = new BitArray(this.RegionSize, isValid);
        }

        /// <summary>
        /// Expands a region by the element type size in both directions unconditionally.
        /// </summary>
        /// <param name="expandSize">The size by which to expand this region.</param>
        public void Expand(Int32 expandSize)
        {
            // TODO: Rollovers
            this.BaseAddress = this.BaseAddress.Subtract(expandSize);
            this.RegionSize += expandSize;
        }

        /// <summary>
        /// Sets the current values of this region.
        /// </summary>
        /// <param name="newValues">The raw bytes of the values.</param>
        public void SetCurrentValues(Byte[] newValues)
        {
            this.CurrentValues = newValues;
        }

        /// <summary>
        /// Sets the previous values of this region.
        /// </summary>
        /// <param name="newValues">The raw bytes of the values.</param>
        public void SetPreviousValues(Byte[] newValues)
        {
            this.PreviousValues = newValues;
        }

        /// <summary>
        /// Gets the time since values were read for this region.
        /// </summary>
        /// <returns>The time since values were read for this region.</returns>
        public DateTime GetTimeSinceLastRead()
        {
            return this.TimeSinceLastRead;
        }

        /// <summary>
        /// Gets the valid bit array associated with this region.
        /// </summary>
        /// <returns>The valid bit array associated with this region.</returns>
        public BitArray GetValidBits()
        {
            return this.ValidBits;
        }

        /// <summary>
        /// Sets the element labels for this snapshot region.
        /// </summary>
        /// <param name="newLabels">The new labels to be assigned.</param>
        public void SetElementLabels(Object[] newLabels)
        {
            this.ElementLabels = newLabels;
        }

        /// <summary>
        /// Determines if a relative comparison can be done for this region, ie current and previous values are loaded.
        /// </summary>
        /// <returns>True if a relative comparison can be done for this region.</returns>
        public Boolean CanCompare()
        {
            if (this.PreviousValues == null || this.CurrentValues == null || this.PreviousValues.Length != this.CurrentValues.Length)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads all memory for this snapshot region.
        /// </summary>
        /// <param name="readSuccess">Whether or not the read was successful.</param>
        /// <param name="keepValues">Whether or not to keep the values returned as current values.</param>
        /// <returns>The bytes read from memory.</returns>
        public Byte[] ReadAllRegionMemory(out Boolean readSuccess, Boolean keepValues = true)
        {
            this.TimeSinceLastRead = DateTime.Now;

            readSuccess = false;
            Byte[] newCurrentValues = EngineCore.GetInstance().OperatingSystemAdapter.ReadBytes(this.BaseAddress, this.RegionSize, out readSuccess);

            if (!readSuccess)
            {
                return null;
            }

            if (keepValues)
            {
                this.SetPreviousValues(this.CurrentValues);
                this.SetCurrentValues(newCurrentValues);
            }

            return newCurrentValues;
        }

        /// <summary>
        /// Determines if an address is contained in this snapshot.
        /// </summary>
        /// <param name="address">The address for which to search.</param>
        /// <returns>True if the address is contained.</returns>
        public Boolean ContainsAddress(IntPtr address)
        {
            if (address.ToUInt64() >= this.BaseAddress.ToUInt64() && address.ToUInt64() <= this.EndAddress.ToUInt64())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the previous values as raw bytes of this snapshot.
        /// </summary>
        /// <returns>The previous values as raw bytes of this snapshot.</returns>
        public Byte[] GetPreviousValues()
        {
            return this.PreviousValues;
        }

        /// <summary>
        /// Gets the current values as raw bytes of this snapshot.
        /// </summary>
        /// <returns>The current values as raw bytes of this snapshot.</returns>
        public Byte[] GetCurrentValues()
        {
            return this.CurrentValues;
        }

        /// <summary>
        /// Gets the colletion of element labels for this snapshot region.
        /// </summary>
        /// <returns>The colletion of element labels for this snapshot region.</returns>
        public Object[] GetElementLabels()
        {
            return this.ElementLabels;
        }

        /// <summary>
        /// Gets the regions in this snapshot with a valid bit set.
        /// </summary>
        /// <returns>The regions in this snapshot with a valid bit set.</returns>
        public IEnumerable<SnapshotRegion> GetValidRegions()
        {
            List<SnapshotRegion> validRegions = new List<SnapshotRegion>();

            if (this.ValidBits == null)
            {
                return validRegions;
            }

            for (Int32 startIndex = 0; startIndex < this.ValidBits.Length; startIndex += this.Alignment)
            {
                if (!this.ValidBits[startIndex])
                {
                    continue;
                }

                // Get the length of this valid segment
                Int32 validRegionSize = 0;
                do
                {
                    // We only care if the aligned elements are valid
                    validRegionSize += this.Alignment;
                }
                while (startIndex + validRegionSize < this.ValidBits.Length && this.ValidBits[startIndex + validRegionSize]);

                // Create new subregion from this valid region
                SnapshotRegion subRegion = new SnapshotRegion(this.BaseAddress + startIndex, validRegionSize);

                // Ensure region size is worth keeping. This can happen if we grab a misaligned segment
                if (subRegion.RegionSize < Conversions.GetTypeSize(this.ElementType))
                {
                    continue;
                }

                // Copy the current values and labels.
                subRegion.SetCurrentValues(this.CurrentValues.LargestSubArray(startIndex, subRegion.RegionSize));
                subRegion.SetPreviousValues(this.PreviousValues.LargestSubArray(startIndex, subRegion.RegionSize));
                subRegion.SetElementLabels(this.ElementLabels.LargestSubArray(startIndex, subRegion.RegionSize));

                validRegions.Add(subRegion);
                startIndex += validRegionSize;
            }

            this.ValidBits = null;
            return validRegions;
        }

        /// <summary>
        /// Gets the number of bytes that this snapshot spans.
        /// </summary>
        /// <returns>The number of bytes that this snapshot spans.</returns>
        public Int64 GetByteCount()
        {
            return this.CurrentValues == null ? 0L : this.CurrentValues.LongLength;
        }

        /// <summary>
        /// Gets the number of elements contained by this snapshot. Equal to ByteCount / Alignment.
        /// </summary>
        /// <returns>The number of elements contained by this snapshot.</returns>
        public Int32 GetElementCount()
        {
            return unchecked((Int32)(this.CurrentValues == null ? 0L : this.CurrentValues.LongLength / this.Alignment));
        }

        /// <summary>
        /// Gets the enumerator for an element reference within this snapshot region.
        /// </summary>
        /// <returns>The enumerator for an element reference within this snapshot region.</returns>
        public IEnumerator GetEnumerator()
        {
            if (this.RegionSize <= 0 || this.Alignment <= 0)
            {
                yield break;
            }

            // Prevent the GC from moving buffers around
            GCHandle currentValuesHandle = GCHandle.Alloc(this.CurrentValues, GCHandleType.Pinned);
            GCHandle previousValuesHandle = GCHandle.Alloc(this.PreviousValues, GCHandleType.Pinned);

            this.SnapshotElementRef = new SnapshotElementRef(this);

            this.SnapshotElementRef.InitializePointers();

            // Return the first element. This allows us to call IncrementPointers each loop unconditionally based on alignment later.
            yield return this.SnapshotElementRef;

            if (this.Alignment == 1)
            {
                // Utilizes ++ operator. This is fast operation wise, but slower because we check every address
                for (Int32 index = 1; index < this.RegionSize; index++)
                {
                    this.SnapshotElementRef.IncrementPointers();
                    yield return this.SnapshotElementRef;
                }
            }
            else
            {
                // Utilizes += operators. This is faster because we access far less addresses
                for (Int32 index = this.Alignment; index < this.RegionSize; index += this.Alignment)
                {
                    this.SnapshotElementRef.AddPointers(this.Alignment);
                    yield return this.SnapshotElementRef;
                }
            }

            // Let the GC do what it wants now
            currentValuesHandle.Free();
            previousValuesHandle.Free();
        }

        /// <summary>
        /// Gets the element size of the current data type.
        /// </summary>
        /// <returns>The element size of the current data type.</returns>
        private Int32 GetElementSize()
        {
            // Switch on type code. Could also do Marshal.SizeOf(DataType), but it is slower
            switch (Type.GetTypeCode(this.GetElementType()))
            {
                case TypeCode.Byte:
                    return sizeof(Byte);
                case TypeCode.SByte:
                    return sizeof(SByte);
                case TypeCode.Int16:
                    return sizeof(Int16);
                case TypeCode.Int32:
                    return sizeof(Int32);
                case TypeCode.Int64:
                    return sizeof(Int64);
                case TypeCode.UInt16:
                    return sizeof(UInt16);
                case TypeCode.UInt32:
                    return sizeof(UInt32);
                case TypeCode.UInt64:
                    return sizeof(UInt64);
                case TypeCode.Single:
                    return sizeof(Single);
                case TypeCode.Double:
                    return sizeof(Double);
                default:
                    throw new Exception("Invalid element type");
            }
        }
    }
    //// End class
}
//// End namespace