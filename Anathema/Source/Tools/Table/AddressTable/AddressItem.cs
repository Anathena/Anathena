using Binarysharp.MemoryManagement;
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
        public UInt64 BaseAddress { get; set; }

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

        private UInt64 _EffectiveAddress;
        public UInt64 EffectiveAddress { get { return _EffectiveAddress; } private set { _EffectiveAddress = value; } }

        public AddressItem(UInt64 BaseAddress, Type ElementType, String Description = null, Int32[] Offsets = null, Boolean IsHex = false, String Value = null)
        {
            this.BaseAddress = BaseAddress;
            this.Description = Description == null ? String.Empty : Description;
            this.ElementType = ElementType;
            this.Offsets = Offsets;
            this.IsHex = IsHex;

            if (!IsHex && CheckSyntax.CanParseValue(ElementType, Value))
                this.Value = Conversions.ParseValue(ElementType, Value);
            else if (IsHex && CheckSyntax.CanParseHex(ElementType, Value))
                this.Value = Conversions.ParseHexAsDec(ElementType, Value);

            this.EffectiveAddress = BaseAddress;
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

        public String GetAddressString()
        {
            if (Offsets != null && Offsets.Length > 0)
                return "P->" + Conversions.ToAddress(EffectiveAddress);

            return Conversions.ToAddress(EffectiveAddress);
        }

        public void ResolveAddress(MemoryEditor MemoryEditor)
        {
            UInt64 Pointer = this.BaseAddress;
            Boolean SuccessReading = true;

            if (Offsets == null)
            { 
                this.EffectiveAddress = Pointer;
                return;
            }

            foreach (Int32 Offset in Offsets)
            {
                Pointer = MemoryEditor.Read<UInt64>((IntPtr)Pointer, out SuccessReading);
                Pointer += (UInt64)Offset;

                if (!SuccessReading)
                    break;
            }

            this.EffectiveAddress = Pointer;
        }

    } // End class

} // End namespace