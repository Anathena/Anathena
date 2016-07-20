using Anathema.Source.Project.Editors.AddressEditor;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Anathema
{
    partial class GUIAddressEditor : Form, IAddressEditorView
    {
        private AdressEditorPresenter AdressEditorPresenter;
        private IEnumerable<Int32> AddressItemIndicies;
        private Int32 MainSelection;
        private Color ItemColor;

        public GUIAddressEditor(Int32 MainSelection, IEnumerable<Int32> AddressItemIndicies)
        {
            InitializeComponent();

            this.MinimumSize = new Size(this.Width, this.Height);
            this.AddressItemIndicies = AddressItemIndicies;
            this.MainSelection = MainSelection;

            this.OffsetTextBox.SetElementType(typeof(Int32));

            InitializeValueTypeComboBox();
            InitializeDefaultItems();

            DescriptionTextBox.Select();

            this.AdressEditorPresenter = new AdressEditorPresenter(this, new TableAddressEntryEditor());
        }

        private void InitializeDefaultItems()
        {
            // Collect address item that was opened and set the display properties
            /*ProjectItem ProjectItem = ProjectExplorer.GetInstance().GetProjectItemAt(AddressItemIndicies.Last());
            if (ProjectItem is AddressItem)
            {
                AddressItem AddressItem = (AddressItem)ProjectItem;
                DescriptionTextBox.Text = AddressItem.Description;
                ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(AddressItem.ElementType.Name);
                AddressTextBox.Text = AddressItem.BaseAddress;
                ValueTextBox.IsHex = AddressItem.IsValueHex;

                if (AddressItem.Offsets == null)
                    return;

                foreach (Int32 Offset in AddressItem.Offsets)
                    OffsetListBox.Items.Add(Offset < 0 ? "-" + Math.Abs(Offset).ToString("X") : Offset.ToString("X"));
            }*/
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetScannablePrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);
        }

        #region Events
        private void ColorButton_Click(Object Sender, EventArgs E)
        {
            ColorDialog ColorDialog = new ColorDialog();
            DialogResult DialogResult = ColorDialog.ShowDialog(this);

            if (DialogResult == DialogResult.OK)
                ItemColor = ColorDialog.Color;
        }

        private void AddOffsetButton_Click(Object Sender, EventArgs E)
        {
            OffsetListBox.Items.Add(OffsetTextBox.GetValueAsHexidecimal());
            OffsetTextBox.Text = String.Empty;
        }

        private void RemoveOffsetButton_Click(Object Sender, EventArgs E)
        {
            foreach (Int32 Item in OffsetListBox.SelectedIndices.Cast<Int32>().Select(x => x).Reverse())
                OffsetListBox.Items.RemoveAt(Item);
        }

        private void OkButton_Click(Object Sender, EventArgs E)
        {
            // Accept the updated changes
            AdressEditorPresenter.AcceptChanges(MainSelection, AddressItemIndicies, DescriptionTextBox.Text, AddressTextBox.GetRawValue(),
                ValueTypeComboBox.SelectedItem.ToString(), ValueTextBox.GetValueAsDecimal(), OffsetListBox.Items.OfType<String>(), ValueTextBox.IsHex, ItemColor);

            this.Close();
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            ValueTextBox.SetElementType(Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString()));
        }

        #endregion

    } // End class

} // End namespace