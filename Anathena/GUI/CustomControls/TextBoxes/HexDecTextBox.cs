using Anathena.Source.Utils.Validation;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Anathena.GUI.CustomControls.TextBoxes
{
    /// <summary>
    /// A textbox that allows for hex and dec values to be stored, validated, and colored. Extends WatermarkTextBox.
    /// </summary>
    public class HexDecTextBox : WatermarkTextBox
    {
        protected Boolean _isHex;
        public Boolean IsHex
        {
            get { return this._isHex; }
            set
            {
                this._isHex = value;
                UpdateValidity();
            }
        }

        private Boolean _textValid;
        private Boolean TextValid
        {
            get { return this._textValid; }
            set
            {
                this._textValid = value;
                UpdateColor();
            }
        }

        private Type elementType { get; set; }

        private MenuItem decimalMenuItem;
        private MenuItem hexidecimalMenuItem;
        private MenuItem convertToDecMenuItem;
        private MenuItem convertToHexMenuItem;
        private ContextMenu rightClickMenu;

        public HexDecTextBox()
        {
            decimalMenuItem = new MenuItem("Decimal");
            hexidecimalMenuItem = new MenuItem("Hexidecimal");
            convertToDecMenuItem = new MenuItem("Convert to Dec");
            convertToHexMenuItem = new MenuItem("Convert to Hex");

            rightClickMenu = new ContextMenu(new MenuItem[] { decimalMenuItem, hexidecimalMenuItem, new MenuItem("-"), convertToDecMenuItem, convertToHexMenuItem });

            rightClickMenu.Popup += RightClickMenu_Popup;
            decimalMenuItem.Click += DecimalMenuItem_Click;
            hexidecimalMenuItem.Click += HexidecimalMenuItem_Click;

            convertToDecMenuItem.Click += ConvertToDecMenuItem_Click;
            convertToHexMenuItem.Click += ConvertToHexMenuItem_Click;

            this.ContextMenu = rightClickMenu;
            this.TextChanged += HexDecTextChanged;

            this.elementType = typeof(UInt64);
        }

        public void SetElementType(Type elementType)
        {
            this.elementType = elementType;
            UpdateValidity();
        }

        public String GetValueAsDecimal()
        {
            if (!TextValid)
                return null;

            if (IsHex)
                return Conversions.ParseValueAsDec(elementType, Conversions.ParseHexStringAsDecString(elementType, this.Text));
            else
                return Conversions.ParseValueAsDec(elementType, this.Text);
        }

        public String GetValueAsHexidecimal()
        {
            if (!TextValid)
                return null;

            if (IsHex)
                return Conversions.ParseDecStringAsHexString(elementType, Conversions.ParseHexStringAsDecString(elementType, this.Text));
            else
                return Conversions.ParseDecStringAsHexString(elementType, Conversions.ParseValueAsDec(elementType, this.Text));
        }

        public String GetRawValue()
        {
            return this.Text;
        }

        [Obfuscation(Exclude = true)]
        public void SetValue(dynamic value)
        {
            if (value == null)
                return;

            String ValueString = value.ToString();

            if (!CheckSyntax.CanParseValue(elementType, ValueString))
                return;

            if (IsHex)
                this.Text = Conversions.ParseDecStringAsHexString(elementType, ValueString);
            else
                this.Text = Conversions.ParseValueAsDec(elementType, ValueString);
        }

        public void SetText(String value)
        {
            this.Text = value;
        }

        public Boolean IsValid()
        {
            return TextValid;
        }

        private void HexidecimalMenuItem_Click(Object sender, EventArgs e)
        {
            this.IsHex = true;
        }

        private void DecimalMenuItem_Click(Object sender, EventArgs e)
        {
            this.IsHex = false;
        }

        private void ConvertToHexMenuItem_Click(Object sender, EventArgs e)
        {
            if (CheckSyntax.CanParseValue(elementType, this.Text))
                this.Text = Conversions.ParseDecStringAsHexString(elementType, this.Text);

            this.IsHex = true;
        }

        private void ConvertToDecMenuItem_Click(Object sender, EventArgs e)
        {
            if (CheckSyntax.CanParseHex(elementType, this.Text))
                this.Text = Conversions.ParseHexStringAsDecString(elementType, this.Text);

            this.IsHex = false;
        }

        private void RightClickMenu_Popup(Object sender, EventArgs e)
        {
            decimalMenuItem.Checked = IsHex ? false : true;
            hexidecimalMenuItem.Checked = IsHex ? true : false;
        }

        private void HexDecTextChanged(Object sender, EventArgs e)
        {
            UpdateValidity();
        }

        private void UpdateValidity()
        {
            if (IsHex)
            {
                if ((CheckSyntax.CanParseHex(elementType, this.Text)))
                {
                    TextValid = true;
                    return;
                }
            }
            else
            {
                if (CheckSyntax.CanParseValue(elementType, this.Text))
                {
                    TextValid = true;
                    return;
                }
            }

            TextValid = false;
            return;
        }

        private void UpdateColor()
        {
            if (!TextValid)
                this.ForeColor = Color.Red;
            else if (IsHex)
                this.ForeColor = Color.Green;
            else
                this.ForeColor = SystemColors.ControlText;

            this.Invalidate();
        }

    } // End class

} // End namespace