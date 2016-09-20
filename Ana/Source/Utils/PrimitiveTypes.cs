namespace Ana.Source.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal class PrimitiveTypes
    {
        private static Type[] excludedTypes = new Type[] { typeof(IntPtr), typeof(UIntPtr), typeof(Boolean) };

        public static IEnumerable<Type> GetPrimitiveTypes()
        {
            return typeof(Int32).Assembly.GetTypes().Where(x => x.IsPrimitive);
        }

        public static IEnumerable<Type> GetScannablePrimitiveTypes()
        {
            return typeof(Int32).Assembly.GetTypes().Where(x => x.IsPrimitive && !excludedTypes.Contains(x));
        }

        public static Boolean IsPrimitive(Type type)
        {
            foreach (Type primitiveType in GetPrimitiveTypes())
            {
                if (type == primitiveType)
                {
                    return true;
                }
            }

            return false;
        }

        public static Int32 GetLargestPrimitiveSize()
        {
            return GetScannablePrimitiveTypes().Select(x => Marshal.SizeOf(x)).Max();
        }
    }
    //// End class
}
//// End namespace