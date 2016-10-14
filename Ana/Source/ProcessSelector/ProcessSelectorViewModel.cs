namespace Ana.Source.ProcessSelector
{
    using Docking;
    using Engine;
    using Engine.Processes;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Utils;

    /// <summary>
    /// View model for the Process Selector
    /// </summary>
    internal class ProcessSelectorViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(ProcessSelectorViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="ProcessSelectorViewModel" /> class
        /// </summary>
        private static Lazy<ProcessSelectorViewModel> processSelectorViewModelInstance = new Lazy<ProcessSelectorViewModel>(
                () => { return new ProcessSelectorViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="ProcessSelectorViewModel" /> class from being created
        /// </summary>
        private ProcessSelectorViewModel() : base("Process Selector")
        {
            this.ContentId = ToolContentId;
            this.IconSource = ImageLoader.LoadImage("pack://application:,,,/Ana;component/Content/Icons/SelectProcess.png");
            this.RefreshProcessListCommand = new RelayCommand(() => this.RefreshProcessList(), () => true);
            this.SelectProcessCommand = new RelayCommand<NormalizedProcess>((process) => this.SelectProcess(process), (process) => true);

            Task.Run(() => { MainViewModel.GetInstance().Subscribe(this); });
        }

        /// <summary>
        /// Gets the command to refresh the process list
        /// </summary>
        public ICommand RefreshProcessListCommand { get; private set; }

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
        /// Gets the name of the opened process
        /// </summary>
        public String ProcessName
        {
            get
            {
                String processName = EngineCore.GetInstance().Processes?.GetOpenedProcess()?.ProcessName;
                return String.IsNullOrEmpty(processName) ? "Please Select a Process" : processName;
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ProcessSelectorViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ProcessSelectorViewModel GetInstance()
        {
            return processSelectorViewModelInstance.Value;
        }

        /// <summary>
        /// Refreshes the process list
        /// </summary>
        private void RefreshProcessList()
        {
            // Raise event to update the process list
            this.RaisePropertyChanged(nameof(this.ProcessList));
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

            // Raise event to update process name in the view
            this.RaisePropertyChanged(nameof(this.ProcessName));

            this.IsVisible = false;
        }
    }
    //// End class
}
//// End namespace