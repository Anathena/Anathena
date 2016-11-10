namespace Ana.Source.Controls
{
    using Mvvm;
    using System;
    using Utils.Validation;

    /// <summary>
    /// View model for the Hex Dec Box
    /// </summary>
    internal class HexDecBoxViewModel : ViewModelBase
    {
        /// <summary>
        /// A value indicating whether the represented value is hexedecimal
        /// </summary>
        private Boolean isHex;

        //// private String text;

        /// <summary>
        /// The value represented
        /// </summary>
        private UInt64 value;

        /// <summary>
        ///  Initializes a new instance of the <see cref="HexDecBoxViewModel" /> class
        /// </summary>
        public HexDecBoxViewModel()
        {
            //// this.Text = String.Empty;
        }

        /*
        public String Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.RaisePropertyChanged(nameof(this.Text));
            }
        }*/

        /// <summary>
        /// Gets or sets the value being represented
        /// </summary>
        public UInt64 Value
        {
            get
            {
                String rawValue = String.Empty; //// this.text;

                if (this.IsDec && CheckSyntax.IsUInt64(rawValue))
                {
                    this.value = Conversions.ParseDecStringAsValue(typeof(UInt64), rawValue);
                }
                else if (this.IsHex && CheckSyntax.CanParseHex(typeof(UInt64), rawValue))
                {
                    this.value = Conversions.ParseHexStringAsValue(typeof(UInt64), rawValue);
                }

                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the represented value is hexedecimal
        /// </summary>
        public Boolean IsHex
        {
            get
            {
                return this.isHex;
            }

            set
            {
                this.isHex = value;
                this.RaisePropertyChanged(nameof(this.IsDec));
                this.RaisePropertyChanged(nameof(this.IsHex));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the represented value is decimal
        /// </summary>
        public Boolean IsDec
        {
            get
            {
                return !this.isHex;
            }

            set
            {
                this.isHex = !value;
                this.RaisePropertyChanged(nameof(this.IsDec));
                this.RaisePropertyChanged(nameof(this.IsHex));
            }
        }
    }
    //// End class
}
//// End namespace