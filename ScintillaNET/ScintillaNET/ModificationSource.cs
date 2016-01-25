using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// The source of a modification
    /// </summary>
    public enum ModificationSource
    {
        /// <summary>
        /// Modification is the result of a user operation.
        /// </summary>
        User = NativeMethods.SC_PERFORMED_USER,

        /// <summary>
        /// Modification is the result of an undo operation.
        /// </summary>
        Undo = NativeMethods.SC_PERFORMED_UNDO,

        /// <summary>
        /// Modification is the result of a redo operation.
        /// </summary>
        Redo = NativeMethods.SC_PERFORMED_REDO
    }
}
