namespace Ana.Source.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Methods for primitive types on the system
    /// </summary>
    internal class PrimitiveTypes
    {
        /// <summary>
        /// System types excluded from the list of primitive types
        /// </summary>
        private static readonly Type[] ExcludedTypes = new Type[]
        {
            typeof(IntPtr),
            typeof(UIntPtr),
            typeof(Boolean)
        };

        /// <summary>
        /// Gets all primitive types
        /// </summary>
        /// <returns>An enumeration of primitive types</returns>
        public static IEnumerable<Type> GetPrimitiveTypes()
        {
            return typeof(Int32).Assembly.GetTypes().Where(x => x.IsPrimitive);
        }

        /// <summary>
        /// Gets primitive types that are available for scanning
        /// </summary>
        /// <returns>An enumeration of scannable types</returns>
        public static IEnumerable<Type> GetScannablePrimitiveTypes()
        {
            return typeof(Int32).Assembly.GetTypes().Where(x => x.IsPrimitive && !ExcludedTypes.Contains(x));
        }

        /// <summary>
        /// Determines if a type is primitive
        /// </summary>
        /// <param name="type">Type type to check</param>
        /// <returns>Returns true if the type is primitive, otherwise false</returns>
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

        /// <summary>
        /// Gets the size of the largest primitive type
        /// </summary>
        /// <returns>The size of the largest primitive type</returns>
        public static Int32 GetLargestPrimitiveSize()
        {
            return GetScannablePrimitiveTypes().Select(x => Marshal.SizeOf(x)).Max();
        }
    }
    //// End class
}
//// End namespace