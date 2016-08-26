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
        private const String AnathenaCheatBrowseURL = "http://www.anathena.com/browser/browser.php";

        private GeckoWebBrowser Browser;
        private Object AccessLock;

        public GUICheatBrowser()
        {
            InitializeComponent();

            // Initialize presenter
            // (No presenter currently, since the browser object does all the work)
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

            BrowserHelper.GetInstance().InitializeBrowserStatic();

            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    Browser = new GeckoWebBrowser();
                    Browser.Navigate(AnathenaCheatBrowseURL);
                    Browser.Dock = DockStyle.Fill;
                    ContentPanel.Controls.Add(Browser);
                }
            });
        }

        #region Events

        private void BackButton_Click(Object Sender, EventArgs E)
        {
            ControlThreadingHelper.InvokeControlAction(Browser, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (Browser.CanGoBack)
                        Browser.GoBack();
                }
            });
        }

        private void ForwardButton_Click(Object Sender, EventArgs E)
        {
            ControlThreadingHelper.InvokeControlAction(Browser, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (Browser.CanGoForward)
                        Browser.GoForward();
                }
            });
        }

        #endregion

    } // End class

} // End namespace