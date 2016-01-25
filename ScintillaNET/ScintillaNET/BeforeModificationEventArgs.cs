using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.BeforeInsert" /> and <see cref="Scintilla.BeforeDelete" /> events.
    /// </summary>
    public class BeforeModificationEventArgs : EventArgs
    {
        private readonly Scintilla scintilla;
        private readonly int bytePosition;
        private readonly int byteLength;
        private readonly IntPtr textPtr;

        internal int? CachedPosition { get; set; }
        internal string CachedText { get; set; }

        /// <summary>
        /// Gets the zero-based document position where the modification will occur.
        /// </summary>
        /// <returns>The zero-based character position within the document where text will be inserted or deleted.</returns>
        public int Position
        {
            get
            {
                if (CachedPosition == null)
                    CachedPosition = scintilla.Lines.ByteToCharPosition(bytePosition);

                return (int)CachedPosition;
            }
        }

        /// <summary>
        /// Gets the source of the modification.
        /// </summary>
        /// <returns>One of the <see cref="ModificationSource" /> enum values.</returns>
        public ModificationSource Source { get; private set; }

        /// <summary>
        /// Gets the text being inserted or deleted.
        /// </summary>
        /// <returns>
        /// The text about to be inserted or deleted, or null when the the source of the modification is an undo/redo operation.
        /// </returns>
        /// <remarks>
        /// This property will return null when <see cref="Source" /> is <see cref="ModificationSource.Undo" /> or <see cref="ModificationSource.Redo" />.
        /// </remarks>
        public unsafe virtual string Text
        {
            get
            {
                if (Source != ModificationSource.User)
                    return null;

                if (CachedText == null)
                {
                    // For some reason the Scintilla overlords don't provide text in
                    // SC_MOD_BEFOREDELETE... but we can get it from the document.
                    if (textPtr == IntPtr.Zero)
                    {
                        var ptr = scintilla.DirectMessage(NativeMethods.SCI_GETRANGEPOINTER, new IntPtr(bytePosition), new IntPtr(byteLength));
                        CachedText = new string((sbyte*)ptr, 0, byteLength, scintilla.Encoding);
                    }
                    else
                    {
                        CachedText = Helpers.GetString(textPtr, byteLength, scintilla.Encoding);
                    }
                }

                return CachedText;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeforeModificationEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="source">The source of the modification.</param>
        /// <param name="bytePosition">The zero-based byte position within the document where text is being modified.</param>
        /// <param name="byteLength">The length in bytes of the text being modified.</param>
        /// <param name="text">A pointer to the text being inserted.</param>
        public BeforeModificationEventArgs(Scintilla scintilla, ModificationSource source, int bytePosition, int byteLength, IntPtr text)
        {
            this.scintilla = scintilla;
            this.bytePosition = bytePosition;
            this.byteLength = byteLength;
            this.textPtr = text;

            Source = source;
        }
    }
}
