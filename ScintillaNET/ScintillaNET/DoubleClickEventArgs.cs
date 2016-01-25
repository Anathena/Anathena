using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.DoubleClick" /> event.
    /// </summary>
    public class DoubleClickEventArgs : EventArgs
    {
        private readonly Scintilla scintilla;
        private readonly int bytePosition;
        private int? position;

        /// <summary>
        /// Gets the line double clicked.
        /// </summary>
        /// <returns>The zero-based index of the double clicked line.</returns>
        public int Line { get; private set; }

        /// <summary>
        /// Gets the modifier keys (SHIFT, CTRL, ALT) held down when double clicked.
        /// </summary>
        /// <returns>A bitwise combination of the Keys enumeration indicating the modifier keys.</returns>
        public Keys Modifiers { get; private set; }

        /// <summary>
        /// Gets the zero-based document position of the text double clicked.
        /// </summary>
        /// <returns>
        /// The zero-based character position within the document of the double clicked text;
        /// otherwise, -1 if not a document position.
        /// </returns>
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
        /// Initializes a new instance of the <see cref="DoubleClickEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="modifiers">The modifier keys that where held down at the time of the double click.</param>
        /// <param name="bytePosition">The zero-based byte position of the double clicked text.</param>
        /// <param name="line">The zero-based line index of the double clicked text.</param>
        public DoubleClickEventArgs(Scintilla scintilla, Keys modifiers, int bytePosition, int line)
        {
            this.scintilla = scintilla;
            this.bytePosition = bytePosition;
            Modifiers = modifiers;
            Line = line;

            if (bytePosition == -1)
                position = -1;
        }
    }
}
