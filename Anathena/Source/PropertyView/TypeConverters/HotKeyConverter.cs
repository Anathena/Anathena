using Anathena.Source.Engine.InputCapture.HotKeys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Anathena.Source.Project.PropertyView.TypeConverters
{
    class HotKeyConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext Context, CultureInfo Culture, Object Value, Type DestinationType)
        {
            if (Value == null)
                return "(None)";

            if (Value.GetType().GetInterfaces().Any(X => X.IsAssignableFrom(typeof(IEnumerable<IHotKey>))))
                return "(HotKey)";

            return base.ConvertTo(Context, Culture, Value, DestinationType);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            return true;
        }

    } // End class

} // End namespace