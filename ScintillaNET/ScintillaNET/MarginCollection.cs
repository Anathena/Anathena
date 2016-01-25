using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// An immutable collection of margins in a <see cref="Scintilla" /> control.
    /// </summary>
    public class MarginCollection : IEnumerable<Margin>
    {
        private readonly Scintilla scintilla;

        /// <summary>
        /// Removes all text displayed in every <see cref="MarginType.Text" /> and <see cref="MarginType.RightText" /> margins.
        /// </summary>
        public void ClearAllText()
        {
            scintilla.DirectMessage(NativeMethods.SCI_MARGINTEXTCLEARALL);
        }

        /// <summary>
        /// Provides an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An object that contains all <see cref="Margin" /> objects within the <see cref="MarginCollection" />.</returns>
        public IEnumerator<Margin> GetEnumerator()
        {
            int count = Count;
            for (int i = 0; i < count; i++)
                yield return this[i];

            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the number of margins in the <see cref="MarginCollection" />.
        /// </summary>
        /// <returns>This property always returns 5.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Count
        {
            get
            {
                return (NativeMethods.SC_MAX_MARGIN + 1);
            }
        }

        /// <summary>
        /// Gets or sets the width in pixels of the left margin padding.
        /// </summary>
        /// <returns>The left margin padding measured in pixels. The default is 1.</returns>
        [DefaultValue(1)]
        [Description("The left margin padding in pixels.")]
        public int Left
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_GETMARGINLEFT).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                scintilla.DirectMessage(NativeMethods.SCI_SETMARGINLEFT, IntPtr.Zero, new IntPtr(value));
            }
        }

        /*
        /// <summary>
        /// Gets or sets the margin options.
        /// </summary>
        /// <returns>
        /// A <see cref="ScintillaNET.MarginOptions" /> that represents the margin options.
        /// The default is <see cref="ScintillaNET.MarginOptions.None" />.
        /// </returns>
        [DefaultValue(MarginOptions.None)]
        [Description("Margin options flags.")]
        [TypeConverter(typeof(FlagsEnumTypeConverter.FlagsEnumConverter))]
        public MarginOptions Options
        {
            get
            {
                return (MarginOptions)scintilla.DirectMessage(NativeMethods.SCI_GETMARGINOPTIONS);
            }
            set
            {
                var options = (int)value;
                scintilla.DirectMessage(NativeMethods.SCI_SETMARGINOPTIONS, new IntPtr(options));
            }
        }
        */

        /// <summary>
        /// Gets or sets the width in pixels of the right margin padding.
        /// </summary>
        /// <returns>The right margin padding measured in pixels. The default is 1.</returns>
        [DefaultValue(1)]
        [Description("The right margin padding in pixels.")]
        public int Right
        {
            get
            {
                return scintilla.DirectMessage(NativeMethods.SCI_GETMARGINRIGHT).ToInt32();
            }
            set
            {
                value = Helpers.ClampMin(value, 0);
                scintilla.DirectMessage(NativeMethods.SCI_SETMARGINRIGHT, IntPtr.Zero, new IntPtr(value));
            }
        }

        /// <summary>
        /// Gets a <see cref="Margin" /> object at the specified index.
        /// </summary>
        /// <param name="index">The margin index.</param>
        /// <returns>An object representing the margin at the specified <paramref name="index" />.</returns>
        /// <remarks>By convention margin 0 is used for line numbers and the two following for symbols.</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Margin this[int index]
        {
            get
            {
                index = Helpers.Clamp(index, 0, Count - 1);
                return new Margin(scintilla, index);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarginCollection" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
        public MarginCollection(Scintilla scintilla)
        {
            this.scintilla = scintilla;
        }
    }
}
