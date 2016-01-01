namespace Anathema
{
    partial class GUITable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUITable));
            this.AddressTableListView = new System.Windows.Forms.ListView();
            this.CheckBoxHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScriptTableListView = new System.Windows.Forms.ListView();
            this.ScriptActiveHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScriptDescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.SaveTableButton = new System.Windows.Forms.ToolStripButton();
            this.LoadTableButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddressTableListView
            // 
            this.AddressTableListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressTableListView.CheckBoxes = true;
            this.AddressTableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CheckBoxHeader,
            this.DescriptionHeader,
            this.AddressHeader,
            this.TypeHeader,
            this.ValueHeader});
            this.AddressTableListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressTableListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTableListView.FullRowSelect = true;
            this.AddressTableListView.Location = new System.Drawing.Point(0, 0);
            this.AddressTableListView.Name = "AddressTableListView";
            this.AddressTableListView.ShowGroups = false;
            this.AddressTableListView.Size = new System.Drawing.Size(508, 226);
            this.AddressTableListView.TabIndex = 143;
            this.AddressTableListView.UseCompatibleStateImageBehavior = false;
            this.AddressTableListView.View = System.Windows.Forms.View.Details;
            this.AddressTableListView.VirtualMode = true;
            this.AddressTableListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.AddressTableListView_RetrieveVirtualItem);
            // 
            // CheckBoxHeader
            // 
            this.CheckBoxHeader.Text = "Frozen";
            this.CheckBoxHeader.Width = 54;
            // 
            // DescriptionHeader
            // 
            this.DescriptionHeader.Text = "Description";
            this.DescriptionHeader.Width = 146;
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
            this.ValueHeader.Width = 143;
            // 
            // ScriptTableListView
            // 
            this.ScriptTableListView.BackColor = System.Drawing.SystemColors.Control;
            this.ScriptTableListView.CheckBoxes = true;
            this.ScriptTableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ScriptActiveHeader,
            this.ScriptDescriptionHeader});
            this.ScriptTableListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptTableListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScriptTableListView.FullRowSelect = true;
            this.ScriptTableListView.Location = new System.Drawing.Point(0, 0);
            this.ScriptTableListView.Name = "ScriptTableListView";
            this.ScriptTableListView.ShowGroups = false;
            this.ScriptTableListView.Size = new System.Drawing.Size(186, 226);
            this.ScriptTableListView.TabIndex = 144;
            this.ScriptTableListView.UseCompatibleStateImageBehavior = false;
            this.ScriptTableListView.View = System.Windows.Forms.View.Details;
            this.ScriptTableListView.VirtualMode = true;
            this.ScriptTableListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ScriptTableListView_RetrieveVirtualItem);
            // 
            // ScriptActiveHeader
            // 
            this.ScriptActiveHeader.Text = "Active";
            this.ScriptActiveHeader.Width = 58;
            // 
            // ScriptDescriptionHeader
            // 
            this.ScriptDescriptionHeader.Text = "Description";
            this.ScriptDescriptionHeader.Width = 146;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.AddressTableListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ScriptTableListView);
            this.splitContainer1.Size = new System.Drawing.Size(698, 226);
            this.splitContainer1.SplitterDistance = 508;
            this.splitContainer1.TabIndex = 145;
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveTableButton,
            this.LoadTableButton,
            this.toolStripSeparator1});
            this.ScanToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ScanToolStrip.Location = new System.Drawing.Point(698, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(24, 226);
            this.ScanToolStrip.TabIndex = 150;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // SaveTableButton
            // 
            this.SaveTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveTableButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveTableButton.Image")));
            this.SaveTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveTableButton.Name = "SaveTableButton";
            this.SaveTableButton.Size = new System.Drawing.Size(21, 20);
            this.SaveTableButton.Text = "Start Tree Scan";
            this.SaveTableButton.Click += new System.EventHandler(this.SaveTableButton_Click);
            // 
            // LoadTableButton
            // 
            this.LoadTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LoadTableButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadTableButton.Image")));
            this.LoadTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoadTableButton.Name = "LoadTableButton";
            this.LoadTableButton.Size = new System.Drawing.Size(21, 20);
            this.LoadTableButton.Text = "Stop";
            this.LoadTableButton.ToolTipText = "Stop Tree Scan";
            this.LoadTableButton.Click += new System.EventHandler(this.LoadTableButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(21, 6);
            // 
            // GUITable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 226);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUITable";
            this.Text = "Table";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView AddressTableListView;
        private System.Windows.Forms.ColumnHeader CheckBoxHeader;
        private System.Windows.Forms.ColumnHeader DescriptionHeader;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private System.Windows.Forms.ListView ScriptTableListView;
        private System.Windows.Forms.ColumnHeader ScriptActiveHeader;
        private System.Windows.Forms.ColumnHeader ScriptDescriptionHeader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton SaveTableButton;
        private System.Windows.Forms.ToolStripButton LoadTableButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}