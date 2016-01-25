using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Enables virtual space for rectangular selections or in other circumstances or in both.
    /// </summary>
    /// <remarks>This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</remarks>
    [Flags]
    public enum VirtualSpace
    {
        /// <summary>
        /// Virtual space is not enabled. This is the default.
        /// </summary>
        None = NativeMethods.SCVS_NONE,

        /// <summary>
        /// Virtual space is enabled for rectangular selections.
        /// </summary>
        RectangularSelection = NativeMethods.SCVS_RECTANGULARSELECTION,

        /// <summary>
        /// Virtual space is user accessible.
        /// </summary>
        UserAccessible = NativeMethods.SCVS_USERACCESSIBLE
    }
}
