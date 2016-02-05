namespace Anathema
{
    partial class GUIChunkScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIChunkScanner));
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.ScanCountLabel = new System.Windows.Forms.ToolStripLabel();
            this.MinChangesValueLabel = new System.Windows.Forms.Label();
            this.MinChangesLabel = new System.Windows.Forms.Label();
            this.MinChangesTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinChangesTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // StopScanButton
            // 
            this.StopScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopScanButton.Image = global::Anathema.Properties.Resources.Stop;
            this.StopScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopScanButton.Name = "StopScanButton";
            this.StopScanButton.Size = new System.Drawing.Size(23, 22);
            this.StopScanButton.Text = "Stop Scan";
            this.StopScanButton.ToolTipText = "Stop Scan";
            this.StopScanButton.Click += new System.EventHandler(this.StopScanButton_Click);
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
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartScanButton,
            this.StopScanButton,
            this.ScanCountLabel});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(266, 25);
            this.ScanToolStrip.TabIndex = 149;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // ScanCountLabel
            // 
            this.ScanCountLabel.Name = "ScanCountLabel";
            this.ScanCountLabel.Size = new System.Drawing.Size(80, 22);
            this.ScanCountLabel.Text = "Scan Count: 0";
            // 
            // MinChangesValueLabel
            // 
            this.MinChangesValueLabel.AutoSize = true;
            this.MinChangesValueLabel.Location = new System.Drawing.Point(81, 60);
            this.MinChangesValueLabel.Name = "MinChangesValueLabel";
            this.MinChangesValueLabel.Size = new System.Drawing.Size(13, 13);
            this.MinChangesValueLabel.TabIndex = 154;
            this.MinChangesValueLabel.Text = "0";
            // 
            // MinChangesLabel
            // 
            this.MinChangesLabel.AutoSize = true;
            this.MinChangesLabel.Location = new System.Drawing.Point(12, 60);
            this.MinChangesLabel.Name = "MinChangesLabel";
            this.MinChangesLabel.Size = new System.Drawing.Size(72, 13);
            this.MinChangesLabel.TabIndex = 153;
            this.MinChangesLabel.Text = "Min Changes:";
            // 
            // MinChangesTrackBar
            // 
            this.MinChangesTrackBar.Location = new System.Drawing.Point(12, 28);
            this.MinChangesTrackBar.Maximum = 16;
            this.MinChangesTrackBar.Minimum = 1;
            this.MinChangesTrackBar.Name = "MinChangesTrackBar";
            this.MinChangesTrackBar.Size = new System.Drawing.Size(116, 45);
            this.MinChangesTrackBar.TabIndex = 155;
            this.MinChangesTrackBar.Value = 3;
            this.MinChangesTrackBar.Scroll += new System.EventHandler(this.MinChangesTrackBar_Scroll);
            // 
            // GUIChunkScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 173);
            this.Controls.Add(this.MinChangesValueLabel);
            this.Controls.Add(this.MinChangesLabel);
            this.Controls.Add(this.MinChangesTrackBar);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIChunkScanner";
            this.Text = "Chunk Scanner";
            this.Resize += new System.EventHandler(this.GUIFilterChunks_Resize);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinChangesTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton StopScanButton;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.Label MinChangesValueLabel;
        private System.Windows.Forms.Label MinChangesLabel;
        private System.Windows.Forms.TrackBar MinChangesTrackBar;
        private System.Windows.Forms.ToolStripLabel ScanCountLabel;
    }
}