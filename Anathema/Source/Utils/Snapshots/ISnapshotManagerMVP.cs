using Anathema.Source.SystemInternals.Processes;
using Anathema.Source.Utils.MVP;
using System;

namespace Anathema.Source.Utils.Snapshots
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
        void UpdateSnapshotCount(Int32 SnapshotCount, Int32 DeletedSnapshotCount);
    }

    interface ISnapshotManagerModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event SnapshotManagerEventHandler UpdateSnapshotCount;

        // Functions invoked by presenter (downstream)
        void CreateNewSnapshot();
        void RedoSnapshot();
        void UndoSnapshot();
        void ClearSnapshots();

        Snapshot GetSnapshotAtIndex(Int32 Index);
    }

    class SnapshotManagerPresenter : Presenter<ISnapshotManagerView, ISnapshotManagerModel>
    {
        private new ISnapshotManagerView View { get; set; }
        private new ISnapshotManagerModel Model { get; set; }

        public SnapshotManagerPresenter(ISnapshotManagerView View, ISnapshotManagerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.UpdateSnapshotCount += UpdateSnapshotCount;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public Snapshot GetSnapshotAtIndex(Int32 Index)
        {
            return Model.GetSnapshotAtIndex(Index);
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
            View.UpdateSnapshotCount(E.SnapshotCount, E.DeletedSnapshotCount);
        }

        #endregion

    } // End class

} // End namespace