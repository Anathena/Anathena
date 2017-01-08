namespace Ana.Source.SignatureCollector
{
    using Docking;
    using Engine;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Signature Collector.
    /// </summary>
    internal class SignatureCollectorViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(SignatureCollectorViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="SignatureCollectorViewModel" /> class.
        /// </summary>
        private static Lazy<SignatureCollectorViewModel> signatureCollectorViewModelInstance = new Lazy<SignatureCollectorViewModel>(
                () => { return new SignatureCollectorViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="SignatureCollectorViewModel" /> class from being created.
        /// </summary>
        private SignatureCollectorViewModel() : base("Signature Collector")
        {
            this.ContentId = SignatureCollectorViewModel.ToolContentId;
            this.CollectSignatureCommand = new RelayCommand(() => Task.Run(() => this.CollectSignature()), () => true);
            this.SignatureModel = new SignatureModel();

            MainViewModel.GetInstance().Subscribe(this);
        }

        public ICommand CollectSignatureCommand { get; private set; }

        public String Signature
        {
            get
            {
                return this.SignatureModel.GetFullSignature();
            }
        }

        public String WindowTitle
        {
            get
            {
                return this.SignatureModel.WindowTitle;
            }

            set
            {
                this.SignatureModel.WindowTitle = value;
                this.RaisePropertyChanged(nameof(this.WindowTitle));
            }
        }

        public String BinaryVersion
        {
            get
            {
                return this.SignatureModel.BinaryVersion;
            }

            set
            {
                this.SignatureModel.BinaryVersion = value;
                this.RaisePropertyChanged(nameof(this.BinaryVersion));
            }
        }

        public String BinaryHeaderHash
        {
            get
            {
                return this.SignatureModel.BinaryHeaderHash;
            }

            set
            {
                this.SignatureModel.BinaryHeaderHash = value;
                this.RaisePropertyChanged(nameof(this.BinaryHeaderHash));
            }
        }

        public String BinaryImportHash
        {
            get
            {
                return this.SignatureModel.BinaryImportHash;
            }

            set
            {
                this.SignatureModel.BinaryImportHash = value;
                this.RaisePropertyChanged(nameof(this.BinaryImportHash));
            }
        }

        public String MainModuleHash
        {
            get
            {
                return this.SignatureModel.MainModuleHash;
            }

            set
            {
                this.SignatureModel.MainModuleHash = value;
                this.RaisePropertyChanged(nameof(this.MainModuleHash));
            }
        }

        public String EmulatorHash
        {
            get
            {
                return this.SignatureModel.EmulatorHash;
            }

            set
            {
                this.SignatureModel.EmulatorHash = value;
                this.RaisePropertyChanged(nameof(this.EmulatorHash));
            }
        }

        private SignatureModel SignatureModel { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="SignatureCollectorViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static SignatureCollectorViewModel GetInstance()
        {
            return SignatureCollectorViewModel.signatureCollectorViewModelInstance.Value;
        }

        private void CollectSignature()
        {
            this.WindowTitle = EngineCore.GetInstance().OperatingSystemAdapter.CollectWindowTitle();
            this.BinaryVersion = EngineCore.GetInstance().OperatingSystemAdapter.CollectBinaryVersion();
            this.BinaryHeaderHash = EngineCore.GetInstance().OperatingSystemAdapter.CollectBinaryHeaderHash();
            this.BinaryImportHash = EngineCore.GetInstance().OperatingSystemAdapter.CollectBinaryImportHash();
            this.MainModuleHash = EngineCore.GetInstance().OperatingSystemAdapter.CollectMainModuleHash();
            this.EmulatorHash = EngineCore.GetInstance().OperatingSystemAdapter.CollectEmulatorHash();

            this.RaisePropertyChanged(nameof(this.Signature));
        }
    }
    //// End class
}
//// End namespace