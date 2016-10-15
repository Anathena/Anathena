namespace Ana.Source.Mvvm.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Utils.Validation;

    /// <summary>
    /// Converts Icons to a format readily usable by the view
    /// </summary>
    internal class IntPtrToAddressConverter : IValueConverter
    {
        /// <summary>
        /// Converts an Icon to a BitmapSource
        /// </summary>
        /// <param name="value">Value to be converted</param>
        /// <param name="targetType">Type to convert to</param>
        /// <param name="parameter">Optional conversion parameter</param>
        /// <param name="culture">Globalization info</param>
        /// <returns>Object with type of BitmapSource. If conversion cannot take place, returns null</returns>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is IntPtr)
            {
                return Conversions.ToAddress((IntPtr)value);
            }

            return null;
        }

        /// <summary>
        /// Not implemented for IconConverter
        /// </summary>
        /// <param name="value">Value to be converted</param>
        /// <param name="targetType">Type to convert to</param>
        /// <param name="parameter">Optional conversion parameter</param>
        /// <param name="culture">Globalization info</param>
        /// <returns>Throws see <see cref="NotImplementedException" /></returns>
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace