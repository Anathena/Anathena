using Anathema.GUI.Tools.Projects.TypeEditors;
using Anathema.Source.Engine;
using Anathema.Source.Project.ProjectItems.TypeConverters;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
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
        public enum ResolveTypeEnum
        {
            Module,
            DotNet,
            Java
        }

        private ResolveTypeEnum _ResolveType;
        [DataMember()]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Properties"), DisplayName("Resolve Type"), Description("Method to use for resolving the address base. If there is an identifier to resolve, the address is treated as an offset.")]
        public ResolveTypeEnum ResolveType
        {
            get { return _ResolveType; }
            set { _ResolveType = value; }
        }

        private String _BaseIdentifier;
        [DataMember()]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Properties"), DisplayName("Resolve Id"), Description("Text identifier to use when resolving the base address, such as a module or .NET Object name")]
        public String BaseIdentifier
        {
            get { return _BaseIdentifier; }
            set { _BaseIdentifier = value == null ? String.Empty : value; }
        }

        private IntPtr _BaseAddress;
        [DataMember()]
        [TypeConverter(typeof(IntPtrConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Properties"), DisplayName("Address Base"), Description("Base address")]
        public IntPtr BaseOffset
        {
            get { return _BaseAddress; }
            set { EffectiveAddress = value; _BaseAddress = value; }
        }

        private IEnumerable<Int32> _Offsets;
        [DataMember()]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(ArrayEditor), typeof(UITypeEditor))]
        [Category("Properties"), DisplayName("Address Offsets"), Description("Address offsets")]
        public IEnumerable<Int32> Offsets
        {
            get { return _Offsets; }
            set { _Offsets = value; }
        }

        [DataMember()]
        [Browsable(false)]
        private String TypeName;
        [RefreshProperties(RefreshProperties.All)]
        [Category("Properties"), DisplayName("Value Type"), Description("Data type of the address")]
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

        private Boolean _IsValueHex;
        [DataMember()]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Properties"), DisplayName("Value as Hex"), Description("Whether or not to display value as hexedecimal")]
        public Boolean IsValueHex
        {
            get { return _IsValueHex; }
            set { _IsValueHex = value; }
        }

        private IntPtr _EffectiveAddress;
        [ReadOnly(true)]
        [TypeConverter(typeof(IntPtrConverter))]
        [Category("Properties"), DisplayName("Address"), Description("Effective address")]
        public IntPtr EffectiveAddress
        {
            get { return _EffectiveAddress; }
            private set { _EffectiveAddress = value; }
        }

        public AddressItem() : this(IntPtr.Zero, typeof(Int32), "New Address") { }
        public AddressItem(IntPtr BaseAddress, Type ElementType, String Description = null, ResolveTypeEnum ResolveType = ResolveTypeEnum.Module, String BaseIdentifier = null,
            IEnumerable<Int32> Offsets = null, Boolean IsValueHex = false, String Value = null) : base(Description)
        {
            this._BaseAddress = BaseAddress;
            this.ElementType = ElementType;
            this._ResolveType = ResolveType;
            this._BaseIdentifier = BaseIdentifier;
            this._Offsets = Offsets;
            this._IsValueHex = IsValueHex;

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
            IntPtr Pointer = BaseOffset;
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

                if (Pointer == IntPtr.Zero)
                    break;

                Pointer = Pointer.Add(Offset);

                if (!SuccessReading)
                    break;
            }

            this.EffectiveAddress = Pointer;
        }

    } // End class

} // End namespace