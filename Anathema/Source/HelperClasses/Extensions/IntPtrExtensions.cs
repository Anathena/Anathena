using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
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
            if (IntPtr.Size == 4)
                return unchecked((IntPtr)(Int32)UInt32.MaxValue);
            else if (IntPtr.Size == 8)
                return unchecked((IntPtr)(Int64)UInt64.MaxValue);
            return IntPtr.Zero;
        }

        public static UIntPtr MaxValue(this UIntPtr Self)
        {
            if (UIntPtr.Size == 4)
                return unchecked((UIntPtr)UInt32.MaxValue);
            else if (UIntPtr.Size == 8)
                return unchecked((UIntPtr)UInt64.MaxValue);
            return UIntPtr.Zero;
        }

        public static IntPtr MaxUserMode(this IntPtr Self)
        {
            if (IntPtr.Size == 4)
                return unchecked((IntPtr)Int32.MaxValue);
            else if (IntPtr.Size == 8)
                return unchecked((IntPtr)Int64.MaxValue);
            return IntPtr.Zero;
        }

        public static IntPtr MaxUserMode(this IntPtr Self, Boolean Is32Bit)
        {
            if (Is32Bit)
                return unchecked((IntPtr)Int32.MaxValue);
            else if (IntPtr.Size == 8)
                return unchecked((IntPtr)Int64.MaxValue);
            return IntPtr.Zero;
        }

        public static UIntPtr MaxUserMode(this UIntPtr Self)
        {
            if (UIntPtr.Size == 4)
                return unchecked((UIntPtr)Int32.MaxValue);
            else if (UIntPtr.Size == 8)
                return unchecked((UIntPtr)Int64.MaxValue);
            return UIntPtr.Zero;
        }

        public static UIntPtr MaxUserMode(this UIntPtr Self, Boolean Is32Bit)
        {
            if (Is32Bit)
                return unchecked((UIntPtr)Int32.MaxValue);
            else if (UIntPtr.Size == 8)
                return unchecked((UIntPtr)Int64.MaxValue);
            return UIntPtr.Zero;
        }

        #endregion

        #region IntPtr

        public static IntPtr Add(this IntPtr Left, IntPtr Right)
        {
            return unchecked(Left.ToUInt64() + Right.ToUInt64()).ToIntPtr();
        }

        public static IntPtr Add(this IntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Add(this IntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Add(this IntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Add(this IntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Add(this IntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Add(this IntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Add(this IntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Add(this IntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64() + (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Subtract(this IntPtr Left, IntPtr Right)
        {
            return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Subtract(this IntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Subtract(this IntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Subtract(this IntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Subtract(this IntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Subtract(this IntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Subtract(this IntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Subtract(this IntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Subtract(this IntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64() - (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Multiply(this IntPtr Left, IntPtr Right)
        {
            return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Multiply(this IntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Multiply(this IntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Multiply(this IntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Multiply(this IntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Multiply(this IntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Multiply(this IntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Multiply(this IntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Multiply(this IntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64() * (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Divide(this IntPtr Left, IntPtr Right)
        {
            return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Divide(this IntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Divide(this IntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Divide(this IntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Divide(this IntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Divide(this IntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Divide(this IntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Divide(this IntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Divide(this IntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64() / (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Mod(this IntPtr Left, IntPtr Right)
        {
            return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Mod(this IntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Mod(this IntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Mod(this IntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Mod(this IntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64() % (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Mod(this IntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Mod(this IntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Mod(this IntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToIntPtr();
        }

        public static IntPtr Mod(this IntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToIntPtr();
        }

        #endregion

        #region UIntPtr

        public static UIntPtr Add(this UIntPtr Left, UIntPtr Right)
        {
            return unchecked(Left.ToUInt64()  + (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Add(this UIntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64()  + (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Add(this UIntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64()  + (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Add(this UIntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64()  + (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Add(this UIntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64()  + (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Add(this UIntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64()  + (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Add(this UIntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64()  + (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Add(this UIntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64()  + (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Add(this UIntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64()  + (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Subtract(this UIntPtr Left, UIntPtr Right)
        {
            return unchecked(Left.ToUInt64()  - (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Subtract(this UIntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64()  - (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Subtract(this UIntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64()  - (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Subtract(this UIntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64()  - (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Subtract(this UIntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64()  - (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Subtract(this UIntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64()  - (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Subtract(this UIntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64()  - (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Subtract(this UIntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64()  - (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Subtract(this UIntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64()  - (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Multiply(this UIntPtr Left, UIntPtr Right)
        {
            return unchecked(Left.ToUInt64()  * (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Multiply(this UIntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64()  * (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Multiply(this UIntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64()  * (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Multiply(this UIntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64()  * (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Multiply(this UIntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64()  * (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Multiply(this UIntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64()  * (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Multiply(this UIntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64()  * (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Multiply(this UIntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64()  * (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Multiply(this UIntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64()  * (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Divide(this UIntPtr Left, UIntPtr Right)
        {
            return unchecked(Left.ToUInt64()  / (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Divide(this UIntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64()  / (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Divide(this UIntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64()  / (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Divide(this UIntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64()  / (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Divide(this UIntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64()  / (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Divide(this UIntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64()  / (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Divide(this UIntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64()  / (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Divide(this UIntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64()  / (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Divide(this UIntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64()  / (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Mod(this UIntPtr Left, UIntPtr Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Mod(this UIntPtr Left, Byte Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Mod(this UIntPtr Left, SByte Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Mod(this UIntPtr Left, Int16 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Mod(this UIntPtr Left, UInt16 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Mod(this UIntPtr Left, Int32 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Mod(this UIntPtr Left, UInt32 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Mod(this UIntPtr Left, Int64 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToUIntPtr();
        }

        public static UIntPtr Mod(this UIntPtr Left, UInt64 Right)
        {
            return unchecked(Left.ToUInt64()  % (UInt64)Right).ToUIntPtr();
        }

        #endregion

    } // End class

} // End namespace