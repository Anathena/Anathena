namespace Ana.Source.Snapshots
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Snapshot Manager
    /// </summary>
    internal class SnapshotManagerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(SnapshotManagerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="SnapshotManagerViewModel" /> class
        /// </summary>
        private static Lazy<SnapshotManagerViewModel> snapshotManagerViewModelInstance = new Lazy<SnapshotManagerViewModel>(
                () => { return new SnapshotManagerViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="SnapshotManagerViewModel" /> class from being created
        /// </summary>
        private SnapshotManagerViewModel() : base("Snapshot Manager")
        {
            this.ContentId = ToolContentId;
            this.IsVisible = true;
            this.NewScanCommand = new RelayCommand(() => Task.Run(() => NewScan()), () => true);
            this.UndoScanCommand = new RelayCommand(() => Task.Run(() => UndoScan()), () => true);

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a command to start a new scan
        /// </summary>
        public ICommand NewScanCommand { get; private set; }

        /// <summary>
        /// Gets a command to undo the last scan
        /// </summary>
        public ICommand UndoScanCommand { get; private set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="SnapshotManagerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static SnapshotManagerViewModel GetInstance()
        {
            return snapshotManagerViewModelInstance.Value;
        }

        /// <summary>
        /// Starts a new scan, clearing old scans
        /// </summary>
        private void NewScan()
        {
            SnapshotManager.GetInstance().ClearSnapshots();
        }

        /// <summary>
        /// Undoes the most recent scan
        /// </summary>
        private void UndoScan()
        {
            SnapshotManager.GetInstance().UndoSnapshot();
        }
    }
    //// End class
}
//// End namespace