using Anathema.Source.Utils.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Anathema.Source.Project.ProjectItems.TypeConverters
{
    class HexArrayConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext Context, CultureInfo Culture, Object Value, Type DestinationType)
        {
            if (Value != null && Value.GetType().GetInterfaces().Contains(typeof(IEnumerable)))
            {
                IEnumerable<Object> TrueValue = (Value as IEnumerable).Cast<Object>();
                return String.Join(", ", TrueValue.Select(X => Conversions.ParseDecAsHex(typeof(UInt64), X.ToString())));
            }

            return base.ConvertTo(Context, Culture, Value, DestinationType);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            return true;
        }

    } // End class

} // End namespace