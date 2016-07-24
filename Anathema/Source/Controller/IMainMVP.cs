using Anathema.Source.Engine.Processes;
using Anathema.Source.Utils;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Controller
{
    delegate void MainEventHandler(Object Sender, MainEventArgs Args);
    class MainEventArgs : EventArgs
    {
        public String ProcessTitle = String.Empty;
        public ProgressItem ProgressItem = null;
        public Boolean Changed = false;
    }

    interface IMainView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateProcessTitle(String ProcessTitle);
        void UpdateProgress(ProgressItem ProgressItem);
        void UpdateHasChanges(Boolean Changed);

        void OpenScriptEditor();
        void OpenLabelThresholder();
    }

    interface IMainModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event MainEventHandler EventUpdateProcessTitle;
        event MainEventHandler EventUpdateProgress;
        event MainEventHandler EventUpdateHasChanges;
        event MainEventHandler EventFinishProgress;
        event MainEventHandler EventOpenScriptEditor;
        event MainEventHandler EventOpenLabelThresholder;

        // Functions invoked by presenter (downstream)
        void RequestOpenTable(String FilePath);
        void RequestMergeTable(String FilePath);
        void RequestSaveTable(String FilePath);
        void RequestCollectValues();
        void RequestNewScan();
        void RequestUndoScan();
        void SetProjectFilePath(String ProjectFilePath);

        Boolean RequestHasChanges();
        String GetProjectFilePath();
    }

    class MainPresenter : Presenter<IMainView, IMainModel>
    {
        private new IMainView View { get; set; }
        private new IMainModel Model { get; set; }

        private List<ProgressItem> PendingActions;
        private Object AccessLock;

        public MainPresenter(IMainView View, IMainModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            PendingActions = new List<ProgressItem>();
            AccessLock = new Object();

            // Bind events triggered by the model
            Model.EventUpdateProcessTitle += EventUpdateProcessTitle;
            Model.EventUpdateProgress += EventUpdateProgress;
            Model.EventUpdateHasChanges += EventUpdateHasChanges;
            Model.EventFinishProgress += EventFinishProgress;
            Model.EventOpenScriptEditor += EventOpenScriptEditor;
            Model.EventOpenLabelThresholder += EventOpenLabelThresholder;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void RequestOpenProject(String FilePath)
        {
            Model.RequestOpenTable(FilePath);
        }

        public void RequestImportProject(String FilePath)
        {
            Model.RequestMergeTable(FilePath);
        }

        public void RequestSaveProject(String FilePath)
        {
            Model.RequestSaveTable(FilePath);
        }

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

        public void SetProjectFilePath(String ProjectFilePath)
        {
            Model.SetProjectFilePath(ProjectFilePath);
        }

        public Boolean RequestHasChanges()
        {
            return Model.RequestHasChanges();
        }

        public String GetProjectFilePath()
        {
            return Model.GetProjectFilePath();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateProcessTitle(Object Sender, MainEventArgs E)
        {
            View.UpdateProcessTitle(E.ProcessTitle);
        }

        private void EventUpdateProgress(Object Sender, MainEventArgs E)
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

        }

        private void EventUpdateHasChanges(Object Sender, MainEventArgs E)
        {
            View.UpdateHasChanges(E.Changed);
        }

        private void EventFinishProgress(Object Sender, MainEventArgs E)
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
        }

        private void EventOpenScriptEditor(Object Sender, MainEventArgs E)
        {
            View.OpenScriptEditor();
        }

        private void EventOpenLabelThresholder(Object Sender, MainEventArgs E)
        {
            View.OpenLabelThresholder();
        }

        #endregion

    } // End class

} // End namespace