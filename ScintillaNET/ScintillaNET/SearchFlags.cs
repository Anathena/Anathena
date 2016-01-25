using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Specifies the how patterns are matched when performing a search in a <see cref="Scintilla" /> control.
    /// </summary>
    /// <remarks>This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</remarks>
    [Flags]
    public enum SearchFlags
    {
        /// <summary>
        /// Matches every instance of the search string.
        /// </summary>
        None = 0,

        /// <summary>
        /// A match only occurs with text that matches the case of the search string.
        /// </summary>
        MatchCase = NativeMethods.SCFIND_MATCHCASE,

        /// <summary>
        /// A match only occurs if the characters before and after are not word characters.
        /// </summary>
        WholeWord = NativeMethods.SCFIND_WHOLEWORD,

        /// <summary>
        /// A match only occurs if the character before is not a word character.
        /// </summary>
        WordStart = NativeMethods.SCFIND_WORDSTART,

        /// <summary>
        /// The search string should be interpreted as a regular expression.
        /// Regular expressions will only match ranges within a single line, never matching over multiple lines.
        /// </summary>
        Regex = NativeMethods.SCFIND_REGEXP,

        /// <summary>
        /// Treat regular expression in a more POSIX compatible manner by interpreting bare '(' and ')' for tagged sections rather than "\(" and "\)".
        /// </summary>
        Posix = NativeMethods.SCFIND_POSIX
    }
}
