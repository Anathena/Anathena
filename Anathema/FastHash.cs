using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Modified FNV Hash to support a start and end index
    /// </summary>
    ///

    class FastHash
    {
        public unsafe static UInt64 ComputeHash(byte[] Data, int Start, int End)
        {
            unchecked
            {
                const UInt64 P = 16777619;
                UInt64 Hash = (UInt64)2166136261;

                // Main hash function
                for (; Start < End; Start += 8)
                {
                    fixed (byte* Value = &Data[Start])
                    {
                        Hash = (Hash ^ *((UInt64*)Value)) * P;
                    }
                }

                // Handle remaining bytes
                if (Start % 8 != 0)
                {
                    throw new NotImplementedException("Stuuuupid");
                }

                // Final hashing magic
                Hash += Hash << 13;
                Hash ^= Hash >> 7;
                Hash += Hash << 3;
                Hash ^= Hash >> 17;
                Hash += Hash << 5;
                return Hash;
            }
        }
    }
}