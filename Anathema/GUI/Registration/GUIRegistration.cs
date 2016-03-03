using Anathema.Utils;
using Gecko;
using Gecko.Events;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIRegistration : Form
    {
        private const String BrowserTag = "?client=true";
        private const String AnathemaRegisterURL = "www.anathemaengine.com/account.php" + BrowserTag;

        private GeckoWebBrowser Browser;

        public GUIRegistration()
        {
            InitializeComponent();

            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            BrowserHelper.GetInstance().InitializeBrowserStatic(BrowserTag);

            Browser = new GeckoWebBrowser();
            Browser.Navigate(AnathemaRegisterURL);
            Browser.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(Browser);

            Browser.Navigating += Browser_Navigating;

            LauncherDialog.Download += LauncherDialog_Download;
        }

        internal class CheckRegistered : RepeatedTask
        {
            private DateTime StartTime;
            private const Int32 Timeout = 10000;
            private IWin32Window Parent;

            public CheckRegistered(IWin32Window Parent) { this.Parent = Parent; }

            public override void Begin()
            {
                StartTime = DateTime.Now;
                base.Begin();
            }

            // Just repeatedly poll the activation file while it downloads and see if it is a good format
            protected override void Update()
            {
                String ActivationFile = BrowserHelper.GetInstance().GetLastDownloadedFile();
                String ActivationCode = File.ReadAllText(ActivationFile);

                if (Regex.IsMatch(ActivationCode, "....-....-....-....-...."))
                {
                    RegistrationManager.GetInstance().Register();
                    CancelFlag = true;
                    MessageBox.Show("Registration Complete. Restart Anathema for all changes to take effect.", "Registration Complete");
                }
                else if (DateTime.Now - StartTime > TimeSpan.FromMilliseconds(Timeout))
                {
                    CancelFlag = true;
                    MessageBox.Show("Timed out acquiring activation file");
                }
            }
        }

        #region Events

        private void Browser_Navigating(Object Sender, GeckoNavigatingEventArgs E)
        {
            if (E.Uri.AbsoluteUri.Contains(BrowserTag))
                return;

            Browser.Navigate(E.Uri.AbsoluteUri + BrowserTag);
        }

        /// <summary>
        ///  TODO: Change this to read in downloaded serial code
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void LauncherDialog_Download(Object Sender, LauncherDialogEvent E)
        {
            CheckRegistered CheckRegistered = new CheckRegistered(this);
            CheckRegistered.Begin();
        }

        #endregion

    } // End class

} // End namespace