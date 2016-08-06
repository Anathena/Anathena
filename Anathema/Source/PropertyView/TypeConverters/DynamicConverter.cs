using Anathema.Source.Project.ProjectItems;
using Anathema.Source.Utils.Validation;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Anathema.Source.Project.PropertyView.TypeConverters
{
    class DynamicConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext Context, CultureInfo Culture, Object Value, Type DestinationType)
        {
            String ValueString = (Value == null) ? String.Empty : Value.ToString();
            Type ValueType = (Value == null) ? null : Value.GetType();
            Boolean IsHex = false;

            if (Context.Instance.GetType().IsAssignableFrom(typeof(AddressItem)))
                IsHex = (Context.Instance as AddressItem).IsValueHex;

            if (Value == null || !CheckSyntax.CanParseValue(ValueType, ValueString))
                return base.ConvertTo(Context, Culture, Value, DestinationType);

            return IsHex ? Conversions.ParseValueAsHex(ValueType, ValueString) : Conversions.ParseValueAsDec(ValueType, ValueString);
        }

        public override Object ConvertFrom(ITypeDescriptorContext Context, CultureInfo Culture, Object Value)
        {
            Type ValueType = null;
            Boolean IsHex = false;

            if (Context.Instance.GetType().IsAssignableFrom(typeof(AddressItem)))
                ValueType = (Context.Instance as AddressItem)?.ElementType;

            if (Context.Instance.GetType().IsAssignableFrom(typeof(AddressItem)))
                IsHex = (Context.Instance as AddressItem).IsValueHex;

            if (ValueType == null || !Value.GetType().IsAssignableFrom(typeof(String)))
                return base.ConvertFrom(Context, Culture, Value);

            if (!(IsHex ? CheckSyntax.CanParseHex(ValueType, Value as String) : CheckSyntax.CanParseValue(ValueType, Value as String)))
                return base.ConvertFrom(Context, Culture, Value);

            return IsHex ? Conversions.ParseHexStringAsValue(ValueType, Value as String) : Conversions.ParseDecStringAsValue(ValueType, Value as String);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            return true;
        }

    } // End class

} // End namespace