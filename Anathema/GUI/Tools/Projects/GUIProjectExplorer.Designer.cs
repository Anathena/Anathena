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
            this.AddAddressButton = new System.Windows.Forms.ToolStripButton();
            this.ProjectToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddressTableTreeView = new Aga.Controls.Tree.TreeViewAdv();
            this.EntryDescriptionColumn = new Aga.Controls.Tree.TreeColumn();
            this.EntryTypeColumn = new Aga.Controls.Tree.TreeColumn();
            this.EntryValueColumn = new Aga.Controls.Tree.TreeColumn();
            this.EntryAddressColumn = new Aga.Controls.Tree.TreeColumn();
            this.FrozenCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.EntryIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this.EntryDescription = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.EntryAddress = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.EntryType = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.EntryValue = new Aga.Controls.Tree.NodeControls.NodeTextBox();
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
            // AddAddressButton
            // 
            this.AddAddressButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddAddressButton.Image = global::Anathema.Properties.Resources.Increased;
            this.AddAddressButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddAddressButton.Name = "AddAddressButton";
            this.AddAddressButton.Size = new System.Drawing.Size(21, 20);
            this.AddAddressButton.Text = "Add New Address";
            this.AddAddressButton.Click += new System.EventHandler(this.AddAddressButton_Click);
            // 
            // ProjectToolStrip
            // 
            this.ProjectToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.ProjectToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ProjectToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddAddressButton});
            this.ProjectToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ProjectToolStrip.Location = new System.Drawing.Point(504, 0);
            this.ProjectToolStrip.Name = "ProjectToolStrip";
            this.ProjectToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ProjectToolStrip.Size = new System.Drawing.Size(24, 225);
            this.ProjectToolStrip.TabIndex = 150;
            // 
            // AddressTableTreeView
            // 
            this.AddressTableTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.AddressTableTreeView.Columns.Add(this.EntryDescriptionColumn);
            this.AddressTableTreeView.Columns.Add(this.EntryTypeColumn);
            this.AddressTableTreeView.Columns.Add(this.EntryValueColumn);
            this.AddressTableTreeView.Columns.Add(this.EntryAddressColumn);
            this.AddressTableTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.AddressTableTreeView.DefaultToolTipProvider = null;
            this.AddressTableTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressTableTreeView.DragDropMarkColor = System.Drawing.Color.Black;
            this.AddressTableTreeView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTableTreeView.FullRowSelect = true;
            this.AddressTableTreeView.GridLineStyle = Aga.Controls.Tree.GridLineStyle.Vertical;
            this.AddressTableTreeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this.AddressTableTreeView.Location = new System.Drawing.Point(0, 0);
            this.AddressTableTreeView.Model = null;
            this.AddressTableTreeView.Name = "AddressTableTreeView";
            this.AddressTableTreeView.NodeControls.Add(this.FrozenCheckBox);
            this.AddressTableTreeView.NodeControls.Add(this.EntryIcon);
            this.AddressTableTreeView.NodeControls.Add(this.EntryDescription);
            this.AddressTableTreeView.NodeControls.Add(this.EntryAddress);
            this.AddressTableTreeView.NodeControls.Add(this.EntryType);
            this.AddressTableTreeView.NodeControls.Add(this.EntryValue);
            this.AddressTableTreeView.SelectedNode = null;
            this.AddressTableTreeView.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.AddressTableTreeView.ShowLines = false;
            this.AddressTableTreeView.Size = new System.Drawing.Size(504, 225);
            this.AddressTableTreeView.TabIndex = 154;
            this.AddressTableTreeView.Text = "Address Table";
            this.AddressTableTreeView.UseColumns = true;
            this.AddressTableTreeView.NodeMouseDoubleClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.AddressTableTreeView_NodeMouseDoubleClick);
            // 
            // EntryDescriptionColumn
            // 
            this.EntryDescriptionColumn.Header = "Description";
            this.EntryDescriptionColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.EntryDescriptionColumn.TooltipText = "Description";
            this.EntryDescriptionColumn.Width = 160;
            // 
            // EntryTypeColumn
            // 
            this.EntryTypeColumn.Header = "Address";
            this.EntryTypeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.EntryTypeColumn.TooltipText = "Memory Address";
            this.EntryTypeColumn.Width = 128;
            // 
            // EntryValueColumn
            // 
            this.EntryValueColumn.Header = "Type";
            this.EntryValueColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.EntryValueColumn.TooltipText = "Data Type";
            this.EntryValueColumn.Width = 72;
            // 
            // EntryAddressColumn
            // 
            this.EntryAddressColumn.Header = "Value";
            this.EntryAddressColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.EntryAddressColumn.TooltipText = "Value at Address";
            this.EntryAddressColumn.Width = 128;
            // 
            // FrozenCheckBox
            // 
            this.FrozenCheckBox.DataPropertyName = "IsChecked";
            this.FrozenCheckBox.EditEnabled = true;
            this.FrozenCheckBox.LeftMargin = 0;
            this.FrozenCheckBox.ParentColumn = this.EntryDescriptionColumn;
            // 
            // EntryIcon
            // 
            this.EntryIcon.DataPropertyName = "EntryIcon";
            this.EntryIcon.LeftMargin = 1;
            this.EntryIcon.ParentColumn = this.EntryDescriptionColumn;
            this.EntryIcon.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // EntryDescription
            // 
            this.EntryDescription.DataPropertyName = "EntryDescription";
            this.EntryDescription.IncrementalSearchEnabled = true;
            this.EntryDescription.LeftMargin = 3;
            this.EntryDescription.ParentColumn = this.EntryDescriptionColumn;
            // 
            // EntryAddress
            // 
            this.EntryAddress.DataPropertyName = "EntryAddress";
            this.EntryAddress.IncrementalSearchEnabled = true;
            this.EntryAddress.LeftMargin = 3;
            this.EntryAddress.ParentColumn = this.EntryTypeColumn;
            // 
            // EntryType
            // 
            this.EntryType.DataPropertyName = "EntryType";
            this.EntryType.IncrementalSearchEnabled = true;
            this.EntryType.LeftMargin = 3;
            this.EntryType.ParentColumn = this.EntryValueColumn;
            // 
            // EntryValue
            // 
            this.EntryValue.DataPropertyName = "EntryValue";
            this.EntryValue.IncrementalSearchEnabled = true;
            this.EntryValue.LeftMargin = 3;
            this.EntryValue.ParentColumn = this.EntryAddressColumn;
            // 
            // GUIProjectExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 225);
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
        private System.Windows.Forms.ToolStripButton AddAddressButton;
        private System.Windows.Forms.ToolStrip ProjectToolStrip;
        private Aga.Controls.Tree.TreeViewAdv AddressTableTreeView;
        private Aga.Controls.Tree.TreeColumn EntryDescriptionColumn;
        private Aga.Controls.Tree.TreeColumn EntryTypeColumn;
        private Aga.Controls.Tree.TreeColumn EntryValueColumn;
        private Aga.Controls.Tree.TreeColumn EntryAddressColumn;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox FrozenCheckBox;
        private Aga.Controls.Tree.NodeControls.NodeIcon EntryIcon;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryDescription;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryType;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryValue;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryAddress;
    }
}