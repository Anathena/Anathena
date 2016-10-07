namespace Ana.Source.Utils.VirtualItemProvider
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a provider of collection details.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection</typeparam>
    internal interface IItemsProvider<T>
    {
        /// <summary>
        /// Gets the total number of items available
        /// </summary>
        /// <returns>The total number of items available</returns>
        Int32 GetCount();

        /// <summary>
        /// Retrieves a continguous collection of items from a larger collection
        /// </summary>
        /// <param name="startIndex">The start index</param>
        /// <param name="count">The number of items to fetch</param>
        /// <returns>A collection of items taken from the main collection</returns>
        IList<T> GetItems(Int32 startIndex, Int32 count);
    }
    //// End class
}
//// End namespace