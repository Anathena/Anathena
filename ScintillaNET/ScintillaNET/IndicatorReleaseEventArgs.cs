using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.IndicatorRelease" /> event.
    /// </summary>
    public class IndicatorReleaseEventArgs : EventArgs
    {
        private readonly Scintilla scintilla;
        private readonly int bytePosition;
        private int? position;

        /// <summary>
        /// Gets the zero-based document position of the text clicked.
        /// </summary>
        /// <returns>The zero-based character position within the document of the clicked text.</returns>
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
        /// Initializes a new instance of the <see cref="IndicatorReleaseEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="bytePosition">The zero-based byte position of the clicked text.</param>
        public IndicatorReleaseEventArgs(Scintilla scintilla, int bytePosition)
        {
            this.scintilla = scintilla;
            this.bytePosition = bytePosition;
        }
    }
}
