using System;
using System.Collections.Generic;

namespace Anathema.Source.SystemInternals.OperatingSystems.Windows.Internals
{
    /// <summary>
    /// Class managing objects implementing <see cref="INamedElement"/> interface.
    /// </summary>
    public abstract class Manager<T> where T : INamedElement
    {
        /// <summary>
        /// The collection of the elements (writable).
        /// </summary>
        protected Dictionary<String, T> InternalItems = new Dictionary<String, T>();

        /// <summary>
        /// The collection of the elements.
        /// </summary>
        public IReadOnlyDictionary<String, T> Items
        {
            get { return InternalItems; }
        }

        #region Methods
        #region DisableAll
        /// <summary>
        /// Disables all items in the manager.
        /// </summary>
        public void DisableAll()
        {
            foreach (KeyValuePair<String, T> Item in InternalItems)
            {
                Item.Value.Disable();
            }
        }

        #endregion
        #region EnableAll
        /// <summary>
        /// Enables all items in the manager.
        /// </summary>
        public void EnableAll()
        {
            foreach (KeyValuePair<String, T> Item in InternalItems)
            {
                Item.Value.Enable();
            }
        }

        #endregion
        #region Remove
        /// <summary>
        /// Removes an element by its name in the manager.
        /// </summary>
        /// <param name="Name">The name of the element to remove.</param>
        public void Remove(String Name)
        {
            // Check if the element exists in the dictionary
            if (InternalItems.ContainsKey(Name))
            {
                try
                {
                    // Dispose the element
                    InternalItems[Name].Dispose();
                }
                finally
                {
                    // Remove the element from the dictionary
                    InternalItems.Remove(Name);
                }
            }
        }

        /// <summary>
        /// Remove a given element.
        /// </summary>
        /// <param name="Item">The element to remove.</param>
        public void Remove(T Item)
        {
            Remove(Item.Name);
        }

        #endregion
        #region RemoveAll
        /// <summary>
        /// Removes all the elements in the manager.
        /// </summary>
        public void RemoveAll()
        {
            // For each element
            foreach (KeyValuePair<String, T> Item in InternalItems)
            {
                // Dispose it
                Item.Value.Dispose();
            }
            // Clear the dictionary
            InternalItems.Clear();
        }
        #endregion
        #endregion

    } // End class

} // End namespace