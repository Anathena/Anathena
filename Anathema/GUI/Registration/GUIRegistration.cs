using Gecko;
using System;
using System.IO;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIRegistration : Form
    {
        private const String AnathemaRegisterURL = "www.anathemaengine.com/account.php?client=true";

        private GeckoWebBrowser Browser;

        public GUIRegistration()
        {
            InitializeComponent();

            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            BrowserHelper.GetInstance().InitializeXpcom();

            Browser = new GeckoWebBrowser();
            Browser.Navigate(AnathemaRegisterURL);
            Browser.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(Browser);

            LauncherDialog.Download += LauncherDialog_Download;

            Browser.Navigated += Browser_Navigated;
        }

        #region Events

        private void Browser_Navigated(Object Sender, GeckoNavigatedEventArgs E)
        {

        }

        /// <summary>
        ///  TODO: Change this to read in downloaded serial code
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void LauncherDialog_Download(Object Sender, LauncherDialogEvent E)
        {
            nsILocalFile ObjectTarget = Xpcom.CreateInstance<nsILocalFile>("@mozilla.org/file/local;1");

            using (nsAString nsAString = new nsAString(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\temp.tmp"))
            {
                ObjectTarget.InitWithPath(nsAString);
            }

            nsIURI Source = IOService.CreateNsIUri(E.Url);
            nsIURI Destination = IOService.CreateNsIUri(new Uri("FILENAMEHERE!!!!").AbsoluteUri);
            nsAStringBase StringBase = new nsAString(System.IO.Path.GetFileName("FILENAMEHERE!!!!"));

            nsIWebBrowserPersist Persist = Xpcom.CreateInstance<nsIWebBrowserPersist>("@mozilla.org/embedding/browser/nsWebBrowserPersist;1");
            nsIDownloadManager DownloadMan = null;
            DownloadMan = Xpcom.CreateInstance<nsIDownloadManager>("@mozilla.org/download-manager;1");
            nsIDownload Download = DownloadMan.AddDownload(0, Source, Destination, StringBase, E.Mime, 0, null, (nsICancelable)Persist, false);

            if (Download != null)
            {
                Persist.SetPersistFlagsAttribute(2 | 32 | 16384);
                Persist.SetProgressListenerAttribute((nsIWebProgressListener)Download);
                Persist.SaveURI(Source, null, null, null, null, (nsISupports)Destination, null);
            }
        }

        #endregion

    } // End class

} // End namespace