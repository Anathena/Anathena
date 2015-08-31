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
            this.EndInputCorrelationButton = new System.Windows.Forms.Button();
            this.StartInputCorrelationButton = new System.Windows.Forms.Button();
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
            // GranularityTrackBar
            // 
            this.GranularityTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.GranularityTrackBar.Location = new System.Drawing.Point(0, 25);
            this.GranularityTrackBar.Maximum = 4;
            this.GranularityTrackBar.Minimum = 1;
            this.GranularityTrackBar.Name = "GranularityTrackBar";
            this.GranularityTrackBar.Size = new System.Drawing.Size(259, 45);
            this.GranularityTrackBar.TabIndex = 138;
            this.GranularityTrackBar.Value = 2;
            // 
            // EndInputCorrelationButton
            // 
            this.EndInputCorrelationButton.Location = new System.Drawing.Point(132, 115);
            this.EndInputCorrelationButton.Name = "EndInputCorrelationButton";
            this.EndInputCorrelationButton.Size = new System.Drawing.Size(75, 23);
            this.EndInputCorrelationButton.TabIndex = 141;
            this.EndInputCorrelationButton.Text = "End Phi";
            this.EndInputCorrelationButton.UseVisualStyleBackColor = true;
            // 
            // StartInputCorrelationButton
            // 
            this.StartInputCorrelationButton.Location = new System.Drawing.Point(51, 115);
            this.StartInputCorrelationButton.Name = "StartInputCorrelationButton";
            this.StartInputCorrelationButton.Size = new System.Drawing.Size(75, 23);
            this.StartInputCorrelationButton.TabIndex = 140;
            this.StartInputCorrelationButton.Text = "Start Phi";
            this.StartInputCorrelationButton.UseVisualStyleBackColor = true;
            // 
            // InputCorrelatorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EndInputCorrelationButton);
            this.Controls.Add(this.StartInputCorrelationButton);
            this.Controls.Add(this.GranularityTrackBar);
            this.Controls.Add(this.ScanToolStrip);
            this.Name = "InputCorrelatorPanel";
            this.Size = new System.Drawing.Size(259, 252);
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
        private System.Windows.Forms.Button EndInputCorrelationButton;
        private System.Windows.Forms.Button StartInputCorrelationButton;
    }
}
