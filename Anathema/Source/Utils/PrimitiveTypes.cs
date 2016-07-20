using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Anathema.Source.Utils
{
    class PrimitiveTypes
    {
        private static Type[] ExcludedTypes = new Type[] { typeof(IntPtr), typeof(UIntPtr), typeof(Boolean) };

        public static IEnumerable<Type> GetPrimitiveTypes()
        {
            return typeof(Int32).Assembly.GetTypes().Where(X => X.IsPrimitive);
        }

        public static IEnumerable<Type> GetScannablePrimitiveTypes()
        {
            return typeof(Int32).Assembly.GetTypes().Where(X => X.IsPrimitive && !ExcludedTypes.Contains(X));
        }

        public static Boolean IsPrimitive(Type Type)
        {
            foreach (Type PrimitiveType in GetPrimitiveTypes())
                if (Type == PrimitiveType)
                    return true;

            return false;
        }

        public static Int32 GetLargestPrimitiveSize()
        {
            return GetScannablePrimitiveTypes().Select(X => Marshal.SizeOf(X)).Max();
        }

    } // End class

} // End namespace