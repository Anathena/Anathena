using Anathena.Source.Project;
using Anathena.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Anathena.GUI.Tools.TypeEditors
{
    partial class GUIHotKeyEditor : Form, IHotKeyEditorView
    {
        private HotKeyEditorPresenter HotKeyEditorPresenter;

        public GUIHotKeyEditor(HotKeyEditorPresenter HotKeyEditorPresenter)
        {
            InitializeComponent();

            this.HotKeyEditorPresenter = HotKeyEditorPresenter;
        }

        private void UpdateListBox()
        {
            HotKeyListView.Items.Clear();
        }

        public void SetHotKeyList(IEnumerable<String> HotKeyList)
        {
            ControlThreadingHelper.InvokeControlAction(HotKeyListView, () =>
            {
                HotKeyListView.Items.Clear();

                if (HotKeyList == null)
                    return;

                foreach (String HotKeyItem in HotKeyList)
                {
                    HotKeyListView.Items.Add(new ListViewItem(new String[] { HotKeyItem }));
                }
            });
        }

        public void SetPendingKeys(String PendingKeys)
        {
            ControlThreadingHelper.InvokeControlAction(HotKeyTextBox, () =>
            {
                HotKeyTextBox.Text = PendingKeys;
            });
        }

        private void ClearInput()
        {
            HotKeyEditorPresenter.ClearInput();
        }

        private void AddHotKey()
        {
            HotKeyEditorPresenter.AddHotKey();
        }

        private void DeleteSelection()
        {
            if (HotKeyListView.SelectedIndices.Count <= 0)
                return;

            HotKeyEditorPresenter.DeleteHotKeys(HotKeyListView.SelectedIndices.Cast<Int32>());
        }

        #region Events

        private void GUIHotKeyEditor_FormClosed(Object Sender, FormClosedEventArgs E)
        {
            HotKeyEditorPresenter.OnClose();
        }

        private void ClearHotKeyButton_Click(Object Sender, EventArgs E)
        {
            ClearInput();
        }

        private void AddHotKeyButton_Click(Object Sender, EventArgs E)
        {
            AddHotKey();
        }

        private void RemoveHotKeyButton_Click(Object Sender, EventArgs E)
        {
            DeleteSelection();
        }

        private void CancelHotKeyButton_Click(Object Sender, EventArgs E)
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