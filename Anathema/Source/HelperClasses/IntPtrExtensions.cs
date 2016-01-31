using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public static class IntPtrExtensions
    {
        public static IntPtr Add(this IntPtr Left, IntPtr Right)
        {
            return (IntPtr)unchecked((UInt64)Left + (UInt64)Right);
        }

        public static IntPtr Subtract(this IntPtr Left, IntPtr Right)
        {
            return (IntPtr)unchecked((UInt64)Left - (UInt64)Right);
        }

        public static IntPtr Multiply(this IntPtr Left, IntPtr Right)
        {
            return (IntPtr)unchecked((UInt64)Left * (UInt64)Right);
        }

        public static IntPtr Divide(this IntPtr Left, IntPtr Right)
        {
            return (IntPtr)unchecked((UInt64)Left / (UInt64)Right);
        }
        
        public static UIntPtr Add(this UIntPtr Left, UIntPtr Right)
        {
            return (UIntPtr)unchecked((UInt64)Left + (UInt64)Right);
        }

        public static UIntPtr Subtract(this UIntPtr Left, UIntPtr Right)
        {
            return (UIntPtr)unchecked((UInt64)Left - (UInt64)Right);
        }

        public static UIntPtr Multiply(this UIntPtr Left, UIntPtr Right)
        {
            return (UIntPtr)unchecked((UInt64)Left * (UInt64)Right);
        }

        public static UIntPtr Divide(this UIntPtr Left, UIntPtr Right)
        {
            return (UIntPtr)unchecked((UInt64)Left / (UInt64)Right);
        }

    } // End class

} // End namespace