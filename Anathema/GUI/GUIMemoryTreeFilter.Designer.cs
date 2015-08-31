namespace Anathema
{
    partial class GUIMemoryTreeFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIMemoryTreeFilter));
            this.GranularityTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartButton = new System.Windows.Forms.ToolStripButton();
            this.StopButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.GranularityTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GranularityTrackBar
            // 
            this.GranularityTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.GranularityTrackBar.Location = new System.Drawing.Point(0, 25);
            this.GranularityTrackBar.Maximum = 64;
            this.GranularityTrackBar.Minimum = 4;
            this.GranularityTrackBar.Name = "GranularityTrackBar";
            this.GranularityTrackBar.Size = new System.Drawing.Size(267, 45);
            this.GranularityTrackBar.TabIndex = 136;
            this.GranularityTrackBar.Value = 16;
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartButton,
            this.StopButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(267, 25);
            this.ScanToolStrip.TabIndex = 137;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // StartButton
            // 
            this.StartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartButton.Image = ((System.Drawing.Image)(resources.GetObject("StartButton.Image")));
            this.StartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(23, 22);
            this.StartButton.Text = "Start Scan";
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopButton.Image = ((System.Drawing.Image)(resources.GetObject("StopButton.Image")));
            this.StopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(23, 22);
            this.StopButton.Text = "Stop";
            this.StopButton.ToolTipText = "Stop Button";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // GUIMemoryTreeFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GranularityTrackBar);
            this.Controls.Add(this.ScanToolStrip);
            this.Name = "GUIMemoryTreeFilter";
            this.Size = new System.Drawing.Size(267, 234);
            this.Load += new System.EventHandler(this.GUIMemoryTreeFilter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GranularityTrackBar)).EndInit();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar GranularityTrackBar;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StartButton;
        private System.Windows.Forms.ToolStripButton StopButton;
    }
}
