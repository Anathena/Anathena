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
        public unsafe static UInt64 ComputeHash(byte[] Data, UInt64 Start, UInt64 End)
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

                // Handle remaining bytes (TODO: this could use some optimization)
                if (Start % 8 != 0)
                {
                    UInt64 RemainderValue = 0;
                    UInt32 Ugh = (UInt32)(Start % 8);
                    Start -= Ugh;
                    for (int Index = 0; Index < Ugh; Index++)
                    {
                        RemainderValue |= (UInt64)Data[Start - (UInt64)Index - 1] << Index * 8;
                    }
                    Hash = (Hash ^ RemainderValue) * P;
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