using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Flags for additional line fold level behavior.
    /// </summary>
    [Flags]
    public enum FoldLevelFlags
    {
        /// <summary>
        /// Indicates that the line is blank and should be treated slightly different than its level may indicate;
        /// otherwise, blank lines should generally not be fold points.
        /// </summary>
        White = NativeMethods.SC_FOLDLEVELWHITEFLAG,

        /// <summary>
        /// Indicates that the line is a header (fold point).
        /// </summary>
        Header = NativeMethods.SC_FOLDLEVELHEADERFLAG
    }
}
