namespace Ana.Source.Project
{
    using CustomControls;
    using Docking;
    using Main;
    using Microsoft.Win32;
    using Mvvm.Command;
    using ProjectItems;
    using PropertyViewer;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using UserSettings;
    using Utils.Extensions;

    /// <summary>
    /// View model for the Project Explorer.
    /// </summary>
    internal class ProjectExplorerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(ProjectExplorerViewModel);

        /// <summary>
        /// The filter to use for saving and loading project filters.
        /// </summary>
        public const String ProjectExtensionFilter = "Cheat File (*.Hax)|*.hax|All files (*.*)|*.*";

        /// <summary>
        /// Singleton instance of the <see cref="ProjectExplorerViewModel" /> class.
        /// </summary>
        private static Lazy<ProjectExplorerViewModel> projectExplorerViewModelInstance = new Lazy<ProjectExplorerViewModel>(
                () => { return new ProjectExplorerViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The project root which contains all project items.
        /// </summary>
        private ProjectRoot projectRoot;

        /// <summary>
        /// The selected project item.
        /// </summary>
        private IEnumerable<ProjectItem> selectedProjectItems;

        /// <summary>
        /// Whether or not there are unsaved project changes.
        /// </summary>
        private Boolean hasUnsavedChanges;

        /// <summary>
        /// The file path to the project file.
        /// </summary>
        private String projectFilePath;

        /// <summary>
        /// Prevents a default instance of the <see cref="ProjectExplorerViewModel" /> class from being created.
        /// </summary>
        private ProjectExplorerViewModel() : base("Project Explorer")
        {
            this.ContentId = ProjectExplorerViewModel.ToolContentId;
            this.ObserverLock = new Object();
            this.ProjectExplorerObservers = new List<IProjectExplorerObserver>();

            // Commands to manipulate project items may not be async due to multi-threading issues when modifying collections
            this.OpenProjectCommand = new RelayCommand(() => this.OpenProject(), () => true);
            this.ImportProjectCommand = new RelayCommand(() => this.ImportProject(), () => true);
            this.ImportSpecificProjectCommand = new RelayCommand<String>((filename) => this.ImportProject(filename), (filename) => true);
            this.SaveProjectCommand = new RelayCommand(() => this.SaveProject(), () => true);
            this.SaveAsProjectCommand = new RelayCommand(() => this.SaveAsProject(), () => true);
            this.AddNewFolderItemCommand = new RelayCommand(() => this.AddNewFolderItem(), () => true);
            this.AddNewAddressItemCommand = new RelayCommand(() => this.AddNewAddressItem(), () => true);
            this.AddNewScriptItemCommand = new RelayCommand(() => this.AddNewScriptItem(), () => true);
            this.DeleteSelectionCommand = new RelayCommand(() => this.DeleteSelection(), () => true);
            this.ToggleSelectionActivationCommand = new RelayCommand(() => this.ToggleSelectionActivation(), () => true);
            this.ProjectRoot = new ProjectRoot();
            this.Update();

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets the command to open a project from disk.
        /// </summary>
        public ICommand OpenProjectCommand { get; private set; }

        /// <summary>
        /// Gets the command to open a project from disk.
        /// </summary>
        public ICommand ImportProjectCommand { get; private set; }

        /// <summary>
        /// Gets the command to open a specific project from disk, used for loading downloaded web projects.
        /// </summary>
        public ICommand ImportSpecificProjectCommand { get; private set; }

        /// <summary>
        /// Gets the command to open a project from disk.
        /// </summary>
        public ICommand SaveProjectCommand { get; private set; }

        /// <summary>
        /// Gets the command to open a project from disk.
        /// </summary>
        public ICommand SaveAsProjectCommand { get; private set; }

        /// <summary>
        /// Gets the command to add a new folder.
        /// </summary>
        public ICommand AddNewFolderItemCommand { get; private set; }

        /// <summary>
        /// Gets the command to add a new address.
        /// </summary>
        public ICommand AddNewAddressItemCommand { get; private set; }

        /// <summary>
        /// Gets the command to add a new script.
        /// </summary>
        public ICommand AddNewScriptItemCommand { get; private set; }

        /// <summary>
        /// Gets the command to delete the selected project explorer items.
        /// </summary>
        public ICommand DeleteSelectionCommand { get; private set; }

        /// <summary>
        /// Gets the command to toggle the activation the selected project explorer items.
        /// </summary>
        public ICommand ToggleSelectionActivationCommand { get; private set; }

        /// <summary>
        /// Gets the command to clear the selected project item.
        /// </summary>
        public ICommand ClearSelectionCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are unsaved changes.
        /// </summary>
        public Boolean HasUnsavedChanges
        {
            get
            {
                return this.hasUnsavedChanges;
            }

            set
            {
                this.hasUnsavedChanges = value;
                this.RaisePropertyChanged(nameof(this.HasUnsavedChanges));
            }
        }

        /// <summary>
        /// Gets the path to the project file.
        /// </summary>
        public String ProjectFilePath
        {
            get
            {
                return this.projectFilePath;
            }

            private set
            {
                this.projectFilePath = value;
                this.RaisePropertyChanged(nameof(this.ProjectFilePath));
            }
        }

        /// <summary>
        /// Gets the root that contains all project items.
        /// </summary>
        public ProjectRoot ProjectRoot
        {
            get
            {
                return this.projectRoot;
            }

            private set
            {
                this.projectRoot = value;
                this.NotifyObserversStructureChange();
                this.RaisePropertyChanged(nameof(this.ProjectRoot));
            }
        }

        /// <summary>
        /// Gets or sets the selected project items.
        /// </summary>
        public IEnumerable<ProjectItem> SelectedProjectItems
        {
            get
            {
                return this.selectedProjectItems;
            }

            set
            {
                this.selectedProjectItems = value;
                PropertyViewerViewModel.GetInstance().SetTargetObjects(this.SelectedProjectItems?.ToArray());
            }
        }

        /// <summary>
        /// Gets or sets a lock that controls access to observing classes.
        /// </summary>
        private Object ObserverLock { get; set; }

        /// <summary>
        /// Gets or sets objects observing changes in the selected objects.
        /// </summary>
        private List<IProjectExplorerObserver> ProjectExplorerObservers { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ProjectExplorerViewModel" /> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static ProjectExplorerViewModel GetInstance()
        {
            return ProjectExplorerViewModel.projectExplorerViewModelInstance.Value;
        }

        /// <summary>
        /// Prompts the user to save the project if there are unsaved changes.
        /// </summary>
        /// <returns>Returns false if canceled, otherwise true.</returns>
        public Boolean PromptSave()
        {
            if (!this.HasUnsavedChanges)
            {
                return true;
            }

            String projectName = Path.GetFileName(this.ProjectFilePath);

            if (String.IsNullOrWhiteSpace(projectName))
            {
                projectName = "Untitled";
            }

            MessageBoxResult result = MessageBoxEx.Show(
                "Save changes to project " + projectName + "?",
                "Unsaved Changes",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
            {
                return false;
            }

            if (result == MessageBoxResult.Yes)
            {
                this.SaveProject();
            }

            return true;
        }

        /// <summary>
        /// Adds a specific address to the project explorer.
        /// </summary>
        /// <param name="baseAddress">The address.</param>
        /// <param name="elementType">The value type.</param>
        public void AddSpecificAddressItem(IntPtr baseAddress, Type elementType)
        {
            this.AddNewProjectItems(true, new AddressItem(baseAddress, elementType));
        }

        /// <summary>
        /// Adds the new project item to the project item collection.
        /// </summary>
        /// <param name="addToSelected">Whether or not the items should be added under the selected item.</param>
        /// <param name="projectItems">The project item to add.</param>
        public void AddNewProjectItems(Boolean addToSelected = true, params ProjectItem[] projectItems)
        {
            if (projectItems.IsNullOrEmpty())
            {
                return;
            }

            foreach (ProjectItem projectItem in projectItems)
            {
                if (ProjectRoot.HasNode(projectItem))
                {
                    return;
                }

                ProjectItem target = this.SelectedProjectItems?.FirstOrDefault();

                // Atempt to find the correct folder to place the new item into
                while (target != null && !(target is FolderItem))
                {
                    target = target.Parent as ProjectItem;
                }

                FolderItem targetFolder = target as FolderItem;

                if (target != null)
                {
                    targetFolder.AddChild(projectItem);
                }
                else
                {
                    this.ProjectRoot.AddChild(projectItem);
                }
            }

            this.NotifyObserversStructureChange();
        }

        /// <summary>
        /// Called when a property is updated in any of the contained project items.
        /// </summary>
        public void OnPropertyUpdate()
        {
            this.NotifyObserversStructureChange();
        }

        /// <summary>
        /// Subscribes the given object to changes in the project structure.
        /// </summary>
        /// <param name="projectExplorerObserver">The object to observe project structure changes.</param>
        public void Subscribe(IProjectExplorerObserver projectExplorerObserver)
        {
            lock (this.ObserverLock)
            {
                if (!this.ProjectExplorerObservers.Contains(projectExplorerObserver))
                {
                    this.ProjectExplorerObservers.Add(projectExplorerObserver);
                    projectExplorerObserver.Update(this.ProjectRoot);
                    projectExplorerObserver.UpdateStructure(this.ProjectRoot);
                }
            }
        }

        /// <summary>
        /// Unsubscribes the given object from changes in the project structure.
        /// </summary>
        /// <param name="projectExplorerObserver">The object to observe project structure changes.</param>
        public void Unsubscribe(IProjectExplorerObserver projectExplorerObserver)
        {
            lock (this.ObserverLock)
            {
                if (this.ProjectExplorerObservers.Contains(projectExplorerObserver))
                {
                    this.ProjectExplorerObservers.Remove(projectExplorerObserver);
                }
            }
        }

        /// <summary>
        /// Adds a new folder to the project items.
        /// </summary>
        private void AddNewFolderItem()
        {
            this.AddNewProjectItems(true, new FolderItem());
        }

        /// <summary>
        /// Adds a new address to the project items.
        /// </summary>
        private void AddNewAddressItem()
        {
            this.AddNewProjectItems(true, new AddressItem());
        }

        /// <summary>
        /// Adds a new script to the project items.
        /// </summary>
        private void AddNewScriptItem()
        {
            this.AddNewProjectItems(true, new ScriptItem());
        }

        /// <summary>
        /// Deletes the selected project explorer items.
        /// </summary>
        private void DeleteSelection()
        {
            this.ProjectRoot.RemoveNodes(this.SelectedProjectItems);
            this.SelectedProjectItems = null;

            this.NotifyObserversStructureChange();
        }

        /// <summary>
        /// Toggles the activation the selected project explorer items.
        /// </summary>
        private void ToggleSelectionActivation()
        {
            if (this.SelectedProjectItems == null || this.SelectedProjectItems.Count() < 0)
            {
                return;
            }

            foreach (ProjectItem projectItem in this.SelectedProjectItems)
            {
                if (projectItem != null)
                {
                    projectItem.IsActivated = !projectItem.IsActivated;
                }
            }
        }

        /// <summary>
        /// Continously updates the project explorer items.
        /// </summary>
        private void Update()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    this.ProjectRoot.Update();
                    this.NotifyObserversValueChange();
                    Thread.Sleep(SettingsViewModel.GetInstance().TableReadInterval);
                }
            });
        }

        /// <summary>
        /// Opens a project from disk.
        /// </summary>
        private void OpenProject()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ProjectExtensionFilter;
            openFileDialog.Title = "Open Project";

            if (openFileDialog.ShowDialog() == true)
            {
                this.ProjectFilePath = openFileDialog.FileName;

                if (this.ProjectFilePath == null || this.ProjectFilePath == String.Empty)
                {
                    return;
                }

                try
                {
                    using (FileStream fileStream = new FileStream(this.ProjectFilePath, FileMode.Open, FileAccess.Read))
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ProjectRoot));
                        this.ProjectRoot = serializer.ReadObject(fileStream) as ProjectRoot;
                        this.HasUnsavedChanges = false;
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Imports a project from disk, adding the project items to the current project.
        /// </summary>
        /// <param name="filename">The file path of the project to import.</param>
        private void ImportProject(String filename = null)
        {
            // Ask for a specific file if one was not explicitly provided
            if (filename == null || filename == String.Empty)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = ProjectExtensionFilter;
                openFileDialog.Title = "Import Project";

                if (openFileDialog.ShowDialog() == true)
                {
                    this.ProjectFilePath = openFileDialog.FileName;

                    if (this.ProjectFilePath == null || this.ProjectFilePath == String.Empty)
                    {
                        return;
                    }

                    filename = this.ProjectFilePath;
                }
            }

            try
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    String why = Encoding.ASCII.GetString(File.ReadAllBytes(filename));
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ProjectRoot));
                    ProjectRoot importedProjectRoot = serializer.ReadObject(fileStream) as ProjectRoot;

                    foreach (ProjectItem child in importedProjectRoot.Children)
                    {
                        this.AddNewProjectItems(false, child);
                    }

                    this.HasUnsavedChanges = true;
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Save a project to disk.
        /// </summary>
        private void SaveProject()
        {
            if (this.ProjectFilePath == null || this.ProjectFilePath == String.Empty)
            {
                this.SaveAsProject();
                return;
            }

            try
            {
                using (FileStream fileStream = new FileStream(this.ProjectFilePath, FileMode.Create, FileAccess.Write))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ProjectRoot));
                    serializer.WriteObject(fileStream, this.ProjectRoot);
                }

                this.HasUnsavedChanges = false;
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Save a project to disk under a new specified project name.
        /// </summary>
        private void SaveAsProject()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ProjectExplorerViewModel.ProjectExtensionFilter;
            saveFileDialog.Title = "Save Project";

            if (saveFileDialog.ShowDialog() == true)
            {
                this.ProjectFilePath = saveFileDialog.FileName;
                this.SaveProject();
            }
        }

        /// <summary>
        /// Notify all observing objects of a change in the project structure.
        /// </summary>
        private void NotifyObserversStructureChange()
        {
            lock (this.ObserverLock)
            {
                foreach (IProjectExplorerObserver observer in this.ProjectExplorerObservers)
                {
                    observer.UpdateStructure(this.ProjectRoot);
                }
            }
        }

        /// <summary>
        /// Notify all observing objects of a change in the project structure.
        /// </summary>
        private void NotifyObserversValueChange()
        {
            lock (this.ObserverLock)
            {
                foreach (IProjectExplorerObserver observer in this.ProjectExplorerObservers)
                {
                    observer.Update(this.ProjectRoot);
                }
            }
        }
    }
    //// End class
}
//// End namespace