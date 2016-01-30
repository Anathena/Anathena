using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    [DataContract()]
    public class AddressItem : TableItem
    {
        [DataMember()]
        public UInt64 Address { get; set; }

        [DataMember()]
        public String Description { get; set; }

        [DataMember()]
        public Boolean IsHex { get; set; }

        [DataMember()]
        public Int32[] Offsets { get; set; }

        [DataMember()]
        public String TypeName { get; set; }

        public Type ElementType { get { return Type.GetType(TypeName); } set { _Value = null; this.TypeName = (value == null ? String.Empty : value.FullName); } }

        private dynamic _Value;
        public dynamic Value { get { return _Value; } set { if (!Activated) _Value = value; } }

        public AddressItem(UInt64 Address, Type ElementType)
        {
            this.Address = Address;
            this.ElementType = ElementType;
            Offsets = null;
        }

        public AddressItem(UInt64 Address, String Description, Type ElementType, Int32[] Offsets, Boolean IsHex)
        {
            this.Address = Address;
            this.Description = Description;
            this.ElementType = ElementType;
            this.Offsets = Offsets;
            this.IsHex = IsHex;
        }

        public String GetValueString()
        {
            if (Value == null)
                return "-";

            dynamic ParseValue;

            switch (Type.GetTypeCode(ElementType))
            {
                case TypeCode.Single:
                    ParseValue = BitConverter.ToUInt32(BitConverter.GetBytes(Value), 0);
                    break;
                case TypeCode.Double:
                    ParseValue = BitConverter.ToUInt64(BitConverter.GetBytes(Value), 0);
                    break;
                default:
                    ParseValue = Value;
                    break;
            }

            try
            {
                if (IsHex)
                    return ParseValue.ToString("X");
            }
            catch { }

            return ParseValue.ToString();
        }

        public void ForceUpdateValue(dynamic Value)
        {
            if (Value == null)
                return;

            this._Value = Value;
        }

    } // End class

} // End namespace