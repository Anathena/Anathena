namespace Ana.Source.Utils.Extensions
{
    using System;

    /// <summary>
    /// Extension methods for converting and operating on <see cref="IntPtr"/> types
    /// </summary>
    public static class IntPtrExtensions
    {
        #region Integer conversions

        /// <summary>
        /// Converts an <see cref="IntPtr"/> to an unsigned <see cref="UInt32"/>
        /// </summary>
        /// <param name="intPtr">The pointer to convert</param>
        /// <returns>The result of the conversion</returns>
        public static UInt32 ToUInt32(this IntPtr intPtr)
        {
            return unchecked((UInt32)(Int32)intPtr);
        }

        /// <summary>
        /// Converts an <see cref="UIntPtr"/> to an unsigned <see cref="UInt32"/>
        /// </summary>
        /// <param name="intPtr">The pointer to convert</param>
        /// <returns>The result of the conversion</returns>
        public static UInt32 ToUInt32(this UIntPtr intPtr)
        {
            return unchecked((UInt32)intPtr);
        }

        /// <summary>
        /// Converts an <see cref="IntPtr"/> to an unsigned <see cref="UInt64"/>
        /// </summary>
        /// <param name="intPtr">The pointer to convert</param>
        /// <returns>The result of the conversion</returns>
        public static UInt64 ToUInt64(this IntPtr intPtr)
        {
            return unchecked((UInt64)(Int64)intPtr);
        }

        /// <summary>
        /// Converts an <see cref="UIntPtr"/> to an unsigned <see cref="UInt64"/>
        /// </summary>
        /// <param name="intPtr">The pointer to convert</param>
        /// <returns>The result of the conversion</returns>
        public static UInt64 ToUInt64(this UIntPtr intPtr)
        {
            return unchecked((UInt64)intPtr);
        }

        #endregion

        #region IntPtr operations

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Add(this IntPtr left, IntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() + (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Add(this IntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() + (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Add(this IntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() + (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Add(this IntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() + (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Add(this IntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() + (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Add(this IntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() + (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Add(this IntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() + (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Add(this IntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() + (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Add(this IntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() + (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Subtract(this IntPtr left, IntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() - (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Subtract(this IntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() - (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Subtract(this IntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() - (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Subtract(this IntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() - (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Subtract(this IntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() - (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Subtract(this IntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() - (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Subtract(this IntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() - (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Subtract(this IntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() - (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Subtract(this IntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() - (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Multiply(this IntPtr left, IntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() * (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Multiply(this IntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() * (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Multiply(this IntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() * (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Multiply(this IntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() * (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Multiply(this IntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() * (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Multiply(this IntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() * (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Multiply(this IntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() * (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Multiply(this IntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() * (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Multiply(this IntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() * (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Divide(this IntPtr left, IntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() / (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Divide(this IntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() / (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Divide(this IntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() / (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Divide(this IntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() / (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Divide(this IntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() / (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Divide(this IntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() / (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Divide(this IntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() / (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Divide(this IntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() / (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Divide(this IntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() / (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Mod(this IntPtr left, IntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() % (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Mod(this IntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() % (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Mod(this IntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() % (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Mod(this IntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() % (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Mod(this IntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() % (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Mod(this IntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() % (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Mod(this IntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() % (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Mod(this IntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() % (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static IntPtr Mod(this IntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked((Int32)(left.ToUInt32() % (UInt32)right)).ToIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToIntPtr();
            }
        }

        #endregion

        #region UIntPtr operations

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Add(this UIntPtr left, UIntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() + (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Add(this UIntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() + (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Add(this UIntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() + (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Add(this UIntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() + (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Add(this UIntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() + (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Add(this UIntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() + (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Add(this UIntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() + (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Add(this UIntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() + (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the addition operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Add(this UIntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() + (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() + (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Subtract(this UIntPtr left, UIntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() - (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Subtract(this UIntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() - (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Subtract(this UIntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() - (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Subtract(this UIntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() - (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Subtract(this UIntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() - (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Subtract(this UIntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() - (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Subtract(this UIntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() - (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Subtract(this UIntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() - (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the subtraction operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Subtract(this UIntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() - (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() - (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Multiply(this UIntPtr left, UIntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() * (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Multiply(this UIntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() * (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Multiply(this UIntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() * (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Multiply(this UIntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() * (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Multiply(this UIntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() * (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Multiply(this UIntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() * (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Multiply(this UIntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() * (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Multiply(this UIntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() * (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the multiplication operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Multiply(this UIntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() * (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() * (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Divide(this UIntPtr left, UIntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() / (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Divide(this UIntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() / (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Divide(this UIntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() / (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Divide(this UIntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() / (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Divide(this UIntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() / (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Divide(this UIntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() / (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Divide(this UIntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() / (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Divide(this UIntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() / (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the division operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Divide(this UIntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() / (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() / (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Mod(this UIntPtr left, UIntPtr right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() % (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Mod(this UIntPtr left, Byte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() % (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Mod(this UIntPtr left, SByte right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() % (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Mod(this UIntPtr left, Int16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() % (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Mod(this UIntPtr left, UInt16 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() % (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Mod(this UIntPtr left, Int32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() % (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Mod(this UIntPtr left, UInt32 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() % (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Mod(this UIntPtr left, Int64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() % (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToUIntPtr();
            }
        }

        /// <summary>
        /// Performs the modulo operation with the given values
        /// </summary>
        /// <param name="left">The left side value</param>
        /// <param name="right">The right side value</param>
        /// <returns>The result of the operation</returns>
        public static UIntPtr Mod(this UIntPtr left, UInt64 right)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32): return unchecked(left.ToUInt32() % (UInt32)right).ToUIntPtr();
                default: return unchecked(left.ToUInt64() % (UInt64)right).ToUIntPtr();
            }
        }
        #endregion
    }
    //// End class
}
//// End namespace