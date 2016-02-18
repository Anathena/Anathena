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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Gecko
{
	partial class ConfirmDialog : Form
	{
		public ConfirmDialog()
		{
			InitializeComponent();
		}
		
		public ConfirmDialog(string message, string title, string button1Text, string button2Text, string button3Text, string checkBoxText)
		{
			InitializeComponent();
			
			this.Font = SystemFonts.MessageBoxFont;
			this.Text = title;
			
			List<String> buttonText = new List<String>();
			
			if (button1Text != null)
				buttonText.Add(button1Text);
			if (button2Text != null)
				buttonText.Add(button2Text);
			if (button3Text != null)
				buttonText.Add(button3Text);
			
			int buttonCount = buttonText.Count;
			
			while (buttonText.Count < 3)
				buttonText.Insert(0, null);
			
			if (buttonCount > 1)
			{
				// assign the icon
				this.MBIcon = MessageBoxIcon.Question;
			}
			else
			{
				// hide the icon
				int right = label.Right;
				pictureBox.Visible = false;
				label.Left = pictureBox.Left;
				label.Width = right - label.Left;
			}
			
			Button [] buttons = { button1, button2, button3 };
			
			int b = 1;
			for (int i = 0; i < 3; i++)
			{
				if (buttonText[i] != null)
				{
					buttons[i].Text = buttonText[i];
					buttons[i].DialogResult = (DialogResult)b++;
				}
				else
				{
					buttons[i].Visible = false;
				}
			}
			
			// set checkbox text
			if (checkBoxText != null)
			{
				checkBox.Text = checkBoxText;
			}
			else
			{
				panel1.Height = label1.Top - 1;
				checkBox.Visible = false;
			}
			
			// set label text
			label.Text = message;
			label.Size = label.GetPreferredSize(new Size(label.Width, label.Height));
			
			// update window size
			int bottomOfContent = Math.Max(label.Bottom, ((MBIcon != MessageBoxIcon.None) ? pictureBox.Bottom : label.Bottom));
			int clientHeight = bottomOfContent + label.Top + panel1.Height;
			
			int buttonExtent = ((buttonText[0] != null) ? (button1.Width + 4) : 0) +
				((buttonText[1] != null) ? (button2.Width + 4) : 0) +
				((buttonText[2] != null) ? (button3.Width + 4) : 0);
			
			int clientWidth = Math.Max(buttonExtent + 16, label.Right + 8);
			
			this.ClientSize = new Size(clientWidth, clientHeight);
		}
		
		private MessageBoxIcon MBIcon;
		
		private void pictureBox_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawIcon(SystemIcons.Question, 0, 0);
		}

		public bool CheckBoxChecked
		{
			get { return this.checkBox.Checked; }
		}

		protected override void WndProc(ref Message m)
		{
			const int WM_NCHITTEST = 0x84;
			const int HTCLIENT = 0x1;
			const int HTCAPTION = 0x2;
			
			base.WndProc(ref m);
			
			if (m.Msg == WM_NCHITTEST)
			{
				if (m.Result != (IntPtr)HTCLIENT)
					m.Result = (IntPtr)HTCAPTION;
			}
		}
	}
}
