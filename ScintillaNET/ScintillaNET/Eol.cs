using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// End-of-line format.
    /// </summary>
    public enum Eol
    {
        /// <summary>
        /// Carriage Return, Line Feed pair "\r\n" (0x0D0A).
        /// </summary>
        CrLf = NativeMethods.SC_EOL_CRLF,

        /// <summary>
        /// Carriage Return '\r' (0x0D).
        /// </summary>
        Cr = NativeMethods.SC_EOL_CR,

        /// <summary>
        /// Line Feed '\n' (0x0A).
        /// </summary>
        Lf = NativeMethods.SC_EOL_LF
    }
}
