using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.CharAdded" /> event.
    /// </summary>
    public class CharAddedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the text character added to a <see cref="Scintilla" /> control.
        /// </summary>
        /// <returns>The character added.</returns>
        public int Char { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharAddedEventArgs" /> class.
        /// </summary>
        /// <param name="ch">The character added.</param>
        public CharAddedEventArgs(int ch)
        {
            Char = ch;
        }
    }
}
