namespace Ana.Source.Utils
{
    using System;

    internal static class Hashing
    {
        public static unsafe UInt64 ComputeCheckSum(Byte[] data)
        {
            unchecked
            {
                UInt64 hash = 0;
                Int32 start = 0;

                // Hashing function
                for (; start < data.Length; start += sizeof(UInt64))
                {
                    fixed (Byte* value = &data[start])
                    {
                        hash ^= *(UInt64*)value;
                    }
                }

                for (; start < data.Length; start++)
                {
                    fixed (Byte* value = &data[start])
                    {
                        hash ^= (UInt64)(*value);
                    }
                }

                return hash;
            }
        }

        /// <summary>
        /// This should be replaced with a real checksum function with better collision avoidance
        /// </summary>
        public static unsafe UInt64 ComputeCheckSum(Byte[] data, UInt64 start, UInt64 end)
        {
            unchecked
            {
                UInt64 hash = 0;
                fixed (Byte* basePointer = &data[start])
                {
                    UInt64* valuePointer = (UInt64*)basePointer;

                    // Hashing function
                    for (; start < end; start += sizeof(UInt64))
                    {
                        hash ^= *valuePointer++;
                    }

                    Byte* remainderPointer = (Byte*)valuePointer;

                    // Handle remaining bytes
                    for (; start < end; start++)
                    {
                        hash ^= (UInt64)(*remainderPointer++);
                    }

                    return hash;
                }
            }
        }
    }
    //// End class
}
//// End namespace