namespace Ana.View
{
    using Source.CheatBrowser;
    using Source.Controls;
    using Source.DotNetExplorer;
    using Source.LabelThresholder;
    using Source.Main;
    using Source.ProcessSelector;
    using Source.Project;
    using Source.PropertyViewer;
    using Source.Results;
    using Source.Scanners.ChangeCounter;
    using Source.Scanners.InputCorrelator;
    using Source.Scanners.ManualScanner;
    using Source.Scanners.ValueCollector;
    using Source.Snapshots;
    using Source.UserSettings;
    using Source.Utils.HotkeyEditor;
    using Source.Utils.ScriptEditor;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    internal class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
        }

        /// <summary>
        /// Gets the Main view model
        /// </summary>
        public MainViewModel MainViewModel
        {
            get
            {
                return MainViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Process Selector view model
        /// </summary>
        public ProcessSelectorViewModel ProcessSelectorViewModel
        {
            get
            {
                return ProcessSelectorViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Change Counter view model
        /// </summary>
        public ChangeCounterViewModel ChangeCounterViewModel
        {
            get
            {
                return ChangeCounterViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Input Correlator view model
        /// </summary>
        public InputCorrelatorViewModel InputCorrelatorViewModel
        {
            get
            {
                return InputCorrelatorViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Label Thresholder view model
        /// </summary>
        public LabelThresholderViewModel LabelThresholderViewModel
        {
            get
            {
                return LabelThresholderViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Manual Scanner view model
        /// </summary>
        public ManualScannerViewModel ManualScannerViewModel
        {
            get
            {
                return ManualScannerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Snapshot Manager view model
        /// </summary>
        public SnapshotManagerViewModel SnapshotManagerViewModel
        {
            get
            {
                return SnapshotManagerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Scan Results view model
        /// </summary>
        public ScanResultsViewModel ScanResultsViewModel
        {
            get
            {
                return ScanResultsViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Cheat Browser view model
        /// </summary>
        public CheatBrowserViewModel CheatBrowserViewModel
        {
            get
            {
                return CheatBrowserViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the .Net Explorer view model
        /// </summary>
        public DotNetExplorerViewModel DotNetExplorerViewModel
        {
            get
            {
                return DotNetExplorerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Property Viewer view model
        /// </summary>
        public PropertyViewerViewModel PropertyViewerViewModel
        {
            get
            {
                return PropertyViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Settings view model
        /// </summary>
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return SettingsViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Project Explorer view model
        /// </summary>
        public ProjectExplorerViewModel ProjectExplorerViewModel
        {
            get
            {
                return ProjectExplorerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Project Explorer view model
        /// </summary>
        public ValueCollectorViewModel ValueCollectorViewModel
        {
            get
            {
                return ValueCollectorViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Script Editor view model
        /// </summary>
        public ScriptEditorViewModel ScriptEditorViewModel
        {
            get
            {
                return new ScriptEditorViewModel();
            }
        }

        /// <summary>
        /// Gets a Offset Editor view model. Note: Not a singleton, will create a new object
        /// </summary>
        public OffsetEditorViewModel OffsetEditorViewModel
        {
            get
            {
                return new OffsetEditorViewModel();
            }
        }

        /// <summary>
        /// Gets a Hotkey Editor view model. Note: Not a singleton, will create a new object
        /// </summary>
        public HotkeyEditorViewModel HotkeyEditorViewModel
        {
            get
            {
                return new HotkeyEditorViewModel();
            }
        }

        /// <summary>
        /// Gets a Hex Dec Box view model. Note: Not a singleton, will create a new object
        /// </summary>
        public HexDecBoxViewModel HexDecBoxViewModel
        {
            get
            {
                return new HexDecBoxViewModel();
            }
        }
    }
    //// End class
}
//// End namespace