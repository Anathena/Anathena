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
        event MainEventHandler EventFinishProgress;
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
        private Object AccessLock;

        public MainPresenter(IMainView View, IMainModel Model) : base(View, Model)
        {
            PendingActions = new List<ProgressItem>();
            AccessLock = new Object();

            // Bind events triggered by the model
            Model.EventUpdateProcessTitle += EventUpdateProcessTitle;
            Model.EventUpdateProgress += EventUpdateProgress;
            Model.EventFinishProgress += EventFinishProgress;
            Model.EventOpenScriptEditor += EventOpenScriptEditor;
            Model.EventOpenLabelThresholder += EventOpenLabelThresholder;
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

        private void EventUpdateProcessTitle(Object Sender, MainEventArgs E)
        {
            Task.Run(() => { View.UpdateProcessTitle(E.ProcessTitle); });
        }

        private void EventUpdateProgress(Object Sender, MainEventArgs E)
        {
            Task.Run(() =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (!PendingActions.Contains(E.ProgressItem))
                        PendingActions.Add(E.ProgressItem);

                    if (PendingActions.Count > 0)
                        View.UpdateProgress(PendingActions[0]);
                    else
                        View.UpdateProgress(null);
                }
            });

        }

        private void EventFinishProgress(Object Sender, MainEventArgs E)
        {
            Task.Run(() =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (PendingActions.Contains(E.ProgressItem))
                        PendingActions.Remove(E.ProgressItem);

                    if (PendingActions.Count > 0)
                        View.UpdateProgress(PendingActions[0]);
                    else
                        View.UpdateProgress(null);
                }
            });
        }

        private void EventOpenScriptEditor(Object Sender, MainEventArgs E)
        {
            Task.Run(() => { View.OpenScriptEditor(); });
        }

        private void EventOpenLabelThresholder(Object Sender, MainEventArgs E)
        {
            Task.Run(() => { View.OpenLabelThresholder(); });
        }

        #endregion

    } // End class

} // End namespace