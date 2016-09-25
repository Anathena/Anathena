namespace Ana.Source.LabelThresholder
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

    /// <summary>
    /// View model for the Label Thresholder
    /// </summary>
    internal class LabelThresholderViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(LabelThresholderViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="LabelThresholderViewModel" /> class
        /// </summary>
        private static Lazy<LabelThresholderViewModel> labelThresholderViewModelInstance = new Lazy<LabelThresholderViewModel>(
                () => { return new LabelThresholderViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="LabelThresholderViewModel" /> class from being created
        /// </summary>
        private LabelThresholderViewModel() : base("Label Thresholder")
        {
            this.ContentId = ToolContentId;
            this.SelectProcessCommand = new RelayCommand<NormalizedProcess>((process) => this.SelectProcess(process), (process) => true);

            MainViewModel.GetInstance().Subscribe(this);
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
        /// Gets a singleton instance of the <see cref="LabelThresholderViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static LabelThresholderViewModel GetInstance()
        {
            return labelThresholderViewModelInstance.Value;
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