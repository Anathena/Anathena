namespace Anathema
{
    partial class GUIBenediction
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIBenediction));
            this.SnapshotTreeView = new System.Windows.Forms.TreeView();
            this.LabelerPanel = new System.Windows.Forms.Panel();
            this.AddressCount = new System.Windows.Forms.Label();
            this.AddressListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilterToolStrip = new System.Windows.Forms.ToolStrip();
            this.SearchSpaceAnalysisButton = new System.Windows.Forms.ToolStripButton();
            this.FiniteStateMachineButton = new System.Windows.Forms.ToolStripButton();
            this.ManualScanButton = new System.Windows.Forms.ToolStripButton();
            this.FilterPanel = new System.Windows.Forms.Panel();
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
            this.DisplayPanel = new System.Windows.Forms.Panel();
            this.FilterToolStrip.SuspendLayout();
            this.FilterPanel.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            this.DisplayPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SnapshotTreeView
            // 
            this.SnapshotTreeView.Location = new System.Drawing.Point(3, 4);
            this.SnapshotTreeView.Name = "SnapshotTreeView";
            this.SnapshotTreeView.Size = new System.Drawing.Size(139, 178);
            this.SnapshotTreeView.TabIndex = 140;
            // 
            // LabelerPanel
            // 
            this.LabelerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelerPanel.Location = new System.Drawing.Point(463, 4);
            this.LabelerPanel.Name = "LabelerPanel";
            this.LabelerPanel.Size = new System.Drawing.Size(314, 178);
            this.LabelerPanel.TabIndex = 144;
            // 
            // AddressCount
            // 
            this.AddressCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.AddressCount.Location = new System.Drawing.Point(0, 0);
            this.AddressCount.Name = "AddressCount";
            this.AddressCount.Size = new System.Drawing.Size(242, 17);
            this.AddressCount.TabIndex = 146;
            this.AddressCount.Text = "Items: 0";
            // 
            // AddressListView
            // 
            this.AddressListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.AddressListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.LabelHeader,
            this.columnHeader2});
            this.AddressListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressListView.FullRowSelect = true;
            this.AddressListView.Location = new System.Drawing.Point(0, 17);
            this.AddressListView.Name = "AddressListView";
            this.AddressListView.Size = new System.Drawing.Size(242, 175);
            this.AddressListView.TabIndex = 145;
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
            // FilterToolStrip
            // 
            this.FilterToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.FilterToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SearchSpaceAnalysisButton,
            this.FiniteStateMachineButton,
            this.ManualScanButton});
            this.FilterToolStrip.Location = new System.Drawing.Point(0, 0);
            this.FilterToolStrip.Name = "FilterToolStrip";
            this.FilterToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.FilterToolStrip.Size = new System.Drawing.Size(307, 25);
            this.FilterToolStrip.TabIndex = 142;
            this.FilterToolStrip.Text = "toolStrip1";
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
            // FilterPanel
            // 
            this.FilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilterPanel.Controls.Add(this.FilterToolStrip);
            this.FilterPanel.Location = new System.Drawing.Point(148, 3);
            this.FilterPanel.Name = "FilterPanel";
            this.FilterPanel.Size = new System.Drawing.Size(309, 179);
            this.FilterPanel.TabIndex = 143;
            // 
            // TableListView
            // 
            this.TableListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.TableListView.Location = new System.Drawing.Point(253, 188);
            this.TableListView.Name = "TableListView";
            this.TableListView.ShowGroups = false;
            this.TableListView.Size = new System.Drawing.Size(525, 219);
            this.TableListView.TabIndex = 141;
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
            this.MainToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
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
            this.MainToolStrip.Location = new System.Drawing.Point(0, 192);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainToolStrip.Size = new System.Drawing.Size(242, 25);
            this.MainToolStrip.TabIndex = 147;
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
            // 
            // DisplayPanel
            // 
            this.DisplayPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplayPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DisplayPanel.Controls.Add(this.AddressListView);
            this.DisplayPanel.Controls.Add(this.MainToolStrip);
            this.DisplayPanel.Controls.Add(this.AddressCount);
            this.DisplayPanel.Location = new System.Drawing.Point(3, 188);
            this.DisplayPanel.Name = "DisplayPanel";
            this.DisplayPanel.Size = new System.Drawing.Size(244, 219);
            this.DisplayPanel.TabIndex = 144;
            // 
            // GUIBenediction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DisplayPanel);
            this.Controls.Add(this.SnapshotTreeView);
            this.Controls.Add(this.LabelerPanel);
            this.Controls.Add(this.FilterPanel);
            this.Controls.Add(this.TableListView);
            this.Name = "GUIBenediction";
            this.Size = new System.Drawing.Size(781, 410);
            this.Resize += new System.EventHandler(this.GUIBenediction_Resize);
            this.FilterToolStrip.ResumeLayout(false);
            this.FilterToolStrip.PerformLayout();
            this.FilterPanel.ResumeLayout(false);
            this.FilterPanel.PerformLayout();
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.DisplayPanel.ResumeLayout(false);
            this.DisplayPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView SnapshotTreeView;
        private System.Windows.Forms.Panel LabelerPanel;
        private System.Windows.Forms.Label AddressCount;
        private System.Windows.Forms.ListView AddressListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader LabelHeader;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStrip FilterToolStrip;
        private System.Windows.Forms.ToolStripButton SearchSpaceAnalysisButton;
        private System.Windows.Forms.ToolStripButton FiniteStateMachineButton;
        private System.Windows.Forms.ToolStripButton ManualScanButton;
        private System.Windows.Forms.Panel FilterPanel;
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
        private System.Windows.Forms.ToolStripButton MemoryViewerButton;
        private System.Windows.Forms.Panel DisplayPanel;
    }
}
