using Anathena.Source.Utils.Extensions;
using Anathena.Source.Utils.Validation;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Anathena.Source.Project.PropertyView.TypeConverters
{
    class AddressConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext Context, CultureInfo Culture, Object Value, Type DestinationType)
        {
            if (CheckSyntax.CanParseValue(typeof(UInt64), Value.ToString()))
                return Conversions.ToAddress(Conversions.ParseDecStringAsValue(typeof(UInt64), Value.ToString()));

            return base.ConvertTo(Context, Culture, Value, DestinationType);
        }

        public override Object ConvertFrom(ITypeDescriptorContext Context, CultureInfo Culture, Object Value)
        {
            if (CheckSyntax.CanParseAddress(Value.ToString()))
                return Conversions.AddressToValue(Value.ToString()).ToIntPtr();

            return base.ConvertFrom(Context, Culture, Value);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            return true;
        }

    } // End class

} // End namespace