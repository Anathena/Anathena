namespace Ana.Source.Main
{
    using Docking;
    using Mvvm;
    using Mvvm.Command;
    using Snapshots.Prefilter;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using Xceed.Wpf.AvalonDock;
    using Xceed.Wpf.AvalonDock.Layout.Serialization;
    /// <summary>
    /// Main view model
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The save file for the docking layout
        /// </summary>
        private const String LayoutSaveFile = "layout.xml";

        /// <summary>
        /// Singleton instance of the <see cref="MainViewModel" /> class
        /// </summary>
        private static Lazy<MainViewModel> mainViewModelInstance = new Lazy<MainViewModel>(
                () => { return new MainViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Collection of tools contained in the main docking panel
        /// </summary>
        private HashSet<ToolViewModel> tools;

        /// <summary>
        /// Prevents a default instance of the <see cref="MainViewModel" /> class from being created
        /// </summary>
        private MainViewModel()
        {
            this.tools = new HashSet<ToolViewModel>();
            this.Close = new RelayCommand<Window>((window) => this.CloseExecute(window), (window) => true);
            this.Maximize = new RelayCommand<Window>((window) => this.MaximizeExecute(window), (window) => true);
            this.Minimize = new RelayCommand<Window>((window) => this.MinimizeExecute(window), (window) => true);
            this.LoadLayout = new RelayCommand<DockingManager>((dockingManager) => this.LoadLayoutExecute(dockingManager), (dockingManager) => true);
            this.SaveLayout = new RelayCommand<DockingManager>((dockingManager) => this.SaveLayoutExecute(dockingManager), (dockingManager) => true);
            this.OpenProject = new RelayCommand(() => this.OpenProjectExecute(), () => true);

            SnapshotPrefilterFactory.GetSnapshotPrefilter(typeof(ShallowPointerPrefilter)).BeginPrefilter();
        }

        /// <summary>
        /// Gets the command to close the main window
        /// </summary>
        public ICommand Close { get; private set; }

        /// <summary>
        /// Gets the command to maximize the main window
        /// </summary>
        public ICommand Maximize { get; private set; }

        /// <summary>
        /// Gets the command to minimize the main window
        /// </summary>
        public ICommand Minimize { get; private set; }

        /// <summary>
        /// Gets the command to open the current docking layout
        /// </summary>
        public ICommand LoadLayout { get; private set; }

        /// <summary>
        /// Gets the command to save the current docking layout
        /// </summary>
        public ICommand SaveLayout { get; private set; }

        /// <summary>
        /// Gets the command to open a project from disk
        /// </summary>
        public ICommand OpenProject { get; private set; }

        /// <summary>
        /// Gets the tools contained in the main docking panel
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
        /// Gets the singleton instance of the <see cref="MainViewModel" /> class
        /// </summary>
        /// <returns>The singleton instance of the <see cref="MainViewModel" /> class</returns>
        public static MainViewModel GetInstance()
        {
            return mainViewModelInstance.Value;
        }

        /// <summary>
        /// Adds a tool to the list of tools controlled by the main view model
        /// </summary>
        /// <param name="toolViewModel">The tool to be added</param>
        public void Subscribe(ToolViewModel toolViewModel)
        {
            if (toolViewModel != null && !this.tools.Contains(toolViewModel))
            {
                this.tools?.Add(toolViewModel);
            }
        }

        /// <summary>
        /// Removes a tool from the list of tools controlled by the main view model
        /// </summary>
        /// <param name="toolViewModel">The tool to be added</param>
        public void Unsubscribe(ToolViewModel toolViewModel)
        {
            if (toolViewModel != null && this.tools.Contains(toolViewModel))
            {
                this.tools?.Remove(toolViewModel);
            }
        }

        /// <summary>
        /// Closes the main window
        /// </summary>
        private void CloseExecute(Window window)
        {
            window.Close();
        }

        /// <summary>
        /// Maximizes the main window
        /// </summary>
        private void MaximizeExecute(Window window)
        {
            window.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Minimizes the main window
        /// </summary>
        private void MinimizeExecute(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Loads and deserializes the saved layout from disk
        /// </summary>
        private void LoadLayoutExecute(DockingManager dockManager)
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(dockManager);
            serializer.Deserialize(LayoutSaveFile);
        }

        /// <summary>
        /// Saves and deserializes the saved layout from disk
        /// </summary>
        private void SaveLayoutExecute(DockingManager dockManager)
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(dockManager);
            serializer.Serialize(LayoutSaveFile);
        }

        /// <summary>
        /// Opens a project from disk
        /// </summary>
        private void OpenProjectExecute()
        {
        }
    }
    //// End class
}
//// End namesapce