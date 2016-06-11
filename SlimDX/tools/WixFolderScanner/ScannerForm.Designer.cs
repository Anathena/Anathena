namespace WixFolderScanner
{
	partial class ScannerForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
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
			this.ExtensionsBox = new System.Windows.Forms.TextBox();
			this.OutputBox = new System.Windows.Forms.TextBox();
			this.ScanButton = new System.Windows.Forms.Button();
			this.FolderBox = new System.Windows.Forms.TextBox();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ExtensionsBox
			// 
			this.ExtensionsBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ExtensionsBox.Location = new System.Drawing.Point(0, 32);
			this.ExtensionsBox.Name = "ExtensionsBox";
			this.ExtensionsBox.Size = new System.Drawing.Size(668, 20);
			this.ExtensionsBox.TabIndex = 1;
			this.ExtensionsBox.Text = "suo cachefile user";
			// 
			// OutputBox
			// 
			this.OutputBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.OutputBox.Location = new System.Drawing.Point(0, 58);
			this.OutputBox.Multiline = true;
			this.OutputBox.Name = "OutputBox";
			this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.OutputBox.Size = new System.Drawing.Size(668, 337);
			this.OutputBox.TabIndex = 2;
			this.OutputBox.WordWrap = false;
			// 
			// ScanButton
			// 
			this.ScanButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.ScanButton.Location = new System.Drawing.Point(297, 401);
			this.ScanButton.Name = "ScanButton";
			this.ScanButton.Size = new System.Drawing.Size(75, 23);
			this.ScanButton.TabIndex = 3;
			this.ScanButton.Text = "Scan";
			this.ScanButton.UseVisualStyleBackColor = true;
			this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
			// 
			// FolderBox
			// 
			this.FolderBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.FolderBox.Location = new System.Drawing.Point(0, 6);
			this.FolderBox.Name = "FolderBox";
			this.FolderBox.Size = new System.Drawing.Size(638, 20);
			this.FolderBox.TabIndex = 0;
			// 
			// BrowseButton
			// 
			this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BrowseButton.Location = new System.Drawing.Point(640, 3);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(28, 23);
			this.BrowseButton.TabIndex = 4;
			this.BrowseButton.Text = "...";
			this.BrowseButton.UseVisualStyleBackColor = true;
			// 
			// ScannerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(668, 426);
			this.Controls.Add(this.BrowseButton);
			this.Controls.Add(this.FolderBox);
			this.Controls.Add(this.ScanButton);
			this.Controls.Add(this.OutputBox);
			this.Controls.Add(this.ExtensionsBox);
			this.Name = "ScannerForm";
			this.Text = "WiX Folder Scanner";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox ExtensionsBox;
		private System.Windows.Forms.TextBox OutputBox;
		private System.Windows.Forms.Button ScanButton;
		private System.Windows.Forms.TextBox FolderBox;
		private System.Windows.Forms.Button BrowseButton;

	}
}

