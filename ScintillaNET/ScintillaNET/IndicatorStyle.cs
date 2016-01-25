using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// The visual appearance of an indicator.
    /// </summary>
    public enum IndicatorStyle
    {
        /// <summary>
        /// Underlined with a single, straight line.
        /// </summary>
        Plain = NativeMethods.INDIC_PLAIN,

        /// <summary>
        /// A squiggly underline. Requires 3 pixels of descender space.
        /// </summary>
        Squiggle = NativeMethods.INDIC_SQUIGGLE,

        /// <summary>
        /// A line of small T shapes.
        /// </summary>
        TT = NativeMethods.INDIC_TT,

        /// <summary>
        /// Diagonal hatching.
        /// </summary>
        Diagonal = NativeMethods.INDIC_DIAGONAL,

        /// <summary>
        /// Strike out.
        /// </summary>
        Strike = NativeMethods.INDIC_STRIKE,

        /// <summary>
        /// An indicator with no visual effect.
        /// </summary>
        Hidden = NativeMethods.INDIC_HIDDEN,

        /// <summary>
        /// A rectangle around the text.
        /// </summary>
        Box = NativeMethods.INDIC_BOX,

        /// <summary>
        /// A rectangle around the text with rounded corners. The rectangle outline and fill transparencies can be adjusted using
        /// <see cref="Indicator.Alpha" /> and <see cref="Indicator.OutlineAlpha" />.
        /// </summary>
        RoundBox = NativeMethods.INDIC_ROUNDBOX,

        /// <summary>
        /// A rectangle around the text. The rectangle outline and fill transparencies can be adjusted using
        /// <see cref="Indicator.Alpha" /> and <see cref="Indicator.OutlineAlpha"/>.
        /// </summary>
        StraightBox = NativeMethods.INDIC_STRAIGHTBOX,

        /// <summary>
        /// A dashed underline.
        /// </summary>
        Dash = NativeMethods.INDIC_DASH,

        /// <summary>
        /// A dotted underline.
        /// </summary>
        Dots = NativeMethods.INDIC_DOTS,

        /// <summary>
        /// Similar to <see cref="Squiggle" /> but only using 2 vertical pixels so will fit under small fonts.
        /// </summary>
        SquiggleLow = NativeMethods.INDIC_SQUIGGLELOW,

        /// <summary>
        /// A dotted rectangle around the text. The dots transparencies can be adjusted using
        /// <see cref="Indicator.Alpha" /> and <see cref="Indicator.OutlineAlpha" />.
        /// </summary>
        DotBox = NativeMethods.INDIC_DOTBOX,

        // PIXMAP

        /// <summary>
        /// A 2-pixel thick underline with 1 pixel insets on either side.
        /// </summary>
        CompositionThick = NativeMethods.INDIC_COMPOSITIONTHICK,

        /// <summary>
        /// A 1-pixel thick underline with 1 pixel insets on either side.
        /// </summary>
        CompositionThin = NativeMethods.INDIC_COMPOSITIONTHIN,

        /// <summary>
        /// A rectangle around the entire character area. The rectangle outline and fill transparencies can be adjusted using
        /// <see cref="Indicator.Alpha" /> and <see cref="Indicator.OutlineAlpha"/>.
        /// </summary>
        FullBox = NativeMethods.INDIC_FULLBOX,

        /// <summary>
        /// An indicator that will change the foreground color of text to the foreground color of the indicator.
        /// </summary>
        TextFore = NativeMethods.INDIC_TEXTFORE
    }
}
