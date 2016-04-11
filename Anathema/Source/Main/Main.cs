using Anathema.Services.Divine;
using Anathema.Services.Snapshots;
using Anathema.Utils.OS;

namespace Anathema
{
    class Main : IMainModel
    {
        private OSInterface OSInterface;
        private static Main MainInstance;

        public event MainEventHandler EventUpdateProcessTitle;
        public event MainEventHandler EventOpenScriptEditor;
        public event MainEventHandler EventOpenLabelThresholder;

        private Main()
        {
            InitializeProcessObserver();

            // TEMP DEBUG
            SnapshotPrefilter.GetInstance();
        }

        public static Main GetInstance()
        {
            if (MainInstance == null)
                MainInstance = new Main();
            return MainInstance;
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
            EventUpdateProcessTitle(this, MainEventArgs);
        }

        public void OpenScriptEditor()
        {
            EventOpenScriptEditor(this, new MainEventArgs());
        }

        public void OpenLabelThresholder()
        {
            EventOpenLabelThresholder(this, new MainEventArgs());
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