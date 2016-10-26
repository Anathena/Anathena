namespace Ana.Source.Project
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using ProjectItems;
    using PropertyViewer;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using UserSettings;
    using Utils.Extensions;

    /// <summary>
    /// View model for the Project Explorer
    /// </summary>
    internal class ProjectExplorerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(ProjectExplorerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="ProjectExplorerViewModel" /> class
        /// </summary>
        private static Lazy<ProjectExplorerViewModel> projectExplorerViewModelInstance = new Lazy<ProjectExplorerViewModel>(
                () => { return new ProjectExplorerViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// The collection of project items
        /// </summary>
        private ReadOnlyCollection<ProjectItemViewModel> projectItems;

        /// <summary>
        /// The selected project item
        /// </summary>
        private ProjectItemViewModel selectedProjectItem;

        /// <summary>
        /// Prevents a default instance of the <see cref="ProjectExplorerViewModel" /> class from being created
        /// </summary>
        private ProjectExplorerViewModel() : base("Project Explorer")
        {
            this.ContentId = ProjectExplorerViewModel.ToolContentId;

            // Commands to manipulate project items may not be async due to multi-threading issues when modifying collections
            this.AddNewFolderItemCommand = new RelayCommand(() => this.AddNewFolderItem(), () => true);
            this.AddNewAddressItemCommand = new RelayCommand(() => this.AddNewAddressItem(), () => true);
            this.AddNewScriptItemCommand = new RelayCommand(() => this.AddNewScriptItem(), () => true);
            this.OpenProjectCommand = new RelayCommand(() => this.OpenProject(), () => true);
            this.ImportProjectCommand = new RelayCommand(() => this.ImportProject(), () => true);
            this.SaveProjectCommand = new RelayCommand(() => this.SaveProject(), () => true);
            this.SaveAsProjectCommand = new RelayCommand(() => this.SaveAsProject(), () => true);
            this.IsVisible = true;
            this.projectItems = new ReadOnlyCollection<ProjectItemViewModel>(new List<ProjectItemViewModel>());
            this.Update();

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets the command to open a project from disk
        /// </summary>
        public ICommand OpenProjectCommand { get; private set; }

        /// <summary>
        /// Gets the command to open a project from disk
        /// </summary>
        public ICommand ImportProjectCommand { get; private set; }

        /// <summary>
        /// Gets the command to open a project from disk
        /// </summary>
        public ICommand SaveProjectCommand { get; private set; }

        /// <summary>
        /// Gets the command to open a project from disk
        /// </summary>
        public ICommand SaveAsProjectCommand { get; private set; }

        /// <summary>
        /// Gets the command to add a new folder
        /// </summary>
        public ICommand AddNewFolderItemCommand { get; private set; }

        /// <summary>
        /// Gets the command to add a new address
        /// </summary>
        public ICommand AddNewAddressItemCommand { get; private set; }

        /// <summary>
        /// Gets the command to add a new script
        /// </summary>
        public ICommand AddNewScriptItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets the collection of project items
        /// </summary>
        public ReadOnlyCollection<ProjectItemViewModel> ProjectItems
        {
            get
            {
                return this.projectItems;
            }

            set
            {
                this.projectItems = value;
                this.RaisePropertyChanged(nameof(this.ProjectItems));
            }
        }

        /// <summary>
        /// Gets or sets the selected project item
        /// </summary>
        public ProjectItemViewModel SelectedProjectItem
        {
            get
            {
                return this.selectedProjectItem;
            }

            set
            {
                this.selectedProjectItem = value;
                PropertyViewerViewModel.GetInstance().SetTargetObjects(this.selectedProjectItem.ProjectItem);
                this.RaisePropertyChanged(nameof(this.SelectedProjectItem));
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ProjectExplorerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ProjectExplorerViewModel GetInstance()
        {
            return ProjectExplorerViewModel.projectExplorerViewModelInstance.Value;
        }

        /// <summary>
        /// Adds a specific address to the project explorer
        /// </summary>
        /// <param name="baseAddress">The address</param>
        /// <param name="elementType">The value type</param>
        public void AddSpecificAddressItem(IntPtr baseAddress, Type elementType)
        {
            this.AddNewProjectItem(new AddressItem(baseAddress, elementType));
        }

        /// <summary>
        /// Adds a new folder to the project items
        /// </summary>
        private void AddNewFolderItem()
        {
            this.AddNewProjectItem(new FolderItem());
        }

        /// <summary>
        /// Adds a new address to the project items
        /// </summary>
        private void AddNewAddressItem()
        {
            this.AddNewProjectItem(new AddressItem());
        }

        /// <summary>
        /// Adds a new script to the project items
        /// </summary>
        private void AddNewScriptItem()
        {
            this.AddNewProjectItem(new ScriptItem());
        }

        /// <summary>
        /// Adds the new project item to the project item collection
        /// </summary>
        /// <param name="projectItem">The project item to add</param>
        private void AddNewProjectItem(ProjectItem projectItem)
        {
            List<ProjectItemViewModel> newItems = new List<ProjectItemViewModel>(this.ProjectItems);

            ProjectItemViewModel folderTarget = this.SelectedProjectItem;

            while (folderTarget != null && !(folderTarget.ProjectItem is FolderItem))
            {
                folderTarget = folderTarget.Parent as ProjectItemViewModel;
            }

            if (folderTarget != null)
            {
                folderTarget.Children.Add(new ProjectItemViewModel(projectItem));
            }
            else
            {
                newItems.Add(new ProjectItemViewModel(projectItem));
            }

            this.ProjectItems = new ReadOnlyCollection<ProjectItemViewModel>(newItems);
        }

        private void Update()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    this.ProjectItems.ForEach(x => this.UpdateRecurse(x));
                    Thread.Sleep(SettingsViewModel.GetInstance().TableReadInterval);
                }
            });
        }

        private void UpdateRecurse(ProjectItemViewModel projectItemViewModel)
        {
            projectItemViewModel?.Children?.ForEach(x => this.UpdateRecurse(x as ProjectItemViewModel));
            projectItemViewModel?.ProjectItem?.Update();
        }

        /// <summary>
        /// Opens a project from disk
        /// </summary>
        private void OpenProject()
        {
        }

        /// <summary>
        /// Imports a project from disk
        /// </summary>
        private void ImportProject()
        {
        }

        /// <summary>
        /// Save a project to disk
        /// </summary>
        private void SaveProject()
        {
        }

        /// <summary>
        /// Save a project to disk under a new specified project name
        /// </summary>
        private void SaveAsProject()
        {
        }
    }
    //// End class
}
//// End namespace