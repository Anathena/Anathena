using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using CefSharp.WinForms;
using CefSharp;
using CefSharp.WinForms.Internals;

namespace Anathema
{
    public partial class GUICheatBrowser : DockContent
    {
        private ChromiumWebBrowser Browser;
        private const String AnathemaCheatBrowseURL = "www.anathemaengine.com/browser.php";
        private const String AnathemaCheatUploadURL = "www.anathemaengine.com/upload.php";

        public GUICheatBrowser()
        {
            InitializeComponent();

            // Initialize presenter

            WindowState = FormWindowState.Maximized;
        }

        private void GUICheatBrowser_Load(Object Sender, EventArgs E)
        {
            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            Browser = new ChromiumWebBrowser(AnathemaCheatBrowseURL);
            Browser.DownloadHandler = new DownloadHandler();

            ContentPanel.Controls.Add(Browser);
        }

        private void LoadUrl(String URL)
        {
            if (!Uri.IsWellFormedUriString(URL, UriKind.RelativeOrAbsolute))
                return;

            Browser.Load(URL);
        }

        #region Events

        private void HomeButton_Click(Object Sender, EventArgs E)
        {
            Browser.Load(AnathemaCheatBrowseURL);
        }

        private void UploadButton_Click(Object Sender, EventArgs E)
        {
            Browser.Load(AnathemaCheatUploadURL);
        }

        private void BackButton_Click(Object Sender, EventArgs E)
        {
            if (Browser.CanGoBack)
                Browser.Back();
        }

        private void ForwardButton_Click(Object Sender, EventArgs E)
        {
            if (Browser.CanGoForward)
                Browser.Forward();
        }

        #endregion

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

        } // End class

    } // End class

} // End namespace