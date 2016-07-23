using Anathema.Source.Engine;
using Anathema.Source.Utils.AddressResolver;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class AddressItem : ProjectItem
    {
        [Obfuscation(Exclude = true)]
        private String _BaseAddress;
        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Category("Properties"), DisplayName("Address"), Description("Base address")]
        public String BaseAddress
        {
            [Obfuscation(Exclude = true)]
            get { return _BaseAddress; }
            [Obfuscation(Exclude = true)]
            set { _BaseAddress = value; OnUpdateField(); }
        }

        [Obfuscation(Exclude = true)]
        private Boolean _IsValueHex;
        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Category("Properties"), DisplayName("Hex"), Description("Whether or not to display value as hexedecimal")]
        public Boolean IsValueHex
        {
            [Obfuscation(Exclude = true)]
            get { return _IsValueHex; }
            [Obfuscation(Exclude = true)]
            set { _IsValueHex = value; OnUpdateField(); }
        }

        [Obfuscation(Exclude = true)]
        private IEnumerable<Int32> _Offsets;
        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Category("Properties"), DisplayName("Offsets"), Description("Address offsets")]
        public IEnumerable<Int32> Offsets
        {
            [Obfuscation(Exclude = true)]
            get { return _Offsets; }
            [Obfuscation(Exclude = true)]
            set { _Offsets = value; OnUpdateField(); }
        }

        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Browsable(false)]
        public String TypeName;
        [Obfuscation(Exclude = true)]
        [Category("Properties"), DisplayName("Type"), Description("Data type of the address")]
        public Type ElementType
        {
            [Obfuscation(Exclude = true)]
            get { return Type.GetType(TypeName); }
            [Obfuscation(Exclude = true)]
            set
            {
                String OldTypeName = this.TypeName;
                TypeName = (value == null ? String.Empty : value.FullName);
                _Value = (OldTypeName != TypeName) ? null : _Value;
                OnUpdateField();
            }
        }

        [Obfuscation(Exclude = true)]
        private dynamic _Value;
        [Obfuscation(Exclude = true)]
        [Category("Properties"), DisplayName("Value"), Description("Value at the address")]
        public dynamic Value
        {
            [Obfuscation(Exclude = true)]
            get { return _Value; }
            [Obfuscation(Exclude = true)]
            set { if (!Activated) { _Value = value; OnUpdateField(); } }
        }

        [Obfuscation(Exclude = true)]
        private IntPtr _EffectiveAddress;
        [Obfuscation(Exclude = true)]
        [Browsable(false)]
        public IntPtr EffectiveAddress
        {
            [Obfuscation(Exclude = true)]
            get { return _EffectiveAddress; }
            [Obfuscation(Exclude = true)]
            private set { _EffectiveAddress = value; OnUpdateField(); }
        }

        public AddressItem() : this(Conversions.ToAddress(IntPtr.Zero), typeof(Int32), "New Address") { }

        public AddressItem(String BaseAddress, Type ElementType, String Description = null, IEnumerable<Int32> Offsets = null, Boolean IsValueHex = false, String Value = null) : base(Description)
        {
            this.BaseAddress = BaseAddress;
            this.ElementType = ElementType;
            this.Offsets = Offsets;
            this.IsValueHex = IsValueHex;

            if (!IsValueHex && CheckSyntax.CanParseValue(ElementType, Value))
                this.Value = Conversions.ParseValue(ElementType, Value);
            else if (IsValueHex && CheckSyntax.CanParseHex(ElementType, Value))
                this.Value = Conversions.ParseHexAsDec(ElementType, Value);
        }

        [Obfuscation(Exclude = true)]
        public String GetValueString()
        {
            if (Value == null)
                return "-";

            if (IsValueHex && CheckSyntax.CanParseValue(ElementType, Value.ToString()))
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
            IntPtr Pointer = AddressResolver.GetInstance().ResolveExpression(BaseAddress);
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