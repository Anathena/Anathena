namespace Ana.Source.ManualScanner
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using Scanners.ScanConstraints;
    using System;
    using System.Threading;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// View model for the Manual Scanner
    /// </summary>
    internal class ManualScannerViewModel : ToolViewModel
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
                LazyThreadSafetyMode.PublicationOnly);

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
            this.StartScanCommand = new RelayCommand(() => this.StartScan(), () => true);
            this.AddSelectedConstraintCommand = new RelayCommand(() => this.AddSelectedConstraint(), () => true);
            this.RemoveSelectedConstraintCommand = new RelayCommand(() => this.RemoveSelectedConstraint(), () => true);
            this.ClearConstraintsCommand = new RelayCommand(() => this.ClearConstraints(), () => true);
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
            this.scanConstraintManager = new ScanConstraintManager();
            this.SelectedScanConstraint = new ScanConstraint(ConstraintsEnum.Equal);
            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets the command begin the scan
        /// </summary>
        public ICommand StartScanCommand { get; private set; }

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

        /// <summary>
        /// Gets a singleton instance of the <see cref="ManualScannerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ManualScannerViewModel GetInstance()
        {
            return ManualScannerViewModel.manualScannerViewModelInstance.Value;
        }

        /// <summary>
        /// Starts the scan using the current constraints
        /// </summary>
        private void StartScan()
        {
        }

        /// <summary>
        /// Adds the selected constraint to the list of scan constraints
        /// </summary>
        private void AddSelectedConstraint()
        {
            this.scanConstraintManager.AddConstraint(this.selectedScanConstraint);
        }

        /// <summary>
        /// Removes the selected constraint from the list of scan constraints
        /// </summary>
        private void RemoveSelectedConstraint()
        {
            this.scanConstraintManager.RemoveConstraints(new Int32[] { 0 });
        }

        /// <summary>
        /// Clears all scan constraints
        /// </summary>
        private void ClearConstraints()
        {
            this.scanConstraintManager.ClearConstraints();
        }

        /// <summary>
        /// Changes the selected scan constraint
        /// </summary>
        /// <param name="constraint">The new scan constraint</param>
        private void ChangeScanConstraintSelection(ConstraintsEnum constraint)
        {
            this.SelectedScanConstraint = new ScanConstraint(constraint);
        }
    }
    //// End class
}
//// End namespace