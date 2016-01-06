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
            this.VariableSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.VariableSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.EitherRadioButton = new System.Windows.Forms.RadioButton();
            this.UserToVariableRadioButton = new System.Windows.Forms.RadioButton();
            this.VariableToUserRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // VariableSizeValueLabel
            // 
            this.VariableSizeValueLabel.AutoSize = true;
            this.VariableSizeValueLabel.Location = new System.Drawing.Point(67, 60);
            this.VariableSizeValueLabel.Name = "VariableSizeValueLabel";
            this.VariableSizeValueLabel.Size = new System.Drawing.Size(20, 13);
            this.VariableSizeValueLabel.TabIndex = 152;
            this.VariableSizeValueLabel.Text = "0B";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(0, 60);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(71, 13);
            this.VariableSizeLabel.TabIndex = 151;
            this.VariableSizeLabel.Text = "Variable Size:";
            // 
            // VariableSizeTrackBar
            // 
            this.VariableSizeTrackBar.LargeChange = 2;
            this.VariableSizeTrackBar.Location = new System.Drawing.Point(0, 28);
            this.VariableSizeTrackBar.Maximum = 3;
            this.VariableSizeTrackBar.Name = "VariableSizeTrackBar";
            this.VariableSizeTrackBar.Size = new System.Drawing.Size(140, 45);
            this.VariableSizeTrackBar.TabIndex = 149;
            this.VariableSizeTrackBar.Value = 2;
            this.VariableSizeTrackBar.Scroll += new System.EventHandler(this.VariableSizeTrackBar_Scroll);
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartScanButton,
            this.StopScanButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(304, 25);
            this.ScanToolStrip.TabIndex = 150;
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
            // StopScanButton
            // 
            this.StopScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopScanButton.Image = global::Anathema.Properties.Resources.Stop;
            this.StopScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopScanButton.Name = "StopScanButton";
            this.StopScanButton.Size = new System.Drawing.Size(23, 22);
            this.StopScanButton.Text = "New Scan";
            this.StopScanButton.Click += new System.EventHandler(this.StopScanButton_Click);
            // 
            // EitherRadioButton
            // 
            this.EitherRadioButton.AutoSize = true;
            this.EitherRadioButton.Location = new System.Drawing.Point(12, 124);
            this.EitherRadioButton.Name = "EitherRadioButton";
            this.EitherRadioButton.Size = new System.Drawing.Size(52, 17);
            this.EitherRadioButton.TabIndex = 170;
            this.EitherRadioButton.Text = "Either";
            this.EitherRadioButton.UseVisualStyleBackColor = true;
            this.EitherRadioButton.CheckedChanged += new System.EventHandler(this.EitherRadioButton_CheckedChanged);
            // 
            // UserToVariableRadioButton
            // 
            this.UserToVariableRadioButton.AutoSize = true;
            this.UserToVariableRadioButton.Location = new System.Drawing.Point(12, 107);
            this.UserToVariableRadioButton.Name = "UserToVariableRadioButton";
            this.UserToVariableRadioButton.Size = new System.Drawing.Size(151, 17);
            this.UserToVariableRadioButton.TabIndex = 169;
            this.UserToVariableRadioButton.Text = "User Responds to Variable";
            this.UserToVariableRadioButton.UseVisualStyleBackColor = true;
            this.UserToVariableRadioButton.CheckedChanged += new System.EventHandler(this.UserToVariableRadioButton_CheckedChanged);
            // 
            // VariableToUserRadioButton
            // 
            this.VariableToUserRadioButton.AutoSize = true;
            this.VariableToUserRadioButton.Checked = true;
            this.VariableToUserRadioButton.Location = new System.Drawing.Point(12, 90);
            this.VariableToUserRadioButton.Name = "VariableToUserRadioButton";
            this.VariableToUserRadioButton.Size = new System.Drawing.Size(151, 17);
            this.VariableToUserRadioButton.TabIndex = 168;
            this.VariableToUserRadioButton.TabStop = true;
            this.VariableToUserRadioButton.Text = "Variable Responds to User";
            this.VariableToUserRadioButton.UseVisualStyleBackColor = true;
            this.VariableToUserRadioButton.CheckedChanged += new System.EventHandler(this.VariableToUserRadioButton_CheckedChanged);
            // 
            // GUILabelerInputCorrelator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 217);
            this.Controls.Add(this.EitherRadioButton);
            this.Controls.Add(this.UserToVariableRadioButton);
            this.Controls.Add(this.VariableToUserRadioButton);
            this.Controls.Add(this.VariableSizeValueLabel);
            this.Controls.Add(this.VariableSizeLabel);
            this.Controls.Add(this.VariableSizeTrackBar);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUILabelerInputCorrelator";
            this.Text = "Input Correlator";
            this.Resize += new System.EventHandler(this.GUILabelerInputCorrelator_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).EndInit();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label VariableSizeValueLabel;
        private System.Windows.Forms.Label VariableSizeLabel;
        private System.Windows.Forms.TrackBar VariableSizeTrackBar;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStripButton StopScanButton;
        private System.Windows.Forms.RadioButton EitherRadioButton;
        private System.Windows.Forms.RadioButton UserToVariableRadioButton;
        private System.Windows.Forms.RadioButton VariableToUserRadioButton;
    }
}