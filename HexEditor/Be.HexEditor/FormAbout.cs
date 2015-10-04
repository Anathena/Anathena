using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Be.HexEditor
{
	/// <summary>
	/// Summary description for FormAbout.
	/// </summary>
	public class FormAbout : Core.FormEx
	{
		private Be.HexEditor.UCAbout ucAbout1;
		private System.Windows.Forms.Button btnOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormAbout()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.btnOK = new System.Windows.Forms.Button();
            this.ucAbout1 = new Be.HexEditor.UCAbout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ucAbout1
            // 
            resources.ApplyResources(this.ucAbout1, "ucAbout1");
            this.ucAbout1.Name = "ucAbout1";
            // 
            // FormAbout
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ucAbout1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FormAbout_CorrectWidth);
            this.Resize += new System.EventHandler(this.FormAbout_CorrectWidth);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			Close();
		}

        private void FormAbout_CorrectWidth(object sender, EventArgs e)
        {
            //var factor = this.DpiNew / Core.FormEx.DpiAtDesign;
            //this.ucAbout1.Width = (int)((this.Width - 40) * factor);
        }
	}
}
