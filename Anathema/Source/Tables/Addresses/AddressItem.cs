using Anathema.Source.Engine;
using Anathema.Source.Utils.AddressResolver;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Tables.Addresses
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class AddressItem : TableItem
    {
        [Obfuscation(Exclude = true)]
        [DataMember()]
        public String BaseAddress { get; set; }

        [Obfuscation(Exclude = true)]
        [DataMember()]
        public String Description { get; set; }

        [Obfuscation(Exclude = true)]
        [DataMember()]
        public Boolean IsHex { get; set; }

        [Obfuscation(Exclude = true)]
        [DataMember()]
        public IEnumerable<Int32> Offsets { get; set; }

        [Obfuscation(Exclude = true)]
        [DataMember()]
        public String TypeName { get; set; }

        [Obfuscation(Exclude = true)]
        public Type ElementType
        {
            [Obfuscation(Exclude = true)]
            get { return Type.GetType(TypeName); }
            [Obfuscation(Exclude = true)]
            set
            {
                String OldTypeName = this.TypeName;
                this.TypeName = (value == null ? String.Empty : value.FullName);
                _Value = (OldTypeName != TypeName) ? null : _Value;
            }
        }

        [Obfuscation(Exclude = true)]
        private dynamic _Value;
        [Obfuscation(Exclude = true)]
        public dynamic Value
        {
            [Obfuscation(Exclude = true)]
            get { return _Value; }
            [Obfuscation(Exclude = true)]
            set { if (!Activated) _Value = value; }
        }

        [Obfuscation(Exclude = true)]
        private IntPtr _EffectiveAddress;
        [Obfuscation(Exclude = true)]
        public IntPtr EffectiveAddress
        {
            [Obfuscation(Exclude = true)]
            get { return _EffectiveAddress; }
            [Obfuscation(Exclude = true)]
            private set { _EffectiveAddress = value; }
        }

        public AddressItem(String BaseAddress, Type ElementType, String Description = null, IEnumerable<Int32> Offsets = null, Boolean IsHex = false, String Value = null)
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
        }

        [Obfuscation(Exclude = true)]
        public String GetValueString()
        {
            if (Value == null)
                return "-";

            if (IsHex && CheckSyntax.CanParseValue(ElementType, Value.ToString()))
                return Conversions.ParseDecAsHex(ElementType, Value.ToString());

            return Value.ToString();
        }

        [Obfuscation(Exclude = true)]
        public void ForceUpdateValue(dynamic Value)
        {
            if (Value == null)
                return;

            this._Value = Value;
        }

        [Obfuscation(Exclude = true)]
        public String GetAddressString()
        {
            if (Offsets != null && Offsets.Count() > 0)
                return "P->" + Conversions.ToAddress(EffectiveAddress);

            return Conversions.ToAddress(EffectiveAddress);
        }

        [Obfuscation(Exclude = true)]
        public void ResolveAddress(EngineCore EngineCore)
        {
            IntPtr Pointer = AddressResolver.GetInstance().ResolveAddress(BaseAddress);
            Boolean SuccessReading = true;

            if (EngineCore == null)
            {
                if (Offsets == null || Offsets.Count() == 0)
                    EffectiveAddress = Pointer;
                else
                    EffectiveAddress = IntPtr.Zero;

                return;
            }

            if (Offsets == null || Offsets.Count() == 0)
            {
                this.EffectiveAddress = Pointer;
                return;
            }

            foreach (Int32 Offset in Offsets)
            {
                Pointer = EngineCore.Memory.Read<IntPtr>(Pointer, out SuccessReading);
                Pointer = Pointer.Add(Offset);

                if (!SuccessReading)
                    break;
            }

            this.EffectiveAddress = Pointer;
        }

    } // End class

} // End namespace