namespace Ana.Source.CheatBrowser
{
    using Docking;
    using Main;
    using System;
    using System.Threading;

    /// <summary>
    /// View model for the Cheat Browser
    /// </summary>
    internal class CheatBrowserViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(CheatBrowserViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="CheatBrowserViewModel" /> class
        /// </summary>
        private static Lazy<CheatBrowserViewModel> cheatBrowserViewModelInstance = new Lazy<CheatBrowserViewModel>(
                () => { return new CheatBrowserViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="CheatBrowserViewModel" /> class from being created
        /// </summary>
        private CheatBrowserViewModel() : base("Cheat Browser")
        {
            this.ContentId = ToolContentId;

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="CheatBrowserViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static CheatBrowserViewModel GetInstance()
        {
            return cheatBrowserViewModelInstance.Value;
        }
    }
    //// End class
}
//// End namespace