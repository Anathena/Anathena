namespace Ana.Source.Utils.TypeConverters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using Validation;

    internal class OffsetConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            if (value == null)
            {
                return "(None)";
            }

            if (value.GetType().GetInterfaces().Contains(typeof(IEnumerable)))
            {
                IEnumerable<Object> trueValue = (value as IEnumerable).Cast<Object>();

                if (trueValue.Count() <= 0)
                {
                    return "(None)";
                }

                return String.Join(", ", trueValue.Select(x => Conversions.ParseDecStringAsHexString(typeof(UInt64), x.ToString())));
            }

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