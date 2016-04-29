using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Anathema.Utils
{
    class PrimitiveTypes
    {
        private static Type[] ExcludedTypes = new Type[]{ typeof(IntPtr), typeof(UIntPtr), typeof(Boolean) };

        public static Type[] GetPrimitiveTypes()
        {
            return typeof(Int32).Assembly.GetTypes().Where(X => X.IsPrimitive && !ExcludedTypes.Contains(X)).ToArray();
        }

        public static Int32 GetLargestPrimitiveSize()
        {
            return GetPrimitiveTypes().Select(X => Marshal.SizeOf(X)).Max();
        }

    } // End class

} // End namespace