using Anathema.Source.Engine.InputCapture;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Anathema.Source.Project.PropertyView.TypeConverters
{
    class HotKeyConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext Context, CultureInfo Culture, Object Value, Type DestinationType)
        {
            if (Value == null)
                return "(None)";

            if (Value.GetType().GetInterfaces().Contains(typeof(HotKeys)))
            {
                HotKeys TrueValue = Value as HotKeys;
                // TODO: Display hotkey preview
                return "(HotKey)";
            }

            return base.ConvertTo(Context, Culture, Value, DestinationType);
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            return true;
        }

    } // End class

} // End namespace