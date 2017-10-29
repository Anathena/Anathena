namespace SqualrCore.Source.Utils
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class DataType
    {
        [DataMember]
        string TypeString
        {
            get
            {
                return this.Type?.FullName;
            }
            set
            {
                this.Type = value == null ? null : Type.GetType(value);
            }
        }

        public DataType() : this(null)
        {
        }

        public DataType(Type t)
        {
            this.Type = t;
        }

        public Type Type { get; set; }

        static public implicit operator Type(DataType dataType)
        {
            return dataType?.Type;
        }

        static public implicit operator DataType(Type t)
        {
            return new DataType(t);
        }

        public static Boolean operator ==(DataType a, DataType b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            return a?.Type == b?.Type;
        }

        public static Boolean operator !=(DataType a, DataType b)
        {
            return !(a == b);
        }

        public static Boolean operator ==(DataType a, Type b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            return a?.Type == b;
        }

        public static Boolean operator !=(DataType a, Type b)
        {
            return !(a == b);
        }

        public static Boolean operator ==(Type a, DataType b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            return a == b?.Type;
        }

        public static Boolean operator !=(Type a, DataType b)
        {
            return !(a == b);
        }

        public override Int32 GetHashCode()
        {
            return this.Type.GetHashCode();
        }

        public override Boolean Equals(Object datatype)
        {
            return (this.Type == (datatype as DataType)?.Type);
        }

        public Boolean Equals(DataType dataType)
        {
            return (this.Type == dataType?.Type);
        }

        public override String ToString()
        {
            return this.Type?.ToString() ?? String.Empty;
        }
    }
    //// End class
}
//// End namespace