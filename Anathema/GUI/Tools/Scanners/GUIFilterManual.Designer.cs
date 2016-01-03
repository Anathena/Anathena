namespace Anathema
{
    partial class GUIFilterManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIFilterManual));
            this.CompareTypeLabel = new System.Windows.Forms.Label();
            this.ValueLeftTextBox = new System.Windows.Forms.TextBox();
            this.ClearConstraintsButton = new System.Windows.Forms.ToolStripButton();
            this.AddConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.LessThanButton = new System.Windows.Forms.ToolStripButton();
            this.GreaterThanButton = new System.Windows.Forms.ToolStripButton();
            this.EqualButton = new System.Windows.Forms.ToolStripButton();
            this.ValueRightTextBox = new System.Windows.Forms.TextBox();
            this.DecreasedButton = new System.Windows.Forms.ToolStripButton();
            this.NegateButton = new System.Windows.Forms.ToolStripButton();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ScanTypeWorldStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.IncreasedButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ConstraintsListView = new System.Windows.Forms.ListView();
            this.ValueTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueLeftHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueRightHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ConstraintHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilterScientificNotationCheckBox = new System.Windows.Forms.CheckBox();
            this.ChangedButton = new System.Windows.Forms.ToolStripButton();
            this.ScanTypeWorldStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CompareTypeLabel
            // 
            this.CompareTypeLabel.Location = new System.Drawing.Point(9, 27);
            this.CompareTypeLabel.Name = "CompareTypeLabel";
            this.CompareTypeLabel.Size = new System.Drawing.Size(160, 13);
            this.CompareTypeLabel.TabIndex = 154;
            this.CompareTypeLabel.Text = "Not Between or Equal to Values";
            this.CompareTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ValueLeftTextBox
            // 
            this.ValueLeftTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueLeftTextBox.Location = new System.Drawing.Point(12, 68);
            this.ValueLeftTextBox.Name = "ValueLeftTextBox";
            this.ValueLeftTextBox.Size = new System.Drawing.Size(124, 20);
            this.ValueLeftTextBox.TabIndex = 151;
            // 
            // ClearConstraintsButton
            // 
            this.ClearConstraintsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearConstraintsButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearConstraintsButton.Image")));
            this.ClearConstraintsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearConstraintsButton.Name = "ClearConstraintsButton";
            this.ClearConstraintsButton.Size = new System.Drawing.Size(23, 22);
            this.ClearConstraintsButton.Text = "Clear Selections";
            // 
            // AddConstraintButton
            // 
            this.AddConstraintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddConstraintButton.Image = ((System.Drawing.Image)(resources.GetObject("AddConstraintButton.Image")));
            this.AddConstraintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddConstraintButton.Name = "AddConstraintButton";
            this.AddConstraintButton.Size = new System.Drawing.Size(23, 22);
            this.AddConstraintButton.Text = "Add to Filter";
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
            this.LessThanButton.Image = ((System.Drawing.Image)(resources.GetObject("LessThanButton.Image")));
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
            this.GreaterThanButton.Image = ((System.Drawing.Image)(resources.GetObject("GreaterThanButton.Image")));
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
            this.EqualButton.Image = ((System.Drawing.Image)(resources.GetObject("EqualButton.Image")));
            this.EqualButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EqualButton.Name = "EqualButton";
            this.EqualButton.Size = new System.Drawing.Size(23, 22);
            this.EqualButton.Text = "Exact Value";
            this.EqualButton.Click += new System.EventHandler(this.EqualButton_Click);
            // 
            // ValueRightTextBox
            // 
            this.ValueRightTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueRightTextBox.Location = new System.Drawing.Point(142, 68);
            this.ValueRightTextBox.Name = "ValueRightTextBox";
            this.ValueRightTextBox.Size = new System.Drawing.Size(127, 20);
            this.ValueRightTextBox.TabIndex = 152;
            // 
            // DecreasedButton
            // 
            this.DecreasedButton.CheckOnClick = true;
            this.DecreasedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecreasedButton.Image = ((System.Drawing.Image)(resources.GetObject("DecreasedButton.Image")));
            this.DecreasedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecreasedButton.Name = "DecreasedButton";
            this.DecreasedButton.Size = new System.Drawing.Size(23, 22);
            this.DecreasedButton.Text = "Decreased Value";
            this.DecreasedButton.Click += new System.EventHandler(this.DecreasedButton_Click);
            // 
            // NegateButton
            // 
            this.NegateButton.CheckOnClick = true;
            this.NegateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NegateButton.Image = ((System.Drawing.Image)(resources.GetObject("NegateButton.Image")));
            this.NegateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NegateButton.Name = "NegateButton";
            this.NegateButton.Size = new System.Drawing.Size(23, 22);
            this.NegateButton.Text = "Negate Property";
            this.NegateButton.Click += new System.EventHandler(this.NegateButton_Click);
            // 
            // ValueTypeComboBox
            // 
            this.ValueTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueTypeComboBox.FormattingEnabled = true;
            this.ValueTypeComboBox.Location = new System.Drawing.Point(175, 41);
            this.ValueTypeComboBox.Name = "ValueTypeComboBox";
            this.ValueTypeComboBox.Size = new System.Drawing.Size(94, 21);
            this.ValueTypeComboBox.TabIndex = 153;
            // 
            // ScanTypeWorldStrip
            // 
            this.ScanTypeWorldStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanTypeWorldStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartScanButton,
            this.toolStripSeparator7,
            this.NegateButton,
            this.ChangedButton,
            this.IncreasedButton,
            this.DecreasedButton,
            this.EqualButton,
            this.GreaterThanButton,
            this.LessThanButton,
            this.toolStripSeparator1,
            this.AddConstraintButton,
            this.ClearConstraintsButton});
            this.ScanTypeWorldStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanTypeWorldStrip.Name = "ScanTypeWorldStrip";
            this.ScanTypeWorldStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanTypeWorldStrip.Size = new System.Drawing.Size(281, 25);
            this.ScanTypeWorldStrip.TabIndex = 155;
            this.ScanTypeWorldStrip.Text = "toolStrip1";
            // 
            // StartScanButton
            // 
            this.StartScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartScanButton.Image = ((System.Drawing.Image)(resources.GetObject("StartScanButton.Image")));
            this.StartScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartScanButton.Name = "StartScanButton";
            this.StartScanButton.Size = new System.Drawing.Size(23, 22);
            this.StartScanButton.Text = "toolStripButton1";
            this.StartScanButton.Click += new System.EventHandler(this.StartScanButton_Click);
            // 
            // IncreasedButton
            // 
            this.IncreasedButton.CheckOnClick = true;
            this.IncreasedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IncreasedButton.Image = ((System.Drawing.Image)(resources.GetObject("IncreasedButton.Image")));
            this.IncreasedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedButton.Name = "IncreasedButton";
            this.IncreasedButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedButton.Text = "Increased Value";
            this.IncreasedButton.Click += new System.EventHandler(this.IncreasedButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ConstraintsListView
            // 
            this.ConstraintsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstraintsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ValueTypeHeader,
            this.ValueLeftHeader,
            this.ValueRightHeader,
            this.ConstraintHeader});
            this.ConstraintsListView.FullRowSelect = true;
            this.ConstraintsListView.Location = new System.Drawing.Point(12, 130);
            this.ConstraintsListView.Name = "ConstraintsListView";
            this.ConstraintsListView.Size = new System.Drawing.Size(257, 102);
            this.ConstraintsListView.TabIndex = 157;
            this.ConstraintsListView.UseCompatibleStateImageBehavior = false;
            this.ConstraintsListView.View = System.Windows.Forms.View.Details;
            // 
            // ValueTypeHeader
            // 
            this.ValueTypeHeader.Text = "Type";
            this.ValueTypeHeader.Width = 50;
            // 
            // ValueLeftHeader
            // 
            this.ValueLeftHeader.Text = "ValueRight";
            this.ValueLeftHeader.Width = 66;
            // 
            // ValueRightHeader
            // 
            this.ValueRightHeader.Text = "Value Right";
            this.ValueRightHeader.Width = 71;
            // 
            // ConstraintHeader
            // 
            this.ConstraintHeader.Text = "Constraint";
            // 
            // FilterScientificNotationCheckBox
            // 
            this.FilterScientificNotationCheckBox.AutoSize = true;
            this.FilterScientificNotationCheckBox.Location = new System.Drawing.Point(12, 45);
            this.FilterScientificNotationCheckBox.Name = "FilterScientificNotationCheckBox";
            this.FilterScientificNotationCheckBox.Size = new System.Drawing.Size(137, 17);
            this.FilterScientificNotationCheckBox.TabIndex = 158;
            this.FilterScientificNotationCheckBox.Text = "Filter Scientific Notation";
            this.FilterScientificNotationCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChangedButton
            // 
            this.ChangedButton.CheckOnClick = true;
            this.ChangedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ChangedButton.Image = ((System.Drawing.Image)(resources.GetObject("ChangedButton.Image")));
            this.ChangedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChangedButton.Name = "ChangedButton";
            this.ChangedButton.Size = new System.Drawing.Size(23, 22);
            this.ChangedButton.Text = "Changed Value";
            this.ChangedButton.Click += new System.EventHandler(this.ChangedButton_Click);
            // 
            // GUIFilterManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 244);
            this.Controls.Add(this.FilterScientificNotationCheckBox);
            this.Controls.Add(this.ConstraintsListView);
            this.Controls.Add(this.CompareTypeLabel);
            this.Controls.Add(this.ValueLeftTextBox);
            this.Controls.Add(this.ValueRightTextBox);
            this.Controls.Add(this.ValueTypeComboBox);
            this.Controls.Add(this.ScanTypeWorldStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIFilterManual";
            this.Text = "Manual Scanner";
            this.ScanTypeWorldStrip.ResumeLayout(false);
            this.ScanTypeWorldStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CompareTypeLabel;
        private System.Windows.Forms.TextBox ValueLeftTextBox;
        private System.Windows.Forms.ToolStripButton ClearConstraintsButton;
        private System.Windows.Forms.ToolStripButton AddConstraintButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton LessThanButton;
        private System.Windows.Forms.ToolStripButton GreaterThanButton;
        private System.Windows.Forms.ToolStripButton EqualButton;
        private System.Windows.Forms.TextBox ValueRightTextBox;
        private System.Windows.Forms.ToolStripButton DecreasedButton;
        private System.Windows.Forms.ToolStripButton NegateButton;
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.ToolStrip ScanTypeWorldStrip;
        private System.Windows.Forms.ToolStripButton IncreasedButton;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView ConstraintsListView;
        private System.Windows.Forms.ColumnHeader ValueTypeHeader;
        private System.Windows.Forms.ColumnHeader ValueLeftHeader;
        private System.Windows.Forms.ColumnHeader ValueRightHeader;
        private System.Windows.Forms.ColumnHeader ConstraintHeader;
        private System.Windows.Forms.CheckBox FilterScientificNotationCheckBox;
        private System.Windows.Forms.ToolStripButton ChangedButton;
    }
}