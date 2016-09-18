namespace Ana.Source.Docking
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ActiveDocumentConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value is FileViewModel)
                return value;

            return Binding.DoNothing;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value is FileViewModel)
                return value;

            return Binding.DoNothing;
        }
    }
    //// End class
}
//// End namespace