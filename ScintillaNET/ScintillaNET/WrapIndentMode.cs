using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Indenting behavior of wrapped sublines.
    /// </summary>
    public enum WrapIndentMode
    {
        /// <summary>
        /// Wrapped sublines aligned to left of window plus the amount set by <see cref="ScintillaNET.Scintilla.WrapStartIndent" />.
        /// This is the default.
        /// </summary>
        Fixed,

        /// <summary>
        /// Wrapped sublines are aligned to first subline indent.
        /// </summary>
        Same,

        /// <summary>
        /// Wrapped sublines are aligned to first subline indent plus one more level of indentation.
        /// </summary>
        Indent = NativeMethods.SC_WRAPINDENT_INDENT
    }
}
