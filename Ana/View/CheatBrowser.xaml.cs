namespace Ana.View
{
    using Source.CheatBrowser;
    using Source.Project;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Navigation;

    /// <summary>
    /// Interaction logic for CheatBrowser.xaml.
    /// </summary>
    internal partial class CheatBrowser : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// The file extension for cheat files.
        /// </summary>
        private const String FileExtension = ".hax";

        /// <summary>
        /// Initializes a new instance of the <see cref="CheatBrowser" /> class.
        /// </summary>
        public CheatBrowser()
        {
            this.InitializeComponent();

            this.browser.Navigating += this.BrowserNavigating;
            this.CheatBrowserViewModel.NavigateHomeCommand.Execute(this.browser);
        }

        /// <summary>
        /// Gets the view model associated with this view.
        /// </summary>
        public CheatBrowserViewModel CheatBrowserViewModel
        {
            get
            {
                return this.DataContext as CheatBrowserViewModel;
            }
        }

        /// <summary>
        /// Invoked when the browser is about to navigate to a new page.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Cancel event args.</param>
        private void BrowserNavigating(Object sender, NavigatingCancelEventArgs e)
        {
            if (e == null || e.Uri == null || e.Uri.AbsoluteUri == null)
            {
                return;
            }

            try
            {
                if (e.Uri.Query.Split('&').Any(x => x.ToLower().EndsWith(FileExtension)))
                {
                    e.Cancel = true;
                    WebClient client = new WebClient();
                    client.DownloadDataCompleted += (source, args) => this.DownloadDataCompleted(source, args);
                    client.DownloadDataAsync(e.Uri);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Event invoked when a download has been completed in the browser.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Download event args.</param>
        private void DownloadDataCompleted(Object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                // Load and import the file.
                String file = Path.GetTempFileName();
                File.WriteAllBytes(file, e.Result);
                ProjectExplorerViewModel.GetInstance().ImportSpecificProjectCommand.Execute(file);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Event for the navigate home menu item click.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void NavigateHomeMenuClick(Object sender, RoutedEventArgs e)
        {
            this.CheatBrowserViewModel.NavigateHomeCommand.Execute(this.browser);
        }
    }
    //// End class
}
//// End namespace