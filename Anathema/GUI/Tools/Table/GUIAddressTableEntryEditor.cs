using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Anathema.Utils;
using Anathema.Utils.Validation;
using Anathema.User.UserAddressTableEntryEditor;
using Anathema.User.UserAddressTable;

namespace Anathema
{
    partial class GUIAddressTableEntryEditor : Form, ITableAddressEntryEditorView
    {
        private TableAddressEntryEditorPresenter TableAddressEntryEditorPresenter;
        private Int32[] AddressTableItemIndicies;
        private Int32 MainSelection;

        public GUIAddressTableEntryEditor(Int32 MainSelection, Int32[] AddressTableItemIndicies, AddressTable.TableColumnEnum ColumnSelection)
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
                case AddressTable.TableColumnEnum.Description:
                    DescriptionTextBox.Select();
                    break;
                case AddressTable.TableColumnEnum.ValueType:
                    ValueTypeComboBox.Select();
                    break;
                case AddressTable.TableColumnEnum.Address:
                    AddressTextBox.Select();
                    break;
                case AddressTable.TableColumnEnum.Value:
                    ValueTextBox.Select();
                    break;
            }

            this.TableAddressEntryEditorPresenter = new TableAddressEntryEditorPresenter(this, new TableAddressEntryEditor());
        }

        private void InitializeDefaultItems()
        {
            // Collect address item that was opened and set the display properties
            AddressItem AddressItem = AddressTable.GetInstance().GetAddressItemAt(AddressTableItemIndicies.Last());
            DescriptionTextBox.Text = AddressItem.Description;
            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(AddressItem.ElementType.Name);
            AddressTextBox.Text = Conversions.ToAddress(AddressItem.BaseAddress);
            ValueTextBox.IsHex = AddressItem.IsHex;

            if (AddressItem.Offsets == null)
                return;

            foreach (Int32 Offset in AddressItem.Offsets)
                OffsetListBox.Items.Add(Offset < 0 ? "-" + Math.Abs(Offset).ToString("X") : Offset.ToString("X"));
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);
        }

        #region Events

        private void AddOffsetButton_Click(Object Sender, EventArgs E)
        {
            if (CheckSyntax.CanParseAddress(OffsetTextBox.GetValueAsHexidecimal()))
            {
                OffsetListBox.Items.Add(OffsetTextBox.GetValueAsHexidecimal());
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
            // Accept the updated changes
            TableAddressEntryEditorPresenter.AcceptChanges(MainSelection, AddressTableItemIndicies, DescriptionTextBox.Text, AddressTextBox.GetValueAsHexidecimal(),
                ValueTypeComboBox.SelectedItem.ToString(), ValueTextBox.GetValueAsDecimal(), OffsetListBox.Items.OfType<String>().ToArray(), ValueTextBox.IsHex);

            this.Close();
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            ValueTextBox.SetElementType(Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString()));
        }

        #endregion

    } // End class

} // End namespace