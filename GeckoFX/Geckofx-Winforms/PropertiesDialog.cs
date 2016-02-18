#region ***** BEGIN LICENSE BLOCK *****
/* Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Skybound Software code.
 *
 * The Initial Developer of the Original Code is Skybound Software.
 * Portions created by the Initial Developer are Copyright (C) 2008-2009
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 */
#endregion END LICENSE BLOCK

using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Gecko
{
	partial class PropertiesDialog : Form
	{
		public PropertiesDialog()
		{
			InitializeComponent();
		}
		
		public PropertiesDialog(nsIDOMDocument doc) : this()
		{
			txtTitle.Text = nsString.Get(doc.GetTitleAttribute);

			txtAddress.Text = nsString.Get(doc.GetDocumentURIAttribute);
			txtReferrer.Text = nsString.Get(doc.GetReferrerAttribute);

			nsIDOMDocumentType docType = doc.GetDoctypeAttribute();
			if (docType != null)
				lblDocType.Text = nsString.Get(docType.GetPublicIdAttribute);
			else
				lblDocType.Text = "(none)";
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData == Keys.F1)
			{
				// display a simple about box when you press F1
				string versionString = GetType().Assembly.GetName().Version.ToString();

				MessageBox.Show("GeckoFX v" + versionString + "\r\n\r\nhttp://bitbucket.org/geckofx/",
					"About GeckoFX", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}
	}
	
	/// <summary>
	/// A custom tab page which provides transparent backgrounds to read-only text boxes.
	/// </summary>
	#region class XPTabPage : TabPage
	class XPTabPage : TabPage
	{
		[DllImport("gdi32")]
		static extern int SetBkMode(IntPtr hdc, int nBkMode);
		
		[DllImport("gdi32")]
		static extern IntPtr GetStockObject(int nIndex);
		
		protected override void WndProc(ref Message m)
		{
			const int WM_CTLCOLOREDIT = 0x133;
			const int WM_CTLCOLORSTATIC = 0x138;
			const int TRANSPARENT = 0x1;
			const int NULL_BRUSH = 0x5;
			
			if (Application.RenderWithVisualStyles)
			{
				if (m.Msg == WM_CTLCOLORSTATIC || m.Msg == WM_CTLCOLOREDIT)
				{
					SetBkMode(m.WParam, TRANSPARENT);
					m.Result = GetStockObject(NULL_BRUSH);
					
					return;
				}
			}
			base.WndProc(ref m);
		}
	}
	#endregion
	
	/// <summary>
	/// A read-only text box with a transparent background.
	/// </summary>
	#region class ReadOnlyTextBox : TextBox
	class ReadOnlyTextBox : TextBox
	{
		public ReadOnlyTextBox()
		{
			this.ReadOnly = true;
			this.Multiline = true;
			this.WordWrap = false;
		}
		
		protected override void WndProc(ref Message m)
		{
			const int WM_ERASEBKGND = 0x14;
			
			switch (m.Msg)
			{
				case WM_ERASEBKGND:
					if (Application.RenderWithVisualStyles)
					{
						VisualStyleRenderer rend = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
						
						rend.DrawParentBackground(new DeviceContext(m.WParam), this.ClientRectangle, this);
						
						m.Result = (IntPtr)1;
						return;
					}
					break;
			}
			
			base.WndProc(ref m);
		}

		protected override void OnLostFocus(EventArgs e)
		{
			Refresh();
			
			base.OnLostFocus(e);
		}
		
		class DeviceContext : IDeviceContext
		{
			public DeviceContext(IntPtr hdc)
			{
				this._Hdc = hdc;
			}
			
			public IntPtr GetHdc() { return _Hdc; }
			IntPtr _Hdc;
			
			public void ReleaseHdc() { }

			public void Dispose()
			{
				_Hdc = IntPtr.Zero;
				GC.SuppressFinalize(this);
			}
		}
	}
	#endregion
}
