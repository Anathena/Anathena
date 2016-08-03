using System;
using System.ComponentModel;
using System.Globalization;

namespace Anathema.Source.Project.ProjectItems.TypeConverters
{
    class LuaScriptConverter : StringConverter
    {
        public override Object ConvertTo(ITypeDescriptorContext Context, CultureInfo Culture, Object Value, Type DestinationType)
        {
            if (Value == null)
                return "(Empty)";
            else
                return "(Script)";
        }

        public override Boolean CanConvertFrom(ITypeDescriptorContext Context, Type SourceType)
        {
            return true;
        }

    } // End class

} // End namespace