using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Represents a line of text in a <see cref="Scintilla" /> control.
    /// </summary>
    public class Line
    {
        #region Fields

        private readonly Scintilla scintilla;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Expands any parent folds to ensure the line is visible.
        /// </summary>
        public void EnsureVisible()
        {
            scintilla.DirectMessage(NativeMethods.SCI_ENSUREVISIBLE, new IntPtr(Index));
        }

        //public void ExpandChildren(int level)
        //{
        //}

        /// <summary>
        /// Performs the specified fold action on the current line and all child lines.
        /// </summary>
        /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
        public void FoldChildren(FoldAction action)
        {
            scintilla.DirectMessage(NativeMethods.SCI_FOLDCHILDREN, new IntPtr(Index), new IntPtr((int)action));
        }

        /// <summary>
        /// Performs the specified fold action on the current line.
        /// </summary>
        /// <param name="action">One of the <see cref="FoldAction" /> enumeration values.</param>
        public void FoldLine(FoldAction action)
        {
            scintilla.DirectMessage(NativeMethods.SCI_FOLDLINE, new IntPtr(Index), new IntPtr((int)action));
        }

        /// <summary>
        /// Searches for the next line that has a folding level that is less than or equal to <paramref name="level" />
        /// and returns the previous line index.
        /// </summary>
        /// <param name="level">The level of the line to search for. A value of -1 will use the current line <see cref="FoldLevel" />.</param>
        /// <returns>
        /// The zero-based index of the next line that has a <see cref="FoldLevel" /> less than or equal
        /// to <paramref name="level" />. If the current line is a fold point and <paramref name="level"/> is -1 the
        /// index returned is the last line that would be made visible or hidden by toggling the fold state.
        /// </returns>
        public int GetLastChild(int level)
        {
            return scintilla.DirectMessage(NativeMethods.SCI_GETLASTCHILD, new IntPtr(Index), new IntPtr(level)).ToInt32();
        }

        /// <summary>
        /// Navigates the caret to the start of the line.
        /// </summary>
        /// <remarks>Any selection is discarded.</remarks>
        public void Goto()
        {
            scintilla.DirectMessage(NativeMethods.SCI_GOTOLINE, new IntPtr(Index));
        }

        /// <summary>
        /// Adds the specified <see cref="Marker" /> to the line.
        /// </summary>
        /// <param name="marker">The zero-based index of the marker to add to the line.</param>
        /// <returns>A <see cref="MarkerHandle" /> which can be used to track the line.</returns>
        /// <remarks>This method does not check if the line already contains the <paramref name="marker" />.</remarks>
        public MarkerHandle MarkerAdd(int marker)
        {
            marker = Helpers.Clamp(marker, 0, scintilla.Markers.Count - 1);
            var handle = scintilla.DirectMessage(NativeMethods.SCI_MARKERADD, new IntPtr(Index), new IntPtr(marker));
            return new MarkerHandle { Value = handle };
        }

        /// <summary>
        /// Adds one or more markers to the line in a single call using a bit mask.
        /// </summary>
        /// <param name="markerMask">An unsigned 32-bit value with each bit cooresponding to one of the 32 zero-based <see cref="Margin" /> indexes to add.</param>
        public void MarkerAddSet(uint markerMask)
        {
            var mask = unchecked((int)markerMask);
            scintilla.DirectMessage(NativeMethods.SCI_MARKERADDSET, new IntPtr(Index), new IntPtr(mask));
        }

        /// <summary>
        /// Removes the specified <see cref="Marker" /> from the line.
        /// </summary>
        /// <param name="marker">The zero-based index of the marker to remove from the line or -1 to delete all markers from the line.</param>
        /// <remarks>If the same marker has been added to the line more than once, this will delete one copy each time it is used.</remarks>
        public void MarkerDelete(int marker)
        {
            marker = Helpers.Clamp(marker, -1, scintilla.Markers.Count - 1);
            scintilla.DirectMessage(NativeMethods.SCI_MARKERDELETE, new IntPtr(Index), new IntPtr(marker));
        }

        /// <summary>
        /// Returns a bit mask indicating which markers are present on the line.
        /// </summary>
        /// <returns>An unsigned 32-bit value with each bit cooresponding to one of the 32 zero-based <see cref="Margin" /> indexes.</returns>
        public uint MarkerGet()
        {
            var mask = scintilla.DirectMessage(NativeMethods.SCI_MARKERGET, new IntPtr(Index)).ToInt32();
            return unchecked((uint)mask);
        }

        /// <summary>
        /// Efficiently searches from the current line forward to the end of the document for the specified markers.
        /// </summary>
        /// <param name="markerMask">An unsigned 32-bit value with each bit cooresponding to one of the 32 zero-based <see cref="Margin" /> indexes.</param>
        /// <returns>If found, the zero-based line index containing one of the markers in <paramref name="markerMask" />; otherwise, -1.</returns>
        /// <remarks>For example, the mask for marker index 10 is 1 shifted left 10 times (1 &lt;&lt; 10).</remarks>
        public int MarkerNext(uint markerMask)
        {
            var mask = unchecked((int)markerMask);
            return scintilla.DirectMessage(NativeMethods.SCI_MARKERNEXT, new IntPtr(Index), new IntPtr(mask)).ToInt32();
        }

        /// <summary>
        /// Efficiently searches from the current line backward to the start of the document for the specified markers.
        /// </summary>
        /// <param name="markerMask">An unsigned 32-bit value with each bit cooresponding to one of the 32 zero-based <see cref="Margin" /> indexes.</param>
        /// <returns>If found, the zero-based line index containing one of the markers in <paramref name="markerMask" />; otherwise, -1.</returns>
        /// <remarks>For example, the mask for marker index 10 is 1 shifted left 10 times (1 &lt;&lt; 10).</remarks>
        public int MarkerPrevious(uint markerMask)
        {
            var mask = unchecked((int)markerMask);
            return scintilla.DirectMessage(NativeMethods.SCI_MARKERPREVIOUS, new IntPtr(Index), new IntPtr(mask)).ToInt32();
        }

        /// <summary>
        /// Toggles the folding state of the line; expanding or contracting all child lines.
        /// </summary>
        /// <remarks>The line must be set as a <see cref="ScintillaNET.FoldLevelFlags.Header" />.</remarks>
        public void ToggleFold()
        {
            scintilla.DirectMessage(NativeMethods.SCI_TOGGLEFOLD, new IntPtr(Index));
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets the number of annotation lines of text.
        /// </summary>
        /// <returns>The number of annotation lines.</returns>
        public int AnnotationLines
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETLINES, new IntPtr(Index)).ToInt32();
            }
        }

        /// <summary>
        /// Gets or sets the style of the annotation text.
        /// </summary>
        /// <returns>
        /// The zero-based index of the annotation text <see cref="Style" /> or 256 when <see cref="AnnotationStyles" />
        /// has been used to set individual character styles.
        /// </returns>
        /// <seealso cref="AnnotationStyles" />
        public int AnnotationStyle
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETSTYLE, new IntPtr(Index)).ToInt32();
            }
            set
            {
                value = Helpers.Clamp(value, 0, scintilla.Styles.Count - 1);
                scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONSETSTYLE, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets an array of style indexes corresponding to each charcter in the <see cref="AnnotationText" />
        /// so that each character may be individually styled.
        /// </summary>
        /// <returns>
        /// An array of <see cref="Style" /> indexes corresponding with each annotation text character or an uninitialized
        /// array when <see cref="AnnotationStyle" /> has been used to set a single style for all characters.
        /// </returns>
        /// <remarks>
        /// <see cref="AnnotationText" /> must be set prior to setting this property.
        /// The <paramref name="value" /> specified should have a length equal to the <see cref="AnnotationText" /> length to properly style all characters.
        /// </remarks>
        /// <seealso cref="AnnotationStyle" />
        public unsafe byte[] AnnotationStyles
        {
            get
            {
                var length = scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETTEXT, new IntPtr(Index)).ToInt32();
                if (length == 0)
                    return new byte[0];

                var text = new byte[length + 1];
                var styles = new byte[length + 1];

                fixed (byte* textPtr = text)
                fixed (byte* stylePtr = styles)
                {
                    scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETTEXT, new IntPtr(Index), new IntPtr(textPtr));
                    scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETSTYLES, new IntPtr(Index), new IntPtr(stylePtr));

                    return Helpers.ByteToCharStyles(stylePtr, textPtr, length, scintilla.Encoding);
                }
            }
            set
            {
                var length = scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETTEXT, new IntPtr(Index)).ToInt32();
                if (length == 0)
                    return;

                var text = new byte[length + 1];
                fixed (byte* textPtr = text)
                {
                    scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETTEXT, new IntPtr(Index), new IntPtr(textPtr));

                    var styles = Helpers.CharToByteStyles(value ?? new byte[0], textPtr, length, scintilla.Encoding);
                    fixed (byte* stylePtr = styles)
                        scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONSETSTYLES, new IntPtr(Index), new IntPtr(stylePtr));
                }
            }
        }

        /// <summary>
        /// Gets or sets the line annotation text.
        /// </summary>
        /// <returns>A String representing the line annotation text.</returns>
        public unsafe string AnnotationText
        {
            get
            {
                var length = scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETTEXT, new IntPtr(Index)).ToInt32();
                if (length == 0)
                    return string.Empty;

                var bytes = new byte[length + 1];
                fixed (byte* bp = bytes)
                {
                    scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETTEXT, new IntPtr(Index), new IntPtr(bp));
                    return Helpers.GetString(new IntPtr(bp), length, scintilla.Encoding);
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    // Scintilla docs suggest that setting to NULL rather than an empty string will free memory
                    scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONGETTEXT, new IntPtr(Index), IntPtr.Zero);
                }
                else
                {
                    var bytes = Helpers.GetBytes(value, scintilla.Encoding, zeroTerminated: true);
                    fixed (byte* bp = bytes)
                        scintilla.DirectMessage(NativeMethods.SCI_ANNOTATIONSETTEXT, new IntPtr(Index), new IntPtr(bp));
                }
            }
        }

        /// <summary>
        /// Searches from the current line to find the index of the next contracted fold header.
        /// </summary>
        /// <returns>The zero-based line index of the next contracted folder header.</returns>
        /// <remarks>If the current line is contracted the current line index is returned.</remarks>
        public int ContractedFoldNext
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_CONTRACTEDFOLDNEXT, new IntPtr(Index)).ToInt32();
            }
        }

        /// <summary>
        /// Gets the zero-based index of the line as displayed in a <see cref="Scintilla" /> control
        /// taking into consideration folded (hidden) lines.
        /// </summary>
        /// <returns>The zero-based display line index.</returns>
        /// <seealso cref="Scintilla.DocLineFromVisible" />
        public int DisplayIndex
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_VISIBLEFROMDOCLINE, new IntPtr(Index)).ToInt32();
            }
        }

        /// <summary>
        /// Gets the zero-based character position in the document where the line ends (exclusive).
        /// </summary>
        /// <returns>The equivalent of <see cref="Position" /> + <see cref="Length" />.</returns>
        public int EndPosition
        {
            get
            {
                return Position + Length;
            }
        }

        /// <summary>
        /// Gets or sets the expanded state (not the visible state) of the line.
        /// </summary>
        /// <remarks>
        /// For toggling the fold state of a single line the <see cref="ToggleFold" /> method should be used.
        /// This property is useful for toggling the state of many folds without updating the display until finished.
        /// </remarks>
        /// <seealso cref="ToggleFold" />
        public bool Expanded
        {
            get
            {
                return (scintilla.DirectMessage(NativeMethods.SCI_GETFOLDEXPANDED, new IntPtr(Index)) != IntPtr.Zero);
            }
            set
            {
                var expanded = (value ? new IntPtr(1) : IntPtr.Zero);
                scintilla.DirectMessage(NativeMethods.SCI_SETFOLDEXPANDED, new IntPtr(Index), expanded);
            }
        }

        /// <summary>
        /// Gets or sets the fold level of the line.
        /// </summary>
        /// <returns>The fold level ranging from 0 to 4095. The default is 1024.</returns>
        public int FoldLevel
        {
            get
            {
                var level = scintilla.DirectMessage(NativeMethods.SCI_GETFOLDLEVEL, new IntPtr(Index)).ToInt32();
                return (level & NativeMethods.SC_FOLDLEVELNUMBERMASK);
            }
            set
            {
                var bits = (int)FoldLevelFlags;
                bits |= value;

                scintilla.DirectMessage(NativeMethods.SCI_SETFOLDLEVEL, new IntPtr(Index), new IntPtr(bits));
            }
        }

        /// <summary>
        /// Gets or sets the fold level flags.
        /// </summary>
        /// <returns>A bitwise combination of the <see cref="FoldLevelFlags" /> enumeration.</returns>
        public FoldLevelFlags FoldLevelFlags
        {
            get
            {
                var flags = scintilla.DirectMessage(NativeMethods.SCI_GETFOLDLEVEL, new IntPtr(Index)).ToInt32();
                return (FoldLevelFlags)(flags & ~NativeMethods.SC_FOLDLEVELNUMBERMASK);
            }
            set
            {
                var bits = FoldLevel;
                bits |= (int)value;

                scintilla.DirectMessage(NativeMethods.SCI_SETFOLDLEVEL, new IntPtr(Index), new IntPtr(bits));
            }
        }

        /// <summary>
        /// Gets the zero-based line index of the first line before the current line that is marked as
        /// <see cref="ScintillaNET.FoldLevelFlags.Header" /> and has a <see cref="FoldLevel" /> less than the current line.
        /// </summary>
        /// <returns>The zero-based line index of the fold parent if present; otherwise, -1.</returns>
        public int FoldParent
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_GETFOLDPARENT, new IntPtr(Index)).ToInt32();
            }
        }

        /// <summary>
        /// Gets the height of the line in pixels.
        /// </summary>
        /// <returns>The height in pixels of the line.</returns>
        /// <remarks>Currently all lines are the same height.</remarks>
        public int Height
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_TEXTHEIGHT, new IntPtr(Index)).ToInt32();
            }
        }

        /// <summary>
        /// Gets the line index.
        /// </summary>
        /// <returns>The zero-based line index within the <see cref="LineCollection" /> that created it.</returns>
        public int Index { get; private set; }

        /// <summary>
        /// Gets the length of the line.
        /// </summary>
        /// <returns>The number of characters in the line including any end of line characters.</returns>
        public int Length
        {
            get
            {
                return scintilla.Lines.CharLineLength(Index);
            }
        }

        /// <summary>
        /// Gets or sets the style of the margin text in a <see cref="MarginType.Text" /> or <see cref="MarginType.RightText" /> margin.
        /// </summary>
        /// <returns>
        /// The zero-based index of the margin text <see cref="Style" /> or 256 when <see cref="MarginStyles" />
        /// has been used to set individual character styles.
        /// </returns>
        /// <seealso cref="MarginStyles" />
        public int MarginStyle
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_MARGINGETSTYLE, new IntPtr(Index)).ToInt32();
            }
            set
            {
                value = Helpers.Clamp(value, 0, scintilla.Styles.Count - 1);
                scintilla.DirectMessage(NativeMethods.SCI_MARGINSETSTYLE, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets an array of style indexes corresponding to each charcter in the <see cref="MarginText" />
        /// so that each character may be individually styled.
        /// </summary>
        /// <returns>
        /// An array of <see cref="Style" /> indexes corresponding with each margin text character or an uninitialized
        /// array when <see cref="MarginStyle" /> has been used to set a single style for all characters.
        /// </returns>
        /// <remarks>
        /// <see cref="MarginText" /> must be set prior to setting this property.
        /// The <paramref name="value" /> specified should have a length equal to the <see cref="MarginText" /> length to properly style all characters.
        /// </remarks>
        /// <seealso cref="MarginStyle" />
        public unsafe byte[] MarginStyles
        {
            get
            {
                var length = scintilla.DirectMessage(NativeMethods.SCI_MARGINGETTEXT, new IntPtr(Index)).ToInt32();
                if (length == 0)
                    return new byte[0];

                var text = new byte[length + 1];
                var styles = new byte[length + 1];

                fixed (byte* textPtr = text)
                fixed (byte* stylePtr = styles)
                {
                    scintilla.DirectMessage(NativeMethods.SCI_MARGINGETTEXT, new IntPtr(Index), new IntPtr(textPtr));
                    scintilla.DirectMessage(NativeMethods.SCI_MARGINGETSTYLES, new IntPtr(Index), new IntPtr(stylePtr));

                    return Helpers.ByteToCharStyles(stylePtr, textPtr, length, scintilla.Encoding);
                }
            }
            set
            {
                var length = scintilla.DirectMessage(NativeMethods.SCI_MARGINGETTEXT, new IntPtr(Index)).ToInt32();
                if (length == 0)
                    return;

                var text = new byte[length + 1];
                fixed (byte* textPtr = text)
                {
                    scintilla.DirectMessage(NativeMethods.SCI_MARGINGETTEXT, new IntPtr(Index), new IntPtr(textPtr));

                    var styles = Helpers.CharToByteStyles(value ?? new byte[0], textPtr, length, scintilla.Encoding);
                    fixed (byte* stylePtr = styles)
                        scintilla.DirectMessage(NativeMethods.SCI_MARGINSETSTYLES, new IntPtr(Index), new IntPtr(stylePtr));
                }
            }
        }

        /// <summary>
        /// Gets or sets the text displayed in the line margin when the margin type is
        /// <see cref="MarginType.Text" /> or <see cref="MarginType.RightText" />.
        /// </summary>
        /// <returns>The text displayed in the line margin.</returns>
        public unsafe string MarginText
        {
            get
            {
                var length = scintilla.DirectMessage(NativeMethods.SCI_MARGINGETTEXT, new IntPtr(Index)).ToInt32();
                if (length == 0)
                    return string.Empty;

                var bytes = new byte[length + 1];
                fixed (byte* bp = bytes)
                {
                    scintilla.DirectMessage(NativeMethods.SCI_MARGINGETTEXT, new IntPtr(Index), new IntPtr(bp));
                    return Helpers.GetString(new IntPtr(bp), length, scintilla.Encoding);
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    // Scintilla docs suggest that setting to NULL rather than an empty string will free memory
                    scintilla.DirectMessage(NativeMethods.SCI_MARGINSETTEXT, new IntPtr(Index), IntPtr.Zero);
                }
                else
                {
                    var bytes = Helpers.GetBytes(value, scintilla.Encoding, zeroTerminated: true);
                    fixed (byte* bp = bytes)
                        scintilla.DirectMessage(NativeMethods.SCI_MARGINSETTEXT, new IntPtr(Index), new IntPtr(bp));
                }
            }
        }

        /// <summary>
        /// Gets the zero-based character position in the document where the line begins.
        /// </summary>
        /// <returns>The document position of the first character in the line.</returns>
        public int Position
        {
            get
            {
                return scintilla.Lines.CharPositionFromLine(Index);
            }
        }

        /// <summary>
        /// Gets the line text.
        /// </summary>
        /// <returns>A string representing the document line.</returns>
        /// <remarks>The returned text includes any end of line characters.</remarks>
        public unsafe string Text
        {
            get
            {
                var start = scintilla.DirectMessage(NativeMethods.SCI_POSITIONFROMLINE, new IntPtr(Index));
                var length = scintilla.DirectMessage(NativeMethods.SCI_LINELENGTH, new IntPtr(Index));
                var ptr = scintilla.DirectMessage(NativeMethods.SCI_GETRANGEPOINTER, start, length);
                if (ptr == IntPtr.Zero)
                    return string.Empty;

                var text = new string((sbyte*)ptr, 0, length.ToInt32(), scintilla.Encoding);
                return text;
            }
        }

        /// <summary>
        /// Sets or gets the line indentation.
        /// </summary>
        /// <returns>The indentation measured in character columns, which corresponds to the width of space characters.</returns>
        public int Indentation
        {
            get
            {
                return (scintilla.DirectMessage(NativeMethods.SCI_GETLINEINDENTATION, new IntPtr(Index)).ToInt32());
            }
            set
            {
                scintilla.DirectMessage(NativeMethods.SCI_SETLINEINDENTATION, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the line is visible.
        /// </summary>
        /// <returns>true if the line is visible; otherwise, false.</returns>
        /// <seealso cref="Scintilla.ShowLines" />
        /// <seealso cref="Scintilla.HideLines" />
        public bool Visible
        {
            get
            {
                return (scintilla.DirectMessage(NativeMethods.SCI_GETLINEVISIBLE, new IntPtr(Index)) != IntPtr.Zero);
            }
        }

        /// <summary>
        /// Gets the number of display lines this line would occupy when wrapping is enabled.
        /// </summary>
        /// <returns>The number of display lines needed to wrap the current document line.</returns>
        public int WrapCount
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_WRAPCOUNT, new IntPtr(Index)).ToInt32();
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Line" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that created this line.</param>
        /// <param name="index">The index of this line within the <see cref="LineCollection" /> that created it.</param>
        public Line(Scintilla scintilla, int index)
        {
            this.scintilla = scintilla;
            Index = index;
        }

        #endregion Constructors
    }
}
