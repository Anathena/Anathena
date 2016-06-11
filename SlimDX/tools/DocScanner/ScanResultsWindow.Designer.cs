namespace DocScanner
{
	partial class ScanResultsWindow
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanResultsWindow));
			this.ResultsList = new System.Windows.Forms.ListBox();
			this.ItemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CopyAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.GoToHeaderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ItemContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// ResultsList
			// 
			this.ResultsList.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ResultsList.FormattingEnabled = true;
			this.ResultsList.IntegralHeight = false;
			this.ResultsList.Location = new System.Drawing.Point(13, 13);
			this.ResultsList.Name = "ResultsList";
			this.ResultsList.Size = new System.Drawing.Size(303, 412);
			this.ResultsList.Sorted = true;
			this.ResultsList.TabIndex = 0;
			this.ResultsList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ResultsList_MouseUp);
			this.ResultsList.DoubleClick += new System.EventHandler(this.ResultsList_DoubleClick);
			// 
			// ItemContextMenu
			// 
			this.ItemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyMenuItem,
            this.CopyAllMenuItem,
            this.toolStripMenuItem1,
            this.GoToHeaderMenuItem});
			this.ItemContextMenu.Name = "ItemContextMenu";
			this.ItemContextMenu.Size = new System.Drawing.Size(150, 76);
			// 
			// CopyMenuItem
			// 
			this.CopyMenuItem.Name = "CopyMenuItem";
			this.CopyMenuItem.Size = new System.Drawing.Size(149, 22);
			this.CopyMenuItem.Text = "&Copy";
			this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
			// 
			// CopyAllMenuItem
			// 
			this.CopyAllMenuItem.Name = "CopyAllMenuItem";
			this.CopyAllMenuItem.Size = new System.Drawing.Size(149, 22);
			this.CopyAllMenuItem.Text = "Copy &All";
			this.CopyAllMenuItem.Click += new System.EventHandler(this.CopyAllMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(146, 6);
			// 
			// GoToHeaderMenuItem
			// 
			this.GoToHeaderMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.GoToHeaderMenuItem.Name = "GoToHeaderMenuItem";
			this.GoToHeaderMenuItem.Size = new System.Drawing.Size(149, 22);
			this.GoToHeaderMenuItem.Text = "&Go to Header";
			this.GoToHeaderMenuItem.Click += new System.EventHandler(this.GoToHeaderMenuItem_Click);
			// 
			// ScanResultsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(328, 435);
			this.Controls.Add(this.ResultsList);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.Name = "ScanResultsWindow";
			this.Text = "Scan Results";
			this.ItemContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox ResultsList;
		private System.Windows.Forms.ContextMenuStrip ItemContextMenu;
		private System.Windows.Forms.ToolStripMenuItem CopyMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CopyAllMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem GoToHeaderMenuItem;
	}
}