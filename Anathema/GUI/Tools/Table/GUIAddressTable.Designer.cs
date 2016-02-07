namespace Anathema
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AddressTableListView = new Anathema.CheckableListView();
            this.FrozenHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressDescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressTableContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToggleFreezeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditAddressEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddNewAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddressTableContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddressTableListView
            // 
            this.AddressTableListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressTableListView.CheckBoxes = true;
            this.AddressTableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FrozenHeader,
            this.AddressDescriptionHeader,
            this.AddressHeader,
            this.TypeHeader,
            this.ValueHeader});
            this.AddressTableListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressTableListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTableListView.FullRowSelect = true;
            this.AddressTableListView.Location = new System.Drawing.Point(0, 0);
            this.AddressTableListView.Name = "AddressTableListView";
            this.AddressTableListView.OwnerDraw = true;
            this.AddressTableListView.ShowGroups = false;
            this.AddressTableListView.Size = new System.Drawing.Size(473, 200);
            this.AddressTableListView.TabIndex = 144;
            this.AddressTableListView.UseCompatibleStateImageBehavior = false;
            this.AddressTableListView.View = System.Windows.Forms.View.Details;
            this.AddressTableListView.VirtualMode = true;
            this.AddressTableListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.AddressTableListView_RetrieveVirtualItem);
            this.AddressTableListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddressTableListView_KeyPress);
            this.AddressTableListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddressTableListView_MouseClick);
            this.AddressTableListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AddressTableListView_MouseDoubleClick);
            // 
            // FrozenHeader
            // 
            this.FrozenHeader.Text = "Frozen";
            this.FrozenHeader.Width = 54;
            // 
            // AddressDescriptionHeader
            // 
            this.AddressDescriptionHeader.Text = "Description";
            this.AddressDescriptionHeader.Width = 146;
            // 
            // AddressHeader
            // 
            this.AddressHeader.Text = "Address";
            this.AddressHeader.Width = 87;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Text = "Type";
            this.TypeHeader.Width = 70;
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 106;
            // 
            // AddressTableContextMenuStrip
            // 
            this.AddressTableContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToggleFreezeToolStripMenuItem,
            this.EditAddressEntryToolStripMenuItem,
            this.DeleteSelectionToolStripMenuItem,
            this.AddNewAddressToolStripMenuItem});
            this.AddressTableContextMenuStrip.Name = "RightClickMenu";
            this.AddressTableContextMenuStrip.Size = new System.Drawing.Size(169, 114);
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
            // GUIAddressTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddressTableListView);
            this.Name = "GUIAddressTable";
            this.Size = new System.Drawing.Size(473, 200);
            this.AddressTableContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CheckableListView AddressTableListView;
        private System.Windows.Forms.ColumnHeader FrozenHeader;
        private System.Windows.Forms.ColumnHeader AddressDescriptionHeader;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.ContextMenuStrip AddressTableContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToggleFreezeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditAddressEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddNewAddressToolStripMenuItem;
    }
}
