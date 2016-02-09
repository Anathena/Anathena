using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace Anathema
{
    public partial class GUISnapshotManager : DockContent, ISnapshotManagerView
    {
        SnapshotManagerPresenter SnapshotManagerPresenter;

        public GUISnapshotManager()
        {
            InitializeComponent();

            // Initialize presenter
            SnapshotManagerPresenter = new SnapshotManagerPresenter(this, SnapshotManager.GetInstance());
        }

        public void RefreshSnapshots()
        {
            ControlThreadingHelper.InvokeControlAction(SnapshotListView, () =>
            {
                SnapshotListView.BeginUpdate();
                SnapshotListView.EndUpdate();
            });
        }

        public void UpdateSnapshotCount(Int32 SnapshotCount)
        {
            ControlThreadingHelper.InvokeControlAction(SnapshotListView, () =>
            {
                SnapshotListView.SetItemCount(SnapshotCount);
            });
        }

        private void CreateNewSnapshot()
        {
            SnapshotManagerPresenter.CreateNewSnapshot();
        }

        private void UndoSnapshot()
        {
            SnapshotManagerPresenter.UndoSnapshot();
        }

        private void RedoSnapshot()
        {
            SnapshotManagerPresenter.RedoSnapshot();
        }

        private void ClearSnapshots()
        {
            SnapshotManagerPresenter.ClearSnapshots();
        }

        #region Events

        private void SnapshotListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = SnapshotManagerPresenter.GetItemAt(E.ItemIndex);
        }

        private void NewSnapshotButton_Click(Object Sender, EventArgs E)
        {
            CreateNewSnapshot();
        }

        private void UndoSnapshotButton_Click(Object Sender, EventArgs E)
        {
            UndoSnapshot();
        }

        private void RedoSnapshotButton_Click(Object Sender, EventArgs E)
        {
            RedoSnapshot();
        }

        private void ClearSnapshotsButton_Click(Object Sender, EventArgs E)
        {
            ClearSnapshots();
        }

        #endregion
        
    } // End class

} // End namespace