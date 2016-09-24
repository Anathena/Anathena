namespace Ana.Source.Utils.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for IEnumerable interfaces
    /// </summary>
    internal static class IEnumerableExtensions
    {
        /// <summary>
        /// A foreach extension method to perform an action on all elements in an enumeration
        /// </summary>
        /// <typeparam name="T">The type of the enumeration</typeparam>
        /// <param name="enumeration">The enumeration to iterate through</param>
        /// <param name="action">The action to perform for each item</param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// Gets the type contained in the IEnumerable interface
        /// </summary>
        /// <param name="source">The enumeration to get the child type of</param>
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