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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIMain));
            this.GUIMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessSelectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChunkScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManualScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputCorrelatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FiniteStateScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.PointerScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.LabelThresholderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScriptEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebuggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CodeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MemoryViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SnapshotManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.GUIToolStrip = new System.Windows.Forms.ToolStrip();
            this.ProcessSelectorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.NewScanButton = new System.Windows.Forms.ToolStripButton();
            this.UndoScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ProcessTitleLabel = new System.Windows.Forms.ToolStripLabel();
            this.CollectValuesButton = new System.Windows.Forms.ToolStripButton();
            this.GUIMenuStrip.SuspendLayout();
            this.GUIToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GUIMenuStrip
            // 
            this.GUIMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.GUIMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ViewToolStripMenuItem});
            this.GUIMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.GUIMenuStrip.Name = "GUIMenuStrip";
            this.GUIMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.GUIMenuStrip.Size = new System.Drawing.Size(732, 24);
            this.GUIMenuStrip.TabIndex = 126;
            this.GUIMenuStrip.Text = "Main Menu Strip";
            this.GUIMenuStrip.MenuActivate += new System.EventHandler(this.GUIMenuStrip_MenuActivate);
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.dieToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // dieToolStripMenuItem
            // 
            this.dieToolStripMenuItem.Name = "dieToolStripMenuItem";
            this.dieToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.dieToolStripMenuItem.Text = "Save";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.SettingsToolStripMenuItem.Text = "Settings";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProcessSelectorToolStripMenuItem,
            this.ScansToolStripMenuItem,
            this.ScriptEditorToolStripMenuItem,
            this.DebuggerToolStripMenuItem,
            this.TableToolStripMenuItem,
            this.ResultsToolStripMenuItem,
            this.SnapshotManagerToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.ViewToolStripMenuItem.Text = "View";
            // 
            // ProcessSelectorToolStripMenuItem
            // 
            this.ProcessSelectorToolStripMenuItem.Name = "ProcessSelectorToolStripMenuItem";
            this.ProcessSelectorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ProcessSelectorToolStripMenuItem.Text = "Process Selector";
            this.ProcessSelectorToolStripMenuItem.Click += new System.EventHandler(this.ProcessSelectorToolStripMenuItem_Click);
            // 
            // ScansToolStripMenuItem
            // 
            this.ScansToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChunkScannerToolStripMenuItem,
            this.TreeScannerToolStripMenuItem,
            this.ManualScannerToolStripMenuItem,
            this.InputCorrelatorToolStripMenuItem,
            this.ChangeCounterToolStripMenuItem,
            this.FiniteStateScannerToolStripMenuItem,
            this.toolStripSeparator4,
            this.PointerScannerToolStripMenuItem,
            this.toolStripSeparator2,
            this.LabelThresholderToolStripMenuItem});
            this.ScansToolStripMenuItem.Name = "ScansToolStripMenuItem";
            this.ScansToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ScansToolStripMenuItem.Text = "Scans";
            // 
            // ChunkScannerToolStripMenuItem
            // 
            this.ChunkScannerToolStripMenuItem.Name = "ChunkScannerToolStripMenuItem";
            this.ChunkScannerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.ChunkScannerToolStripMenuItem.Text = "Chunk Scanner";
            this.ChunkScannerToolStripMenuItem.Click += new System.EventHandler(this.ChunkScannerToolStripMenuItem_Click);
            // 
            // TreeScannerToolStripMenuItem
            // 
            this.TreeScannerToolStripMenuItem.Name = "TreeScannerToolStripMenuItem";
            this.TreeScannerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.TreeScannerToolStripMenuItem.Text = "Tree Scanner";
            this.TreeScannerToolStripMenuItem.Click += new System.EventHandler(this.TreeScannerToolStripMenuItem_Click);
            // 
            // ManualScannerToolStripMenuItem
            // 
            this.ManualScannerToolStripMenuItem.Name = "ManualScannerToolStripMenuItem";
            this.ManualScannerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.ManualScannerToolStripMenuItem.Text = "Manual Scanner";
            this.ManualScannerToolStripMenuItem.Click += new System.EventHandler(this.ManualScannerToolStripMenuItem_Click);
            // 
            // InputCorrelatorToolStripMenuItem
            // 
            this.InputCorrelatorToolStripMenuItem.Name = "InputCorrelatorToolStripMenuItem";
            this.InputCorrelatorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.InputCorrelatorToolStripMenuItem.Text = "Input Correlator";
            this.InputCorrelatorToolStripMenuItem.Click += new System.EventHandler(this.InputCorrelatorToolStripMenuItem_Click);
            // 
            // ChangeCounterToolStripMenuItem
            // 
            this.ChangeCounterToolStripMenuItem.Name = "ChangeCounterToolStripMenuItem";
            this.ChangeCounterToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.ChangeCounterToolStripMenuItem.Text = "Change Counter";
            this.ChangeCounterToolStripMenuItem.Click += new System.EventHandler(this.ChangeCounterToolStripMenuItem_Click);
            // 
            // FiniteStateScannerToolStripMenuItem
            // 
            this.FiniteStateScannerToolStripMenuItem.Name = "FiniteStateScannerToolStripMenuItem";
            this.FiniteStateScannerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.FiniteStateScannerToolStripMenuItem.Text = "Finite State Scanner";
            this.FiniteStateScannerToolStripMenuItem.Click += new System.EventHandler(this.FiniteStateScannerToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(174, 6);
            // 
            // PointerScannerToolStripMenuItem
            // 
            this.PointerScannerToolStripMenuItem.Name = "PointerScannerToolStripMenuItem";
            this.PointerScannerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.PointerScannerToolStripMenuItem.Text = "Pointer Scanner";
            this.PointerScannerToolStripMenuItem.Click += new System.EventHandler(this.PointerScannerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(174, 6);
            // 
            // LabelThresholderToolStripMenuItem
            // 
            this.LabelThresholderToolStripMenuItem.Name = "LabelThresholderToolStripMenuItem";
            this.LabelThresholderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.LabelThresholderToolStripMenuItem.Text = "Label Thresholder";
            this.LabelThresholderToolStripMenuItem.Click += new System.EventHandler(this.LabelThresholderToolStripMenuItem_Click);
            // 
            // ScriptEditorToolStripMenuItem
            // 
            this.ScriptEditorToolStripMenuItem.Name = "ScriptEditorToolStripMenuItem";
            this.ScriptEditorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ScriptEditorToolStripMenuItem.Text = "Script Editor";
            this.ScriptEditorToolStripMenuItem.Click += new System.EventHandler(this.ScriptEditorToolStripMenuItem_Click);
            // 
            // DebuggerToolStripMenuItem
            // 
            this.DebuggerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CodeViewToolStripMenuItem,
            this.MemoryViewToolStripMenuItem});
            this.DebuggerToolStripMenuItem.Name = "DebuggerToolStripMenuItem";
            this.DebuggerToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.DebuggerToolStripMenuItem.Text = "Debugger";
            // 
            // CodeViewToolStripMenuItem
            // 
            this.CodeViewToolStripMenuItem.Name = "CodeViewToolStripMenuItem";
            this.CodeViewToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.CodeViewToolStripMenuItem.Text = "Code View";
            this.CodeViewToolStripMenuItem.Click += new System.EventHandler(this.CodeViewToolStripMenuItem_Click);
            // 
            // MemoryViewToolStripMenuItem
            // 
            this.MemoryViewToolStripMenuItem.Name = "MemoryViewToolStripMenuItem";
            this.MemoryViewToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.MemoryViewToolStripMenuItem.Text = "Memory View";
            this.MemoryViewToolStripMenuItem.Click += new System.EventHandler(this.MemoryViewToolStripMenuItem_Click);
            // 
            // TableToolStripMenuItem
            // 
            this.TableToolStripMenuItem.Name = "TableToolStripMenuItem";
            this.TableToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.TableToolStripMenuItem.Text = "Table";
            this.TableToolStripMenuItem.Click += new System.EventHandler(this.TableToolStripMenuItem_Click);
            // 
            // ResultsToolStripMenuItem
            // 
            this.ResultsToolStripMenuItem.Name = "ResultsToolStripMenuItem";
            this.ResultsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ResultsToolStripMenuItem.Text = "Results";
            this.ResultsToolStripMenuItem.Click += new System.EventHandler(this.ResultsToolStripMenuItem_Click);
            // 
            // SnapshotManagerToolStripMenuItem
            // 
            this.SnapshotManagerToolStripMenuItem.Name = "SnapshotManagerToolStripMenuItem";
            this.SnapshotManagerToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SnapshotManagerToolStripMenuItem.Text = "Snapshot Manager";
            this.SnapshotManagerToolStripMenuItem.Click += new System.EventHandler(this.SnapshotManagerToolStripMenuItem_Click);
            // 
            // ContentPanel
            // 
            this.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.DockBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ContentPanel.Location = new System.Drawing.Point(0, 49);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(732, 492);
            this.ContentPanel.TabIndex = 145;
            // 
            // GUIToolStrip
            // 
            this.GUIToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.GUIToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProcessSelectorButton,
            this.toolStripSeparator3,
            this.CollectValuesButton,
            this.NewScanButton,
            this.UndoScanButton,
            this.toolStripSeparator1,
            this.ProcessTitleLabel});
            this.GUIToolStrip.Location = new System.Drawing.Point(0, 24);
            this.GUIToolStrip.Name = "GUIToolStrip";
            this.GUIToolStrip.Size = new System.Drawing.Size(732, 25);
            this.GUIToolStrip.TabIndex = 148;
            this.GUIToolStrip.Text = "Main Tool Strip";
            // 
            // ProcessSelectorButton
            // 
            this.ProcessSelectorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ProcessSelectorButton.Image = ((System.Drawing.Image)(resources.GetObject("ProcessSelectorButton.Image")));
            this.ProcessSelectorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProcessSelectorButton.Name = "ProcessSelectorButton";
            this.ProcessSelectorButton.Size = new System.Drawing.Size(23, 22);
            this.ProcessSelectorButton.Text = "SelectProcessButton";
            this.ProcessSelectorButton.Click += new System.EventHandler(this.ProcessSelectorButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // NewScanButton
            // 
            this.NewScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewScanButton.Image = global::Anathema.Properties.Resources.New;
            this.NewScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewScanButton.Name = "NewScanButton";
            this.NewScanButton.Size = new System.Drawing.Size(23, 22);
            this.NewScanButton.Text = "New Scan";
            this.NewScanButton.Click += new System.EventHandler(this.NewScanButton_Click);
            // 
            // UndoScanButton
            // 
            this.UndoScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoScanButton.Image = global::Anathema.Properties.Resources.Undo;
            this.UndoScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoScanButton.Name = "UndoScanButton";
            this.UndoScanButton.Size = new System.Drawing.Size(23, 22);
            this.UndoScanButton.Text = "Undo Scan";
            this.UndoScanButton.Click += new System.EventHandler(this.UndoScanButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ProcessTitleLabel
            // 
            this.ProcessTitleLabel.Name = "ProcessTitleLabel";
            this.ProcessTitleLabel.Size = new System.Drawing.Size(113, 22);
            this.ProcessTitleLabel.Text = "No Process Selected";
            // 
            // CollectValuesButton
            // 
            this.CollectValuesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollectValuesButton.Image = global::Anathema.Properties.Resources.BenedictionIcon;
            this.CollectValuesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollectValuesButton.Name = "CollectValuesButton";
            this.CollectValuesButton.Size = new System.Drawing.Size(23, 22);
            this.CollectValuesButton.Text = "Collect Values";
            this.CollectValuesButton.Click += new System.EventHandler(this.CollectValuesButton_Click);
            // 
            // GUIMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 541);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.GUIToolStrip);
            this.Controls.Add(this.GUIMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "GUIMain";
            this.Text = "Anathema";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUIMain_FormClosing);
            this.GUIMenuStrip.ResumeLayout(false);
            this.GUIMenuStrip.PerformLayout();
            this.GUIToolStrip.ResumeLayout(false);
            this.GUIToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip GUIMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel ContentPanel;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DebuggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SnapshotManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FiniteStateScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ManualScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TreeScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStrip GUIToolStrip;
        private System.Windows.Forms.ToolStripButton ProcessSelectorButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ProcessSelectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel ProcessTitleLabel;
        private System.Windows.Forms.ToolStripMenuItem ChunkScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton NewScanButton;
        private System.Windows.Forms.ToolStripButton UndoScanButton;
        private System.Windows.Forms.ToolStripMenuItem ChangeCounterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputCorrelatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScriptEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem LabelThresholderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem CodeViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MemoryViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem PointerScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton CollectValuesButton;
    }
}

