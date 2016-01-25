using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// The symbol displayed by a <see cref="Marker" />
    /// </summary>
    public enum MarkerSymbol
    {
        /// <summary>
        /// A circle. This symbol is typically used to indicate a breakpoint.
        /// </summary>
        Circle = NativeMethods.SC_MARK_CIRCLE,

        /// <summary>
        /// A rectangel with rounded edges.
        /// </summary>
        RoundRect = NativeMethods.SC_MARK_ROUNDRECT,

        /// <summary>
        /// An arrow (triangle) pointing right.
        /// </summary>
        Arrow = NativeMethods.SC_MARK_ARROW,

        /// <summary>
        /// A rectangle that is wider than it is tall.
        /// </summary>
        SmallRect = NativeMethods.SC_MARK_SMALLRECT,

        /// <summary>
        /// An arrow and tail pointing right. This symbol is typically used to indicate the current line of execution.
        /// </summary>
        ShortArrow = NativeMethods.SC_MARK_SHORTARROW,

        /// <summary>
        /// An invisible symbol useful for tracking the movement of lines.
        /// </summary>
        Empty = NativeMethods.SC_MARK_EMPTY,

        /// <summary>
        /// An arrow (triangle) pointing down.
        /// </summary>
        ArrowDown = NativeMethods.SC_MARK_ARROWDOWN,

        /// <summary>
        /// A minus (-) symbol.
        /// </summary>
        Minus = NativeMethods.SC_MARK_MINUS,

        /// <summary>
        /// A plus (+) symbol.
        /// </summary>
        Plus = NativeMethods.SC_MARK_PLUS,

        /// <summary>
        /// A thin vertical line. This symbol is typically used on the middle line of an expanded fold block.
        /// </summary>
        VLine = NativeMethods.SC_MARK_VLINE,

        /// <summary>
        /// A thin 'L' shaped line. This symbol is typically used on the last line of an expanded fold block.
        /// </summary>
        LCorner = NativeMethods.SC_MARK_LCORNER,

        /// <summary>
        /// A thin 't' shaped line. This symbol is typically used on the last line of an expanded nested fold block.
        /// </summary>
        TCorner = NativeMethods.SC_MARK_TCORNER,

        /// <summary>
        /// A plus (+) symbol with surrounding box. This symbol is typically used on the first line of a collapsed fold block.
        /// </summary>
        BoxPlus = NativeMethods.SC_MARK_BOXPLUS,

        /// <summary>
        /// A plus (+) symbol with surrounding box and thin vertical line. This symbol is typically used on the first line of a collapsed nested fold block.
        /// </summary>
        BoxPlusConnected = NativeMethods.SC_MARK_BOXPLUSCONNECTED,

        /// <summary>
        /// A minus (-) symbol with surrounding box. This symbol is typically used on the first line of an expanded fold block.
        /// </summary>
        BoxMinus = NativeMethods.SC_MARK_BOXMINUS,

        /// <summary>
        /// A minus (-) symbol with surrounding box and thin vertical line. This symbol is typically used on the first line of an expanded nested fold block.
        /// </summary>
        BoxMinusConnected = NativeMethods.SC_MARK_BOXMINUSCONNECTED,

        /// <summary>
        /// Similar to a <see cref="LCorner" />, but curved.
        /// </summary>
        LCornerCurve = NativeMethods.SC_MARK_LCORNERCURVE,

        /// <summary>
        /// Similar to a <see cref="TCorner" />, but curved.
        /// </summary>
        TCornerCurve = NativeMethods.SC_MARK_TCORNERCURVE,

        /// <summary>
        /// Similar to a <see cref="BoxPlus" /> but surrounded by a circle.
        /// </summary>
        CirclePlus = NativeMethods.SC_MARK_CIRCLEPLUS,

        /// <summary>
        /// Similar to a <see cref="BoxPlusConnected" />, but surrounded by a circle.
        /// </summary>
        CirclePlusConnected = NativeMethods.SC_MARK_CIRCLEPLUSCONNECTED,

        /// <summary>
        /// Similar to a <see cref="BoxMinus" />, but surrounded by a circle.
        /// </summary>
        CircleMinus = NativeMethods.SC_MARK_CIRCLEMINUS,

        /// <summary>
        /// Similar to a <see cref="BoxMinusConnected" />, but surrounded by a circle.
        /// </summary>
        CircleMinusConnected = NativeMethods.SC_MARK_CIRCLEMINUSCONNECTED,

        /// <summary>
        /// A special marker that displays no symbol but will affect the background color of the line.
        /// </summary>
        Background = NativeMethods.SC_MARK_BACKGROUND,

        /// <summary>
        /// Three dots (ellipsis).
        /// </summary>
        DotDotDot = NativeMethods.SC_MARK_DOTDOTDOT,

        /// <summary>
        /// Three bracket style arrows.
        /// </summary>
        Arrows = NativeMethods.SC_MARK_ARROWS,

        // PixMap = NativeMethods.SC_MARK_PIXMAP,

        /// <summary>
        /// A rectangle occupying the entire marker space.
        /// </summary>
        FullRect = NativeMethods.SC_MARK_FULLRECT,

        /// <summary>
        /// A rectangle occupying only the left edge of the marker space.
        /// </summary>
        LeftRect = NativeMethods.SC_MARK_LEFTRECT,

        /// <summary>
        /// A special marker left available to plugins.
        /// </summary>
        Available = NativeMethods.SC_MARK_AVAILABLE,

        /// <summary>
        /// A special marker that displays no symbol but will underline the current line text.
        /// </summary>
        Underline = NativeMethods.SC_MARK_UNDERLINE,

        /// <summary>
        /// A user-defined image. Images can be set using the <see cref="Marker.DefineRgbaImage" /> method.
        /// </summary>
        RgbaImage = NativeMethods.SC_MARK_RGBAIMAGE,

        /// <summary>
        /// A left-rotated bookmark.
        /// </summary>
        Bookmark = NativeMethods.SC_MARK_BOOKMARK,

        // Character = NativeMethods.SC_MARK_CHARACTER
    }
}
