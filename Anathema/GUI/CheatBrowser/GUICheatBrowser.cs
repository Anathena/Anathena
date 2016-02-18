using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Gecko;

namespace Anathema
{
    public partial class GUICheatBrowser : DockContent
    {
        // private ChromiumWebBrowser Browser;
        private const String AnathemaCheatBrowseURL = "www.anathemaengine.com/browser.php";
        private const String AnathemaCheatUploadURL = "www.anathemaengine.com/upload.php";

        private GeckoWebBrowser Browser;

        public GUICheatBrowser()
        {
            InitializeComponent();

            // Initialize presenter
            // (No presenter currently, since the browser does all the work)

            WindowState = FormWindowState.Maximized;
        }

        private void GUICheatBrowser_Load(Object Sender, EventArgs E)
        {
            // Initialize browser after load to reduce lag before window appears
            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            if (OSInterface.IsAnathema32Bit())
                Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "xulrunner-32"));

            if (OSInterface.IsAnathema64Bit())
                Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "xulrunner-64"));

            Browser = new GeckoWebBrowser();
            Browser.Navigate(AnathemaCheatBrowseURL);
            Browser.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(Browser);

            LauncherDialog.Download += LauncherDialog_Download;
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

        private void HomeButton_Click(Object Sender, EventArgs E)
        {
            Browser.Navigate(AnathemaCheatBrowseURL);
        }

        private void UploadButton_Click(Object Sender, EventArgs E)
        {
            Browser.Navigate(AnathemaCheatUploadURL);
        }

        private void BackButton_Click(Object Sender, EventArgs E)
        {
            if (Browser.CanGoBack)
                Browser.GoBack();
        }

        private void ForwardButton_Click(Object Sender, EventArgs E)
        {
            if (Browser.CanGoForward)
                Browser.GoForward();
        }

        #endregion
        /*
        internal class DownloadHandler : IDownloadHandler
        {
            public void OnDownloadUpdated(IBrowser Browser, DownloadItem DownloadItem, IDownloadItemCallback Callback) { }
            public void OnBeforeDownload(IBrowser Browser, DownloadItem DownloadItem, IBeforeDownloadCallback Callback)
            {
                if (!Callback.IsDisposed)
                {
                    using (Callback)
                    {
                        Callback.Continue(DownloadItem.SuggestedFileName, true);
                    }
                }
            }

        } // End class*/

    } // End class

} // End namespace