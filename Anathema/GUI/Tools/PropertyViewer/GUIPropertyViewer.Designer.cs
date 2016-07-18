namespace Anathema.GUI
{
    partial class GUIPropertyViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIPropertyViewer));
            this.ProjectContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToggleFreezeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditAddressEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddNewAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EntryCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.EntryIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this.EntryDescription = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.PropertiesView = new Aga.Controls.Tree.TreeViewAdv();
            this.PropertyColumn = new Aga.Controls.Tree.TreeColumn();
            this.ValueColumn = new Aga.Controls.Tree.TreeColumn();
            this.ProjectContextMenuStrip.SuspendLayout();
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
            // PropertiesView
            // 
            this.PropertiesView.BackColor = System.Drawing.SystemColors.Window;
            this.PropertiesView.Columns.Add(this.PropertyColumn);
            this.PropertiesView.Columns.Add(this.ValueColumn);
            this.PropertiesView.Cursor = System.Windows.Forms.Cursors.Default;
            this.PropertiesView.DefaultToolTipProvider = null;
            this.PropertiesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertiesView.DragDropMarkColor = System.Drawing.Color.Black;
            this.PropertiesView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PropertiesView.FullRowSelect = true;
            this.PropertiesView.GridLineStyle = Aga.Controls.Tree.GridLineStyle.Vertical;
            this.PropertiesView.LineColor = System.Drawing.SystemColors.ControlDark;
            this.PropertiesView.Location = new System.Drawing.Point(0, 0);
            this.PropertiesView.Model = null;
            this.PropertiesView.Name = "PropertiesView";
            this.PropertiesView.NodeControls.Add(this.EntryCheckBox);
            this.PropertiesView.NodeControls.Add(this.EntryIcon);
            this.PropertiesView.NodeControls.Add(this.EntryDescription);
            this.PropertiesView.SelectedNode = null;
            this.PropertiesView.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.PropertiesView.ShowLines = false;
            this.PropertiesView.Size = new System.Drawing.Size(263, 216);
            this.PropertiesView.TabIndex = 154;
            this.PropertiesView.Text = "Project Explorer";
            this.PropertiesView.UseColumns = true;
            this.PropertiesView.NodeMouseDoubleClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.ProjectExplorerTreeView_NodeMouseDoubleClick);
            // 
            // PropertyColumn
            // 
            this.PropertyColumn.Header = "Property";
            this.PropertyColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.PropertyColumn.TooltipText = null;
            this.PropertyColumn.Width = 128;
            // 
            // ValueColumn
            // 
            this.ValueColumn.Header = "Value";
            this.ValueColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.ValueColumn.TooltipText = null;
            this.ValueColumn.Width = 128;
            // 
            // GUIProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 216);
            this.Controls.Add(this.PropertiesView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIProperties";
            this.Text = "Properties";
            this.ProjectContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip ProjectContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToggleFreezeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditAddressEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddNewAddressToolStripMenuItem;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox EntryCheckBox;
        private Aga.Controls.Tree.NodeControls.NodeIcon EntryIcon;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryDescription;
        private Aga.Controls.Tree.TreeViewAdv PropertiesView;
        private Aga.Controls.Tree.TreeColumn PropertyColumn;
        private Aga.Controls.Tree.TreeColumn ValueColumn;
    }
}