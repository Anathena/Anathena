using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// The display of a cursor when over a margin.
    /// </summary>
    public enum MarginCursor
    {
        /// <summary>
        /// A normal arrow.
        /// </summary>
        Arrow = NativeMethods.SC_CURSORARROW,

        /// <summary>
        /// A reversed arrow.
        /// </summary>
        ReverseArrow = NativeMethods.SC_CURSORREVERSEARROW
    }
}
