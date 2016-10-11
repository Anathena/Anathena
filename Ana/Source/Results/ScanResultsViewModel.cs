namespace Ana.Source.ScanResults
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using Snapshots;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Input;
    using Utils.Validation;
    /// <summary>
    /// View model for the Process Selector
    /// </summary>
    internal class ScanResultsViewModel : ToolViewModel, ISnapshotObserver
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(ScanResultsViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="ScanResultsViewModel" /> class
        /// </summary>
        private static Lazy<ScanResultsViewModel> scanResultsViewModelInstance = new Lazy<ScanResultsViewModel>(
                () => { return new ScanResultsViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        private UInt64 addressCount;
        private List<SnapshotElement> activeViewableElements;
        private IEnumerable<SnapshotElement> addresses;

        /// <summary>
        /// Prevents a default instance of the <see cref="ScanResultsViewModel" /> class from being created
        /// </summary>
        private ScanResultsViewModel() : base("Scan Results")
        {
            this.ContentId = ToolContentId;
            this.AddAddressCommand = new RelayCommand<Object>((address) => this.AddAddress(address), (address) => true);
            this.activeViewableElements = new List<SnapshotElement>();
            this.IsVisible = true;

            // Temp debug
            SnapshotRegion<Null> k = new SnapshotRegion<Null>(new IntPtr(0x6666), 420);
            this.addresses = new List<SnapshotElement>(new SnapshotElement[] { new SnapshotElement<Null>(k), new SnapshotElement<Null>(k),
                new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k),
                new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k),
                new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k),
                new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k),
                new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k), new SnapshotElement<Null>(k),
                new SnapshotElement<Null>(k) });
            this.RaisePropertyChanged(nameof(this.Addresses));

            SnapshotManager.GetInstance().Subscribe(this);
            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets the command to scroll down the view
        /// </summary>
        public ICommand ScrollDownViewCommand { get; private set; }

        /// <summary>
        /// Gets the command to scroll up the view
        /// </summary>
        public ICommand ScrollUpViewCommand { get; private set; }

        /// <summary>
        /// Gets the command to select a target process
        /// </summary>
        public ICommand AddAddressCommand { get; private set; }

        /// <summary>
        /// The the size (in B, KB, MB, GB, TB, etc) of the results found
        /// </summary>
        public String ResultSize
        {
            get
            {
                return Conversions.BytesToMetric<UInt64>(this.addressCount);
            }
        }

        /// <summary>
        /// The total number of addresses found
        /// </summary>
        public UInt64 ResultCount
        {
            get
            {
                return this.addressCount;
            }
            set
            {
                this.addressCount = value;
                this.RaisePropertyChanged(nameof(this.ResultCount));
                this.RaisePropertyChanged(nameof(this.ResultSize));
            }
        }

        /// <summary>
        /// Gets the address elements
        /// </summary>
        public IEnumerable<SnapshotElement> Addresses
        {
            get
            {
                return addresses;
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ScanResultsViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ScanResultsViewModel GetInstance()
        {
            return ScanResultsViewModel.scanResultsViewModelInstance.Value;
        }

        /// <summary>
        /// Recieves an update of the active snapshot
        /// </summary>
        /// <param name="process">The active snapshot</param>
        public void Update(Snapshot snapshot)
        {
            this.ResultCount = snapshot == null ? 0 : snapshot.GetElementCount();
        }

        /// <summary>
        /// Adds the given address to the table
        /// </summary>
        private void AddAddress(Object address)
        {
        }
    }
    //// End class
}
//// End namespace