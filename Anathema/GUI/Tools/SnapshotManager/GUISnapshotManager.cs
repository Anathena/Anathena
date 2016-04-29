using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Anathema.Utils.MVP;
using Anathema.Services.Snapshots;
using Anathema.Utils;
using System.Drawing;
using Anathema.Utils.Validation;

namespace Anathema
{
    public partial class GUISnapshotManager : DockContent, ISnapshotManagerView
    {
        private SnapshotManagerPresenter SnapshotManagerPresenter;
        private ListViewCache ListViewCache;

        private Int32 SnapshotCount;

        // TODO: Kill these and use some smart indexof() stuff
        private Int32 ScanTypeIndex = 0;
        private Int32 MemorySizeIndex = 1;
        private Int32 TimeStampIndex = 2;

        public GUISnapshotManager()
        {
            InitializeComponent();

            SnapshotManagerPresenter = new SnapshotManagerPresenter(this, SnapshotManager.GetInstance());
            ListViewCache = new ListViewCache();
        }

        public void UpdateSnapshotCount(Int32 SnapshotCount, Int32 DeletedSnapshotCount)
        {
            this.SnapshotCount = SnapshotCount;

            ListViewCache.FlushCache();

            ControlThreadingHelper.InvokeControlAction(SnapshotListView, () =>
            {
                SnapshotListView.SetItemCount(SnapshotCount + DeletedSnapshotCount);
                SnapshotListView.BeginUpdate();
                SnapshotListView.EndUpdate();
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
            ListViewItem Item = ListViewCache.Get((UInt64)E.ItemIndex);
            
            if (Item != null)
            {
                E.Item = Item;
                return;
            }

            Snapshot Snapshot = SnapshotManagerPresenter.GetItemAt(E.ItemIndex);

            Item = ListViewCache.Add(E.ItemIndex, new String[] { String.Empty, String.Empty, String.Empty });

            Item.ForeColor = (E.ItemIndex + 1 > SnapshotCount) ? Color.LightGray : SystemColors.ControlText;
            Item.BackColor = (E.ItemIndex + 1 == SnapshotCount) ? SystemColors.Highlight : SystemColors.Control;

            Item.SubItems[ScanTypeIndex].Text = Snapshot == null ? "New Scan" : Snapshot.GetScanMethod();
            Item.SubItems[MemorySizeIndex].Text = Snapshot == null ? "-" : Conversions.BytesToMetric(Snapshot.GetMemorySize());
            Item.SubItems[TimeStampIndex].Text = Snapshot == null ? "-" : Snapshot.GetTimeStamp().ToLongTimeString();

            E.Item = Item;
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