using Anathema.Source.Utils;
using Anathema.Source.Utils.Caches;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Snapshots;
using Anathema.Source.Utils.Validation;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema
{
    public partial class GUISnapshotManager : DockContent, ISnapshotManagerView
    {
        private SnapshotManagerPresenter SnapshotManagerPresenter;
        private ListViewCache ListViewCache;
        private Object AccessLock;

        private const String NewScanText = "NewScan";
        private const String EmptyEntry = "-";

        private Int32 SnapshotCount;

        public GUISnapshotManager()
        {
            InitializeComponent();

            SnapshotManagerPresenter = new SnapshotManagerPresenter(this, SnapshotManager.GetInstance());
            ListViewCache = new ListViewCache();
            AccessLock = new Object();
        }

        public void UpdateSnapshotCount(Int32 SnapshotCount, Int32 DeletedSnapshotCount)
        {
            using (TimedLock.Lock(AccessLock))
            {
                this.SnapshotCount = SnapshotCount;

                ControlThreadingHelper.InvokeControlAction(SnapshotListView, () =>
                {
                    SnapshotListView.BeginUpdate();
                    SnapshotListView.SetItemCount(SnapshotCount + DeletedSnapshotCount);
                    ListViewCache.FlushCache();
                    SnapshotListView.EndUpdate();
                });
            }
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

            Snapshot Snapshot = SnapshotManagerPresenter.GetSnapshotAtIndex(E.ItemIndex);


            Item = ListViewCache.Add(E.ItemIndex, new String[SnapshotListView.Columns.Count]);

            Item.ForeColor = (E.ItemIndex + 1 > SnapshotCount) ? Color.LightGray : SystemColors.ControlText;
            Item.BackColor = (E.ItemIndex + 1 == SnapshotCount) ? SystemColors.Highlight : SystemColors.Control;

            if (Snapshot == null)
            {
                Item.SubItems[SnapshotListView.Columns.IndexOf(ScanMethodHeader)].Text = NewScanText;
                Item.SubItems[SnapshotListView.Columns.IndexOf(SizeHeader)].Text = EmptyEntry;
                Item.SubItems[SnapshotListView.Columns.IndexOf(TimeStampHeader)].Text = EmptyEntry;
            }
            else
            {
                Item.SubItems[SnapshotListView.Columns.IndexOf(ScanMethodHeader)].Text = Snapshot.GetScanMethod();
                Item.SubItems[SnapshotListView.Columns.IndexOf(SizeHeader)].Text = Conversions.BytesToMetric(Snapshot.GetMemorySize());
                Item.SubItems[SnapshotListView.Columns.IndexOf(TimeStampHeader)].Text = Snapshot.GetTimeStamp().ToLongTimeString();
            }

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