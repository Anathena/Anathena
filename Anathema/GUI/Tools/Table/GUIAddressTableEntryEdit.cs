using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Anathema
{
    partial class GUIAddressTableEntryEdit : Form, ITableAddressEntryEditorView
    {
        private TableAddressEntryEditorPresenter TableAddressEntryEditorPresenter;
        private Int32[] AddressTableItemIndicies;
        private Int32 MainSelection;

        public GUIAddressTableEntryEdit(Int32 MainSelection, Int32[] AddressTableItemIndicies, Table.TableColumnEnum ColumnSelection)
        {
            InitializeComponent();

            this.MinimumSize = new Size(this.Width, this.Height);
            this.AddressTableItemIndicies = AddressTableItemIndicies;
            this.MainSelection = MainSelection;

            InitializeValueTypeComboBox();
            InitializeDefaultItems();

            ValidateCurrentOffset();

            switch(ColumnSelection)
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
            AddressTextBox.Text = Conversions.ToAddress(AddressItem.Address.ToString());

            if (AddressItem.Offsets != null)
                foreach (Int32 Offset in AddressItem.Offsets)
                    OffsetListBox.Items.Add(Offset);
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);
        }

        private void ValidateCurrentOffset()
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

        private void OkButton_Click(Object Sender, EventArgs E)
        {
            TableAddressEntryEditorPresenter.AcceptChanges(MainSelection, AddressTableItemIndicies, DescriptionTextBox.Text, AddressTextBox.Text, 
                ValueTypeComboBox.SelectedItem.ToString(), ValueTextBox.Text, OffsetListBox.Items.OfType<String>().ToArray());

            this.Close();
        }

        private void OffsetTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateCurrentOffset();
        }

        #endregion
    }
}
