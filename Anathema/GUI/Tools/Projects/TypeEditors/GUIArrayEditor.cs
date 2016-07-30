using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Anathema.GUI.Tools.Projects.TypeEditors
{
    public partial class GUIArrayEditor : Form
    {
        private List<Int32> _Offsets { get; set; }
        public List<Int32> Offsets { get { return _Offsets; } private set { _Offsets = (value == null ? new List<Int32>() : value); } }

        public GUIArrayEditor(List<Int32> Offsets)
        {
            InitializeComponent();

            // Clone input offsets rather than grabbing a reference
            this.Offsets = Offsets?.Select(X => X).ToList();

            UpdateListBox();
        }

        private void UpdateListBox()
        {
            OffsetsListView.Items.Clear();

            foreach (Int32 Offset in Offsets)
            {
                String Decimal = Offset.ToString();
                String Hexadecimal = Offset < 0 ? "-" + Math.Abs(Offset).ToString("X") : Offset.ToString("X");
                OffsetsListView.Items.Add(new ListViewItem(new String[] { Hexadecimal, Decimal }));
            }
        }

        private void AddOffset()
        {
            if (CheckSyntax.CanParseAddress(OffsetHexDecTextBox.GetValueAsHexidecimal()))
            {
                Offsets.Add(Conversions.ParseValue(typeof(Int32), OffsetHexDecTextBox.GetValueAsDecimal()));
                OffsetHexDecTextBox.Text = String.Empty;
            }

            UpdateListBox();
        }

        private void DeleteSelection()
        {
            if (OffsetsListView.SelectedIndices.Count <= 0)
                return;

            foreach (Int32 Item in OffsetsListView.SelectedIndices.OfType<Int32>().Reverse())
                Offsets.RemoveAt(Item);

            UpdateListBox();
        }

        #region Events

        private void AddOffsetButton_Click(Object Sender, EventArgs E)
        {
            AddOffset();
        }

        private void RemoveOffsetButton_Click(Object Sender, EventArgs E)
        {
            DeleteSelection();
        }

        private void CancelButton_Click(Object Sender, EventArgs E)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OkayButton_Click(Object Sender, EventArgs E)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DeleteSelectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            DeleteSelection();
        }

        #endregion

    } // End class

} // End namespace