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

        public Type ElementType { get { return Type.GetType(TypeName); } set { this.TypeName = (value == null ? String.Empty : value.FullName); } }

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

        public void ForceUpdateValue(dynamic Value)
        {
            if (Value == null)
                return;

            this._Value = Value;
        }

    } // End class

} // End namespace