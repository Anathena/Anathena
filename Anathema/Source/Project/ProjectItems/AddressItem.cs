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
    [Obfuscation(ApplyToMembers = true)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class AddressItem : ProjectItem
    {
        private String _BaseAddress;
        [DataMember()]
        [Category("Properties"), DisplayName("Address"), Description("Base address")]
        public String BaseAddress
        {
            get { return _BaseAddress; }
            set { _BaseAddress = value; }
        }

        private Boolean _IsValueHex;
        [DataMember()]
        [Category("Properties"), DisplayName("Hex"), Description("Whether or not to display value as hexedecimal")]
        public Boolean IsValueHex
        {
            get { return _IsValueHex; }
            set { _IsValueHex = value; }
        }

        private IEnumerable<Int32> _Offsets;
        [DataMember()]
        [Category("Properties"), DisplayName("Offsets"), Description("Address offsets")]
        public IEnumerable<Int32> Offsets
        {
            get { return _Offsets; }
            set { _Offsets = value; }
        }

        [DataMember()]
        [Browsable(false)]
        private String TypeName;
        [Category("Properties"), DisplayName("Type"), Description("Data type of the address")]
        public Type ElementType
        {
            get { return Type.GetType(TypeName); }
            set
            {
                String OldTypeName = this.TypeName;
                TypeName = (value == null ? String.Empty : value.FullName);
                _Value = (OldTypeName != TypeName) ? null : _Value;
            }
        }

        private dynamic _Value;
        [Category("Properties"), DisplayName("Value"), Description("Value at the address")]
        public dynamic Value
        {
            get { return _Value; }
            set { if (!Activated) { _Value = value; } }
        }

        private IntPtr _EffectiveAddress;
        [Browsable(false)]
        public IntPtr EffectiveAddress
        {
            get { return _EffectiveAddress; }
            private set { _EffectiveAddress = value; }
        }

        public AddressItem() : this(Conversions.ToAddress(IntPtr.Zero), typeof(Int32), "New Address") { }
        public AddressItem(String BaseAddress, Type ElementType, String Description = null, IEnumerable<Int32> Offsets = null, Boolean IsValueHex = false, String Value = null) : base(Description)
        {
            this._BaseAddress = BaseAddress;
            this._Offsets = Offsets;
            this._IsValueHex = IsValueHex;
            this.ElementType = ElementType;

            if (!_IsValueHex && CheckSyntax.CanParseValue(ElementType, Value))
                this._Value = Conversions.ParseValue(ElementType, Value);
            else if (_IsValueHex && CheckSyntax.CanParseHex(ElementType, Value))
                this._Value = Conversions.ParseHexAsDec(ElementType, Value);
        }

        public String GetValueString()
        {
            if (Value == null)
                return "-";

            if (IsValueHex && CheckSyntax.CanParseValue(ElementType, Value.ToString()))
                return Conversions.ParseDecAsHex(ElementType, Value.ToString());

            return Value.ToString();
        }

        public void ForceUpdateValue(dynamic Value)
        {
            if (Value == null)
                return;

            this._Value = Value;
        }

        public override void Update(EngineCore EngineCore)
        {
            Boolean ReadSuccess;

            ResolveAddress(EngineCore);

            if (EngineCore == null)
                return;

            if (GetActivationState())
            {
                // Freeze current value if this entry is activated
                if (Value != null)
                    EngineCore.Memory.Write(ElementType, EffectiveAddress, Value);
            }
            else
            {
                // Otherwise we read as normal
                Value = EngineCore.Memory.Read(ElementType, EffectiveAddress, out ReadSuccess);
            }
        }

        public String GetAddressString()
        {
            if (Offsets != null && Offsets.Count() > 0)
                return "P->" + Conversions.ToAddress(EffectiveAddress);

            return Conversions.ToAddress(EffectiveAddress);
        }

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