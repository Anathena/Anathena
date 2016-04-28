using System;
using Anathema.Utils.Extensions;
using System.Collections.Generic;

namespace Anathema.Utils.OS
{
    /// <summary>
    /// Defines an OS independent region in process memory space
    /// </summary>
    public class NormalizedRegion
    {
        public IntPtr BaseAddress;
        public Int32 RegionSize;

        public IntPtr EndAddress { get { return BaseAddress.Add(RegionSize); } set { this.RegionSize = (Int32)value.Subtract(BaseAddress); } }

        public NormalizedRegion(IntPtr BaseAddress, Int32 RegionSize)
        {
            this.BaseAddress = BaseAddress;
            this.RegionSize = RegionSize;
        }

        /// <summary>
        /// Returns a collection of regions within this region, based on the specified chunking size.
        /// Ex) If this region is 257 bytes, chunking with a size of 64 will return 5 new regions.
        /// </summary>
        /// <param name="ChunkSize"></param>
        /// <returns></returns>
        public IEnumerable<NormalizedRegion> ChunkNormalizedRegion(Int32 ChunkSize)
        {
            if (ChunkSize <= 0)
                throw new Exception("Invalid chunk size specified for region");

            Int32 ChunkCount = RegionSize / ChunkSize + (RegionSize % ChunkSize == 0 ? 0 : 1);

            NormalizedRegion[] Chunks = new NormalizedRegion[ChunkCount];

            for (Int32 Index = 0; Index < ChunkCount; Index++)
            {
                Int32 Size = ChunkSize;

                if (Index == ChunkCount - 1 && RegionSize % ChunkSize == 0)
                    Size = RegionSize % ChunkSize;

                Chunks[Index] = new NormalizedRegion(this.BaseAddress.Add(ChunkSize * Index), Size);
            }

            return Chunks;
        }

        /// <summary>
        /// Determines if a page has a higher base address
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <returns></returns>
        public static Boolean operator >(NormalizedRegion First, NormalizedRegion Second)
        {
            return First.BaseAddress.ToUInt64() > Second.BaseAddress.ToUInt64();
        }

        /// <summary>
        /// Determines if a page has a lower base address
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <returns></returns>
        public static Boolean operator <(NormalizedRegion First, NormalizedRegion Second)
        {
            return First.BaseAddress.ToUInt64() < Second.BaseAddress.ToUInt64();
        }

        /// <summary>
        /// Determines if a page has an equal or higher base address
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <returns></returns>
        public static Boolean operator >=(NormalizedRegion First, NormalizedRegion Second)
        {
            return First.BaseAddress.ToUInt64() >= Second.BaseAddress.ToUInt64();
        }
        
        /// <summary>
        /// Determines if a page has an equal or lower base address
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <returns></returns>
        public static Boolean operator <=(NormalizedRegion First, NormalizedRegion Second)
        {
            return First.BaseAddress.ToUInt64() <= Second.BaseAddress.ToUInt64();
        }

    } // End interface

} // End namespace