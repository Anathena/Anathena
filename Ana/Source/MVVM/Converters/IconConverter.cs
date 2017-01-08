namespace Ana.Source.Mvvm.Converters
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Converts Icons to a format readily usable by the view.
    /// </summary>
    internal class IconConverter : IValueConverter
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

            if (value is Icon)
            {
                Bitmap bitmap = (value as Icon).ToBitmap();
                IntPtr bitmaphandle = bitmap.GetHbitmap();
                return Imaging.CreateBitmapSourceFromHBitmap(bitmaphandle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
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