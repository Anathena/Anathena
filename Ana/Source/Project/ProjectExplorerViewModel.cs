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
    using System.Windows.Input;

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
            this.ContentId = ToolContentId;

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

            MainViewModel.GetInstance().Subscribe(this);
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
                PropertyViewerViewModel.GetInstance().SetTargetObject(this.selectedProjectItem.ProjectItem);
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

            ProjectItemViewModel target = null;

            foreach (ProjectItemViewModel projectItemViewModel in this.ProjectItems)
            {
                target = this.GetAddProjectItemTargetRecurse(projectItemViewModel);

                if (target != null)
                {
                    break;
                }
            }

            if (target != null)
            {
                target.Children.Add(new ProjectItemViewModel(projectItem));
            }
            else
            {
                newItems.Add(new ProjectItemViewModel(projectItem));
            }

            this.ProjectItems = new ReadOnlyCollection<ProjectItemViewModel>(newItems);
        }

        /// <summary>
        /// Helper function for determining the correct folder to which the project item is added
        /// </summary>
        /// <param name="projectItemViewModel">The view model of the project item being added</param>
        /// <returns>The view model for the project item to which the project item will be added</returns>
        private ProjectItemViewModel GetAddProjectItemTargetRecurse(ProjectItemViewModel projectItemViewModel)
        {
            if (projectItemViewModel.ProjectItem is FolderItem)
            {
                if (projectItemViewModel.IsSelected)
                {
                    return projectItemViewModel;
                }
                else
                {
                    foreach (ProjectItemViewModel childViewModel in projectItemViewModel.Children)
                    {
                        ProjectItemViewModel childResult = this.GetAddProjectItemTargetRecurse(childViewModel);

                        if (childResult != null)
                        {
                            return childResult;
                        }
                    }
                }
            }

            return null;
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