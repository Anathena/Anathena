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

        /// <summary>
        /// Gets or sets an object that contains the target process signature.
        /// </summary>
        public SignatureModel SignatureModel { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="SignatureCollectorViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static SignatureCollectorViewModel GetInstance()
        {
            return SignatureCollectorViewModel.signatureCollectorViewModelInstance.Value;
        }

        /// <summary>
        /// Collects the signature of the running target application.
        /// </summary>
        private void CollectSignature()
        {
            this.SignatureModel.WindowTitle = EngineCore.GetInstance().OperatingSystemAdapter.CollectWindowTitle();
            this.SignatureModel.BinaryVersion = EngineCore.GetInstance().OperatingSystemAdapter.CollectBinaryVersion();
            this.SignatureModel.BinaryHeaderHash = EngineCore.GetInstance().OperatingSystemAdapter.CollectBinaryHeaderHash();
            this.SignatureModel.BinaryImportHash = EngineCore.GetInstance().OperatingSystemAdapter.CollectBinaryImportHash();
            this.SignatureModel.MainModuleHash = EngineCore.GetInstance().OperatingSystemAdapter.CollectMainModuleHash();
            this.SignatureModel.EmulatorHash = EngineCore.GetInstance().OperatingSystemAdapter.CollectEmulatorHash();

            this.RaisePropertyChanged(nameof(this.Signature));
        }
    }
    //// End class
}
//// End namespace