using Anathena.Source.Utils.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Anathena.Source.Project.PropertyView.TypeConverters
{
    class OffsetConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext Context, CultureInfo Culture, Object Value, Type DestinationType)
        {
            if (Value == null)
                return "(None)";

            if (Value.GetType().GetInterfaces().Contains(typeof(IEnumerable)))
            {
                IEnumerable<Object> TrueValue = (Value as IEnumerable).Cast<Object>();

                if (TrueValue.Count() <= 0)
                    return "(None)";

                return String.Join(", ", TrueValue.Select(X => Conversions.ParseDecStringAsHexString(typeof(UInt64), X.ToString())));
            }

            return base.ConvertTo(Context, Culture, Value, DestinationType);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            return true;
        }

    } // End class

} // End namespace