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

    /// <summary>
    /// View model for the Process Selector
    /// </summary>
    internal class ScanResultsViewModel : ToolViewModel
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

        /// <summary>
        /// Prevents a default instance of the <see cref="ScanResultsViewModel" /> class from being created
        /// </summary>
        private ScanResultsViewModel() : base("Scan Results")
        {
            this.ContentId = ToolContentId;
            this.AddAddressCommand = new RelayCommand<Object>((address) => this.AddAddress(address), (address) => true);
            this.IsVisible = true;

            // Temp debug
            SnapshotRegion<Null> k = new SnapshotRegion<Null>(new IntPtr(0x6666), 420);
            Addresses = new List<SnapshotElement>(new SnapshotElement[] { new SnapshotElement<Null>(k), new SnapshotElement<Null>(k) });

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
        /// The total number of addresses found
        /// </summary>
        public UInt64 AddressCount { get; set; }

        /// <summary>
        /// Gets the addresses
        /// </summary>
        public IEnumerable<SnapshotElement> Addresses { get; private set; }

        /// <summary>
        /// Gets the labels for the addresses
        /// </summary>
        public IEnumerable<Object> AddressLabels { get; private set; }

        /// <summary>
        /// Gets the values of the addresses
        /// </summary>
        public IEnumerable<Object> AddressValues { get; private set; }

        /// <summary>
        /// Gets the previous values of the addresses
        /// </summary>
        public IEnumerable<Object> AddressPreviousValues { get; private set; }

        /// <summary>
        /// Gets or sets the view window into what addresses are passed to the view
        /// </summary>
        private UInt64 ViewWindowStart { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ScanResultsViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ScanResultsViewModel GetInstance()
        {
            return scanResultsViewModelInstance.Value;
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