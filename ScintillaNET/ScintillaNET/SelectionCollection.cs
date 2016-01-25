using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// A multiple selection collection.
    /// </summary>
    public class SelectionCollection : IEnumerable<Selection>
    {
        private readonly Scintilla scintilla;

        /// <summary>
        /// Provides an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An object that contains all <see cref="Selection" /> objects within the <see cref="SelectionCollection" />.</returns>
        public IEnumerator<Selection> GetEnumerator()
        {
            int count = Count;
            for (int i = 0; i < count; i++)
                yield return this[i];

            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the number of active selections.
        /// </summary>
        /// <returns>The number of selections in the <see cref="SelectionCollection" />.</returns>
        public int Count
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONS).ToInt32();
            }
        }

        /// <summary>
        /// Gets a value indicating whether all selection ranges are empty.
        /// </summary>
        /// <returns>true if all selection ranges are empty; otherwise, false.</returns>
        public bool IsEmpty
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONEMPTY) != IntPtr.Zero;
            }
        }

        /// <summary>
        /// Gets the <see cref="Selection" /> at the specified zero-based index.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="Selection" /> to get.</param>
        /// <returns>The <see cref="Selection" /> at the specified index.</returns>
        public Selection this[int index]
        {
            get
            {
                index = Helpers.Clamp(index, 0, Count - 1);
                return new Selection(scintilla, index);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionCollection" /> class.
        /// </summary>
        /// <param name="scintilla"></param>
        public SelectionCollection(Scintilla scintilla)
        {
            this.scintilla = scintilla;
        }
    }
}
