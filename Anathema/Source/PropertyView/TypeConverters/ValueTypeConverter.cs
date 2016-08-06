using Anathema.Source.Utils;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Anathema.Source.Project.PropertyView.TypeConverters
{
    class ValueTypeConverter : StringConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext Context)
        {
            return new StandardValuesCollection(PrimitiveTypes.GetScannablePrimitiveTypes().Select(X => X.ToString()).ToList());
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            if (SourceType == null || SourceType == typeof(String))
                return true;

            return base.CanConvertFrom(Context, SourceType);
        }

        public override Object ConvertFrom(ITypeDescriptorContext Context, CultureInfo Culture, Object Value)
        {
            foreach (Type Type in PrimitiveTypes.GetScannablePrimitiveTypes())
            {
                if (Type == null || !(Value is String))
                    break;

                if (Type.ToString() == Value as String)
                    return Type;
            }

            return base.ConvertFrom(Context, Culture, Value);
        }

        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext Context)
        {
            return true;
        }

    } // End class

} // End namespace