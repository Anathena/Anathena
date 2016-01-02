using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Anathema
{
    public partial class GUIAddressTableEntryEdit : Form
    {
        public GUIAddressTableEntryEdit(String Description, String ValueType, String Address, String[] Offsets)
        {
            InitializeComponent();

            InitializeValueTypeComboBox();

            // Add initial items
            DescriptionTextBox.Text = Description;
            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(ValueType);
            AddressTextBox.Text = Address;
            if (Offsets != null)
                foreach (String Offset in Offsets)
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
            if (CheckSyntax.Int32Value(OffsetTextBox.Text) || CheckSyntax.Address(OffsetTextBox.Text))
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
            if (CheckSyntax.Int32Value(OffsetTextBox.Text) || CheckSyntax.Address(OffsetListBox.Text))
                OffsetListBox.Items.Add(OffsetListBox.Text);
        }

        private void RemoveOffsetButton_Click(Object Sender, EventArgs E)
        {
            foreach (Int32 Index in OffsetListBox.SelectedIndices)
                OffsetListBox.Items.Remove(Index);
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
