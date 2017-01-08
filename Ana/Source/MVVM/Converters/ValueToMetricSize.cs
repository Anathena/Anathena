namespace Ana.Source.Mvvm.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Converts a size in bytes to the metric size (B, KB, MB, GB, TB, PB, EB).
    /// </summary>
    internal class ValueToMetricSize : IValueConverter
    {
        /// <summary>
        /// Converts an Icon to a BitmapSource.
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

            // Note: UInt64s run out around EB
            String[] suffix = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

            UInt64 realByteCount = (UInt64)System.Convert.ChangeType(value, typeof(UInt64));

            if (realByteCount == 0)
            {
                return "0" + suffix[0];
            }

            Int32 place = System.Convert.ToInt32(Math.Floor(Math.Log(realByteCount, 1024)));
            Double number = Math.Round(realByteCount / Math.Pow(1024, place), 1);
            return number.ToString() + suffix[place];
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