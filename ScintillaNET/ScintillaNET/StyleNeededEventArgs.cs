using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.StyleNeeded" /> event.
    /// </summary>
    public class StyleNeededEventArgs : EventArgs
    {
        private readonly Scintilla scintilla;
        private readonly int bytePosition;
        private int? position;

        /// <summary>
        /// Gets the document position where styling should end. The <see cref="Scintilla.GetEndStyled" /> method
        /// indicates the last position styled correctly and the starting place for where styling should begin.
        /// </summary>
        /// <returns>The zero-based position within the document to perform styling up to.</returns>
        public int Position
        {
            get
            {
                if (position == null)
                    position = scintilla.Lines.ByteToCharPosition(bytePosition);

                return (int)position;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleNeededEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="bytePosition">The zero-based byte position within the document to stop styling.</param>
        public StyleNeededEventArgs(Scintilla scintilla, int bytePosition)
        {
            this.scintilla = scintilla;
            this.bytePosition = bytePosition;
        }
    }
}
