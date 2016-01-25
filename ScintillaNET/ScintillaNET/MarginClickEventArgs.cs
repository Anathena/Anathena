using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.MarginClick" /> event.
    /// </summary>
    public class MarginClickEventArgs : EventArgs
    {
        private readonly Scintilla scintilla;
        private readonly int bytePosition;
        private int? position;

        /// <summary>
        /// Gets the margin clicked.
        /// </summary>
        /// <returns>The zero-based index of the clicked margin.</returns>
        public int Margin { get; private set; }

        /// <summary>
        /// Gets the modifier keys (SHIFT, CTRL, ALT) held down when the margin was clicked.
        /// </summary>
        /// <returns>A bitwise combination of the Keys enumeration indicating the modifier keys.</returns>
        public Keys Modifiers { get; private set; }

        /// <summary>
        /// Gets the zero-based document position where the line ajacent to the clicked margin starts.
        /// </summary>
        /// <returns>The zero-based character position within the document of the start of the line adjacent to the margin clicked.</returns>
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
        /// Initializes a new instance of the <see cref="MarginClickEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="modifiers">The modifier keys that where held down at the time of the margin click.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where the line adjacent to the clicked margin starts.</param>
        /// <param name="margin">The zero-based index of the clicked margin.</param>
        public MarginClickEventArgs(Scintilla scintilla, Keys modifiers, int bytePosition, int margin)
        {
            this.scintilla = scintilla;
            this.bytePosition = bytePosition;
            Modifiers = modifiers;
            Margin = margin;
        }
    }
}
