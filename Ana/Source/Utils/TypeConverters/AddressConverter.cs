namespace Ana.Source.Utils.TypeConverters
{
    using Extensions;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using Validation;

    internal class AddressConverter : TypeConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            if (CheckSyntax.CanParseValue(typeof(UInt64), value.ToString()))
            {
                return Conversions.ToAddress(Conversions.ParseDecStringAsValue(typeof(UInt64), value.ToString()));
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
        {
            if (CheckSyntax.CanParseAddress(value.ToString()))
            {
                return Conversions.AddressToValue(value.ToString()).ToIntPtr();
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override Boolean CanConvertTo(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(IntPtr);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(String);
        }
    }
    //// End class
}
//// End namespace