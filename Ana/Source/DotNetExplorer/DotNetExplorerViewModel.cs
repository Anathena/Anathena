namespace Ana.Source.DotNetExplorer
{
    using Docking;
    using Engine;
    using Engine.Processes;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Input;
    using Utils;

    /// <summary>
    /// View model for the .Net Explorer
    /// </summary>
    internal class DotNetExplorerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="DotNetExplorerViewModel" /> class
        /// </summary>
        private static Lazy<DotNetExplorerViewModel> dotNetExplorerViewModelInstance = new Lazy<DotNetExplorerViewModel>(
                () => { return new DotNetExplorerViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(DotNetExplorerViewModel);

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetExplorerViewModel" /> class
        /// </summary>
        private DotNetExplorerViewModel() : base(".Net Explorer")
        {
            this.ContentId = ToolContentId;
            this.IconSource = ImageLoader.LoadImage("pack://application:,,/Content/Icons/SelectProcess.png");

            this.SelectProcessCommand = new RelayCommand<NormalizedProcess>((process) => this.SelectProcess(process), (process) => true);

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="DotNetExplorerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static DotNetExplorerViewModel GetInstance()
        {
            return dotNetExplorerViewModelInstance.Value;
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