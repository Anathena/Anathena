using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.DwellStart" /> and <see cref="Scintilla.DwellEnd" /> events.
    /// </summary>
    public class DwellEventArgs : EventArgs
    {
        private readonly Scintilla scintilla;
        private readonly int bytePosition;
        private int? position;

        /// <summary>
        /// Gets the zero-based document position where the mouse pointer was lingering.
        /// </summary>
        /// <returns>The nearest zero-based document position to where the mouse pointer was lingering.</returns>
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
        /// Gets the x-coordinate of the mouse pointer.
        /// </summary>
        /// <returns>The x-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</returns>
        public int X { get; private set; }

        /// <summary>
        /// Gets the y-coordinate of the mouse pointer.
        /// </summary>
        /// <returns>The y-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</returns>
        public int Y { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DwellEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where the mouse pointer was lingering.</param>
        /// <param name="x">The x-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</param>
        /// <param name="y">The y-coordinate of the mouse pointer relative to the <see cref="Scintilla" /> control.</param>
        public DwellEventArgs(Scintilla scintilla, int bytePosition, int x, int y)
        {
            this.scintilla = scintilla;
            this.bytePosition = bytePosition;
            X = x;
            Y = y;

            // The position is not over text
            if (bytePosition < 0)
                position = bytePosition;
        }
    }
}
