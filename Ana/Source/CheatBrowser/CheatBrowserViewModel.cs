namespace Ana.Source.CheatBrowser
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Threading;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Cheat Browser.
    /// </summary>
    internal class CheatBrowserViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(CheatBrowserViewModel);

        /// <summary>
        /// The home url for the cheat browser.
        /// </summary>
        private const String HomeUrl = "http://www.anathena.com/CheatBrowser/Index";

        /// <summary>
        /// Singleton instance of the <see cref="CheatBrowserViewModel" /> class.
        /// </summary>
        private static Lazy<CheatBrowserViewModel> cheatBrowserViewModelInstance = new Lazy<CheatBrowserViewModel>(
                () => { return new CheatBrowserViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="CheatBrowserViewModel" /> class from being created.
        /// </summary>
        private CheatBrowserViewModel() : base("Cheat Browser")
        {
            this.ContentId = CheatBrowserViewModel.ToolContentId;

            // Note: Cannot be async, navigation must take place on the same thread as GUI
            this.NavigateHomeCommand = new RelayCommand<WebBrowser>((browser) => this.NavigateHome(browser), (browser) => true);

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets the command to navigate home.
        /// </summary>
        public ICommand NavigateHomeCommand { get; private set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="CheatBrowserViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static CheatBrowserViewModel GetInstance()
        {
            return cheatBrowserViewModelInstance.Value;
        }

        /// <summary>
        /// Navigates home in the browser.
        /// </summary>
        /// <param name="browser">The web browser.</param>
        private void NavigateHome(WebBrowser browser)
        {
            browser.Navigate(HomeUrl);
        }
    }
    //// End class
}
//// End namespace