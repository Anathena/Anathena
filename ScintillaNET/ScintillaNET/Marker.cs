using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Represents a margin marker in a <see cref="Scintilla" /> control.
    /// </summary>
    public class Marker
    {
        private readonly Scintilla scintilla;

        /// <summary>
        /// An unsigned 32-bit mask of all <see cref="Margin" /> indexes where each bit cooresponds to a margin index.
        /// </summary>
        public const uint MaskAll = unchecked((uint)-1);

        /// <summary>
        /// An unsigned 32-bit mask of folder <see cref="Margin" /> indexes (25 through 31) where each bit cooresponds to a margin index.
        /// </summary>
        /// <seealso cref="Margin.Mask" />
        public const uint MaskFolders = NativeMethods.SC_MASK_FOLDERS;

        /// <summary>
        /// Folder end marker index. This marker is typically configured to display the <see cref="MarkerSymbol.BoxPlusConnected" /> symbol.
        /// </summary>
        public const int FolderEnd = NativeMethods.SC_MARKNUM_FOLDEREND;

        /// <summary>
        /// Folder open marker index. This marker is typically configured to display the <see cref="MarkerSymbol.BoxMinusConnected" /> symbol.
        /// </summary>
        public const int FolderOpenMid = NativeMethods.SC_MARKNUM_FOLDEROPENMID;

        /// <summary>
        /// Folder mid tail marker index. This marker is typically configured to display the <see cref="MarkerSymbol.TCorner" /> symbol.
        /// </summary>
        public const int FolderMidTail = NativeMethods.SC_MARKNUM_FOLDERMIDTAIL;

        /// <summary>
        /// Folder tail marker index. This marker is typically configured to display the <see cref="MarkerSymbol.LCorner" /> symbol.
        /// </summary>
        public const int FolderTail = NativeMethods.SC_MARKNUM_FOLDERTAIL;

        /// <summary>
        /// Folder sub marker index. This marker is typically configured to display the <see cref="MarkerSymbol.VLine" /> symbol.
        /// </summary>
        public const int FolderSub = NativeMethods.SC_MARKNUM_FOLDERSUB;

        /// <summary>
        /// Folder marker index. This marker is typically configured to display the <see cref="MarkerSymbol.BoxPlus" /> symbol.
        /// </summary>
        public const int Folder = NativeMethods.SC_MARKNUM_FOLDER;

        /// <summary>
        /// Folder open marker index. This marker is typically configured to display the <see cref="MarkerSymbol.BoxMinus" /> symbol.
        /// </summary>
        public const int FolderOpen = NativeMethods.SC_MARKNUM_FOLDEROPEN;

        /// <summary>
        /// Sets the marker symbol to a custom image.
        /// </summary>
        /// <param name="image">The Bitmap to use as a marker symbol.</param>
        /// <remarks>Calling this method will also update the <see cref="Symbol" /> property to <see cref="MarkerSymbol.RgbaImage" />.</remarks>
        public unsafe void DefineRgbaImage(Bitmap image)
        {
            if (image == null)
                return;

            scintilla.DirectMessage(NativeMethods.SCI_RGBAIMAGESETWIDTH, new IntPtr(image.Width));
            scintilla.DirectMessage(NativeMethods.SCI_RGBAIMAGESETHEIGHT, new IntPtr(image.Height));

            var bytes = Helpers.BitmapToArgb(image);
            fixed (byte* bp = bytes)
                scintilla.DirectMessage(NativeMethods.SCI_MARKERDEFINERGBAIMAGE, new IntPtr(Index), new IntPtr(bp));
        }

        /// <summary>
        /// Removes this marker from all lines.
        /// </summary>
        public void DeleteAll()
        {
            scintilla.MarkerDeleteAll(Index);
        }

        /// <summary>
        /// Sets the foreground alpha transparency for markers that are drawn in the content area.
        /// </summary>
        /// <param name="alpha">The alpha transparency ranging from 0 (completely transparent) to 255 (no transparency).</param>
        /// <remarks>See the remarks on the <see cref="SetBackColor" /> method for a full explanation of when a marker can be drawn in the content area.</remarks>
        /// <seealso cref="SetBackColor" />
        public void SetAlpha(int alpha)
        {
            alpha = Helpers.Clamp(alpha, 0, 255);
            scintilla.DirectMessage(NativeMethods.SCI_MARKERSETALPHA, new IntPtr(Index), new IntPtr(alpha));
        }

        /// <summary>
        /// Sets the background color of the marker.
        /// </summary>
        /// <param name="color">The <see cref="Marker" /> background Color. The default is White.</param>
        /// <remarks>
        /// The background color of the whole line will be drawn in the <paramref name="color" /> specified when the marker is not visible
        /// because it is hidden by a <see cref="Margin.Mask" /> or the <see cref="Margin.Width" /> is zero.
        /// </remarks>
        /// <seealso cref="SetAlpha" />
        public void SetBackColor(Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            scintilla.DirectMessage(NativeMethods.SCI_MARKERSETBACK, new IntPtr(Index), new IntPtr(colour));
        }

        /// <summary>
        /// Sets the foreground color of the marker.
        /// </summary>
        /// <param name="color">The <see cref="Marker" /> foreground Color. The default is Black.</param>
        public void SetForeColor(Color color)
        {
            var colour = ColorTranslator.ToWin32(color);
            scintilla.DirectMessage(NativeMethods.SCI_MARKERSETFORE, new IntPtr(Index), new IntPtr(colour));
        }

        /// <summary>
        /// Gets the zero-based marker index this object represents.
        /// </summary>
        /// <returns>The marker index within the <see cref="MarkerCollection" />.</returns>
        public int Index { get; private set; }

        /// <summary>
        /// Gets or sets the marker symbol.
        /// </summary>
        /// <returns>
        /// One of the <see cref="ScintillaNET.MarkerSymbol" /> enumeration values.
        /// The default is <see cref="ScintillaNET.MarkerSymbol.Circle" />.
        /// </returns>
        public MarkerSymbol Symbol
        {
            get
            {
                return (MarkerSymbol)scintilla.DirectMessage(NativeMethods.SCI_MARKERSYMBOLDEFINED, new IntPtr(Index));
            }
            set
            {
                var markerSymbol = (int)value;
                scintilla.DirectMessage(NativeMethods.SCI_MARKERDEFINE, new IntPtr(Index), new IntPtr(markerSymbol));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Marker" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that created this marker.</param>
        /// <param name="index">The index of this style within the <see cref="MarkerCollection" /> that created it.</param>
        public Marker(Scintilla scintilla, int index)
        {
            this.scintilla = scintilla;
            Index = index;
        }
    }
}
