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
                UpdateColor();
            }
        }

        private Type ElementType;

        private MenuItem DecimalMenuItem;
        private MenuItem HexidecimalMenuItem;
        private ContextMenu RightClickMenu;

        public HexDecTextBox()
        {
            DecimalMenuItem = new MenuItem("Decimal");
            HexidecimalMenuItem  = new MenuItem("Hexidecimal");
            RightClickMenu = new ContextMenu(new MenuItem[] { DecimalMenuItem, HexidecimalMenuItem });

            RightClickMenu.Popup += RightClickMenu_Popup;
            DecimalMenuItem.Click += DecimalMenuItem_Click;
            HexidecimalMenuItem.Click += HexidecimalMenuItem_Click;

            this.ContextMenu = RightClickMenu;
            this.TextChanged += HexDecTextChanged;

            this.ElementType = typeof(UInt64);
        }

        public void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;
            UpdateColor();
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
                    this.Invalidate();
                    return;
                }
            }
            else
            {
                if (CheckSyntax.CanParseValue(ElementType, this.Text))
                { 
                    this.ForeColor = SystemColors.ControlText;
                    this.Invalidate();
                    return;
                }
            }
            this.ForeColor = Color.Red;
            this.Invalidate();
        }

    } // End class

} // End namespace