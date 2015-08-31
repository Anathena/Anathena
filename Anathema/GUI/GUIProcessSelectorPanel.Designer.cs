namespace Anathema
{
    partial class GUIProcessSelector
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIProcessSelector));
            this.ProcessListView = new System.Windows.Forms.ListView();
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GUIToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.AcceptProcessButton = new System.Windows.Forms.ToolStripButton();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.RightClickMenu.SuspendLayout();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessListView
            // 
            this.ProcessListView.AutoArrange = false;
            this.ProcessListView.ContextMenuStrip = this.RightClickMenu;
            this.ProcessListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessListView.FullRowSelect = true;
            this.ProcessListView.LabelWrap = false;
            this.ProcessListView.Location = new System.Drawing.Point(0, 25);
            this.ProcessListView.MultiSelect = false;
            this.ProcessListView.Name = "ProcessListView";
            this.ProcessListView.ShowGroups = false;
            this.ProcessListView.Size = new System.Drawing.Size(240, 252);
            this.ProcessListView.TabIndex = 24;
            this.ProcessListView.TileSize = new System.Drawing.Size(16, 16);
            this.ProcessListView.UseCompatibleStateImageBehavior = false;
            this.ProcessListView.View = System.Windows.Forms.View.SmallIcon;
            this.ProcessListView.DoubleClick += new System.EventHandler(this.ProcessListView_DoubleClick);
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenProcessToolStripMenuItem,
            this.RefreshToolStripMenuItem});
            this.RightClickMenu.Name = "RightClickMenu";
            this.RightClickMenu.Size = new System.Drawing.Size(147, 48);
            // 
            // OpenProcessToolStripMenuItem
            // 
            this.OpenProcessToolStripMenuItem.Name = "OpenProcessToolStripMenuItem";
            this.OpenProcessToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.OpenProcessToolStripMenuItem.Text = "Open Process";
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.RefreshToolStripMenuItem.Text = "Refresh";
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AcceptProcessButton,
            this.RefreshButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(240, 25);
            this.ScanToolStrip.TabIndex = 122;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // AcceptProcessButton
            // 
            this.AcceptProcessButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AcceptProcessButton.Image = ((System.Drawing.Image)(resources.GetObject("AcceptProcessButton.Image")));
            this.AcceptProcessButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AcceptProcessButton.Name = "AcceptProcessButton";
            this.AcceptProcessButton.Size = new System.Drawing.Size(23, 22);
            this.AcceptProcessButton.Text = "Accept Process";
            this.AcceptProcessButton.Click += new System.EventHandler(this.AcceptProcessButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshButton.Text = "Refresh Processes";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ProcessSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProcessListView);
            this.Controls.Add(this.ScanToolStrip);
            this.Name = "ProcessSelector";
            this.Size = new System.Drawing.Size(240, 277);
            this.Load += new System.EventHandler(this.GUIProcessSelector_Load);
            this.RightClickMenu.ResumeLayout(false);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ProcessListView;
        private System.Windows.Forms.ContextMenuStrip RightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
        private System.Windows.Forms.ToolTip GUIToolTip;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton AcceptProcessButton;
        private System.Windows.Forms.ToolStripButton RefreshButton;
    }
}
