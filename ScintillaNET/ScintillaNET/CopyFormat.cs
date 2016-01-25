using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Specifies the clipboard formats to copy.
    /// </summary>
    [Flags]
    public enum CopyFormat
    {
        /// <summary>
        /// Copies text to the clipboard in Unicode format.
        /// </summary>
        Text = 1 << 0,

        /// <summary>
        /// Copies text to the clipboard in Rich Text Format (RTF).
        /// </summary>
        Rtf = 1 << 1,

        /// <summary>
        /// Copies text to the clipboard in HyperText Markup Language (HTML) format.
        /// </summary>
        Html = 1 << 2
    }
}
