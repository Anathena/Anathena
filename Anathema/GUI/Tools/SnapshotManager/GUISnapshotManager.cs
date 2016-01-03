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

        public void UpdateSnapshotDisplay(ListViewItem[] Snapshots)
        {
            ControlThreadingHelper.InvokeControlAction(SnapshotListView, () =>
            {
                SnapshotListView.BeginUpdate();
                SnapshotListView.Items.Clear();
                SnapshotListView.Items.AddRange(Snapshots);
                SnapshotListView.EndUpdate();
            });
        }

        public void CreateNewSnapshot()
        {
            SnapshotManagerPresenter.CreateNewSnapshot();
        }

        public void UndoSnapshot()
        {
            SnapshotManagerPresenter.UndoSnapshot();
        }

        public void RedoSnapshot()
        {
            SnapshotManagerPresenter.RedoSnapshot();
        }

        public void ClearSnapshots()
        {
            SnapshotManagerPresenter.ClearSnapshots();
        }

        #region Events

        private void NewSnapshotButton_Click(object sender, EventArgs e)
        {
            CreateNewSnapshot();
        }

        private void UndoSnapshotButton_Click(object sender, EventArgs e)
        {
            UndoSnapshot();
        }

        private void RedoSnapshotButton_Click(object sender, EventArgs e)
        {
            RedoSnapshot();
        }

        private void ClearSnapshotsButton_Click(object sender, EventArgs e)
        {
            ClearSnapshots();
        }

        #endregion
    }
}
