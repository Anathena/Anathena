namespace Ana.View
{
    using System;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    /// <summary>
    /// Interaction logic for CheatBrowser.xaml
    /// </summary>
    internal partial class CheatBrowser : UserControl
    {
        /// <summary>
        /// The file extension for cheat files
        /// </summary>
        private const String FileExtension = ".hax";

        /// <summary>
        /// The subdirectory to store downloaded cheat files
        /// </summary>
        private const String FileStorageDirectoryName = "Cheats";

        /// <summary>
        /// The path to all user application files
        /// </summary>
        private static readonly String ApplicationFiles = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        /// The complete directory to store saved cheat files
        /// </summary>
        private static readonly String SavePath = Path.Combine(Path.Combine(ApplicationFiles, Assembly.GetExecutingAssembly().GetName().Name), CheatBrowser.FileStorageDirectoryName);

        /// <summary>
        /// Initializes a new instance of the <see cref="CheatBrowser" /> class
        /// </summary>
        public CheatBrowser()
        {
            this.InitializeComponent();

            this.browser.Navigating += this.BrowserNavigating;
        }

        /// <summary>
        /// Invoked when the browser is about to navigate to a new page
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Cancel event args</param>
        private void BrowserNavigating(Object sender, NavigatingCancelEventArgs e)
        {
            if (e == null || e.Uri == null || e.Uri.AbsoluteUri == null)
            {
                return;
            }

            try
            {
                if (e.Uri.AbsoluteUri.ToLower().EndsWith(FileExtension))
                {
                    e.Cancel = true;

                    WebClient client = new WebClient();
                    client.DownloadDataCompleted += (source, args) => this.DownloadDataCompleted(source, args, Path.GetFileName(e.Uri.AbsoluteUri));
                    client.DownloadDataAsync(e.Uri);
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Invoked when a download is complete, before the bytes are written
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Download event args</param>
        /// <param name="fileName">The name of the file being downloaded</param>
        private void DownloadDataCompleted(Object sender, DownloadDataCompletedEventArgs e, String fileName)
        {
            try
            {
                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }

                String saveLocation = Path.Combine(SavePath, fileName);
                File.WriteAllBytes(saveLocation, e.Result);
            }
            catch
            {
                return;
            }
        }
    }
    //// End class
}
//// End namespace