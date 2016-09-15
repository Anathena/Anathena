using Anathena.Source.Engine;
using Anathena.Source.Engine.AddressResolver;
using Anathena.Source.Engine.AddressResolver.DotNet;
using Anathena.Source.Engine.Processes;
using Anathena.Source.Engine.Proxy;
using Anathena.Source.Project;
using Anathena.Source.Snapshots;
using Anathena.Source.Snapshots.Prefilter;
using Anathena.Source.Utils;
using System;
using System.Threading;

namespace Anathena.Source.Controller
{
    class Main : IMainModel
    {
        // Singleton instance of Main
        private static Lazy<Main> mainInstance = new Lazy<Main>(() => { return new Main(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore engineCore;
        private String projectFilePath;

        public event MainEventHandler EventUpdateProcessTitle;
        public event MainEventHandler EventUpdateProgress;
        public event MainEventHandler EventUpdateHasChanges;
        public event MainEventHandler EventFinishProgress;
        public event MainEventHandler EventOpenScriptEditor;
        public event MainEventHandler EventOpenLabelThresholder;

        private Main()
        {
            InitializeProcessObserver();
            InitializeBackgroundTasks();
        }

        public void OnGUIOpen() { }

        public static Main GetInstance()
        {
            return mainInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore engineCore)
        {
            this.engineCore = engineCore;

            MainEventArgs MainEventArgs = new MainEventArgs();
            MainEventArgs.ProcessTitle = engineCore.Memory.GetProcess()?.ProcessName ?? String.Empty;
            EventUpdateProcessTitle?.Invoke(this, MainEventArgs);
        }

        private void InitializeBackgroundTasks()
        {
            ProxyCommunicator.GetInstance().InitializeServices();

            SnapshotPrefilterFactory.GetSnapshotPrefilter(typeof(ShallowPointerPrefilter)).BeginPrefilter();
            DotNetObjectCollector.GetInstance().Begin();
            AddressResolver.GetInstance().Begin();
        }

        public void UpdateActionProgress(ProgressItem progressItem)
        {
            MainEventArgs mainEventArgs = new MainEventArgs();
            mainEventArgs.ProgressItem = progressItem;
            EventUpdateProgress?.Invoke(this, mainEventArgs);
        }

        public void UpdateHasChanges(Boolean changed)
        {
            MainEventArgs mainEventArgs = new MainEventArgs();
            mainEventArgs.Changed = changed;
            EventUpdateHasChanges?.Invoke(this, mainEventArgs);
        }

        public void FinishActionProgress(ProgressItem progressItem)
        {
            MainEventArgs mainEventArgs = new MainEventArgs();
            mainEventArgs.ProgressItem = progressItem;
            EventFinishProgress?.Invoke(this, mainEventArgs);
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
            return projectFilePath;
        }

        public void SetProjectFilePath(String projectFilePath)
        {
            this.projectFilePath = projectFilePath;
        }

        public void RequestOpenTable(String filePath)
        {
            ProjectExplorer.GetInstance().OpenProject(filePath);
        }

        public void RequestMergeTable(String filePath)
        {
            ProjectExplorer.GetInstance().ImportProject(filePath);
        }

        public void RequestSaveTable(String filePath)
        {
            ProjectExplorer.GetInstance().SaveProject(filePath);
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