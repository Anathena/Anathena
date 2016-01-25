using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// The rendering technology used in a <see cref="Scintilla" /> control.
    /// </summary>
    public enum Technology
    {
        /// <summary>
        /// Renders text using GDI. This is the default.
        /// </summary>
        Default = NativeMethods.SC_TECHNOLOGY_DEFAULT,

        /// <summary>
        /// Renders text using Direct2D/DirectWrite. Since Direct2D buffers drawing,
        /// Scintilla's buffering can be turned off with <see cref="Scintilla.BufferedDraw" />.
        /// </summary>
        DirectWrite = NativeMethods.SC_TECHNOLOGY_DIRECTWRITE
    }
}
