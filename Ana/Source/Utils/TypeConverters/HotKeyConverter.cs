namespace Ana.Source.Utils.TypeConverters
{
    using Engine.Input.HotKeys;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    internal class HotkeyConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            if (value == null)
            {
                return "(None)";
            }

            if (value.GetType().GetInterfaces().Any(x => x.IsAssignableFrom(typeof(IEnumerable<IHotkey>))))
            {
                return "(HotKey)";
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