using System;
using System.Collections.Generic;

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

    } // End class

} // End namespace