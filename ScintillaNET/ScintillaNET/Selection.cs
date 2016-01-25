using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Represents a selection when there are multiple active selections in a <see cref="Scintilla" /> control.
    /// </summary>
    public class Selection
    {
        private readonly Scintilla scintilla;

        /// <summary>
        /// Gets or sets the anchor position of the selection.
        /// </summary>
        /// <returns>The zero-based document position of the selection anchor.</returns>
        public int Anchor
        {
            get
            {
                var pos = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNANCHOR, new IntPtr(Index)).ToInt32();
                if (pos <= 0)
                    return pos;

                return scintilla.Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, scintilla.TextLength);
                value = scintilla.Lines.CharToBytePosition(value);
                scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNANCHOR, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the amount of anchor virtual space.
        /// </summary>
        /// <returns>The amount of virtual space past the end of the line offsetting the selection anchor.</returns>
        public int AnchorVirtualSpace
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNANCHORVIRTUALSPACE, new IntPtr(Index)).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNANCHORVIRTUALSPACE, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the caret position of the selection.
        /// </summary>
        /// <returns>The zero-based document position of the selection caret.</returns>
        public int Caret
        {
            get
            {
                var pos = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNCARET, new IntPtr(Index)).ToInt32();
                if (pos <= 0)
                    return pos;

                return scintilla.Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, scintilla.TextLength);
                value = scintilla.Lines.CharToBytePosition(value);
                scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNCARET, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the amount of caret virtual space.
        /// </summary>
        /// <returns>The amount of virtual space past the end of the line offsetting the selection caret.</returns>
        public int CaretVirtualSpace
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNCARETVIRTUALSPACE, new IntPtr(Index)).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNCARETVIRTUALSPACE, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the end position of the selection.
        /// </summary>
        /// <returns>The zero-based document position where the selection ends.</returns>
        public int End
        {
            get
            {
                var pos = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNEND, new IntPtr(Index)).ToInt32();
                if (pos <= 0)
                    return pos;

                return scintilla.Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, scintilla.TextLength);
                value = scintilla.Lines.CharToBytePosition(value);
                scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNEND, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets the selection index.
        /// </summary>
        /// <returns>The zero-based selection index within the <see cref="SelectionCollection" /> that created it.</returns>
        public int Index { get; private set; }

        /// <summary>
        /// Gets or sets the start position of the selection.
        /// </summary>
        /// <returns>The zero-based document position where the selection starts.</returns>
        public int Start
        {
            get
            {
                var pos = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONNSTART, new IntPtr(Index)).ToInt32();
                if (pos <= 0)
                    return pos;

                return scintilla.Lines.ByteToCharPosition(pos);
            }
            set
            {
                value = Helpers.Clamp(value, 0, scintilla.TextLength);
                value = scintilla.Lines.CharToBytePosition(value);
                scintilla.DirectMessage(NativeMethods.SCI_SETSELECTIONNSTART, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Selection" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that created this selection.</param>
        /// <param name="index">The index of this selection within the <see cref="SelectionCollection" /> that created it.</param>
        public Selection(Scintilla scintilla, int index)
        {
            this.scintilla = scintilla;
            Index = index;
        }
    }
}
