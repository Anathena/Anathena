namespace Anathema.GUI.Tools
{
    partial class GUIDotNetExplorer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIDotNetExplorer));
            this.GUIToolStrip = new System.Windows.Forms.ToolStrip();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.ObjectExplorerTreeView = new Aga.Controls.Tree.TreeViewAdv();
            this.EntryName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.GUIToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GUIToolStrip
            // 
            this.GUIToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.GUIToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshButton});
            this.GUIToolStrip.Location = new System.Drawing.Point(0, 0);
            this.GUIToolStrip.Name = "GUIToolStrip";
            this.GUIToolStrip.Size = new System.Drawing.Size(284, 25);
            this.GUIToolStrip.TabIndex = 150;
            this.GUIToolStrip.Text = "Main Tool Strip";
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshButton.Image = global::Anathema.Properties.Resources.Undo;
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ObjectExplorerTreeView
            // 
            this.ObjectExplorerTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.ObjectExplorerTreeView.DefaultToolTipProvider = null;
            this.ObjectExplorerTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectExplorerTreeView.DragDropMarkColor = System.Drawing.Color.Black;
            this.ObjectExplorerTreeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this.ObjectExplorerTreeView.Location = new System.Drawing.Point(0, 25);
            this.ObjectExplorerTreeView.Model = null;
            this.ObjectExplorerTreeView.Name = "ObjectExplorerTreeView";
            this.ObjectExplorerTreeView.NodeControls.Add(this.EntryName);
            this.ObjectExplorerTreeView.SelectedNode = null;
            this.ObjectExplorerTreeView.Size = new System.Drawing.Size(284, 236);
            this.ObjectExplorerTreeView.TabIndex = 151;
            this.ObjectExplorerTreeView.NodeMouseDoubleClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.ObjectExplorerTreeView_NodeMouseDoubleClick);
            this.ObjectExplorerTreeView.SelectionChanged += new System.EventHandler(this.ObjectExplorerTreeView_SelectionChanged);
            // 
            // EntryName
            // 
            this.EntryName.DataPropertyName = "EntryName";
            this.EntryName.IncrementalSearchEnabled = true;
            this.EntryName.LeftMargin = 3;
            this.EntryName.ParentColumn = null;
            // 
            // GUIDotNetExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ObjectExplorerTreeView);
            this.Controls.Add(this.GUIToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIDotNetExplorer";
            this.Text = ".Net Explorer";
            this.GUIToolStrip.ResumeLayout(false);
            this.GUIToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip GUIToolStrip;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private Aga.Controls.Tree.TreeViewAdv ObjectExplorerTreeView;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryName;
    }
}