using System;

namespace Anathema.Source.PropertyEditor
{

    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreProperty : Attribute { public IgnoreProperty() { } }

    /// <summary>
    /// Represents a property to be displayed in the property viewer
    /// </summary>
    public class Property
    {
        private String Name;
        private Object Value;

        public Property(String Name, Object Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        public String GetName()
        {
            return Name;
        }

        public String GetValueString()
        {
            if (Value == null)
                return String.Empty;

            return Value.ToString();
        }

        public Object GetValue()
        {
            return Value;
        }

        /*
        public RepresentationEnum GetRepresentation()
        {
            if (Value.GetType().IsEnum)
                return RepresentationEnum.Enum;

            if (PrimitiveTypes.IsPrimitive(Value.GetType()))
                return RepresentationEnum.Primitive;

            return RepresentationEnum.Custom;
        }
        */

    } // End class

} // End namepsace