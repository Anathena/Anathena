namespace Anathema
{
    partial class GUILabelerInputCorrelator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DockableWindow));
            this.AlignmentLabel = new System.Windows.Forms.Label();
            this.AlignmentNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.CustomSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.CustomSizeCheckBox = new System.Windows.Forms.CheckBox();
            this.ReactUnsureRadioButton = new System.Windows.Forms.RadioButton();
            this.ReactUserToInputRadioButton = new System.Windows.Forms.RadioButton();
            this.ReactVariableToInputRadioButton = new System.Windows.Forms.RadioButton();
            this.FragmentSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.GranularityTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartSSAButton = new System.Windows.Forms.ToolStripButton();
            this.StopSSAButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.AlignmentNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomSizeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GranularityTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AlignmentLabel
            // 
            this.AlignmentLabel.AutoSize = true;
            this.AlignmentLabel.Location = new System.Drawing.Point(25, 128);
            this.AlignmentLabel.Name = "AlignmentLabel";
            this.AlignmentLabel.Size = new System.Drawing.Size(53, 13);
            this.AlignmentLabel.TabIndex = 159;
            this.AlignmentLabel.Text = "Alignment";
            // 
            // AlignmentNumericUpDown
            // 
            this.AlignmentNumericUpDown.Location = new System.Drawing.Point(84, 126);
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
            this.AlignmentNumericUpDown.TabIndex = 158;
            this.AlignmentNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // CustomSizeNumericUpDown
            // 
            this.CustomSizeNumericUpDown.Location = new System.Drawing.Point(84, 100);
            this.CustomSizeNumericUpDown.Name = "CustomSizeNumericUpDown";
            this.CustomSizeNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.CustomSizeNumericUpDown.TabIndex = 157;
            // 
            // CustomSizeCheckBox
            // 
            this.CustomSizeCheckBox.AutoSize = true;
            this.CustomSizeCheckBox.Location = new System.Drawing.Point(0, 101);
            this.CustomSizeCheckBox.Name = "CustomSizeCheckBox";
            this.CustomSizeCheckBox.Size = new System.Drawing.Size(84, 17);
            this.CustomSizeCheckBox.TabIndex = 156;
            this.CustomSizeCheckBox.Text = "Custom Size";
            this.CustomSizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReactUnsureRadioButton
            // 
            this.ReactUnsureRadioButton.AutoSize = true;
            this.ReactUnsureRadioButton.Location = new System.Drawing.Point(0, 198);
            this.ReactUnsureRadioButton.Name = "ReactUnsureRadioButton";
            this.ReactUnsureRadioButton.Size = new System.Drawing.Size(59, 17);
            this.ReactUnsureRadioButton.TabIndex = 155;
            this.ReactUnsureRadioButton.Text = "Unsure";
            this.ReactUnsureRadioButton.UseVisualStyleBackColor = true;
            // 
            // ReactUserToInputRadioButton
            // 
            this.ReactUserToInputRadioButton.AutoSize = true;
            this.ReactUserToInputRadioButton.Location = new System.Drawing.Point(0, 175);
            this.ReactUserToInputRadioButton.Name = "ReactUserToInputRadioButton";
            this.ReactUserToInputRadioButton.Size = new System.Drawing.Size(137, 17);
            this.ReactUserToInputRadioButton.TabIndex = 154;
            this.ReactUserToInputRadioButton.Text = "User Reacts to Variable";
            this.ReactUserToInputRadioButton.UseVisualStyleBackColor = true;
            // 
            // ReactVariableToInputRadioButton
            // 
            this.ReactVariableToInputRadioButton.AutoSize = true;
            this.ReactVariableToInputRadioButton.Checked = true;
            this.ReactVariableToInputRadioButton.Location = new System.Drawing.Point(0, 152);
            this.ReactVariableToInputRadioButton.Name = "ReactVariableToInputRadioButton";
            this.ReactVariableToInputRadioButton.Size = new System.Drawing.Size(143, 17);
            this.ReactVariableToInputRadioButton.TabIndex = 153;
            this.ReactVariableToInputRadioButton.TabStop = true;
            this.ReactVariableToInputRadioButton.Text = "Variable Reacts To Input";
            this.ReactVariableToInputRadioButton.UseVisualStyleBackColor = true;
            // 
            // FragmentSizeValueLabel
            // 
            this.FragmentSizeValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.FragmentSizeValueLabel.AutoSize = true;
            this.FragmentSizeValueLabel.Location = new System.Drawing.Point(77, 60);
            this.FragmentSizeValueLabel.Name = "FragmentSizeValueLabel";
            this.FragmentSizeValueLabel.Size = new System.Drawing.Size(42, 13);
            this.FragmentSizeValueLabel.TabIndex = 152;
            this.FragmentSizeValueLabel.Text = "0 Bytes";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(0, 60);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(71, 13);
            this.VariableSizeLabel.TabIndex = 151;
            this.VariableSizeLabel.Text = "Variable Size:";
            // 
            // GranularityTrackBar
            // 
            this.GranularityTrackBar.LargeChange = 2;
            this.GranularityTrackBar.Location = new System.Drawing.Point(0, 28);
            this.GranularityTrackBar.Maximum = 3;
            this.GranularityTrackBar.Name = "GranularityTrackBar";
            this.GranularityTrackBar.Size = new System.Drawing.Size(253, 45);
            this.GranularityTrackBar.TabIndex = 149;
            this.GranularityTrackBar.Value = 2;
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartSSAButton,
            this.StopSSAButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(284, 25);
            this.ScanToolStrip.TabIndex = 150;
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
            // 
            // StopSSAButton
            // 
            this.StopSSAButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopSSAButton.Image = ((System.Drawing.Image)(resources.GetObject("StopSSAButton.Image")));
            this.StopSSAButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopSSAButton.Name = "StopSSAButton";
            this.StopSSAButton.Size = new System.Drawing.Size(23, 22);
            this.StopSSAButton.Text = "New Scan";
            // 
            // DockableWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
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
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockableWindow";
            this.Text = "Input Correlator";
            ((System.ComponentModel.ISupportInitialize)(this.AlignmentNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomSizeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GranularityTrackBar)).EndInit();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AlignmentLabel;
        private System.Windows.Forms.NumericUpDown AlignmentNumericUpDown;
        private System.Windows.Forms.NumericUpDown CustomSizeNumericUpDown;
        private System.Windows.Forms.CheckBox CustomSizeCheckBox;
        private System.Windows.Forms.RadioButton ReactUnsureRadioButton;
        private System.Windows.Forms.RadioButton ReactUserToInputRadioButton;
        private System.Windows.Forms.RadioButton ReactVariableToInputRadioButton;
        private System.Windows.Forms.Label FragmentSizeValueLabel;
        private System.Windows.Forms.Label VariableSizeLabel;
        private System.Windows.Forms.TrackBar GranularityTrackBar;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StartSSAButton;
        private System.Windows.Forms.ToolStripButton StopSSAButton;
    }
}