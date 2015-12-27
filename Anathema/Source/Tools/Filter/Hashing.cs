using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class Hashing
    {
        /// <summary>
        /// This should be replaced with a real checksum function with better collision avoidance
        /// </summary>
        public unsafe static UInt64 ComputeCheckSum(Byte[] Data, UInt64 Start, UInt64 End)
        {
            unchecked
            {
                UInt64 Hash = 0;

                // Hashing function
                for (; Start < End; Start += sizeof(UInt64))
                {
                    fixed (Byte* Value = &Data[Start])
                    {
                        Hash ^= *(UInt64*)(Value);
                    }
                }
                for (; Start < End; Start++)
                {
                    fixed (Byte* Value = &Data[Start])
                    {
                        Hash ^= (UInt64)(*(Value));
                    }
                }

                return Hash;
            }
        }
    }
}
