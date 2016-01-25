using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Additional location options for line wrapping visual indicators.
    /// </summary>
    public enum WrapVisualFlagLocation
    {
        /// <summary>
        /// Wrap indicators are drawn near the border. This is the default.
        /// </summary>
        Default = NativeMethods.SC_WRAPVISUALFLAGLOC_DEFAULT,

        /// <summary>
        /// Wrap indicators are drawn at the end of sublines near the text.
        /// </summary>
        EndByText = NativeMethods.SC_WRAPVISUALFLAGLOC_END_BY_TEXT,

        /// <summary>
        /// Wrap indicators are drawn at the beginning of sublines near the text.
        /// </summary>
        StartByText = NativeMethods.SC_WRAPVISUALFLAGLOC_START_BY_TEXT
    }
}
