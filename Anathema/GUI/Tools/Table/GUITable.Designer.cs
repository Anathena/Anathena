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
            this.CheatTableSplitContainer = new System.Windows.Forms.SplitContainer();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.SaveTableButton = new System.Windows.Forms.ToolStripButton();
            this.LoadTableButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CheatTableButton = new System.Windows.Forms.ToolStripButton();
            this.FSMTableButton = new System.Windows.Forms.ToolStripButton();
            this.AddressTableListView = new Anathema.CheckableListView();
            this.FrozenHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressDescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScriptTableListView = new Anathema.CheckableListView();
            this.ScriptActiveHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScriptDescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FSMTableListView = new Anathema.FlickerFreeListView();
            this.FSMDescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.CheatTableSplitContainer)).BeginInit();
            this.CheatTableSplitContainer.Panel1.SuspendLayout();
            this.CheatTableSplitContainer.Panel2.SuspendLayout();
            this.CheatTableSplitContainer.SuspendLayout();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheatTableSplitContainer
            // 
            this.CheatTableSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheatTableSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.CheatTableSplitContainer.Name = "CheatTableSplitContainer";
            // 
            // CheatTableSplitContainer.Panel1
            // 
            this.CheatTableSplitContainer.Panel1.Controls.Add(this.AddressTableListView);
            // 
            // CheatTableSplitContainer.Panel2
            // 
            this.CheatTableSplitContainer.Panel2.Controls.Add(this.ScriptTableListView);
            this.CheatTableSplitContainer.Size = new System.Drawing.Size(698, 225);
            this.CheatTableSplitContainer.SplitterDistance = 506;
            this.CheatTableSplitContainer.TabIndex = 145;
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveTableButton,
            this.LoadTableButton,
            this.ToolStripSeparator1,
            this.CheatTableButton,
            this.FSMTableButton});
            this.ScanToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ScanToolStrip.Location = new System.Drawing.Point(698, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(24, 225);
            this.ScanToolStrip.TabIndex = 150;
            // 
            // SaveTableButton
            // 
            this.SaveTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveTableButton.Image = global::Anathema.Properties.Resources.Save;
            this.SaveTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveTableButton.Name = "SaveTableButton";
            this.SaveTableButton.Size = new System.Drawing.Size(21, 20);
            this.SaveTableButton.Text = "Save Table";
            this.SaveTableButton.Click += new System.EventHandler(this.SaveTableButton_Click);
            // 
            // LoadTableButton
            // 
            this.LoadTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LoadTableButton.Image = global::Anathema.Properties.Resources.Open;
            this.LoadTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoadTableButton.Name = "LoadTableButton";
            this.LoadTableButton.Size = new System.Drawing.Size(21, 20);
            this.LoadTableButton.Text = "Open Table";
            this.LoadTableButton.ToolTipText = "Stop Tree Scan";
            this.LoadTableButton.Click += new System.EventHandler(this.LoadTableButton_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(21, 6);
            // 
            // CheatTableButton
            // 
            this.CheatTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CheatTableButton.Image = global::Anathema.Properties.Resources.BenedictionIcon;
            this.CheatTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CheatTableButton.Name = "CheatTableButton";
            this.CheatTableButton.Size = new System.Drawing.Size(21, 20);
            this.CheatTableButton.Text = "Cheat Table";
            this.CheatTableButton.Click += new System.EventHandler(this.CheatTableButton_Click);
            // 
            // FSMTableButton
            // 
            this.FSMTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FSMTableButton.Image = global::Anathema.Properties.Resources.CelestialIcon;
            this.FSMTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FSMTableButton.Name = "FSMTableButton";
            this.FSMTableButton.Size = new System.Drawing.Size(21, 20);
            this.FSMTableButton.Text = "FSM Table";
            this.FSMTableButton.Click += new System.EventHandler(this.FSMTableButton_Click);
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
            this.AddressTableListView.Size = new System.Drawing.Size(506, 225);
            this.AddressTableListView.TabIndex = 143;
            this.AddressTableListView.UseCompatibleStateImageBehavior = false;
            this.AddressTableListView.View = System.Windows.Forms.View.Details;
            this.AddressTableListView.VirtualMode = true;
            this.AddressTableListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.AddressTableListView_RetrieveVirtualItem);
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
            this.ScriptTableListView.OwnerDraw = true;
            this.ScriptTableListView.ShowGroups = false;
            this.ScriptTableListView.Size = new System.Drawing.Size(188, 225);
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
            // FSMTableListView
            // 
            this.FSMTableListView.BackColor = System.Drawing.SystemColors.Control;
            this.FSMTableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FSMDescriptionHeader});
            this.FSMTableListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FSMTableListView.Location = new System.Drawing.Point(0, 0);
            this.FSMTableListView.Name = "FSMTableListView";
            this.FSMTableListView.Size = new System.Drawing.Size(722, 225);
            this.FSMTableListView.TabIndex = 151;
            this.FSMTableListView.UseCompatibleStateImageBehavior = false;
            this.FSMTableListView.View = System.Windows.Forms.View.Details;
            this.FSMTableListView.VirtualMode = true;
            this.FSMTableListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.FSMTableListView_RetrieveVirtualItem);
            // 
            // FSMDescriptionHeader
            // 
            this.FSMDescriptionHeader.Text = "Description";
            this.FSMDescriptionHeader.Width = 663;
            // 
            // GUITable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 225);
            this.Controls.Add(this.CheatTableSplitContainer);
            this.Controls.Add(this.ScanToolStrip);
            this.Controls.Add(this.FSMTableListView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUITable";
            this.Text = "Table";
            this.CheatTableSplitContainer.Panel1.ResumeLayout(false);
            this.CheatTableSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CheatTableSplitContainer)).EndInit();
            this.CheatTableSplitContainer.ResumeLayout(false);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckableListView AddressTableListView;
        private System.Windows.Forms.ColumnHeader FrozenHeader;
        private System.Windows.Forms.ColumnHeader AddressDescriptionHeader;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private CheckableListView ScriptTableListView;
        private System.Windows.Forms.ColumnHeader ScriptActiveHeader;
        private System.Windows.Forms.ColumnHeader ScriptDescriptionHeader;
        private System.Windows.Forms.SplitContainer CheatTableSplitContainer;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton SaveTableButton;
        private System.Windows.Forms.ToolStripButton LoadTableButton;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton CheatTableButton;
        private System.Windows.Forms.ToolStripButton FSMTableButton;
        private FlickerFreeListView FSMTableListView;
        private System.Windows.Forms.ColumnHeader FSMDescriptionHeader;
    }
}