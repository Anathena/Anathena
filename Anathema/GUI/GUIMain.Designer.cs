namespace Anathema
{
    partial class GUIMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIMain));
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
            this.MemoryViewerButton = new System.Windows.Forms.ToolStripButton();
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
            this.WriteTimer = new System.Windows.Forms.Timer(this.components);
            this.AddressListViewRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addItemToTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showItemsAsSignedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectedProcessLabel = new System.Windows.Forms.Label();
            this.FilterToolStrip = new System.Windows.Forms.ToolStrip();
            this.SelectProcessButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SearchSpaceAnalysisButton = new System.Windows.Forms.ToolStripButton();
            this.FiniteStateMachineButton = new System.Windows.Forms.ToolStripButton();
            this.ManualScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.InputCorrelatorButton = new System.Windows.Forms.ToolStripButton();
            this.FilterPanel = new System.Windows.Forms.Panel();
            this.AddressCount = new System.Windows.Forms.Label();
            this.AddressListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelerPanel = new System.Windows.Forms.Panel();
            this.UpdateFoundTimer = new System.Windows.Forms.Timer(this.components);
            this.GUIUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SnapshotTreeView = new System.Windows.Forms.TreeView();
            this.MainToolStrip.SuspendLayout();
            this.GUIMenuStrip.SuspendLayout();
            this.AddressListViewRightClickMenu.SuspendLayout();
            this.FilterToolStrip.SuspendLayout();
            this.SuspendLayout();
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
            this.TableListView.Location = new System.Drawing.Point(269, 313);
            this.TableListView.Name = "TableListView";
            this.TableListView.ShowGroups = false;
            this.TableListView.Size = new System.Drawing.Size(522, 250);
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
            this.MainToolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.UndoTableDeleteButton,
            this.MemoryViewerButton});
            this.MainToolStrip.Location = new System.Drawing.Point(59, 538);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainToolStrip.Size = new System.Drawing.Size(199, 25);
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
            // MemoryViewerButton
            // 
            this.MemoryViewerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MemoryViewerButton.Image = ((System.Drawing.Image)(resources.GetObject("MemoryViewerButton.Image")));
            this.MemoryViewerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MemoryViewerButton.Name = "MemoryViewerButton";
            this.MemoryViewerButton.Size = new System.Drawing.Size(23, 22);
            this.MemoryViewerButton.Text = "Memory Viewer";
            this.MemoryViewerButton.Click += new System.EventHandler(this.MemoryViewerButton_Click);
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
            this.GUIMenuStrip.Size = new System.Drawing.Size(798, 24);
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
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
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
            this.SelectedProcessLabel.Location = new System.Drawing.Point(203, 5);
            this.SelectedProcessLabel.Name = "SelectedProcessLabel";
            this.SelectedProcessLabel.Size = new System.Drawing.Size(107, 13);
            this.SelectedProcessLabel.TabIndex = 129;
            this.SelectedProcessLabel.Text = "No Process Selected";
            this.SelectedProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FilterToolStrip
            // 
            this.FilterToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.FilterToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.FilterToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectProcessButton,
            this.toolStripSeparator2,
            this.SearchSpaceAnalysisButton,
            this.FiniteStateMachineButton,
            this.ManualScanButton,
            this.toolStripSeparator1,
            this.InputCorrelatorButton});
            this.FilterToolStrip.Location = new System.Drawing.Point(153, 31);
            this.FilterToolStrip.Name = "FilterToolStrip";
            this.FilterToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.FilterToolStrip.Size = new System.Drawing.Size(130, 25);
            this.FilterToolStrip.TabIndex = 121;
            this.FilterToolStrip.Text = "toolStrip1";
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // SearchSpaceAnalysisButton
            // 
            this.SearchSpaceAnalysisButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchSpaceAnalysisButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchSpaceAnalysisButton.Image")));
            this.SearchSpaceAnalysisButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchSpaceAnalysisButton.Name = "SearchSpaceAnalysisButton";
            this.SearchSpaceAnalysisButton.Size = new System.Drawing.Size(23, 22);
            this.SearchSpaceAnalysisButton.Text = "Search Space Analysis";
            this.SearchSpaceAnalysisButton.ToolTipText = "Hash Trees";
            this.SearchSpaceAnalysisButton.Click += new System.EventHandler(this.SearchSpaceAnalysisButton_Click);
            // 
            // FiniteStateMachineButton
            // 
            this.FiniteStateMachineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FiniteStateMachineButton.Image = ((System.Drawing.Image)(resources.GetObject("FiniteStateMachineButton.Image")));
            this.FiniteStateMachineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FiniteStateMachineButton.Name = "FiniteStateMachineButton";
            this.FiniteStateMachineButton.Size = new System.Drawing.Size(23, 22);
            this.FiniteStateMachineButton.Text = "Finite State Scanner";
            this.FiniteStateMachineButton.ToolTipText = "Finite State Scanner";
            this.FiniteStateMachineButton.Click += new System.EventHandler(this.FiniteStateMachineButton_Click);
            // 
            // ManualScanButton
            // 
            this.ManualScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ManualScanButton.Image = ((System.Drawing.Image)(resources.GetObject("ManualScanButton.Image")));
            this.ManualScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ManualScanButton.Name = "ManualScanButton";
            this.ManualScanButton.Size = new System.Drawing.Size(23, 22);
            this.ManualScanButton.Text = "Manual Scan";
            this.ManualScanButton.ToolTipText = "Manual Scan";
            this.ManualScanButton.Click += new System.EventHandler(this.ManualScanButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // InputCorrelatorButton
            // 
            this.InputCorrelatorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InputCorrelatorButton.Image = ((System.Drawing.Image)(resources.GetObject("InputCorrelatorButton.Image")));
            this.InputCorrelatorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InputCorrelatorButton.Name = "InputCorrelatorButton";
            this.InputCorrelatorButton.Size = new System.Drawing.Size(23, 22);
            this.InputCorrelatorButton.Text = "Input Correlator";
            this.InputCorrelatorButton.Click += new System.EventHandler(this.InputCorrelatorButton_Click);
            // 
            // FilterPanel
            // 
            this.FilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilterPanel.Location = new System.Drawing.Point(153, 59);
            this.FilterPanel.Name = "FilterPanel";
            this.FilterPanel.Size = new System.Drawing.Size(313, 248);
            this.FilterPanel.TabIndex = 137;
            // 
            // AddressCount
            // 
            this.AddressCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddressCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressCount.Location = new System.Drawing.Point(12, 313);
            this.AddressCount.Name = "AddressCount";
            this.AddressCount.Size = new System.Drawing.Size(246, 17);
            this.AddressCount.TabIndex = 139;
            this.AddressCount.Text = "Items: 0";
            // 
            // AddressListView
            // 
            this.AddressListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.AddressListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddressListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.LabelHeader,
            this.columnHeader2});
            this.AddressListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressListView.FullRowSelect = true;
            this.AddressListView.Location = new System.Drawing.Point(12, 333);
            this.AddressListView.Name = "AddressListView";
            this.AddressListView.Size = new System.Drawing.Size(246, 202);
            this.AddressListView.TabIndex = 138;
            this.AddressListView.UseCompatibleStateImageBehavior = false;
            this.AddressListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 86;
            // 
            // LabelHeader
            // 
            this.LabelHeader.Text = "Label";
            this.LabelHeader.Width = 86;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            // 
            // LabelerPanel
            // 
            this.LabelerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelerPanel.Location = new System.Drawing.Point(472, 59);
            this.LabelerPanel.Name = "LabelerPanel";
            this.LabelerPanel.Size = new System.Drawing.Size(314, 248);
            this.LabelerPanel.TabIndex = 138;
            // 
            // UpdateFoundTimer
            // 
            this.UpdateFoundTimer.Enabled = true;
            this.UpdateFoundTimer.Interval = 513;
            // 
            // GUIUpdateTimer
            // 
            this.GUIUpdateTimer.Enabled = true;
            this.GUIUpdateTimer.Tick += new System.EventHandler(this.GUIUpdateTimer_Tick);
            // 
            // SnapshotTreeView
            // 
            this.SnapshotTreeView.Location = new System.Drawing.Point(12, 59);
            this.SnapshotTreeView.Name = "SnapshotTreeView";
            this.SnapshotTreeView.Size = new System.Drawing.Size(135, 248);
            this.SnapshotTreeView.TabIndex = 0;
            // 
            // GUIAnathema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 573);
            this.Controls.Add(this.SnapshotTreeView);
            this.Controls.Add(this.LabelerPanel);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.AddressCount);
            this.Controls.Add(this.AddressListView);
            this.Controls.Add(this.FilterToolStrip);
            this.Controls.Add(this.FilterPanel);
            this.Controls.Add(this.SelectedProcessLabel);
            this.Controls.Add(this.GUIMenuStrip);
            this.Controls.Add(this.TableListView);
            this.Name = "GUIAnathema";
            this.Text = "Anathema";
            this.Load += new System.EventHandler(this.GUIMain_Load);
            this.Resize += new System.EventHandler(this.GUIMain_Resize);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.GUIMenuStrip.ResumeLayout(false);
            this.GUIMenuStrip.PerformLayout();
            this.AddressListViewRightClickMenu.ResumeLayout(false);
            this.FilterToolStrip.ResumeLayout(false);
            this.FilterToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Timer WriteTimer;
        private System.Windows.Forms.ContextMenuStrip AddressListViewRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem addItemToTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showItemsAsSignedToolStripMenuItem;
        private System.Windows.Forms.Label SelectedProcessLabel;
        private System.Windows.Forms.ToolStrip FilterToolStrip;
        private System.Windows.Forms.ToolStripButton SelectProcessButton;
        private System.Windows.Forms.ToolStripButton SearchSpaceAnalysisButton;
        private System.Windows.Forms.ToolStripButton FiniteStateMachineButton;
        private System.Windows.Forms.Panel FilterPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ManualScanButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton InputCorrelatorButton;
        private System.Windows.Forms.Label AddressCount;
        private System.Windows.Forms.ListView AddressListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader LabelHeader;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripButton MemoryViewerButton;
        private System.Windows.Forms.Panel LabelerPanel;
        private System.Windows.Forms.Timer UpdateFoundTimer;
        private System.Windows.Forms.Timer GUIUpdateTimer;
        private System.Windows.Forms.TreeView SnapshotTreeView;
    }
}

