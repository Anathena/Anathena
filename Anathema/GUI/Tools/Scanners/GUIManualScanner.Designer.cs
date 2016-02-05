namespace Anathema
{
    partial class GUIManualScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIManualScanner));
            this.ValueTextBox = new Anathema.HexDecTextBox();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ConstraintsListView = new System.Windows.Forms.ListView();
            this.ConstraintHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilterScientificNotationCheckBox = new System.Windows.Forms.CheckBox();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.AddConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.ClearConstraintsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ScanOptionsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ChangedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnchangedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IncreasedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecreasedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EqualToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NotEqualToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IncreasedByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecreasedByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GreaterThanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GreaterThanOrEqualToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LessThanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LessThanOrEqualToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTextBox.ForeColor = System.Drawing.Color.Red;
            this.ValueTextBox.IsHex = false;
            this.ValueTextBox.Location = new System.Drawing.Point(12, 28);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(407, 20);
            this.ValueTextBox.TabIndex = 151;
            this.ValueTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.ValueTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueTextBox.WaterMarkText = null;
            // 
            // ValueTypeComboBox
            // 
            this.ValueTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueTypeComboBox.FormattingEnabled = true;
            this.ValueTypeComboBox.Location = new System.Drawing.Point(12, 52);
            this.ValueTypeComboBox.Name = "ValueTypeComboBox";
            this.ValueTypeComboBox.Size = new System.Drawing.Size(94, 21);
            this.ValueTypeComboBox.TabIndex = 153;
            this.ValueTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ValueTypeComboBox_SelectedIndexChanged);
            // 
            // ConstraintsListView
            // 
            this.ConstraintsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstraintsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ConstraintHeader});
            this.ConstraintsListView.FullRowSelect = true;
            this.ConstraintsListView.Location = new System.Drawing.Point(12, 92);
            this.ConstraintsListView.Name = "ConstraintsListView";
            this.ConstraintsListView.Size = new System.Drawing.Size(407, 132);
            this.ConstraintsListView.TabIndex = 157;
            this.ConstraintsListView.UseCompatibleStateImageBehavior = false;
            this.ConstraintsListView.View = System.Windows.Forms.View.Details;
            // 
            // ConstraintHeader
            // 
            this.ConstraintHeader.Text = "Constraint";
            this.ConstraintHeader.Width = 397;
            // 
            // FilterScientificNotationCheckBox
            // 
            this.FilterScientificNotationCheckBox.AutoSize = true;
            this.FilterScientificNotationCheckBox.Location = new System.Drawing.Point(132, 54);
            this.FilterScientificNotationCheckBox.Name = "FilterScientificNotationCheckBox";
            this.FilterScientificNotationCheckBox.Size = new System.Drawing.Size(137, 17);
            this.FilterScientificNotationCheckBox.TabIndex = 158;
            this.FilterScientificNotationCheckBox.Text = "Filter Scientific Notation";
            this.FilterScientificNotationCheckBox.UseVisualStyleBackColor = true;
            // 
            // StartScanButton
            // 
            this.StartScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartScanButton.Image = global::Anathema.Properties.Resources.RightArrow;
            this.StartScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartScanButton.Name = "StartScanButton";
            this.StartScanButton.Size = new System.Drawing.Size(23, 22);
            this.StartScanButton.Text = "Start Scan";
            this.StartScanButton.Click += new System.EventHandler(this.StartScanButton_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // AddConstraintButton
            // 
            this.AddConstraintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddConstraintButton.Image = global::Anathema.Properties.Resources.DownArrows;
            this.AddConstraintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddConstraintButton.Name = "AddConstraintButton";
            this.AddConstraintButton.Size = new System.Drawing.Size(23, 22);
            this.AddConstraintButton.Text = "Add Constraint";
            this.AddConstraintButton.Click += new System.EventHandler(this.AddConstraintButton_Click);
            // 
            // RemoveConstraintButton
            // 
            this.RemoveConstraintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveConstraintButton.Image = global::Anathema.Properties.Resources.X;
            this.RemoveConstraintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveConstraintButton.Name = "RemoveConstraintButton";
            this.RemoveConstraintButton.Size = new System.Drawing.Size(23, 22);
            this.RemoveConstraintButton.Text = "Remove Selected Constraints";
            this.RemoveConstraintButton.Click += new System.EventHandler(this.RemoveConstraintButton_Click);
            // 
            // ClearConstraintsButton
            // 
            this.ClearConstraintsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearConstraintsButton.Image = global::Anathema.Properties.Resources.Cancel;
            this.ClearConstraintsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearConstraintsButton.Name = "ClearConstraintsButton";
            this.ClearConstraintsButton.Size = new System.Drawing.Size(23, 22);
            this.ClearConstraintsButton.Text = "Clear Constraints";
            this.ClearConstraintsButton.Click += new System.EventHandler(this.ClearConstraintsButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ScanOptionsToolStripDropDownButton
            // 
            this.ScanOptionsToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScanOptionsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangedToolStripMenuItem,
            this.UnchangedToolStripMenuItem,
            this.IncreasedToolStripMenuItem,
            this.DecreasedToolStripMenuItem,
            this.EqualToToolStripMenuItem,
            this.NotEqualToToolStripMenuItem,
            this.IncreasedByToolStripMenuItem,
            this.DecreasedByToolStripMenuItem,
            this.GreaterThanToolStripMenuItem,
            this.GreaterThanOrEqualToToolStripMenuItem,
            this.LessThanToolStripMenuItem,
            this.LessThanOrEqualToToolStripMenuItem});
            this.ScanOptionsToolStripDropDownButton.Image = global::Anathema.Properties.Resources.Changed;
            this.ScanOptionsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanOptionsToolStripDropDownButton.Name = "ScanOptionsToolStripDropDownButton";
            this.ScanOptionsToolStripDropDownButton.Size = new System.Drawing.Size(29, 22);
            this.ScanOptionsToolStripDropDownButton.Text = "toolStripDropDownButton1";
            // 
            // ChangedToolStripMenuItem
            // 
            this.ChangedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Changed;
            this.ChangedToolStripMenuItem.Name = "ChangedToolStripMenuItem";
            this.ChangedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.ChangedToolStripMenuItem.Text = "Changed";
            this.ChangedToolStripMenuItem.Click += new System.EventHandler(this.ChangedToolStripMenuItem_Click);
            // 
            // UnchangedToolStripMenuItem
            // 
            this.UnchangedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Unchanged;
            this.UnchangedToolStripMenuItem.Name = "UnchangedToolStripMenuItem";
            this.UnchangedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.UnchangedToolStripMenuItem.Text = "Unchanged";
            this.UnchangedToolStripMenuItem.Click += new System.EventHandler(this.UnchangedToolStripMenuItem_Click);
            // 
            // IncreasedToolStripMenuItem
            // 
            this.IncreasedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Increased;
            this.IncreasedToolStripMenuItem.Name = "IncreasedToolStripMenuItem";
            this.IncreasedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.IncreasedToolStripMenuItem.Text = "Increased";
            this.IncreasedToolStripMenuItem.Click += new System.EventHandler(this.IncreasedToolStripMenuItem_Click);
            // 
            // DecreasedToolStripMenuItem
            // 
            this.DecreasedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Decreased;
            this.DecreasedToolStripMenuItem.Name = "DecreasedToolStripMenuItem";
            this.DecreasedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.DecreasedToolStripMenuItem.Text = "Decreased";
            this.DecreasedToolStripMenuItem.Click += new System.EventHandler(this.DecreasedToolStripMenuItem_Click);
            // 
            // EqualToToolStripMenuItem
            // 
            this.EqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.Equal;
            this.EqualToToolStripMenuItem.Name = "EqualToToolStripMenuItem";
            this.EqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.EqualToToolStripMenuItem.Text = "Equal to";
            this.EqualToToolStripMenuItem.Click += new System.EventHandler(this.EqualToToolStripMenuItem_Click);
            // 
            // NotEqualToToolStripMenuItem
            // 
            this.NotEqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.NotEqual;
            this.NotEqualToToolStripMenuItem.Name = "NotEqualToToolStripMenuItem";
            this.NotEqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.NotEqualToToolStripMenuItem.Text = "Not Equal to";
            this.NotEqualToToolStripMenuItem.Click += new System.EventHandler(this.NotEqualToToolStripMenuItem_Click);
            // 
            // IncreasedByToolStripMenuItem
            // 
            this.IncreasedByToolStripMenuItem.Image = global::Anathema.Properties.Resources.PlusX;
            this.IncreasedByToolStripMenuItem.Name = "IncreasedByToolStripMenuItem";
            this.IncreasedByToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.IncreasedByToolStripMenuItem.Text = "Increased by";
            this.IncreasedByToolStripMenuItem.Click += new System.EventHandler(this.IncreasedByToolStripMenuItem_Click);
            // 
            // DecreasedByToolStripMenuItem
            // 
            this.DecreasedByToolStripMenuItem.Image = global::Anathema.Properties.Resources.MinusX;
            this.DecreasedByToolStripMenuItem.Name = "DecreasedByToolStripMenuItem";
            this.DecreasedByToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.DecreasedByToolStripMenuItem.Text = "Decreased by";
            this.DecreasedByToolStripMenuItem.Click += new System.EventHandler(this.DecreasedByToolStripMenuItem_Click);
            // 
            // GreaterThanToolStripMenuItem
            // 
            this.GreaterThanToolStripMenuItem.Image = global::Anathema.Properties.Resources.GreaterThan;
            this.GreaterThanToolStripMenuItem.Name = "GreaterThanToolStripMenuItem";
            this.GreaterThanToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.GreaterThanToolStripMenuItem.Text = "Greater Than";
            this.GreaterThanToolStripMenuItem.Click += new System.EventHandler(this.GreaterThanToolStripMenuItem_Click);
            // 
            // GreaterThanOrEqualToToolStripMenuItem
            // 
            this.GreaterThanOrEqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.GreaterThanOrEqual;
            this.GreaterThanOrEqualToToolStripMenuItem.Name = "GreaterThanOrEqualToToolStripMenuItem";
            this.GreaterThanOrEqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.GreaterThanOrEqualToToolStripMenuItem.Text = "Greater Than or Equal to";
            this.GreaterThanOrEqualToToolStripMenuItem.Click += new System.EventHandler(this.GreaterThanOrEqualToToolStripMenuItem_Click);
            // 
            // LessThanToolStripMenuItem
            // 
            this.LessThanToolStripMenuItem.Image = global::Anathema.Properties.Resources.LessThan;
            this.LessThanToolStripMenuItem.Name = "LessThanToolStripMenuItem";
            this.LessThanToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.LessThanToolStripMenuItem.Text = "Less Than";
            this.LessThanToolStripMenuItem.Click += new System.EventHandler(this.LessThanToolStripMenuItem_Click);
            // 
            // LessThanOrEqualToToolStripMenuItem
            // 
            this.LessThanOrEqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.LessThanOrEqual;
            this.LessThanOrEqualToToolStripMenuItem.Name = "LessThanOrEqualToToolStripMenuItem";
            this.LessThanOrEqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.LessThanOrEqualToToolStripMenuItem.Text = "Less Than or Equal to";
            this.LessThanOrEqualToToolStripMenuItem.Click += new System.EventHandler(this.LessThanOrEqualToToolStripMenuItem_Click);
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartScanButton,
            this.toolStripSeparator7,
            this.AddConstraintButton,
            this.RemoveConstraintButton,
            this.ClearConstraintsButton,
            this.toolStripSeparator1,
            this.ScanOptionsToolStripDropDownButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(431, 25);
            this.ScanToolStrip.TabIndex = 155;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // GUIManualScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 236);
            this.Controls.Add(this.FilterScientificNotationCheckBox);
            this.Controls.Add(this.ConstraintsListView);
            this.Controls.Add(this.ValueTextBox);
            this.Controls.Add(this.ValueTypeComboBox);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIManualScanner";
            this.Text = "Manual Scanner";
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private HexDecTextBox ValueTextBox;
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.ListView ConstraintsListView;
        private System.Windows.Forms.ColumnHeader ConstraintHeader;
        private System.Windows.Forms.CheckBox FilterScientificNotationCheckBox;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton AddConstraintButton;
        private System.Windows.Forms.ToolStripButton RemoveConstraintButton;
        private System.Windows.Forms.ToolStripButton ClearConstraintsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton ScanOptionsToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem ChangedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UnchangedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IncreasedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DecreasedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IncreasedByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DecreasedByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GreaterThanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GreaterThanOrEqualToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LessThanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LessThanOrEqualToToolStripMenuItem;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripMenuItem EqualToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NotEqualToToolStripMenuItem;
    }
}