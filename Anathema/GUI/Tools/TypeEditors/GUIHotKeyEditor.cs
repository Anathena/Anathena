using Anathema.Source.Engine.InputCapture;
using Anathema.Source.Project;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anathema.GUI.Tools.TypeEditors
{
    partial class GUIHotKeyEditor : Form, IHotKeyEditorView
    {
        private HotKeys _HotKeys { get; set; }
        public HotKeys HotKeys { get { return _HotKeys; } private set { _HotKeys = (value == null ? new HotKeys() : value); } }

        private HotKeyEditorPresenter HotKeyEditorPresenter;

        public GUIHotKeyEditor(HotKeyEditorPresenter HotKeyEditorPresenter)
        {
            InitializeComponent();

            this.HotKeyEditorPresenter = HotKeyEditorPresenter;
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

        public void SetHotKeyList(IEnumerable<String> HotKeyList)
        {

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