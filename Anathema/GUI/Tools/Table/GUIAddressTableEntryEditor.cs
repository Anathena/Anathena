using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Anathema
{
    partial class GUIAddressTableEntryEditor : Form, ITableAddressEntryEditorView
    {
        private TableAddressEntryEditorPresenter TableAddressEntryEditorPresenter;
        private Int32[] AddressTableItemIndicies;
        private Int32 MainSelection;

        public GUIAddressTableEntryEditor(Int32 MainSelection, Int32[] AddressTableItemIndicies, Table.TableColumnEnum ColumnSelection)
        {
            InitializeComponent();

            this.MinimumSize = new Size(this.Width, this.Height);
            this.AddressTableItemIndicies = AddressTableItemIndicies;
            this.MainSelection = MainSelection;

            this.OffsetTextBox.SetElementType(typeof(Int32));

            InitializeValueTypeComboBox();
            InitializeDefaultItems();

            switch (ColumnSelection)
            {
                case Table.TableColumnEnum.Description:
                    DescriptionTextBox.Select();
                    break;
                case Table.TableColumnEnum.ValueType:
                    ValueTypeComboBox.Select();
                    break;
                case Table.TableColumnEnum.Address:
                    AddressTextBox.Select();
                    break;
                case Table.TableColumnEnum.Value:
                    ValueTextBox.Select();
                    break;
            }

            this.TableAddressEntryEditorPresenter = new TableAddressEntryEditorPresenter(this, new TableAddressEntryEditor());
        }

        private void InitializeDefaultItems()
        {
            AddressItem AddressItem = Table.GetInstance().GetAddressItemAt(AddressTableItemIndicies.Last());
            DescriptionTextBox.Text = AddressItem.Description;
            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(AddressItem.ElementType.Name);
            AddressTextBox.Text = Conversions.ToAddress(AddressItem.Address);
            // IsHexCheckBox.Checked = AddressItem.IsHex;

            if (AddressItem.Offsets != null)
                foreach (Int32 Offset in AddressItem.Offsets)
                    OffsetListBox.Items.Add(Offset.ToString("X"));
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);
        }

        #region Events

        private void AddOffsetButton_Click(Object Sender, EventArgs E)
        {
            if (CheckSyntax.CanParseAddress(OffsetTextBox.Text))
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

        private void OkButton_Click(Object Sender, EventArgs E)
        {
            TableAddressEntryEditorPresenter.AcceptChanges(MainSelection, AddressTableItemIndicies, DescriptionTextBox.Text, AddressTextBox.Text,
                ValueTypeComboBox.SelectedItem.ToString(), ValueTextBox.Text, OffsetListBox.Items.OfType<String>().ToArray(), false);

            this.Close();
        }

        private void OffsetTextBox_TextChanged(Object Sender, EventArgs E)
        {
            return;
            if (CheckSyntax.CanParseAddress(OffsetTextBox.Text))
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

        private void ValueTextBox_TextChanged(Object Sender, EventArgs E)
        {
            if (CheckSyntax.CanParseValue(Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString()), ValueTextBox.Text))
                ValueTextBox.ForeColor = SystemColors.ControlText;
            else
                ValueTextBox.ForeColor = Color.Red;
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            ValueTextBox_TextChanged(Sender, E);

            // ValueTextBox.SetElementType(Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString()));
        }

        #endregion

    }
}
