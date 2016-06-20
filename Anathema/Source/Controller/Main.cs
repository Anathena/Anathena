using Anathema.Source.Prefilter;
using Anathema.Source.Engine;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Tables;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Snapshots;
using System;

namespace Anathema.Source.Controller
{
    class Main : IMainModel
    {
        private static Lazy<Main> MainInstance = new Lazy<Main>(() => { return new Main(); });
        private EngineCore EngineCore;

        public event MainEventHandler EventUpdateProcessTitle;
        public event MainEventHandler EventUpdateProgress;
        public event MainEventHandler EventUpdateHasChanges;
        public event MainEventHandler EventFinishProgress;
        public event MainEventHandler EventOpenScriptEditor;
        public event MainEventHandler EventOpenLabelThresholder;

        private String ProjectFilePath;

        private Main()
        {
            InitializeProcessObserver();

            SnapshotPrefilterFactory.GetSnapshotPrefilter(typeof(LinkedListSnapshotPrefilter)).BeginPrefilter();
        }

        public void OnGUIOpen() { }

        public static Main GetInstance()
        {
            return MainInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;

            MainEventArgs MainEventArgs = new MainEventArgs();
            MainEventArgs.ProcessTitle = EngineCore.Memory.GetProcess().ProcessName;
            EventUpdateProcessTitle?.Invoke(this, MainEventArgs);
        }

        public void UpdateActionProgress(ProgressItem ProgressItem)
        {
            MainEventArgs MainEventArgs = new MainEventArgs();
            MainEventArgs.ProgressItem = ProgressItem;
            EventUpdateProgress?.Invoke(this, MainEventArgs);
        }

        public void UpdateHasChanges(Boolean Changed)
        {
            MainEventArgs MainEventArgs = new MainEventArgs();
            MainEventArgs.Changed = Changed;
            EventUpdateHasChanges?.Invoke(this, MainEventArgs);
        }

        public void FinishActionProgress(ProgressItem ProgressItem)
        {
            MainEventArgs MainEventArgs = new MainEventArgs();
            MainEventArgs.ProgressItem = ProgressItem;
            EventFinishProgress?.Invoke(this, MainEventArgs);
        }

        public void OpenScriptEditor()
        {
            EventOpenScriptEditor?.Invoke(this, new MainEventArgs());
        }

        public void OpenLabelThresholder()
        {
            EventOpenLabelThresholder?.Invoke(this, new MainEventArgs());
        }

        public String GetProjectFilePath()
        {
            return ProjectFilePath;
        }

        public void SetProjectFilePath(String ProjectFilePath)
        {
            this.ProjectFilePath = ProjectFilePath;
        }

        public void RequestOpenTable(String FilePath)
        {
            TableManager.GetInstance().OpenTable(FilePath);
        }

        public void RequestMergeTable(String FilePath)
        {
            TableManager.GetInstance().MergeTable(FilePath);
        }

        public void RequestSaveTable(String FilePath)
        {
            TableManager.GetInstance().SaveTable(FilePath);
        }

        public Boolean RequestHasChanges()
        {
            return TableManager.GetInstance().HasChanges();
        }

        public void RequestCollectValues()
        {
            SnapshotManager.GetInstance().CollectValues();
        }

        public void RequestNewScan()
        {
            SnapshotManager.GetInstance().CreateNewSnapshot();
        }

        public void RequestUndoScan()
        {
            SnapshotManager.GetInstance().UndoSnapshot();
        }

    } // End class

} // End namespace