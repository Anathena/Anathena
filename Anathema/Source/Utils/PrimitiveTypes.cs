using System;
using System.Linq;

namespace Anathema.Utils
{
    class PrimitiveTypes
    {
        private static Type[] ExcludedTypes = new Type[]{ typeof(IntPtr), typeof(UIntPtr), typeof(Boolean) };

        public static Type[] GetPrimitiveTypes()
        {
            return typeof(Int32).Assembly.GetTypes().Where(x => x.IsPrimitive && !ExcludedTypes.Contains(x)).ToArray();
        }

    } // End class

} // End namespace