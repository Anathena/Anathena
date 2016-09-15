using Anathena.Source.Engine.Processes;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using System;
using System.Collections.Generic;

namespace Anathena.Source.Controller
{
    delegate void MainEventHandler(Object Sender, MainEventArgs Args);
    class MainEventArgs : EventArgs
    {
        public String ProcessTitle { get; set; }
        public ProgressItem ProgressItem { get; set; }
        public Boolean Changed { get; set; }

        public MainEventArgs()
        {
            ProcessTitle = String.Empty;
            ProgressItem = null;
            Changed = false;
        }
    }

    interface IMainView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateProcessTitle(String processTitle);
        void UpdateProgress(ProgressItem progressItem);
        void UpdateHasChanges(Boolean changed);

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
        void RequestOpenTable(String filePath);
        void RequestMergeTable(String filePath);
        void RequestSaveTable(String filePath);
        void RequestCollectValues();
        void RequestNewScan();
        void RequestUndoScan();
        void SetProjectFilePath(String projectFilePath);

        Boolean RequestHasChanges();
        String GetProjectFilePath();
    }

    class MainPresenter : Presenter<IMainView, IMainModel>
    {
        private new IMainView view { get; set; }
        private new IMainModel model { get; set; }

        private List<ProgressItem> pendingActions;
        private Object accessLock;

        public MainPresenter(IMainView view, IMainModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            pendingActions = new List<ProgressItem>();
            accessLock = new Object();

            // Bind events triggered by the model
            model.EventUpdateProcessTitle += EventUpdateProcessTitle;
            model.EventUpdateProgress += EventUpdateProgress;
            model.EventUpdateHasChanges += EventUpdateHasChanges;
            model.EventFinishProgress += EventFinishProgress;
            model.EventOpenLabelThresholder += EventOpenLabelThresholder;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void RequestOpenProject(String filePath)
        {
            model.RequestOpenTable(filePath);
        }

        public void RequestImportProject(String filePath)
        {
            model.RequestMergeTable(filePath);
        }

        public void RequestSaveProject(String filePath)
        {
            model.RequestSaveTable(filePath);
        }

        public void RequestCollectValues()
        {
            model.RequestCollectValues();
        }

        public void RequestNewScan()
        {
            model.RequestNewScan();
        }

        public void RequestUndoScan()
        {
            model.RequestUndoScan();
        }

        public void SetProjectFilePath(String projectFilePath)
        {
            model.SetProjectFilePath(projectFilePath);
        }

        public Boolean RequestHasChanges()
        {
            return model.RequestHasChanges();
        }

        public String GetProjectFilePath()
        {
            return model.GetProjectFilePath();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateProcessTitle(Object sender, MainEventArgs e)
        {
            view.UpdateProcessTitle(e.ProcessTitle);
        }

        private void EventUpdateProgress(Object sender, MainEventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                if (!pendingActions.Contains(e.ProgressItem))
                    pendingActions.Add(e.ProgressItem);

                if (pendingActions.Count > 0)
                    view.UpdateProgress(pendingActions[0]);
                else
                    view.UpdateProgress(null);
            }

        }

        private void EventUpdateHasChanges(Object sender, MainEventArgs e)
        {
            view.UpdateHasChanges(e.Changed);
        }

        private void EventFinishProgress(Object sender, MainEventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                if (pendingActions.Contains(e.ProgressItem))
                    pendingActions.Remove(e.ProgressItem);

                if (pendingActions.Count > 0)
                    view.UpdateProgress(pendingActions[0]);
                else
                    view.UpdateProgress(null);
            }
        }

        private void EventOpenLabelThresholder(Object sender, MainEventArgs e)
        {
            view.OpenLabelThresholder();
        }

        #endregion

    } // End class

} // End namespace