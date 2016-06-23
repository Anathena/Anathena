using Anathema.Source.Engine.DotNetObjectCollector;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Utils.DotNetExplorer
{
    delegate void DotNetExplorerEventHandler(Object Sender, DotNetExplorerEventArgs Args);
    class DotNetExplorerEventArgs : EventArgs
    {
        public List<DotNetObject> ObjectTrees = null;
    }

    interface IDotNetExplorerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateObjectTrees(List<DotNetObject> ObjectTrees);
    }

    interface IDotNetExplorerModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event DotNetExplorerEventHandler EventRefreshObjectTrees;

        // Functions invoked by presenter (downstream)
        void RefreshObjectTrees();
    }

    class DotNetExplorerPresenter : Presenter<IDotNetExplorerView, IDotNetExplorerModel>
    {
        private new IDotNetExplorerView View { get; set; }
        private new IDotNetExplorerModel Model { get; set; }

        public DotNetExplorerPresenter(IDotNetExplorerView View, IDotNetExplorerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventRefreshObjectTrees += EventRefreshObjectTrees;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void RefreshObjectTrees()
        {
            Model.RefreshObjectTrees();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventRefreshObjectTrees(Object Sender, DotNetExplorerEventArgs E)
        {
            if (E == null || E.ObjectTrees == null)
                return;

            View.UpdateObjectTrees(E.ObjectTrees);
        }

        #endregion

    } // End class

} // End namespace