namespace Ana.Source.Utils.TypeConverters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    internal class LuaScriptConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            if (value == null)
            {
                return "(Empty)";
            }
            else
            {
                return "(Script)";
            }
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
    }
    //// End class
}
//// End namespace