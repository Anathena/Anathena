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
    internal class HexDecTextBox : WatermarkTextBox
    {
        /// <summary>
        /// The data type being represented
        /// </summary>
        private Type elementType;

        /// <summary>
        /// Value indicating whether the value is displayed as hex
        /// </summary>
        private Boolean isHex;

        /// <summary>
        /// Value indicating whether the current value is valid for the current data type
        /// </summary>
        private Boolean textValid;

        /// <summary>
        /// Initializes a new instance of the <see cref="HexDecTextBox" /> class
        /// </summary>
        public HexDecTextBox() : this(typeof(UInt64))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HexDecTextBox" /> class
        /// </summary>
        /// <param name="elementType">The value type being edited</param>
        public HexDecTextBox(Type elementType)
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

            this.ElementType = elementType;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the value is displayed as hex
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
                this.UpdateValidity();
            }
        }

        /// <summary>
        /// Gets or sets the data type being represented
        /// </summary>
        public Type ElementType
        {
            get
            {
                return this.elementType;
            }

            set
            {
                this.elementType = value;
                this.UpdateValidity();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current value is valid for the current data type
        /// </summary>
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

        /// <summary>
        /// Gets or sets the Decimal context menu item
        /// </summary>
        private MenuItem DecimalMenuItem { get; set; }

        /// <summary>
        /// Gets or sets the Hexedecimal context menu item
        /// </summary>
        private MenuItem HexidecimalMenuItem { get; set; }

        /// <summary>
        /// Gets or sets the Convert to Dec context menu item
        /// </summary>
        private MenuItem ConvertToDecMenuItem { get; set; }

        /// <summary>
        /// Gets or sets the Convert to Hex context menu item
        /// </summary>
        private MenuItem ConvertToHexMenuItem { get; set; }

        /// <summary>
        /// Gets or sets the context menu
        /// </summary>
        private ContextMenu RightClickMenu { get; set; }

        /// <summary>
        /// Gets the value as its standard decimal representation
        /// </summary>
        /// <returns>The decimal value</returns>
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

        /// <summary>
        /// Gets the value as a hexedecimal representation
        /// </summary>
        /// <returns>The hexedecimal value string</returns>
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

        /// <summary>
        /// Gets the raw value being represented
        /// </summary>
        /// <returns>The raw value</returns>
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

        /// <summary>
        /// Sets the raw value being represented
        /// </summary>
        /// <param name="value">The raw value</param>
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

        /// <summary>
        /// Determines if the current value is valid for the current data type
        /// </summary>
        /// <returns>True if the current value is valid</returns>
        public Boolean IsValid()
        {
            return this.IsTextValid;
        }

        /// <summary>
        /// Determines if the current text is valid for the current data type
        /// </summary>
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

        /// <summary>
        /// Updates the color based on the validity of the current text
        /// </summary>
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

        /// <summary>
        /// Invoked when the hexedecimal menu item is clicked
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void HexidecimalMenuItemClick(Object sender, EventArgs e)
        {
            this.IsHex = true;
        }

        /// <summary>
        /// Invoked when the decimal menu item is clicked
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void DecimalMenuItemClick(Object sender, EventArgs e)
        {
            this.IsHex = false;
        }

        /// <summary>
        /// Invoked when the convert to hexedecimal menu item is clicked
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void ConvertToHexMenuItemClick(Object sender, EventArgs e)
        {
            if (CheckSyntax.CanParseValue(this.ElementType, this.Text))
            {
                this.Text = Conversions.ParseDecStringAsHexString(this.ElementType, this.Text);
            }

            this.IsHex = true;
        }

        /// <summary>
        /// Invoked when the convert to decimal menu item is clicked
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void ConvertToDecMenuItemClick(Object sender, EventArgs e)
        {
            if (CheckSyntax.CanParseHex(this.ElementType, this.Text))
            {
                this.Text = Conversions.ParseHexStringAsDecString(this.ElementType, this.Text);
            }

            this.IsHex = false;
        }

        /// <summary>
        /// Invoked when the context menu opens
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void RightClickMenuPopup(Object sender, EventArgs e)
        {
            this.DecimalMenuItem.Checked = this.IsHex ? false : true;
            this.HexidecimalMenuItem.Checked = this.IsHex ? true : false;
        }

        /// <summary>
        /// Invoked when the text is changed in the hex dec box
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void HexDecTextChanged(Object sender, EventArgs e)
        {
            this.UpdateValidity();
        }
    }
    //// End class
}
//// End namespace