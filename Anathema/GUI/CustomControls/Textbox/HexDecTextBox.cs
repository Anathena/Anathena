using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anathema
{
    /// <summary>
    /// A textbox that supports a watermak hint.
    /// </summary>
    public class HexDecTextBox : TextBox
    {
        protected Boolean _IsHex;
        public Boolean IsHex
        {
            get { return this._IsHex; }
            set
            {
                this._IsHex = value;
                this.ForeColor = IsHex ? Color.Green : Color.Black;
                UpdateColor();
            }
        }

        private Type ElementType;

        private static MenuItem DecimalMenuItem = new MenuItem("Decimal");
        private static MenuItem HexidecimalMenuItem = new MenuItem("Hexidecimal");
        private static ContextMenu RightClickMenu = new ContextMenu(new MenuItem[] { DecimalMenuItem, HexidecimalMenuItem });

        public HexDecTextBox()
        {
            RightClickMenu.Popup += RightClickMenu_Popup;
            DecimalMenuItem.Click += DecimalMenuItem_Click;
            HexidecimalMenuItem.Click += HexidecimalMenuItem_Click;

            this.ContextMenu = RightClickMenu;
            this.TextChanged += HexDecTextChanged;
        }

        public void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;
        }

        private void HexidecimalMenuItem_Click(Object Sender, EventArgs E)
        {
            this.IsHex = true;
        }

        private void DecimalMenuItem_Click(Object Sender, EventArgs E)
        {
            this.IsHex = false;
        }

        private void RightClickMenu_Popup(Object Sender, EventArgs E)
        {
            DecimalMenuItem.Checked = IsHex ? false : true;
            HexidecimalMenuItem.Checked = IsHex ? true : false;
        }

        private void HexDecTextChanged(Object Sender, EventArgs E)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            if (IsHex)
            {
                if ((CheckSyntax.Address(this.Text) && CheckSyntax.CanParseValue(ElementType, Conversions.AddressToValue(this.Text).ToString())))
                {
                    this.ForeColor = Color.Green;
                    return;
                }
            }
            else
            {
                if (CheckSyntax.CanParseValue(ElementType, this.Text))
                { 
                    this.ForeColor = SystemColors.ControlText;
                    return;
                }
            }
            this.ForeColor = Color.Red;
        }

    } // End class

} // End namespace