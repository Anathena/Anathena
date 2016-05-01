using Anathema.Services.Snapshots;
using Anathema.Source.Utils;
using Anathema.Utils.OS;
using System;

namespace Anathema
{
    class Main : IMainModel
    {
        private static Lazy<Main> MainInstance = new Lazy<Main>(() => { return new Main(); });
        private OSInterface OSInterface;

        public event MainEventHandler EventUpdateProcessTitle;
        public event MainEventHandler EventUpdateProgress;
        public event MainEventHandler EventOpenScriptEditor;
        public event MainEventHandler EventOpenLabelThresholder;

        private Main()
        {
            InitializeProcessObserver();

            SnapshotPrefilter.GetInstance().Begin();
        }

        public static Main GetInstance()
        {
            return MainInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;

            MainEventArgs MainEventArgs = new MainEventArgs();
            MainEventArgs.ProcessTitle = OSInterface.Process.GetProcessName();
            EventUpdateProcessTitle?.Invoke(this, MainEventArgs);
        }

        public void UpdateActionProgress(ProgressItem ProgressItem)
        {
            MainEventArgs MainEventArgs = new MainEventArgs();
            MainEventArgs.ProgressItem = ProgressItem;
            EventUpdateProgress?.Invoke(this, MainEventArgs);
        }

        public void OpenScriptEditor()
        {
            EventOpenScriptEditor?.Invoke(this, new MainEventArgs());
        }

        public void OpenLabelThresholder()
        {
            EventOpenLabelThresholder?.Invoke(this, new MainEventArgs());
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