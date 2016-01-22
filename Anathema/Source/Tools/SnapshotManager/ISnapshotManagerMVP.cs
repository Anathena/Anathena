using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    delegate void SnapshotManagerEventHandler(Object Sender, SnapshotManagerEventArgs Args);
    class SnapshotManagerEventArgs : EventArgs
    {
        public Int32 DeletedSnapshotCount = 0;
        public Int32 SnapshotCount = 0;
    }

    interface ISnapshotManagerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateSnapshotCount(Int32 SnapshotCount);
        void RefreshSnapshots();
    }

    interface ISnapshotManagerModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event SnapshotManagerEventHandler UpdateSnapshotCount;
        event SnapshotManagerEventHandler RefreshSnapshots;
        event SnapshotManagerEventHandler FlushCache;

        // Functions invoked by presenter (downstream)
        void CreateNewSnapshot();
        void RedoSnapshot();
        void UndoSnapshot();
        void ClearSnapshots();

        Snapshot GetSnapshotAt(Int32 Index);
    }

    class SnapshotManagerPresenter : Presenter<ISnapshotManagerView, ISnapshotManagerModel>
    {
        protected new ISnapshotManagerView View;
        protected new ISnapshotManagerModel Model;

        private ListViewCache ListViewCache;
        private Int32 DeletedSnapshotCount;
        private Int32 SnapshotCount;

        private Int32 ScanTypeIndex = 0;
        private Int32 MemorySizeIndex = 1;
        private Int32 TimeStampIndex = 2;

        public SnapshotManagerPresenter(ISnapshotManagerView View, ISnapshotManagerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            ListViewCache = new ListViewCache();

            // Bind events triggered by the model
            Model.UpdateSnapshotCount += UpdateSnapshotCount;
            Model.RefreshSnapshots += RefreshSnapshots;
            Model.FlushCache += FlushCache;
        }

        #region Method definitions called by the view (downstream)

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            Model.UpdateMemoryEditor(MemoryEditor);
        }

        public ListViewItem GetItemAt(Int32 Index)
        {
            ListViewItem Item = ListViewCache.Get(Index);
            Snapshot Snapshot = Model.GetSnapshotAt(Index);

            // Try to update and return the item if it is a valid item
            if (Item != null)
            {
                Item.ForeColor = Index > (SnapshotCount - DeletedSnapshotCount) ? Color.LightGray : SystemColors.ControlText;
                Item.BackColor = Index == (SnapshotCount - 1) ? SystemColors.Highlight : SystemColors.Control;
                return Item;
            }

            // Add the properties to the manager and get the list view item back
            Item = ListViewCache.Add(Index, new String[] { String.Empty, String.Empty, String.Empty });

            Item.SubItems[ScanTypeIndex].Text = Snapshot == null ? "New Scan" : Snapshot.GetScanMethod();
            Item.SubItems[MemorySizeIndex].Text = Snapshot == null ? "-" : Conversions.BytesToMetric(Snapshot.GetMemorySize());
            Item.SubItems[TimeStampIndex].Text = Snapshot == null ? "-" : Snapshot.GetTimeStamp().ToLongTimeString();

            Item.ForeColor = Index > (SnapshotCount - DeletedSnapshotCount) ? Color.LightGray : SystemColors.ControlText;
            Item.BackColor = Index == (SnapshotCount - 1) ? SystemColors.Highlight : SystemColors.Control;
            return Item;
        }

        public void CreateNewSnapshot()
        {
            Model.CreateNewSnapshot();
        }

        public void RedoSnapshot()
        {
            Model.RedoSnapshot();
        }

        public void UndoSnapshot()
        {
            Model.UndoSnapshot();
        }

        public void ClearSnapshots()
        {
            Model.ClearSnapshots();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void UpdateSnapshotCount(Object Sender, SnapshotManagerEventArgs E)
        {
            this.SnapshotCount = E.SnapshotCount;
            this.DeletedSnapshotCount = E.DeletedSnapshotCount;
            View.UpdateSnapshotCount(E.SnapshotCount + E.DeletedSnapshotCount);
        }

        private void RefreshSnapshots(Object Sender, SnapshotManagerEventArgs E)
        {
            View.RefreshSnapshots();
        }

        private void FlushCache(Object Sender, SnapshotManagerEventArgs E)
        {
            ListViewCache.FlushCache();
            View.RefreshSnapshots();
        }

        #endregion
    }
}
