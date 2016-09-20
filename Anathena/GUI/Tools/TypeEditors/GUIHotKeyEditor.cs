using Ana.Source.Project;
using Ana.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ana.GUI.Tools.TypeEditors
{
    partial class GUIHotKeyEditor : Form, IHotKeyEditorView
    {
        private HotKeyEditorPresenter hotKeyEditorPresenter;

        public GUIHotKeyEditor(HotKeyEditorPresenter hotKeyEditorPresenter)
        {
            InitializeComponent();

            this.hotKeyEditorPresenter = hotKeyEditorPresenter;
        }

        private void UpdateListBox()
        {
            hotKeyListView.Items.Clear();
        }

        public void SetHotKeyList(IEnumerable<String> hotKeyList)
        {
            ControlThreadingHelper.InvokeControlAction(hotKeyListView, () =>
            {
                hotKeyListView.Items.Clear();

                if (hotKeyList == null)
                    return;

                foreach (String hotKeyItem in hotKeyList)
                    hotKeyListView.Items.Add(new ListViewItem(new String[] { hotKeyItem }));
            });
        }

        public void SetPendingKeys(String pendingKeys)
        {
            ControlThreadingHelper.InvokeControlAction(HotKeyTextBox, () =>
            {
                HotKeyTextBox.Text = pendingKeys;
            });
        }

        private void ClearInput()
        {
            hotKeyEditorPresenter.ClearInput();
        }

        private void AddHotKey()
        {
            hotKeyEditorPresenter.AddHotKey();
        }

        private void DeleteSelection()
        {
            if (hotKeyListView.SelectedIndices.Count <= 0)
                return;

            hotKeyEditorPresenter.DeleteHotKeys(hotKeyListView.SelectedIndices.Cast<Int32>());
        }

        #region Events

        private void GUIHotKeyEditor_FormClosed(Object sender, FormClosedEventArgs e)
        {
            hotKeyEditorPresenter.OnClose();
        }

        private void ClearHotKeyButton_Click(Object sender, EventArgs e)
        {
            ClearInput();
        }

        private void AddHotKeyButton_Click(Object sender, EventArgs e)
        {
            AddHotKey();
        }

        private void RemoveHotKeyButton_Click(Object sender, EventArgs e)
        {
            DeleteSelection();
        }

        private void CancelHotKeyButton_Click(Object sender, EventArgs e)
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