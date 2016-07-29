using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.Validation;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Anathema.Source.Project.ProjectItems.TypeConverters
{
    class IntPtrConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext Context, CultureInfo Culture, Object Value, Type DestinationType)
        {
            if (CheckSyntax.CanParseValue(typeof(UInt64), Value.ToString()))
                return Conversions.ToAddress(Conversions.ParseValue(typeof(UInt64), Value.ToString()));

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