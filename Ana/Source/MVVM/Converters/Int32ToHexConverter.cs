namespace Ana.Source.Mvvm.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Utils;

    /// <summary>
    /// Converts an Int32 value to a hexedecimal value.
    /// </summary>
    internal class Int32ToHexConverter : IValueConverter
    {
        /// <summary>
        /// Converts an Int32 to a Hex string.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>A hex string. If conversion cannot take place, returns null.</returns>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return String.Empty;
            }

            if (value is Int32)
            {
                return Conversions.ParseDynamicAsHexString(typeof(Int32), value);
            }

            return String.Empty;
        }

        /// <summary>
        /// Hex string to an Int32.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>An Int32. If conversion cannot take place, returns 0.</returns>
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }

            if (value is String)
            {
                if (CheckSyntax.CanParseHex(typeof(Int32), value.ToString()))
                {
                    return Conversions.ParseHexStringAsDynamic(typeof(Int32), value.ToString());
                }
            }

            return 0;
        }
    }
    //// End class
}
//// End namespace