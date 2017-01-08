namespace Ana.Source.Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Extension methods for dependency objects.
    /// </summary>
    internal static class DependencyObjectExtensions
    {
        /// <summary>
        /// Gets all decendent dependency objects from the provided dependency object.
        /// </summary>
        /// <param name="root">The root dependency object.</param>
        /// <param name="depth">The maximum search depth.</param>
        /// <returns>A collection of all descendent dependency objects found.</returns>
        public static IEnumerable<DependencyObject> Descendents(this DependencyObject root, Int32 depth)
        {
            Int32 count = VisualTreeHelper.GetChildrenCount(root);
            for (Int32 index = 0; index < count; index++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(root, index);
                yield return child;

                if (depth > 0)
                {
                    foreach (var descendent in Descendents(child, --depth))
                    {
                        yield return descendent;
                    }
                }
            }
        }

        /// <summary>
        /// Gets all decendent dependency objects from the provided dependency object.
        /// </summary>
        /// <param name="root">The root dependency object.</param>
        /// <returns>A collection of all descendent dependency objects found.</returns>
        public static IEnumerable<DependencyObject> Descendents(this DependencyObject root)
        {
            return Descendents(root, Int32.MaxValue);
        }

        /// <summary>
        /// Gets all ancestor dependency objects from the provided dependency object.
        /// </summary>
        /// <param name="root">The root dependency object.</param>
        /// <returns>A collection of all ancestor dependency objects found.</returns>
        public static IEnumerable<DependencyObject> Ancestors(this DependencyObject root)
        {
            DependencyObject current = VisualTreeHelper.GetParent(root);
            while (current != null)
            {
                yield return current;
                current = VisualTreeHelper.GetParent(current);
            }
        }
    }
    //// End class
}
//// End namespace