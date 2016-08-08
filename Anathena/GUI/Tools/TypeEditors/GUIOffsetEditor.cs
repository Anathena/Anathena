using Anathena.Source.Project;
using Anathena.Source.Utils.MVP;
using Anathena.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Anathena.GUI.Tools.TypeEditors
{
    partial class GUIOffsetEditor : Form, IOffsetEditorView
    {
        private OffsetEditorPresenter OffsetEditorPresenter;

        public GUIOffsetEditor(OffsetEditorPresenter OffsetEditorPresenter)
        {
            InitializeComponent();

            this.OffsetEditorPresenter = OffsetEditorPresenter;
        }

        public void SetOffsets(IEnumerable<Tuple<String, String>> HexDecOffsets)
        {
            ControlThreadingHelper.InvokeControlAction(OffsetsListView, () =>
            {
                OffsetsListView.Items.Clear();

                foreach (Tuple<String, String> HexDecOffset in HexDecOffsets)
                {
                    OffsetsListView.Items.Add(new ListViewItem(new String[] { HexDecOffset.Item1, HexDecOffset.Item2 }));
                }
            });
        }

        private void AddOffset()
        {
            if (CheckSyntax.CanParseAddress(OffsetHexDecTextBox.GetValueAsHexidecimal()))
            {
                OffsetEditorPresenter.AddOffset(Conversions.ParseDecStringAsValue(typeof(Int32), OffsetHexDecTextBox.GetValueAsDecimal()));
                OffsetHexDecTextBox.Text = String.Empty;
            }
        }

        private void DeleteSelection()
        {
            if (OffsetsListView.SelectedIndices.Count <= 0)
                return;

            OffsetEditorPresenter.DeleteOffsets(OffsetsListView.SelectedIndices.OfType<Int32>());
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