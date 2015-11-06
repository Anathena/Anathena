namespace Anathema
{
    partial class GUIInputCorrelator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIInputCorrelator));
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartSSAButton = new System.Windows.Forms.ToolStripButton();
            this.StopSSAButton = new System.Windows.Forms.ToolStripButton();
            this.GranularityTrackBar = new System.Windows.Forms.TrackBar();
            this.FragmentSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.ReactVariableToInputRadioButton = new System.Windows.Forms.RadioButton();
            this.ReactUserToInputRadioButton = new System.Windows.Forms.RadioButton();
            this.ReactUnsureRadioButton = new System.Windows.Forms.RadioButton();
            this.CustomSizeCheckBox = new System.Windows.Forms.CheckBox();
            this.CustomSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AlignmentNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AlignmentLabel = new System.Windows.Forms.Label();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.ScanToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GranularityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomSizeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlignmentNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartSSAButton,
            this.StopSSAButton,
            this.toolStripButton2});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(259, 25);
            this.ScanToolStrip.TabIndex = 139;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // StartSSAButton
            // 
            this.StartSSAButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartSSAButton.Image = ((System.Drawing.Image)(resources.GetObject("StartSSAButton.Image")));
            this.StartSSAButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartSSAButton.Name = "StartSSAButton";
            this.StartSSAButton.Size = new System.Drawing.Size(23, 22);
            this.StartSSAButton.Text = "Start Scan";
            this.StartSSAButton.Click += new System.EventHandler(this.StartSSAButton_Click);
            // 
            // StopSSAButton
            // 
            this.StopSSAButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopSSAButton.Image = ((System.Drawing.Image)(resources.GetObject("StopSSAButton.Image")));
            this.StopSSAButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopSSAButton.Name = "StopSSAButton";
            this.StopSSAButton.Size = new System.Drawing.Size(23, 22);
            this.StopSSAButton.Text = "New Scan";
            this.StopSSAButton.Click += new System.EventHandler(this.StopSSAButton_Click);
            // 
            // GranularityTrackBar
            // 
            this.GranularityTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.GranularityTrackBar.LargeChange = 2;
            this.GranularityTrackBar.Location = new System.Drawing.Point(0, 25);
            this.GranularityTrackBar.Maximum = 3;
            this.GranularityTrackBar.Name = "GranularityTrackBar";
            this.GranularityTrackBar.Size = new System.Drawing.Size(259, 45);
            this.GranularityTrackBar.TabIndex = 138;
            this.GranularityTrackBar.Value = 2;
            this.GranularityTrackBar.Scroll += new System.EventHandler(this.GranularityTrackBar_Scroll);
            // 
            // FragmentSizeValueLabel
            // 
            this.FragmentSizeValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.FragmentSizeValueLabel.AutoSize = true;
            this.FragmentSizeValueLabel.Location = new System.Drawing.Point(161, 7);
            this.FragmentSizeValueLabel.Name = "FragmentSizeValueLabel";
            this.FragmentSizeValueLabel.Size = new System.Drawing.Size(42, 13);
            this.FragmentSizeValueLabel.TabIndex = 141;
            this.FragmentSizeValueLabel.Text = "0 Bytes";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(84, 7);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(71, 13);
            this.VariableSizeLabel.TabIndex = 140;
            this.VariableSizeLabel.Text = "Variable Size:";
            // 
            // ReactVariableToInputRadioButton
            // 
            this.ReactVariableToInputRadioButton.AutoSize = true;
            this.ReactVariableToInputRadioButton.Checked = true;
            this.ReactVariableToInputRadioButton.Location = new System.Drawing.Point(3, 127);
            this.ReactVariableToInputRadioButton.Name = "ReactVariableToInputRadioButton";
            this.ReactVariableToInputRadioButton.Size = new System.Drawing.Size(143, 17);
            this.ReactVariableToInputRadioButton.TabIndex = 142;
            this.ReactVariableToInputRadioButton.TabStop = true;
            this.ReactVariableToInputRadioButton.Text = "Variable Reacts To Input";
            this.ReactVariableToInputRadioButton.UseVisualStyleBackColor = true;
            // 
            // ReactUserToInputRadioButton
            // 
            this.ReactUserToInputRadioButton.AutoSize = true;
            this.ReactUserToInputRadioButton.Location = new System.Drawing.Point(3, 150);
            this.ReactUserToInputRadioButton.Name = "ReactUserToInputRadioButton";
            this.ReactUserToInputRadioButton.Size = new System.Drawing.Size(137, 17);
            this.ReactUserToInputRadioButton.TabIndex = 143;
            this.ReactUserToInputRadioButton.Text = "User Reacts to Variable";
            this.ReactUserToInputRadioButton.UseVisualStyleBackColor = true;
            // 
            // ReactUnsureRadioButton
            // 
            this.ReactUnsureRadioButton.AutoSize = true;
            this.ReactUnsureRadioButton.Location = new System.Drawing.Point(3, 173);
            this.ReactUnsureRadioButton.Name = "ReactUnsureRadioButton";
            this.ReactUnsureRadioButton.Size = new System.Drawing.Size(59, 17);
            this.ReactUnsureRadioButton.TabIndex = 144;
            this.ReactUnsureRadioButton.Text = "Unsure";
            this.ReactUnsureRadioButton.UseVisualStyleBackColor = true;
            // 
            // CustomSizeCheckBox
            // 
            this.CustomSizeCheckBox.AutoSize = true;
            this.CustomSizeCheckBox.Location = new System.Drawing.Point(3, 76);
            this.CustomSizeCheckBox.Name = "CustomSizeCheckBox";
            this.CustomSizeCheckBox.Size = new System.Drawing.Size(84, 17);
            this.CustomSizeCheckBox.TabIndex = 145;
            this.CustomSizeCheckBox.Text = "Custom Size";
            this.CustomSizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // CustomSizeNumericUpDown
            // 
            this.CustomSizeNumericUpDown.Location = new System.Drawing.Point(87, 75);
            this.CustomSizeNumericUpDown.Name = "CustomSizeNumericUpDown";
            this.CustomSizeNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.CustomSizeNumericUpDown.TabIndex = 146;
            // 
            // AlignmentNumericUpDown
            // 
            this.AlignmentNumericUpDown.Location = new System.Drawing.Point(87, 101);
            this.AlignmentNumericUpDown.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.AlignmentNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AlignmentNumericUpDown.Name = "AlignmentNumericUpDown";
            this.AlignmentNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.AlignmentNumericUpDown.TabIndex = 147;
            this.AlignmentNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // AlignmentLabel
            // 
            this.AlignmentLabel.AutoSize = true;
            this.AlignmentLabel.Location = new System.Drawing.Point(28, 103);
            this.AlignmentLabel.Name = "AlignmentLabel";
            this.AlignmentLabel.Size = new System.Drawing.Size(53, 13);
            this.AlignmentLabel.TabIndex = 148;
            this.AlignmentLabel.Text = "Alignment";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // GUIInputCorrelator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AlignmentLabel);
            this.Controls.Add(this.AlignmentNumericUpDown);
            this.Controls.Add(this.CustomSizeNumericUpDown);
            this.Controls.Add(this.CustomSizeCheckBox);
            this.Controls.Add(this.ReactUnsureRadioButton);
            this.Controls.Add(this.ReactUserToInputRadioButton);
            this.Controls.Add(this.ReactVariableToInputRadioButton);
            this.Controls.Add(this.FragmentSizeValueLabel);
            this.Controls.Add(this.VariableSizeLabel);
            this.Controls.Add(this.GranularityTrackBar);
            this.Controls.Add(this.ScanToolStrip);
            this.Name = "GUIInputCorrelator";
            this.Size = new System.Drawing.Size(259, 252);
            this.Load += new System.EventHandler(this.GUIInputCorrelator_Load);
            this.Resize += new System.EventHandler(this.GUIInputCorrelator_Resize);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GranularityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomSizeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlignmentNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StartSSAButton;
        private System.Windows.Forms.ToolStripButton StopSSAButton;
        private System.Windows.Forms.TrackBar GranularityTrackBar;
        private System.Windows.Forms.Label FragmentSizeValueLabel;
        private System.Windows.Forms.Label VariableSizeLabel;
        private System.Windows.Forms.RadioButton ReactVariableToInputRadioButton;
        private System.Windows.Forms.RadioButton ReactUserToInputRadioButton;
        private System.Windows.Forms.RadioButton ReactUnsureRadioButton;
        private System.Windows.Forms.CheckBox CustomSizeCheckBox;
        private System.Windows.Forms.NumericUpDown CustomSizeNumericUpDown;
        private System.Windows.Forms.NumericUpDown AlignmentNumericUpDown;
        private System.Windows.Forms.Label AlignmentLabel;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}
