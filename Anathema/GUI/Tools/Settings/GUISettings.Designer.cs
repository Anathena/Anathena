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
            this.ExcludedProtectionFlagsGroupBox = new System.Windows.Forms.GroupBox();
            this.ExcludedExecuteCheckBox = new System.Windows.Forms.CheckBox();
            this.ExcludedWriteCheckBox = new System.Windows.Forms.CheckBox();
            this.ExcludedCopyOnWriteCheckBox = new System.Windows.Forms.CheckBox();
            this.GeneralGroupBox = new System.Windows.Forms.GroupBox();
            this.EndAddressTextBox = new Anathema.HexDecTextBox();
            this.StartAddressTextBox = new Anathema.HexDecTextBox();
            this.ScanCustomRangeRadioButton = new System.Windows.Forms.RadioButton();
            this.ScanUserModeRadioButton = new System.Windows.Forms.RadioButton();
            this.AlignmentTextBox = new Anathema.HexDecTextBox();
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
            this.InputCorrelatorTimeOutIntervalTextBox = new Anathema.HexDecTextBox();
            this.ResultsReadIntervalTextBox = new Anathema.HexDecTextBox();
            this.TableReadIntervalTextBox = new Anathema.HexDecTextBox();
            this.RescanIntervalTextBox = new Anathema.HexDecTextBox();
            this.FreezeIntervalTextBox = new Anathema.HexDecTextBox();
            this.InputCorrelatorTimeoutIntervalLabel = new System.Windows.Forms.Label();
            this.ResultsReadIntervalLabel = new System.Windows.Forms.Label();
            this.TableReadIntervalLabel = new System.Windows.Forms.Label();
            this.FreezeIntervalLabel = new System.Windows.Forms.Label();
            this.RescanTimerLabel = new System.Windows.Forms.Label();
            this.SettingsTabControl.SuspendLayout();
            this.ScanTabPage.SuspendLayout();
            this.ExcludedProtectionFlagsGroupBox.SuspendLayout();
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
            this.AcceptButton.Location = new System.Drawing.Point(285, 302);
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
            this.CancelButton.Location = new System.Drawing.Point(366, 302);
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
            this.SettingsTabControl.Size = new System.Drawing.Size(429, 284);
            this.SettingsTabControl.TabIndex = 15;
            // 
            // ScanTabPage
            // 
            this.ScanTabPage.Controls.Add(this.ExcludedProtectionFlagsGroupBox);
            this.ScanTabPage.Controls.Add(this.GeneralGroupBox);
            this.ScanTabPage.Controls.Add(this.MemoryTypeGroupBox);
            this.ScanTabPage.Controls.Add(this.MemoryProtectionGroupBox);
            this.ScanTabPage.Location = new System.Drawing.Point(4, 22);
            this.ScanTabPage.Name = "ScanTabPage";
            this.ScanTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ScanTabPage.Size = new System.Drawing.Size(421, 258);
            this.ScanTabPage.TabIndex = 1;
            this.ScanTabPage.Text = "Scan Settings";
            this.ScanTabPage.UseVisualStyleBackColor = true;
            // 
            // ExcludedProtectionFlagsGroupBox
            // 
            this.ExcludedProtectionFlagsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExcludedProtectionFlagsGroupBox.Controls.Add(this.ExcludedExecuteCheckBox);
            this.ExcludedProtectionFlagsGroupBox.Controls.Add(this.ExcludedWriteCheckBox);
            this.ExcludedProtectionFlagsGroupBox.Controls.Add(this.ExcludedCopyOnWriteCheckBox);
            this.ExcludedProtectionFlagsGroupBox.Location = new System.Drawing.Point(159, 6);
            this.ExcludedProtectionFlagsGroupBox.Name = "ExcludedProtectionFlagsGroupBox";
            this.ExcludedProtectionFlagsGroupBox.Size = new System.Drawing.Size(256, 100);
            this.ExcludedProtectionFlagsGroupBox.TabIndex = 8;
            this.ExcludedProtectionFlagsGroupBox.TabStop = false;
            this.ExcludedProtectionFlagsGroupBox.Text = "Excluded Protection Flags";
            // 
            // ExcludedExecuteCheckBox
            // 
            this.ExcludedExecuteCheckBox.AutoSize = true;
            this.ExcludedExecuteCheckBox.Location = new System.Drawing.Point(63, 28);
            this.ExcludedExecuteCheckBox.Name = "ExcludedExecuteCheckBox";
            this.ExcludedExecuteCheckBox.Size = new System.Drawing.Size(65, 17);
            this.ExcludedExecuteCheckBox.TabIndex = 3;
            this.ExcludedExecuteCheckBox.Text = "Execute";
            this.ExcludedExecuteCheckBox.UseVisualStyleBackColor = true;
            this.ExcludedExecuteCheckBox.CheckedChanged += new System.EventHandler(this.ExcludedExecuteCheckBox_CheckedChanged);
            // 
            // ExcludedWriteCheckBox
            // 
            this.ExcludedWriteCheckBox.AutoSize = true;
            this.ExcludedWriteCheckBox.Location = new System.Drawing.Point(6, 28);
            this.ExcludedWriteCheckBox.Name = "ExcludedWriteCheckBox";
            this.ExcludedWriteCheckBox.Size = new System.Drawing.Size(51, 17);
            this.ExcludedWriteCheckBox.TabIndex = 2;
            this.ExcludedWriteCheckBox.Text = "Write";
            this.ExcludedWriteCheckBox.UseVisualStyleBackColor = true;
            this.ExcludedWriteCheckBox.CheckedChanged += new System.EventHandler(this.ExcludedWriteCheckBox_CheckedChanged);
            // 
            // ExcludedCopyOnWriteCheckBox
            // 
            this.ExcludedCopyOnWriteCheckBox.AutoSize = true;
            this.ExcludedCopyOnWriteCheckBox.Location = new System.Drawing.Point(6, 51);
            this.ExcludedCopyOnWriteCheckBox.Name = "ExcludedCopyOnWriteCheckBox";
            this.ExcludedCopyOnWriteCheckBox.Size = new System.Drawing.Size(93, 17);
            this.ExcludedCopyOnWriteCheckBox.TabIndex = 1;
            this.ExcludedCopyOnWriteCheckBox.Text = "Copy on Write";
            this.ExcludedCopyOnWriteCheckBox.UseVisualStyleBackColor = true;
            this.ExcludedCopyOnWriteCheckBox.CheckedChanged += new System.EventHandler(this.ExcludedCopyOnWriteCheckBox_CheckedChanged);
            // 
            // GeneralGroupBox
            // 
            this.GeneralGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GeneralGroupBox.Controls.Add(this.EndAddressTextBox);
            this.GeneralGroupBox.Controls.Add(this.StartAddressTextBox);
            this.GeneralGroupBox.Controls.Add(this.ScanCustomRangeRadioButton);
            this.GeneralGroupBox.Controls.Add(this.ScanUserModeRadioButton);
            this.GeneralGroupBox.Controls.Add(this.AlignmentTextBox);
            this.GeneralGroupBox.Controls.Add(this.AlignmentLabel);
            this.GeneralGroupBox.Location = new System.Drawing.Point(159, 112);
            this.GeneralGroupBox.Name = "GeneralGroupBox";
            this.GeneralGroupBox.Size = new System.Drawing.Size(256, 140);
            this.GeneralGroupBox.TabIndex = 9;
            this.GeneralGroupBox.TabStop = false;
            this.GeneralGroupBox.Text = "General";
            // 
            // EndAddressTextBox
            // 
            this.EndAddressTextBox.ForeColor = System.Drawing.Color.Red;
            this.EndAddressTextBox.IsHex = true;
            this.EndAddressTextBox.Location = new System.Drawing.Point(128, 85);
            this.EndAddressTextBox.Name = "EndAddressTextBox";
            this.EndAddressTextBox.Size = new System.Drawing.Size(116, 20);
            this.EndAddressTextBox.TabIndex = 6;
            this.EndAddressTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.EndAddressTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndAddressTextBox.WaterMarkText = null;
            // 
            // StartAddressTextBox
            // 
            this.StartAddressTextBox.ForeColor = System.Drawing.Color.Red;
            this.StartAddressTextBox.IsHex = true;
            this.StartAddressTextBox.Location = new System.Drawing.Point(6, 85);
            this.StartAddressTextBox.Name = "StartAddressTextBox";
            this.StartAddressTextBox.Size = new System.Drawing.Size(116, 20);
            this.StartAddressTextBox.TabIndex = 5;
            this.StartAddressTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.StartAddressTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartAddressTextBox.WaterMarkText = null;
            // 
            // ScanCustomRangeRadioButton
            // 
            this.ScanCustomRangeRadioButton.AutoSize = true;
            this.ScanCustomRangeRadioButton.Location = new System.Drawing.Point(6, 62);
            this.ScanCustomRangeRadioButton.Name = "ScanCustomRangeRadioButton";
            this.ScanCustomRangeRadioButton.Size = new System.Drawing.Size(123, 17);
            this.ScanCustomRangeRadioButton.TabIndex = 4;
            this.ScanCustomRangeRadioButton.Text = "Scan Custom Range";
            this.ScanCustomRangeRadioButton.UseVisualStyleBackColor = true;
            this.ScanCustomRangeRadioButton.CheckedChanged += new System.EventHandler(this.ScanCustomRangeRadioButton_CheckedChanged);
            // 
            // ScanUserModeRadioButton
            // 
            this.ScanUserModeRadioButton.AutoSize = true;
            this.ScanUserModeRadioButton.Location = new System.Drawing.Point(6, 39);
            this.ScanUserModeRadioButton.Name = "ScanUserModeRadioButton";
            this.ScanUserModeRadioButton.Size = new System.Drawing.Size(141, 17);
            this.ScanUserModeRadioButton.TabIndex = 3;
            this.ScanUserModeRadioButton.Text = "Scan Usermode Memory";
            this.ScanUserModeRadioButton.UseVisualStyleBackColor = true;
            this.ScanUserModeRadioButton.CheckedChanged += new System.EventHandler(this.ScanUserModeRadioButton_CheckedChanged);
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
            this.MemoryTypeGroupBox.Size = new System.Drawing.Size(147, 140);
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
            this.RequiredExecuteCheckBox.UseVisualStyleBackColor = true;
            this.RequiredExecuteCheckBox.CheckedChanged += new System.EventHandler(this.RequiredExecuteCheckBox_CheckedChanged);
            // 
            // RequiredWriteCheckBox
            // 
            this.RequiredWriteCheckBox.AutoSize = true;
            this.RequiredWriteCheckBox.Location = new System.Drawing.Point(6, 28);
            this.RequiredWriteCheckBox.Name = "RequiredWriteCheckBox";
            this.RequiredWriteCheckBox.Size = new System.Drawing.Size(51, 17);
            this.RequiredWriteCheckBox.TabIndex = 2;
            this.RequiredWriteCheckBox.Text = "Write";
            this.RequiredWriteCheckBox.UseVisualStyleBackColor = true;
            this.RequiredWriteCheckBox.CheckedChanged += new System.EventHandler(this.RequiredWriteCheckBox_CheckedChanged);
            // 
            // RequiredCopyOnWriteCheckBox
            // 
            this.RequiredCopyOnWriteCheckBox.AutoSize = true;
            this.RequiredCopyOnWriteCheckBox.Location = new System.Drawing.Point(6, 51);
            this.RequiredCopyOnWriteCheckBox.Name = "RequiredCopyOnWriteCheckBox";
            this.RequiredCopyOnWriteCheckBox.Size = new System.Drawing.Size(93, 17);
            this.RequiredCopyOnWriteCheckBox.TabIndex = 1;
            this.RequiredCopyOnWriteCheckBox.Text = "Copy on Write";
            this.RequiredCopyOnWriteCheckBox.UseVisualStyleBackColor = true;
            this.RequiredCopyOnWriteCheckBox.CheckedChanged += new System.EventHandler(this.RequiredCopyOnWriteCheckBox_CheckedChanged);
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.Controls.Add(this.IntervalsGroupBox);
            this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTabPage.Size = new System.Drawing.Size(421, 258);
            this.GeneralTabPage.TabIndex = 0;
            this.GeneralTabPage.Text = "General";
            this.GeneralTabPage.UseVisualStyleBackColor = true;
            // 
            // IntervalsGroupBox
            // 
            this.IntervalsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IntervalsGroupBox.Controls.Add(this.InputCorrelatorTimeOutIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.ResultsReadIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.TableReadIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.RescanIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.FreezeIntervalTextBox);
            this.IntervalsGroupBox.Controls.Add(this.InputCorrelatorTimeoutIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.ResultsReadIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.TableReadIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.FreezeIntervalLabel);
            this.IntervalsGroupBox.Controls.Add(this.RescanTimerLabel);
            this.IntervalsGroupBox.Location = new System.Drawing.Point(6, 6);
            this.IntervalsGroupBox.Name = "IntervalsGroupBox";
            this.IntervalsGroupBox.Size = new System.Drawing.Size(409, 164);
            this.IntervalsGroupBox.TabIndex = 8;
            this.IntervalsGroupBox.TabStop = false;
            this.IntervalsGroupBox.Text = "Intervals (ms)";
            // 
            // InputCorrelatorTimeOutIntervalTextBox
            // 
            this.InputCorrelatorTimeOutIntervalTextBox.ForeColor = System.Drawing.Color.Red;
            this.InputCorrelatorTimeOutIntervalTextBox.IsHex = false;
            this.InputCorrelatorTimeOutIntervalTextBox.Location = new System.Drawing.Point(6, 123);
            this.InputCorrelatorTimeOutIntervalTextBox.Name = "InputCorrelatorTimeOutIntervalTextBox";
            this.InputCorrelatorTimeOutIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.InputCorrelatorTimeOutIntervalTextBox.TabIndex = 13;
            this.InputCorrelatorTimeOutIntervalTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.InputCorrelatorTimeOutIntervalTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputCorrelatorTimeOutIntervalTextBox.WaterMarkText = null;
            // 
            // ResultsReadIntervalTextBox
            // 
            this.ResultsReadIntervalTextBox.ForeColor = System.Drawing.Color.Red;
            this.ResultsReadIntervalTextBox.IsHex = false;
            this.ResultsReadIntervalTextBox.Location = new System.Drawing.Point(6, 97);
            this.ResultsReadIntervalTextBox.Name = "ResultsReadIntervalTextBox";
            this.ResultsReadIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.ResultsReadIntervalTextBox.TabIndex = 12;
            this.ResultsReadIntervalTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.ResultsReadIntervalTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultsReadIntervalTextBox.WaterMarkText = null;
            // 
            // TableReadIntervalTextBox
            // 
            this.TableReadIntervalTextBox.ForeColor = System.Drawing.Color.Red;
            this.TableReadIntervalTextBox.IsHex = false;
            this.TableReadIntervalTextBox.Location = new System.Drawing.Point(6, 71);
            this.TableReadIntervalTextBox.Name = "TableReadIntervalTextBox";
            this.TableReadIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.TableReadIntervalTextBox.TabIndex = 11;
            this.TableReadIntervalTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.TableReadIntervalTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TableReadIntervalTextBox.WaterMarkText = null;
            // 
            // RescanIntervalTextBox
            // 
            this.RescanIntervalTextBox.ForeColor = System.Drawing.Color.Red;
            this.RescanIntervalTextBox.IsHex = false;
            this.RescanIntervalTextBox.Location = new System.Drawing.Point(6, 45);
            this.RescanIntervalTextBox.Name = "RescanIntervalTextBox";
            this.RescanIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.RescanIntervalTextBox.TabIndex = 10;
            this.RescanIntervalTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.RescanIntervalTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RescanIntervalTextBox.WaterMarkText = null;
            // 
            // FreezeIntervalTextBox
            // 
            this.FreezeIntervalTextBox.ForeColor = System.Drawing.Color.Red;
            this.FreezeIntervalTextBox.IsHex = false;
            this.FreezeIntervalTextBox.Location = new System.Drawing.Point(6, 19);
            this.FreezeIntervalTextBox.Name = "FreezeIntervalTextBox";
            this.FreezeIntervalTextBox.Size = new System.Drawing.Size(68, 20);
            this.FreezeIntervalTextBox.TabIndex = 9;
            this.FreezeIntervalTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.FreezeIntervalTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FreezeIntervalTextBox.WaterMarkText = null;
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
            // ResultsReadIntervalLabel
            // 
            this.ResultsReadIntervalLabel.AutoSize = true;
            this.ResultsReadIntervalLabel.Location = new System.Drawing.Point(80, 100);
            this.ResultsReadIntervalLabel.Name = "ResultsReadIntervalLabel";
            this.ResultsReadIntervalLabel.Size = new System.Drawing.Size(109, 13);
            this.ResultsReadIntervalLabel.TabIndex = 7;
            this.ResultsReadIntervalLabel.Text = "Results Read Interval";
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
            // FreezeIntervalLabel
            // 
            this.FreezeIntervalLabel.AutoSize = true;
            this.FreezeIntervalLabel.Location = new System.Drawing.Point(80, 22);
            this.FreezeIntervalLabel.Name = "FreezeIntervalLabel";
            this.FreezeIntervalLabel.Size = new System.Drawing.Size(77, 13);
            this.FreezeIntervalLabel.TabIndex = 5;
            this.FreezeIntervalLabel.Text = "Freeze Interval";
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
            // GUISettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 329);
            this.Controls.Add(this.SettingsTabControl);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AcceptButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUISettings";
            this.Text = "Settings";
            this.SettingsTabControl.ResumeLayout(false);
            this.ScanTabPage.ResumeLayout(false);
            this.ExcludedProtectionFlagsGroupBox.ResumeLayout(false);
            this.ExcludedProtectionFlagsGroupBox.PerformLayout();
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
        private System.Windows.Forms.Label ResultsReadIntervalLabel;
        private System.Windows.Forms.Label TableReadIntervalLabel;
        private System.Windows.Forms.Label FreezeIntervalLabel;
        private System.Windows.Forms.GroupBox MemoryProtectionGroupBox;
        private System.Windows.Forms.GroupBox IntervalsGroupBox;
        private System.Windows.Forms.GroupBox MemoryTypeGroupBox;
        private System.Windows.Forms.CheckBox NoneCheckBox;
        private System.Windows.Forms.Label InputCorrelatorTimeoutIntervalLabel;
        private System.Windows.Forms.GroupBox GeneralGroupBox;
        private System.Windows.Forms.Label AlignmentLabel;
        private System.Windows.Forms.GroupBox ExcludedProtectionFlagsGroupBox;
        private System.Windows.Forms.CheckBox ExcludedExecuteCheckBox;
        private System.Windows.Forms.CheckBox ExcludedWriteCheckBox;
        private System.Windows.Forms.CheckBox ExcludedCopyOnWriteCheckBox;
        private HexDecTextBox AlignmentTextBox;
        private HexDecTextBox EndAddressTextBox;
        private HexDecTextBox StartAddressTextBox;
        private System.Windows.Forms.RadioButton ScanCustomRangeRadioButton;
        private System.Windows.Forms.RadioButton ScanUserModeRadioButton;
        private HexDecTextBox RescanIntervalTextBox;
        private HexDecTextBox FreezeIntervalTextBox;
        private HexDecTextBox InputCorrelatorTimeOutIntervalTextBox;
        private HexDecTextBox ResultsReadIntervalTextBox;
        private HexDecTextBox TableReadIntervalTextBox;
    }
}