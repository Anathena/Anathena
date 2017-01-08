namespace Ana.Source.Utils.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for IEnumerable interfaces.
    /// </summary>
    internal static class IEnumerableExtensions
    {
        /// <summary>
        /// Determines if a collection is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="enumeration">The enumeration to iterate through.</param>
        /// <returns>True if the collection is null or empty, otherwise false.</returns>
        public static Boolean IsNullOrEmpty<T>(this IEnumerable<T> enumeration)
        {
            return enumeration == null || !enumeration.Any();
        }

        /// <summary>
        /// A foreach extension method to perform an action on all elements in an enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="enumeration">The enumeration to iterate through.</param>
        /// <param name="action">The action to perform for each item.</param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// Gets the type contained in the IEnumerable interface.
        /// </summary>
        /// <param name="source">The enumeration of which we will get the child type.</param>
        /// <returns>A type contained in the IEnumerable interface. Returns null if none found.</returns>
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

        /// <summary>
        /// Adds a single element to the end of an IEnumerable.
        /// </summary>
        /// <typeparam name="T">Type of enumerable to return.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="element">The element to append.</param>
        /// <returns>IEnumerable containing all the input elements, followed by the specified additional element.</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element)
        {
            if (source == null)
            {
                return source;
            }

            return ConcatIterator(element, source, false);
        }

        /// <summary>
        /// Adds a single element to the start of an IEnumerable.
        /// </summary>
        /// <typeparam name="T">Type of enumerable to return.</typeparam>
        /// <param name="tail">The source enumerable.</param>
        /// <param name="head">The element to prepend.</param>
        /// <returns>IEnumerable containing the specified additional element, followed by all the input elements.</returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> tail, T head)
        {
            if (tail == null)
            {
                return tail;
            }

            return ConcatIterator(head, tail, true);
        }

        /// <summary>
        /// Concatenation iterater to allow appending and prepending information.
        /// </summary>
        /// <typeparam name="T">Type of enumerable to return.</typeparam>
        /// <param name="extraElement">The element being added.</param>
        /// <param name="source">The IEnumerable source.</param>
        /// <param name="insertAtStart">Whether or not to append or prepend.</param>
        /// <returns>IEnumerable with all source elements and the new one either appended or prepended.</returns>
        private static IEnumerable<T> ConcatIterator<T>(T extraElement, IEnumerable<T> source, Boolean insertAtStart)
        {
            if (insertAtStart)
            {
                yield return extraElement;
            }

            foreach (T e in source)
            {
                yield return e;
            }

            if (!insertAtStart)
            {
                yield return extraElement;
            }
        }
    }
    //// End class
}
//// End namespace