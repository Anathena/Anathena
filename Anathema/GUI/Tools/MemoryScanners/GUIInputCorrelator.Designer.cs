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
            this.VariableSizeValueLabel = new System.Windows.Forms.Label();
            this.VariableSizeLabel = new System.Windows.Forms.Label();
            this.VariableSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.EitherRadioButton = new System.Windows.Forms.RadioButton();
            this.UserToVariableRadioButton = new System.Windows.Forms.RadioButton();
            this.VariableToUserRadioButton = new System.Windows.Forms.RadioButton();
            this.InputTreeView = new System.Windows.Forms.TreeView();
            this.AddANDButton = new System.Windows.Forms.Button();
            this.AddORButton = new System.Windows.Forms.Button();
            this.AddNOTButton = new System.Windows.Forms.Button();
            this.AddInputButton = new System.Windows.Forms.Button();
            this.InputTextBox = new Anathema.WatermarkTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.VariableSizeTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // VariableSizeValueLabel
            // 
            this.VariableSizeValueLabel.AutoSize = true;
            this.VariableSizeValueLabel.Location = new System.Drawing.Point(67, 60);
            this.VariableSizeValueLabel.Name = "VariableSizeValueLabel";
            this.VariableSizeValueLabel.Size = new System.Drawing.Size(20, 13);
            this.VariableSizeValueLabel.TabIndex = 152;
            this.VariableSizeValueLabel.Text = "0B";
            // 
            // VariableSizeLabel
            // 
            this.VariableSizeLabel.AutoSize = true;
            this.VariableSizeLabel.Location = new System.Drawing.Point(0, 60);
            this.VariableSizeLabel.Name = "VariableSizeLabel";
            this.VariableSizeLabel.Size = new System.Drawing.Size(71, 13);
            this.VariableSizeLabel.TabIndex = 151;
            this.VariableSizeLabel.Text = "Variable Size:";
            // 
            // VariableSizeTrackBar
            // 
            this.VariableSizeTrackBar.LargeChange = 2;
            this.VariableSizeTrackBar.Location = new System.Drawing.Point(0, 28);
            this.VariableSizeTrackBar.Maximum = 3;
            this.VariableSizeTrackBar.Name = "VariableSizeTrackBar";
            this.VariableSizeTrackBar.Size = new System.Drawing.Size(140, 45);
            this.VariableSizeTrackBar.TabIndex = 149;
            this.VariableSizeTrackBar.Value = 2;
            this.VariableSizeTrackBar.Scroll += new System.EventHandler(this.VariableSizeTrackBar_Scroll);
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
            this.ScanToolStrip.Size = new System.Drawing.Size(345, 25);
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
            // EitherRadioButton
            // 
            this.EitherRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EitherRadioButton.AutoSize = true;
            this.EitherRadioButton.Location = new System.Drawing.Point(182, 62);
            this.EitherRadioButton.Name = "EitherRadioButton";
            this.EitherRadioButton.Size = new System.Drawing.Size(52, 17);
            this.EitherRadioButton.TabIndex = 170;
            this.EitherRadioButton.Text = "Either";
            this.EitherRadioButton.UseVisualStyleBackColor = true;
            this.EitherRadioButton.CheckedChanged += new System.EventHandler(this.EitherRadioButton_CheckedChanged);
            // 
            // UserToVariableRadioButton
            // 
            this.UserToVariableRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UserToVariableRadioButton.AutoSize = true;
            this.UserToVariableRadioButton.Location = new System.Drawing.Point(182, 45);
            this.UserToVariableRadioButton.Name = "UserToVariableRadioButton";
            this.UserToVariableRadioButton.Size = new System.Drawing.Size(151, 17);
            this.UserToVariableRadioButton.TabIndex = 169;
            this.UserToVariableRadioButton.Text = "User Responds to Variable";
            this.UserToVariableRadioButton.UseVisualStyleBackColor = true;
            this.UserToVariableRadioButton.CheckedChanged += new System.EventHandler(this.UserToVariableRadioButton_CheckedChanged);
            // 
            // VariableToUserRadioButton
            // 
            this.VariableToUserRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VariableToUserRadioButton.AutoSize = true;
            this.VariableToUserRadioButton.Checked = true;
            this.VariableToUserRadioButton.Location = new System.Drawing.Point(182, 28);
            this.VariableToUserRadioButton.Name = "VariableToUserRadioButton";
            this.VariableToUserRadioButton.Size = new System.Drawing.Size(151, 17);
            this.VariableToUserRadioButton.TabIndex = 168;
            this.VariableToUserRadioButton.TabStop = true;
            this.VariableToUserRadioButton.Text = "Variable Responds to User";
            this.VariableToUserRadioButton.UseVisualStyleBackColor = true;
            this.VariableToUserRadioButton.CheckedChanged += new System.EventHandler(this.VariableToUserRadioButton_CheckedChanged);
            // 
            // InputTreeView
            // 
            this.InputTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputTreeView.Location = new System.Drawing.Point(12, 150);
            this.InputTreeView.Name = "InputTreeView";
            this.InputTreeView.Size = new System.Drawing.Size(321, 84);
            this.InputTreeView.TabIndex = 171;
            // 
            // AddANDButton
            // 
            this.AddANDButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddANDButton.Location = new System.Drawing.Point(177, 121);
            this.AddANDButton.Name = "AddANDButton";
            this.AddANDButton.Size = new System.Drawing.Size(75, 23);
            this.AddANDButton.TabIndex = 172;
            this.AddANDButton.Text = "Add AND";
            this.AddANDButton.UseVisualStyleBackColor = true;
            this.AddANDButton.Click += new System.EventHandler(this.AddANDButton_Click);
            // 
            // AddORButton
            // 
            this.AddORButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddORButton.Location = new System.Drawing.Point(258, 121);
            this.AddORButton.Name = "AddORButton";
            this.AddORButton.Size = new System.Drawing.Size(75, 23);
            this.AddORButton.TabIndex = 173;
            this.AddORButton.Text = "Add OR";
            this.AddORButton.UseVisualStyleBackColor = true;
            this.AddORButton.Click += new System.EventHandler(this.AddORButton_Click);
            // 
            // AddNOTButton
            // 
            this.AddNOTButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddNOTButton.Location = new System.Drawing.Point(96, 121);
            this.AddNOTButton.Name = "AddNOTButton";
            this.AddNOTButton.Size = new System.Drawing.Size(75, 23);
            this.AddNOTButton.TabIndex = 174;
            this.AddNOTButton.Text = "Add NOT";
            this.AddNOTButton.UseVisualStyleBackColor = true;
            this.AddNOTButton.Click += new System.EventHandler(this.AddNOTButton_Click);
            // 
            // AddInputButton
            // 
            this.AddInputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddInputButton.Location = new System.Drawing.Point(12, 121);
            this.AddInputButton.Name = "AddInputButton";
            this.AddInputButton.Size = new System.Drawing.Size(75, 23);
            this.AddInputButton.TabIndex = 175;
            this.AddInputButton.Text = "Add Input";
            this.AddInputButton.UseVisualStyleBackColor = true;
            this.AddInputButton.Click += new System.EventHandler(this.AddInputButton_Click);
            // 
            // InputTextBox
            // 
            this.InputTextBox.AcceptsReturn = true;
            this.InputTextBox.AcceptsTab = true;
            this.InputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InputTextBox.Location = new System.Drawing.Point(12, 95);
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
            // GUIInputCorrelator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 246);
            this.Controls.Add(this.InputTextBox);
            this.Controls.Add(this.AddInputButton);
            this.Controls.Add(this.AddNOTButton);
            this.Controls.Add(this.AddORButton);
            this.Controls.Add(this.AddANDButton);
            this.Controls.Add(this.InputTreeView);
            this.Controls.Add(this.EitherRadioButton);
            this.Controls.Add(this.UserToVariableRadioButton);
            this.Controls.Add(this.VariableToUserRadioButton);
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
        private System.Windows.Forms.RadioButton EitherRadioButton;
        private System.Windows.Forms.RadioButton UserToVariableRadioButton;
        private System.Windows.Forms.RadioButton VariableToUserRadioButton;
        private System.Windows.Forms.TreeView InputTreeView;
        private System.Windows.Forms.Button AddANDButton;
        private System.Windows.Forms.Button AddORButton;
        private System.Windows.Forms.Button AddNOTButton;
        private System.Windows.Forms.Button AddInputButton;
        private WatermarkTextBox InputTextBox;
    }
}