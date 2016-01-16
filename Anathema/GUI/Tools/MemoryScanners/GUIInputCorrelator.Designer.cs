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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.VariableSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.VariableSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteNodeButton = new System.Windows.Forms.ToolStripButton();
            this.ClearInputsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AddInputButton = new System.Windows.Forms.ToolStripButton();
            this.AddNOTButton = new System.Windows.Forms.ToolStripButton();
            this.AddANDButton = new System.Windows.Forms.ToolStripButton();
            this.AddORButton = new System.Windows.Forms.ToolStripButton();
            this.InputContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputTextBox = new Anathema.WatermarkTextBox();
            this.InputTreeView = new Anathema.HighlightPreservingTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            this.InputContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // VariableSizeValueLabel
            // 
            this.VariableSizeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VariableSizeValueLabel.AutoSize = true;
            this.VariableSizeValueLabel.Location = new System.Drawing.Point(328, 64);
            this.VariableSizeValueLabel.Name = "VariableSizeValueLabel";
            this.VariableSizeValueLabel.Size = new System.Drawing.Size(20, 13);
            this.VariableSizeValueLabel.TabIndex = 152;
            this.VariableSizeValueLabel.Text = "0B";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(261, 64);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(71, 13);
            this.VariableSizeLabel.TabIndex = 151;
            this.VariableSizeLabel.Text = "Variable Size:";
            // 
            // VariableSizeTrackBar
            // 
            this.VariableSizeTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VariableSizeTrackBar.LargeChange = 2;
            this.VariableSizeTrackBar.Location = new System.Drawing.Point(264, 34);
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
            this.DeleteNodeButton,
            this.ClearInputsButton,
            this.toolStripSeparator2,
            this.AddInputButton,
            this.AddNOTButton,
            this.AddANDButton,
            this.AddORButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(422, 25);
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
            // DeleteNodeButton
            // 
            this.DeleteNodeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteNodeButton.Image = global::Anathema.Properties.Resources.X;
            this.DeleteNodeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteNodeButton.Name = "DeleteNodeButton";
            this.DeleteNodeButton.Size = new System.Drawing.Size(23, 22);
            this.DeleteNodeButton.Text = "Delete Selection";
            this.DeleteNodeButton.Click += new System.EventHandler(this.DeleteNodeButton_Click);
            // 
            // ClearInputsButton
            // 
            this.ClearInputsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearInputsButton.Image = global::Anathema.Properties.Resources.Cancel;
            this.ClearInputsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearInputsButton.Name = "ClearInputsButton";
            this.ClearInputsButton.Size = new System.Drawing.Size(23, 22);
            this.ClearInputsButton.Text = "Clear Inputs";
            this.ClearInputsButton.Click += new System.EventHandler(this.ClearInputsButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // AddInputButton
            // 
            this.AddInputButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddInputButton.Image = global::Anathema.Properties.Resources.DownArrows;
            this.AddInputButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddInputButton.Name = "AddInputButton";
            this.AddInputButton.Size = new System.Drawing.Size(23, 22);
            this.AddInputButton.Text = "Add Input";
            this.AddInputButton.Click += new System.EventHandler(this.AddInputButton_Click);
            // 
            // AddNOTButton
            // 
            this.AddNOTButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddNOTButton.Image = global::Anathema.Properties.Resources.Negation;
            this.AddNOTButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddNOTButton.Name = "AddNOTButton";
            this.AddNOTButton.Size = new System.Drawing.Size(23, 22);
            this.AddNOTButton.Text = "Add Logical NOT";
            this.AddNOTButton.Click += new System.EventHandler(this.AddNOTButton_Click);
            // 
            // AddANDButton
            // 
            this.AddANDButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddANDButton.Image = global::Anathema.Properties.Resources.Increased;
            this.AddANDButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddANDButton.Name = "AddANDButton";
            this.AddANDButton.Size = new System.Drawing.Size(23, 22);
            this.AddANDButton.Text = "Add Logical AND";
            this.AddANDButton.Click += new System.EventHandler(this.AddANDButton_Click);
            // 
            // AddORButton
            // 
            this.AddORButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddORButton.Image = global::Anathema.Properties.Resources.Decreased;
            this.AddORButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddORButton.Name = "AddORButton";
            this.AddORButton.Size = new System.Drawing.Size(23, 22);
            this.AddORButton.Text = "Add Logical OR";
            this.AddORButton.Click += new System.EventHandler(this.AddORButton_Click);
            // 
            // InputContextMenuStrip
            // 
            this.InputContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem});
            this.InputContextMenuStrip.Name = "InputContextMenuStrip";
            this.InputContextMenuStrip.Size = new System.Drawing.Size(108, 26);
            this.InputContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.InputContextMenuStrip_Opening);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.DeleteToolStripMenuItem.Text = "Delete";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // InputTextBox
            // 
            this.InputTextBox.AcceptsReturn = true;
            this.InputTextBox.AcceptsTab = true;
            this.InputTextBox.Location = new System.Drawing.Point(12, 34);
            this.InputTextBox.Multiline = true;
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(159, 20);
            this.InputTextBox.TabIndex = 176;
            this.InputTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.InputTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputTextBox.WaterMarkText = "Press a Key";
            this.InputTextBox.TextChanged += new System.EventHandler(this.InputTextBox_TextChanged);
            this.InputTextBox.Enter += new System.EventHandler(this.InputTextBox_Enter);
            this.InputTextBox.Leave += new System.EventHandler(this.InputTextBox_Leave);
            // 
            // InputTreeView
            // 
            this.InputTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputTreeView.Location = new System.Drawing.Point(12, 113);
            this.InputTreeView.Name = "InputTreeView";
            this.InputTreeView.Size = new System.Drawing.Size(398, 172);
            this.InputTreeView.TabIndex = 171;
            // 
            // GUIInputCorrelator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 297);
            this.Controls.Add(this.InputTextBox);
            this.Controls.Add(this.InputTreeView);
            this.Controls.Add(this.VariableSizeValueLabel);
            this.Controls.Add(this.VariableSizeLabel);
            this.Controls.Add(this.VariableSizeTrackBar);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIInputCorrelator";
            this.Text = "Input Correlator";
            this.Resize += new System.EventHandler(this.GUILabelerInputCorrelator_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).EndInit();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.InputContextMenuStrip.ResumeLayout(false);
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
        private HighlightPreservingTreeView InputTreeView;
        private WatermarkTextBox InputTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton DeleteNodeButton;
        private System.Windows.Forms.ToolStripButton ClearInputsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton AddInputButton;
        private System.Windows.Forms.ToolStripButton AddNOTButton;
        private System.Windows.Forms.ToolStripButton AddANDButton;
        private System.Windows.Forms.ToolStripButton AddORButton;
        private System.Windows.Forms.ContextMenuStrip InputContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
    }
}