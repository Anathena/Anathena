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
        private readonly static String ApplicationFiles = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private const String FileExtension = ".hax";

        private const String FileStorageDirectoryName = "Cheats";

        private readonly static String SavePath = Path.Combine(Path.Combine(ApplicationFiles, Assembly.GetExecutingAssembly().GetName().Name), FileStorageDirectoryName);

        /// <summary>
        /// Initializes a new instance of the <see cref="CheatBrowser" /> class
        /// </summary>
        public CheatBrowser()
        {
            this.InitializeComponent();

            this.browser.Navigating += BrowserNavigating;
        }

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
                    client.DownloadDataCompleted += (source, args) => DownloadDataCompleted(source, args, Path.GetFileName(e.Uri.AbsoluteUri));
                    client.DownloadDataAsync(e.Uri);
                }
            }
            catch
            {
                return;
            }
        }

        private void DownloadDataCompleted(Object sender, DownloadDataCompletedEventArgs e, String fileName)
        {
            try
            {
                if (!(Directory.Exists(SavePath)))
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