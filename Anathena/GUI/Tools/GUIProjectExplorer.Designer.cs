namespace Anathena.GUI.Tools
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
            this.ToggleActivationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddNewItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddressRightClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScriptRightClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FolderRightClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectExplorerTreeView = new Aga.Controls.Tree.TreeViewAdv();
            this.EntryCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.EntryIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this.EntryDescription = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.ProjectToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddItemDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.AddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddNewScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectContextMenuStrip.SuspendLayout();
            this.ProjectToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProjectContextMenuStrip
            // 
            this.ProjectContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToggleActivationToolStripMenuItem,
            this.DeleteSelectionToolStripMenuItem,
            this.AddNewItemToolStripMenuItem});
            this.ProjectContextMenuStrip.Name = "RightClickMenu";
            this.ProjectContextMenuStrip.Size = new System.Drawing.Size(168, 70);
            this.ProjectContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ProjectContextMenuStrip_Opening);
            // 
            // ToggleActivationToolStripMenuItem
            // 
            this.ToggleActivationToolStripMenuItem.Name = "ToggleActivationToolStripMenuItem";
            this.ToggleActivationToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.ToggleActivationToolStripMenuItem.Text = "Toggle Activation";
            this.ToggleActivationToolStripMenuItem.Click += new System.EventHandler(this.ToggleFreezeToolStripMenuItem_Click);
            // 
            // DeleteSelectionToolStripMenuItem
            // 
            this.DeleteSelectionToolStripMenuItem.Name = "DeleteSelectionToolStripMenuItem";
            this.DeleteSelectionToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.DeleteSelectionToolStripMenuItem.Text = "Delete Selection";
            this.DeleteSelectionToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectionToolStripMenuItem_Click);
            // 
            // AddNewItemToolStripMenuItem
            // 
            this.AddNewItemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddressRightClickToolStripMenuItem,
            this.ScriptRightClickToolStripMenuItem,
            this.FolderRightClickToolStripMenuItem});
            this.AddNewItemToolStripMenuItem.Name = "AddNewItemToolStripMenuItem";
            this.AddNewItemToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.AddNewItemToolStripMenuItem.Text = "Add New...";
            // 
            // AddressRightClickToolStripMenuItem
            // 
            this.AddressRightClickToolStripMenuItem.Image = global::Anathena.Properties.Resources.CollectValues;
            this.AddressRightClickToolStripMenuItem.Name = "AddressRightClickToolStripMenuItem";
            this.AddressRightClickToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.AddressRightClickToolStripMenuItem.Text = "Address";
            this.AddressRightClickToolStripMenuItem.Click += new System.EventHandler(this.AddressRightClickToolStripMenuItem_Click);
            // 
            // ScriptRightClickToolStripMenuItem
            // 
            this.ScriptRightClickToolStripMenuItem.Image = global::Anathena.Properties.Resources.CollectValues;
            this.ScriptRightClickToolStripMenuItem.Name = "ScriptRightClickToolStripMenuItem";
            this.ScriptRightClickToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.ScriptRightClickToolStripMenuItem.Text = "Script";
            this.ScriptRightClickToolStripMenuItem.Click += new System.EventHandler(this.ScriptRightClickToolStripMenuItem_Click);
            // 
            // FolderRightClickToolStripMenuItem
            // 
            this.FolderRightClickToolStripMenuItem.Image = global::Anathena.Properties.Resources.Open;
            this.FolderRightClickToolStripMenuItem.Name = "FolderRightClickToolStripMenuItem";
            this.FolderRightClickToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.FolderRightClickToolStripMenuItem.Text = "Folder";
            this.FolderRightClickToolStripMenuItem.Click += new System.EventHandler(this.FolderRightClickToolStripMenuItem_Click);
            // 
            // ProjectExplorerTreeView
            // 
            this.ProjectExplorerTreeView.AllowDrop = true;
            this.ProjectExplorerTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.ProjectExplorerTreeView.ContextMenuStrip = this.ProjectContextMenuStrip;
            this.ProjectExplorerTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.ProjectExplorerTreeView.DefaultToolTipProvider = null;
            this.ProjectExplorerTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectExplorerTreeView.DragDropMarkColor = System.Drawing.Color.Black;
            this.ProjectExplorerTreeView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectExplorerTreeView.FullRowSelect = true;
            this.ProjectExplorerTreeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this.ProjectExplorerTreeView.Location = new System.Drawing.Point(0, 25);
            this.ProjectExplorerTreeView.Model = null;
            this.ProjectExplorerTreeView.Name = "ProjectExplorerTreeView";
            this.ProjectExplorerTreeView.NodeControls.Add(this.EntryCheckBox);
            this.ProjectExplorerTreeView.NodeControls.Add(this.EntryIcon);
            this.ProjectExplorerTreeView.NodeControls.Add(this.EntryDescription);
            this.ProjectExplorerTreeView.SelectedNode = null;
            this.ProjectExplorerTreeView.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.ProjectExplorerTreeView.ShowLines = false;
            this.ProjectExplorerTreeView.Size = new System.Drawing.Size(214, 319);
            this.ProjectExplorerTreeView.TabIndex = 154;
            this.ProjectExplorerTreeView.Text = "Project Explorer";
            this.ProjectExplorerTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ProjectExplorerTreeView_ItemDrag);
            this.ProjectExplorerTreeView.NodeMouseDoubleClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.ProjectExplorerTreeView_NodeMouseDoubleClick);
            this.ProjectExplorerTreeView.SelectionChanged += new System.EventHandler(this.ProjectExplorerTreeView_SelectionChanged);
            this.ProjectExplorerTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ProjectExplorerTreeView_DragDrop);
            this.ProjectExplorerTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ProjectExplorerTreeView_DragEnter);
            this.ProjectExplorerTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.ProjectExplorerTreeView_DragOver);
            this.ProjectExplorerTreeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProjectExplorerTreeView_KeyPress);
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
            this.AddNewScriptToolStripMenuItem,
            this.FolderToolStripMenuItem});
            this.AddItemDownButton.Image = global::Anathena.Properties.Resources.Increased;
            this.AddItemDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddItemDownButton.Name = "AddItemDownButton";
            this.AddItemDownButton.Size = new System.Drawing.Size(29, 22);
            this.AddItemDownButton.Text = "AddItemDropDownButton";
            // 
            // AddressToolStripMenuItem
            // 
            this.AddressToolStripMenuItem.Image = global::Anathena.Properties.Resources.CollectValues;
            this.AddressToolStripMenuItem.Name = "AddressToolStripMenuItem";
            this.AddressToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.AddressToolStripMenuItem.Text = "Add New Address";
            this.AddressToolStripMenuItem.Click += new System.EventHandler(this.AddressToolStripMenuItem_Click);
            // 
            // AddNewScriptToolStripMenuItem
            // 
            this.AddNewScriptToolStripMenuItem.Image = global::Anathena.Properties.Resources.CollectValues;
            this.AddNewScriptToolStripMenuItem.Name = "AddNewScriptToolStripMenuItem";
            this.AddNewScriptToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.AddNewScriptToolStripMenuItem.Text = "Add New Script";
            this.AddNewScriptToolStripMenuItem.Click += new System.EventHandler(this.AddNewScriptToolStripMenuItem_Click);
            // 
            // FolderToolStripMenuItem
            // 
            this.FolderToolStripMenuItem.Image = global::Anathena.Properties.Resources.Open;
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
            this.Controls.Add(this.ProjectExplorerTreeView);
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
        private System.Windows.Forms.ToolStripMenuItem ToggleActivationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddNewItemToolStripMenuItem;
        private Aga.Controls.Tree.TreeViewAdv ProjectExplorerTreeView;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox EntryCheckBox;
        private Aga.Controls.Tree.NodeControls.NodeIcon EntryIcon;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryDescription;
        private System.Windows.Forms.ToolStrip ProjectToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton AddItemDownButton;
        private System.Windows.Forms.ToolStripMenuItem AddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddressRightClickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FolderRightClickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddNewScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScriptRightClickToolStripMenuItem;
    }
}