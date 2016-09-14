using Anathena.Source.Utils;
using Anathena.Source.Utils.Browser;
using Anathena.Source.Utils.MVP;
using Gecko;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools
{
    public partial class GUICheatBrowser : DockContent
    {
        // private ChromiumWebBrowser Browser;
        private const String anathenaCheatBrowseURL = "http://www.anathena.com/browser/browser.php";

        private GeckoWebBrowser browser;
        private Object accessLock;

        public GUICheatBrowser()
        {
            InitializeComponent();

            // Initialize presenter
            // (No presenter currently, since the browser object does all the work)
            accessLock = new Object();

            WindowState = FormWindowState.Maximized;
        }

        // Browser initialization done on load event rather than constructor to reduce visual lag
        private void GUICheatBrowser_Load(Object sender, EventArgs e)
        {
            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            BrowserHelper.GetInstance().InitializeBrowserStatic();

            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    browser = new GeckoWebBrowser();
                    browser.Navigate(anathenaCheatBrowseURL);
                    browser.Dock = DockStyle.Fill;
                    ContentPanel.Controls.Add(browser);
                }
            });
        }

        #region Events

        private void BackButton_Click(Object sender, EventArgs e)
        {
            ControlThreadingHelper.InvokeControlAction(browser, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (browser.CanGoBack)
                        browser.GoBack();
                }
            });
        }

        private void ForwardButton_Click(Object sender, EventArgs e)
        {
            ControlThreadingHelper.InvokeControlAction(browser, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (browser.CanGoForward)
                        browser.GoForward();
                }
            });
        }

        #endregion

    } // End class

} // End namespace