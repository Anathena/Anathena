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
            this.CompareTypeLabel = new System.Windows.Forms.Label();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.LessThanButton = new System.Windows.Forms.ToolStripButton();
            this.GreaterThanButton = new System.Windows.Forms.ToolStripButton();
            this.EqualButton = new System.Windows.Forms.ToolStripButton();
            this.DecreasedButton = new System.Windows.Forms.ToolStripButton();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.AddConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.ClearConstraintsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.UnchangedButton = new System.Windows.Forms.ToolStripButton();
            this.ChangedButton = new System.Windows.Forms.ToolStripButton();
            this.IncreasedButton = new System.Windows.Forms.ToolStripButton();
            this.NotEqualButton = new System.Windows.Forms.ToolStripButton();
            this.IncreasedByXButton = new System.Windows.Forms.ToolStripButton();
            this.DecreasedByXButton = new System.Windows.Forms.ToolStripButton();
            this.ConstraintsListView = new System.Windows.Forms.ListView();
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ConstraintHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilterScientificNotationCheckBox = new System.Windows.Forms.CheckBox();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CompareTypeLabel
            // 
            this.CompareTypeLabel.Location = new System.Drawing.Point(275, 55);
            this.CompareTypeLabel.Name = "CompareTypeLabel";
            this.CompareTypeLabel.Size = new System.Drawing.Size(95, 13);
            this.CompareTypeLabel.TabIndex = 154;
            this.CompareTypeLabel.Text = "Invalid";
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTextBox.Location = new System.Drawing.Point(12, 28);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(358, 20);
            this.ValueTextBox.TabIndex = 151;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // LessThanButton
            // 
            this.LessThanButton.CheckOnClick = true;
            this.LessThanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LessThanButton.Image = global::Anathema.Properties.Resources.LessThan;
            this.LessThanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LessThanButton.Name = "LessThanButton";
            this.LessThanButton.Size = new System.Drawing.Size(23, 22);
            this.LessThanButton.Text = "Less Than Value";
            this.LessThanButton.Click += new System.EventHandler(this.LessThanButton_Click);
            // 
            // GreaterThanButton
            // 
            this.GreaterThanButton.CheckOnClick = true;
            this.GreaterThanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GreaterThanButton.Image = global::Anathema.Properties.Resources.GreaterThan;
            this.GreaterThanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GreaterThanButton.Name = "GreaterThanButton";
            this.GreaterThanButton.Size = new System.Drawing.Size(23, 22);
            this.GreaterThanButton.Text = "Greater Than Value";
            this.GreaterThanButton.Click += new System.EventHandler(this.GreaterThanButton_Click);
            // 
            // EqualButton
            // 
            this.EqualButton.CheckOnClick = true;
            this.EqualButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EqualButton.Image = global::Anathema.Properties.Resources.Equal;
            this.EqualButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EqualButton.Name = "EqualButton";
            this.EqualButton.Size = new System.Drawing.Size(23, 22);
            this.EqualButton.Text = "Exact Value";
            this.EqualButton.Click += new System.EventHandler(this.EqualButton_Click);
            // 
            // DecreasedButton
            // 
            this.DecreasedButton.CheckOnClick = true;
            this.DecreasedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecreasedButton.Image = global::Anathema.Properties.Resources.Decreased;
            this.DecreasedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecreasedButton.Name = "DecreasedButton";
            this.DecreasedButton.Size = new System.Drawing.Size(23, 22);
            this.DecreasedButton.Text = "Decreased Value";
            this.DecreasedButton.Click += new System.EventHandler(this.DecreasedButton_Click);
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
            this.UnchangedButton,
            this.ChangedButton,
            this.IncreasedButton,
            this.DecreasedButton,
            this.NotEqualButton,
            this.EqualButton,
            this.GreaterThanButton,
            this.LessThanButton,
            this.IncreasedByXButton,
            this.DecreasedByXButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(382, 25);
            this.ScanToolStrip.TabIndex = 155;
            this.ScanToolStrip.Text = "toolStrip1";
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
            this.ClearConstraintsButton.Text = "toolStripButton3";
            this.ClearConstraintsButton.Click += new System.EventHandler(this.ClearConstraintsButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // UnchangedButton
            // 
            this.UnchangedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UnchangedButton.Image = global::Anathema.Properties.Resources.Unchanged;
            this.UnchangedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UnchangedButton.Name = "UnchangedButton";
            this.UnchangedButton.Size = new System.Drawing.Size(23, 22);
            this.UnchangedButton.Text = "Unchanged";
            this.UnchangedButton.Click += new System.EventHandler(this.UnchangedButton_Click);
            // 
            // ChangedButton
            // 
            this.ChangedButton.CheckOnClick = true;
            this.ChangedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ChangedButton.Image = global::Anathema.Properties.Resources.Changed;
            this.ChangedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChangedButton.Name = "ChangedButton";
            this.ChangedButton.Size = new System.Drawing.Size(23, 22);
            this.ChangedButton.Text = "Changed Value";
            this.ChangedButton.Click += new System.EventHandler(this.ChangedButton_Click);
            // 
            // IncreasedButton
            // 
            this.IncreasedButton.CheckOnClick = true;
            this.IncreasedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IncreasedButton.Image = global::Anathema.Properties.Resources.Increased;
            this.IncreasedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedButton.Name = "IncreasedButton";
            this.IncreasedButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedButton.Text = "Increased Value";
            this.IncreasedButton.Click += new System.EventHandler(this.IncreasedButton_Click);
            // 
            // NotEqualButton
            // 
            this.NotEqualButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NotEqualButton.Image = global::Anathema.Properties.Resources.NotEqual;
            this.NotEqualButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NotEqualButton.Name = "NotEqualButton";
            this.NotEqualButton.Size = new System.Drawing.Size(23, 22);
            this.NotEqualButton.Text = "Not Equal";
            this.NotEqualButton.Click += new System.EventHandler(this.NotEqualButton_Click);
            // 
            // IncreasedByXButton
            // 
            this.IncreasedByXButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IncreasedByXButton.Image = global::Anathema.Properties.Resources.PlusX;
            this.IncreasedByXButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedByXButton.Name = "IncreasedByXButton";
            this.IncreasedByXButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedByXButton.Text = "Increase By X";
            this.IncreasedByXButton.Click += new System.EventHandler(this.IncreasedByXButton_Click);
            // 
            // DecreasedByXButton
            // 
            this.DecreasedByXButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecreasedByXButton.Image = global::Anathema.Properties.Resources.MinusX;
            this.DecreasedByXButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecreasedByXButton.Name = "DecreasedByXButton";
            this.DecreasedByXButton.Size = new System.Drawing.Size(23, 22);
            this.DecreasedByXButton.Text = "Decrease By X";
            this.DecreasedByXButton.Click += new System.EventHandler(this.DecreasedByXButton_Click);
            // 
            // ConstraintsListView
            // 
            this.ConstraintsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstraintsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ValueHeader,
            this.ConstraintHeader});
            this.ConstraintsListView.FullRowSelect = true;
            this.ConstraintsListView.Location = new System.Drawing.Point(12, 92);
            this.ConstraintsListView.Name = "ConstraintsListView";
            this.ConstraintsListView.Size = new System.Drawing.Size(358, 132);
            this.ConstraintsListView.TabIndex = 157;
            this.ConstraintsListView.UseCompatibleStateImageBehavior = false;
            this.ConstraintsListView.View = System.Windows.Forms.View.Details;
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 147;
            // 
            // ConstraintHeader
            // 
            this.ConstraintHeader.Text = "Constraint";
            this.ConstraintHeader.Width = 144;
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
            // GUIManualScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 236);
            this.Controls.Add(this.FilterScientificNotationCheckBox);
            this.Controls.Add(this.ConstraintsListView);
            this.Controls.Add(this.CompareTypeLabel);
            this.Controls.Add(this.ValueTextBox);
            this.Controls.Add(this.ValueTypeComboBox);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIManualScanner";
            this.Text = "Manual Scanner";
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CompareTypeLabel;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton LessThanButton;
        private System.Windows.Forms.ToolStripButton GreaterThanButton;
        private System.Windows.Forms.ToolStripButton EqualButton;
        private System.Windows.Forms.ToolStripButton DecreasedButton;
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton IncreasedButton;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ListView ConstraintsListView;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.ColumnHeader ConstraintHeader;
        private System.Windows.Forms.CheckBox FilterScientificNotationCheckBox;
        private System.Windows.Forms.ToolStripButton ChangedButton;
        private System.Windows.Forms.ToolStripButton UnchangedButton;
        private System.Windows.Forms.ToolStripButton NotEqualButton;
        private System.Windows.Forms.ToolStripButton DecreasedByXButton;
        private System.Windows.Forms.ToolStripButton IncreasedByXButton;
        private System.Windows.Forms.ToolStripButton AddConstraintButton;
        private System.Windows.Forms.ToolStripButton RemoveConstraintButton;
        private System.Windows.Forms.ToolStripButton ClearConstraintsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}