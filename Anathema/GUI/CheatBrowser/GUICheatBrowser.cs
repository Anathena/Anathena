using Anathema.Source.Utils;
using Anathema.Utils.Browser;
using Anathema.Utils.MVP;
using Gecko;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUICheatBrowser : DockContent
    {
        // private ChromiumWebBrowser Browser;
        private const String AnathemaCheatBrowseURL = "www.anathemaengine.com/browser.php";
        private const String AnathemaCheatUploadURL = "www.anathemaengine.com/upload.php";

        private GeckoWebBrowser Browser;
        private Object AccessLock;

        public GUICheatBrowser()
        {
            InitializeComponent();

            // Initialize presenter
            // (No presenter currently, since the browser does all the work)
            AccessLock = new Object();

            WindowState = FormWindowState.Maximized;
        }

        // Browser initialization done on load event rather than constructor to reduce visual lag
        private void GUICheatBrowser_Load(Object Sender, EventArgs E)
        {
            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            using (TimedLock.Lock(AccessLock))
            {
                BrowserHelper.GetInstance().InitializeBrowserStatic();

                ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
                {
                    Browser = new GeckoWebBrowser();
                    Browser.Navigate(AnathemaCheatBrowseURL);
                    Browser.Dock = DockStyle.Fill;
                    ContentPanel.Controls.Add(Browser);
                });
            }
        }

        #region Events

        private void HomeButton_Click(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                Browser.Navigate(AnathemaCheatBrowseURL);
            }
        }

        private void UploadButton_Click(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                Browser.Navigate(AnathemaCheatUploadURL);
            }
        }

        private void BackButton_Click(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Browser.CanGoBack)
                    Browser.GoBack();
            }
        }

        private void ForwardButton_Click(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Browser.CanGoForward)
                    Browser.GoForward();
            }
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