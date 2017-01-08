namespace Ana.Source.Mvvm.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converter class to convert a boolean to a <see cref="Visibility"/> value.
    /// </summary>
    [ValueConversion(typeof(Boolean), typeof(Visibility))]
    internal class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary> 
        /// Converts a value. 
        /// </summary> 
        /// <param name="value">The value produced by the binding source.</param> 
        /// <param name="targetType">The type of the binding target property.</param> 
        /// <param name="parameter">The converter parameter to use.</param> 
        /// <param name="culture">The culture to use in the converter.</param> 
        /// <returns> 
        /// A converted value. If the method returns null, the valid null value is used. 
        /// </returns> 
        public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Boolean && targetType == typeof(Visibility))
            {
                Boolean val = (Boolean)value;

                if (val)
                {
                    return Visibility.Visible;
                }
                else if (parameter != null && parameter is Visibility)
                {
                    return parameter;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }

            if (value == null)
            {
                if (parameter != null && parameter is Visibility)
                {
                    return parameter;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Visible;
        }

        /// <summary> 
        /// Converts a value. 
        /// </summary> 
        /// <param name="value">The value that is produced by the binding target.</param> 
        /// <param name="targetType">The type to convert to.</param> 
        /// <param name="parameter">The converter parameter to use.</param> 
        /// <param name="culture">The culture to use in the converter.</param> 
        /// <returns> 
        /// A converted value. If the method returns null, the valid null value is used. 
        /// </returns> 
        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    //// End class
}
//// End namespace