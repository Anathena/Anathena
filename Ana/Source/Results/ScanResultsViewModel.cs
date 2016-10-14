namespace Ana.Source.Results
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
        /// The number of elements to display on each page
        /// </summary>
        private const Int32 PageSize = 128;

        /// <summary>
        /// Singleton instance of the <see cref="ScanResultsViewModel" /> class
        /// </summary>
        private static Lazy<ScanResultsViewModel> scanResultsViewModelInstance = new Lazy<ScanResultsViewModel>(
                () => { return new ScanResultsViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// The current page of scan results
        /// </summary>
        private UInt64 currentPage;

        /// <summary>
        /// The total number of addresses
        /// </summary>
        private UInt64 addressCount;

        /// <summary>
        /// The addresses on the current page
        /// </summary>
        private IEnumerable<ScanResult> addresses;

        /// <summary>
        /// Prevents a default instance of the <see cref="ScanResultsViewModel" /> class from being created
        /// </summary>
        private ScanResultsViewModel() : base("Scan Results")
        {
            this.ContentId = ToolContentId;
            this.FirstPageCommand = new RelayCommand(() => this.FirstPage(), () => true);
            this.LastPageCommand = new RelayCommand(() => this.LastPage(), () => true);
            this.PreviousPageCommand = new RelayCommand(() => this.PreviousPage(), () => true);
            this.NextPageCommand = new RelayCommand(() => this.NextPage(), () => true);
            this.AddAddressCommand = new RelayCommand<ScanResult>((address) => this.AddAddress(address), (address) => true);
            this.IsVisible = true;
            this.addresses = new List<ScanResult>();

            SnapshotManager.GetInstance().Subscribe(this);
            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets the command to go to the first page
        /// </summary>
        public ICommand FirstPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to go to the last page
        /// </summary>
        public ICommand LastPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to go to the previous page
        /// </summary>
        public ICommand PreviousPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to go to the next page
        /// </summary>
        public ICommand NextPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to select a target process
        /// </summary>
        public ICommand AddAddressCommand { get; private set; }

        /// <summary>
        /// Gets or sets the total number of addresses found
        /// </summary>
        public UInt64 CurrentPage
        {
            get
            {
                return this.currentPage;
            }

            set
            {
                this.currentPage = value;
                this.LoadResults();
                this.RaisePropertyChanged(nameof(this.CurrentPage));
            }
        }

        /// <summary>
        /// Gets the total number of addresses found
        /// </summary>
        public UInt64 PageCount
        {
            get
            {
                return this.ResultCount / ScanResultsViewModel.PageSize;
            }
        }

        /// <summary>
        /// Gets the size (in B, KB, MB, GB, TB, etc) of the results found
        /// </summary>
        public String ResultSize
        {
            get
            {
                return Conversions.BytesToMetric<UInt64>(this.addressCount);
            }
        }

        /// <summary>
        /// Gets or sets the total number of addresses found
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
                this.RaisePropertyChanged(nameof(this.PageCount));
            }
        }

        /// <summary>
        /// Gets the address elements
        /// </summary>
        public IEnumerable<ScanResult> Addresses
        {
            get
            {
                return this.addresses;
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
        /// <param name="snapshot">The active snapshot</param>
        public void Update(Snapshot snapshot)
        {
            this.ResultCount = snapshot == null ? 0 : snapshot.GetElementCount();
            this.CurrentPage = 0;
        }

        /// <summary>
        /// Loads the results for the current page
        /// </summary>
        private void LoadResults()
        {
            Snapshot snapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            if (snapshot == null)
            {
                return;
            }

            List<ScanResult> newAddresses = new List<ScanResult>();

            UInt64 startIndex = Math.Min(ScanResultsViewModel.PageSize * this.CurrentPage, snapshot.GetElementCount());

            UInt64 endIndex = Math.Min((ScanResultsViewModel.PageSize * this.CurrentPage) + ScanResultsViewModel.PageSize, snapshot.GetElementCount());

            for (UInt64 index = startIndex; index < endIndex; index++)
            {
                SnapshotElement element = snapshot[(Int32)index];

                dynamic label = String.Empty;
                if (((dynamic)snapshot)[(Int32)index].ElementLabel != null)
                {
                    label = ((dynamic)snapshot)[(Int32)index].ElementLabel;
                }

                newAddresses.Add(new ScanResult(element.BaseAddress, element.GetValue().ToString(), element.GetPreviousValue().ToString(), label));
            }

            this.addresses = newAddresses;
            this.RaisePropertyChanged(nameof(this.Addresses));
        }

        /// <summary>
        /// Goes to the first page of results
        /// </summary>
        private void FirstPage()
        {
            this.CurrentPage = 0;
        }

        /// <summary>
        /// Goes to the last page of results
        /// </summary>
        private void LastPage()
        {
            this.CurrentPage = this.PageCount;
        }

        /// <summary>
        /// Goes to the previous page of results
        /// </summary>
        private void PreviousPage()
        {
            this.CurrentPage = this.CurrentPage == 0 ? this.CurrentPage : this.CurrentPage - 1;
        }

        /// <summary>
        /// Goes to the next page of results
        /// </summary>
        private void NextPage()
        {
            this.CurrentPage = this.CurrentPage >= this.PageCount ? this.CurrentPage : this.CurrentPage + 1;
        }

        /// <summary>
        /// Adds the given scan result address to the project explorer
        /// </summary>
        /// <param name="scanResult">The scan result to add to the project explorer</param>
        private void AddAddress(ScanResult scanResult)
        {
        }
    }
    //// End class
}
//// End namespace