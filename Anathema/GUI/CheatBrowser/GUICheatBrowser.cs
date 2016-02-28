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
            BrowserHelper.GetInstance().InitializeBrowserStatic();

            Browser = new GeckoWebBrowser();
            Browser.Navigate(AnathemaCheatBrowseURL);
            Browser.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(Browser);
        }

        #region Events
        
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