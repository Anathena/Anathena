namespace Anathema
{
    partial class Anathema
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Anathema));
            this.label3 = new System.Windows.Forms.Label();
            this.TableListView = new System.Windows.Forms.ListView();
            this.CheckBoxHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.OpenATButton = new System.Windows.Forms.ToolStripButton();
            this.MergeATButton = new System.Windows.Forms.ToolStripButton();
            this.SaveATButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AddSelectedButton = new System.Windows.Forms.ToolStripButton();
            this.AddSpecificButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearTableButton = new System.Windows.Forms.ToolStripButton();
            this.UndoTableDeleteButton = new System.Windows.Forms.ToolStripButton();
            this.GUIMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CPUInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GUIEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HexEdtiorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MemoryRegionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MemoryViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateFoundTimer = new System.Windows.Forms.Timer(this.components);
            this.WriteTimer = new System.Windows.Forms.Timer(this.components);
            this.AddressListViewRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addItemToTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showItemsAsSignedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectedProcessLabel = new System.Windows.Forms.Label();
            this.AddressListView = new System.Windows.Forms.ListView();
            this._AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._LastValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ScanTypeWorldStrip = new System.Windows.Forms.ToolStrip();
            this.NotEqualValButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.UnknownValButton = new System.Windows.Forms.ToolStripButton();
            this.ChangedValButton = new System.Windows.Forms.ToolStripButton();
            this.IncreasedValButton = new System.Windows.Forms.ToolStripButton();
            this.DecreasedValButton = new System.Windows.Forms.ToolStripButton();
            this.EqualValButton = new System.Windows.Forms.ToolStripButton();
            this.GreaterThanValButton = new System.Windows.Forms.ToolStripButton();
            this.LessThanValButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ResetScanCompareButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddSettingsToFilterButton = new System.Windows.Forms.ToolStripButton();
            this.ClearFilterButton = new System.Windows.Forms.ToolStripButton();
            this.AddressCount = new System.Windows.Forms.Label();
            this.ScanValueTB = new System.Windows.Forms.TextBox();
            this.ScanTimeLabel = new System.Windows.Forms.Label();
            this.DataTypeCBB = new System.Windows.Forms.ComboBox();
            this.CompareTypeLabel = new System.Windows.Forms.Label();
            this.ScanValueUpperTB = new System.Windows.Forms.TextBox();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.SelectProcessButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.NewScanButton = new System.Windows.Forms.ToolStripButton();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.NextScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.UndoScanButton = new System.Windows.Forms.ToolStripButton();
            this.AbortScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ConvertToHexBinButton = new System.Windows.Forms.ToolStripButton();
            this.ScanOptionsButton = new System.Windows.Forms.ToolStripButton();
            this.ToggleCompareFirstScanButton = new System.Windows.Forms.ToolStripButton();
            this.ScanHistoryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.EndSSAButton = new System.Windows.Forms.Button();
            this.StartSSAButton = new System.Windows.Forms.Button();
            this.PageVisualizerButton = new System.Windows.Forms.Button();
            this.TakeSnapshotButton = new System.Windows.Forms.Button();
            this.SnapshotListBox = new System.Windows.Forms.ListBox();
            this.SaveSnapshotButton = new System.Windows.Forms.Button();
            this.RestoreSnapshotButton = new System.Windows.Forms.Button();
            this.EndRSSAButton = new System.Windows.Forms.Button();
            this.StartRSSAButton = new System.Windows.Forms.Button();
            this.AddConstraintButton = new System.Windows.Forms.Button();
            this.MainToolStrip.SuspendLayout();
            this.GUIMenuStrip.SuspendLayout();
            this.AddressListViewRightClickMenu.SuspendLayout();
            this.ScanTypeWorldStrip.SuspendLayout();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(268, 308);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 114;
            this.label3.Text = "Table";
            // 
            // TableListView
            // 
            this.TableListView.BackColor = System.Drawing.SystemColors.Control;
            this.TableListView.CheckBoxes = true;
            this.TableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CheckBoxHeader,
            this.DescriptionHeader,
            this.AddressHeader,
            this.TypeHeader,
            this.ValueHeader});
            this.TableListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TableListView.FullRowSelect = true;
            this.TableListView.Location = new System.Drawing.Point(12, 327);
            this.TableListView.Name = "TableListView";
            this.TableListView.ShowGroups = false;
            this.TableListView.Size = new System.Drawing.Size(561, 265);
            this.TableListView.TabIndex = 112;
            this.TableListView.UseCompatibleStateImageBehavior = false;
            this.TableListView.View = System.Windows.Forms.View.Details;
            // 
            // CheckBoxHeader
            // 
            this.CheckBoxHeader.Text = "";
            this.CheckBoxHeader.Width = 24;
            // 
            // DescriptionHeader
            // 
            this.DescriptionHeader.Text = "Description";
            this.DescriptionHeader.Width = 146;
            // 
            // AddressHeader
            // 
            this.AddressHeader.Text = "Address";
            this.AddressHeader.Width = 93;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Text = "Type";
            this.TypeHeader.Width = 70;
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 204;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.MainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenATButton,
            this.MergeATButton,
            this.SaveATButton,
            this.toolStripSeparator3,
            this.AddSelectedButton,
            this.AddSpecificButton,
            this.toolStripSeparator5,
            this.ClearTableButton,
            this.UndoTableDeleteButton});
            this.MainToolStrip.Location = new System.Drawing.Point(12, 305);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainToolStrip.Size = new System.Drawing.Size(176, 25);
            this.MainToolStrip.TabIndex = 113;
            // 
            // OpenATButton
            // 
            this.OpenATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenATButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenATButton.Image")));
            this.OpenATButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenATButton.Name = "OpenATButton";
            this.OpenATButton.Size = new System.Drawing.Size(23, 22);
            // 
            // MergeATButton
            // 
            this.MergeATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MergeATButton.Image = ((System.Drawing.Image)(resources.GetObject("MergeATButton.Image")));
            this.MergeATButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MergeATButton.Name = "MergeATButton";
            this.MergeATButton.Size = new System.Drawing.Size(23, 22);
            // 
            // SaveATButton
            // 
            this.SaveATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveATButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveATButton.Image")));
            this.SaveATButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveATButton.Name = "SaveATButton";
            this.SaveATButton.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // AddSelectedButton
            // 
            this.AddSelectedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddSelectedButton.Image = ((System.Drawing.Image)(resources.GetObject("AddSelectedButton.Image")));
            this.AddSelectedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddSelectedButton.Name = "AddSelectedButton";
            this.AddSelectedButton.Size = new System.Drawing.Size(23, 22);
            // 
            // AddSpecificButton
            // 
            this.AddSpecificButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddSpecificButton.Image = ((System.Drawing.Image)(resources.GetObject("AddSpecificButton.Image")));
            this.AddSpecificButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddSpecificButton.Name = "AddSpecificButton";
            this.AddSpecificButton.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // ClearTableButton
            // 
            this.ClearTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearTableButton.Enabled = false;
            this.ClearTableButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearTableButton.Image")));
            this.ClearTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearTableButton.Name = "ClearTableButton";
            this.ClearTableButton.Size = new System.Drawing.Size(23, 22);
            // 
            // UndoTableDeleteButton
            // 
            this.UndoTableDeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoTableDeleteButton.Enabled = false;
            this.UndoTableDeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoTableDeleteButton.Image")));
            this.UndoTableDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoTableDeleteButton.Name = "UndoTableDeleteButton";
            this.UndoTableDeleteButton.Size = new System.Drawing.Size(23, 22);
            // 
            // GUIMenuStrip
            // 
            this.GUIMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.GUIMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ProcessToolStripMenuItem,
            this.ToolsToolStripMenuItem});
            this.GUIMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.GUIMenuStrip.Name = "GUIMenuStrip";
            this.GUIMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.GUIMenuStrip.Size = new System.Drawing.Size(811, 24);
            this.GUIMenuStrip.TabIndex = 126;
            this.GUIMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.dieToolStripMenuItem,
            this.openToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.testToolStripMenuItem.Text = "New";
            // 
            // dieToolStripMenuItem
            // 
            this.dieToolStripMenuItem.Name = "dieToolStripMenuItem";
            this.dieToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.dieToolStripMenuItem.Text = "Save";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // ProcessToolStripMenuItem
            // 
            this.ProcessToolStripMenuItem.Name = "ProcessToolStripMenuItem";
            this.ProcessToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.ProcessToolStripMenuItem.Text = "Process";
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CPUInfoToolStripMenuItem,
            this.GUIEditorToolStripMenuItem,
            this.HexEdtiorToolStripMenuItem,
            this.MemoryRegionsToolStripMenuItem,
            this.MemoryViewerToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.ToolsToolStripMenuItem.Text = "Tools";
            // 
            // CPUInfoToolStripMenuItem
            // 
            this.CPUInfoToolStripMenuItem.Name = "CPUInfoToolStripMenuItem";
            this.CPUInfoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.CPUInfoToolStripMenuItem.Text = "CPU Info";
            // 
            // GUIEditorToolStripMenuItem
            // 
            this.GUIEditorToolStripMenuItem.Name = "GUIEditorToolStripMenuItem";
            this.GUIEditorToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.GUIEditorToolStripMenuItem.Text = "GUI Editor";
            // 
            // HexEdtiorToolStripMenuItem
            // 
            this.HexEdtiorToolStripMenuItem.Name = "HexEdtiorToolStripMenuItem";
            this.HexEdtiorToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.HexEdtiorToolStripMenuItem.Text = "Hex Edtior";
            // 
            // MemoryRegionsToolStripMenuItem
            // 
            this.MemoryRegionsToolStripMenuItem.Name = "MemoryRegionsToolStripMenuItem";
            this.MemoryRegionsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.MemoryRegionsToolStripMenuItem.Text = "Memory Regions";
            // 
            // MemoryViewerToolStripMenuItem
            // 
            this.MemoryViewerToolStripMenuItem.Name = "MemoryViewerToolStripMenuItem";
            this.MemoryViewerToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.MemoryViewerToolStripMenuItem.Text = "Memory Viewer";
            // 
            // UpdateFoundTimer
            // 
            this.UpdateFoundTimer.Enabled = true;
            this.UpdateFoundTimer.Interval = 513;
            // 
            // WriteTimer
            // 
            this.WriteTimer.Enabled = true;
            this.WriteTimer.Interval = 193;
            // 
            // AddressListViewRightClickMenu
            // 
            this.AddressListViewRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addItemToTableToolStripMenuItem,
            this.showItemsAsSignedToolStripMenuItem});
            this.AddressListViewRightClickMenu.Name = "AddressListViewRightClickMenu";
            this.AddressListViewRightClickMenu.Size = new System.Drawing.Size(189, 48);
            // 
            // addItemToTableToolStripMenuItem
            // 
            this.addItemToTableToolStripMenuItem.Name = "addItemToTableToolStripMenuItem";
            this.addItemToTableToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.addItemToTableToolStripMenuItem.Text = "Add Item to Table";
            // 
            // showItemsAsSignedToolStripMenuItem
            // 
            this.showItemsAsSignedToolStripMenuItem.Name = "showItemsAsSignedToolStripMenuItem";
            this.showItemsAsSignedToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.showItemsAsSignedToolStripMenuItem.Text = "Show Items as Signed";
            // 
            // SelectedProcessLabel
            // 
            this.SelectedProcessLabel.AutoSize = true;
            this.SelectedProcessLabel.Location = new System.Drawing.Point(256, 5);
            this.SelectedProcessLabel.Name = "SelectedProcessLabel";
            this.SelectedProcessLabel.Size = new System.Drawing.Size(107, 13);
            this.SelectedProcessLabel.TabIndex = 129;
            this.SelectedProcessLabel.Text = "No Process Selected";
            this.SelectedProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddressListView
            // 
            this.AddressListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.AddressListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._AddressHeader,
            this._ValueHeader,
            this._LastValueHeader});
            this.AddressListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressListView.FullRowSelect = true;
            this.AddressListView.Location = new System.Drawing.Point(12, 46);
            this.AddressListView.Name = "AddressListView";
            this.AddressListView.Size = new System.Drawing.Size(283, 243);
            this.AddressListView.TabIndex = 116;
            this.AddressListView.UseCompatibleStateImageBehavior = false;
            this.AddressListView.View = System.Windows.Forms.View.Details;
            this.AddressListView.VirtualMode = true;
            // 
            // _AddressHeader
            // 
            this._AddressHeader.Text = "Address";
            this._AddressHeader.Width = 86;
            // 
            // _ValueHeader
            // 
            this._ValueHeader.Text = "Value";
            this._ValueHeader.Width = 86;
            // 
            // _LastValueHeader
            // 
            this._LastValueHeader.Text = "Last Value";
            this._LastValueHeader.Width = 97;
            // 
            // ProgressBar
            // 
            this.ProgressBar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ProgressBar.ForeColor = System.Drawing.Color.SteelBlue;
            this.ProgressBar.Location = new System.Drawing.Point(12, 27);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(283, 16);
            this.ProgressBar.Step = 1;
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressBar.TabIndex = 115;
            // 
            // ScanTypeWorldStrip
            // 
            this.ScanTypeWorldStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanTypeWorldStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ScanTypeWorldStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanTypeWorldStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NotEqualValButton,
            this.toolStripSeparator6,
            this.UnknownValButton,
            this.ChangedValButton,
            this.IncreasedValButton,
            this.DecreasedValButton,
            this.EqualValButton,
            this.GreaterThanValButton,
            this.LessThanValButton,
            this.toolStripSeparator7,
            this.ResetScanCompareButton,
            this.toolStripSeparator1,
            this.AddSettingsToFilterButton,
            this.ClearFilterButton});
            this.ScanTypeWorldStrip.Location = new System.Drawing.Point(529, 109);
            this.ScanTypeWorldStrip.Name = "ScanTypeWorldStrip";
            this.ScanTypeWorldStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanTypeWorldStrip.Size = new System.Drawing.Size(274, 25);
            this.ScanTypeWorldStrip.TabIndex = 122;
            this.ScanTypeWorldStrip.Text = "toolStrip1";
            // 
            // NotEqualValButton
            // 
            this.NotEqualValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NotEqualValButton.Image = ((System.Drawing.Image)(resources.GetObject("NotEqualValButton.Image")));
            this.NotEqualValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NotEqualValButton.Name = "NotEqualValButton";
            this.NotEqualValButton.Size = new System.Drawing.Size(23, 22);
            this.NotEqualValButton.Text = "Negate Property";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // UnknownValButton
            // 
            this.UnknownValButton.Checked = true;
            this.UnknownValButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UnknownValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UnknownValButton.Image = ((System.Drawing.Image)(resources.GetObject("UnknownValButton.Image")));
            this.UnknownValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UnknownValButton.Name = "UnknownValButton";
            this.UnknownValButton.Size = new System.Drawing.Size(23, 22);
            this.UnknownValButton.Text = "Unknown Value";
            // 
            // ChangedValButton
            // 
            this.ChangedValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ChangedValButton.Image = ((System.Drawing.Image)(resources.GetObject("ChangedValButton.Image")));
            this.ChangedValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChangedValButton.Name = "ChangedValButton";
            this.ChangedValButton.Size = new System.Drawing.Size(23, 22);
            this.ChangedValButton.Text = "Changed Value";
            // 
            // IncreasedValButton
            // 
            this.IncreasedValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IncreasedValButton.Image = ((System.Drawing.Image)(resources.GetObject("IncreasedValButton.Image")));
            this.IncreasedValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedValButton.Name = "IncreasedValButton";
            this.IncreasedValButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedValButton.Text = "Increased Value";
            // 
            // DecreasedValButton
            // 
            this.DecreasedValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecreasedValButton.Image = ((System.Drawing.Image)(resources.GetObject("DecreasedValButton.Image")));
            this.DecreasedValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecreasedValButton.Name = "DecreasedValButton";
            this.DecreasedValButton.Size = new System.Drawing.Size(23, 22);
            this.DecreasedValButton.Text = "Decreased Value";
            // 
            // EqualValButton
            // 
            this.EqualValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EqualValButton.Image = ((System.Drawing.Image)(resources.GetObject("EqualValButton.Image")));
            this.EqualValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EqualValButton.Name = "EqualValButton";
            this.EqualValButton.Size = new System.Drawing.Size(23, 22);
            this.EqualValButton.Text = "Exact Value";
            // 
            // GreaterThanValButton
            // 
            this.GreaterThanValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GreaterThanValButton.Image = ((System.Drawing.Image)(resources.GetObject("GreaterThanValButton.Image")));
            this.GreaterThanValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GreaterThanValButton.Name = "GreaterThanValButton";
            this.GreaterThanValButton.Size = new System.Drawing.Size(23, 22);
            this.GreaterThanValButton.Text = "Greater Than Value";
            // 
            // LessThanValButton
            // 
            this.LessThanValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LessThanValButton.Image = ((System.Drawing.Image)(resources.GetObject("LessThanValButton.Image")));
            this.LessThanValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LessThanValButton.Name = "LessThanValButton";
            this.LessThanValButton.Size = new System.Drawing.Size(23, 22);
            this.LessThanValButton.Text = "Less Than Value";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // ResetScanCompareButton
            // 
            this.ResetScanCompareButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ResetScanCompareButton.Image = ((System.Drawing.Image)(resources.GetObject("ResetScanCompareButton.Image")));
            this.ResetScanCompareButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetScanCompareButton.Name = "ResetScanCompareButton";
            this.ResetScanCompareButton.Size = new System.Drawing.Size(23, 22);
            this.ResetScanCompareButton.Text = "Clear Selections";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // AddSettingsToFilterButton
            // 
            this.AddSettingsToFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddSettingsToFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("AddSettingsToFilterButton.Image")));
            this.AddSettingsToFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddSettingsToFilterButton.Name = "AddSettingsToFilterButton";
            this.AddSettingsToFilterButton.Size = new System.Drawing.Size(23, 22);
            this.AddSettingsToFilterButton.Text = "Add to Filter";
            // 
            // ClearFilterButton
            // 
            this.ClearFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearFilterButton.Image")));
            this.ClearFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearFilterButton.Name = "ClearFilterButton";
            this.ClearFilterButton.Size = new System.Drawing.Size(23, 22);
            this.ClearFilterButton.Text = "Clear Filter";
            // 
            // AddressCount
            // 
            this.AddressCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressCount.Location = new System.Drawing.Point(12, 288);
            this.AddressCount.Name = "AddressCount";
            this.AddressCount.Size = new System.Drawing.Size(214, 17);
            this.AddressCount.TabIndex = 123;
            this.AddressCount.Text = "Items: 0";
            // 
            // ScanValueTB
            // 
            this.ScanValueTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanValueTB.Location = new System.Drawing.Point(528, 55);
            this.ScanValueTB.Name = "ScanValueTB";
            this.ScanValueTB.Size = new System.Drawing.Size(128, 20);
            this.ScanValueTB.TabIndex = 117;
            // 
            // ScanTimeLabel
            // 
            this.ScanTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScanTimeLabel.Location = new System.Drawing.Point(207, 288);
            this.ScanTimeLabel.Name = "ScanTimeLabel";
            this.ScanTimeLabel.Size = new System.Drawing.Size(88, 17);
            this.ScanTimeLabel.TabIndex = 124;
            this.ScanTimeLabel.Text = "Time: 0";
            // 
            // DataTypeCBB
            // 
            this.DataTypeCBB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataTypeCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DataTypeCBB.FormattingEnabled = true;
            this.DataTypeCBB.Items.AddRange(new object[] {
            "Binary",
            "Byte",
            "Int 16",
            "Int 32",
            "Int 64",
            "Single",
            "Double",
            "Text",
            "Array of Bytes",
            "All (Byte to Double)"});
            this.DataTypeCBB.Location = new System.Drawing.Point(532, 85);
            this.DataTypeCBB.Name = "DataTypeCBB";
            this.DataTypeCBB.Size = new System.Drawing.Size(101, 21);
            this.DataTypeCBB.TabIndex = 119;
            // 
            // CompareTypeLabel
            // 
            this.CompareTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CompareTypeLabel.Location = new System.Drawing.Point(639, 88);
            this.CompareTypeLabel.Name = "CompareTypeLabel";
            this.CompareTypeLabel.Size = new System.Drawing.Size(160, 13);
            this.CompareTypeLabel.TabIndex = 120;
            this.CompareTypeLabel.Text = "Not Between or Equal to Values";
            this.CompareTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ScanValueUpperTB
            // 
            this.ScanValueUpperTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanValueUpperTB.Location = new System.Drawing.Point(671, 55);
            this.ScanValueUpperTB.Name = "ScanValueUpperTB";
            this.ScanValueUpperTB.Size = new System.Drawing.Size(131, 20);
            this.ScanValueUpperTB.TabIndex = 118;
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectProcessButton,
            this.toolStripSeparator4,
            this.NewScanButton,
            this.StartScanButton,
            this.NextScanButton,
            this.toolStripSeparator2,
            this.UndoScanButton,
            this.AbortScanButton,
            this.toolStripSeparator8,
            this.ConvertToHexBinButton,
            this.ScanOptionsButton,
            this.ToggleCompareFirstScanButton,
            this.ScanHistoryButton,
            this.toolStripButton2});
            this.ScanToolStrip.Location = new System.Drawing.Point(528, 27);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(274, 25);
            this.ScanToolStrip.TabIndex = 121;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // SelectProcessButton
            // 
            this.SelectProcessButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectProcessButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectProcessButton.Image")));
            this.SelectProcessButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectProcessButton.Name = "SelectProcessButton";
            this.SelectProcessButton.Size = new System.Drawing.Size(23, 22);
            this.SelectProcessButton.Text = "Select Process";
            this.SelectProcessButton.Click += new System.EventHandler(this.SelectProcessButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // NewScanButton
            // 
            this.NewScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewScanButton.Image = ((System.Drawing.Image)(resources.GetObject("NewScanButton.Image")));
            this.NewScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewScanButton.Name = "NewScanButton";
            this.NewScanButton.Size = new System.Drawing.Size(23, 22);
            this.NewScanButton.Text = "New Scan";
            // 
            // StartScanButton
            // 
            this.StartScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartScanButton.Image = ((System.Drawing.Image)(resources.GetObject("StartScanButton.Image")));
            this.StartScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartScanButton.Name = "StartScanButton";
            this.StartScanButton.Size = new System.Drawing.Size(23, 22);
            this.StartScanButton.Text = "Start Scan";
            // 
            // NextScanButton
            // 
            this.NextScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextScanButton.Image = ((System.Drawing.Image)(resources.GetObject("NextScanButton.Image")));
            this.NextScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextScanButton.Name = "NextScanButton";
            this.NextScanButton.Size = new System.Drawing.Size(23, 22);
            this.NextScanButton.Text = "Next Scan";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // UndoScanButton
            // 
            this.UndoScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoScanButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoScanButton.Image")));
            this.UndoScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoScanButton.Name = "UndoScanButton";
            this.UndoScanButton.Size = new System.Drawing.Size(23, 22);
            this.UndoScanButton.Text = "Undo Scan";
            // 
            // AbortScanButton
            // 
            this.AbortScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AbortScanButton.Image = ((System.Drawing.Image)(resources.GetObject("AbortScanButton.Image")));
            this.AbortScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AbortScanButton.Name = "AbortScanButton";
            this.AbortScanButton.Size = new System.Drawing.Size(23, 22);
            this.AbortScanButton.Text = "Abort Scan";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // ConvertToHexBinButton
            // 
            this.ConvertToHexBinButton.CheckOnClick = true;
            this.ConvertToHexBinButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ConvertToHexBinButton.Image = ((System.Drawing.Image)(resources.GetObject("ConvertToHexBinButton.Image")));
            this.ConvertToHexBinButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConvertToHexBinButton.Name = "ConvertToHexBinButton";
            this.ConvertToHexBinButton.Size = new System.Drawing.Size(23, 22);
            this.ConvertToHexBinButton.Text = "Convert to Hex or Bin";
            // 
            // ScanOptionsButton
            // 
            this.ScanOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScanOptionsButton.Image = ((System.Drawing.Image)(resources.GetObject("ScanOptionsButton.Image")));
            this.ScanOptionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanOptionsButton.Name = "ScanOptionsButton";
            this.ScanOptionsButton.Size = new System.Drawing.Size(23, 22);
            this.ScanOptionsButton.Text = "Scan Options";
            // 
            // ToggleCompareFirstScanButton
            // 
            this.ToggleCompareFirstScanButton.CheckOnClick = true;
            this.ToggleCompareFirstScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToggleCompareFirstScanButton.Image = ((System.Drawing.Image)(resources.GetObject("ToggleCompareFirstScanButton.Image")));
            this.ToggleCompareFirstScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToggleCompareFirstScanButton.Name = "ToggleCompareFirstScanButton";
            this.ToggleCompareFirstScanButton.Size = new System.Drawing.Size(23, 22);
            this.ToggleCompareFirstScanButton.Text = "Toggle Compare to First Scan";
            // 
            // ScanHistoryButton
            // 
            this.ScanHistoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScanHistoryButton.Image = ((System.Drawing.Image)(resources.GetObject("ScanHistoryButton.Image")));
            this.ScanHistoryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanHistoryButton.Name = "ScanHistoryButton";
            this.ScanHistoryButton.Size = new System.Drawing.Size(23, 22);
            this.ScanHistoryButton.Text = "Scan History";
            this.ScanHistoryButton.ToolTipText = "Scan history for this scan session";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // EndSSAButton
            // 
            this.EndSSAButton.Location = new System.Drawing.Point(609, 166);
            this.EndSSAButton.Name = "EndSSAButton";
            this.EndSSAButton.Size = new System.Drawing.Size(75, 23);
            this.EndSSAButton.TabIndex = 128;
            this.EndSSAButton.Text = "End SSA";
            this.EndSSAButton.UseVisualStyleBackColor = true;
            this.EndSSAButton.Click += new System.EventHandler(this.EndSSAButton_Click);
            // 
            // StartSSAButton
            // 
            this.StartSSAButton.Location = new System.Drawing.Point(528, 166);
            this.StartSSAButton.Name = "StartSSAButton";
            this.StartSSAButton.Size = new System.Drawing.Size(75, 23);
            this.StartSSAButton.TabIndex = 127;
            this.StartSSAButton.Text = "Start SSA";
            this.StartSSAButton.UseVisualStyleBackColor = true;
            this.StartSSAButton.Click += new System.EventHandler(this.StartSSAButton_Click);
            // 
            // PageVisualizerButton
            // 
            this.PageVisualizerButton.Location = new System.Drawing.Point(724, 195);
            this.PageVisualizerButton.Name = "PageVisualizerButton";
            this.PageVisualizerButton.Size = new System.Drawing.Size(75, 23);
            this.PageVisualizerButton.TabIndex = 130;
            this.PageVisualizerButton.Text = "Graph";
            this.PageVisualizerButton.UseVisualStyleBackColor = true;
            this.PageVisualizerButton.Click += new System.EventHandler(this.PageVisualizerButton_Click);
            // 
            // TakeSnapshotButton
            // 
            this.TakeSnapshotButton.Location = new System.Drawing.Point(528, 137);
            this.TakeSnapshotButton.Name = "TakeSnapshotButton";
            this.TakeSnapshotButton.Size = new System.Drawing.Size(70, 23);
            this.TakeSnapshotButton.TabIndex = 131;
            this.TakeSnapshotButton.Text = "Snapshot";
            this.TakeSnapshotButton.UseVisualStyleBackColor = true;
            // 
            // SnapshotListBox
            // 
            this.SnapshotListBox.FormattingEnabled = true;
            this.SnapshotListBox.Location = new System.Drawing.Point(301, 27);
            this.SnapshotListBox.Name = "SnapshotListBox";
            this.SnapshotListBox.Size = new System.Drawing.Size(221, 95);
            this.SnapshotListBox.TabIndex = 132;
            // 
            // SaveSnapshotButton
            // 
            this.SaveSnapshotButton.Location = new System.Drawing.Point(601, 137);
            this.SaveSnapshotButton.Name = "SaveSnapshotButton";
            this.SaveSnapshotButton.Size = new System.Drawing.Size(92, 23);
            this.SaveSnapshotButton.TabIndex = 131;
            this.SaveSnapshotButton.Text = "Save Snapshot";
            this.SaveSnapshotButton.UseVisualStyleBackColor = true;
            // 
            // RestoreSnapshotButton
            // 
            this.RestoreSnapshotButton.Location = new System.Drawing.Point(699, 137);
            this.RestoreSnapshotButton.Name = "RestoreSnapshotButton";
            this.RestoreSnapshotButton.Size = new System.Drawing.Size(104, 23);
            this.RestoreSnapshotButton.TabIndex = 131;
            this.RestoreSnapshotButton.Text = "Restore Snapshot";
            this.RestoreSnapshotButton.UseVisualStyleBackColor = true;
            // 
            // EndRSSAButton
            // 
            this.EndRSSAButton.Location = new System.Drawing.Point(609, 195);
            this.EndRSSAButton.Name = "EndRSSAButton";
            this.EndRSSAButton.Size = new System.Drawing.Size(75, 23);
            this.EndRSSAButton.TabIndex = 128;
            this.EndRSSAButton.Text = "End RSSA";
            this.EndRSSAButton.UseVisualStyleBackColor = true;
            this.EndRSSAButton.Click += new System.EventHandler(this.EndRSSAButton_Click);
            // 
            // StartRSSAButton
            // 
            this.StartRSSAButton.Location = new System.Drawing.Point(528, 195);
            this.StartRSSAButton.Name = "StartRSSAButton";
            this.StartRSSAButton.Size = new System.Drawing.Size(75, 23);
            this.StartRSSAButton.TabIndex = 127;
            this.StartRSSAButton.Text = "Start RSSA";
            this.StartRSSAButton.UseVisualStyleBackColor = true;
            this.StartRSSAButton.Click += new System.EventHandler(this.StartRSSAButton_Click);
            // 
            // AddConstraintButton
            // 
            this.AddConstraintButton.Location = new System.Drawing.Point(724, 166);
            this.AddConstraintButton.Name = "AddConstraintButton";
            this.AddConstraintButton.Size = new System.Drawing.Size(75, 23);
            this.AddConstraintButton.TabIndex = 130;
            this.AddConstraintButton.Text = "Add Constr";
            this.AddConstraintButton.UseVisualStyleBackColor = true;
            this.AddConstraintButton.Click += new System.EventHandler(this.PageVisualizerButton_Click);
            // 
            // Anathema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 629);
            this.Controls.Add(this.SnapshotListBox);
            this.Controls.Add(this.RestoreSnapshotButton);
            this.Controls.Add(this.SaveSnapshotButton);
            this.Controls.Add(this.TakeSnapshotButton);
            this.Controls.Add(this.AddConstraintButton);
            this.Controls.Add(this.PageVisualizerButton);
            this.Controls.Add(this.ScanToolStrip);
            this.Controls.Add(this.StartRSSAButton);
            this.Controls.Add(this.EndRSSAButton);
            this.Controls.Add(this.StartSSAButton);
            this.Controls.Add(this.EndSSAButton);
            this.Controls.Add(this.ScanValueUpperTB);
            this.Controls.Add(this.ScanValueTB);
            this.Controls.Add(this.ScanTypeWorldStrip);
            this.Controls.Add(this.SelectedProcessLabel);
            this.Controls.Add(this.CompareTypeLabel);
            this.Controls.Add(this.GUIMenuStrip);
            this.Controls.Add(this.DataTypeCBB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TableListView);
            this.Controls.Add(this.ScanTimeLabel);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.AddressCount);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.AddressListView);
            this.Name = "Anathema";
            this.Text = "Anathema";
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.GUIMenuStrip.ResumeLayout(false);
            this.GUIMenuStrip.PerformLayout();
            this.AddressListViewRightClickMenu.ResumeLayout(false);
            this.ScanTypeWorldStrip.ResumeLayout(false);
            this.ScanTypeWorldStrip.PerformLayout();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView TableListView;
        private System.Windows.Forms.ColumnHeader CheckBoxHeader;
        private System.Windows.Forms.ColumnHeader DescriptionHeader;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton OpenATButton;
        private System.Windows.Forms.ToolStripButton MergeATButton;
        private System.Windows.Forms.ToolStripButton SaveATButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton AddSelectedButton;
        private System.Windows.Forms.ToolStripButton AddSpecificButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton ClearTableButton;
        private System.Windows.Forms.ToolStripButton UndoTableDeleteButton;
        private System.Windows.Forms.MenuStrip GUIMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CPUInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GUIEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HexEdtiorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MemoryRegionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MemoryViewerToolStripMenuItem;
        private System.Windows.Forms.Timer UpdateFoundTimer;
        private System.Windows.Forms.Timer WriteTimer;
        private System.Windows.Forms.ContextMenuStrip AddressListViewRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem addItemToTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showItemsAsSignedToolStripMenuItem;
        private System.Windows.Forms.Label SelectedProcessLabel;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton SelectProcessButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton NewScanButton;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStripButton NextScanButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton UndoScanButton;
        private System.Windows.Forms.ToolStripButton AbortScanButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton ConvertToHexBinButton;
        private System.Windows.Forms.ToolStripButton ScanOptionsButton;
        private System.Windows.Forms.ToolStripButton ToggleCompareFirstScanButton;
        private System.Windows.Forms.ToolStripButton ScanHistoryButton;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TextBox ScanValueUpperTB;
        private System.Windows.Forms.TextBox ScanValueTB;
        private System.Windows.Forms.ToolStrip ScanTypeWorldStrip;
        private System.Windows.Forms.ToolStripButton NotEqualValButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton UnknownValButton;
        private System.Windows.Forms.ToolStripButton ChangedValButton;
        private System.Windows.Forms.ToolStripButton IncreasedValButton;
        private System.Windows.Forms.ToolStripButton DecreasedValButton;
        private System.Windows.Forms.ToolStripButton EqualValButton;
        private System.Windows.Forms.ToolStripButton GreaterThanValButton;
        private System.Windows.Forms.ToolStripButton LessThanValButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton ResetScanCompareButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton AddSettingsToFilterButton;
        private System.Windows.Forms.ToolStripButton ClearFilterButton;
        private System.Windows.Forms.Label CompareTypeLabel;
        private System.Windows.Forms.ComboBox DataTypeCBB;
        private System.Windows.Forms.Label ScanTimeLabel;
        private System.Windows.Forms.Label AddressCount;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.ListView AddressListView;
        private System.Windows.Forms.ColumnHeader _AddressHeader;
        private System.Windows.Forms.ColumnHeader _ValueHeader;
        private System.Windows.Forms.ColumnHeader _LastValueHeader;
        private System.Windows.Forms.Button PageVisualizerButton;
        private System.Windows.Forms.Button StartSSAButton;
        private System.Windows.Forms.Button EndSSAButton;
        private System.Windows.Forms.Button TakeSnapshotButton;
        private System.Windows.Forms.ListBox SnapshotListBox;
        private System.Windows.Forms.Button SaveSnapshotButton;
        private System.Windows.Forms.Button RestoreSnapshotButton;
        private System.Windows.Forms.Button EndRSSAButton;
        private System.Windows.Forms.Button StartRSSAButton;
        private System.Windows.Forms.Button AddConstraintButton;
    }
}

