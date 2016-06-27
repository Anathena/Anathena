namespace Anathema.GUI
{
    partial class GUIAddressTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIAddressTable));
            this.AddressTableContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToggleFreezeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditAddressEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddNewAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddAddressButton = new System.Windows.Forms.ToolStripButton();
            this.TableToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddressTableTreeView = new Aga.Controls.Tree.TreeViewAdv();
            this.EntryDescriptionColumn = new Aga.Controls.Tree.TreeColumn();
            this.EntryTypeColumn = new Aga.Controls.Tree.TreeColumn();
            this.EntryValueColumn = new Aga.Controls.Tree.TreeColumn();
            this.EntryAddressColumn = new Aga.Controls.Tree.TreeColumn();
            this.FreezeCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.EntryIcon = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this.EntryDescription = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.EntryAddress = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.EntryType = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.EntryValue = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.AddressTableListView = new Anathema.GUI.CheckableListView();
            this.FrozenHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressTableContextMenuStrip.SuspendLayout();
            this.TableToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddressTableContextMenuStrip
            // 
            this.AddressTableContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToggleFreezeToolStripMenuItem,
            this.EditAddressEntryToolStripMenuItem,
            this.DeleteSelectionToolStripMenuItem,
            this.AddNewAddressToolStripMenuItem});
            this.AddressTableContextMenuStrip.Name = "RightClickMenu";
            this.AddressTableContextMenuStrip.Size = new System.Drawing.Size(169, 92);
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
            // TableToolStrip
            // 
            this.TableToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.TableToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TableToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddAddressButton});
            this.TableToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.TableToolStrip.Location = new System.Drawing.Point(488, 0);
            this.TableToolStrip.Name = "TableToolStrip";
            this.TableToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TableToolStrip.Size = new System.Drawing.Size(24, 225);
            this.TableToolStrip.TabIndex = 150;
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
            this.AddressTableTreeView.GridLineStyle = Aga.Controls.Tree.GridLineStyle.Vertical;
            this.AddressTableTreeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this.AddressTableTreeView.Location = new System.Drawing.Point(0, 0);
            this.AddressTableTreeView.Model = null;
            this.AddressTableTreeView.Name = "AddressTableTreeView";
            this.AddressTableTreeView.NodeControls.Add(this.FreezeCheckBox);
            this.AddressTableTreeView.NodeControls.Add(this.EntryIcon);
            this.AddressTableTreeView.NodeControls.Add(this.EntryDescription);
            this.AddressTableTreeView.NodeControls.Add(this.EntryAddress);
            this.AddressTableTreeView.NodeControls.Add(this.EntryType);
            this.AddressTableTreeView.NodeControls.Add(this.EntryValue);
            this.AddressTableTreeView.SelectedNode = null;
            this.AddressTableTreeView.Size = new System.Drawing.Size(488, 225);
            this.AddressTableTreeView.TabIndex = 154;
            this.AddressTableTreeView.Text = "Address Table";
            this.AddressTableTreeView.UseColumns = true;
            // 
            // EntryDescriptionColumn
            // 
            this.EntryDescriptionColumn.Header = "Description";
            this.EntryDescriptionColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.EntryDescriptionColumn.TooltipText = "Description";
            this.EntryDescriptionColumn.Width = 128;
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
            // FreezeCheckBox
            // 
            this.FreezeCheckBox.DataPropertyName = "IsChecked";
            this.FreezeCheckBox.LeftMargin = 0;
            this.FreezeCheckBox.ParentColumn = this.EntryDescriptionColumn;
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
            // AddressTableListView
            // 
            this.AddressTableListView.AllowDrop = true;
            this.AddressTableListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressTableListView.CheckBoxes = true;
            this.AddressTableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FrozenHeader,
            this.DescriptionHeader,
            this.AddressHeader,
            this.TypeHeader,
            this.ValueHeader});
            this.AddressTableListView.ContextMenuStrip = this.AddressTableContextMenuStrip;
            this.AddressTableListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTableListView.FullRowSelect = true;
            this.AddressTableListView.Location = new System.Drawing.Point(0, 0);
            this.AddressTableListView.Name = "AddressTableListView";
            this.AddressTableListView.OwnerDraw = true;
            this.AddressTableListView.ShowGroups = false;
            this.AddressTableListView.Size = new System.Drawing.Size(422, 139);
            this.AddressTableListView.TabIndex = 151;
            this.AddressTableListView.UseCompatibleStateImageBehavior = false;
            this.AddressTableListView.View = System.Windows.Forms.View.Details;
            this.AddressTableListView.VirtualMode = true;
            this.AddressTableListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.AddressTableListView_ItemDrag);
            this.AddressTableListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.AddressTableListView_RetrieveVirtualItem);
            this.AddressTableListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.AddressTableListView_DragDrop);
            this.AddressTableListView.DragOver += new System.Windows.Forms.DragEventHandler(this.AddressTableListView_DragOver);
            this.AddressTableListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddressTableListView_MouseClick);
            this.AddressTableListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AddressTableListView_MouseDoubleClick);
            // 
            // FrozenHeader
            // 
            this.FrozenHeader.Text = "";
            this.FrozenHeader.Width = 24;
            // 
            // DescriptionHeader
            // 
            this.DescriptionHeader.Text = "Description";
            this.DescriptionHeader.Width = 115;
            // 
            // AddressHeader
            // 
            this.AddressHeader.Text = "Address";
            this.AddressHeader.Width = 156;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Text = "Type";
            this.TypeHeader.Width = 73;
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 113;
            // 
            // GUIAddressTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 225);
            this.Controls.Add(this.AddressTableTreeView);
            this.Controls.Add(this.AddressTableListView);
            this.Controls.Add(this.TableToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIAddressTable";
            this.Text = "Addresses";
            this.AddressTableContextMenuStrip.ResumeLayout(false);
            this.TableToolStrip.ResumeLayout(false);
            this.TableToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GUI.CheckableListView AddressTableListView;
        private System.Windows.Forms.ColumnHeader FrozenHeader;
        private System.Windows.Forms.ColumnHeader DescriptionHeader;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.ContextMenuStrip AddressTableContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToggleFreezeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditAddressEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddNewAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton AddAddressButton;
        private System.Windows.Forms.ToolStrip TableToolStrip;
        private Aga.Controls.Tree.TreeViewAdv AddressTableTreeView;
        private Aga.Controls.Tree.TreeColumn EntryDescriptionColumn;
        private Aga.Controls.Tree.TreeColumn EntryTypeColumn;
        private Aga.Controls.Tree.TreeColumn EntryValueColumn;
        private Aga.Controls.Tree.TreeColumn EntryAddressColumn;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox FreezeCheckBox;
        private Aga.Controls.Tree.NodeControls.NodeIcon EntryIcon;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryDescription;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryType;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryValue;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryAddress;
    }
}