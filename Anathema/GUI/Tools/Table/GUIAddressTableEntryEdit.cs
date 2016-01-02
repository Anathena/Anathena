using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Anathema
{
    public partial class GUIAddressTableEntryEdit : Form
    {
        public GUIAddressTableEntryEdit(Int32 AddressTableItemIndex)
        {
            InitializeComponent();

            this.MinimumSize = new Size(this.Width, this.Height);

            //String Description, String ValueType, String Address, String[] Offsets
            InitializeValueTypeComboBox();

            AddressItem AddressItem = Table.GetInstance().GetAddressItemAt(AddressTableItemIndex);

            // Add initial items
            DescriptionTextBox.Text = AddressItem.Description;
            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(AddressItem.ElementType.Name);
            AddressTextBox.Text = Conversions.ToAddress(AddressItem.Address.ToString());
            if (AddressItem.Offsets != null)
                foreach (Int32 Offset in AddressItem.Offsets)
                    OffsetListBox.Items.Add(Offset);

            UpdateOffset();
        }

        private void InitializeValueTypeComboBox()
        {
            Type[] Primitives = typeof(Int32).Assembly.GetTypes().Where(Type => Type.IsPrimitive && Type != typeof(Boolean) && Type != typeof(IntPtr) && Type != typeof(UIntPtr)).ToArray();

            foreach (Type Primitive in Primitives)
                ValueTypeComboBox.Items.Add(Primitive.Name);
        }

        private void UpdateOffset()
        {
            if (CheckSyntax.Int64Value(OffsetTextBox.Text) || CheckSyntax.Address(OffsetTextBox.Text))
            {
                OffsetTextBox.ForeColor = Color.Black;
                AddOffsetButton.Enabled = true;
            }
            else
            {
                OffsetTextBox.ForeColor = Color.Red;
                AddOffsetButton.Enabled = false;
            }
        }
        
        #region Events

        private void AddOffsetButton_Click(Object Sender, EventArgs E)
        {
            if (CheckSyntax.Int64Value(OffsetTextBox.Text) || CheckSyntax.Address(OffsetTextBox.Text))
            {
                OffsetListBox.Items.Add(OffsetTextBox.Text);
                OffsetTextBox.Text = String.Empty;
            }
        }

        private void RemoveOffsetButton_Click(Object Sender, EventArgs E)
        {
            foreach (Int32 Item in OffsetListBox.SelectedIndices.Cast<Int32>().Select(x => x).Reverse())
                OffsetListBox.Items.RemoveAt(Item);
        }

        private void AcceptButton_Click(Object Sender, EventArgs E)
        {

        }

        private void CancelButton_Click(Object Sender, EventArgs E)
        {

        }

        private void OffsetTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateOffset();
        }

        #endregion
    }
}
