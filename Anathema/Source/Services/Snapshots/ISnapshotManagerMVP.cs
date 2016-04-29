using Anathema.Services.ProcessManager;
using Anathema.Utils.MVP;
using System;

namespace Anathema.Services.Snapshots
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

        Snapshot GetSnapshotAt(Int32 Index);
    }

    class SnapshotManagerPresenter : Presenter<ISnapshotManagerView, ISnapshotManagerModel>
    {
        protected new ISnapshotManagerView View;
        protected new ISnapshotManagerModel Model;
        
        public SnapshotManagerPresenter(ISnapshotManagerView View, ISnapshotManagerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
            
            // Bind events triggered by the model
            Model.UpdateSnapshotCount += UpdateSnapshotCount;
        }

        #region Method definitions called by the view (downstream)

        public Snapshot GetItemAt(Int32 Index)
        {
            return Model.GetSnapshotAt(Index);
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