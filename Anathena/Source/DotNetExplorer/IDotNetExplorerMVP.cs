using Anathena.Source.Engine.AddressResolver.DotNet;
using Anathena.Source.Engine.Processes;
using Anathena.Source.Utils.MVP;
using System;
using System.Collections.Generic;

namespace Anathena.Source.DotNetExplorer
{
    public delegate void DotNetExplorerEventHandler(Object Sender, DotNetExplorerEventArgs Args);
    public class DotNetExplorerEventArgs : EventArgs
    {
        public List<DotNetObject> objectTrees = null;
    }

    interface IDotNetExplorerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateObjectTrees(List<DotNetObject> objectTrees);
    }

    interface IDotNetExplorerModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event DotNetExplorerEventHandler EventRefreshObjectTrees;

        // Functions invoked by presenter (downstream)
        void AddToTable(DotNetObject dotNetObject);
        void UpdateSelection(IEnumerable<DotNetObject> dotNetObjects);
        void RefreshObjectTrees();
    }

    class DotNetExplorerPresenter : Presenter<IDotNetExplorerView, IDotNetExplorerModel>
    {
        private new IDotNetExplorerView view { get; set; }
        private new IDotNetExplorerModel model { get; set; }

        public DotNetExplorerPresenter(IDotNetExplorerView view, IDotNetExplorerModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventRefreshObjectTrees += EventRefreshObjectTrees;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void AddToTable(DotNetObject dotNetObject)
        {
            model.AddToTable(dotNetObject);
        }

        public void UpdateSelection(IEnumerable<DotNetObject> dotNetObjects)
        {
            model.UpdateSelection(dotNetObjects);
        }

        public void RefreshObjectTrees()
        {
            model.RefreshObjectTrees();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventRefreshObjectTrees(Object sender, DotNetExplorerEventArgs e)
        {
            if (e == null || e.objectTrees == null)
                return;

            view.UpdateObjectTrees(e.objectTrees);
        }

        #endregion

    } // End class

} // End namespace