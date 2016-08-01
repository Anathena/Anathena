using Anathema.Source.Utils.Validation;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Anathema.Source.Project.ProjectItems.TypeConverters
{
    class DynamicConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext Context, CultureInfo Culture, Object Value, Type DestinationType)
        {
            String ValueString = (Value == null) ? String.Empty : Value.ToString();
            Type ValueType = (Value == null) ? null : Value.GetType();
            Boolean DisplayAsHex = false;

            if (Value == null || !CheckSyntax.CanParseValue(ValueType, ValueString))
                return base.ConvertTo(Context, Culture, Value, DestinationType);

            if (Context.Instance.GetType().IsAssignableFrom(typeof(AddressItem)))
                DisplayAsHex = (Context.Instance as AddressItem).IsValueHex;

            return DisplayAsHex ? Conversions.ParseValueAsHex(ValueType, ValueString) : Conversions.ParseValueAsDec(ValueType, ValueString);
        }

        public override Object ConvertFrom(ITypeDescriptorContext Context, CultureInfo Culture, Object Value)
        {
            Type ValueType = null;

            if (Context.Instance.GetType().IsAssignableFrom(typeof(AddressItem)))
                ValueType = (Context.Instance as AddressItem)?.ElementType;

            if (ValueType == null || !Value.GetType().IsAssignableFrom(typeof(String)))
                return base.ConvertFrom(Context, Culture, Value);

            return Conversions.ParseValue(ValueType, Value as String);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            return true;
        }

    } // End class

} // End namespace