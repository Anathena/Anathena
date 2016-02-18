namespace Anathema
{
    partial class GUISettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUISettings));
            this.AcceptButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SettingsTabControl = new System.Windows.Forms.TabControl();
            this.ScanTabPage = new System.Windows.Forms.TabPage();
            this.FilteredProtectionFlagsGroupBox = new System.Windows.Forms.GroupBox();
            this.FilteredExecuteCheckBox = new System.Windows.Forms.CheckBox();
            this.FilteredWriteCheckBox = new System.Windows.Forms.CheckBox();
            this.CopyOnWriteCheckBox = new System.Windows.Forms.CheckBox();
            this.GeneralGroupBox = new System.Windows.Forms.GroupBox();
            this.AlignmentLabel = new System.Windows.Forms.Label();
            this.MemoryTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.NoneCheckBox = new System.Windows.Forms.CheckBox();
            this.PrivateCheckBox = new System.Windows.Forms.CheckBox();
            this.ImageCheckBox = new System.Windows.Forms.CheckBox();
            this.MappedCheckBox = new System.Windows.Forms.CheckBox();
            this.MemoryProtectionGroupBox = new System.Windows.Forms.GroupBox();
            this.RequiredExecuteCheckBox = new System.Windows.Forms.CheckBox();
            this.RequiredWriteCheckBox = new System.Windows.Forms.CheckBox();
            this.RequiredCopyOnWriteCheckBox = new System.Windows.Forms.CheckBox();
            this.GeneralTabPage = new System.Windows.Forms.TabPage();
            this.IntervalsGroupBox = new System.Windows.Forms.GroupBox();
            this.InputCorrelatorTimeoutIntervalLabel = new System.Windows.Forms.Label();
            this.InputCorrelatorTimeoutIntervalTextBox = new System.Windows.Forms.TextBox();
            this.RescanIntervalTextBox = new System.Windows.Forms.TextBox();
            this.ResultsReadIntervalLabel = new System.Windows.Forms.Label();
            this.FreezeIntervalTextBox = new System.Windows.Forms.TextBox();
            this.TableReadIntervalLabel = new System.Windows.Forms.Label();
            this.TableReadIntervalTextBox = new System.Windows.Forms.TextBox();
            this.FreezeIntervalLabel = new System.Windows.Forms.Label();
            this.ResultsReadIntervalTextBox = new System.Windows.Forms.TextBox();
            this.RescanTimerLabel = new System.Windows.Forms.Label();
            this.AlignmentTextBox = new Anathema.HexDecTextBox();
            this.SettingsTabControl.SuspendLayout();
            this.ScanTabPage.SuspendLayout();
            this.FilteredProtectionFlagsGroupBox.SuspendLayout();
            this.GeneralGroupBox.SuspendLayout();
            this.MemoryTypeGroupBox.SuspendLayout();
            this.MemoryProtectionGroupBox.SuspendLayout();
            this.GeneralTabPage.SuspendLayout();
            this.IntervalsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AcceptButton
            // 
            this.AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AcceptButton.Location = new System.Drawing.Point(285, 294);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptButton.TabIndex = 13;
            this.AcceptButton.Text = "Accept";
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(366, 294);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 14;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // SettingsTabControl
            // 
            this.SettingsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsTabControl.Controls.Add(this.ScanTabPage);
            this.SettingsTabControl.Controls.Add(this.GeneralTabPage);
            this.SettingsTabControl.Location = new System.Drawing.Point(12, 12);
            this.SettingsTabControl.Name = "SettingsTabControl";
            this.SettingsTabControl.SelectedIndex = 0;
            this.SettingsTabControl.Size = new System.Drawing.Size(429, 276);
            this.SettingsTabControl.TabIndex = 15;
            // 
            // ScanTabPage
            // 
            this.ScanTabPage.Controls.Add(this.FilteredProtectionFlagsGroupBox);
            this.ScanTabPage.Controls.Add(this.GeneralGroupBox);
            this.ScanTabPage.Controls.Add(this.MemoryTypeGroupBox);
            this.ScanTabPage.Controls.Add(this.MemoryProtectionGroupBox);
            this.ScanTabPage.Location = new System.Drawing.Point(4, 22);
            this.ScanTabPage.Name = "ScanTabPage";
            this.ScanTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ScanTabPage.Size = new System.Drawing.Size(421, 250);
            this.ScanTabPage.TabIndex = 1;
            this.ScanTabPage.Text = "Scan Settings";
            this.ScanTabPage.UseVisualStyleBackColor = true;
            // 
            // FilteredProtectionFlagsGroupBox
            // 
            this.FilteredProtectionFlagsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilteredProtectionFlagsGroupBox.Controls.Add(this.FilteredExecuteCheckBox);
            this.FilteredProtectionFlagsGroupBox.Controls.Add(this.FilteredWriteCheckBox);
            this.FilteredProtectionFlagsGroupBox.Controls.Add(this.CopyOnWriteCheckBox);
            this.FilteredProtectionFlagsGroupBox.Location = new System.Drawing.Point(159, 6);
            this.FilteredProtectionFlagsGroupBox.Name = "FilteredProtectionFlagsGroupBox";
            this.FilteredProtectionFlagsGroupBox.Size = new System.Drawing.Size(256, 100);
            this.FilteredProtectionFlagsGroupBox.TabIndex = 8;
            this.FilteredProtectionFlagsGroupBox.TabStop = false;
            this.FilteredProtectionFlagsGroupBox.Text = "Required Protection Flags";
            // 
            // FilteredExecuteCheckBox
            // 
            this.FilteredExecuteCheckBox.AutoSize = true;
            this.FilteredExecuteCheckBox.Location = new System.Drawing.Point(63, 28);
            this.FilteredExecuteCheckBox.Name = "FilteredExecuteCheckBox";
            this.FilteredExecuteCheckBox.Size = new System.Drawing.Size(65, 17);
            this.FilteredExecuteCheckBox.TabIndex = 3;
            this.FilteredExecuteCheckBox.Text = "Execute";
            this.FilteredExecuteCheckBox.ThreeState = true;
            this.FilteredExecuteCheckBox.UseVisualStyleBackColor = true;
            // 
            // FilteredWriteCheckBox
            // 
            this.FilteredWriteCheckBox.AutoSize = true;
            this.FilteredWriteCheckBox.Location = new System.Drawing.Point(6, 28);
            this.FilteredWriteCheckBox.Name = "FilteredWriteCheckBox";
            this.FilteredWriteCheckBox.Size = new System.Drawing.Size(51, 17);
            this.FilteredWriteCheckBox.TabIndex = 2;
            this.FilteredWriteCheckBox.Text = "Write";
            this.FilteredWriteCheckBox.ThreeState = true;
            this.FilteredWriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // CopyOnWriteCheckBox
            // 
            this.CopyOnWriteCheckBox.AutoSize = true;
            this.CopyOnWriteCheckBox.Location = new System.Drawing.Point(6, 51);
            this.CopyOnWriteCheckBox.Name = "CopyOnWriteCheckBox";
            this.CopyOnWriteCheckBox.Size = new System.Drawing.Size(93, 17);
            this.CopyOnWriteCheckBox.TabIndex = 1;
            this.CopyOnWriteCheckBox.Text = "Copy on Write";
            this.CopyOnWriteCheckBox.ThreeState = true;
            this.CopyOnWriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // GeneralGroupBox
            // 
            this.GeneralGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GeneralGroupBox.Controls.Add(this.AlignmentTextBox);
            this.GeneralGroupBox.Controls.Add(this.AlignmentLabel);
            this.GeneralGroupBox.Location = new System.Drawing.Point(190, 112);
            this.GeneralGroupBox.Name = "GeneralGroupBox";
            this.GeneralGroupBox.Size = new System.Drawing.Size(225, 132);
            this.GeneralGroupBox.TabIndex = 9;
            this.GeneralGroupBox.TabStop = false;
            this.GeneralGroupBox.Text = "General";
            // 
            // AlignmentLabel
            // 
            this.AlignmentLabel.AutoSize = true;
            this.AlignmentLabel.Location = new System.Drawing.Point(6, 16);
            this.AlignmentLabel.Name = "AlignmentLabel";
            this.AlignmentLabel.Size = new System.Drawing.Size(56, 13);
            this.AlignmentLabel.TabIndex = 1;
            this.AlignmentLabel.Text = "Alignment:";
            // 
            // MemoryTypeGroupBox
            // 
            this.MemoryTypeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MemoryTypeGroupBox.Controls.Add(this.NoneCheckBox);
            this.MemoryTypeGroupBox.Controls.Add(this.PrivateCheckBox);
            this.MemoryTypeGroupBox.Controls.Add(this.ImageCheckBox);
            this.MemoryTypeGroupBox.Controls.Add(this.MappedCheckBox);
            this.MemoryTypeGroupBox.Location = new System.Drawing.Point(6, 112);
            this.MemoryTypeGroupBox.Name = "MemoryTypeGroupBox";
            this.MemoryTypeGroupBox.Size = new System.Drawing.Size(178, 132);
            this.MemoryTypeGroupBox.TabIndex = 8;
            this.MemoryTypeGroupBox.TabStop = false;
            this.MemoryTypeGroupBox.Text = "Memory Type";
            // 
            // NoneCheckBox
            // 
            this.NoneCheckBox.AutoSize = true;
            this.NoneCheckBox.Location = new System.Drawing.Point(6, 19);
            this.NoneCheckBox.Name = "NoneCheckBox";
            this.NoneCheckBox.Size = new System.Drawing.Size(52, 17);
            this.NoneCheckBox.TabIndex = 7;
            this.NoneCheckBox.Text = "None";
            this.NoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // PrivateCheckBox
            // 
            this.PrivateCheckBox.AutoSize = true;
            this.PrivateCheckBox.Location = new System.Drawing.Point(6, 42);
            this.PrivateCheckBox.Name = "PrivateCheckBox";
            this.PrivateCheckBox.Size = new System.Drawing.Size(59, 17);
            this.PrivateCheckBox.TabIndex = 4;
            this.PrivateCheckBox.Text = "Private";
            this.PrivateCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImageCheckBox
            // 
            this.ImageCheckBox.AutoSize = true;
            this.ImageCheckBox.Location = new System.Drawing.Point(71, 19);
            this.ImageCheckBox.Name = "ImageCheckBox";
            this.ImageCheckBox.Size = new System.Drawing.Size(55, 17);
            this.ImageCheckBox.TabIndex = 5;
            this.ImageCheckBox.Text = "Image";
            this.ImageCheckBox.UseVisualStyleBackColor = true;
            // 
            // MappedCheckBox
            // 
            this.MappedCheckBox.AutoSize = true;
            this.MappedCheckBox.Location = new System.Drawing.Point(71, 42);
            this.MappedCheckBox.Name = "MappedCheckBox";
            this.MappedCheckBox.Size = new System.Drawing.Size(65, 17);
            this.MappedCheckBox.TabIndex = 6;
            this.MappedCheckBox.Text = "Mapped";
            this.MappedCheckBox.UseVisualStyleBackColor = true;
            // 
            // MemoryProtectionGroupBox
            // 
            this.MemoryProtectionGroupBox.Controls.Add(this.RequiredExecuteCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.RequiredWriteCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.RequiredCopyOnWriteCheckBox);
            this.MemoryProtectionGroupBox.Location = new System.Drawing.Point(6, 6);
            this.MemoryProtectionGroupBox.Name = "MemoryProtectionGroupBox";
            this.MemoryProtectionGroupBox.Size = new System.Drawing.Size(147, 100);
            this.MemoryProtectionGroupBox.TabIndex = 7;
            this.MemoryProtectionGroupBox.TabStop = false;
            this.MemoryProtectionGroupBox.Text = "Required Protection Flags";
            // 
            // RequiredExecuteCheckBox
            // 
            this.RequiredExecuteCheckBox.AutoSize = true;
            this.RequiredExecuteCheckBox.Location = new System.Drawing.Point(63, 28);
            this.RequiredExecuteCheckBox.Name = "RequiredExecuteCheckBox";
            this.RequiredExecuteCheckBox.Size = new System.Drawing.Size(65, 17);
            this.RequiredExecuteCheckBox.TabIndex = 3;
            this.RequiredExecuteCheckBox.Text = "Execute";
            this.RequiredExecuteCheckBox.ThreeState = true;
            this.RequiredExecuteCheckBox.UseVisualStyleBackColor = true;
            // 
            // RequiredWriteCheckBox
            // 
            this.RequiredWriteCheckBox.AutoSize = true;
            this.RequiredWriteCheckBox.Location = new System.Drawing.Point(6, 28);
            this.RequiredWriteCheckBox.Name = "RequiredWriteCheckBox";
            this.RequiredWriteCheckBox.Size = new System.Drawing.Size(51, 17);
            this.RequiredWriteCheckBox.TabIndex = 2;
            this.RequiredWriteCheckBox.Text = "Write";
            this.RequiredWriteCheckBox.ThreeState = true;
            this.RequiredWriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // RequiredCopyOnWriteCheckBox
            // 
            this.RequiredCopyOnWriteCheckBox.AutoSize = true;
            this.RequiredCopyOnWriteCheckBox.Location = new System.Drawing.Point(6, 51);
            this.RequiredCopyOnWriteCheckBox.Name = "RequiredCopyOnWriteCheckBox";
            this.RequiredCopyOnWriteCheckBox.Size = new System.Drawing.Size(93, 17);
            this.RequiredCopyOnWriteCheckBox.TabIndex = 1;
            this.RequiredCopyOnWriteCheckBox.Text = "Copy on Write";
            this.RequiredCopyOnWriteCheckBox.ThreeState = true;
            this.RequiredCopyOnWriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.Controls.Add(this.IntervalsGroupBox);
            this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTabPage.Size = new System.Drawing.Size(421, 250);
            this.GeneralTabPage.TabIndex = 0;
            this.GeneralTabPage.Text = "General";
            this.GeneralTabPage.UseVisualStyleBackColor = true;
            // 
            // IntervalsGroupBox
            // 
            this.IntervalsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IntervalsGroupBox.Controls.Add(this.InputCorrelatorTimeoutIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.InputCorrelatorTimeoutIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.RescanIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.ResultsReadIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.FreezeIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.TableReadIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.TableReadIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.FreezeIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.ResultsReadIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.RescanTimerLabel);
            this.IntervalsGroupBox.Location = new System.Drawing.Point(6, 6);
            this.IntervalsGroupBox.Name = "IntervalsGroupBox";
            this.IntervalsGroupBox.Size = new System.Drawing.Size(226, 164);
            this.IntervalsGroupBox.TabIndex = 8;
            this.IntervalsGroupBox.TabStop = false;
            this.IntervalsGroupBox.Text = "Intervals (ms)";
            // 
            // InputCorrelatorTimeoutIntervalLabel
            // 
            this.InputCorrelatorTimeoutIntervalLabel.AutoSize = true;
            this.InputCorrelatorTimeoutIntervalLabel.Location = new System.Drawing.Point(80, 126);
            this.InputCorrelatorTimeoutIntervalLabel.Name = "InputCorrelatorTimeoutIntervalLabel";
            this.InputCorrelatorTimeoutIntervalLabel.Size = new System.Drawing.Size(120, 13);
            this.InputCorrelatorTimeoutIntervalLabel.TabIndex = 9;
            this.InputCorrelatorTimeoutIntervalLabel.Text = "Input Correlator Timeout";
            // 
            // InputCorrelatorTimeoutIntervalTextBox
            // 
            this.InputCorrelatorTimeoutIntervalTextBox.Location = new System.Drawing.Point(6, 123);
            this.InputCorrelatorTimeoutIntervalTextBox.Name = "InputCorrelatorTimeoutIntervalTextBox";
            this.InputCorrelatorTimeoutIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.InputCorrelatorTimeoutIntervalTextBox.TabIndex = 8;
            // 
            // RescanIntervalTextBox
            // 
            this.RescanIntervalTextBox.Location = new System.Drawing.Point(6, 45);
            this.RescanIntervalTextBox.Name = "RescanIntervalTextBox";
            this.RescanIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.RescanIntervalTextBox.TabIndex = 3;
            // 
            // ResultsReadIntervalLabel
            // 
            this.ResultsReadIntervalLabel.AutoSize = true;
            this.ResultsReadIntervalLabel.Location = new System.Drawing.Point(80, 100);
            this.ResultsReadIntervalLabel.Name = "ResultsReadIntervalLabel";
            this.ResultsReadIntervalLabel.Size = new System.Drawing.Size(109, 13);
            this.ResultsReadIntervalLabel.TabIndex = 7;
            this.ResultsReadIntervalLabel.Text = "Results Read Interval";
            // 
            // FreezeIntervalTextBox
            // 
            this.FreezeIntervalTextBox.Location = new System.Drawing.Point(6, 19);
            this.FreezeIntervalTextBox.Name = "FreezeIntervalTextBox";
            this.FreezeIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.FreezeIntervalTextBox.TabIndex = 0;
            // 
            // TableReadIntervalLabel
            // 
            this.TableReadIntervalLabel.AutoSize = true;
            this.TableReadIntervalLabel.Location = new System.Drawing.Point(80, 74);
            this.TableReadIntervalLabel.Name = "TableReadIntervalLabel";
            this.TableReadIntervalLabel.Size = new System.Drawing.Size(101, 13);
            this.TableReadIntervalLabel.TabIndex = 6;
            this.TableReadIntervalLabel.Text = "Table Read Interval";
            // 
            // TableReadIntervalTextBox
            // 
            this.TableReadIntervalTextBox.Location = new System.Drawing.Point(6, 71);
            this.TableReadIntervalTextBox.Name = "TableReadIntervalTextBox";
            this.TableReadIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.TableReadIntervalTextBox.TabIndex = 1;
            // 
            // FreezeIntervalLabel
            // 
            this.FreezeIntervalLabel.AutoSize = true;
            this.FreezeIntervalLabel.Location = new System.Drawing.Point(80, 22);
            this.FreezeIntervalLabel.Name = "FreezeIntervalLabel";
            this.FreezeIntervalLabel.Size = new System.Drawing.Size(77, 13);
            this.FreezeIntervalLabel.TabIndex = 5;
            this.FreezeIntervalLabel.Text = "Freeze Interval";
            // 
            // ResultsReadIntervalTextBox
            // 
            this.ResultsReadIntervalTextBox.Location = new System.Drawing.Point(6, 97);
            this.ResultsReadIntervalTextBox.Name = "ResultsReadIntervalTextBox";
            this.ResultsReadIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.ResultsReadIntervalTextBox.TabIndex = 2;
            // 
            // RescanTimerLabel
            // 
            this.RescanTimerLabel.AutoSize = true;
            this.RescanTimerLabel.Location = new System.Drawing.Point(80, 48);
            this.RescanTimerLabel.Name = "RescanTimerLabel";
            this.RescanTimerLabel.Size = new System.Drawing.Size(82, 13);
            this.RescanTimerLabel.TabIndex = 4;
            this.RescanTimerLabel.Text = "Rescan Interval";
            // 
            // AlignmentTextBox
            // 
            this.AlignmentTextBox.ForeColor = System.Drawing.Color.Red;
            this.AlignmentTextBox.IsHex = false;
            this.AlignmentTextBox.Location = new System.Drawing.Point(68, 13);
            this.AlignmentTextBox.Name = "AlignmentTextBox";
            this.AlignmentTextBox.Size = new System.Drawing.Size(47, 20);
            this.AlignmentTextBox.TabIndex = 2;
            this.AlignmentTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.AlignmentTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlignmentTextBox.WaterMarkText = null;
            // 
            // GUISettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 321);
            this.Controls.Add(this.SettingsTabControl);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AcceptButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUISettings";
            this.Text = "Settings";
            this.SettingsTabControl.ResumeLayout(false);
            this.ScanTabPage.ResumeLayout(false);
            this.FilteredProtectionFlagsGroupBox.ResumeLayout(false);
            this.FilteredProtectionFlagsGroupBox.PerformLayout();
            this.GeneralGroupBox.ResumeLayout(false);
            this.GeneralGroupBox.PerformLayout();
            this.MemoryTypeGroupBox.ResumeLayout(false);
            this.MemoryTypeGroupBox.PerformLayout();
            this.MemoryProtectionGroupBox.ResumeLayout(false);
            this.MemoryProtectionGroupBox.PerformLayout();
            this.GeneralTabPage.ResumeLayout(false);
            this.IntervalsGroupBox.ResumeLayout(false);
            this.IntervalsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TabControl SettingsTabControl;
        private System.Windows.Forms.TabPage GeneralTabPage;
        private System.Windows.Forms.TabPage ScanTabPage;
        private System.Windows.Forms.CheckBox RequiredExecuteCheckBox;
        private System.Windows.Forms.CheckBox RequiredWriteCheckBox;
        private System.Windows.Forms.CheckBox RequiredCopyOnWriteCheckBox;
        private System.Windows.Forms.CheckBox PrivateCheckBox;
        private System.Windows.Forms.CheckBox MappedCheckBox;
        private System.Windows.Forms.CheckBox ImageCheckBox;
        private System.Windows.Forms.Label RescanTimerLabel;
        private System.Windows.Forms.TextBox RescanIntervalTextBox;
        private System.Windows.Forms.TextBox ResultsReadIntervalTextBox;
        private System.Windows.Forms.TextBox TableReadIntervalTextBox;
        private System.Windows.Forms.TextBox FreezeIntervalTextBox;
        private System.Windows.Forms.Label ResultsReadIntervalLabel;
        private System.Windows.Forms.Label TableReadIntervalLabel;
        private System.Windows.Forms.Label FreezeIntervalLabel;
        private System.Windows.Forms.GroupBox MemoryProtectionGroupBox;
        private System.Windows.Forms.GroupBox IntervalsGroupBox;
        private System.Windows.Forms.GroupBox MemoryTypeGroupBox;
        private System.Windows.Forms.CheckBox NoneCheckBox;
        private System.Windows.Forms.Label InputCorrelatorTimeoutIntervalLabel;
        private System.Windows.Forms.TextBox InputCorrelatorTimeoutIntervalTextBox;
        private System.Windows.Forms.GroupBox GeneralGroupBox;
        private System.Windows.Forms.Label AlignmentLabel;
        private System.Windows.Forms.GroupBox FilteredProtectionFlagsGroupBox;
        private System.Windows.Forms.CheckBox FilteredExecuteCheckBox;
        private System.Windows.Forms.CheckBox FilteredWriteCheckBox;
        private System.Windows.Forms.CheckBox CopyOnWriteCheckBox;
        private HexDecTextBox AlignmentTextBox;
    }
}