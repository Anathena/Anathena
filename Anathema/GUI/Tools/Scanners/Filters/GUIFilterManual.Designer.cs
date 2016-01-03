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
            this.ScanValueTB = new System.Windows.Forms.TextBox();
            this.ENotationButton = new System.Windows.Forms.ToolStripButton();
            this.ClearConstraintsButton = new System.Windows.Forms.ToolStripButton();
            this.AddConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.LessThanValButton = new System.Windows.Forms.ToolStripButton();
            this.GreaterThanValButton = new System.Windows.Forms.ToolStripButton();
            this.EqualValButton = new System.Windows.Forms.ToolStripButton();
            this.ScanValueUpperTB = new System.Windows.Forms.TextBox();
            this.DecreasedValButton = new System.Windows.Forms.ToolStripButton();
            this.NotEqualValButton = new System.Windows.Forms.ToolStripButton();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ScanTypeWorldStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.IncreasedValueButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ConstraintsListView = new System.Windows.Forms.ListView();
            this.ValueTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueLeftHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueRightHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ConstraintHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScanTypeWorldStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CompareTypeLabel
            // 
            this.CompareTypeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CompareTypeLabel.Location = new System.Drawing.Point(12, 25);
            this.CompareTypeLabel.Name = "CompareTypeLabel";
            this.CompareTypeLabel.Size = new System.Drawing.Size(160, 13);
            this.CompareTypeLabel.TabIndex = 154;
            this.CompareTypeLabel.Text = "Not Between or Equal to Values";
            this.CompareTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ScanValueTB
            // 
            this.ScanValueTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanValueTB.Location = new System.Drawing.Point(12, 68);
            this.ScanValueTB.Name = "ScanValueTB";
            this.ScanValueTB.Size = new System.Drawing.Size(124, 20);
            this.ScanValueTB.TabIndex = 151;
            // 
            // ENotationButton
            // 
            this.ENotationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ENotationButton.Image = ((System.Drawing.Image)(resources.GetObject("ENotationButton.Image")));
            this.ENotationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ENotationButton.Name = "ENotationButton";
            this.ENotationButton.Size = new System.Drawing.Size(23, 22);
            this.ENotationButton.Text = "toolStripButton1";
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
            // LessThanValButton
            // 
            this.LessThanValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LessThanValButton.Image = ((System.Drawing.Image)(resources.GetObject("LessThanValButton.Image")));
            this.LessThanValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LessThanValButton.Name = "LessThanValButton";
            this.LessThanValButton.Size = new System.Drawing.Size(23, 22);
            this.LessThanValButton.Text = "Less Than Value";
            // 
            // GreaterThanValButton
            // 
            this.GreaterThanValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GreaterThanValButton.Image = ((System.Drawing.Image)(resources.GetObject("GreaterThanValButton.Image")));
            this.GreaterThanValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GreaterThanValButton.Name = "GreaterThanValButton";
            this.GreaterThanValButton.Size = new System.Drawing.Size(23, 22);
            this.GreaterThanValButton.Text = "Greater Than Value";
            // 
            // EqualValButton
            // 
            this.EqualValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EqualValButton.Image = ((System.Drawing.Image)(resources.GetObject("EqualValButton.Image")));
            this.EqualValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EqualValButton.Name = "EqualValButton";
            this.EqualValButton.Size = new System.Drawing.Size(23, 22);
            this.EqualValButton.Text = "Exact Value";
            // 
            // ScanValueUpperTB
            // 
            this.ScanValueUpperTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanValueUpperTB.Location = new System.Drawing.Point(142, 68);
            this.ScanValueUpperTB.Name = "ScanValueUpperTB";
            this.ScanValueUpperTB.Size = new System.Drawing.Size(127, 20);
            this.ScanValueUpperTB.TabIndex = 152;
            // 
            // DecreasedValButton
            // 
            this.DecreasedValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecreasedValButton.Image = ((System.Drawing.Image)(resources.GetObject("DecreasedValButton.Image")));
            this.DecreasedValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecreasedValButton.Name = "DecreasedValButton";
            this.DecreasedValButton.Size = new System.Drawing.Size(23, 22);
            this.DecreasedValButton.Text = "Decreased Value";
            // 
            // NotEqualValButton
            // 
            this.NotEqualValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NotEqualValButton.Image = ((System.Drawing.Image)(resources.GetObject("NotEqualValButton.Image")));
            this.NotEqualValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NotEqualValButton.Name = "NotEqualValButton";
            this.NotEqualValButton.Size = new System.Drawing.Size(23, 22);
            this.NotEqualValButton.Text = "Negate Property";
            // 
            // ValueTypeComboBox
            // 
            this.ValueTypeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ValueTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueTypeComboBox.FormattingEnabled = true;
            this.ValueTypeComboBox.Location = new System.Drawing.Point(12, 41);
            this.ValueTypeComboBox.Name = "ValueTypeComboBox";
            this.ValueTypeComboBox.Size = new System.Drawing.Size(94, 21);
            this.ValueTypeComboBox.TabIndex = 153;
            // 
            // ScanTypeWorldStrip
            // 
            this.ScanTypeWorldStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanTypeWorldStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator7,
            this.NotEqualValButton,
            this.IncreasedValueButton,
            this.DecreasedValButton,
            this.EqualValButton,
            this.GreaterThanValButton,
            this.LessThanValButton,
            this.toolStripSeparator1,
            this.AddConstraintButton,
            this.ClearConstraintsButton,
            this.ENotationButton});
            this.ScanTypeWorldStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanTypeWorldStrip.Name = "ScanTypeWorldStrip";
            this.ScanTypeWorldStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanTypeWorldStrip.Size = new System.Drawing.Size(281, 25);
            this.ScanTypeWorldStrip.TabIndex = 155;
            this.ScanTypeWorldStrip.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // IncreasedValueButton
            // 
            this.IncreasedValueButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IncreasedValueButton.Image = ((System.Drawing.Image)(resources.GetObject("IncreasedValueButton.Image")));
            this.IncreasedValueButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedValueButton.Name = "IncreasedValueButton";
            this.IncreasedValueButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedValueButton.Text = "Increased Value";
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
            this.ConstraintsListView.Location = new System.Drawing.Point(12, 154);
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
            this.ValueRightHeader.Width = 76;
            // 
            // ConstraintHeader
            // 
            this.ConstraintHeader.Text = "Constraint";
            // 
            // GUIFilterManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 268);
            this.Controls.Add(this.ConstraintsListView);
            this.Controls.Add(this.CompareTypeLabel);
            this.Controls.Add(this.ScanValueTB);
            this.Controls.Add(this.ScanValueUpperTB);
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
        private System.Windows.Forms.TextBox ScanValueTB;
        private System.Windows.Forms.ToolStripButton ENotationButton;
        private System.Windows.Forms.ToolStripButton ClearConstraintsButton;
        private System.Windows.Forms.ToolStripButton AddConstraintButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton LessThanValButton;
        private System.Windows.Forms.ToolStripButton GreaterThanValButton;
        private System.Windows.Forms.ToolStripButton EqualValButton;
        private System.Windows.Forms.TextBox ScanValueUpperTB;
        private System.Windows.Forms.ToolStripButton DecreasedValButton;
        private System.Windows.Forms.ToolStripButton NotEqualValButton;
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.ToolStrip ScanTypeWorldStrip;
        private System.Windows.Forms.ToolStripButton IncreasedValueButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView ConstraintsListView;
        private System.Windows.Forms.ColumnHeader ValueTypeHeader;
        private System.Windows.Forms.ColumnHeader ValueLeftHeader;
        private System.Windows.Forms.ColumnHeader ValueRightHeader;
        private System.Windows.Forms.ColumnHeader ConstraintHeader;
    }
}