namespace Ana.Source.Controls
{
    using Mvvm;
    using System;
    using Utils.Validation;

    internal class HexDecBoxViewModel : ViewModelBase
    {
        private Boolean isHex;

        private String text;

        public HexDecBoxViewModel()
        {
            this.Text = String.Empty;
        }

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
        }

        public UInt64 Value
        {
            get
            {
                String RawValue = this.text;

                if (this.IsDec && CheckSyntax.IsUInt64(RawValue))
                {
                    return Conversions.ParseDecStringAsValue(typeof(UInt64), RawValue);
                }
                else if (this.IsHex && CheckSyntax.CanParseHex(typeof(UInt64), RawValue))
                {
                    return Conversions.ParseHexStringAsValue(typeof(UInt64), RawValue);
                }

                return 0;
            }
        }

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