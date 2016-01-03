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
        public Stack<Snapshot> Snapshots = null;
        public Stack<Snapshot> DeletedSnapshots = null;
    }

    interface ISnapshotManagerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateSnapshotDisplay(ListViewItem[] Snapshots);
    }

    interface ISnapshotManagerModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event SnapshotManagerEventHandler UpdateSnapshotDisplay;

        // Functions invoked by presenter (downstream)
        void CreateNewSnapshot();
        void RedoSnapshot();
        void UndoSnapshot();
        void ClearSnapshots();
    }

    class SnapshotManagerPresenter : Presenter<ISnapshotManagerView, ISnapshotManagerModel>
    {
        new ISnapshotManagerView View;
        new ISnapshotManagerModel Model;

        public SnapshotManagerPresenter(ISnapshotManagerView View, ISnapshotManagerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.UpdateSnapshotDisplay += UpdateSnapshotDisplay;
        }

        #region Method definitions called by the view (downstream)

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            Model.UpdateMemoryEditor(MemoryEditor);
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

        private void UpdateSnapshotDisplay(Object Sender, SnapshotManagerEventArgs E)
        {
            List<ListViewItem> ListViewItems = new List<ListViewItem>();

            foreach (Snapshot Snapshot in E.Snapshots.Reverse())
            {
                if (Snapshot == null)
                    ListViewItems.Add(new ListViewItem(new String[] { "New Scan", "-", "-" }));
                else
                    ListViewItems.Add(new ListViewItem(new String[] { Snapshot.GetScanMethod(), Conversions.BytesToMetric(Snapshot.GetMemorySize()), Snapshot.GetTimeStamp().ToLongTimeString() }));
            }

            if (ListViewItems.Count != 0)
                ListViewItems.Last().BackColor = SystemColors.Highlight;

            foreach (Snapshot Snapshot in E.DeletedSnapshots)
            {
                ListViewItems.Add(new ListViewItem(new String[] { Snapshot.GetScanMethod(), Conversions.BytesToMetric(Snapshot.GetMemorySize()), Snapshot.GetTimeStamp().ToLongTimeString() }));
                ListViewItems.Last().ForeColor = Color.LightGray;
            }

            View.UpdateSnapshotDisplay(ListViewItems.ToArray());
        }

        #endregion
    }
}
