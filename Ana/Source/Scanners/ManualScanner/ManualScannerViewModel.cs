namespace Ana.Source.Scanners.ManualScanner
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using Results.ScanResults;
    using ScanConstraints;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// View model for the Manual Scanner
    /// </summary>
    internal class ManualScannerViewModel : ToolViewModel, IScanResultsObserver
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(ManualScannerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="ManualScannerViewModel" /> class
        /// </summary>
        private static Lazy<ManualScannerViewModel> manualScannerViewModelInstance = new Lazy<ManualScannerViewModel>(
                () => { return new ManualScannerViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The scan constraint currently selected
        /// </summary>
        private ScanConstraint selectedScanConstraint;

        /// <summary>
        /// Manager for all active scan constraints
        /// </summary>
        private ScanConstraintManager scanConstraintManager;

        /// <summary>
        /// Prevents a default instance of the <see cref="ManualScannerViewModel" /> class from being created
        /// </summary>
        private ManualScannerViewModel() : base("Manual Scanner")
        {
            this.ContentId = ManualScannerViewModel.ToolContentId;
            this.IsVisible = true;
            this.StartScanCommand = new RelayCommand(() => Task.Run(() => this.StartScan()), () => true);

            // Note: Not async to avoid updates slower than the perception threshold
            this.UpdateActiveValueCommand = new RelayCommand<dynamic>((newValue) => this.UpdateActiveValue(newValue), (newValue) => true);
            this.SelectChangedCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.Changed), () => true);
            this.SelectDecreasedCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.Decreased), () => true);
            this.SelectDecreasedByXCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.DecreasedByX), () => true);
            this.SelectEqualCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.Equal), () => true);
            this.SelectGreaterThanCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.GreaterThan), () => true);
            this.SelectGreaterThanOrEqualCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.GreaterThanOrEqual), () => true);
            this.SelectIncreasedCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.Increased), () => true);
            this.SelectIncreasedByXCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.IncreasedByX), () => true);
            this.SelectLessThanCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.LessThan), () => true);
            this.SelectLessThanOrEqualCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.LessThanOrEqual), () => true);
            this.SelectNotEqualCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.NotEqual), () => true);
            this.SelectNotScientificNotationCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.NotScientificNotation), () => true);
            this.SelectUnchangedCommand = new RelayCommand(() => this.ChangeScanConstraintSelection(ConstraintsEnum.Unchanged), () => true);

            // Note: Constraint modifying commands cannot be async since they modify the observable collection, which must be done on the same thread as the GUI
            this.AddSelectedConstraintCommand = new RelayCommand(() => this.AddSelectedConstraint(), () => true);
            this.RemoveSelectedConstraintCommand = new RelayCommand(() => this.RemoveSelectedConstraint(), () => true);
            this.ClearConstraintsCommand = new RelayCommand(() => this.ClearConstraints(), () => true);
            this.SelectedScanConstraint = new ScanConstraint(ConstraintsEnum.Equal);
            this.ManualScannerModel = new ManualScannerModel();
            this.ScanConstraintManager = new ScanConstraintManager();
            this.ScanConstraintManager.SetElementType(typeof(Int32));

            Task.Run(() => ScanResultsViewModel.GetInstance().Subscribe(this));
            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets the command begin the scan
        /// </summary>
        public ICommand StartScanCommand { get; private set; }

        /// <summary>
        /// Gets the command to update the value of the active scan constraint
        /// </summary>
        public ICommand UpdateActiveValueCommand { get; private set; }

        /// <summary>
        /// Gets the command to add the selected constraint to the list of scan constraints
        /// </summary>
        public ICommand AddSelectedConstraintCommand { get; private set; }

        /// <summary>
        /// Gets the command to remove the selected constraint to the list of scan constraints
        /// </summary>
        public ICommand RemoveSelectedConstraintCommand { get; private set; }

        /// <summary>
        /// Gets the command to clear all added constraints
        /// </summary>
        public ICommand ClearConstraintsCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.Changed"/> constraint
        /// </summary>
        public ICommand SelectChangedCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.Decreased"/> constraint
        /// </summary>
        public ICommand SelectDecreasedCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.DecreasedByX"/> constraint
        /// </summary>
        public ICommand SelectDecreasedByXCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.Equal"/> constraint
        /// </summary>
        public ICommand SelectEqualCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.GreaterThan"/> constraint
        /// </summary>
        public ICommand SelectGreaterThanCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.GreaterThanOrEqual"/> constraint
        /// </summary>
        public ICommand SelectGreaterThanOrEqualCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.Increased"/> constraint
        /// </summary>
        public ICommand SelectIncreasedCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.IncreasedByX"/> constraint
        /// </summary>
        public ICommand SelectIncreasedByXCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.Invalid"/> constraint
        /// </summary>
        public ICommand SelectInvalidCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.LessThan"/> constraint
        /// </summary>
        public ICommand SelectLessThanCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.LessThanOrEqual"/> constraint
        /// </summary>
        public ICommand SelectLessThanOrEqualCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.NotEqual"/> constraint
        /// </summary>
        public ICommand SelectNotEqualCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.NotScientificNotation"/> constraint
        /// </summary>
        public ICommand SelectNotScientificNotationCommand { get; private set; }

        /// <summary>
        /// Gets the command to select the <see cref="ConstraintsEnum.Unchanged"/> constraint
        /// </summary>
        public ICommand SelectUnchangedCommand { get; private set; }

        public ObservableCollection<ScanConstraint> Constraints
        {
            get
            {
                return this.ScanConstraintManager.ValueConstraints;
            }
        }

        public ObservableCollection<ScanConstraint> ActiveScanConstraint
        {
            get
            {
                return new ObservableCollection<ScanConstraint>(new ScanConstraint[] { this.SelectedScanConstraint });
            }
        }

        public ScanConstraintManager ScanConstraintManager
        {
            get
            {
                return this.scanConstraintManager;
            }

            set
            {
                this.scanConstraintManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected scan constraint
        /// </summary>
        public ScanConstraint SelectedScanConstraint
        {
            get
            {
                return this.selectedScanConstraint;
            }

            set
            {
                this.selectedScanConstraint = value;
                this.RaisePropertyChanged(nameof(this.SelectedScanConstraint));
                this.RaisePropertyChanged(nameof(this.ScanConstraintImage));
                this.RaisePropertyChanged(nameof(this.ActiveScanConstraint));
            }
        }

        /// <summary>
        /// Gets the image associated with the selected scan constraint
        /// </summary>
        public BitmapSource ScanConstraintImage
        {
            get
            {
                return this.SelectedScanConstraint.ConstraintImage;
            }
        }

        private ManualScannerModel ManualScannerModel { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ManualScannerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ManualScannerViewModel GetInstance()
        {
            return ManualScannerViewModel.manualScannerViewModelInstance.Value;
        }

        public void Update(Type activeType)
        {
            this.ScanConstraintManager.SetElementType(activeType);
        }

        /// <summary>
        /// Starts the scan using the current constraints
        /// </summary>
        private void StartScan()
        {
            // Create a constraint manager that includes the current active constraint
            ScanConstraintManager allScanConstraints = this.ScanConstraintManager.Clone();
            allScanConstraints.AddConstraint(this.SelectedScanConstraint);

            ManualScannerModel.SetScanConstraintManager(allScanConstraints);
            ManualScannerModel.Begin();
        }

        private Object UpdateActiveValue(dynamic newValue)
        {
            this.SelectedScanConstraint.ConstraintValue = newValue;
            this.RaisePropertyChanged(nameof(this.SelectedScanConstraint));
            this.RaisePropertyChanged(nameof(this.ScanConstraintImage));
            this.RaisePropertyChanged(nameof(this.ActiveScanConstraint));
            return null;
        }

        /// <summary>
        /// Adds the selected constraint to the list of scan constraints
        /// </summary>
        private void AddSelectedConstraint()
        {
            this.ScanConstraintManager.AddConstraint(this.SelectedScanConstraint);
            this.SelectedScanConstraint = new ScanConstraint(this.SelectedScanConstraint.Constraint, this.SelectedScanConstraint.ConstraintValue);
        }

        /// <summary>
        /// Removes the selected constraint from the list of scan constraints
        /// </summary>
        private void RemoveSelectedConstraint()
        {
            this.ScanConstraintManager.RemoveConstraints(new Int32[] { 0 });
        }

        /// <summary>
        /// Clears all scan constraints
        /// </summary>
        private void ClearConstraints()
        {
            this.ScanConstraintManager.ClearConstraints();
        }

        /// <summary>
        /// Changes the selected scan constraint
        /// </summary>
        /// <param name="constraint">The new scan constraint</param>
        private void ChangeScanConstraintSelection(ConstraintsEnum constraint)
        {
            this.SelectedScanConstraint.Constraint = constraint;
            this.RaisePropertyChanged(nameof(this.SelectedScanConstraint));
            this.RaisePropertyChanged(nameof(this.ScanConstraintImage));
            this.RaisePropertyChanged(nameof(this.ActiveScanConstraint));
        }
    }
    //// End class
}
//// End namespace