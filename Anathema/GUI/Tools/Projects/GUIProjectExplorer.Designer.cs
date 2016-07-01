namespace Anathema.GUI
{
    partial class GUIProjectExplorer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIProjectExplorer));
            this.ProjectContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToggleFreezeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditAddressEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddNewAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddressTableTreeView = new Aga.Controls.Tree.TreeViewAdv();
            this.EntryCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.EntryIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this.EntryDescription = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.ProjectToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddItemDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.AddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectContextMenuStrip.SuspendLayout();
            this.ProjectToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProjectContextMenuStrip
            // 
            this.ProjectContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToggleFreezeToolStripMenuItem,
            this.EditAddressEntryToolStripMenuItem,
            this.DeleteSelectionToolStripMenuItem,
            this.AddNewAddressToolStripMenuItem});
            this.ProjectContextMenuStrip.Name = "RightClickMenu";
            this.ProjectContextMenuStrip.Size = new System.Drawing.Size(169, 92);
            // 
            // ToggleFreezeToolStripMenuItem
            // 
            this.ToggleFreezeToolStripMenuItem.Name = "ToggleFreezeToolStripMenuItem";
            this.ToggleFreezeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.ToggleFreezeToolStripMenuItem.Text = "Toggle Freeze";
            this.ToggleFreezeToolStripMenuItem.Click += new System.EventHandler(this.ToggleFreezeToolStripMenuItem_Click);
            // 
            // EditAddressEntryToolStripMenuItem
            // 
            this.EditAddressEntryToolStripMenuItem.Name = "EditAddressEntryToolStripMenuItem";
            this.EditAddressEntryToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.EditAddressEntryToolStripMenuItem.Text = "Edit Entry";
            this.EditAddressEntryToolStripMenuItem.Click += new System.EventHandler(this.EditAddressEntryToolStripMenuItem_Click);
            // 
            // DeleteSelectionToolStripMenuItem
            // 
            this.DeleteSelectionToolStripMenuItem.Name = "DeleteSelectionToolStripMenuItem";
            this.DeleteSelectionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.DeleteSelectionToolStripMenuItem.Text = "Delete Selection";
            this.DeleteSelectionToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectionToolStripMenuItem_Click);
            // 
            // AddNewAddressToolStripMenuItem
            // 
            this.AddNewAddressToolStripMenuItem.Name = "AddNewAddressToolStripMenuItem";
            this.AddNewAddressToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.AddNewAddressToolStripMenuItem.Text = "Add New Address";
            this.AddNewAddressToolStripMenuItem.Click += new System.EventHandler(this.AddNewAddressToolStripMenuItem_Click);
            // 
            // AddressTableTreeView
            // 
            this.AddressTableTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.AddressTableTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.AddressTableTreeView.DefaultToolTipProvider = null;
            this.AddressTableTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressTableTreeView.DragDropMarkColor = System.Drawing.Color.Black;
            this.AddressTableTreeView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTableTreeView.FullRowSelect = true;
            this.AddressTableTreeView.GridLineStyle = Aga.Controls.Tree.GridLineStyle.Vertical;
            this.AddressTableTreeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this.AddressTableTreeView.Location = new System.Drawing.Point(0, 25);
            this.AddressTableTreeView.Model = null;
            this.AddressTableTreeView.Name = "AddressTableTreeView";
            this.AddressTableTreeView.NodeControls.Add(this.EntryCheckBox);
            this.AddressTableTreeView.NodeControls.Add(this.EntryIcon);
            this.AddressTableTreeView.NodeControls.Add(this.EntryDescription);
            this.AddressTableTreeView.SelectedNode = null;
            this.AddressTableTreeView.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.AddressTableTreeView.ShowLines = false;
            this.AddressTableTreeView.Size = new System.Drawing.Size(214, 319);
            this.AddressTableTreeView.TabIndex = 154;
            this.AddressTableTreeView.Text = "Project Explorer";
            this.AddressTableTreeView.NodeMouseDoubleClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.AddressTableTreeView_NodeMouseDoubleClick);
            // 
            // EntryCheckBox
            // 
            this.EntryCheckBox.DataPropertyName = "IsChecked";
            this.EntryCheckBox.EditEnabled = true;
            this.EntryCheckBox.LeftMargin = 0;
            this.EntryCheckBox.ParentColumn = null;
            // 
            // EntryIcon
            // 
            this.EntryIcon.DataPropertyName = "EntryIcon";
            this.EntryIcon.LeftMargin = 1;
            this.EntryIcon.ParentColumn = null;
            this.EntryIcon.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // EntryDescription
            // 
            this.EntryDescription.DataPropertyName = "EntryDescription";
            this.EntryDescription.IncrementalSearchEnabled = true;
            this.EntryDescription.LeftMargin = 3;
            this.EntryDescription.ParentColumn = null;
            // 
            // ProjectToolStrip
            // 
            this.ProjectToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ProjectToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddItemDownButton});
            this.ProjectToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ProjectToolStrip.Name = "ProjectToolStrip";
            this.ProjectToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ProjectToolStrip.Size = new System.Drawing.Size(214, 25);
            this.ProjectToolStrip.TabIndex = 155;
            this.ProjectToolStrip.Text = "toolStrip1";
            // 
            // AddItemDownButton
            // 
            this.AddItemDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddItemDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddressToolStripMenuItem,
            this.FolderToolStripMenuItem});
            this.AddItemDownButton.Image = global::Anathema.Properties.Resources.Increased;
            this.AddItemDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddItemDownButton.Name = "AddItemDownButton";
            this.AddItemDownButton.Size = new System.Drawing.Size(29, 22);
            this.AddItemDownButton.Text = "AddItemDropDownButton";
            // 
            // AddressToolStripMenuItem
            // 
            this.AddressToolStripMenuItem.Image = global::Anathema.Properties.Resources.CollectValues;
            this.AddressToolStripMenuItem.Name = "AddressToolStripMenuItem";
            this.AddressToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.AddressToolStripMenuItem.Text = "Add New Address";
            this.AddressToolStripMenuItem.Click += new System.EventHandler(this.AddressToolStripMenuItem_Click);
            // 
            // FolderToolStripMenuItem
            // 
            this.FolderToolStripMenuItem.Image = global::Anathema.Properties.Resources.Open;
            this.FolderToolStripMenuItem.Name = "FolderToolStripMenuItem";
            this.FolderToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.FolderToolStripMenuItem.Text = "Add New Folder";
            this.FolderToolStripMenuItem.Click += new System.EventHandler(this.FolderToolStripMenuItem_Click);
            // 
            // GUIProjectExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 344);
            this.Controls.Add(this.AddressTableTreeView);
            this.Controls.Add(this.ProjectToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIProjectExplorer";
            this.Text = "Project Explorer";
            this.ProjectContextMenuStrip.ResumeLayout(false);
            this.ProjectToolStrip.ResumeLayout(false);
            this.ProjectToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip ProjectContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToggleFreezeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditAddressEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddNewAddressToolStripMenuItem;
        private Aga.Controls.Tree.TreeViewAdv AddressTableTreeView;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox EntryCheckBox;
        private Aga.Controls.Tree.NodeControls.NodeIcon EntryIcon;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryDescription;
        private System.Windows.Forms.ToolStrip ProjectToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton AddItemDownButton;
        private System.Windows.Forms.ToolStripMenuItem AddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FolderToolStripMenuItem;
    }
}