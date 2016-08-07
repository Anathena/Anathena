using Anathema.Source.Engine.InputCapture;
using Anathema.Source.Project;
using Anathema.Source.Utils.MVP;
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
            HotKeyListView.Items.Clear();
        }

        public void SetHotKeyList(IEnumerable<Tuple<String, String>> HotKeyList)
        {
            ControlThreadingHelper.InvokeControlAction(HotKeyListView, () =>
            {
                HotKeyListView.Items.Clear();

                if (HotKeyList == null)
                    return;

                foreach (Tuple<String, String> HotKeyItem in HotKeyList)
                {
                    HotKeyListView.Items.Add(new ListViewItem(new String[] { HotKeyItem.Item1, HotKeyItem.Item2 }));
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
        }

        #region Events

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