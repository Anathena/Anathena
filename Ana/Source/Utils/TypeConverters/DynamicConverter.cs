namespace Ana.Source.Utils.TypeConverters
{
    using Project.ProjectItems;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using Validation;

    internal class DynamicConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            String valueString = (value == null) ? String.Empty : value.ToString();
            Type valueType = (value == null) ? null : value.GetType();
            Boolean isHex = false;

            if (context.Instance.GetType().IsAssignableFrom(typeof(AddressItem)))
            {
                isHex = (context.Instance as AddressItem).IsValueHex;
            }

            if (value == null || !CheckSyntax.CanParseValue(valueType, valueString))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            return isHex ? Conversions.ParseValueAsHex(valueType, valueString) : Conversions.ParseValueAsDec(valueType, valueString);
        }

        public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
        {
            Type valueType = null;
            Boolean isHex = false;

            if (context.Instance.GetType().IsAssignableFrom(typeof(AddressItem)))
            {
                valueType = (context.Instance as AddressItem)?.ElementType;
            }

            if (context.Instance.GetType().IsAssignableFrom(typeof(AddressItem)))
            {
                isHex = (context.Instance as AddressItem).IsValueHex;
            }

            if (valueType == null || !value.GetType().IsAssignableFrom(typeof(String)))
            {
                return base.ConvertFrom(context, culture, value);
            }

            if (!(isHex ? CheckSyntax.CanParseHex(valueType, value as String) : CheckSyntax.CanParseValue(valueType, value as String)))
            {
                return base.ConvertFrom(context, culture, value);
            }

            return isHex ? Conversions.ParseHexStringAsValue(valueType, value as String) : Conversions.ParseDecStringAsValue(valueType, value as String);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
    }
    //// End class
}
//// End namespace