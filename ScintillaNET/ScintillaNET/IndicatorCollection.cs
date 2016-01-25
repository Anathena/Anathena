using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ScintillaNET
{
    /// <summary>
    /// An immutable collection of indicators in a <see cref="Scintilla" /> control.
    /// </summary>
    public class IndicatorCollection : IEnumerable<Indicator>
    {
        private readonly Scintilla scintilla;

        /// <summary>
        /// Provides an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An object that contains all <see cref="Indicator" /> objects within the <see cref="IndicatorCollection" />.</returns>
        public IEnumerator<Indicator> GetEnumerator()
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
        /// Gets the number of indicators.
        /// </summary>
        /// <returns>The number of indicators in the <see cref="IndicatorCollection" />.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Count
        {
            get
            {
                return (NativeMethods.INDIC_MAX + 1);
            }
        }

        /// <summary>
        /// Gets an <see cref="Indicator" /> object at the specified index.
        /// </summary>
        /// <param name="index">The indicator index.</param>
        /// <returns>An object representing the indicator at the specified <paramref name="index" />.</returns>
        /// <remarks>
        /// Indicators 0 through 7 are used by lexers.
        /// Indicators 32 through 35 are used for IME.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Indicator this[int index]
        {
            get
            {
                index = Helpers.Clamp(index, 0, Count - 1);
                return new Indicator(scintilla, index);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndicatorCollection" /> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
        public IndicatorCollection(Scintilla scintilla)
        {
            this.scintilla = scintilla;
        }
    }
}
