using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Lexer property types.
    /// </summary>
    public enum PropertyType
    {
        /// <summary>
        /// A Boolean property. This is the default.
        /// </summary>
        Boolean = NativeMethods.SC_TYPE_BOOLEAN,

        /// <summary>
        /// An integer property.
        /// </summary>
        Integer = NativeMethods.SC_TYPE_INTEGER,

        /// <summary>
        /// A string property.
        /// </summary>
        String = NativeMethods.SC_TYPE_STRING
    }
}
