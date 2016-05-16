using Anathema.Utils.MVP;
using System;

namespace Anathema.User.UserTable
{
    delegate void TableEventHandler(Object Sender, TableEventArgs Args);
    class TableEventArgs : EventArgs
    {
        public Boolean HasChanges;
    }

    interface ITableView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateHasChanges(Boolean HasChanges);
    }

    interface ITableModel : IModel
    {
        // Events triggered by the model (upstream)
        event TableEventHandler EventHasChanges;

        // Functions invoked by presenter (downstream)
        Boolean SaveTable(String Path);
        Boolean OpenTable(String Path);
        Boolean MergeTable(String Path);

        Boolean HasChanges();
    }

    class TablePresenter : Presenter<ITableView, ITableModel>
    {
        protected new ITableView View { get; set; }
        protected new ITableModel Model { get; set; }

        public TablePresenter(ITableView View, ITableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            Model.EventHasChanges += EventHasChanges;
        }

        #region Method definitions called by the view (downstream)

        public Boolean SaveTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.SaveTable(Path);
        }

        public Boolean OpenTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.OpenTable(Path);
        }

        public Boolean MergeTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.MergeTable(Path);
        }

        public Boolean HasChanges()
        {
            return Model.HasChanges();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventHasChanges(Object Sender, TableEventArgs E)
        {
            View.UpdateHasChanges(E.HasChanges);
        }

        #endregion

    } // End class

} // End namespace