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
            if (!CheckSyntax.CanParseValue(Value?.GetType(), Value?.ToString()))
                return base.ConvertTo(Context, Culture, Value, DestinationType);

            switch (Type.GetTypeCode(Value?.GetType()))
            {
                case TypeCode.Byte: return Conversions.ParseValueAsDec(typeof(Byte), Value.ToString());
                case TypeCode.SByte: return Conversions.ParseValueAsDec(typeof(SByte), Value.ToString());
                case TypeCode.Int16: return Conversions.ParseValueAsDec(typeof(Int16), Value.ToString());
                case TypeCode.Int32: return Conversions.ParseValueAsDec(typeof(Int32), Value.ToString());
                case TypeCode.Int64: return Conversions.ParseValueAsDec(typeof(Int64), Value.ToString());
                case TypeCode.UInt16: return Conversions.ParseValueAsDec(typeof(UInt16), Value.ToString());
                case TypeCode.UInt32: return Conversions.ParseValueAsDec(typeof(UInt32), Value.ToString());
                case TypeCode.UInt64: return Conversions.ParseValueAsDec(typeof(UInt64), Value.ToString());
                case TypeCode.Single: return Conversions.ParseValueAsDec(typeof(Single), Value.ToString());
                case TypeCode.Double: return Conversions.ParseValueAsDec(typeof(Double), Value.ToString());
            }

            return base.ConvertTo(Context, Culture, Value, DestinationType);
        }

        public override Object ConvertFrom(ITypeDescriptorContext Context, CultureInfo Culture, Object Value)
        {
            Type ValueType = null;

            if (Context.Instance.GetType().IsAssignableFrom(typeof(AddressItem)))
                ValueType = (Context.Instance as AddressItem)?.ElementType;

            if (ValueType == null || !Value.GetType().IsAssignableFrom(typeof(String)))
                return base.ConvertFrom(Context, Culture, Value);

            String ValueString = Value as String;

            switch (Type.GetTypeCode(ValueType))
            {
                case TypeCode.Byte: return Conversions.ParseValue(typeof(Byte), ValueString);
                case TypeCode.SByte: return Conversions.ParseValue(typeof(SByte), ValueString);
                case TypeCode.Int16: return Conversions.ParseValue(typeof(Int16), ValueString);
                case TypeCode.Int32: return Conversions.ParseValue(typeof(Int32), ValueString);
                case TypeCode.Int64: return Conversions.ParseValue(typeof(Int64), ValueString);
                case TypeCode.UInt16: return Conversions.ParseValue(typeof(UInt16), ValueString);
                case TypeCode.UInt32: return Conversions.ParseValue(typeof(UInt32), ValueString);
                case TypeCode.UInt64: return Conversions.ParseValue(typeof(UInt64), ValueString);
                case TypeCode.Single: return Conversions.ParseValue(typeof(Single), ValueString);
                case TypeCode.Double: return Conversions.ParseValue(typeof(Double), ValueString);
            }

            return base.ConvertFrom(Context, Culture, Value);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            return true;
        }

    } // End class

} // End namespace