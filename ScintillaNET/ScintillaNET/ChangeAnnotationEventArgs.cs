using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.ChangeAnnotation" /> event.
    /// </summary>
    public class ChangeAnnotationEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the line index where the annotation changed.
        /// </summary>
        /// <returns>The zero-based line index where the annotation change occurred.</returns>
        public int Line { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeAnnotationEventArgs" /> class.
        /// </summary>
        /// <param name="line">The zero-based line index of the annotation that changed.</param>
        public ChangeAnnotationEventArgs(int line)
        {
            Line = line;
        }
    }
}
