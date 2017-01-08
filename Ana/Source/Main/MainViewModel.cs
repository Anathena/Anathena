namespace Ana.Source.Main
{
    using Docking;
    using Engine.AddressResolver;
    using Engine.AddressResolver.DotNet;
    using Mvvm;
    using Mvvm.Command;
    using Output;
    using Project;
    using Scanners.BackgroundScans.Prefilters;
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using Xceed.Wpf.AvalonDock;
    using Xceed.Wpf.AvalonDock.Layout.Serialization;

    /// <summary>
    /// Main view model
    /// Note: There are several MVVM responsability violations in this class, but these are isolated and acceptable.
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Default layout file for browsing cheats.
        /// </summary>
        private const String StandardLayoutResource = "Ana.Content.StandardLayout.xml";

        /// <summary>
        /// Default layout file for browsing cheats.
        /// </summary>
        private const String DeveloperLayoutResource = "Ana.Content.DeveloperLayout.xml";

        /// <summary>
        /// The save file for the docking layout.
        /// </summary>
        private const String LayoutSaveFile = "layout.xml";

        /// <summary>
        /// Singleton instance of the <see cref="MainViewModel" /> class
        /// </summary>
        private static Lazy<MainViewModel> mainViewModelInstance = new Lazy<MainViewModel>(
                () => { return new MainViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Collection of tools contained in the main docking panel.
        /// </summary>
        private HashSet<ToolViewModel> tools;

        /// <summary>
        /// Prevents a default instance of the <see cref="MainViewModel" /> class from being created.
        /// </summary>
        private MainViewModel()
        {
            OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Info, "Anathena Started");

            this.tools = new HashSet<ToolViewModel>();

            // Note: These cannot be async, as the logic to update the layout or window cannot be on a new thread
            this.CloseCommand = new RelayCommand<Window>((window) => this.Close(window), (window) => true);
            this.MaximizeRestoreCommand = new RelayCommand<Window>((window) => this.MaximizeRestore(window), (window) => true);
            this.MinimizeCommand = new RelayCommand<Window>((window) => this.Minimize(window), (window) => true);
            this.ResetLayoutStandardCommand = new RelayCommand<DockingManager>((dockingManager) => this.ResetLayoutStandard(dockingManager), (dockingManager) => true);
            this.ResetLayoutDeveloperCommand = new RelayCommand<DockingManager>((dockingManager) => this.ResetLayoutDeveloper(dockingManager), (dockingManager) => true);
            this.LoadLayoutCommand = new RelayCommand<DockingManager>((dockingManager) => this.LoadLayout(dockingManager), (dockingManager) => true);
            this.DisplayChangeLogCommand = new RelayCommand(() => this.DisplayChangeLog(), () => true);
            this.SaveLayoutCommand = new RelayCommand<DockingManager>((dockingManager) => this.SaveLayout(dockingManager), (dockingManager) => true);
            this.StartBackgroundServices();
        }

        /// <summary>
        /// Gets the command to close the main window.
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        /// <summary>
        /// Gets the command to maximize the main window.
        /// </summary>
        public ICommand MaximizeRestoreCommand { get; private set; }

        /// <summary>
        /// Gets the command to minimize the main window.
        /// </summary>
        public ICommand MinimizeCommand { get; private set; }

        /// <summary>
        /// Gets the command to reset the current docking layout to the default.
        /// </summary>
        public ICommand ResetLayoutStandardCommand { get; private set; }

        /// <summary>
        /// Gets the command to reset the current docking layout to the default.
        /// </summary>
        public ICommand ResetLayoutDeveloperCommand { get; private set; }

        /// <summary>
        /// Gets the command to open the change log.
        /// </summary>
        public ICommand DisplayChangeLogCommand { get; private set; }

        /// <summary>
        /// Gets the command to open the current docking layout.
        /// </summary>
        public ICommand LoadLayoutCommand { get; private set; }

        /// <summary>
        /// Gets the command to save the current docking layout.
        /// </summary>
        public ICommand SaveLayoutCommand { get; private set; }

        /// <summary>
        /// Gets the tools contained in the main docking panel.
        /// </summary>
        public IEnumerable<ToolViewModel> Tools
        {
            get
            {
                if (this.tools == null)
                {
                    this.tools = new HashSet<ToolViewModel>();
                }

                return this.tools;
            }
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <returns>The singleton instance of the <see cref="MainViewModel" /> class.</returns>
        public static MainViewModel GetInstance()
        {
            return mainViewModelInstance.Value;
        }

        /// <summary>
        /// Adds a tool to the list of tools controlled by the main view model.
        /// </summary>
        /// <param name="toolViewModel">The tool to be added.</param>
        public void Subscribe(ToolViewModel toolViewModel)
        {
            if (toolViewModel != null && !this.tools.Contains(toolViewModel))
            {
                this.tools?.Add(toolViewModel);
            }

            this.RaisePropertyChanged(nameof(this.Tools));
        }

        /// <summary>
        /// Removes a tool from the list of tools controlled by the main view model.
        /// </summary>
        /// <param name="toolViewModel">The tool to be added.</param>
        public void Unsubscribe(ToolViewModel toolViewModel)
        {
            if (toolViewModel != null && this.tools.Contains(toolViewModel))
            {
                this.tools?.Remove(toolViewModel);
            }

            this.RaisePropertyChanged(nameof(this.Tools));
        }

        /// <summary>
        /// Starts useful services that run in the background to assist in various operations.
        /// </summary>
        private void StartBackgroundServices()
        {
            SnapshotPrefilterFactory.GetSnapshotPrefilter(typeof(ChunkLinkedListPrefilter)).BeginPrefilter();
            //// PointerCollector.GetInstance().Begin();
            DotNetObjectCollector.GetInstance().Begin();
            AddressResolver.GetInstance().Begin();

            OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Info, "Background Services Started");
        }

        /// <summary>
        /// Closes the main window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        private void Close(Window window)
        {
            if (!ProjectExplorerViewModel.GetInstance().PromptSave())
            {
                return;
            }

            window.Close();
        }

        /// <summary>
        /// Maximizes or Restores the main window.
        /// </summary>
        /// <param name="window">The window to maximize or restore.</param>
        private void MaximizeRestore(Window window)
        {
            if (window == null)
            {
                return;
            }

            if (window.WindowState != WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }
        }

        /// <summary>
        /// Minimizes the main window.
        /// </summary>
        /// <param name="window">The window to minimize.</param>
        private void Minimize(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Displays the change log to the user if there has been a recent update
        /// </summary>
        private void DisplayChangeLog()
        {
            try
            {
                if (!ApplicationDeployment.IsNetworkDeployed || !ApplicationDeployment.CurrentDeployment.IsFirstRun)
                {
                    return;
                }
            }
            catch
            {
                return;
            }

            View.ChangeLog changeLog = new View.ChangeLog();
            changeLog.Owner = Application.Current.MainWindow;
            changeLog.ShowDialog();
        }

        /// <summary>
        /// Loads and deserializes the default layout from the project resources.
        /// </summary>
        /// <param name="dockManager">The docking root to which content is loaded.</param>
        private void ResetLayoutStandard(DockingManager dockManager)
        {
            this.LoadLayout(dockManager, resourceName: StandardLayoutResource);
        }

        /// <summary>
        /// Loads and deserializes the default layout from the project resources.
        /// </summary>
        /// <param name="dockManager">The docking root to which content is loaded.</param>
        private void ResetLayoutDeveloper(DockingManager dockManager)
        {
            this.LoadLayout(dockManager, resourceName: DeveloperLayoutResource);
        }

        /// <summary>
        /// Loads and deserializes the saved layout from disk. If no layout found, the default is loaded from resources.
        /// </summary>
        /// <param name="dockManager">The docking root to which content is loaded.</param>
        /// <param name="resourceName">Resource to load the layout from. This is optional.</param>
        private void LoadLayout(DockingManager dockManager, String resourceName = null)
        {
            // Attempt to load from personal saved layout file
            if (String.IsNullOrEmpty(resourceName))
            {
                if (File.Exists(MainViewModel.LayoutSaveFile))
                {
                    try
                    {
                        XmlLayoutSerializer serializer = new XmlLayoutSerializer(dockManager);
                        serializer.Deserialize(MainViewModel.LayoutSaveFile);
                        return;
                    }
                    catch
                    {
                    }
                }

                // Something went wrong or the file is not present -- use the standard layout
                resourceName = MainViewModel.StandardLayoutResource;
            }

            // Attempt to load layout from resource name
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        XmlLayoutSerializer serializer = new XmlLayoutSerializer(dockManager);
                        serializer.Deserialize(stream);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Saves and deserializes the saved layout from disk.
        /// </summary>
        /// <param name="dockManager">The docking root to save.</param>
        private void SaveLayout(DockingManager dockManager)
        {
            try
            {
                XmlLayoutSerializer serializer = new XmlLayoutSerializer(dockManager);
                serializer.Serialize(MainViewModel.LayoutSaveFile);
            }
            catch
            {
            }
        }
    }
    //// End class
}
//// End namesapce