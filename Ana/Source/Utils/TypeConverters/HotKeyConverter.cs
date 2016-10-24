namespace Ana.Source.Utils.TypeConverters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    internal class HotKeyConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            if (value == null)
            {
                return "(None)";
            }

            return "(None)";
            // TODO: Add this back
            // if (Value.GetType().GetInterfaces().Any(X => X.IsAssignableFrom(typeof(IEnumerable<IHotKey>))))
            // {
            //     return "(HotKey)";
            // }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
    }
    //// End class
}
//// End namespace