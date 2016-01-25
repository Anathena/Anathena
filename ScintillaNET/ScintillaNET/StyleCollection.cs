using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// An immutable collection of style definitions in a <see cref="Scintilla" /> control.
    /// </summary>
    public class StyleCollection : IEnumerable<Style>
    {
        private readonly Scintilla scintilla;

        /// <summary>
        /// Provides an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An object that contains all <see cref="Style" /> objects within the <see cref="StyleCollection" />.</returns>
        public IEnumerator<Style> GetEnumerator()
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
        /// Gets the number of styles.
        /// </summary>
        /// <returns>The number of styles in the <see cref="StyleCollection" />.</returns>
        public int Count
        {
            get
            {
                return (NativeMethods.STYLE_MAX + 1);
            }
        }

        /// <summary>
        /// Gets a <see cref="Style" /> object at the specified index.
        /// </summary>
        /// <param name="index">The style definition index.</param>
        /// <returns>An object representing the style definition at the specified <paramref name="index" />.</returns>
        /// <remarks>Styles 32 through 39 have special significance.</remarks>
        public Style this[int index]
        {
            get
            {
                index = Helpers.Clamp(index, 0, Count - 1);
                return new Style(scintilla, index);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleCollection" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
        public StyleCollection(Scintilla scintilla)
        {
            this.scintilla = scintilla;
        }
    }
}
