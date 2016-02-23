using Gecko;
using System;
using System.IO;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIRegistration : Form
    {
        private const String AnathemaCheatBrowseURL = "www.anathemaengine.com/browser.php";

        private GeckoWebBrowser Browser;

        public GUIRegistration(Boolean IsTrialValid)
        {
            InitializeComponent();

            if (IsTrialValid)
            {

            }
            else
            {

            }
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
        }

        #region Events

        #endregion

    } // End class

} // End namespace