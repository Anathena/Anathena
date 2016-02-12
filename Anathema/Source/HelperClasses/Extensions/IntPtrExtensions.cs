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

        public static UInt64 ToUInt64(this IntPtr IntPtr)
        {
            return unchecked((UInt64)(Int64)IntPtr);
        }

        public static UInt64 ToUInt64(this UIntPtr Self)
        {
            return unchecked((UInt64)(Int64)Self);
        }

        public static IntPtr MaxValue(this IntPtr Self)
        {
            if (IntPtr.Size == 4)
                return unchecked((IntPtr)UInt32.MaxValue);
            else if (IntPtr.Size == 8)
                return unchecked((IntPtr)UInt64.MaxValue);
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

        #endregion

        #region IntPtr

        public static IntPtr Add(this IntPtr Left, IntPtr Right)
        {
            return (IntPtr)unchecked((UInt64)Left + (UInt64)Right);
        }

        public static IntPtr Add<T>(this IntPtr Left, T Right) where T : struct
        {
            return (IntPtr)unchecked((UInt64)Left + (UInt64)Convert.ChangeType(Right, typeof(UInt64)));
        }

        public static IntPtr Subtract(this IntPtr Left, IntPtr Right)
        {
            return (IntPtr)unchecked((UInt64)Left - (UInt64)Right);
        }

        public static IntPtr Subtract<T>(this IntPtr Left, T Right) where T : struct
        {
            return (IntPtr)unchecked((UInt64)Left - (UInt64)Convert.ChangeType(Right, typeof(UInt64)));
        }

        public static IntPtr Multiply(this IntPtr Left, IntPtr Right)
        {
            return (IntPtr)unchecked((UInt64)Left * (UInt64)Right);
        }

        public static IntPtr Multiply<T>(this IntPtr Left, T Right) where T : struct
        {
            return (IntPtr)unchecked((UInt64)Left * (UInt64)Convert.ChangeType(Right, typeof(UInt64)));
        }

        public static IntPtr Divide(this IntPtr Left, IntPtr Right)
        {
            return (IntPtr)unchecked((UInt64)Left / (UInt64)Right);
        }

        public static IntPtr Divide<T>(this IntPtr Left, T Right) where T : struct
        {
            return (IntPtr)unchecked((UInt64)Left / (UInt64)Convert.ChangeType(Right, typeof(UInt64)));
        }

        #endregion

        #region UIntPtr

        public static UIntPtr Add(this UIntPtr Left, UIntPtr Right)
        {
            return (UIntPtr)unchecked((UInt64)Left + (UInt64)Right);
        }

        public static UIntPtr Add<T>(this UIntPtr Left, T Right) where T : struct
        {
            return (UIntPtr)unchecked((UInt64)Left + (UInt64)Convert.ChangeType(Right, typeof(UInt64)));
        }

        public static UIntPtr Subtract(this UIntPtr Left, UIntPtr Right)
        {
            return (UIntPtr)unchecked((UInt64)Left - (UInt64)Right);
        }

        public static UIntPtr Subtract<T>(this UIntPtr Left, T Right) where T : struct
        {
            return (UIntPtr)unchecked((UInt64)Left - (UInt64)Convert.ChangeType(Right, typeof(UInt64)));
        }

        public static UIntPtr Multiply(this UIntPtr Left, UIntPtr Right)
        {
            return (UIntPtr)unchecked((UInt64)Left * (UInt64)Right);
        }

        public static UIntPtr Multiply<T>(this UIntPtr Left, T Right) where T : struct
        {
            return (UIntPtr)unchecked((UInt64)Left * (UInt64)Convert.ChangeType(Right, typeof(UInt64)));
        }

        public static UIntPtr Divide(this UIntPtr Left, UIntPtr Right)
        {
            return (UIntPtr)unchecked((UInt64)Left / (UInt64)Right);
        }

        public static UIntPtr Divide<T>(this UIntPtr Left, T Right) where T : struct
        {
            return (UIntPtr)unchecked((UInt64)Left / (UInt64)Convert.ChangeType(Right, typeof(UInt64)));
        }

        #endregion

    } // End class

} // End namespace