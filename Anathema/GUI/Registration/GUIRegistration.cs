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
            return;
        }

        private void InitializeBrowser(String PHPGet)
        {
            if (OSInterface.IsAnathema32Bit())
                Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "xulrunner-32"));

            if (OSInterface.IsAnathema64Bit())
                Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "xulrunner-64"));

            Browser = new GeckoWebBrowser();
            Browser.Navigate(AnathemaRegisterURL);
            Browser.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(Browser);
        }

        #region Events

        #endregion

    } // End class

} // End namespace