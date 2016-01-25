using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// The possible casing styles of a style.
    /// </summary>
    public enum StyleCase
    {
        /// <summary>
        /// Display the text normally.
        /// </summary>
        Mixed = NativeMethods.SC_CASE_MIXED,

        /// <summary>
        /// Display the text in upper case.
        /// </summary>
        Upper = NativeMethods.SC_CASE_UPPER,

        /// <summary>
        /// Display the text in lower case.
        /// </summary>
        Lower = NativeMethods.SC_CASE_LOWER,

        /// <summary>
        /// Display the text in camel case.
        /// </summary>
        Camel = NativeMethods.SC_CASE_CAMEL
    }
}
