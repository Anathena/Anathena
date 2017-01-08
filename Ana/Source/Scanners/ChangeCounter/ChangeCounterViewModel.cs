namespace Ana.Source.Scanners.ChangeCounter
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Change Counter.
    /// </summary>
    internal class ChangeCounterViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(ChangeCounterViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="ChangeCounterViewModel" /> class.
        /// </summary>
        private static Lazy<ChangeCounterViewModel> changeCounterViewModelInstance = new Lazy<ChangeCounterViewModel>(
                () => { return new ChangeCounterViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="ChangeCounterViewModel" /> class from being created.
        /// </summary>
        private ChangeCounterViewModel() : base("Change Counter")
        {
            this.ContentId = ChangeCounterViewModel.ToolContentId;
            this.StartScanCommand = new RelayCommand(() => Task.Run(() => this.StartScan()), () => true);
            this.StopScanCommand = new RelayCommand(() => Task.Run(() => this.StopScan()), () => true);
            this.ChangeCounterModel = new ChangeCounterModel(this.ScanCountUpdated);

            MainViewModel.GetInstance().Subscribe(this);
        }

        public ICommand StartScanCommand { get; private set; }

        public ICommand StopScanCommand { get; private set; }

        public Int32 ScanCount
        {
            get
            {
                return ChangeCounterModel.ScanCount;
            }
        }

        public Boolean ScanReady
        {
            get
            {
                return true;
            }
        }

        public Boolean StopScanReady
        {
            get
            {
                return !this.ScanReady;
            }
        }

        private ChangeCounterModel ChangeCounterModel { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ChangeCounterViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static ChangeCounterViewModel GetInstance()
        {
            return ChangeCounterViewModel.changeCounterViewModelInstance.Value;
        }

        private void ScanCountUpdated()
        {
            this.RaisePropertyChanged(nameof(this.ScanCount));
        }

        private void StartScan()
        {
            this.ChangeCounterModel.Begin();
        }

        private void StopScan()
        {
            this.ChangeCounterModel.End();
        }
    }
    //// End class
}
//// End namespace