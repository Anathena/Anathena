using Anathena.Source.Engine.Processes;
using Anathena.Source.Utils.MVP;
using System;

namespace Anathena.Source.Snapshots
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
        private new ISnapshotManagerView view { get; set; }
        private new ISnapshotManagerModel model { get; set; }

        public SnapshotManagerPresenter(ISnapshotManagerView view, ISnapshotManagerModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.UpdateSnapshotCount += UpdateSnapshotCount;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public Snapshot GetSnapshotAtIndex(Int32 Index)
        {
            return model.GetSnapshotAtIndex(Index);
        }

        public void CreateNewSnapshot()
        {
            model.CreateNewSnapshot();
        }

        public void RedoSnapshot()
        {
            model.RedoSnapshot();
        }

        public void UndoSnapshot()
        {
            model.UndoSnapshot();
        }

        public void ClearSnapshots()
        {
            model.ClearSnapshots();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void UpdateSnapshotCount(Object Sender, SnapshotManagerEventArgs E)
        {
            view.UpdateSnapshotCount(E.SnapshotCount, E.DeletedSnapshotCount);
        }

        #endregion

    } // End class

} // End namespace