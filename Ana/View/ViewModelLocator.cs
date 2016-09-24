namespace Ana.View
{
    using Source.ChangeCounter;
    using Source.CheatBrowser;
    using Source.DotNetExplorer;
    using Source.LabelThresholder;
    using Source.Main;
    using Source.ManualScanner;
    using Source.ProcessSelector;
    using Source.Project;
    using Source.PropertyViewer;
    using Source.ScanResults;
    using Source.Snapshots;
    using Source.UserSettings;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    internal class ViewModelLocator
    {
        /// <summary>
        /// Main view model
        /// </summary>
        private static MainViewModel mainViewModel;

        /// <summary>
        /// Process Selector view model
        /// </summary>
        private static ProcessSelectorViewModel processSelectorViewModel;

        /// <summary>
        /// Change Counter view model
        /// </summary>
        private static ChangeCounterViewModel changeCounterViewModel;

        /// <summary>
        /// Label Thresholder view model
        /// </summary>
        private static LabelThresholderViewModel labelThresholderViewModel;

        /// <summary>
        /// Manual Scanner view model
        /// </summary>
        private static ManualScannerViewModel manualScannerViewModel;

        /// <summary>
        /// Snapshot Manager view model
        /// </summary>
        private static SnapshotManagerViewModel snapshotManagerViewModel;

        /// <summary>
        /// Scan Results view model
        /// </summary>
        private static ScanResultsViewModel scanResultsViewModel;

        /// <summary>
        /// Cheat Browser view model
        /// </summary>
        private static CheatBrowserViewModel cheatBrowserViewModel;

        /// <summary>
        /// .Net Explorer view model
        /// </summary>
        private static DotNetExplorerViewModel dotNetExplorerViewModel;

        /// <summary>
        /// Property Viewer view model
        /// </summary>
        private static PropertyViewerViewModel propertyViewerViewModel;

        /// <summary>
        /// Settings view model
        /// </summary>
        private static SettingsViewModel settingsViewModel;

        /// <summary>
        /// Project Explorer view model
        /// </summary>
        private static ProjectExplorerViewModel projectExplorerViewModel;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            // TODO: Figure out how to remove these and switch to Lazy Singletons and still have WPF recognize them
            mainViewModel = MainViewModel.GetInstance();
            processSelectorViewModel = new ProcessSelectorViewModel();
            changeCounterViewModel = new ChangeCounterViewModel();
            labelThresholderViewModel = new LabelThresholderViewModel();
            manualScannerViewModel = new ManualScannerViewModel();
            snapshotManagerViewModel = new SnapshotManagerViewModel();
            scanResultsViewModel = new ScanResultsViewModel();
            cheatBrowserViewModel = new CheatBrowserViewModel();
            dotNetExplorerViewModel = new DotNetExplorerViewModel();
            propertyViewerViewModel = new PropertyViewerViewModel();
            settingsViewModel = new SettingsViewModel();
            projectExplorerViewModel = new ProjectExplorerViewModel();
        }

        /// <summary>
        /// Gets the Main view model
        /// </summary>
        public MainViewModel MainViewModel
        {
            get
            {
                if (mainViewModel == null)
                {
                    mainViewModel = MainViewModel.GetInstance();
                }

                return mainViewModel;
            }
        }

        /// <summary>
        /// Gets the Process Selector view model
        /// </summary>
        public ProcessSelectorViewModel ProcessSelectorViewModel
        {
            get
            {
                if (processSelectorViewModel == null)
                {
                    processSelectorViewModel = new ProcessSelectorViewModel();
                }

                return processSelectorViewModel;
            }
        }

        /// <summary>
        /// Gets the Change Counter view model
        /// </summary>
        public ChangeCounterViewModel ChangeCounterViewModel
        {
            get
            {
                if (changeCounterViewModel == null)
                {
                    changeCounterViewModel = new ChangeCounterViewModel();
                }

                return changeCounterViewModel;
            }
        }

        /// <summary>
        /// Gets the Label Thresholder view model
        /// </summary>
        public LabelThresholderViewModel LabelThresholderViewModel
        {
            get
            {
                if (labelThresholderViewModel == null)
                {
                    labelThresholderViewModel = new LabelThresholderViewModel();
                }

                return labelThresholderViewModel;
            }
        }

        /// <summary>
        /// Gets the Manual Scanner view model
        /// </summary>
        public ManualScannerViewModel ManualScannerViewModel
        {
            get
            {
                if (manualScannerViewModel == null)
                {
                    manualScannerViewModel = new ManualScannerViewModel();
                }

                return manualScannerViewModel;
            }
        }

        /// <summary>
        /// Gets the Snapshot Manager view model
        /// </summary>
        public SnapshotManagerViewModel SnapshotManagerViewModel
        {
            get
            {
                if (snapshotManagerViewModel == null)
                {
                    snapshotManagerViewModel = new SnapshotManagerViewModel();
                }

                return snapshotManagerViewModel;
            }
        }

        /// <summary>
        /// Gets the Scan Results view model
        /// </summary>
        public ScanResultsViewModel ScanResultsViewModel
        {
            get
            {
                if (scanResultsViewModel == null)
                {
                    scanResultsViewModel = new ScanResultsViewModel();
                }

                return scanResultsViewModel;
            }
        }

        /// <summary>
        /// Gets the Cheat Browser view model
        /// </summary>
        public CheatBrowserViewModel CheatBrowserViewModel
        {
            get
            {
                if (cheatBrowserViewModel == null)
                {
                    cheatBrowserViewModel = new CheatBrowserViewModel();
                }

                return cheatBrowserViewModel;
            }
        }

        /// <summary>
        /// Gets the .Net Explorer view model
        /// </summary>
        public DotNetExplorerViewModel DotNetExplorerViewModel
        {
            get
            {
                if (dotNetExplorerViewModel == null)
                {
                    dotNetExplorerViewModel = new DotNetExplorerViewModel();
                }

                return dotNetExplorerViewModel;
            }
        }

        /// <summary>
        /// Gets the Property Viewer view model
        /// </summary>
        public PropertyViewerViewModel PropertyViewerViewModel
        {
            get
            {
                if (propertyViewerViewModel == null)
                {
                    propertyViewerViewModel = new PropertyViewerViewModel();
                }

                return propertyViewerViewModel;
            }
        }

        /// <summary>
        /// Gets the Settings view model
        /// </summary>
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                if (settingsViewModel == null)
                {
                    settingsViewModel = new SettingsViewModel();
                }

                return settingsViewModel;
            }
        }

        /// <summary>
        /// Gets the Project Explorer view model
        /// </summary>
        public ProjectExplorerViewModel ProjectExplorerViewModel
        {
            get
            {
                if (projectExplorerViewModel == null)
                {
                    projectExplorerViewModel = new ProjectExplorerViewModel();
                }

                return projectExplorerViewModel;
            }
        }
    }
    //// End class
}
//// End namespace