using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.IndicatorClick" /> event.
    /// </summary>
    public class IndicatorClickEventArgs : IndicatorReleaseEventArgs
    {
        /// <summary>
        /// Gets the modifier keys (SHIFT, CTRL, ALT) held down when clicked.
        /// </summary>
        /// <returns>A bitwise combination of the Keys enumeration indicating the modifier keys.</returns>
        public Keys Modifiers { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndicatorClickEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="modifiers">The modifier keys that where held down at the time of the click.</param>
        /// <param name="bytePosition">The zero-based byte position of the clicked text.</param>
        public IndicatorClickEventArgs(Scintilla scintilla, Keys modifiers, int bytePosition) : base(scintilla, bytePosition)
        {
            Modifiers = modifiers;
        }
    }
}
