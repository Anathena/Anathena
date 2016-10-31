namespace Ana.Source.Scanners.PointerScanner
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Input Correlator
    /// </summary>
    internal class PointerScannerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(PointerScannerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="PointerScannerViewModel" /> class
        /// </summary>
        private static Lazy<PointerScannerViewModel> inputCorrelatorViewModelInstance = new Lazy<PointerScannerViewModel>(
                () => { return new PointerScannerViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="InputCorrelatorViewModel" /> class from being created
        /// </summary>
        private PointerScannerViewModel() : base("Pointer Scanner")
        {
            this.ContentId = PointerScannerViewModel.ToolContentId;
            this.StartScanCommand = new RelayCommand(() => Task.Run(() => this.StartScan()), () => true);
            this.StopScanCommand = new RelayCommand(() => Task.Run(() => this.StopScan()), () => true);
            this.PointerScannerModel = new PointerScannerModel();

            MainViewModel.GetInstance().Subscribe(this);
        }

        public ICommand StartScanCommand { get; private set; }

        public ICommand StopScanCommand { get; private set; }

        private PointerScannerModel PointerScannerModel { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ChangeCounterViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static PointerScannerViewModel GetInstance()
        {
            return inputCorrelatorViewModelInstance.Value;
        }

        private void StartScan()
        {
            this.PointerScannerModel.Begin();
        }

        private void StopScan()
        {
            this.PointerScannerModel.End();
        }
    }
    //// End class
}
//// End namespace