using Ana.Source.Project;
using Ana.Source.Utils.MVP;
using Ana.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ana.GUI.Tools.TypeEditors
{
    partial class GUIOffsetEditor : Form, IOffsetEditorView
    {
        private OffsetEditorPresenter offsetEditorPresenter;

        public GUIOffsetEditor(OffsetEditorPresenter offsetEditorPresenter)
        {
            InitializeComponent();

            this.offsetEditorPresenter = offsetEditorPresenter;
        }

        public void SetOffsets(IEnumerable<Tuple<String, String>> hexDecOffsets)
        {
            ControlThreadingHelper.InvokeControlAction(OffsetsListView, () =>
            {
                OffsetsListView.Items.Clear();

                foreach (Tuple<String, String> hexDecOffset in hexDecOffsets)
                {
                    OffsetsListView.Items.Add(new ListViewItem(new String[] { hexDecOffset.Item1, hexDecOffset.Item2 }));
                }
            });
        }

        private void AddOffset()
        {
            if (CheckSyntax.CanParseAddress(OffsetHexDecTextBox.GetValueAsHexidecimal()))
            {
                offsetEditorPresenter.AddOffset(Conversions.ParseDecStringAsValue(typeof(Int32), OffsetHexDecTextBox.GetValueAsDecimal()));
                OffsetHexDecTextBox.Text = String.Empty;
            }
        }

        private void DeleteSelection()
        {
            if (OffsetsListView.SelectedIndices.Count <= 0)
                return;

            offsetEditorPresenter.DeleteOffsets(OffsetsListView.SelectedIndices.OfType<Int32>());
        }

        #region Events

        private void AddOffsetButton_Click(Object sender, EventArgs e)
        {
            AddOffset();
        }

        private void RemoveOffsetButton_Click(Object sender, EventArgs e)
        {
            DeleteSelection();
        }

        private void CancelOffsetsButton_Click(Object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OkayButton_Click(Object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DeleteSelectionToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            DeleteSelection();
        }

        #endregion

    } // End class

} // End namespace