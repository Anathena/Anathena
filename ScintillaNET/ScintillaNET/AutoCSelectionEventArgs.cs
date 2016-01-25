using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Provides data for the <see cref="Scintilla.AutoCSelection" /> event.
    /// </summary>
    public class AutoCSelectionEventArgs : EventArgs
    {
        private readonly Scintilla scintilla;
        private readonly IntPtr textPtr;
        private readonly int bytePosition;
        private int? position;
        private string text;

        /// <summary>
        /// Gets the fillup character that caused the completion.
        /// </summary>
        /// <returns>The fillup character used to cause the completion; otherwise, 0.</returns>
        /// <remarks>Only a <see cref="ListCompletionMethod" /> of <see cref="ScintillaNET.ListCompletionMethod.FillUp" /> will return a non-zero character.</remarks>
        /// <seealso cref="Scintilla.AutoCSetFillUps" />
        public int Char { get; private set; }

        /// <summary>
        /// Gets a value indicating how the completion occurred.
        /// </summary>
        /// <returns>One of the <see cref="ScintillaNET.ListCompletionMethod" /> enumeration values.</returns>
        public ListCompletionMethod ListCompletionMethod { get; private set; }

        /// <summary>
        /// Gets the start position of the word being completed.
        /// </summary>
        /// <returns>The zero-based document position of the word being completed.</returns>
        public int Position
        {
            get
            {
                if (position == null)
                    position = scintilla.Lines.ByteToCharPosition(bytePosition);

                return (int)position;
            }
        }

        /// <summary>
        /// Gets the text of the selected autocompletion item.
        /// </summary>
        /// <returns>The selected autocompletion item text.</returns>
        public unsafe string Text
        {
            get
            {
                if (text == null)
                {
                    var len = 0;
                    while (((byte*)textPtr)[len] != 0)
                        len++;

                    text = Helpers.GetString(textPtr, len, scintilla.Encoding);
                }

                return text;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCSelectionEventArgs" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
        /// <param name="bytePosition">The zero-based byte position within the document of the word being completed.</param>
        /// <param name="text">A pointer to the selected autocompletion text.</param>
        /// <param name="ch">The character that caused the completion.</param>
        /// <param name="listCompletionMethod">A value indicating the way in which the completion occurred.</param>
        public AutoCSelectionEventArgs(Scintilla scintilla, int bytePosition, IntPtr text, int ch, ListCompletionMethod listCompletionMethod)
        {
            this.scintilla = scintilla;
            this.bytePosition = bytePosition;
            this.textPtr = text;
            Char = ch;
            ListCompletionMethod = listCompletionMethod;
        }
    }
}
