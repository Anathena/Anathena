namespace Ana.Source.Utils.TypeConverters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using Utils;

    internal class ValueTypeConverter : StringConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(PrimitiveTypes.GetScannablePrimitiveTypes().Select(x => x.ToString()).ToList());
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == null || sourceType == typeof(String))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
        {
            foreach (Type type in PrimitiveTypes.GetScannablePrimitiveTypes())
            {
                if (type == null || !(value is String))
                {
                    break;
                }

                if (type.ToString() == value as String)
                {
                    return type;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
    //// End class
}
//// End namespace