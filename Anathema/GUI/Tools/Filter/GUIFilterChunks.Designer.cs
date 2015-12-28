namespace Anathema
{
    partial class GUIFilterChunks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIFilterChunks));
            this.MemorySizeValueLabel = new System.Windows.Forms.Label();
            this.HashSizeLabel = new System.Windows.Forms.Label();
            this.ChunkSizeValueLabel = new System.Windows.Forms.Label();
            this.ChunkSizeLabel = new System.Windows.Forms.Label();
            this.ChunkSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.MinChangesValueLabel = new System.Windows.Forms.Label();
            this.MinChangesLabel = new System.Windows.Forms.Label();
            this.MinChangesTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.ChunkSizeTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinChangesTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // MemorySizeValueLabel
            // 
            this.MemorySizeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MemorySizeValueLabel.AutoSize = true;
            this.MemorySizeValueLabel.Location = new System.Drawing.Point(67, 26);
            this.MemorySizeValueLabel.Name = "MemorySizeValueLabel";
            this.MemorySizeValueLabel.Size = new System.Drawing.Size(20, 13);
            this.MemorySizeValueLabel.TabIndex = 152;
            this.MemorySizeValueLabel.Text = "0B";
            // 
            // HashSizeLabel
            // 
            this.HashSizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HashSizeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HashSizeLabel.Location = new System.Drawing.Point(0, 25);
            this.HashSizeLabel.Name = "HashSizeLabel";
            this.HashSizeLabel.Size = new System.Drawing.Size(266, 15);
            this.HashSizeLabel.TabIndex = 151;
            this.HashSizeLabel.Text = "Memory Size:";
            // 
            // ChunkSizeValueLabel
            // 
            this.ChunkSizeValueLabel.AutoSize = true;
            this.ChunkSizeValueLabel.Location = new System.Drawing.Point(63, 75);
            this.ChunkSizeValueLabel.Name = "ChunkSizeValueLabel";
            this.ChunkSizeValueLabel.Size = new System.Drawing.Size(20, 13);
            this.ChunkSizeValueLabel.TabIndex = 147;
            this.ChunkSizeValueLabel.Text = "0B";
            // 
            // ChunkSizeLabel
            // 
            this.ChunkSizeLabel.AutoSize = true;
            this.ChunkSizeLabel.Location = new System.Drawing.Point(2, 75);
            this.ChunkSizeLabel.Name = "ChunkSizeLabel";
            this.ChunkSizeLabel.Size = new System.Drawing.Size(64, 13);
            this.ChunkSizeLabel.TabIndex = 146;
            this.ChunkSizeLabel.Text = "Chunk Size:";
            // 
            // ChunkSizeTrackBar
            // 
            this.ChunkSizeTrackBar.Location = new System.Drawing.Point(0, 43);
            this.ChunkSizeTrackBar.Minimum = 5;
            this.ChunkSizeTrackBar.Name = "ChunkSizeTrackBar";
            this.ChunkSizeTrackBar.Size = new System.Drawing.Size(119, 45);
            this.ChunkSizeTrackBar.TabIndex = 148;
            this.ChunkSizeTrackBar.Value = 10;
            this.ChunkSizeTrackBar.Scroll += new System.EventHandler(this.ChunkSizeTrackBar_Scroll);
            // 
            // StopScanButton
            // 
            this.StopScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopScanButton.Image = ((System.Drawing.Image)(resources.GetObject("StopScanButton.Image")));
            this.StopScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopScanButton.Name = "StopScanButton";
            this.StopScanButton.Size = new System.Drawing.Size(23, 22);
            this.StopScanButton.Text = "Stop";
            this.StopScanButton.ToolTipText = "Stop Tree Scan";
            this.StopScanButton.Click += new System.EventHandler(this.StopScanButton_Click);
            // 
            // StartScanButton
            // 
            this.StartScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartScanButton.Image = ((System.Drawing.Image)(resources.GetObject("StartScanButton.Image")));
            this.StartScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartScanButton.Name = "StartScanButton";
            this.StartScanButton.Size = new System.Drawing.Size(23, 22);
            this.StartScanButton.Text = "Start Tree Scan";
            this.StartScanButton.Click += new System.EventHandler(this.StartScanButton_Click);
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
            this.ScanToolStrip.Size = new System.Drawing.Size(266, 25);
            this.ScanToolStrip.TabIndex = 149;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // MinChangesValueLabel
            // 
            this.MinChangesValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinChangesValueLabel.AutoSize = true;
            this.MinChangesValueLabel.Location = new System.Drawing.Point(232, 75);
            this.MinChangesValueLabel.Name = "MinChangesValueLabel";
            this.MinChangesValueLabel.Size = new System.Drawing.Size(13, 13);
            this.MinChangesValueLabel.TabIndex = 154;
            this.MinChangesValueLabel.Text = "0";
            // 
            // MinChangesLabel
            // 
            this.MinChangesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinChangesLabel.AutoSize = true;
            this.MinChangesLabel.Location = new System.Drawing.Point(164, 75);
            this.MinChangesLabel.Name = "MinChangesLabel";
            this.MinChangesLabel.Size = new System.Drawing.Size(72, 13);
            this.MinChangesLabel.TabIndex = 153;
            this.MinChangesLabel.Text = "Min Changes:";
            // 
            // MinChangesTrackBar
            // 
            this.MinChangesTrackBar.Location = new System.Drawing.Point(128, 43);
            this.MinChangesTrackBar.Maximum = 16;
            this.MinChangesTrackBar.Minimum = 1;
            this.MinChangesTrackBar.Name = "MinChangesTrackBar";
            this.MinChangesTrackBar.Size = new System.Drawing.Size(126, 45);
            this.MinChangesTrackBar.TabIndex = 155;
            this.MinChangesTrackBar.Value = 3;
            this.MinChangesTrackBar.Scroll += new System.EventHandler(this.MinChangesTrackBar_Scroll);
            // 
            // GUIFilterChunks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 173);
            this.Controls.Add(this.MinChangesValueLabel);
            this.Controls.Add(this.MinChangesLabel);
            this.Controls.Add(this.MinChangesTrackBar);
            this.Controls.Add(this.ChunkSizeValueLabel);
            this.Controls.Add(this.ChunkSizeLabel);
            this.Controls.Add(this.ChunkSizeTrackBar);
            this.Controls.Add(this.MemorySizeValueLabel);
            this.Controls.Add(this.HashSizeLabel);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIFilterChunks";
            this.Text = "Chunk Scanner";
            this.Resize += new System.EventHandler(this.GUIFilterChunks_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ChunkSizeTrackBar)).EndInit();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinChangesTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label MemorySizeValueLabel;
        private System.Windows.Forms.Label HashSizeLabel;
        private System.Windows.Forms.Label ChunkSizeValueLabel;
        private System.Windows.Forms.Label ChunkSizeLabel;
        private System.Windows.Forms.TrackBar ChunkSizeTrackBar;
        private System.Windows.Forms.ToolStripButton StopScanButton;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.Label MinChangesValueLabel;
        private System.Windows.Forms.Label MinChangesLabel;
        private System.Windows.Forms.TrackBar MinChangesTrackBar;
    }
}