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
        private const String AnathemaCheatBrowseURL = "www.anathemaengine.com";

        public GUICheatBrowser()
        {
            InitializeComponent();

            // Initialize presenter


            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            Text = "CefSharp";
            WindowState = FormWindowState.Maximized;

            Browser = new ChromiumWebBrowser(AnathemaCheatBrowseURL)
            {
                MenuHandler = null,
                Dock = DockStyle.Fill,
            };
            ContentPanel.Controls.Add(Browser);

            Browser.ConsoleMessage += OnBrowserConsoleMessage;
            Browser.StatusMessage += OnBrowserStatusMessage;
            Browser.TitleChanged += OnBrowserTitleChanged;
            Browser.AddressChanged += OnBrowserAddressChanged;
        }

        private void LoadUrl(String URL)
        {
            if (!Uri.IsWellFormedUriString(URL, UriKind.RelativeOrAbsolute))
                return;

            Browser.Load(URL);
        }

        #region Events

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            //DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            //this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            //this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            //this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }

        #endregion

    } // End class

} // End namespace