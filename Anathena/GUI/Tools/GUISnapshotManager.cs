using Ana.Source.Snapshots;
using Ana.Source.Utils;
using Ana.Source.Utils.DataStructures;
using Ana.Source.Utils.MVP;
using Ana.Source.Utils.Validation;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Ana.GUI.Tools
{
    public partial class GUISnapshotManager : DockContent, ISnapshotManagerView
    {
        private SnapshotManagerPresenter snapshotManagerPresenter;
        private ListViewCache listViewCache;
        private Object accessLock;

        private const String newScanText = "NewScan";
        private const String emptyEntry = "-";

        private Int32 snapshotCount;

        public GUISnapshotManager()
        {
            InitializeComponent();

            snapshotManagerPresenter = new SnapshotManagerPresenter(this, SnapshotManager.GetInstance());
            listViewCache = new ListViewCache();
            accessLock = new Object();
        }

        public void UpdateSnapshotCount(Int32 SnapshotCount, Int32 DeletedSnapshotCount)
        {
            using (TimedLock.Lock(accessLock))
            {
                this.snapshotCount = SnapshotCount;

                ControlThreadingHelper.InvokeControlAction(SnapshotListView, () =>
                {
                    SnapshotListView.BeginUpdate();
                    SnapshotListView.SetItemCount(SnapshotCount + DeletedSnapshotCount);
                    listViewCache.FlushCache();
                    SnapshotListView.EndUpdate();
                });
            }
        }

        private void CreateNewSnapshot()
        {
            snapshotManagerPresenter.CreateNewSnapshot();
        }

        private void UndoSnapshot()
        {
            snapshotManagerPresenter.UndoSnapshot();
        }

        private void RedoSnapshot()
        {
            snapshotManagerPresenter.RedoSnapshot();
        }

        private void ClearSnapshots()
        {
            snapshotManagerPresenter.ClearSnapshots();
        }

        #region Events

        private void SnapshotListView_RetrieveVirtualItem(Object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem item = listViewCache.Get((UInt64)e.ItemIndex);

            if (item != null)
            {
                e.Item = item;
                return;
            }

            Snapshot snapshot = snapshotManagerPresenter.GetSnapshotAtIndex(e.ItemIndex);


            item = listViewCache.Add(e.ItemIndex, new String[SnapshotListView.Columns.Count]);

            item.ForeColor = (e.ItemIndex + 1 > snapshotCount) ? Color.LightGray : SystemColors.ControlText;
            item.BackColor = (e.ItemIndex + 1 == snapshotCount) ? SystemColors.Highlight : SystemColors.Control;

            if (snapshot == null)
            {
                item.SubItems[SnapshotListView.Columns.IndexOf(ScanMethodHeader)].Text = newScanText;
                item.SubItems[SnapshotListView.Columns.IndexOf(SizeHeader)].Text = emptyEntry;
                item.SubItems[SnapshotListView.Columns.IndexOf(TimeStampHeader)].Text = emptyEntry;
            }
            else
            {
                item.SubItems[SnapshotListView.Columns.IndexOf(ScanMethodHeader)].Text = snapshot.GetScanMethod();
                item.SubItems[SnapshotListView.Columns.IndexOf(SizeHeader)].Text = Conversions.BytesToMetric(snapshot.GetMemorySize());
                item.SubItems[SnapshotListView.Columns.IndexOf(TimeStampHeader)].Text = snapshot.GetTimeStamp().ToLongTimeString();
            }

            e.Item = item;
        }

        private void NewSnapshotButton_Click(Object sender, EventArgs e)
        {
            CreateNewSnapshot();
        }

        private void UndoSnapshotButton_Click(Object sender, EventArgs e)
        {
            UndoSnapshot();
        }

        private void RedoSnapshotButton_Click(Object sender, EventArgs e)
        {
            RedoSnapshot();
        }

        private void ClearSnapshotsButton_Click(Object sender, EventArgs e)
        {
            ClearSnapshots();
        }

        #endregion

    } // End class

} // End namespace