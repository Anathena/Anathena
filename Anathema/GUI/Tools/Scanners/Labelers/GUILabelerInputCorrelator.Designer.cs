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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUILabelerInputCorrelator));
            this.AlignmentLabel = new System.Windows.Forms.Label();
            this.VariableSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.VariableSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartSSAButton = new System.Windows.Forms.ToolStripButton();
            this.StopSSAButton = new System.Windows.Forms.ToolStripButton();
            this.AlignmentSizeValueLabel = new System.Windows.Forms.Label();
            this.AlignmentSizeTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlignmentSizeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // AlignmentLabel
            // 
            this.AlignmentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AlignmentLabel.AutoSize = true;
            this.AlignmentLabel.Location = new System.Drawing.Point(221, 60);
            this.AlignmentLabel.Name = "AlignmentLabel";
            this.AlignmentLabel.Size = new System.Drawing.Size(56, 13);
            this.AlignmentLabel.TabIndex = 159;
            this.AlignmentLabel.Text = "Alignment:";
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
            this.StartSSAButton,
            this.StopSSAButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(304, 25);
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
            // AlignmentSizeValueLabel
            // 
            this.AlignmentSizeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AlignmentSizeValueLabel.AutoSize = true;
            this.AlignmentSizeValueLabel.Location = new System.Drawing.Point(273, 60);
            this.AlignmentSizeValueLabel.Name = "AlignmentSizeValueLabel";
            this.AlignmentSizeValueLabel.Size = new System.Drawing.Size(13, 13);
            this.AlignmentSizeValueLabel.TabIndex = 162;
            this.AlignmentSizeValueLabel.Text = "0";
            // 
            // AlignmentSizeTrackBar
            // 
            this.AlignmentSizeTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AlignmentSizeTrackBar.LargeChange = 2;
            this.AlignmentSizeTrackBar.Location = new System.Drawing.Point(166, 28);
            this.AlignmentSizeTrackBar.Maximum = 3;
            this.AlignmentSizeTrackBar.Name = "AlignmentSizeTrackBar";
            this.AlignmentSizeTrackBar.Size = new System.Drawing.Size(138, 45);
            this.AlignmentSizeTrackBar.TabIndex = 160;
            this.AlignmentSizeTrackBar.Value = 2;
            this.AlignmentSizeTrackBar.Scroll += new System.EventHandler(this.AlignmentTrackBar_Scroll);
            // 
            // GUILabelerInputCorrelator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 217);
            this.Controls.Add(this.AlignmentSizeValueLabel);
            this.Controls.Add(this.AlignmentLabel);
            this.Controls.Add(this.AlignmentSizeTrackBar);
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
            ((System.ComponentModel.ISupportInitialize)(this.AlignmentSizeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AlignmentLabel;
        private System.Windows.Forms.Label VariableSizeValueLabel;
        private System.Windows.Forms.Label VariableSizeLabel;
        private System.Windows.Forms.TrackBar VariableSizeTrackBar;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StartSSAButton;
        private System.Windows.Forms.ToolStripButton StopSSAButton;
        private System.Windows.Forms.Label AlignmentSizeValueLabel;
        private System.Windows.Forms.TrackBar AlignmentSizeTrackBar;
    }
}