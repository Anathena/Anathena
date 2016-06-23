using Anathema.Source.Engine.DotNetObjectCollector;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Setting;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Utils.DotNetExplorer
{
    delegate void DotNetExplorerEventHandler(Object Sender, DotNetExplorerEventArgs Args);
    class DotNetExplorerEventArgs : EventArgs
    {
        public List<DotNetObject> ObjectTrees;
    }

    interface IDotNetExplorerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateVirtualPages(List<DotNetObject> ObjectTrees);
    }

    abstract class IDotNetExplorerModel : RepeatedTask, IModel
    {
        public void OnGUIOpen() { }

        // Events triggered by the model (upstream)
        public event DotNetExplorerEventHandler EventUpdateVirtualPages;
        protected virtual void OnEventUpdateVirtualPages(DotNetExplorerEventArgs E)
        {
            EventUpdateVirtualPages?.Invoke(this, E);
        }

        protected override void Update()
        {
            UpdateInterval = Settings.GetInstance().GetResultReadInterval();
        }

        // Functions invoked by presenter (downstream)
        public abstract void RefreshVirtualPages();
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
            Model.EventUpdateVirtualPages += EventUpdateVirtualPages;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void RefreshVirtualPages()
        {
            Model.RefreshVirtualPages();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateVirtualPages(Object Sender, DotNetExplorerEventArgs E)
        {
            View.UpdateVirtualPages(null);
        }

        #endregion

    } // End class

} // End namespace