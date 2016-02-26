using Gecko;
using System;
using System.IO;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIRegistration : Form
    {
        private const String AnathemaRegisterURL = "www.anathemaengine.com/purchase.php";

        private GeckoWebBrowser Browser;

        public GUIRegistration(Boolean IsRegistered, Boolean IsTrial)
        {
            InitializeComponent();

            // Perhaps navigate to registration php page with a GET with Ticks. If not registered and logged in acct is registered,
            // Then we register. If not registered and trial dead, tell them to fuckin pay money

            // Check if already registered
            if (IsRegistered)
            {
                InitializeBrowser("?Registered=1");
                return;
            }

            // Check if already
            if (IsTrial)
            {
                InitializeBrowser("?Trial=0");
                return;
            }

            // Not registered and trial over
            InitializeBrowser("?Trial=0");

            LauncherDialog.Download += LauncherDialog_Download;
            return;
        }

        private void InitializeBrowser(String PHPGet)
        {

            BrowserHelper.GetInstance().InitializeXpcom();

            Browser = new GeckoWebBrowser();
            Browser.Navigate(AnathemaRegisterURL + PHPGet);
            Browser.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(Browser);
        }

        #region Events

        /// <summary>
        ///  Handle save file dialog for downloads. Code ripped from the internet, no idea how it works.
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

            Stream SaveStream;
            SaveFileDialog SaveFileDialog = new SaveFileDialog();

            SaveFileDialog.Filter = "All files (*.*)|*.*";
            SaveFileDialog.FilterIndex = 2;
            SaveFileDialog.RestoreDirectory = true;
            SaveFileDialog.FileName = E.Filename;

            if (SaveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            if ((SaveStream = SaveFileDialog.OpenFile()) == null)
                return;

            nsIURI Source = IOService.CreateNsIUri(E.Url);
            nsIURI Destination = IOService.CreateNsIUri(new Uri(SaveFileDialog.FileName).AbsoluteUri);
            nsAStringBase StringBase = new nsAString(System.IO.Path.GetFileName(SaveFileDialog.FileName));

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

            SaveStream.Close();
        }

        #endregion

    } // End class

} // End namespace