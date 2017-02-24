namespace Ana.Source.Project.ProjectItems
{
    using Editors.OffsetEditor;
    using Engine;
    using Engine.AddressResolver;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Linq;
    using System.Runtime.Serialization;
    using Utils;
    using Utils.Extensions;
    using Utils.TypeConverters;

    /// <summary>
    /// Defines an address that can be added to the project explorer.
    /// </summary>
    [DataContract]
    internal class AddressItem : ProjectItem
    {
        /// <summary>
        /// The identifier type for this address item.
        /// </summary>
        [Browsable(false)]
        private AddressResolver.ResolveTypeEnum resolveType;

        /// <summary>
        /// The identifier for the base address of this object.
        /// </summary>
        [Browsable(false)]
        private String baseIdentifier;

        /// <summary>
        /// The base address of this object. This will be added as an offset from the resolved base identifier.
        /// </summary>
        [Browsable(false)]
        private IntPtr baseAddress;

        /// <summary>
        /// The pointer offsets of this address item.
        /// </summary>
        [Browsable(false)]
        private IEnumerable<Int32> offsets;

        /// <summary>
        /// The type name of the element type of this address. Used for serailization purposes.
        /// </summary>
        [DataMember]
        [Browsable(false)]
        private String typeName;

        /// <summary>
        /// The value at this address.
        /// </summary>
        [Browsable(false)]
        private dynamic addressValue;

        /// <summary>
        /// A value indicating whether the value at this address should be displayed as hex.
        /// </summary>
        [Browsable(false)]
        private Boolean isValueHex;

        /// <summary>
        /// The effective address after tracing all pointer offsets.
        /// </summary>
        [Browsable(false)]
        private IntPtr effectiveAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressItem" /> class.
        /// </summary>
        public AddressItem() : this(IntPtr.Zero, typeof(Int32), "New Address")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressItem" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address. This will be added as an offset from the resolved base identifier.</param>
        /// <param name="elementType">The data type of the value at this address.</param>
        /// <param name="description">The description of this address.</param>
        /// <param name="resolveType">The identifier type for this address item.</param>
        /// <param name="baseIdentifier">The identifier for the base address of this object.</param>
        /// <param name="offsets">The pointer offsets of this address item.</param>
        /// <param name="isValueHex">A value indicating whether the value at this address should be displayed as hex.</param>
        /// <param name="value">The value at this address. If none provided, it will be figured out later. Used here to allow immediate view updates upon creation.</param>
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
                this.addressValue = Conversions.ParsePrimitiveStringAsDynamic(elementType, value);
            }
            else if (this.isValueHex && CheckSyntax.CanParseHex(elementType, value))
            {
                this.addressValue = Conversions.ParseHexStringAsPrimitiveString(elementType, value);
            }
        }

        /// <summary>
        /// Gets or sets the identifier type for this address item.
        /// </summary>
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
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.ResolveType));
            }
        }

        /// <summary>
        /// Gets or sets the identifier for the base address of this object.
        /// </summary>
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
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.BaseIdentifier));
            }
        }

        /// <summary>
        /// Gets or sets the base address of this object. This will be added as an offset from the resolved base identifier.
        /// </summary>
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
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.BaseAddress));
            }
        }

        /// <summary>
        /// Gets or sets the pointer offsets of this address item.
        /// </summary>
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
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.Offsets));
            }
        }

        /// <summary>
        /// Gets or sets the element type of the value at this address.
        /// </summary>
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
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.ElementType));
            }
        }

        /// <summary>
        /// Gets or sets the value at this address.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether the value at this address should be displayed as hex.
        /// </summary>
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
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.IsValueHex));
            }
        }

        /// <summary>
        /// Gets the effective address after tracing all pointer offsets.
        /// </summary>
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

        /// <summary>
        /// Update event for this project item. Resolves addresses and values.
        /// </summary>
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

        /// <summary>
        /// Clones the project item.
        /// </summary>
        /// <returns>The clone of the project item.</returns>
        public override ProjectItem Clone()
        {
            AddressItem clone = new AddressItem();
            clone.Description = this.Description;
            clone.Parent = this.Parent;
            clone.resolveType = this.resolveType;
            clone.baseIdentifier = this.baseIdentifier;
            clone.baseAddress = this.baseAddress;
            clone.offsets = this.offsets?.ToArray();
            clone.typeName = this.typeName;
            clone.addressValue = this.addressValue;
            clone.isValueHex = this.isValueHex;
            clone.effectiveAddress = this.effectiveAddress;
            return clone;
        }

        /// <summary>
        /// Resolves the address of an address, pointer, or managed object.
        /// </summary>
        /// <returns>The base address of this object.</returns>
        public IntPtr ResolveAddress()
        {
            IntPtr pointer = IntPtr.Zero;
            Boolean successReading = true;

            switch (this.ResolveType)
            {
                case AddressResolver.ResolveTypeEnum.Module:
                    pointer = AddressResolver.GetInstance().ResolveModule(this.BaseIdentifier);
                    break;
                case AddressResolver.ResolveTypeEnum.GlobalKeyword:
                    pointer = AddressResolver.GetInstance().ResolveGlobalKeyword(this.BaseIdentifier);
                    break;
                case AddressResolver.ResolveTypeEnum.DotNet:
                    pointer = AddressResolver.GetInstance().ResolveDotNetObject(this.BaseIdentifier);
                    break;
            }

            pointer = pointer.Add(this.BaseAddress);

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

                pointer = pointer.Add(offset);

                if (pointer == IntPtr.Zero || !successReading)
                {
                    pointer = IntPtr.Zero;
                    break;
                }
            }

            return pointer;
        }

        /// <summary>
        /// Writes a value to the computed address of this item.
        /// </summary>
        /// <param name="newValue">The value to write.</param>
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