namespace Ana.Source.Engine.OperatingSystems
{
    using Output;
    using System;
    using System.Collections.Generic;
    using Utils.Extensions;

    /// <summary>
    /// Defines an OS independent region in process memory space.
    /// </summary>
    internal class NormalizedRegion
    {
        /// <summary>
        /// The size of the region.
        /// </summary>
        private Int32 regionSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalizedRegion" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of the region.</param>
        /// <param name="regionSize">The size of the region.</param>
        public NormalizedRegion(IntPtr baseAddress, Int32 regionSize)
        {
            this.BaseAddress = baseAddress;
            this.RegionSize = regionSize;
        }

        /// <summary>
        /// Gets or sets the base address of the region.
        /// </summary>
        public IntPtr BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the size of the region.
        /// </summary>
        public Int32 RegionSize
        {
            get
            {
                return this.regionSize;
            }

            set
            {
                this.regionSize = value <= 0 ? 1 : value;
            }
        }

        /// <summary>
        /// Gets or sets the end address of the region.
        /// </summary>
        public IntPtr EndAddress
        {
            get
            {
                return this.BaseAddress.Add(this.RegionSize);
            }

            set
            {
                this.RegionSize = (Int32)value.Subtract(this.BaseAddress);
            }
        }

        /// <summary>
        /// Determines if a page has a higher base address.
        /// </summary>
        /// <param name="first">The first region being compared.</param>
        /// <param name="second">The second region being compared.</param>
        /// <returns>The result of the comparison.</returns>
        public static Boolean operator >(NormalizedRegion first, NormalizedRegion second)
        {
            return first.BaseAddress.ToUInt64() > second.BaseAddress.ToUInt64();
        }

        /// <summary>
        /// Determines if a page has a lower base address.
        /// </summary>
        /// <param name="first">The first region being compared.</param>
        /// <param name="second">The second region being compared.</param>
        /// <returns>The result of the comparison.</returns>
        public static Boolean operator <(NormalizedRegion first, NormalizedRegion second)
        {
            return first.BaseAddress.ToUInt64() < second.BaseAddress.ToUInt64();
        }

        /// <summary>
        /// Determines if a page has an equal or higher base address.
        /// </summary>
        /// <param name="first">The first region being compared.</param>
        /// <param name="second">The second region being compared.</param>
        /// <returns>The result of the comparison.</returns>
        public static Boolean operator >=(NormalizedRegion first, NormalizedRegion second)
        {
            return first.BaseAddress.ToUInt64() >= second.BaseAddress.ToUInt64();
        }

        /// <summary>
        /// Determines if a page has an equal or lower base address.
        /// </summary>
        /// <param name="first">The first region being compared.</param>
        /// <param name="second">The second region being compared.</param>
        /// <returns>The result of the comparison.</returns>
        public static Boolean operator <=(NormalizedRegion first, NormalizedRegion second)
        {
            return first.BaseAddress.ToUInt64() <= second.BaseAddress.ToUInt64();
        }

        /// <summary>
        /// Returns a collection of regions within this region, based on the specified chunking size.
        /// Ex) If this region is 257 bytes, chunking with a size of 64 will return 5 new regions.
        /// </summary>
        /// <param name="chunkSize">The size to break down the region into.</param>
        /// <returns>A collection of regions broken down from the original region based on the chunk size.</returns>
        public IEnumerable<NormalizedRegion> ChunkNormalizedRegion(Int32 chunkSize)
        {
            if (chunkSize <= 0)
            {
                String error = "Invalid chunk size specified for region";
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Fatal, error);
                throw new Exception(error);
            }

            chunkSize = Math.Min(chunkSize, this.RegionSize);

            Int32 chunkCount = (this.RegionSize / chunkSize) + (this.RegionSize % chunkSize == 0 ? 0 : 1);

            NormalizedRegion[] chunks = new NormalizedRegion[chunkCount];

            for (Int32 index = 0; index < chunkCount; index++)
            {
                Int32 size = chunkSize;

                // Set size to the remainder if on the final chunk and they are not divisible evenly
                if (index == chunkCount - 1 && this.RegionSize > chunkSize && this.RegionSize % chunkSize != 0)
                {
                    size = this.RegionSize % chunkSize;
                }

                chunks[index] = new NormalizedRegion(this.BaseAddress.Add(chunkSize * index), size);
            }

            return chunks;
        }
    }
    //// End interface
}
//// End namespace