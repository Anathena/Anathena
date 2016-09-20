using System;

namespace Ana.Source.Utils.Extensions
{
    public static class Int64Extensions
    {
        public static IntPtr ToIntPtr(this Int64 Int64)
        {
            return unchecked((IntPtr)Int64);
        }

        public static UIntPtr ToUIntPtr(this Int64 Int64)
        {
            return unchecked((UIntPtr)(UInt64)Int64);
        }

        public static IntPtr ToIntPtr(this UInt64 UInt64)
        {
            return unchecked((IntPtr)(Int64)UInt64);
        }

        public static UIntPtr ToUIntPtr(this UInt64 UInt64)
        {
            return unchecked((UIntPtr)UInt64);
        }

        public static IntPtr ToIntPtr(this Int32 Int32)
        {
            return unchecked((IntPtr)Int32);
        }

        public static UIntPtr ToUIntPtr(this Int32 Int32)
        {
            return unchecked((UIntPtr)(UInt64)Int32);
        }

        public static IntPtr ToIntPtr(this UInt32 UInt32)
        {
            return unchecked((IntPtr)(Int64)UInt32);
        }

        public static UIntPtr ToUIntPtr(this UInt32 UInt32)
        {
            return unchecked((UIntPtr)UInt32);
        }

    } // End class

} // End namespace