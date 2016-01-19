using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class Hashing
    {
        public unsafe static UInt64 ComputeCheckSum(Byte[] Data)
        {
            unchecked
            {
                UInt64 Hash = 0;
                Int32 Start = 0;

                // Hashing function
                for (; Start < Data.Length; Start += sizeof(UInt64))
                {
                    fixed (Byte* Value = &Data[Start])
                    {
                        Hash ^= *(UInt64*)(Value);
                    }
                }
                for (; Start < Data.Length; Start++)
                {
                    fixed (Byte* Value = &Data[Start])
                    {
                        Hash ^= (UInt64)(*(Value));
                    }
                }

                return Hash;
            }
        }

        /// <summary>
        /// This should be replaced with a real checksum function with better collision avoidance
        /// </summary>
        public unsafe static UInt64 ComputeCheckSum(Byte[] Data, UInt64 Start, UInt64 End)
        {
            unchecked
            {
                UInt64 Hash = 0;
                fixed (Byte* BasePointer = &Data[Start])
                {
                    UInt64* ValuePointer = (UInt64*)BasePointer;

                    // Hashing function
                    for (; Start < End; Start += sizeof(UInt64))
                        Hash ^= *ValuePointer++;

                    Byte* RemainderPointer = (Byte*)ValuePointer;
                    // Handle remaining bytes
                    for (; Start < End; Start++)
                        Hash ^= (UInt64)(*RemainderPointer++);

                    return Hash;
                }
            }
        }

        /*
        public unsafe static UInt32 ComputeCheckSum(Byte[] Data, UInt32 Start, UInt32 End)
        {
            unchecked
            {
                UInt32 Hash = 0;

                // Hashing function
                for (; Start < End; Start += sizeof(UInt32))
                {
                    fixed (Byte* Value = &Data[Start])
                    {
                        Hash ^= *(UInt32*)(Value);
                    }
                }
                for (; Start < End; Start++)
                {
                    fixed (Byte* Value = &Data[Start])
                    {
                        Hash ^= (UInt32)(*(Value));
                    }
                }

                return Hash;
            }
        }*/

    } // End class

} // End namespace
