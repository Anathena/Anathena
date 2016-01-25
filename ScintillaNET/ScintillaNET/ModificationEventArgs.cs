using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.Insert" /> and <see cref="Scintilla.Delete" /> events.
    /// </summary>
    public class ModificationEventArgs : BeforeModificationEventArgs
    {
        private readonly Scintilla scintilla;
        private readonly int bytePosition;
        private readonly int byteLength;
        private readonly IntPtr textPtr;

        /// <summary>
        /// Gets the number of lines added or removed.
        /// </summary>
        /// <returns>The number of lines added to the document when text is inserted, or the number of lines removed from the document when text is deleted.</returns>
        /// <remarks>When lines are deleted the return value will be negative.</remarks>
        public int LinesAdded { get; private set; }

        /// <summary>
        /// Gets the text that was inserted or deleted.
        /// </summary>
        /// <returns>The text inserted or deleted from the document.</returns>
        public override unsafe string Text
        {
            get
            {
                if (CachedText == null)
                    CachedText = Helpers.GetString(textPtr, byteLength, scintilla.Encoding);

                return CachedText;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModificationEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="source">The source of the modification.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where text was modified.</param>
        /// <param name="byteLength">The length in bytes of the inserted or deleted text.</param>
        /// <param name="text">>A pointer to the text inserted or deleted.</param>
        /// <param name="linesAdded">The number of lines added or removed (delta).</param>
        public ModificationEventArgs(Scintilla scintilla, ModificationSource source, int bytePosition, int byteLength, IntPtr text, int linesAdded) : base(scintilla, source, bytePosition, byteLength, text)
        {
            this.scintilla = scintilla;
            this.bytePosition = bytePosition;
            this.byteLength = byteLength;
            this.textPtr = text;

            LinesAdded = linesAdded;
        }
    }
}
