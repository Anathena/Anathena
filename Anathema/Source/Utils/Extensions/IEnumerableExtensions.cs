using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Anathema.Source.Utils.Extensions
{
    static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> Enumeration, Action<T> Action)
        {
            foreach (T Item in Enumeration)
            {
                Action(Item);
            }
        }

        public static Type GetElementType(this IEnumerable Source)
        {
            Type EnumerableType = Source.GetType();

            if (EnumerableType.IsArray)
                return EnumerableType.GetElementType();

            if (EnumerableType.IsGenericType)
                return EnumerableType.GetGenericArguments().First();

            return null;
        }

    } // End class

} // End namespace