namespace Ana.Source.Results.ScanResults
{
    using Content;
    using Docking;
    using Engine;
    using Main;
    using Mvvm.Command;
    using Project;
    using Snapshots;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using UserSettings;
    using Utils.Extensions;

    /// <summary>
    /// View model for the Process Selector.
    /// </summary>
    internal class ScanResultsViewModel : ToolViewModel, ISnapshotObserver
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(ScanResultsViewModel);

        /// <summary>
        /// The number of elements to display on each page.
        /// </summary>
        private const Int32 PageSize = 64;

        /// <summary>
        /// Singleton instance of the <see cref="ScanResultsViewModel" /> class.
        /// </summary>
        private static Lazy<ScanResultsViewModel> scanResultsViewModelInstance = new Lazy<ScanResultsViewModel>(
                () => { return new ScanResultsViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The active data type for the scan results.
        /// </summary>
        private Type activeType;

        /// <summary>
        /// The current page of scan results.
        /// </summary>
        private Int64 currentPage;

        /// <summary>
        /// The total number of addresses.
        /// </summary>
        private Int64 addressCount;

        /// <summary>
        /// The total number of bytes in memory.
        /// </summary>
        private Int64 byteCount;

        /// <summary>
        /// The addresses on the current page.
        /// </summary>
        private ObservableCollection<ScanResult> addresses;

        /// <summary>
        /// Prevents a default instance of the <see cref="ScanResultsViewModel" /> class from being created.
        /// </summary>
        private ScanResultsViewModel() : base("Scan Results")
        {
            this.ContentId = ScanResultsViewModel.ToolContentId;
            this.ChangeTypeSByteCommand = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(SByte))), () => true);
            this.ChangeTypeInt16Command = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(Int16))), () => true);
            this.ChangeTypeInt32Command = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(Int32))), () => true);
            this.ChangeTypeInt64Command = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(Int64))), () => true);
            this.ChangeTypeByteCommand = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(Byte))), () => true);
            this.ChangeTypeUInt16Command = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(UInt16))), () => true);
            this.ChangeTypeUInt32Command = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(UInt32))), () => true);
            this.ChangeTypeUInt64Command = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(UInt64))), () => true);
            this.ChangeTypeSingleCommand = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(Single))), () => true);
            this.ChangeTypeDoubleCommand = new RelayCommand(() => Task.Run(() => this.ChangeType(typeof(Double))), () => true);
            this.FirstPageCommand = new RelayCommand(() => Task.Run(() => this.FirstPage()), () => true);
            this.LastPageCommand = new RelayCommand(() => Task.Run(() => this.LastPage()), () => true);
            this.PreviousPageCommand = new RelayCommand(() => Task.Run(() => this.PreviousPage()), () => true);
            this.NextPageCommand = new RelayCommand(() => Task.Run(() => this.NextPage()), () => true);
            this.AddAddressCommand = new RelayCommand<ScanResult>((address) => Task.Run(() => this.AddAddress(address)), (address) => true);
            this.ScanResultsObservers = new List<IScanResultsObserver>();
            this.ObserverLock = new Object();
            this.ActiveType = typeof(Int32);
            this.addresses = new ObservableCollection<ScanResult>();

            SnapshotManager.GetInstance().Subscribe(this);
            MainViewModel.GetInstance().Subscribe(this);

            this.UpdateScanResults();
        }

        /// <summary>
        /// Gets the command to change the active data type to SByte.
        /// </summary>
        public ICommand ChangeTypeSByteCommand { get; private set; }

        /// <summary>
        /// Gets the command to change the active data type to Int16.
        /// </summary>
        public ICommand ChangeTypeInt16Command { get; private set; }

        /// <summary>
        /// Gets the command to change the active data type to Int32.
        /// </summary>
        public ICommand ChangeTypeInt32Command { get; private set; }

        /// <summary>
        /// Gets the command to change the active data type to Int64.
        /// </summary>
        public ICommand ChangeTypeInt64Command { get; private set; }

        /// <summary>
        /// Gets the command to change the active data type to Byte.
        /// </summary>
        public ICommand ChangeTypeByteCommand { get; private set; }

        /// <summary>
        /// Gets the command to change the active data type to UInt16.
        /// </summary>
        public ICommand ChangeTypeUInt16Command { get; private set; }

        /// <summary>
        /// Gets the command to change the active data type to UInt32.
        /// </summary>
        public ICommand ChangeTypeUInt32Command { get; private set; }

        /// <summary>
        /// Gets the command to change the active data type to UInt64.
        /// </summary>
        public ICommand ChangeTypeUInt64Command { get; private set; }

        /// <summary>
        /// Gets the command to change the active data type to Single.
        /// </summary>
        public ICommand ChangeTypeSingleCommand { get; private set; }

        /// <summary>
        /// Gets the command to change the active data type to Double.
        /// </summary>
        public ICommand ChangeTypeDoubleCommand { get; private set; }

        /// <summary>
        /// Gets the command to go to the first page.
        /// </summary>
        public ICommand FirstPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to go to the last page.
        /// </summary>
        public ICommand LastPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to go to the previous page.
        /// </summary>
        public ICommand PreviousPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to go to the next page.
        /// </summary>
        public ICommand NextPageCommand { get; private set; }

        /// <summary>
        /// Gets the command to select a target process.
        /// </summary>
        public ICommand AddAddressCommand { get; private set; }

        /// <summary>
        /// Gets or sets the active scan results data type.
        /// </summary>
        public Type ActiveType
        {
            get
            {
                return this.activeType;
            }

            set
            {
                this.activeType = value;
                this.NotifyObservers();
                this.RaisePropertyChanged(nameof(this.ActiveType));
                this.RaisePropertyChanged(nameof(this.ActiveTypeName));
                this.RaisePropertyChanged(nameof(this.ActiveTypeImage));
            }
        }

        /// <summary>
        /// Gets the name associated with the active data type.
        /// </summary>
        public String ActiveTypeName
        {
            get
            {
                switch (Type.GetTypeCode(this.ActiveType))
                {
                    case TypeCode.SByte:
                        return "SByte";
                    case TypeCode.Int16:
                        return "Int16";
                    case TypeCode.Int32:
                        return "Int32";
                    case TypeCode.Int64:
                        return "Int64";
                    case TypeCode.Byte:
                        return "Byte";
                    case TypeCode.UInt16:
                        return "UInt16";
                    case TypeCode.UInt32:
                        return "UInt32";
                    case TypeCode.UInt64:
                        return "UInt64";
                    case TypeCode.Single:
                        return "Single";
                    case TypeCode.Double:
                        return "Double";
                    default:
                        return "Invalid Type";
                }
            }
        }

        /// <summary>
        /// Gets the image associated with the active data type.
        /// </summary>
        public BitmapSource ActiveTypeImage
        {
            get
            {
                switch (Type.GetTypeCode(this.ActiveType))
                {
                    case TypeCode.SByte:
                        return Images.BlueBlocks1;
                    case TypeCode.Int16:
                        return Images.BlueBlocks2;
                    case TypeCode.Int32:
                        return Images.BlueBlocks4;
                    case TypeCode.Int64:
                        return Images.BlueBlocks8;
                    case TypeCode.Byte:
                        return Images.PurpleBlocks1;
                    case TypeCode.UInt16:
                        return Images.PurpleBlocks2;
                    case TypeCode.UInt32:
                        return Images.PurpleBlocks4;
                    case TypeCode.UInt64:
                        return Images.PurpleBlocks8;
                    case TypeCode.Single:
                        return Images.OrangeBlocks4;
                    case TypeCode.Double:
                        return Images.OrangeBlocks8;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the total number of addresses found.
        /// </summary>
        public Int64 CurrentPage
        {
            get
            {
                return this.currentPage;
            }

            set
            {
                this.currentPage = value;
                this.LoadScanResults();
                this.RaisePropertyChanged(nameof(this.CurrentPage));
            }
        }

        /// <summary>
        /// Gets the total number of addresses found.
        /// </summary>
        public Int64 PageCount
        {
            get
            {
                return this.ResultCount / ScanResultsViewModel.PageSize;
            }
        }

        /// <summary>
        /// Gets or sets the total number of bytes found.
        /// </summary>
        public Int64 ByteCount
        {
            get
            {
                return this.byteCount;
            }

            set
            {
                this.byteCount = value;
                this.RaisePropertyChanged(nameof(this.ByteCount));
            }
        }

        /// <summary>
        /// Gets or sets the total number of addresses found.
        /// </summary>
        public Int64 ResultCount
        {
            get
            {
                return this.addressCount;
            }

            set
            {
                this.addressCount = value;
                this.RaisePropertyChanged(nameof(this.ResultCount));
                this.RaisePropertyChanged(nameof(this.PageCount));
            }
        }

        /// <summary>
        /// Gets the address elements.
        /// </summary>
        public ObservableCollection<ScanResult> Addresses
        {
            get
            {
                return this.addresses;
            }
        }

        /// <summary>
        /// Gets or sets the lock for accessing observers.
        /// </summary>
        private Object ObserverLock { get; set; }

        /// <summary>
        /// Gets or sets objects observing changes in the active snapshot.
        /// </summary>
        private List<IScanResultsObserver> ScanResultsObservers { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ScanResultsViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static ScanResultsViewModel GetInstance()
        {
            return ScanResultsViewModel.scanResultsViewModelInstance.Value;
        }

        /// <summary>
        /// Subscribes the given object to changes in the active snapshot.
        /// </summary>
        /// <param name="snapshotObserver">The object to observe active snapshot changes.</param>
        public void Subscribe(IScanResultsObserver snapshotObserver)
        {
            lock (this.ObserverLock)
            {
                if (!this.ScanResultsObservers.Contains(snapshotObserver))
                {
                    this.ScanResultsObservers.Add(snapshotObserver);
                    snapshotObserver.Update(this.ActiveType);
                }
            }
        }

        /// <summary>
        /// Unsubscribes the given object from changes in the active snapshot.
        /// </summary>
        /// <param name="snapshotObserver">The object to observe active snapshot changes.</param>
        public void Unsubscribe(IScanResultsObserver snapshotObserver)
        {
            lock (this.ObserverLock)
            {
                if (this.ScanResultsObservers.Contains(snapshotObserver))
                {
                    this.ScanResultsObservers.Remove(snapshotObserver);
                }
            }
        }

        /// <summary>
        /// Recieves an update of the active snapshot.
        /// </summary>
        /// <param name="snapshot">The active snapshot.</param>
        public void Update(Snapshot snapshot)
        {
            this.ResultCount = snapshot == null ? 0 : snapshot.GetElementCount();
            this.ByteCount = snapshot == null ? 0 : snapshot.GetByteCount();
            this.CurrentPage = 0;
        }

        /// <summary>
        /// Loads the results for the current page.
        /// </summary>
        private void LoadScanResults()
        {
            Snapshot snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(createIfNone: false);
            ObservableCollection<ScanResult> newAddresses = new ObservableCollection<ScanResult>();

            if (snapshot == null)
            {
                this.addresses = newAddresses;
                this.RaisePropertyChanged(nameof(this.Addresses));
                return;
            }

            Int64 startIndex = Math.Min(ScanResultsViewModel.PageSize * this.CurrentPage, snapshot.GetElementCount());
            Int64 endIndex = Math.Min((ScanResultsViewModel.PageSize * this.CurrentPage) + ScanResultsViewModel.PageSize, snapshot.GetElementCount());

            for (Int64 index = startIndex; index < endIndex; index++)
            {
                SnapshotElementRef element = snapshot[index];

                String label = String.Empty;

                if (element.GetElementLabel() != null)
                {
                    label = element.GetElementLabel().ToString();
                }

                String currentValue = String.Empty;
                if (element.HasCurrentValue())
                {
                    currentValue = element.GetCurrentValue().ToString();
                }

                String previousValue = String.Empty;
                if (element.HasPreviousValue())
                {
                    previousValue = element.GetPreviousValue().ToString();
                }

                newAddresses.Add(new ScanResult(element.BaseAddress, currentValue, previousValue, label));
            }

            this.addresses = newAddresses;
            this.RaisePropertyChanged(nameof(this.Addresses));
        }

        /// <summary>
        /// Updates the values for the current scan results.
        /// </summary>
        private void UpdateScanResults()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Boolean readSuccess;
                    this.Addresses.ForEach(x => x.ElementValue = EngineCore.GetInstance().OperatingSystemAdapter.Read(this.ActiveType, x.ElementAddress, out readSuccess).ToString());
                    this.RaisePropertyChanged(nameof(this.Addresses));
                    Thread.Sleep(SettingsViewModel.GetInstance().ResultReadInterval);
                }
            });
        }

        /// <summary>
        /// Changes the active scan results type.
        /// </summary>
        /// <param name="newType">The new scan results type.</param>
        private void ChangeType(Type newType)
        {
            this.ActiveType = newType;
        }

        /// <summary>
        /// Goes to the first page of results.
        /// </summary>
        private void FirstPage()
        {
            this.CurrentPage = 0;
        }

        /// <summary>
        /// Goes to the last page of results.
        /// </summary>
        private void LastPage()
        {
            this.CurrentPage = this.PageCount;
        }

        /// <summary>
        /// Goes to the previous page of results.
        /// </summary>
        private void PreviousPage()
        {
            this.CurrentPage = this.CurrentPage == 0 ? this.CurrentPage : this.CurrentPage - 1;
        }

        /// <summary>
        /// Goes to the next page of results.
        /// </summary>
        private void NextPage()
        {
            this.CurrentPage = this.CurrentPage >= this.PageCount ? this.CurrentPage : this.CurrentPage + 1;
        }

        /// <summary>
        /// Adds the given scan result address to the project explorer.
        /// </summary>
        /// <param name="scanResult">The scan result to add to the project explorer.</param>
        private void AddAddress(ScanResult scanResult)
        {
            ProjectExplorerViewModel.GetInstance().AddSpecificAddressItem(scanResult.ElementAddress, this.ActiveType);
        }

        /// <summary>
        /// Notify all observing objects of an active type change.
        /// </summary>
        private void NotifyObservers()
        {
            lock (this.ObserverLock)
            {
                foreach (IScanResultsObserver observer in this.ScanResultsObservers)
                {
                    observer.Update(this.ActiveType);
                }
            }
        }
    }
    //// End class
}
//// End namespace