namespace Ana.Source.Utils.TypeConverters
{
    using Project.ProjectItems;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// Dynamic type converter for use in the property viewer.
    /// </summary>
    internal class DynamicConverter : StringConverter
    {
        /// <summary>
        /// Converts a value to the proper dynamic type.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="culture">Globalization info.</param>
        /// <param name="value">The value being converted.</param>
        /// <param name="destinationType">The target type to convert to.</param>
        /// <returns>The converted value.</returns>
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

            return isHex ? Conversions.ParseDynamicAsHexString(valueType, valueString) : valueString;
        }

        /// <summary>
        /// Converts an address string to the corresponding value.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="culture">Globalization info.</param>
        /// <param name="value">The value being converted.</param>
        /// <returns>The converted value.</returns>
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

            return isHex ? Conversions.ParseHexStringAsDynamic(valueType, value as String) : Conversions.ParsePrimitiveStringAsDynamic(valueType, value as String);
        }

        /// <summary>
        /// Determines if this converter can convert to the given source type.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns>True if this converter can convert to the given type.</returns>
        public override Boolean CanConvertTo(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        /// <summary>
        /// Determines if this converter can convert from the given source type.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns>True if this converter can convert from the given type.</returns>
        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
    }
    //// End class
}
//// End namespace