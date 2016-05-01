using Anathema.Services.ProcessManager;
using Anathema.Source.Utils;
using Anathema.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void MainEventHandler(Object Sender, MainEventArgs Args);
    class MainEventArgs : EventArgs
    {
        public String ProcessTitle = String.Empty;
        public ProgressItem ProgressItem = null;
    }

    interface IMainView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateProcessTitle(String ProcessTitle);
        void UpdateProgress(ProgressItem ProgressItem);

        void OpenScriptEditor();
        void OpenLabelThresholder();
    }

    interface IMainModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event MainEventHandler EventUpdateProcessTitle;
        event MainEventHandler EventUpdateProgress;
        event MainEventHandler EventOpenScriptEditor;
        event MainEventHandler EventOpenLabelThresholder;

        // Functions invoked by presenter (downstream)

        void RequestCollectValues();
        void RequestNewScan();
        void RequestUndoScan();
    }

    class MainPresenter : Presenter<IMainView, IMainModel>
    {
        private List<ProgressItem> PendingActions;

        public MainPresenter(IMainView View, IMainModel Model) : base(View, Model)
        {
            PendingActions = new List<ProgressItem>();

            // Bind events triggered by the model
            Model.EventUpdateProcessTitle += UpdateProcessTitle;
            Model.EventUpdateProgress += UpdateProgress;
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
            Task.Run(() => { View.UpdateProcessTitle(E.ProcessTitle); });
        }

        private void UpdateProgress(Object Sender, MainEventArgs E)
        {
            Task.Run(() =>
            {
                if (E.ProgressItem.ActionComplete() && PendingActions.Contains(E.ProgressItem))
                    PendingActions.Remove(E.ProgressItem);
                else if (!E.ProgressItem.ActionComplete() && !PendingActions.Contains(E.ProgressItem))
                    PendingActions.Add(E.ProgressItem);

                if (PendingActions.Count > 0)
                    View.UpdateProgress(PendingActions[0]);
                else
                    View.UpdateProgress(null);
            });
        }

        private void OpenScriptEditor(Object Sender, MainEventArgs E)
        {
            Task.Run(() => { View.OpenScriptEditor(); });
        }

        private void OpenLabelThresholder(Object Sender, MainEventArgs E)
        {
            Task.Run(() => { View.OpenLabelThresholder(); });
        }

        #endregion

    } // End class

} // End namespace