using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// The long line edge display mode.
    /// </summary>
    public enum EdgeMode
    {
        /// <summary>
        /// Long lines are not indicated. This is the default.
        /// </summary>
        None = NativeMethods.EDGE_NONE,

        /// <summary>
        /// Long lines are indicated with a vertical line.
        /// </summary>
        Line = NativeMethods.EDGE_LINE,

        /// <summary>
        /// Long lines are indicated with a background color.
        /// </summary>
        Background = NativeMethods.EDGE_BACKGROUND
    }
}
