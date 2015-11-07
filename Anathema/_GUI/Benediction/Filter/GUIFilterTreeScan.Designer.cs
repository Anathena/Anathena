namespace Anathema
{
    partial class GUIFilterTreeScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIFilterTreeScan));
            this.GranularityTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartButton = new System.Windows.Forms.ToolStripButton();
            this.StopButton = new System.Windows.Forms.ToolStripButton();
            this.FragmentSizeLabel = new System.Windows.Forms.Label();
            this.FragmentSizeValueLabel = new System.Windows.Forms.Label();
            this.AdvancedSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.TreeSplitsValueLabel = new System.Windows.Forms.Label();
            this.TreeSplitsLabel = new System.Windows.Forms.Label();
            this.HashSizeLabel = new System.Windows.Forms.Label();
            this.MemorySizeValueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GranularityTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            this.AdvancedSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // GranularityTrackBar
            // 
            this.GranularityTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.GranularityTrackBar.LargeChange = 4;
            this.GranularityTrackBar.Location = new System.Drawing.Point(3, 16);
            this.GranularityTrackBar.Maximum = 16;
            this.GranularityTrackBar.Name = "GranularityTrackBar";
            this.GranularityTrackBar.Size = new System.Drawing.Size(255, 45);
            this.GranularityTrackBar.TabIndex = 136;
            this.GranularityTrackBar.Value = 6;
            this.GranularityTrackBar.Scroll += new System.EventHandler(this.GranularityTrackBar_Scroll);
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartButton,
            this.StopButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 209);
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
            this.StartButton.Text = "Start Tree Scan";
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
            this.StopButton.ToolTipText = "Stop Tree Scan";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // FragmentSizeLabel
            // 
            this.FragmentSizeLabel.AutoSize = true;
            this.FragmentSizeLabel.Location = new System.Drawing.Point(6, 48);
            this.FragmentSizeLabel.Name = "FragmentSizeLabel";
            this.FragmentSizeLabel.Size = new System.Drawing.Size(77, 13);
            this.FragmentSizeLabel.TabIndex = 138;
            this.FragmentSizeLabel.Text = "Fragment Size:";
            // 
            // FragmentSizeValueLabel
            // 
            this.FragmentSizeValueLabel.AutoSize = true;
            this.FragmentSizeValueLabel.Location = new System.Drawing.Point(81, 48);
            this.FragmentSizeValueLabel.Name = "FragmentSizeValueLabel";
            this.FragmentSizeValueLabel.Size = new System.Drawing.Size(42, 13);
            this.FragmentSizeValueLabel.TabIndex = 139;
            this.FragmentSizeValueLabel.Text = "0 Bytes";
            // 
            // AdvancedSettingsGroupBox
            // 
            this.AdvancedSettingsGroupBox.Controls.Add(this.FragmentSizeValueLabel);
            this.AdvancedSettingsGroupBox.Controls.Add(this.FragmentSizeLabel);
            this.AdvancedSettingsGroupBox.Controls.Add(this.GranularityTrackBar);
            this.AdvancedSettingsGroupBox.Location = new System.Drawing.Point(3, 117);
            this.AdvancedSettingsGroupBox.Name = "AdvancedSettingsGroupBox";
            this.AdvancedSettingsGroupBox.Size = new System.Drawing.Size(261, 89);
            this.AdvancedSettingsGroupBox.TabIndex = 143;
            this.AdvancedSettingsGroupBox.TabStop = false;
            this.AdvancedSettingsGroupBox.Text = "Advanced Settings";
            // 
            // TreeSplitsValueLabel
            // 
            this.TreeSplitsValueLabel.AutoSize = true;
            this.TreeSplitsValueLabel.Location = new System.Drawing.Point(85, 34);
            this.TreeSplitsValueLabel.Name = "TreeSplitsValueLabel";
            this.TreeSplitsValueLabel.Size = new System.Drawing.Size(13, 13);
            this.TreeSplitsValueLabel.TabIndex = 147;
            this.TreeSplitsValueLabel.Text = "0";
            // 
            // TreeSplitsLabel
            // 
            this.TreeSplitsLabel.AutoSize = true;
            this.TreeSplitsLabel.Location = new System.Drawing.Point(9, 34);
            this.TreeSplitsLabel.Name = "TreeSplitsLabel";
            this.TreeSplitsLabel.Size = new System.Drawing.Size(60, 13);
            this.TreeSplitsLabel.TabIndex = 146;
            this.TreeSplitsLabel.Text = "Tree Splits:";
            // 
            // HashSizeLabel
            // 
            this.HashSizeLabel.AutoSize = true;
            this.HashSizeLabel.Location = new System.Drawing.Point(9, 47);
            this.HashSizeLabel.Name = "HashSizeLabel";
            this.HashSizeLabel.Size = new System.Drawing.Size(70, 13);
            this.HashSizeLabel.TabIndex = 144;
            this.HashSizeLabel.Text = "Memory Size:";
            // 
            // MemorySizeValueLabel
            // 
            this.MemorySizeValueLabel.AutoSize = true;
            this.MemorySizeValueLabel.Location = new System.Drawing.Point(85, 47);
            this.MemorySizeValueLabel.Name = "MemorySizeValueLabel";
            this.MemorySizeValueLabel.Size = new System.Drawing.Size(13, 13);
            this.MemorySizeValueLabel.TabIndex = 145;
            this.MemorySizeValueLabel.Text = "0";
            // 
            // GUIFilterTreeScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ScanToolStrip);
            this.Controls.Add(this.TreeSplitsValueLabel);
            this.Controls.Add(this.TreeSplitsLabel);
            this.Controls.Add(this.MemorySizeValueLabel);
            this.Controls.Add(this.HashSizeLabel);
            this.Controls.Add(this.AdvancedSettingsGroupBox);
            this.Name = "GUIFilterTreeScan";
            this.Size = new System.Drawing.Size(267, 234);
            this.Resize += new System.EventHandler(this.GUIMemoryTreeFilter_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.GranularityTrackBar)).EndInit();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.AdvancedSettingsGroupBox.ResumeLayout(false);
            this.AdvancedSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar GranularityTrackBar;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StartButton;
        private System.Windows.Forms.ToolStripButton StopButton;
        private System.Windows.Forms.Label FragmentSizeLabel;
        private System.Windows.Forms.Label FragmentSizeValueLabel;
        private System.Windows.Forms.GroupBox AdvancedSettingsGroupBox;
        private System.Windows.Forms.Label TreeSplitsValueLabel;
        private System.Windows.Forms.Label TreeSplitsLabel;
        private System.Windows.Forms.Label HashSizeLabel;
        private System.Windows.Forms.Label MemorySizeValueLabel;
    }
}
