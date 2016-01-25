using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// The line wrapping strategy.
    /// </summary>
    public enum WrapMode
    {
        /// <summary>
        /// Line wrapping is disabled. This is the default.
        /// </summary>
        None = NativeMethods.SC_WRAP_NONE,

        /// <summary>
        /// Lines are wrapped on word or style boundaries.
        /// </summary>
        Word = NativeMethods.SC_WRAP_WORD,

        /// <summary>
        /// Lines are wrapped between any character.
        /// </summary>
        Char = NativeMethods.SC_WRAP_CHAR,

        /// <summary>
        /// Lines are wrapped on whitespace.
        /// </summary>
        Whitespace = NativeMethods.SC_WRAP_WHITESPACE
    }
}
