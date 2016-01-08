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
            this.AcceptButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SettingsTabControl = new System.Windows.Forms.TabControl();
            this.GeneralTabPage = new System.Windows.Forms.TabPage();
            this.IntervalsGroupBox = new System.Windows.Forms.GroupBox();
            this.RepeatScanIntervalTextBox = new System.Windows.Forms.TextBox();
            this.ResultsReadIntervalLabel = new System.Windows.Forms.Label();
            this.FreezeIntervalTextBox = new System.Windows.Forms.TextBox();
            this.TableReadIntervalLabel = new System.Windows.Forms.Label();
            this.TableIntervalTimerTextBox = new System.Windows.Forms.TextBox();
            this.FreezeIntervalLabel = new System.Windows.Forms.Label();
            this.ResultsReadIntervalTextBox = new System.Windows.Forms.TextBox();
            this.RepeatScanTimerLabel = new System.Windows.Forms.Label();
            this.ScanTabPage = new System.Windows.Forms.TabPage();
            this.MemoryProtectionGroupBox = new System.Windows.Forms.GroupBox();
            this.PrivateCheckBox = new System.Windows.Forms.CheckBox();
            this.ReadWriteCheckBox = new System.Windows.Forms.CheckBox();
            this.MappedCheckBox = new System.Windows.Forms.CheckBox();
            this.ReadOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.ImageCheckBox = new System.Windows.Forms.CheckBox();
            this.NoAccessCheckBox = new System.Windows.Forms.CheckBox();
            this.CommitCheckBox = new System.Windows.Forms.CheckBox();
            this.FreeCheckBox = new System.Windows.Forms.CheckBox();
            this.ReserveCheckBox = new System.Windows.Forms.CheckBox();
            this.ExecuteReadCheckBox = new System.Windows.Forms.CheckBox();
            this.ExecuteCheckBox = new System.Windows.Forms.CheckBox();
            this.WriteCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.GuardCheckBox = new System.Windows.Forms.CheckBox();
            this.ExecuteWriteCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.ExecuteReadWriteCheckBox = new System.Windows.Forms.CheckBox();
            this.WriteCombineCheckBox = new System.Windows.Forms.CheckBox();
            this.NoCacheCheckBox = new System.Windows.Forms.CheckBox();
            this.MemoryTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.MemoryStateGroupBox = new System.Windows.Forms.GroupBox();
            this.SettingsTabControl.SuspendLayout();
            this.GeneralTabPage.SuspendLayout();
            this.IntervalsGroupBox.SuspendLayout();
            this.ScanTabPage.SuspendLayout();
            this.MemoryProtectionGroupBox.SuspendLayout();
            this.MemoryTypeGroupBox.SuspendLayout();
            this.MemoryStateGroupBox.SuspendLayout();
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
            this.SettingsTabControl.Controls.Add(this.GeneralTabPage);
            this.SettingsTabControl.Controls.Add(this.ScanTabPage);
            this.SettingsTabControl.Location = new System.Drawing.Point(12, 12);
            this.SettingsTabControl.Name = "SettingsTabControl";
            this.SettingsTabControl.SelectedIndex = 0;
            this.SettingsTabControl.Size = new System.Drawing.Size(429, 276);
            this.SettingsTabControl.TabIndex = 15;
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.Controls.Add(this.IntervalsGroupBox);
            this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTabPage.Size = new System.Drawing.Size(321, 250);
            this.GeneralTabPage.TabIndex = 0;
            this.GeneralTabPage.Text = "General";
            this.GeneralTabPage.UseVisualStyleBackColor = true;
            // 
            // IntervalsGroupBox
            // 
            this.IntervalsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IntervalsGroupBox.Controls.Add(this.RepeatScanIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.ResultsReadIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.FreezeIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.TableReadIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.TableIntervalTimerTextBox);
            this.IntervalsGroupBox.Controls.Add(this.FreezeIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.ResultsReadIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.RepeatScanTimerLabel);
            this.IntervalsGroupBox.Location = new System.Drawing.Point(6, 6);
            this.IntervalsGroupBox.Name = "IntervalsGroupBox";
            this.IntervalsGroupBox.Size = new System.Drawing.Size(309, 130);
            this.IntervalsGroupBox.TabIndex = 8;
            this.IntervalsGroupBox.TabStop = false;
            this.IntervalsGroupBox.Text = "Intervals";
            // 
            // RepeatScanIntervalTextBox
            // 
            this.RepeatScanIntervalTextBox.Location = new System.Drawing.Point(6, 19);
            this.RepeatScanIntervalTextBox.Name = "RepeatScanIntervalTextBox";
            this.RepeatScanIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.RepeatScanIntervalTextBox.TabIndex = 3;
            // 
            // ResultsReadIntervalLabel
            // 
            this.ResultsReadIntervalLabel.AutoSize = true;
            this.ResultsReadIntervalLabel.Location = new System.Drawing.Point(80, 100);
            this.ResultsReadIntervalLabel.Name = "ResultsReadIntervalLabel";
            this.ResultsReadIntervalLabel.Size = new System.Drawing.Size(131, 13);
            this.ResultsReadIntervalLabel.TabIndex = 7;
            this.ResultsReadIntervalLabel.Text = "Results Read Interval (ms)";
            // 
            // FreezeIntervalTextBox
            // 
            this.FreezeIntervalTextBox.Location = new System.Drawing.Point(6, 45);
            this.FreezeIntervalTextBox.Name = "FreezeIntervalTextBox";
            this.FreezeIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.FreezeIntervalTextBox.TabIndex = 0;
            // 
            // TableReadIntervalLabel
            // 
            this.TableReadIntervalLabel.AutoSize = true;
            this.TableReadIntervalLabel.Location = new System.Drawing.Point(80, 74);
            this.TableReadIntervalLabel.Name = "TableReadIntervalLabel";
            this.TableReadIntervalLabel.Size = new System.Drawing.Size(123, 13);
            this.TableReadIntervalLabel.TabIndex = 6;
            this.TableReadIntervalLabel.Text = "Table Read Interval (ms)";
            // 
            // TableIntervalTimerTextBox
            // 
            this.TableIntervalTimerTextBox.Location = new System.Drawing.Point(6, 71);
            this.TableIntervalTimerTextBox.Name = "TableIntervalTimerTextBox";
            this.TableIntervalTimerTextBox.Size = new System.Drawing.Size(68, 20);
            this.TableIntervalTimerTextBox.TabIndex = 1;
            // 
            // FreezeIntervalLabel
            // 
            this.FreezeIntervalLabel.AutoSize = true;
            this.FreezeIntervalLabel.Location = new System.Drawing.Point(80, 48);
            this.FreezeIntervalLabel.Name = "FreezeIntervalLabel";
            this.FreezeIntervalLabel.Size = new System.Drawing.Size(99, 13);
            this.FreezeIntervalLabel.TabIndex = 5;
            this.FreezeIntervalLabel.Text = "Freeze Interval (ms)";
            // 
            // ResultsReadIntervalTextBox
            // 
            this.ResultsReadIntervalTextBox.Location = new System.Drawing.Point(6, 97);
            this.ResultsReadIntervalTextBox.Name = "ResultsReadIntervalTextBox";
            this.ResultsReadIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.ResultsReadIntervalTextBox.TabIndex = 2;
            // 
            // RepeatScanTimerLabel
            // 
            this.RepeatScanTimerLabel.AutoSize = true;
            this.RepeatScanTimerLabel.Location = new System.Drawing.Point(80, 22);
            this.RepeatScanTimerLabel.Name = "RepeatScanTimerLabel";
            this.RepeatScanTimerLabel.Size = new System.Drawing.Size(130, 13);
            this.RepeatScanTimerLabel.TabIndex = 4;
            this.RepeatScanTimerLabel.Text = "Repeat Scan Interval (ms)";
            // 
            // ScanTabPage
            // 
            this.ScanTabPage.Controls.Add(this.MemoryStateGroupBox);
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
            // MemoryProtectionGroupBox
            // 
            this.MemoryProtectionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoryProtectionGroupBox.Controls.Add(this.WriteCombineCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.NoCacheCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.GuardCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.ExecuteWriteCopyCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.ExecuteReadWriteCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.ExecuteReadCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.ExecuteCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.WriteCopyCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.ReadWriteCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.ReadOnlyCheckBox);
            this.MemoryProtectionGroupBox.Controls.Add(this.NoAccessCheckBox);
            this.MemoryProtectionGroupBox.Location = new System.Drawing.Point(6, 6);
            this.MemoryProtectionGroupBox.Name = "MemoryProtectionGroupBox";
            this.MemoryProtectionGroupBox.Size = new System.Drawing.Size(409, 100);
            this.MemoryProtectionGroupBox.TabIndex = 7;
            this.MemoryProtectionGroupBox.TabStop = false;
            this.MemoryProtectionGroupBox.Text = "Protection Flags";
            // 
            // PrivateCheckBox
            // 
            this.PrivateCheckBox.AutoSize = true;
            this.PrivateCheckBox.Location = new System.Drawing.Point(6, 19);
            this.PrivateCheckBox.Name = "PrivateCheckBox";
            this.PrivateCheckBox.Size = new System.Drawing.Size(59, 17);
            this.PrivateCheckBox.TabIndex = 4;
            this.PrivateCheckBox.Text = "Private";
            this.PrivateCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReadWriteCheckBox
            // 
            this.ReadWriteCheckBox.AutoSize = true;
            this.ReadWriteCheckBox.Location = new System.Drawing.Point(6, 74);
            this.ReadWriteCheckBox.Name = "ReadWriteCheckBox";
            this.ReadWriteCheckBox.Size = new System.Drawing.Size(77, 17);
            this.ReadWriteCheckBox.TabIndex = 3;
            this.ReadWriteCheckBox.Text = "ReadWrite";
            this.ReadWriteCheckBox.ThreeState = true;
            this.ReadWriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // MappedCheckBox
            // 
            this.MappedCheckBox.AutoSize = true;
            this.MappedCheckBox.Location = new System.Drawing.Point(6, 65);
            this.MappedCheckBox.Name = "MappedCheckBox";
            this.MappedCheckBox.Size = new System.Drawing.Size(65, 17);
            this.MappedCheckBox.TabIndex = 6;
            this.MappedCheckBox.Text = "Mapped";
            this.MappedCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReadOnlyCheckBox
            // 
            this.ReadOnlyCheckBox.AutoSize = true;
            this.ReadOnlyCheckBox.Location = new System.Drawing.Point(6, 51);
            this.ReadOnlyCheckBox.Name = "ReadOnlyCheckBox";
            this.ReadOnlyCheckBox.Size = new System.Drawing.Size(73, 17);
            this.ReadOnlyCheckBox.TabIndex = 2;
            this.ReadOnlyCheckBox.Text = "ReadOnly";
            this.ReadOnlyCheckBox.ThreeState = true;
            this.ReadOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImageCheckBox
            // 
            this.ImageCheckBox.AutoSize = true;
            this.ImageCheckBox.Location = new System.Drawing.Point(6, 42);
            this.ImageCheckBox.Name = "ImageCheckBox";
            this.ImageCheckBox.Size = new System.Drawing.Size(55, 17);
            this.ImageCheckBox.TabIndex = 5;
            this.ImageCheckBox.Text = "Image";
            this.ImageCheckBox.UseVisualStyleBackColor = true;
            // 
            // NoAccessCheckBox
            // 
            this.NoAccessCheckBox.AutoSize = true;
            this.NoAccessCheckBox.Location = new System.Drawing.Point(6, 28);
            this.NoAccessCheckBox.Name = "NoAccessCheckBox";
            this.NoAccessCheckBox.Size = new System.Drawing.Size(75, 17);
            this.NoAccessCheckBox.TabIndex = 1;
            this.NoAccessCheckBox.Text = "NoAccess";
            this.NoAccessCheckBox.ThreeState = true;
            this.NoAccessCheckBox.UseVisualStyleBackColor = true;
            // 
            // CommitCheckBox
            // 
            this.CommitCheckBox.AutoSize = true;
            this.CommitCheckBox.Location = new System.Drawing.Point(6, 19);
            this.CommitCheckBox.Name = "CommitCheckBox";
            this.CommitCheckBox.Size = new System.Drawing.Size(60, 17);
            this.CommitCheckBox.TabIndex = 7;
            this.CommitCheckBox.Text = "Commit";
            this.CommitCheckBox.UseVisualStyleBackColor = true;
            // 
            // FreeCheckBox
            // 
            this.FreeCheckBox.AutoSize = true;
            this.FreeCheckBox.Location = new System.Drawing.Point(6, 65);
            this.FreeCheckBox.Name = "FreeCheckBox";
            this.FreeCheckBox.Size = new System.Drawing.Size(47, 17);
            this.FreeCheckBox.TabIndex = 9;
            this.FreeCheckBox.Text = "Free";
            this.FreeCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReserveCheckBox
            // 
            this.ReserveCheckBox.AutoSize = true;
            this.ReserveCheckBox.Location = new System.Drawing.Point(6, 42);
            this.ReserveCheckBox.Name = "ReserveCheckBox";
            this.ReserveCheckBox.Size = new System.Drawing.Size(66, 17);
            this.ReserveCheckBox.TabIndex = 8;
            this.ReserveCheckBox.Text = "Reserve";
            this.ReserveCheckBox.UseVisualStyleBackColor = true;
            // 
            // ExecuteReadCheckBox
            // 
            this.ExecuteReadCheckBox.AutoSize = true;
            this.ExecuteReadCheckBox.Location = new System.Drawing.Point(87, 74);
            this.ExecuteReadCheckBox.Name = "ExecuteReadCheckBox";
            this.ExecuteReadCheckBox.Size = new System.Drawing.Size(91, 17);
            this.ExecuteReadCheckBox.TabIndex = 12;
            this.ExecuteReadCheckBox.Text = "ExecuteRead";
            this.ExecuteReadCheckBox.ThreeState = true;
            this.ExecuteReadCheckBox.UseVisualStyleBackColor = true;
            // 
            // ExecuteCheckBox
            // 
            this.ExecuteCheckBox.AutoSize = true;
            this.ExecuteCheckBox.Location = new System.Drawing.Point(87, 51);
            this.ExecuteCheckBox.Name = "ExecuteCheckBox";
            this.ExecuteCheckBox.Size = new System.Drawing.Size(65, 17);
            this.ExecuteCheckBox.TabIndex = 11;
            this.ExecuteCheckBox.Text = "Execute";
            this.ExecuteCheckBox.ThreeState = true;
            this.ExecuteCheckBox.UseVisualStyleBackColor = true;
            // 
            // WriteCopyCheckBox
            // 
            this.WriteCopyCheckBox.AutoSize = true;
            this.WriteCopyCheckBox.Location = new System.Drawing.Point(87, 28);
            this.WriteCopyCheckBox.Name = "WriteCopyCheckBox";
            this.WriteCopyCheckBox.Size = new System.Drawing.Size(75, 17);
            this.WriteCopyCheckBox.TabIndex = 10;
            this.WriteCopyCheckBox.Text = "WriteCopy";
            this.WriteCopyCheckBox.ThreeState = true;
            this.WriteCopyCheckBox.UseVisualStyleBackColor = true;
            // 
            // GuardCheckBox
            // 
            this.GuardCheckBox.AutoSize = true;
            this.GuardCheckBox.Location = new System.Drawing.Point(184, 74);
            this.GuardCheckBox.Name = "GuardCheckBox";
            this.GuardCheckBox.Size = new System.Drawing.Size(55, 17);
            this.GuardCheckBox.TabIndex = 15;
            this.GuardCheckBox.Text = "Guard";
            this.GuardCheckBox.ThreeState = true;
            this.GuardCheckBox.UseVisualStyleBackColor = true;
            // 
            // ExecuteWriteCopyCheckBox
            // 
            this.ExecuteWriteCopyCheckBox.AutoSize = true;
            this.ExecuteWriteCopyCheckBox.Location = new System.Drawing.Point(184, 51);
            this.ExecuteWriteCopyCheckBox.Name = "ExecuteWriteCopyCheckBox";
            this.ExecuteWriteCopyCheckBox.Size = new System.Drawing.Size(114, 17);
            this.ExecuteWriteCopyCheckBox.TabIndex = 14;
            this.ExecuteWriteCopyCheckBox.Text = "ExecuteWriteCopy";
            this.ExecuteWriteCopyCheckBox.ThreeState = true;
            this.ExecuteWriteCopyCheckBox.UseVisualStyleBackColor = true;
            // 
            // ExecuteReadWriteCheckBox
            // 
            this.ExecuteReadWriteCheckBox.AutoSize = true;
            this.ExecuteReadWriteCheckBox.Location = new System.Drawing.Point(184, 28);
            this.ExecuteReadWriteCheckBox.Name = "ExecuteReadWriteCheckBox";
            this.ExecuteReadWriteCheckBox.Size = new System.Drawing.Size(116, 17);
            this.ExecuteReadWriteCheckBox.TabIndex = 13;
            this.ExecuteReadWriteCheckBox.Text = "ExecuteReadWrite";
            this.ExecuteReadWriteCheckBox.ThreeState = true;
            this.ExecuteReadWriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // WriteCombineCheckBox
            // 
            this.WriteCombineCheckBox.AutoSize = true;
            this.WriteCombineCheckBox.Location = new System.Drawing.Point(306, 51);
            this.WriteCombineCheckBox.Name = "WriteCombineCheckBox";
            this.WriteCombineCheckBox.Size = new System.Drawing.Size(92, 17);
            this.WriteCombineCheckBox.TabIndex = 17;
            this.WriteCombineCheckBox.Text = "WriteCombine";
            this.WriteCombineCheckBox.ThreeState = true;
            this.WriteCombineCheckBox.UseVisualStyleBackColor = true;
            // 
            // NoCacheCheckBox
            // 
            this.NoCacheCheckBox.AutoSize = true;
            this.NoCacheCheckBox.Location = new System.Drawing.Point(306, 28);
            this.NoCacheCheckBox.Name = "NoCacheCheckBox";
            this.NoCacheCheckBox.Size = new System.Drawing.Size(71, 17);
            this.NoCacheCheckBox.TabIndex = 16;
            this.NoCacheCheckBox.Text = "NoCache";
            this.NoCacheCheckBox.ThreeState = true;
            this.NoCacheCheckBox.UseVisualStyleBackColor = true;
            // 
            // MemoryTypeGroupBox
            // 
            this.MemoryTypeGroupBox.Controls.Add(this.PrivateCheckBox);
            this.MemoryTypeGroupBox.Controls.Add(this.ImageCheckBox);
            this.MemoryTypeGroupBox.Controls.Add(this.MappedCheckBox);
            this.MemoryTypeGroupBox.Location = new System.Drawing.Point(6, 112);
            this.MemoryTypeGroupBox.Name = "MemoryTypeGroupBox";
            this.MemoryTypeGroupBox.Size = new System.Drawing.Size(200, 100);
            this.MemoryTypeGroupBox.TabIndex = 8;
            this.MemoryTypeGroupBox.TabStop = false;
            this.MemoryTypeGroupBox.Text = "Memory Type";
            // 
            // MemoryStateGroupBox
            // 
            this.MemoryStateGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoryStateGroupBox.Controls.Add(this.CommitCheckBox);
            this.MemoryStateGroupBox.Controls.Add(this.ReserveCheckBox);
            this.MemoryStateGroupBox.Controls.Add(this.FreeCheckBox);
            this.MemoryStateGroupBox.Location = new System.Drawing.Point(212, 112);
            this.MemoryStateGroupBox.Name = "MemoryStateGroupBox";
            this.MemoryStateGroupBox.Size = new System.Drawing.Size(203, 100);
            this.MemoryStateGroupBox.TabIndex = 9;
            this.MemoryStateGroupBox.TabStop = false;
            this.MemoryStateGroupBox.Text = "Memory State";
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
            this.Name = "GUISettings";
            this.Text = "Settings";
            this.SettingsTabControl.ResumeLayout(false);
            this.GeneralTabPage.ResumeLayout(false);
            this.IntervalsGroupBox.ResumeLayout(false);
            this.IntervalsGroupBox.PerformLayout();
            this.ScanTabPage.ResumeLayout(false);
            this.MemoryProtectionGroupBox.ResumeLayout(false);
            this.MemoryProtectionGroupBox.PerformLayout();
            this.MemoryTypeGroupBox.ResumeLayout(false);
            this.MemoryTypeGroupBox.PerformLayout();
            this.MemoryStateGroupBox.ResumeLayout(false);
            this.MemoryStateGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TabControl SettingsTabControl;
        private System.Windows.Forms.TabPage GeneralTabPage;
        private System.Windows.Forms.TabPage ScanTabPage;
        private System.Windows.Forms.CheckBox ReadWriteCheckBox;
        private System.Windows.Forms.CheckBox ReadOnlyCheckBox;
        private System.Windows.Forms.CheckBox NoAccessCheckBox;
        private System.Windows.Forms.CheckBox PrivateCheckBox;
        private System.Windows.Forms.CheckBox MappedCheckBox;
        private System.Windows.Forms.CheckBox ImageCheckBox;
        private System.Windows.Forms.Label RepeatScanTimerLabel;
        private System.Windows.Forms.TextBox RepeatScanIntervalTextBox;
        private System.Windows.Forms.TextBox ResultsReadIntervalTextBox;
        private System.Windows.Forms.TextBox TableIntervalTimerTextBox;
        private System.Windows.Forms.TextBox FreezeIntervalTextBox;
        private System.Windows.Forms.Label ResultsReadIntervalLabel;
        private System.Windows.Forms.Label TableReadIntervalLabel;
        private System.Windows.Forms.Label FreezeIntervalLabel;
        private System.Windows.Forms.GroupBox MemoryProtectionGroupBox;
        private System.Windows.Forms.GroupBox IntervalsGroupBox;
        private System.Windows.Forms.CheckBox CommitCheckBox;
        private System.Windows.Forms.CheckBox FreeCheckBox;
        private System.Windows.Forms.CheckBox ReserveCheckBox;
        private System.Windows.Forms.CheckBox WriteCombineCheckBox;
        private System.Windows.Forms.CheckBox NoCacheCheckBox;
        private System.Windows.Forms.CheckBox GuardCheckBox;
        private System.Windows.Forms.CheckBox ExecuteWriteCopyCheckBox;
        private System.Windows.Forms.CheckBox ExecuteReadWriteCheckBox;
        private System.Windows.Forms.CheckBox ExecuteReadCheckBox;
        private System.Windows.Forms.CheckBox ExecuteCheckBox;
        private System.Windows.Forms.CheckBox WriteCopyCheckBox;
        private System.Windows.Forms.GroupBox MemoryStateGroupBox;
        private System.Windows.Forms.GroupBox MemoryTypeGroupBox;
    }
}