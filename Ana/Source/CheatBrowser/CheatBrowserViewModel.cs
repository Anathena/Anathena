namespace Ana.Source.CheatBrowser
{
    using Docking;
    using Engine;
    using Gecko;
    using Main;
    using Microsoft.Win32;
    using Mvvm.Command;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Input;

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
        /// The home url for the cheat browser
        /// </summary>
        private const String HomeUrl = "http://www.anathena.com/browser/browser.php";

        /// <summary>
        /// The filter to use for the save file dialog
        /// </summary>
        private const String ExtensionFilter = "Cheat File(*.Hax)|*.hax|All files(*.*)|*.*";

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
            this.ContentId = CheatBrowserViewModel.ToolContentId;
            this.NavigateHomeCommand = new RelayCommand<GeckoWebBrowser>((browser) => this.NavigateHome(browser), (browser) => true);

            // Initialize engine for Gecko Fx web browser
            if (EngineCore.GetInstance().OperatingSystemAdapter.IsAnathena32Bit())
            {
                Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "xulrunner-32"));
            }
            else if (EngineCore.GetInstance().OperatingSystemAdapter.IsAnathena64Bit())
            {
                Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "xulrunner-64"));
            }

            LauncherDialog.Download += this.LauncherDialogDownload;
            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets the command to navigate home
        /// </summary>
        public ICommand NavigateHomeCommand { get; private set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="CheatBrowserViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static CheatBrowserViewModel GetInstance()
        {
            return cheatBrowserViewModelInstance.Value;
        }

        /// <summary>
        /// Navigates home in the browser
        /// </summary>
        /// <param name="browser">The Gecko Fx web browser</param>
        private void NavigateHome(GeckoWebBrowser browser)
        {
            browser.Navigate(HomeUrl);
        }

        /// <summary>
        /// Handle save file dialog for downloads. Code ripped from the internet, no idea how it works
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event parameters</param>
        private void LauncherDialogDownload(Object sender, LauncherDialogEvent e)
        {
            nsILocalFile objectTarget = Xpcom.CreateInstance<nsILocalFile>("@mozilla.org/file/local;1");
            Stream saveStream;

            String fileName;
            String fileDirectory;

            using (nsAString nsaString = new nsAString(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\temp.tmp"))
            {
                objectTarget.InitWithPath(nsaString);
            }

            // Allow user to select the save location
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ExtensionFilter;
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = e.Filename;

            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }

            fileDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
            fileName = Path.GetFileName(saveFileDialog.FileName);

            if ((saveStream = File.OpenWrite(Path.Combine(fileDirectory, fileName))) == null)
            {
                return;
            }

            nsIURI source = IOService.CreateNsIUri(e.Url);
            nsIURI destination = IOService.CreateNsIUri(new Uri(Path.Combine(fileDirectory, fileName)).AbsoluteUri);
            nsAStringBase stringBase = new nsAString(Path.GetFileName(fileName));

            nsIWebBrowserPersist persist = Xpcom.CreateInstance<nsIWebBrowserPersist>("@mozilla.org/embedding/browser/nsWebBrowserPersist;1");
            nsIDownloadManager downloadMan = null;
            downloadMan = Xpcom.CreateInstance<nsIDownloadManager>("@mozilla.org/download-manager;1");
            nsIDownload download = downloadMan.AddDownload(0, source, destination, stringBase, e.Mime, 0, null, persist, false);

            if (download != null)
            {
                persist.SetPersistFlagsAttribute(2 | 32 | 16384);
                persist.SetProgressListenerAttribute(download);
                persist.SaveURI(source, null, null, null, null, (nsISupports)destination, null);
            }

            saveStream.Flush();
            saveStream.Close();
        }
    }
    //// End class
}
//// End namespace