namespace Anathema
{
    partial class GUIFilterFSM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIFilterFSM));
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ScanTypeWorldStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.RemoveConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.UnchangedButton = new System.Windows.Forms.ToolStripButton();
            this.ChangedButton = new System.Windows.Forms.ToolStripButton();
            this.IncreasedButton = new System.Windows.Forms.ToolStripButton();
            this.DecreasedButton = new System.Windows.Forms.ToolStripButton();
            this.NotEqualButton = new System.Windows.Forms.ToolStripButton();
            this.EqualButton = new System.Windows.Forms.ToolStripButton();
            this.GreaterThanButton = new System.Windows.Forms.ToolStripButton();
            this.LessThanButton = new System.Windows.Forms.ToolStripButton();
            this.IncreasedByXButton = new System.Windows.Forms.ToolStripButton();
            this.DecreasedByXButton = new System.Windows.Forms.ToolStripButton();
            this.FSMBuilderPanel = new System.Windows.Forms.Panel();
            this.ScanTypeWorldStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTextBox.Location = new System.Drawing.Point(121, 28);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(336, 20);
            this.ValueTextBox.TabIndex = 159;
            // 
            // ValueTypeComboBox
            // 
            this.ValueTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueTypeComboBox.FormattingEnabled = true;
            this.ValueTypeComboBox.Location = new System.Drawing.Point(12, 28);
            this.ValueTypeComboBox.Name = "ValueTypeComboBox";
            this.ValueTypeComboBox.Size = new System.Drawing.Size(94, 21);
            this.ValueTypeComboBox.TabIndex = 160;
            // 
            // ScanTypeWorldStrip
            // 
            this.ScanTypeWorldStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanTypeWorldStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartScanButton,
            this.toolStripSeparator7,
            this.RemoveConstraintButton,
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
            this.ScanTypeWorldStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ScanTypeWorldStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanTypeWorldStrip.Name = "ScanTypeWorldStrip";
            this.ScanTypeWorldStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanTypeWorldStrip.Size = new System.Drawing.Size(469, 25);
            this.ScanTypeWorldStrip.TabIndex = 162;
            this.ScanTypeWorldStrip.Text = "toolStrip1";
            // 
            // StartScanButton
            // 
            this.StartScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartScanButton.Image = ((System.Drawing.Image)(resources.GetObject("StartScanButton.Image")));
            this.StartScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartScanButton.Name = "StartScanButton";
            this.StartScanButton.Size = new System.Drawing.Size(23, 22);
            this.StartScanButton.Text = "Start Scan";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // RemoveConstraintButton
            // 
            this.RemoveConstraintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveConstraintButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveConstraintButton.Image")));
            this.RemoveConstraintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveConstraintButton.Name = "RemoveConstraintButton";
            this.RemoveConstraintButton.Size = new System.Drawing.Size(23, 22);
            this.RemoveConstraintButton.Text = "Remove Selected Constraints";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // UnchangedButton
            // 
            this.UnchangedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UnchangedButton.Image = ((System.Drawing.Image)(resources.GetObject("UnchangedButton.Image")));
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
            this.ChangedButton.Image = ((System.Drawing.Image)(resources.GetObject("ChangedButton.Image")));
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
            this.IncreasedButton.Image = ((System.Drawing.Image)(resources.GetObject("IncreasedButton.Image")));
            this.IncreasedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedButton.Name = "IncreasedButton";
            this.IncreasedButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedButton.Text = "Increased Value";
            this.IncreasedButton.Click += new System.EventHandler(this.IncreasedButton_Click);
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
            // NotEqualButton
            // 
            this.NotEqualButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NotEqualButton.Image = ((System.Drawing.Image)(resources.GetObject("NotEqualButton.Image")));
            this.NotEqualButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NotEqualButton.Name = "NotEqualButton";
            this.NotEqualButton.Size = new System.Drawing.Size(23, 22);
            this.NotEqualButton.Text = "Not Equal";
            this.NotEqualButton.Click += new System.EventHandler(this.NotEqualButton_Click);
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
            // IncreasedByXButton
            // 
            this.IncreasedByXButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IncreasedByXButton.Image = ((System.Drawing.Image)(resources.GetObject("IncreasedByXButton.Image")));
            this.IncreasedByXButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedByXButton.Name = "IncreasedByXButton";
            this.IncreasedByXButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedByXButton.Text = "Increase By X";
            this.IncreasedByXButton.Click += new System.EventHandler(this.IncreasedByXButton_Click);
            // 
            // DecreasedByXButton
            // 
            this.DecreasedByXButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecreasedByXButton.Image = ((System.Drawing.Image)(resources.GetObject("DecreasedByXButton.Image")));
            this.DecreasedByXButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecreasedByXButton.Name = "DecreasedByXButton";
            this.DecreasedByXButton.Size = new System.Drawing.Size(23, 22);
            this.DecreasedByXButton.Text = "Decrease By X";
            this.DecreasedByXButton.Click += new System.EventHandler(this.DecreasedByXButton_Click);
            // 
            // FSMBuilderPanel
            // 
            this.FSMBuilderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FSMBuilderPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FSMBuilderPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FSMBuilderPanel.Location = new System.Drawing.Point(12, 55);
            this.FSMBuilderPanel.Name = "FSMBuilderPanel";
            this.FSMBuilderPanel.Size = new System.Drawing.Size(445, 203);
            this.FSMBuilderPanel.TabIndex = 163;
            this.FSMBuilderPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseClick);
            this.FSMBuilderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseDown);
            this.FSMBuilderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseMove);
            this.FSMBuilderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseUp);
            // 
            // GUIFilterFSM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 270);
            this.Controls.Add(this.FSMBuilderPanel);
            this.Controls.Add(this.ValueTextBox);
            this.Controls.Add(this.ValueTypeComboBox);
            this.Controls.Add(this.ScanTypeWorldStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIFilterFSM";
            this.Text = "Finite State Scanner";
            this.ScanTypeWorldStrip.ResumeLayout(false);
            this.ScanTypeWorldStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.ToolStrip ScanTypeWorldStrip;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton UnchangedButton;
        private System.Windows.Forms.ToolStripButton ChangedButton;
        private System.Windows.Forms.ToolStripButton IncreasedButton;
        private System.Windows.Forms.ToolStripButton DecreasedButton;
        private System.Windows.Forms.ToolStripButton NotEqualButton;
        private System.Windows.Forms.ToolStripButton EqualButton;
        private System.Windows.Forms.ToolStripButton GreaterThanButton;
        private System.Windows.Forms.ToolStripButton LessThanButton;
        private System.Windows.Forms.ToolStripButton IncreasedByXButton;
        private System.Windows.Forms.ToolStripButton DecreasedByXButton;
        private System.Windows.Forms.ToolStripButton RemoveConstraintButton;
        private System.Windows.Forms.Panel FSMBuilderPanel;
    }
}