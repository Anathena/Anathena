using Anathema.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void MainEventHandler(Object Sender, MainEventArgs Args);
    class MainEventArgs : EventArgs
    {
        public String ProcessTitle = String.Empty;
    }

    interface IMainView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateProcessTitle(String ProcessTitle);

        void OpenScriptEditor();
        void OpenLabelThresholder();
    }

    interface IMainModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event MainEventHandler EventUpdateProcessTitle;
        event MainEventHandler EventOpenScriptEditor;
        event MainEventHandler EventOpenLabelThresholder;

        // Functions invoked by presenter (downstream)

        void RequestCollectValues();
        void RequestNewScan();
        void RequestUndoScan();
    }

    class MainPresenter : Presenter<IMainView, IMainModel>
    {
        public MainPresenter(IMainView View, IMainModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventUpdateProcessTitle += UpdateProcessTitle;
            Model.EventOpenScriptEditor += OpenScriptEditor;
            Model.EventOpenLabelThresholder += OpenLabelThresholder;
        }

        #region Method definitions called by the view (downstream)

        public void RequestCollectValues()
        {
            Model.RequestCollectValues();
        }

        public void RequestNewScan()
        {
            Model.RequestNewScan();
        }

        public void RequestUndoScan()
        {
            Model.RequestUndoScan();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void UpdateProcessTitle(Object Sender, MainEventArgs E)
        {
            View.UpdateProcessTitle(E.ProcessTitle);
        }

        private void OpenScriptEditor(Object Sender, MainEventArgs E)
        {
            View.OpenScriptEditor();
        }

        private void OpenLabelThresholder(Object Sender, MainEventArgs E)
        {
            View.OpenLabelThresholder();
        }

        #endregion
    }
}
