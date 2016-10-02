namespace Ana.Source.ManualScanner
{
    using Docking;
    using Main;
    using System;
    using System.Threading;

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
        /// Prevents a default instance of the <see cref="ManualScannerViewModel" /> class from being created
        /// </summary>
        private ManualScannerViewModel() : base("Manual Scanner")
        {
            this.ContentId = ToolContentId;
            this.IsVisible = true;

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ManualScannerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ManualScannerViewModel GetInstance()
        {
            return manualScannerViewModelInstance.Value;
        }
    }
    //// End class
}
//// End namespace