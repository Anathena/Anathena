using Anathema.Source.Engine;
using Anathema.Source.Engine.DotNetObjectCollector;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Prefilter;
using Anathema.Source.Project;
using Anathema.Source.Utils;
using Anathema.Source.Utils.AddressResolver;
using Anathema.Source.Utils.Snapshots;
using System;
using System.Threading;

namespace Anathema.Source.Controller
{
    class Main : IMainModel
    {
        // Singleton instance of Main
        private static Lazy<Main> MainInstance = new Lazy<Main>(() => { return new Main(); }, LazyThreadSafetyMode.PublicationOnly);

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
            InitializeBackgroundTasks();
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
            MainEventArgs.ProcessTitle = EngineCore.Memory.GetProcess()?.ProcessName ?? String.Empty;
            EventUpdateProcessTitle?.Invoke(this, MainEventArgs);
        }

        private void InitializeBackgroundTasks()
        {
            SnapshotPrefilterFactory.GetSnapshotPrefilter(typeof(LinkedListSnapshotPrefilter)).BeginPrefilter();
            DotNetObjectCollector.GetInstance().Begin();
            AddressResolver.GetInstance().Begin();
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
            ProjectExplorer.GetInstance().OpenTable(FilePath);
        }

        public void RequestMergeTable(String FilePath)
        {
            ProjectExplorer.GetInstance().MergeTable(FilePath);
        }

        public void RequestSaveTable(String FilePath)
        {
            ProjectExplorer.GetInstance().SaveTable(FilePath);
        }

        public Boolean RequestHasChanges()
        {
            return ProjectExplorer.GetInstance().HasChanges();
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