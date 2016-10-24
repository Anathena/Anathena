namespace Ana.Source.Snapshots
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Snapshot Manager
    /// </summary>
    internal class SnapshotManagerViewModel : ToolViewModel, ISnapshotObserver
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(SnapshotManagerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="SnapshotManagerViewModel"/> class
        /// </summary>
        private static Lazy<SnapshotManagerViewModel> snapshotManagerViewModelInstance = new Lazy<SnapshotManagerViewModel>(
                () => { return new SnapshotManagerViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="SnapshotManagerViewModel"/> class from being created
        /// </summary>
        private SnapshotManagerViewModel() : base("Snapshot Manager")
        {
            this.ContentId = ToolContentId;
            this.IsVisible = true;
            this.ClearSnapshotsCommand = new RelayCommand(() => Task.Run(() => this.ClearSnapshots()), () => true);
            this.UndoSnapshotCommand = new RelayCommand(() => Task.Run(() => this.UndoSnapshot()), () => true);
            this.RedoSnapshotCommand = new RelayCommand(() => Task.Run(() => this.RedoSnapshot()), () => true);

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
            Task.Run(() => SnapshotManager.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets a command to start a new scan
        /// </summary>
        public ICommand ClearSnapshotsCommand { get; private set; }

        /// <summary>
        /// Gets a command to undo the last scan
        /// </summary>
        public ICommand UndoSnapshotCommand { get; private set; }

        /// <summary>
        /// Gets a command to redo the last scan
        /// </summary>
        public ICommand RedoSnapshotCommand { get; private set; }

        public ObservableCollection<Snapshot> Snapshots
        {
            get
            {
                return new ObservableCollection<Snapshot>(SnapshotManager.GetInstance().Snapshots);
            }
        }

        public ObservableCollection<Snapshot> DeletedSnapshots
        {
            get
            {
                return new ObservableCollection<Snapshot>(SnapshotManager.GetInstance().DeletedSnapshots.Reverse());
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="SnapshotManagerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static SnapshotManagerViewModel GetInstance()
        {
            return snapshotManagerViewModelInstance.Value;
        }

        public void Update(Snapshot snapshot)
        {
            this.RaisePropertyChanged(nameof(this.Snapshots));
            this.RaisePropertyChanged(nameof(this.DeletedSnapshots));
        }

        /// <summary>
        /// Clears all snapshots
        /// </summary>
        private void ClearSnapshots()
        {
            SnapshotManager.GetInstance().ClearSnapshots();
        }

        /// <summary>
        /// Undoes the most recent snapshot
        /// </summary>
        private void UndoSnapshot()
        {
            SnapshotManager.GetInstance().UndoSnapshot();
        }

        /// <summary>
        /// Redoes the most recent snapshot
        /// </summary>
        private void RedoSnapshot()
        {
            SnapshotManager.GetInstance().RedoSnapshot();
        }
    }
    //// End class
}
//// End namespace