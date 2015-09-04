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
            this.ScanToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GranularityTrackBar)).BeginInit();
            this.SuspendLayout();
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
            this.FragmentSizeValueLabel.Location = new System.Drawing.Point(144, 5);
            this.FragmentSizeValueLabel.Name = "FragmentSizeValueLabel";
            this.FragmentSizeValueLabel.Size = new System.Drawing.Size(42, 13);
            this.FragmentSizeValueLabel.TabIndex = 141;
            this.FragmentSizeValueLabel.Text = "0 Bytes";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(69, 5);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(71, 13);
            this.VariableSizeLabel.TabIndex = 140;
            this.VariableSizeLabel.Text = "Variable Size:";
            // 
            // GUIInputCorrelator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
    }
}
