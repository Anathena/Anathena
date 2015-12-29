namespace Anathema
{
    partial class GUIResults
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
            this.AddressCount = new System.Windows.Forms.Label();
            this.VariableSizeValueLabel = new System.Windows.Forms.Label();
            this.ResultsListView = new Anathema.FlickerFreeListView();
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ChangeTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ByteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Int16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Int32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Int64ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeSignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SingleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DoubleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddressCount
            // 
            this.AddressCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AddressCount.Location = new System.Drawing.Point(0, 244);
            this.AddressCount.Name = "AddressCount";
            this.AddressCount.Size = new System.Drawing.Size(284, 17);
            this.AddressCount.TabIndex = 152;
            this.AddressCount.Text = "Active Snapshot Size:";
            // 
            // VariableSizeValueLabel
            // 
            this.VariableSizeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VariableSizeValueLabel.AutoSize = true;
            this.VariableSizeValueLabel.Location = new System.Drawing.Point(109, 245);
            this.VariableSizeValueLabel.Name = "VariableSizeValueLabel";
            this.VariableSizeValueLabel.Size = new System.Drawing.Size(20, 13);
            this.VariableSizeValueLabel.TabIndex = 154;
            this.VariableSizeValueLabel.Text = "0B";
            // 
            // ResultsListView
            // 
            this.ResultsListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.ResultsListView.BackColor = System.Drawing.SystemColors.Control;
            this.ResultsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AddressHeader,
            this.ValueHeader,
            this.LabelHeader});
            this.ResultsListView.ContextMenuStrip = this.RightClickMenu;
            this.ResultsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultsListView.FullRowSelect = true;
            this.ResultsListView.Location = new System.Drawing.Point(0, 0);
            this.ResultsListView.Name = "ResultsListView";
            this.ResultsListView.Size = new System.Drawing.Size(284, 261);
            this.ResultsListView.TabIndex = 151;
            this.ResultsListView.UseCompatibleStateImageBehavior = false;
            this.ResultsListView.View = System.Windows.Forms.View.Details;
            // 
            // AddressHeader
            // 
            this.AddressHeader.Text = "Address";
            this.AddressHeader.Width = 86;
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 104;
            // 
            // LabelHeader
            // 
            this.LabelHeader.Text = "Label";
            this.LabelHeader.Width = 88;
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeTypeToolStripMenuItem,
            this.ChangeSignToolStripMenuItem});
            this.RightClickMenu.Name = "RightClickMenu";
            this.RightClickMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // ChangeTypeToolStripMenuItem
            // 
            this.ChangeTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ByteToolStripMenuItem,
            this.Int16ToolStripMenuItem,
            this.Int32ToolStripMenuItem,
            this.Int64ToolStripMenuItem,
            this.SingleToolStripMenuItem,
            this.DoubleToolStripMenuItem});
            this.ChangeTypeToolStripMenuItem.Name = "ChangeTypeToolStripMenuItem";
            this.ChangeTypeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ChangeTypeToolStripMenuItem.Text = "Change Type";
            // 
            // ByteToolStripMenuItem
            // 
            this.ByteToolStripMenuItem.Name = "ByteToolStripMenuItem";
            this.ByteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ByteToolStripMenuItem.Text = "Byte";
            this.ByteToolStripMenuItem.Click += new System.EventHandler(this.ByteToolStripMenuItem_Click);
            // 
            // Int16ToolStripMenuItem
            // 
            this.Int16ToolStripMenuItem.Name = "Int16ToolStripMenuItem";
            this.Int16ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.Int16ToolStripMenuItem.Text = "Int16";
            this.Int16ToolStripMenuItem.Click += new System.EventHandler(this.Int16ToolStripMenuItem_Click);
            // 
            // Int32ToolStripMenuItem
            // 
            this.Int32ToolStripMenuItem.Name = "Int32ToolStripMenuItem";
            this.Int32ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.Int32ToolStripMenuItem.Text = "Int32";
            this.Int32ToolStripMenuItem.Click += new System.EventHandler(this.Int32ToolStripMenuItem_Click);
            // 
            // Int64ToolStripMenuItem
            // 
            this.Int64ToolStripMenuItem.Name = "Int64ToolStripMenuItem";
            this.Int64ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.Int64ToolStripMenuItem.Text = "Int64";
            this.Int64ToolStripMenuItem.Click += new System.EventHandler(this.Int64ToolStripMenuItem_Click);
            // 
            // ChangeSignToolStripMenuItem
            // 
            this.ChangeSignToolStripMenuItem.Name = "ChangeSignToolStripMenuItem";
            this.ChangeSignToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ChangeSignToolStripMenuItem.Text = "Change Sign";
            this.ChangeSignToolStripMenuItem.Click += new System.EventHandler(this.ChangeSignToolStripMenuItem_Click);
            // 
            // SingleToolStripMenuItem
            // 
            this.SingleToolStripMenuItem.Name = "SingleToolStripMenuItem";
            this.SingleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SingleToolStripMenuItem.Text = "Single";
            this.SingleToolStripMenuItem.Click += new System.EventHandler(this.SingleToolStripMenuItem_Click);
            // 
            // DoubleToolStripMenuItem
            // 
            this.DoubleToolStripMenuItem.Name = "DoubleToolStripMenuItem";
            this.DoubleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DoubleToolStripMenuItem.Text = "Double";
            this.DoubleToolStripMenuItem.Click += new System.EventHandler(this.DoubleToolStripMenuItem_Click);
            // 
            // GUIResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.VariableSizeValueLabel);
            this.Controls.Add(this.AddressCount);
            this.Controls.Add(this.ResultsListView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIResults";
            this.Text = "Results";
            this.RightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlickerFreeListView ResultsListView;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.ColumnHeader LabelHeader;
        private System.Windows.Forms.Label AddressCount;
        private System.Windows.Forms.Label VariableSizeValueLabel;
        private System.Windows.Forms.ContextMenuStrip RightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem ChangeTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ByteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Int16ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Int32ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Int64ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SingleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DoubleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeSignToolStripMenuItem;
    }
}