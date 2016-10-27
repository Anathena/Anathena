namespace Ana.Source.Controls
{
    using Mvvm;
    using System;
    using Utils.Validation;

    internal class HexDecBoxViewModel : ViewModelBase
    {
        private Boolean isHex;

        //// private String text;

        private UInt64 value;

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