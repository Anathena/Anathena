using System;

namespace Ana.Source.Utils.Extensions
{
    public static class IntPtrExtensions
    {
        #region Conversions

        public static UInt32 ToUInt32(this IntPtr IntPtr)
        {
            return unchecked((UInt32)(Int32)IntPtr);
        }

        public static UInt32 ToUInt32(this UIntPtr Self)
        {
            return unchecked((UInt32)Self);
        }

        public static UInt64 ToUInt64(this IntPtr IntPtr)
        {
            return unchecked((UInt64)(Int64)IntPtr);
        }

        public static UInt64 ToUInt64(this UIntPtr Self)
        {
            return unchecked((UInt64)Self);
        }

        public static IntPtr MaxValue(this IntPtr Self)
        {
            if (IntPtr.Size == sizeof(Int32))
                return unchecked((IntPtr)(Int32)UInt32.MaxValue);
            else if (IntPtr.Size == sizeof(Int64))
                return unchecked((IntPtr)(Int64)UInt64.MaxValue);
            return IntPtr.Zero;
        }

        public static UIntPtr MaxValue(this UIntPtr Self)
        {
            if (UIntPtr.Size == sizeof(Int32))
                return unchecked((UIntPtr)UInt32.MaxValue);
            else if (UIntPtr.Size == sizeof(Int64))
                return unchecked((UIntPtr)UInt64.MaxValue);
            return UIntPtr.Zero;
        }

        public static IntPtr MaxUserMode(this IntPtr Self)
        {
            if (IntPtr.Size == sizeof(Int32))
                return unchecked((IntPtr)Int32.MaxValue);
            else if (IntPtr.Size == sizeof(Int64))
                return unchecked((IntPtr)Int64.MaxValue);
            return IntPtr.Zero;
        }

        public static IntPtr MaxUserMode(this IntPtr Self, Boolean Is32Bit)
        {
            if (Is32Bit)
                return unchecked((IntPtr)Int32.MaxValue);
            else if (IntPtr.Size == sizeof(Int64))
                return unchecked((IntPtr)Int64.MaxValue);
            return IntPtr.Zero;
        }

        public static UIntPtr MaxUserMode(this UIntPtr Self)
        {
            if (UIntPtr.Size == sizeof(Int32))
                return unchecked((UIntPtr)Int32.MaxValue);
            else if (UIntPtr.Size == sizeof(Int64))
                return unchecked((UIntPtr)Int64.MaxValue);
            return UIntPtr.Zero;
        }

        public static UIntPtr MaxUserMode(this UIntPtr Self, Boolean Is32Bit)
        {
            if (Is32Bit)
                return unchecked((UIntPtr)Int32.MaxValue);
            else if (UIntPtr.Size == sizeof(Int64))
                return unchecked((UIntPtr)Int64.MaxValue);
            return UIntPtr.Zero;
        }

        #endregion

        #region IntPtr

        public static IntPtr Add(this IntPtr Left, IntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() + (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Add(this IntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() + (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Add(this IntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() + (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Add(this IntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() + (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Add(this IntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() + (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Add(this IntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() + (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Add(this IntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() + (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Add(this IntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() + (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Add(this IntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() + (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Subtract(this IntPtr Left, IntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() - (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Subtract(this IntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() - (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Subtract(this IntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() - (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Subtract(this IntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() - (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Subtract(this IntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() - (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Subtract(this IntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() - (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Subtract(this IntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() - (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Subtract(this IntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() - (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Subtract(this IntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() - (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Multiply(this IntPtr Left, IntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() * (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Multiply(this IntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() * (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Multiply(this IntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() * (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Multiply(this IntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() * (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Multiply(this IntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() * (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Multiply(this IntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() * (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Multiply(this IntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() * (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Multiply(this IntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() * (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Multiply(this IntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() * (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Divide(this IntPtr Left, IntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() / (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Divide(this IntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() / (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Divide(this IntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() / (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Divide(this IntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() / (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Divide(this IntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() / (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Divide(this IntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() / (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Divide(this IntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() / (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Divide(this IntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() / (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Divide(this IntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() / (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Mod(this IntPtr Left, IntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() % (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Mod(this IntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() % (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Mod(this IntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() % (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Mod(this IntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() % (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Mod(this IntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() % (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Mod(this IntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() % (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Mod(this IntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() % (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Mod(this IntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() % (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
            }
        }

        public static IntPtr Mod(this IntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(Left.ToUInt32() % (UInt32)Right)).ToIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
            }
        }

        #endregion

        #region UIntPtr

        public static UIntPtr Add(this UIntPtr Left, UIntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() + (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Add(this UIntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() + (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Add(this UIntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() + (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Add(this UIntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() + (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Add(this UIntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() + (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Add(this UIntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() + (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Add(this UIntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() + (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Add(this UIntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() + (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Add(this UIntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() + (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() + (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Subtract(this UIntPtr Left, UIntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() - (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Subtract(this UIntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() - (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Subtract(this UIntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() - (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Subtract(this UIntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() - (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Subtract(this UIntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() - (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Subtract(this UIntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() - (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Subtract(this UIntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() - (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Subtract(this UIntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() - (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Subtract(this UIntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() - (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() - (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Multiply(this UIntPtr Left, UIntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() * (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Multiply(this UIntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() * (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Multiply(this UIntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() * (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Multiply(this UIntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() * (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Multiply(this UIntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() * (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Multiply(this UIntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() * (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Multiply(this UIntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() * (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Multiply(this UIntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() * (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Multiply(this UIntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() * (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() * (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Divide(this UIntPtr Left, UIntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() / (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Divide(this UIntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() / (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Divide(this UIntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() / (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Divide(this UIntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() / (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Divide(this UIntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() / (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Divide(this UIntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() / (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Divide(this UIntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() / (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Divide(this UIntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() / (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Divide(this UIntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() / (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() / (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Mod(this UIntPtr Left, UIntPtr Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() % (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Mod(this UIntPtr Left, Byte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() % (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Mod(this UIntPtr Left, SByte Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() % (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Mod(this UIntPtr Left, Int16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() % (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Mod(this UIntPtr Left, UInt16 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() % (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Mod(this UIntPtr Left, Int32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() % (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Mod(this UIntPtr Left, UInt32 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() % (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Mod(this UIntPtr Left, Int64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() % (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToUIntPtr();
            }
        }

        public static UIntPtr Mod(this UIntPtr Left, UInt64 Right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(Left.ToUInt32() % (UInt32)Right).ToUIntPtr();
                default: return unchecked(Left.ToUInt64() % (UInt64)Right).ToUIntPtr();
            }
        }

        #endregion

    } // End class

} // End namespace