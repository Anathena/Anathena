using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public static class Int64Extensions
    {

        public static IntPtr ToIntPtr(this Int64 Int64)
        {
            return unchecked((IntPtr)Int64);
        }

        public static UIntPtr ToUIntPtr(this Int64 Int64)
        {
            return unchecked((UIntPtr)Int64);
        }

        public static IntPtr ToIntPtr(this UInt64 UInt64)
        {
            return unchecked((IntPtr)UInt64);
        }

        public static UIntPtr ToUIntPtr(this UInt64 UInt64)
        {
            return unchecked((UIntPtr)UInt64);
        }

    } // End class

} // End namespace