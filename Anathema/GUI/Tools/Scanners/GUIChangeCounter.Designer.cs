namespace Anathema.GUI
{
    partial class GUIChangeCounter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIChangeCounter));
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.ScanCountLabel = new System.Windows.Forms.ToolStripLabel();
            this.MaxChangesValueLabel = new System.Windows.Forms.Label();
            this.MaxChangesLabel = new System.Windows.Forms.Label();
            this.MaxChangesTrackBar = new System.Windows.Forms.TrackBar();
            this.MinChangesValueLabel = new System.Windows.Forms.Label();
            this.MinChangesLabel = new System.Windows.Forms.Label();
            this.MinChangesTrackBar = new System.Windows.Forms.TrackBar();
            this.VariableSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.VariableSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxChangesTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinChangesTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).BeginInit();
            this.SuspendLayout();
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
            this.ScanToolStrip.Size = new System.Drawing.Size(284, 25);
            this.ScanToolStrip.TabIndex = 140;
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
            // ScanCountLabel
            // 
            this.ScanCountLabel.Name = "ScanCountLabel";
            this.ScanCountLabel.Size = new System.Drawing.Size(80, 22);
            this.ScanCountLabel.Text = "Scan Count: 0";
            // 
            // MaxChangesValueLabel
            // 
            this.MaxChangesValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxChangesValueLabel.AutoSize = true;
            this.MaxChangesValueLabel.Location = new System.Drawing.Point(246, 60);
            this.MaxChangesValueLabel.Name = "MaxChangesValueLabel";
            this.MaxChangesValueLabel.Size = new System.Drawing.Size(13, 13);
            this.MaxChangesValueLabel.TabIndex = 160;
            this.MaxChangesValueLabel.Text = "0";
            // 
            // MaxChangesLabel
            // 
            this.MaxChangesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxChangesLabel.AutoSize = true;
            this.MaxChangesLabel.Location = new System.Drawing.Point(174, 60);
            this.MaxChangesLabel.Name = "MaxChangesLabel";
            this.MaxChangesLabel.Size = new System.Drawing.Size(75, 13);
            this.MaxChangesLabel.TabIndex = 159;
            this.MaxChangesLabel.Text = "Max Changes:";
            // 
            // MaxChangesTrackBar
            // 
            this.MaxChangesTrackBar.Location = new System.Drawing.Point(146, 28);
            this.MaxChangesTrackBar.Maximum = 16;
            this.MaxChangesTrackBar.Name = "MaxChangesTrackBar";
            this.MaxChangesTrackBar.Size = new System.Drawing.Size(126, 45);
            this.MaxChangesTrackBar.TabIndex = 161;
            this.MaxChangesTrackBar.Value = 16;
            this.MaxChangesTrackBar.Scroll += new System.EventHandler(this.MaxChangesTrackBar_Scroll);
            // 
            // MinChangesValueLabel
            // 
            this.MinChangesValueLabel.AutoSize = true;
            this.MinChangesValueLabel.Location = new System.Drawing.Point(71, 60);
            this.MinChangesValueLabel.Name = "MinChangesValueLabel";
            this.MinChangesValueLabel.Size = new System.Drawing.Size(13, 13);
            this.MinChangesValueLabel.TabIndex = 157;
            this.MinChangesValueLabel.Text = "0";
            // 
            // MinChangesLabel
            // 
            this.MinChangesLabel.AutoSize = true;
            this.MinChangesLabel.Location = new System.Drawing.Point(2, 60);
            this.MinChangesLabel.Name = "MinChangesLabel";
            this.MinChangesLabel.Size = new System.Drawing.Size(72, 13);
            this.MinChangesLabel.TabIndex = 156;
            this.MinChangesLabel.Text = "Min Changes:";
            // 
            // MinChangesTrackBar
            // 
            this.MinChangesTrackBar.Location = new System.Drawing.Point(0, 28);
            this.MinChangesTrackBar.Maximum = 15;
            this.MinChangesTrackBar.Name = "MinChangesTrackBar";
            this.MinChangesTrackBar.Size = new System.Drawing.Size(124, 45);
            this.MinChangesTrackBar.TabIndex = 158;
            this.MinChangesTrackBar.Value = 1;
            this.MinChangesTrackBar.Scroll += new System.EventHandler(this.MinChangesTrackBar_Scroll);
            // 
            // VariableSizeValueLabel
            // 
            this.VariableSizeValueLabel.AutoSize = true;
            this.VariableSizeValueLabel.Location = new System.Drawing.Point(74, 111);
            this.VariableSizeValueLabel.Name = "VariableSizeValueLabel";
            this.VariableSizeValueLabel.Size = new System.Drawing.Size(13, 13);
            this.VariableSizeValueLabel.TabIndex = 163;
            this.VariableSizeValueLabel.Text = "0";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(7, 111);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(71, 13);
            this.VariableSizeLabel.TabIndex = 162;
            this.VariableSizeLabel.Text = "Variable Size:";
            // 
            // VariableSizeTrackBar
            // 
            this.VariableSizeTrackBar.Location = new System.Drawing.Point(5, 79);
            this.VariableSizeTrackBar.Maximum = 3;
            this.VariableSizeTrackBar.Name = "VariableSizeTrackBar";
            this.VariableSizeTrackBar.Size = new System.Drawing.Size(119, 45);
            this.VariableSizeTrackBar.TabIndex = 164;
            this.VariableSizeTrackBar.Value = 2;
            this.VariableSizeTrackBar.Scroll += new System.EventHandler(this.VariableSizeTrackBar_Scroll);
            // 
            // GUIChangeCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 146);
            this.Controls.Add(this.VariableSizeValueLabel);
            this.Controls.Add(this.VariableSizeLabel);
            this.Controls.Add(this.VariableSizeTrackBar);
            this.Controls.Add(this.MaxChangesValueLabel);
            this.Controls.Add(this.MaxChangesLabel);
            this.Controls.Add(this.MaxChangesTrackBar);
            this.Controls.Add(this.MinChangesValueLabel);
            this.Controls.Add(this.MinChangesLabel);
            this.Controls.Add(this.MinChangesTrackBar);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIChangeCounter";
            this.Text = "Change Counter";
            this.Resize += new System.EventHandler(this.GUILabelerChangeCounter_Resize);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxChangesTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinChangesTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStripButton StopScanButton;
        private System.Windows.Forms.Label MaxChangesValueLabel;
        private System.Windows.Forms.Label MaxChangesLabel;
        private System.Windows.Forms.TrackBar MaxChangesTrackBar;
        private System.Windows.Forms.Label MinChangesValueLabel;
        private System.Windows.Forms.Label MinChangesLabel;
        private System.Windows.Forms.TrackBar MinChangesTrackBar;
        private System.Windows.Forms.Label VariableSizeValueLabel;
        private System.Windows.Forms.Label VariableSizeLabel;
        private System.Windows.Forms.TrackBar VariableSizeTrackBar;
        private System.Windows.Forms.ToolStripLabel ScanCountLabel;
    }
}