using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// Represents an indicator in a <see cref="Scintilla" /> control.
    /// </summary>
    public class Indicator
    {
        #region Fields

        private readonly Scintilla scintilla;

        #endregion Fields

        #region Constants

        /// <summary>
        /// An OR mask to use with <see cref="Scintilla.IndicatorValue" /> and <see cref="IndicatorFlags.ValueFore" /> to indicate
        /// that the user-defined indicator value should be treated as a RGB color.
        /// </summary>
        public const int ValueBit = NativeMethods.SC_INDICVALUEBIT;

        /// <summary>
        /// An AND mask to use with <see cref="Indicator.ValueAt" /> to retrieve the user-defined value as a RGB color when being treated as such.
        /// </summary>
        public const int ValueMask = NativeMethods.SC_INDICVALUEMASK;

        #endregion Constants

        #region Methods

        /// <summary>
        /// Given a document position which is filled with this indicator, will return the document position
        /// where the use of this indicator ends.
        /// </summary>
        /// <param name="position">A zero-based document position using this indicator.</param>
        /// <returns>The zero-based document position where the use of this indicator ends.</returns>
        /// <remarks>
        /// Specifying a <paramref name="position" /> which is not filled with this indicator will cause this method
        /// to return the end position of the range where this indicator is not in use (the negative space). If this
        /// indicator is not in use anywhere within the document the return value will be 0.
        /// </remarks>
        public int End(int position)
        {
            position = Helpers.Clamp(position, 0, scintilla.TextLength);
            position = scintilla.Lines.CharToBytePosition(position);
            position = scintilla.DirectMessage(NativeMethods.SCI_INDICATOREND, new IntPtr(Index), new IntPtr(position)).ToInt32();
            return scintilla.Lines.ByteToCharPosition(position);
        }

        /// <summary>
        /// Given a document position which is filled with this indicator, will return the document position
        /// where the use of this indicator starts.
        /// </summary>
        /// <param name="position">A zero-based document position using this indicator.</param>
        /// <returns>The zero-based document position where the use of this indicator starts.</returns>
        /// <remarks>
        /// Specifying a <paramref name="position" /> which is not filled with this indicator will cause this method
        /// to return the start position of the range where this indicator is not in use (the negative space). If this
        /// indicator is not in use anywhere within the document the return value will be 0.
        /// </remarks>
        public int Start(int position)
        {
            position = Helpers.Clamp(position, 0, scintilla.TextLength);
            position = scintilla.Lines.CharToBytePosition(position);
            position = scintilla.DirectMessage(NativeMethods.SCI_INDICATORSTART, new IntPtr(Index), new IntPtr(position)).ToInt32();
            return scintilla.Lines.ByteToCharPosition(position);
        }

        /// <summary>
        /// Returns the user-defined value for the indicator at the specified position.
        /// </summary>
        /// <param name="position">The zero-based document position to get the indicator value for.</param>
        /// <returns>The user-defined value at the specified <paramref name="position" />.</returns>
        public int ValueAt(int position)
        {
            position = Helpers.Clamp(position, 0, scintilla.TextLength);
            position = scintilla.Lines.CharToBytePosition(position);

            return scintilla.DirectMessage(NativeMethods.SCI_INDICATORVALUEAT, new IntPtr(Index), new IntPtr(position)).ToInt32();
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets or sets the alpha transparency of the indicator.
        /// </summary>
        /// <returns>
        /// The alpha transparency ranging from 0 (completely transparent)
        /// to 255 (no transparency). The default is 30.
        /// </returns>
        public int Alpha
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_INDICGETALPHA, new IntPtr(Index)).ToInt32();
            }
            set
            {
                value = Helpers.Clamp(value, 0, 255);
                scintilla.DirectMessage(NativeMethods.SCI_INDICSETALPHA, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the indicator flags.
        /// </summary>
        /// <returns>
        /// A bitwise combination of the <see cref="IndicatorFlags" /> enumeration.
        /// The default is <see cref="IndicatorFlags.None" />.
        /// </returns>
        public IndicatorFlags Flags
        {
            get
            {
                return (IndicatorFlags)scintilla.DirectMessage(NativeMethods.SCI_INDICGETFLAGS, new IntPtr(Index));
            }
            set
            {
                int flags = (int)value;
                scintilla.DirectMessage(NativeMethods.SCI_INDICSETFLAGS, new IntPtr(Index), new IntPtr(flags));
            }
        }

        /// <summary>
        /// Gets or sets the color used to draw an indicator.
        /// </summary>
        /// <returns>The Color used to draw an indicator. The default varies.</returns>
        /// <remarks>Changing the <see cref="ForeColor" /> property will reset the <see cref="HoverForeColor" />.</remarks>
        /// <seealso cref="HoverForeColor" />
        public Color ForeColor
        {
            get
            {
                var color = scintilla.DirectMessage(NativeMethods.SCI_INDICGETFORE, new IntPtr(Index)).ToInt32();
                return ColorTranslator.FromWin32(color);
            }
            set
            {
                var color = ColorTranslator.ToWin32(value);
                scintilla.DirectMessage(NativeMethods.SCI_INDICSETFORE, new IntPtr(Index), new IntPtr(color));
            }
        }

        /// <summary>
        /// Gets or sets the color used to draw an indicator when the mouse or caret is over an indicator.
        /// </summary>
        /// <returns>
        /// The Color used to draw an indicator.
        /// By default, the hover style is equal to the regular <see cref="ForeColor" />.
        /// </returns>
        /// <remarks>Changing the <see cref="ForeColor" /> property will reset the <see cref="HoverForeColor" />.</remarks>
        /// <seealso cref="ForeColor" />
        public Color HoverForeColor
        {
            get
            {
                var color = scintilla.DirectMessage(NativeMethods.SCI_INDICGETHOVERFORE, new IntPtr(Index)).ToInt32();
                return ColorTranslator.FromWin32(color);
            }
            set
            {
                var color = ColorTranslator.ToWin32(value);
                scintilla.DirectMessage(NativeMethods.SCI_INDICSETHOVERFORE, new IntPtr(Index), new IntPtr(color));
            }
        }

        /// <summary>
        /// Gets or sets the indicator style used when the mouse or caret is over an indicator.
        /// </summary>
        /// <returns>
        /// One of the <see cref="ScintillaNET.IndicatorStyle" /> enumeration values.
        /// By default, the hover style is equal to the regular <see cref="Style" />.
        /// </returns>
        /// <remarks>Changing the <see cref="Style" /> property will reset the <see cref="HoverStyle" />.</remarks>
        /// <seealso cref="Style" />
        public IndicatorStyle HoverStyle
        {
            get
            {
                return (IndicatorStyle)scintilla.DirectMessage(NativeMethods.SCI_INDICGETHOVERSTYLE, new IntPtr(Index));
            }
            set
            {
                var style = (int)value;
                scintilla.DirectMessage(NativeMethods.SCI_INDICSETHOVERSTYLE, new IntPtr(Index), new IntPtr(style));
            }
        }

        /// <summary>
        /// Gets the zero-based indicator index this object represents.
        /// </summary>
        /// <returns>The indicator definition index within the <see cref="IndicatorCollection" />.</returns>
        public int Index { get; private set; }

        /// <summary>
        /// Gets or sets the alpha transparency of the indicator outline.
        /// </summary>
        /// <returns>
        /// The alpha transparency ranging from 0 (completely transparent)
        /// to 255 (no transparency). The default is 50.
        /// </returns>
        public int OutlineAlpha
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_INDICGETOUTLINEALPHA, new IntPtr(Index)).ToInt32();
            }
            set
            {
                value = Helpers.Clamp(value, 0, 255);
                scintilla.DirectMessage(NativeMethods.SCI_INDICSETOUTLINEALPHA, new IntPtr(Index), new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets or sets the indicator style.
        /// </summary>
        /// <returns>One of the <see cref="ScintillaNET.IndicatorStyle" /> enumeration values. The default varies.</returns>
        /// <remarks>Changing the <see cref="Style" /> property will reset the <see cref="HoverStyle" />.</remarks>
        /// <seealso cref="HoverStyle" />
        public IndicatorStyle Style
        {
            get
            {
                return (IndicatorStyle)scintilla.DirectMessage(NativeMethods.SCI_INDICGETSTYLE, new IntPtr(Index));
            }
            set
            {
                var style = (int)value;
                scintilla.DirectMessage(NativeMethods.SCI_INDICSETSTYLE, new IntPtr(Index), new IntPtr(style));
            }
        }

        /// <summary>
        /// Gets or sets whether indicators are drawn under or over text.
        /// </summary>
        /// <returns>true to draw the indicator under text; otherwise, false. The default is false.</returns>
        /// <remarks>Drawing indicators under text requires <see cref="Phases.One" /> or <see cref="Phases.Multiple" /> drawing.</remarks>
        public bool Under
        {
            get
            {
                return (scintilla.DirectMessage(NativeMethods.SCI_INDICGETUNDER, new IntPtr(Index)) != IntPtr.Zero);
            }
            set
            {
                var under = (value ? new IntPtr(1) : IntPtr.Zero);
                scintilla.DirectMessage(NativeMethods.SCI_INDICSETUNDER, new IntPtr(Index), under);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Indicator" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that created this indicator.</param>
        /// <param name="index">The index of this style within the <see cref="IndicatorCollection" /> that created it.</param>
        public Indicator(Scintilla scintilla, int index)
        {
            this.scintilla = scintilla;
            Index = index;
        }

        #endregion Constructors
    }
}
