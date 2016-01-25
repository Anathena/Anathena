using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.HotspotClick" />, <see cref="Scintilla.HotspotDoubleClick" />,
    /// and <see cref="Scintilla.HotspotReleaseClick" /> events.
    /// </summary>
    public class HotspotClickEventArgs : EventArgs
    {
        private readonly Scintilla scintilla;
        private readonly int bytePosition;
        private int? position;

        /// <summary>
        /// Gets the modifier keys (SHIFT, CTRL, ALT) held down when clicked.
        /// </summary>
        /// <returns>A bitwise combination of the Keys enumeration indicating the modifier keys.</returns>
        /// <remarks>Only the state of the CTRL key is reported in the <see cref="Scintilla.HotspotReleaseClick" /> event.</remarks>
        public Keys Modifiers { get; private set; }

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
        /// Initializes a new instance of the <see cref="HotspotClickEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="modifiers">The modifier keys that where held down at the time of the click.</param>
        /// <param name="bytePosition">The zero-based byte position of the clicked text.</param>
        public HotspotClickEventArgs(Scintilla scintilla, Keys modifiers, int bytePosition)
        {
            this.scintilla = scintilla;
            this.bytePosition = bytePosition;
            Modifiers = modifiers;
        }
    }
}
