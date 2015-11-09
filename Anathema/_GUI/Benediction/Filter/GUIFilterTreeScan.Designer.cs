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
            this.FragmentSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartButton = new System.Windows.Forms.ToolStripButton();
            this.StopButton = new System.Windows.Forms.ToolStripButton();
            this.FragmentSizeLabel = new System.Windows.Forms.Label();
            this.FragmentSizeValueLabel = new System.Windows.Forms.Label();
            this.AdvancedSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.ChangeRangeLabel = new System.Windows.Forms.Label();
            this.NumericMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.NumberOfChangesLabel = new System.Windows.Forms.Label();
            this.NumericMinDepth = new System.Windows.Forms.NumericUpDown();
            this.TreeSplitsValueLabel = new System.Windows.Forms.Label();
            this.TreeSplitsLabel = new System.Windows.Forms.Label();
            this.HashSizeLabel = new System.Windows.Forms.Label();
            this.MemorySizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.VariableSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.DummyToolStrip = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.FragmentSizeTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            this.AdvancedSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMinDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // FragmentSizeTrackBar
            // 
            this.FragmentSizeTrackBar.LargeChange = 4;
            this.FragmentSizeTrackBar.Location = new System.Drawing.Point(6, 16);
            this.FragmentSizeTrackBar.Maximum = 16;
            this.FragmentSizeTrackBar.Name = "FragmentSizeTrackBar";
            this.FragmentSizeTrackBar.Size = new System.Drawing.Size(127, 45);
            this.FragmentSizeTrackBar.TabIndex = 136;
            this.FragmentSizeTrackBar.Value = 6;
            this.FragmentSizeTrackBar.Scroll += new System.EventHandler(this.GranularityTrackBar_Scroll);
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartButton,
            this.StopButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 25);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(276, 25);
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
            this.AdvancedSettingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AdvancedSettingsGroupBox.Controls.Add(this.VariableSizeValueLabel);
            this.AdvancedSettingsGroupBox.Controls.Add(this.VariableSizeLabel);
            this.AdvancedSettingsGroupBox.Controls.Add(this.VariableSizeTrackBar);
            this.AdvancedSettingsGroupBox.Controls.Add(this.ChangeRangeLabel);
            this.AdvancedSettingsGroupBox.Controls.Add(this.NumericMaxDepth);
            this.AdvancedSettingsGroupBox.Controls.Add(this.NumberOfChangesLabel);
            this.AdvancedSettingsGroupBox.Controls.Add(this.NumericMinDepth);
            this.AdvancedSettingsGroupBox.Controls.Add(this.FragmentSizeValueLabel);
            this.AdvancedSettingsGroupBox.Controls.Add(this.FragmentSizeLabel);
            this.AdvancedSettingsGroupBox.Controls.Add(this.FragmentSizeTrackBar);
            this.AdvancedSettingsGroupBox.Location = new System.Drawing.Point(3, 79);
            this.AdvancedSettingsGroupBox.Name = "AdvancedSettingsGroupBox";
            this.AdvancedSettingsGroupBox.Size = new System.Drawing.Size(270, 108);
            this.AdvancedSettingsGroupBox.TabIndex = 143;
            this.AdvancedSettingsGroupBox.TabStop = false;
            this.AdvancedSettingsGroupBox.Text = "Advanced Settings";
            // 
            // ChangeRangeLabel
            // 
            this.ChangeRangeLabel.AutoSize = true;
            this.ChangeRangeLabel.Location = new System.Drawing.Point(122, 84);
            this.ChangeRangeLabel.Name = "ChangeRangeLabel";
            this.ChangeRangeLabel.Size = new System.Drawing.Size(16, 13);
            this.ChangeRangeLabel.TabIndex = 143;
            this.ChangeRangeLabel.Text = "to";
            // 
            // NumericMaxDepth
            // 
            this.NumericMaxDepth.Location = new System.Drawing.Point(144, 82);
            this.NumericMaxDepth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NumericMaxDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericMaxDepth.Name = "NumericMaxDepth";
            this.NumericMaxDepth.Size = new System.Drawing.Size(59, 20);
            this.NumericMaxDepth.TabIndex = 142;
            this.NumericMaxDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // NumberOfChangesLabel
            // 
            this.NumberOfChangesLabel.AutoSize = true;
            this.NumberOfChangesLabel.Location = new System.Drawing.Point(57, 66);
            this.NumberOfChangesLabel.Name = "NumberOfChangesLabel";
            this.NumberOfChangesLabel.Size = new System.Drawing.Size(152, 13);
            this.NumberOfChangesLabel.TabIndex = 141;
            this.NumberOfChangesLabel.Text = "Expected Number of Changes:";
            // 
            // NumericMinDepth
            // 
            this.NumericMinDepth.Location = new System.Drawing.Point(57, 82);
            this.NumericMinDepth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NumericMinDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericMinDepth.Name = "NumericMinDepth";
            this.NumericMinDepth.Size = new System.Drawing.Size(59, 20);
            this.NumericMinDepth.TabIndex = 140;
            this.NumericMinDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // TreeSplitsValueLabel
            // 
            this.TreeSplitsValueLabel.AutoSize = true;
            this.TreeSplitsValueLabel.Location = new System.Drawing.Point(82, 50);
            this.TreeSplitsValueLabel.Name = "TreeSplitsValueLabel";
            this.TreeSplitsValueLabel.Size = new System.Drawing.Size(13, 13);
            this.TreeSplitsValueLabel.TabIndex = 147;
            this.TreeSplitsValueLabel.Text = "0";
            // 
            // TreeSplitsLabel
            // 
            this.TreeSplitsLabel.AutoSize = true;
            this.TreeSplitsLabel.Location = new System.Drawing.Point(6, 50);
            this.TreeSplitsLabel.Name = "TreeSplitsLabel";
            this.TreeSplitsLabel.Size = new System.Drawing.Size(60, 13);
            this.TreeSplitsLabel.TabIndex = 146;
            this.TreeSplitsLabel.Text = "Tree Splits:";
            // 
            // HashSizeLabel
            // 
            this.HashSizeLabel.AutoSize = true;
            this.HashSizeLabel.Location = new System.Drawing.Point(6, 63);
            this.HashSizeLabel.Name = "HashSizeLabel";
            this.HashSizeLabel.Size = new System.Drawing.Size(70, 13);
            this.HashSizeLabel.TabIndex = 144;
            this.HashSizeLabel.Text = "Memory Size:";
            // 
            // MemorySizeValueLabel
            // 
            this.MemorySizeValueLabel.AutoSize = true;
            this.MemorySizeValueLabel.Location = new System.Drawing.Point(82, 63);
            this.MemorySizeValueLabel.Name = "MemorySizeValueLabel";
            this.MemorySizeValueLabel.Size = new System.Drawing.Size(13, 13);
            this.MemorySizeValueLabel.TabIndex = 145;
            this.MemorySizeValueLabel.Text = "0";
            // 
            // VariableSizeTrackBar
            // 
            this.VariableSizeTrackBar.LargeChange = 4;
            this.VariableSizeTrackBar.Location = new System.Drawing.Point(129, 16);
            this.VariableSizeTrackBar.Maximum = 5;
            this.VariableSizeTrackBar.Name = "VariableSizeTrackBar";
            this.VariableSizeTrackBar.Size = new System.Drawing.Size(132, 45);
            this.VariableSizeTrackBar.TabIndex = 145;
            this.VariableSizeTrackBar.Value = 3;
            this.VariableSizeTrackBar.Scroll += new System.EventHandler(this.VariableSizeTrackBar_Scroll);
            // 
            // VariableSizeValueLabel
            // 
            this.VariableSizeValueLabel.AutoSize = true;
            this.VariableSizeValueLabel.Location = new System.Drawing.Point(216, 48);
            this.VariableSizeValueLabel.Name = "VariableSizeValueLabel";
            this.VariableSizeValueLabel.Size = new System.Drawing.Size(42, 13);
            this.VariableSizeValueLabel.TabIndex = 147;
            this.VariableSizeValueLabel.Text = "0 Bytes";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(139, 48);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(71, 13);
            this.VariableSizeLabel.TabIndex = 146;
            this.VariableSizeLabel.Text = "Variable Size:";
            // 
            // DummyToolStrip
            // 
            this.DummyToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.DummyToolStrip.Location = new System.Drawing.Point(0, 0);
            this.DummyToolStrip.Name = "DummyToolStrip";
            this.DummyToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.DummyToolStrip.Size = new System.Drawing.Size(276, 25);
            this.DummyToolStrip.TabIndex = 148;
            // 
            // GUIFilterTreeScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AdvancedSettingsGroupBox);
            this.Controls.Add(this.ScanToolStrip);
            this.Controls.Add(this.TreeSplitsValueLabel);
            this.Controls.Add(this.TreeSplitsLabel);
            this.Controls.Add(this.MemorySizeValueLabel);
            this.Controls.Add(this.HashSizeLabel);
            this.Controls.Add(this.DummyToolStrip);
            this.Name = "GUIFilterTreeScan";
            this.Size = new System.Drawing.Size(276, 303);
            this.Resize += new System.EventHandler(this.GUIMemoryTreeFilter_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.FragmentSizeTrackBar)).EndInit();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.AdvancedSettingsGroupBox.ResumeLayout(false);
            this.AdvancedSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMinDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar FragmentSizeTrackBar;
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
        private System.Windows.Forms.Label ChangeRangeLabel;
        private System.Windows.Forms.NumericUpDown NumericMaxDepth;
        private System.Windows.Forms.Label NumberOfChangesLabel;
        private System.Windows.Forms.NumericUpDown NumericMinDepth;
        private System.Windows.Forms.Label VariableSizeValueLabel;
        private System.Windows.Forms.Label VariableSizeLabel;
        private System.Windows.Forms.TrackBar VariableSizeTrackBar;
        private System.Windows.Forms.ToolStrip DummyToolStrip;
    }
}
