namespace Ana.Source.Project.ProjectItems
{
    using Engine;
    using Engine.AddressResolver;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Utils.Extensions;
    using Utils.OffsetEditor;
    using Utils.TypeConverters;
    using Utils.Validation;

    /// <summary>
    /// Defines an address that can be added to the project explorer
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [DataContract]
    internal class AddressItem : ProjectItem
    {
        [Browsable(false)]
        private AddressResolver.ResolveTypeEnum resolveType;

        [Browsable(false)]
        private String baseIdentifier;

        [Browsable(false)]
        private IntPtr baseAddress;

        [Browsable(false)]
        private IEnumerable<Int32> offsets;

        [DataMember]
        [Browsable(false)]
        private String typeName;

        [Browsable(false)]
        private dynamic addressValue;

        [Browsable(false)]
        private Boolean isValueHex;

        [Browsable(false)]
        private IntPtr effectiveAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressItem" /> class
        /// </summary>
        public AddressItem() : this(IntPtr.Zero, typeof(Int32), "New Address")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressItem" /> class
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="elementType"></param>
        /// <param name="description"></param>
        /// <param name="resolveType"></param>
        /// <param name="baseIdentifier"></param>
        /// <param name="offsets"></param>
        /// <param name="isValueHex"></param>
        /// <param name="value"></param>
        public AddressItem(
            IntPtr baseAddress,
            Type elementType,
            String description = "New Address",
            AddressResolver.ResolveTypeEnum resolveType = AddressResolver.ResolveTypeEnum.Module,
            String baseIdentifier = null,
            IEnumerable<Int32> offsets = null,
            Boolean isValueHex = false,
            String value = null)
            : base(description)
        {
            // Bypass setters to avoid running setter code
            this.baseAddress = baseAddress;
            this.ElementType = elementType;
            this.resolveType = resolveType;
            this.baseIdentifier = baseIdentifier;
            this.offsets = offsets;
            this.isValueHex = isValueHex;

            if (!this.isValueHex && CheckSyntax.CanParseValue(elementType, value))
            {
                this.addressValue = Utils.Validation.Conversions.ParseDecStringAsValue(elementType, value);
            }
            else if (this.isValueHex && CheckSyntax.CanParseHex(elementType, value))
            {
                this.addressValue = Conversions.ParseHexStringAsDecString(elementType, value);
            }
        }

        [DataMember]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Properties"), DisplayName("Resolve Type"), Description("Method to use for resolving the address base. If there is an identifier to resolve, the address is treated as an offset.")]
        public AddressResolver.ResolveTypeEnum ResolveType
        {
            get
            {
                return this.resolveType;
            }

            set
            {
                this.resolveType = value;
                ProjectExplorerDeprecated.GetInstance().ProjectChanged();
                this.NotifyPropertyChanged(nameof(this.ResolveType));
            }
        }

        [DataMember]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Properties"), DisplayName("Resolve Id"), Description("Text identifier to use when resolving the base address, such as a module or .NET Object name")]
        public String BaseIdentifier
        {
            get
            {
                return this.baseIdentifier;
            }

            set
            {
                this.baseIdentifier = value == null ? String.Empty : value;
                ProjectExplorerDeprecated.GetInstance().ProjectChanged();
                this.NotifyPropertyChanged(nameof(this.BaseIdentifier));
            }
        }

        [DataMember]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(AddressConverter))]
        [Category("Properties"), DisplayName("Address Base"), Description("Base address")]
        public IntPtr BaseAddress
        {
            get
            {
                return this.baseAddress;
            }

            set
            {
                this.EffectiveAddress = value;
                this.baseAddress = value;
                ProjectExplorerDeprecated.GetInstance().ProjectChanged();
                this.NotifyPropertyChanged(nameof(this.BaseAddress));
            }
        }

        [DataMember]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(OffsetConverter))]
        [Editor(typeof(OffsetEditorModel), typeof(UITypeEditor))]
        [Category("Properties"), DisplayName("Address Offsets"), Description("Address offsets")]
        public IEnumerable<Int32> Offsets
        {
            get
            {
                return this.offsets;
            }

            set
            {
                this.offsets = value;
                ProjectExplorerDeprecated.GetInstance().ProjectChanged();
                this.NotifyPropertyChanged(nameof(this.Offsets));
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ValueTypeConverter))]
        [Category("Properties"), DisplayName("Value Type"), Description("Data type of the address")]
        public Type ElementType
        {
            get
            {
                return Type.GetType(this.typeName);
            }

            set
            {
                String oldTypeName = this.typeName;
                this.typeName = value == null ? String.Empty : value.FullName;
                this.addressValue = (oldTypeName != this.typeName) ? null : this.addressValue;
                ProjectExplorerDeprecated.GetInstance().ProjectChanged();
                this.NotifyPropertyChanged(nameof(this.ElementType));
            }
        }

        [TypeConverter(typeof(DynamicConverter))]
        [Category("Properties"), DisplayName("Value"), Description("Value at the address")]
        public dynamic Value
        {
            get
            {
                return this.addressValue;
            }

            set
            {
                this.addressValue = value;
                this.WriteValue(value);
                this.NotifyPropertyChanged(nameof(this.Value));
            }
        }

        [DataMember]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Properties"), DisplayName("Value as Hex"), Description("Whether or not to display value as hexedecimal")]
        public Boolean IsValueHex
        {
            get
            {
                return this.isValueHex;
            }

            set
            {
                this.isValueHex = value;
                ProjectExplorerDeprecated.GetInstance().ProjectChanged();
                this.NotifyPropertyChanged(nameof(this.IsValueHex));
            }
        }

        [ReadOnly(true)]
        [TypeConverter(typeof(AddressConverter))]
        [Category("Properties"), DisplayName("Address"), Description("Effective address")]
        public IntPtr EffectiveAddress
        {
            get
            {
                return this.effectiveAddress;
            }

            private set
            {
                this.effectiveAddress = value;
                this.NotifyPropertyChanged(nameof(this.EffectiveAddress));
            }
        }

        public override void Update()
        {
            this.EffectiveAddress = this.ResolveAddress();

            if (this.IsActivated)
            {
                // Freeze current value if this entry is activated
                dynamic value = this.Value;
                if (value != null && value.GetType() == this.ElementType)
                {
                    this.WriteValue(value);
                }
            }
            else
            {
                // Otherwise we read as normal (bypass value setter and set value directly to avoid a write-back to memory)
                Boolean readSuccess;
                this.addressValue = EngineCore.GetInstance()?.OperatingSystemAdapter?.Read(this.ElementType, this.EffectiveAddress, out readSuccess);
            }
        }

        public String GetAddressString()
        {
            if (this.Offsets != null && this.Offsets.Count() > 0)
            {
                return "P->" + Conversions.ToAddress(this.EffectiveAddress);
            }

            return Conversions.ToAddress(this.EffectiveAddress);
        }

        public IntPtr ResolveAddress()
        {
            IntPtr pointer = IntPtr.Zero;
            Boolean successReading = true;

            switch (this.ResolveType)
            {
                case AddressResolver.ResolveTypeEnum.Module:
                    pointer = this.BaseAddress;
                    break;
                case AddressResolver.ResolveTypeEnum.DotNet:
                    pointer = AddressResolver.GetInstance().ResolveDotNetObject(this.BaseIdentifier);
                    pointer = pointer.Add(this.BaseAddress);
                    break;
            }

            if (EngineCore.GetInstance() == null)
            {
                if (this.Offsets != null && this.Offsets.Count() > 0)
                {
                    pointer = IntPtr.Zero;
                }

                return pointer;
            }

            if (this.Offsets == null || this.Offsets.Count() == 0)
            {
                return pointer;
            }

            foreach (Int32 offset in this.Offsets)
            {
                if (EngineCore.GetInstance().Processes.IsOpenedProcess32Bit())
                {
                    pointer = EngineCore.GetInstance().OperatingSystemAdapter.Read<Int32>(pointer, out successReading).ToIntPtr();
                }
                else
                {
                    pointer = EngineCore.GetInstance().OperatingSystemAdapter.Read<Int64>(pointer, out successReading).ToIntPtr();
                }

                if (pointer == IntPtr.Zero)
                {
                    break;
                }

                pointer = pointer.Add(offset);

                if (!successReading)
                {
                    break;
                }
            }

            return pointer;
        }

        private void WriteValue(dynamic newValue)
        {
            if (newValue == null)
            {
                return;
            }

            EngineCore.GetInstance()?.OperatingSystemAdapter?.Write(this.ElementType, this.EffectiveAddress, newValue);
        }
    }
    //// End class
}
//// End namespace