namespace Ana.Source.Utils.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for IEnumerable interfaces
    /// </summary>
    static class IEnumerableExtensions
    {
        /// <summary>
        /// This shouldn't exist, this was a bad idea
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T Item in enumeration)
            {
                action(Item);
            }
        }

        /// <summary>
        /// Gets the type contained in the IEnumerable interface
        /// </summary>
        /// <param name="source"></param>
        /// <returns>A type contained in the IEnumerable interface. Returns null if none found</returns>
        public static Type GetElementType(this IEnumerable source)
        {
            Type enumerableType = source.GetType();

            if (enumerableType.IsArray)
            {
                return enumerableType.GetElementType();
            }

            if (enumerableType.IsGenericType)
            {
                return enumerableType.GetGenericArguments().First();
            }

            return null;
        }

    }
    //// End class
}
//// End namespace