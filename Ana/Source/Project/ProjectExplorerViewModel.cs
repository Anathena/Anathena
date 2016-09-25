namespace Ana.Source.Project
{
    using Docking;
    using Engine;
    using Engine.Processes;
    using Main;
    using Mvvm.Command;
    using ProjectItems;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Windows.Input;
    using Utils;

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

        private readonly ReadOnlyCollection<ProjectItemViewModel> projectItems;

        /// <summary>
        /// Prevents a default instance of the <see cref="ProjectExplorerViewModel" /> class from being created
        /// </summary>
        private ProjectExplorerViewModel() : base("Project Explorer")
        {
            this.ContentId = ToolContentId;
            this.IconSource = ImageLoader.LoadImage("pack://application:,,/Content/Icons/SelectProcess.png");
            this.SelectProcessCommand = new RelayCommand<NormalizedProcess>((process) => this.SelectProcess(process), (process) => true);
            this.IsVisible = true;


            this.projectItems = new ReadOnlyCollection<ProjectItemViewModel>
                (
                new List<ProjectItemViewModel>(new ProjectItemViewModel[] { new ProjectItemViewModel(new FolderItem(), null) })
                // (from region in projectItems select new ProjectItemViewModel(region, null)).ToList()
                );

            MainViewModel.GetInstance().Subscribe(this);
        }



        public ReadOnlyCollection<ProjectItemViewModel> Regions
        {
            get
            {
                return projectItems;
            }
        }

        /// <summary>
        /// Gets the command to select a target process
        /// </summary>
        public ICommand SelectProcessCommand { get; private set; }

        /// <summary>
        /// Gets the processes running on the machine
        /// </summary>
        public IEnumerable<NormalizedProcess> ProcessList
        {
            get
            {
                return EngineCore.GetInstance().Processes.GetProcesses();
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ProjectExplorerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ProjectExplorerViewModel GetInstance()
        {
            return projectExplorerViewModelInstance.Value;
        }

        /// <summary>
        /// Makes the target process selection
        /// </summary>
        /// <param name="process">The process being selected</param>
        private void SelectProcess(NormalizedProcess process)
        {
            if (process == null)
            {
                return;
            }

            EngineCore.GetInstance().Processes.OpenProcess(process);

            this.IsVisible = false;
        }
    }
    //// End class
}
//// End namespace