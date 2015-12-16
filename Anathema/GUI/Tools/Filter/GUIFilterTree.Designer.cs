namespace Anathema
{
    partial class GUIFilterTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIFilterTree));
            this.MemorySizeValueLabel = new System.Windows.Forms.Label();
            this.HashSizeLabel = new System.Windows.Forms.Label();
            this.VariableSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.VariableSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
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
            // VariableSizeValueLabel
            // 
            this.VariableSizeValueLabel.AutoSize = true;
            this.VariableSizeValueLabel.Location = new System.Drawing.Point(92, 75);
            this.VariableSizeValueLabel.Name = "VariableSizeValueLabel";
            this.VariableSizeValueLabel.Size = new System.Drawing.Size(20, 13);
            this.VariableSizeValueLabel.TabIndex = 147;
            this.VariableSizeValueLabel.Text = "0B";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(2, 75);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(94, 13);
            this.VariableSizeLabel.TabIndex = 146;
            this.VariableSizeLabel.Text = "Max Variable Size:";
            // 
            // VariableSizeTrackBar
            // 
            this.VariableSizeTrackBar.LargeChange = 4;
            this.VariableSizeTrackBar.Location = new System.Drawing.Point(0, 43);
            this.VariableSizeTrackBar.Maximum = 3;
            this.VariableSizeTrackBar.Name = "VariableSizeTrackBar";
            this.VariableSizeTrackBar.Size = new System.Drawing.Size(132, 45);
            this.VariableSizeTrackBar.TabIndex = 145;
            this.VariableSizeTrackBar.Value = 3;
            this.VariableSizeTrackBar.Scroll += new System.EventHandler(this.VariableSizeTrackBar_Scroll);
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
            // GUIFilterTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 173);
            this.Controls.Add(this.VariableSizeValueLabel);
            this.Controls.Add(this.VariableSizeLabel);
            this.Controls.Add(this.VariableSizeTrackBar);
            this.Controls.Add(this.MemorySizeValueLabel);
            this.Controls.Add(this.HashSizeLabel);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIFilterTree";
            this.Text = "Tree Scanner";
            this.Resize += new System.EventHandler(this.GUIFilterTree_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).EndInit();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label MemorySizeValueLabel;
        private System.Windows.Forms.Label HashSizeLabel;
        private System.Windows.Forms.Label VariableSizeValueLabel;
        private System.Windows.Forms.Label VariableSizeLabel;
        private System.Windows.Forms.TrackBar VariableSizeTrackBar;
        private System.Windows.Forms.ToolStripButton StopScanButton;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
    }
}