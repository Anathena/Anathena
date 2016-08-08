namespace Anathema.GUI.Tools.Scanners
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIInputCorrelator));
            this.VariableSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.VariableSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ScanCountLabel = new System.Windows.Forms.ToolStripLabel();
            this.EditKeysButton = new System.Windows.Forms.Button();
            this.HotKeyListView = new Anathema.GUI.CustomControls.ListViews.FlickerFreeListView();
            this.HotKeyHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // VariableSizeValueLabel
            // 
            this.VariableSizeValueLabel.AutoSize = true;
            this.VariableSizeValueLabel.Location = new System.Drawing.Point(86, 60);
            this.VariableSizeValueLabel.Name = "VariableSizeValueLabel";
            this.VariableSizeValueLabel.Size = new System.Drawing.Size(20, 13);
            this.VariableSizeValueLabel.TabIndex = 152;
            this.VariableSizeValueLabel.Text = "0B";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(19, 60);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(71, 13);
            this.VariableSizeLabel.TabIndex = 151;
            this.VariableSizeLabel.Text = "Variable Size:";
            // 
            // VariableSizeTrackBar
            // 
            this.VariableSizeTrackBar.LargeChange = 2;
            this.VariableSizeTrackBar.Location = new System.Drawing.Point(12, 28);
            this.VariableSizeTrackBar.Maximum = 3;
            this.VariableSizeTrackBar.Name = "VariableSizeTrackBar";
            this.VariableSizeTrackBar.Size = new System.Drawing.Size(146, 45);
            this.VariableSizeTrackBar.TabIndex = 149;
            this.VariableSizeTrackBar.Value = 2;
            this.VariableSizeTrackBar.Scroll += new System.EventHandler(this.VariableSizeTrackBar_Scroll);
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartScanButton,
            this.StopScanButton,
            this.toolStripSeparator1,
            this.ScanCountLabel});
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
            this.StopScanButton.Text = "Stop Scan";
            this.StopScanButton.Click += new System.EventHandler(this.StopScanButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ScanCountLabel
            // 
            this.ScanCountLabel.Name = "ScanCountLabel";
            this.ScanCountLabel.Size = new System.Drawing.Size(80, 22);
            this.ScanCountLabel.Text = "Scan Count: 0";
            // 
            // EditKeysButton
            // 
            this.EditKeysButton.Location = new System.Drawing.Point(164, 28);
            this.EditKeysButton.Name = "EditKeysButton";
            this.EditKeysButton.Size = new System.Drawing.Size(75, 23);
            this.EditKeysButton.TabIndex = 177;
            this.EditKeysButton.Text = "Edit Keys";
            this.EditKeysButton.UseVisualStyleBackColor = true;
            this.EditKeysButton.Click += new System.EventHandler(this.EditKeysButton_Click);
            // 
            // HotKeyListView
            // 
            this.HotKeyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HotKeyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HotKeyHeader});
            this.HotKeyListView.FullRowSelect = true;
            this.HotKeyListView.Location = new System.Drawing.Point(12, 79);
            this.HotKeyListView.Name = "HotKeyListView";
            this.HotKeyListView.Size = new System.Drawing.Size(280, 94);
            this.HotKeyListView.TabIndex = 178;
            this.HotKeyListView.UseCompatibleStateImageBehavior = false;
            this.HotKeyListView.View = System.Windows.Forms.View.Details;
            // 
            // HotKeyHeader
            // 
            this.HotKeyHeader.Text = "Hot Key";
            this.HotKeyHeader.Width = 256;
            // 
            // GUIInputCorrelator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 185);
            this.Controls.Add(this.HotKeyListView);
            this.Controls.Add(this.EditKeysButton);
            this.Controls.Add(this.VariableSizeValueLabel);
            this.Controls.Add(this.VariableSizeLabel);
            this.Controls.Add(this.VariableSizeTrackBar);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIInputCorrelator";
            this.Text = "Correlator";
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel ScanCountLabel;
        private System.Windows.Forms.Button EditKeysButton;
        private CustomControls.ListViews.FlickerFreeListView HotKeyListView;
        private System.Windows.Forms.ColumnHeader HotKeyHeader;
    }
}