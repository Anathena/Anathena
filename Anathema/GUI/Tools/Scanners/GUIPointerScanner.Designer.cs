namespace Anathema
{
    partial class GUIPointerScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIPointerScanner));
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.RebuildPointersButton = new System.Windows.Forms.ToolStripButton();
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddSelectedResultsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TargetAddressLabel = new System.Windows.Forms.Label();
            this.MaxOffsetLabel = new System.Windows.Forms.Label();
            this.MaxLevelLabel = new System.Windows.Forms.Label();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.PointerScanTabControl = new System.Windows.Forms.TabControl();
            this.SettingsTabPage = new System.Windows.Forms.TabPage();
            this.RescanGroupBox = new System.Windows.Forms.GroupBox();
            this.ValueTextBox = new Anathema.HexDecTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.ClearConstraintsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ScanOptionsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ChangedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnchangedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IncreasedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecreasedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EqualToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NotEqualToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IncreasedByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecreasedByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GreaterThanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GreaterThanOrEqualToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LessThanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LessThanOrEqualToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IgnoreAddressCheckBox = new System.Windows.Forms.CheckBox();
            this.ConstraintsListView = new System.Windows.Forms.ListView();
            this.ConstraintHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaxLevelTextBox = new Anathema.HexDecTextBox();
            this.MaxOffsetTextBox = new Anathema.HexDecTextBox();
            this.ValueTypeLabel = new System.Windows.Forms.Label();
            this.TargetAddressTextBox = new Anathema.HexDecTextBox();
            this.ResultsTabPage = new System.Windows.Forms.TabPage();
            this.PointerListView = new Anathema.FlickerFreeListView();
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BaseHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NotScientificNotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScanToolStrip.SuspendLayout();
            this.PointerScanTabControl.SuspendLayout();
            this.SettingsTabPage.SuspendLayout();
            this.RescanGroupBox.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.ResultsTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartScanButton,
            this.RebuildPointersButton,
            this.StopScanButton,
            this.toolStripSeparator1,
            this.AddSelectedResultsButton,
            this.toolStripSeparator2});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(417, 25);
            this.ScanToolStrip.TabIndex = 149;
            // 
            // StartScanButton
            // 
            this.StartScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartScanButton.Image = global::Anathema.Properties.Resources.RightArrow;
            this.StartScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartScanButton.Name = "StartScanButton";
            this.StartScanButton.Size = new System.Drawing.Size(23, 22);
            this.StartScanButton.Text = "Build Pointers";
            this.StartScanButton.Click += new System.EventHandler(this.StartScanButton_Click);
            // 
            // RebuildPointersButton
            // 
            this.RebuildPointersButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RebuildPointersButton.Image = global::Anathema.Properties.Resources.NextScan1;
            this.RebuildPointersButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RebuildPointersButton.Name = "RebuildPointersButton";
            this.RebuildPointersButton.Size = new System.Drawing.Size(23, 22);
            this.RebuildPointersButton.Text = "Rebuild Pointers";
            this.RebuildPointersButton.Click += new System.EventHandler(this.RebuildPointersButton_Click);
            // 
            // StopScanButton
            // 
            this.StopScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopScanButton.Image = global::Anathema.Properties.Resources.Stop;
            this.StopScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopScanButton.Name = "StopScanButton";
            this.StopScanButton.Size = new System.Drawing.Size(23, 22);
            this.StopScanButton.Text = "Stop Scan";
            this.StopScanButton.ToolTipText = "Stop Scan";
            this.StopScanButton.Click += new System.EventHandler(this.StopScanButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // AddSelectedResultsButton
            // 
            this.AddSelectedResultsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddSelectedResultsButton.Image = global::Anathema.Properties.Resources.DownArrows;
            this.AddSelectedResultsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddSelectedResultsButton.Name = "AddSelectedResultsButton";
            this.AddSelectedResultsButton.Size = new System.Drawing.Size(23, 22);
            this.AddSelectedResultsButton.Text = "Add Selected to Table";
            this.AddSelectedResultsButton.Click += new System.EventHandler(this.AddSelectedResultsButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // TargetAddressLabel
            // 
            this.TargetAddressLabel.AutoSize = true;
            this.TargetAddressLabel.Location = new System.Drawing.Point(8, 9);
            this.TargetAddressLabel.Name = "TargetAddressLabel";
            this.TargetAddressLabel.Size = new System.Drawing.Size(45, 13);
            this.TargetAddressLabel.TabIndex = 151;
            this.TargetAddressLabel.Text = "Address";
            // 
            // MaxOffsetLabel
            // 
            this.MaxOffsetLabel.AutoSize = true;
            this.MaxOffsetLabel.Location = new System.Drawing.Point(215, 35);
            this.MaxOffsetLabel.Name = "MaxOffsetLabel";
            this.MaxOffsetLabel.Size = new System.Drawing.Size(58, 13);
            this.MaxOffsetLabel.TabIndex = 153;
            this.MaxOffsetLabel.Text = "Max Offset";
            // 
            // MaxLevelLabel
            // 
            this.MaxLevelLabel.AutoSize = true;
            this.MaxLevelLabel.Location = new System.Drawing.Point(215, 9);
            this.MaxLevelLabel.Name = "MaxLevelLabel";
            this.MaxLevelLabel.Size = new System.Drawing.Size(56, 13);
            this.MaxLevelLabel.TabIndex = 155;
            this.MaxLevelLabel.Text = "Max Level";
            // 
            // ValueTypeComboBox
            // 
            this.ValueTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueTypeComboBox.FormattingEnabled = true;
            this.ValueTypeComboBox.Location = new System.Drawing.Point(75, 32);
            this.ValueTypeComboBox.Name = "ValueTypeComboBox";
            this.ValueTypeComboBox.Size = new System.Drawing.Size(134, 21);
            this.ValueTypeComboBox.TabIndex = 158;
            this.ValueTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ValueTypeComboBox_SelectedIndexChanged);
            // 
            // PointerScanTabControl
            // 
            this.PointerScanTabControl.Controls.Add(this.SettingsTabPage);
            this.PointerScanTabControl.Controls.Add(this.ResultsTabPage);
            this.PointerScanTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PointerScanTabControl.Location = new System.Drawing.Point(0, 25);
            this.PointerScanTabControl.Name = "PointerScanTabControl";
            this.PointerScanTabControl.SelectedIndex = 0;
            this.PointerScanTabControl.Size = new System.Drawing.Size(417, 234);
            this.PointerScanTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.PointerScanTabControl.TabIndex = 159;
            // 
            // SettingsTabPage
            // 
            this.SettingsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.SettingsTabPage.Controls.Add(this.RescanGroupBox);
            this.SettingsTabPage.Controls.Add(this.MaxLevelTextBox);
            this.SettingsTabPage.Controls.Add(this.MaxOffsetTextBox);
            this.SettingsTabPage.Controls.Add(this.ValueTypeLabel);
            this.SettingsTabPage.Controls.Add(this.TargetAddressTextBox);
            this.SettingsTabPage.Controls.Add(this.TargetAddressLabel);
            this.SettingsTabPage.Controls.Add(this.ValueTypeComboBox);
            this.SettingsTabPage.Controls.Add(this.MaxOffsetLabel);
            this.SettingsTabPage.Controls.Add(this.MaxLevelLabel);
            this.SettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsTabPage.Name = "SettingsTabPage";
            this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTabPage.Size = new System.Drawing.Size(409, 208);
            this.SettingsTabPage.TabIndex = 0;
            this.SettingsTabPage.Text = "Settings";
            // 
            // RescanGroupBox
            // 
            this.RescanGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RescanGroupBox.Controls.Add(this.ValueTextBox);
            this.RescanGroupBox.Controls.Add(this.toolStrip1);
            this.RescanGroupBox.Controls.Add(this.IgnoreAddressCheckBox);
            this.RescanGroupBox.Controls.Add(this.ConstraintsListView);
            this.RescanGroupBox.Location = new System.Drawing.Point(11, 71);
            this.RescanGroupBox.Name = "RescanGroupBox";
            this.RescanGroupBox.Size = new System.Drawing.Size(392, 129);
            this.RescanGroupBox.TabIndex = 163;
            this.RescanGroupBox.TabStop = false;
            this.RescanGroupBox.Text = "Rescan Constraints";
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.ForeColor = System.Drawing.Color.Red;
            this.ValueTextBox.IsHex = true;
            this.ValueTextBox.Location = new System.Drawing.Point(6, 44);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(134, 20);
            this.ValueTextBox.TabIndex = 164;
            this.ValueTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.ValueTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueTextBox.WaterMarkText = null;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddConstraintButton,
            this.RemoveConstraintButton,
            this.ClearConstraintsButton,
            this.toolStripSeparator3,
            this.ScanOptionsToolStripDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(386, 25);
            this.toolStrip1.TabIndex = 165;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AddConstraintButton
            // 
            this.AddConstraintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddConstraintButton.Image = global::Anathema.Properties.Resources.DownArrows;
            this.AddConstraintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddConstraintButton.Name = "AddConstraintButton";
            this.AddConstraintButton.Size = new System.Drawing.Size(23, 22);
            this.AddConstraintButton.Text = "Add Constraint";
            // 
            // RemoveConstraintButton
            // 
            this.RemoveConstraintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveConstraintButton.Image = global::Anathema.Properties.Resources.X;
            this.RemoveConstraintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveConstraintButton.Name = "RemoveConstraintButton";
            this.RemoveConstraintButton.Size = new System.Drawing.Size(23, 22);
            this.RemoveConstraintButton.Text = "Remove Selected Constraints";
            // 
            // ClearConstraintsButton
            // 
            this.ClearConstraintsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearConstraintsButton.Image = global::Anathema.Properties.Resources.Cancel;
            this.ClearConstraintsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearConstraintsButton.Name = "ClearConstraintsButton";
            this.ClearConstraintsButton.Size = new System.Drawing.Size(23, 22);
            this.ClearConstraintsButton.Text = "Clear Constraints";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // ScanOptionsToolStripDropDownButton
            // 
            this.ScanOptionsToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScanOptionsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangedToolStripMenuItem,
            this.UnchangedToolStripMenuItem,
            this.IncreasedToolStripMenuItem,
            this.DecreasedToolStripMenuItem,
            this.EqualToToolStripMenuItem,
            this.NotEqualToToolStripMenuItem,
            this.IncreasedByToolStripMenuItem,
            this.DecreasedByToolStripMenuItem,
            this.GreaterThanToolStripMenuItem,
            this.GreaterThanOrEqualToToolStripMenuItem,
            this.LessThanToolStripMenuItem,
            this.LessThanOrEqualToToolStripMenuItem,
            this.NotScientificNotationToolStripMenuItem});
            this.ScanOptionsToolStripDropDownButton.Image = global::Anathema.Properties.Resources.Changed;
            this.ScanOptionsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanOptionsToolStripDropDownButton.Name = "ScanOptionsToolStripDropDownButton";
            this.ScanOptionsToolStripDropDownButton.Size = new System.Drawing.Size(29, 22);
            this.ScanOptionsToolStripDropDownButton.Text = "Scan Options";
            // 
            // ChangedToolStripMenuItem
            // 
            this.ChangedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Changed;
            this.ChangedToolStripMenuItem.Name = "ChangedToolStripMenuItem";
            this.ChangedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.ChangedToolStripMenuItem.Text = "Changed";
            // 
            // UnchangedToolStripMenuItem
            // 
            this.UnchangedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Unchanged;
            this.UnchangedToolStripMenuItem.Name = "UnchangedToolStripMenuItem";
            this.UnchangedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.UnchangedToolStripMenuItem.Text = "Unchanged";
            // 
            // IncreasedToolStripMenuItem
            // 
            this.IncreasedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Increased;
            this.IncreasedToolStripMenuItem.Name = "IncreasedToolStripMenuItem";
            this.IncreasedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.IncreasedToolStripMenuItem.Text = "Increased";
            // 
            // DecreasedToolStripMenuItem
            // 
            this.DecreasedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Decreased;
            this.DecreasedToolStripMenuItem.Name = "DecreasedToolStripMenuItem";
            this.DecreasedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.DecreasedToolStripMenuItem.Text = "Decreased";
            // 
            // EqualToToolStripMenuItem
            // 
            this.EqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.Equal;
            this.EqualToToolStripMenuItem.Name = "EqualToToolStripMenuItem";
            this.EqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.EqualToToolStripMenuItem.Text = "Equal to";
            // 
            // NotEqualToToolStripMenuItem
            // 
            this.NotEqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.NotEqual;
            this.NotEqualToToolStripMenuItem.Name = "NotEqualToToolStripMenuItem";
            this.NotEqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.NotEqualToToolStripMenuItem.Text = "Not Equal to";
            // 
            // IncreasedByToolStripMenuItem
            // 
            this.IncreasedByToolStripMenuItem.Image = global::Anathema.Properties.Resources.PlusX;
            this.IncreasedByToolStripMenuItem.Name = "IncreasedByToolStripMenuItem";
            this.IncreasedByToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.IncreasedByToolStripMenuItem.Text = "Increased by";
            // 
            // DecreasedByToolStripMenuItem
            // 
            this.DecreasedByToolStripMenuItem.Image = global::Anathema.Properties.Resources.MinusX;
            this.DecreasedByToolStripMenuItem.Name = "DecreasedByToolStripMenuItem";
            this.DecreasedByToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.DecreasedByToolStripMenuItem.Text = "Decreased by";
            // 
            // GreaterThanToolStripMenuItem
            // 
            this.GreaterThanToolStripMenuItem.Image = global::Anathema.Properties.Resources.GreaterThan;
            this.GreaterThanToolStripMenuItem.Name = "GreaterThanToolStripMenuItem";
            this.GreaterThanToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.GreaterThanToolStripMenuItem.Text = "Greater Than";
            // 
            // GreaterThanOrEqualToToolStripMenuItem
            // 
            this.GreaterThanOrEqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.GreaterThanOrEqual;
            this.GreaterThanOrEqualToToolStripMenuItem.Name = "GreaterThanOrEqualToToolStripMenuItem";
            this.GreaterThanOrEqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.GreaterThanOrEqualToToolStripMenuItem.Text = "Greater Than or Equal to";
            // 
            // LessThanToolStripMenuItem
            // 
            this.LessThanToolStripMenuItem.Image = global::Anathema.Properties.Resources.LessThan;
            this.LessThanToolStripMenuItem.Name = "LessThanToolStripMenuItem";
            this.LessThanToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.LessThanToolStripMenuItem.Text = "Less Than";
            // 
            // LessThanOrEqualToToolStripMenuItem
            // 
            this.LessThanOrEqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.LessThanOrEqual;
            this.LessThanOrEqualToToolStripMenuItem.Name = "LessThanOrEqualToToolStripMenuItem";
            this.LessThanOrEqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.LessThanOrEqualToToolStripMenuItem.Text = "Less Than or Equal to";
            // 
            // IgnoreAddressCheckBox
            // 
            this.IgnoreAddressCheckBox.AutoSize = true;
            this.IgnoreAddressCheckBox.Location = new System.Drawing.Point(146, 46);
            this.IgnoreAddressCheckBox.Name = "IgnoreAddressCheckBox";
            this.IgnoreAddressCheckBox.Size = new System.Drawing.Size(97, 17);
            this.IgnoreAddressCheckBox.TabIndex = 159;
            this.IgnoreAddressCheckBox.Text = "Ignore Address";
            this.IgnoreAddressCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConstraintsListView
            // 
            this.ConstraintsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstraintsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ConstraintHeader});
            this.ConstraintsListView.FullRowSelect = true;
            this.ConstraintsListView.Location = new System.Drawing.Point(6, 70);
            this.ConstraintsListView.Name = "ConstraintsListView";
            this.ConstraintsListView.Size = new System.Drawing.Size(380, 53);
            this.ConstraintsListView.TabIndex = 158;
            this.ConstraintsListView.UseCompatibleStateImageBehavior = false;
            this.ConstraintsListView.View = System.Windows.Forms.View.Details;
            // 
            // ConstraintHeader
            // 
            this.ConstraintHeader.Text = "Constraint";
            this.ConstraintHeader.Width = 312;
            // 
            // MaxLevelTextBox
            // 
            this.MaxLevelTextBox.ForeColor = System.Drawing.Color.Red;
            this.MaxLevelTextBox.IsHex = false;
            this.MaxLevelTextBox.Location = new System.Drawing.Point(279, 6);
            this.MaxLevelTextBox.Name = "MaxLevelTextBox";
            this.MaxLevelTextBox.Size = new System.Drawing.Size(64, 20);
            this.MaxLevelTextBox.TabIndex = 162;
            this.MaxLevelTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.MaxLevelTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxLevelTextBox.WaterMarkText = null;
            // 
            // MaxOffsetTextBox
            // 
            this.MaxOffsetTextBox.ForeColor = System.Drawing.Color.Red;
            this.MaxOffsetTextBox.IsHex = true;
            this.MaxOffsetTextBox.Location = new System.Drawing.Point(279, 32);
            this.MaxOffsetTextBox.Name = "MaxOffsetTextBox";
            this.MaxOffsetTextBox.Size = new System.Drawing.Size(64, 20);
            this.MaxOffsetTextBox.TabIndex = 161;
            this.MaxOffsetTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.MaxOffsetTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxOffsetTextBox.WaterMarkText = null;
            // 
            // ValueTypeLabel
            // 
            this.ValueTypeLabel.AutoSize = true;
            this.ValueTypeLabel.Location = new System.Drawing.Point(8, 35);
            this.ValueTypeLabel.Name = "ValueTypeLabel";
            this.ValueTypeLabel.Size = new System.Drawing.Size(61, 13);
            this.ValueTypeLabel.TabIndex = 160;
            this.ValueTypeLabel.Text = "Value Type";
            // 
            // TargetAddressTextBox
            // 
            this.TargetAddressTextBox.ForeColor = System.Drawing.Color.Red;
            this.TargetAddressTextBox.IsHex = true;
            this.TargetAddressTextBox.Location = new System.Drawing.Point(75, 6);
            this.TargetAddressTextBox.Name = "TargetAddressTextBox";
            this.TargetAddressTextBox.Size = new System.Drawing.Size(134, 20);
            this.TargetAddressTextBox.TabIndex = 159;
            this.TargetAddressTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.TargetAddressTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TargetAddressTextBox.WaterMarkText = null;
            // 
            // ResultsTabPage
            // 
            this.ResultsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.ResultsTabPage.Controls.Add(this.PointerListView);
            this.ResultsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ResultsTabPage.Name = "ResultsTabPage";
            this.ResultsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ResultsTabPage.Size = new System.Drawing.Size(409, 208);
            this.ResultsTabPage.TabIndex = 1;
            this.ResultsTabPage.Text = "Results";
            // 
            // PointerListView
            // 
            this.PointerListView.BackColor = System.Drawing.SystemColors.Window;
            this.PointerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ValueHeader,
            this.BaseHeader});
            this.PointerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PointerListView.FullRowSelect = true;
            this.PointerListView.Location = new System.Drawing.Point(3, 3);
            this.PointerListView.Name = "PointerListView";
            this.PointerListView.Size = new System.Drawing.Size(403, 202);
            this.PointerListView.TabIndex = 157;
            this.PointerListView.UseCompatibleStateImageBehavior = false;
            this.PointerListView.View = System.Windows.Forms.View.Details;
            this.PointerListView.VirtualMode = true;
            this.PointerListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.PointerListView_RetrieveVirtualItem);
            this.PointerListView.DoubleClick += new System.EventHandler(this.PointerListView_DoubleClick);
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 88;
            // 
            // BaseHeader
            // 
            this.BaseHeader.Text = "Base";
            this.BaseHeader.Width = 94;
            // 
            // NotScientificNotationToolStripMenuItem
            // 
            this.NotScientificNotationToolStripMenuItem.Image = global::Anathema.Properties.Resources.Intersection;
            this.NotScientificNotationToolStripMenuItem.Name = "NotScientificNotationToolStripMenuItem";
            this.NotScientificNotationToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.NotScientificNotationToolStripMenuItem.Text = "Not Scientific Notation";
            // 
            // GUIPointerScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 259);
            this.Controls.Add(this.PointerScanTabControl);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIPointerScanner";
            this.Text = "Pointer Scanner";
            this.Resize += new System.EventHandler(this.GUIPointerScanner_Resize);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.PointerScanTabControl.ResumeLayout(false);
            this.SettingsTabPage.ResumeLayout(false);
            this.SettingsTabPage.PerformLayout();
            this.RescanGroupBox.ResumeLayout(false);
            this.RescanGroupBox.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResultsTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StopScanButton;
        private System.Windows.Forms.Label TargetAddressLabel;
        private System.Windows.Forms.Label MaxOffsetLabel;
        private System.Windows.Forms.Label MaxLevelLabel;
        private FlickerFreeListView PointerListView;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.ToolStripButton RebuildPointersButton;
        private System.Windows.Forms.ColumnHeader BaseHeader;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton AddSelectedResultsButton;
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.TabControl PointerScanTabControl;
        private System.Windows.Forms.TabPage SettingsTabPage;
        private System.Windows.Forms.TabPage ResultsTabPage;
        private HexDecTextBox TargetAddressTextBox;
        private System.Windows.Forms.Label ValueTypeLabel;
        private HexDecTextBox MaxOffsetTextBox;
        private HexDecTextBox MaxLevelTextBox;
        private System.Windows.Forms.GroupBox RescanGroupBox;
        private System.Windows.Forms.ListView ConstraintsListView;
        private System.Windows.Forms.ColumnHeader ConstraintHeader;
        private System.Windows.Forms.CheckBox IgnoreAddressCheckBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton AddConstraintButton;
        private System.Windows.Forms.ToolStripButton RemoveConstraintButton;
        private System.Windows.Forms.ToolStripButton ClearConstraintsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton ScanOptionsToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem ChangedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UnchangedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IncreasedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DecreasedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EqualToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NotEqualToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IncreasedByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DecreasedByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GreaterThanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GreaterThanOrEqualToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LessThanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LessThanOrEqualToToolStripMenuItem;
        private HexDecTextBox ValueTextBox;
        private System.Windows.Forms.ToolStripMenuItem NotScientificNotationToolStripMenuItem;
    }
}