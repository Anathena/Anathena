using Anathema.Source.Engine.InputCapture;
using System;
using System.Windows.Forms;

namespace Anathema.GUI.Tools.TypeEditors
{
    public partial class GUIHotKeyEditor : Form
    {
        private InputBinding _HotKeys { get; set; }
        public InputBinding HotKeys { get { return _HotKeys; } private set { _HotKeys = (value == null ? new InputBinding() : value); } }

        public GUIHotKeyEditor(InputBinding HotKeys)
        {
            InitializeComponent();

            // Clone input offsets rather than grabbing a reference
            this.HotKeys = HotKeys;

            UpdateListBox();
        }

        private void UpdateListBox()
        {
            OffsetsListView.Items.Clear();
        }

        private void AddOffset()
        {
            UpdateListBox();
        }

        private void DeleteSelection()
        {
            if (OffsetsListView.SelectedIndices.Count <= 0)
                return;

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