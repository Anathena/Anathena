namespace Ana.Source.Mvvm.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Utils;

    /// <summary>
    /// Converts IntPtrs to an address with leading 0s.
    /// </summary>
    internal class IntPtrToAddressConverter : IValueConverter
    {
        /// <summary>
        /// Converts an IntPtr to an address string.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>Object with type of BitmapSource. If conversion cannot take place, returns null.</returns>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is IntPtr)
            {
                return Conversions.ToHex((IntPtr)value, formatAsAddress: true, includePrefix: false);
            }

            return null;
        }

        /// <summary>
        /// Not used or implemented.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>Throws see <see cref="NotImplementedException" />.</returns>
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace