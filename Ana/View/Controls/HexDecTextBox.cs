namespace Ana.View.Controls
{
    using Source.Utils.Validation;
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// A textbox that allows for hex and dec values to be stored, validated, and colored. Extends WatermarkTextBox.
    /// </summary>
    public class HexDecTextBox : WatermarkTextBox
    {
        private Boolean isHex;

        private Boolean textValid;

        public HexDecTextBox()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(0x33, 0x33, 0x33); // Color.Gray; // Color.FromArgb(0x333333);

            this.DecimalMenuItem = new MenuItem("Decimal");
            this.HexidecimalMenuItem = new MenuItem("Hexidecimal");
            this.ConvertToDecMenuItem = new MenuItem("Convert to Dec");
            this.ConvertToHexMenuItem = new MenuItem("Convert to Hex");

            this.RightClickMenu = new ContextMenu(new MenuItem[] { this.DecimalMenuItem, this.HexidecimalMenuItem, new MenuItem("-"), this.ConvertToDecMenuItem, this.ConvertToHexMenuItem });

            this.RightClickMenu.Popup += this.RightClickMenuPopup;
            this.DecimalMenuItem.Click += this.DecimalMenuItemClick;
            this.HexidecimalMenuItem.Click += this.HexidecimalMenuItemClick;

            this.ConvertToDecMenuItem.Click += this.ConvertToDecMenuItemClick;
            this.ConvertToHexMenuItem.Click += this.ConvertToHexMenuItemClick;

            this.ContextMenu = this.RightClickMenu;
            this.TextChanged += this.HexDecTextChanged;

            this.ElementType = typeof(UInt64);
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
                this.UpdateValidity();
            }
        }

        private Boolean IsTextValid
        {
            get
            {
                return this.textValid;
            }

            set
            {
                this.textValid = value;
                this.UpdateColor();
            }
        }

        private Type ElementType { get; set; }

        private MenuItem DecimalMenuItem { get; set; }

        private MenuItem HexidecimalMenuItem { get; set; }

        private MenuItem ConvertToDecMenuItem { get; set; }

        private MenuItem ConvertToHexMenuItem { get; set; }

        private ContextMenu RightClickMenu { get; set; }

        public void SetElementType(Type elementType)
        {
            this.ElementType = elementType;
            this.UpdateValidity();
        }

        public String GetValueAsDecimal()
        {
            if (!this.IsTextValid)
            {
                return null;
            }

            if (this.IsHex)
            {
                return Conversions.ParseValueAsDec(this.ElementType, Conversions.ParseHexStringAsDecString(this.ElementType, this.Text));
            }
            else
            {
                return Conversions.ParseValueAsDec(this.ElementType, this.Text);
            }
        }

        public String GetValueAsHexidecimal()
        {
            if (!this.IsTextValid)
            {
                return null;
            }

            if (this.IsHex)
            {
                return Conversions.ParseDecStringAsHexString(this.ElementType, Conversions.ParseHexStringAsDecString(this.ElementType, this.Text));
            }
            else
            {
                return Conversions.ParseDecStringAsHexString(this.ElementType, Conversions.ParseValueAsDec(this.ElementType, this.Text));
            }
        }

        public dynamic GetValue()
        {
            if (!this.IsTextValid)
            {
                return null;
            }

            if (this.IsHex)
            {
                return Conversions.ParseHexStringAsValue(this.ElementType, this.Text);
            }
            else
            {
                return Conversions.ParseDecStringAsValue(this.ElementType, this.Text);
            }
        }

        public String GetRawValue()
        {
            return this.Text;
        }

        [Obfuscation(Exclude = true)]
        public void SetValue(dynamic value)
        {
            if (value == null)
            {
                return;
            }

            String valueString = value.ToString();

            if (!CheckSyntax.CanParseValue(this.ElementType, valueString))
            {
                return;
            }

            if (this.IsHex)
            {
                this.Text = Conversions.ParseDecStringAsHexString(this.ElementType, valueString);
            }
            else
            {
                this.Text = Conversions.ParseValueAsDec(this.ElementType, valueString);
            }
        }

        public void SetText(String value)
        {
            this.Text = value;
        }

        public Boolean IsValid()
        {
            return this.IsTextValid;
        }

        private void HexidecimalMenuItemClick(Object sender, EventArgs e)
        {
            this.IsHex = true;
        }

        private void DecimalMenuItemClick(Object sender, EventArgs e)
        {
            this.IsHex = false;
        }

        private void ConvertToHexMenuItemClick(Object sender, EventArgs e)
        {
            if (CheckSyntax.CanParseValue(this.ElementType, this.Text))
            {
                this.Text = Conversions.ParseDecStringAsHexString(this.ElementType, this.Text);
            }

            this.IsHex = true;
        }

        private void ConvertToDecMenuItemClick(Object sender, EventArgs e)
        {
            if (CheckSyntax.CanParseHex(this.ElementType, this.Text))
            {
                this.Text = Conversions.ParseHexStringAsDecString(this.ElementType, this.Text);
            }

            this.IsHex = false;
        }

        private void RightClickMenuPopup(Object sender, EventArgs e)
        {
            this.DecimalMenuItem.Checked = this.IsHex ? false : true;
            this.HexidecimalMenuItem.Checked = this.IsHex ? true : false;
        }

        private void HexDecTextChanged(Object sender, EventArgs e)
        {
            this.UpdateValidity();
        }

        private void UpdateValidity()
        {
            if (this.IsHex)
            {
                if (CheckSyntax.CanParseHex(this.ElementType, this.Text))
                {
                    this.IsTextValid = true;
                    return;
                }
            }
            else
            {
                if (CheckSyntax.CanParseValue(this.ElementType, this.Text))
                {
                    this.IsTextValid = true;
                    return;
                }
            }

            this.IsTextValid = false;
            return;
        }

        private void UpdateColor()
        {
            if (!this.IsTextValid)
            {
                this.ForeColor = Color.Red;
            }
            else if (this.IsHex)
            {
                this.ForeColor = Color.ForestGreen;
            }
            else
            {
                this.ForeColor = Color.White;
            }

            this.Invalidate();
        }
    }
    //// End class
}
//// End namespace