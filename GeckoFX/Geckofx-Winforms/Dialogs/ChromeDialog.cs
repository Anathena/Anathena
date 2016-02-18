using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gecko
{
	public partial class ChromeDialog : Form
	{
		public ChromeDialog()
		{
			InitializeComponent();
		}

		public GeckoWebBrowser WebBrowser
		{
			get { return webBrowser; }
		}
		
		private void webBrowser_DocumentTitleChanged(object sender, EventArgs e)
		{
			this.Text = webBrowser.DocumentTitle;
		}

		private void webBrowser_WindowSetBounds(object sender, GeckoWindowSetBoundsEventArgs e)
		{
			this.SetBounds(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height, e.BoundsSpecified);
		}

		private void webBrowser_WindowClosed(object sender, EventArgs e)
		{
			this.Close();
		}

	}
}