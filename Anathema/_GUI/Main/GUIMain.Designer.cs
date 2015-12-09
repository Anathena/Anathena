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
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessSelectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebuggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StateScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManualScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputCorrelatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SnapshotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.GUIToolStrip = new System.Windows.Forms.ToolStrip();
            this.ProcessSelectorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TargetProcessLabel = new System.Windows.Forms.ToolStripLabel();
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
            this.GUIMenuStrip.Size = new System.Drawing.Size(944, 24);
            this.GUIMenuStrip.TabIndex = 126;
            this.GUIMenuStrip.Text = "menuStrip1";
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
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProcessSelectorToolStripMenuItem,
            this.DebuggerToolStripMenuItem,
            this.ResultsToolStripMenuItem,
            this.FilterToolStripMenuItem,
            this.LabelerToolStripMenuItem,
            this.SnapshotsToolStripMenuItem,
            this.TableToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.ViewToolStripMenuItem.Text = "View";
            // 
            // ProcessSelectorToolStripMenuItem
            // 
            this.ProcessSelectorToolStripMenuItem.Name = "ProcessSelectorToolStripMenuItem";
            this.ProcessSelectorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ProcessSelectorToolStripMenuItem.Text = "Process Selector";
            this.ProcessSelectorToolStripMenuItem.Click += new System.EventHandler(this.ProcessSelectorToolStripMenuItem_Click);
            // 
            // DebuggerToolStripMenuItem
            // 
            this.DebuggerToolStripMenuItem.Name = "DebuggerToolStripMenuItem";
            this.DebuggerToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.DebuggerToolStripMenuItem.Text = "Debugger";
            this.DebuggerToolStripMenuItem.Click += new System.EventHandler(this.DebuggerToolStripMenuItem_Click);
            // 
            // ResultsToolStripMenuItem
            // 
            this.ResultsToolStripMenuItem.Name = "ResultsToolStripMenuItem";
            this.ResultsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ResultsToolStripMenuItem.Text = "Results";
            this.ResultsToolStripMenuItem.Click += new System.EventHandler(this.ResultsToolStripMenuItem_Click);
            // 
            // FilterToolStripMenuItem
            // 
            this.FilterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StateScannerToolStripMenuItem,
            this.ManualScannerToolStripMenuItem,
            this.TreeScannerToolStripMenuItem});
            this.FilterToolStripMenuItem.Name = "FilterToolStripMenuItem";
            this.FilterToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.FilterToolStripMenuItem.Text = "Filter";
            // 
            // StateScannerToolStripMenuItem
            // 
            this.StateScannerToolStripMenuItem.Name = "StateScannerToolStripMenuItem";
            this.StateScannerToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.StateScannerToolStripMenuItem.Text = "State Scanner";
            this.StateScannerToolStripMenuItem.Click += new System.EventHandler(this.StateScannerToolStripMenuItem_Click);
            // 
            // ManualScannerToolStripMenuItem
            // 
            this.ManualScannerToolStripMenuItem.Name = "ManualScannerToolStripMenuItem";
            this.ManualScannerToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ManualScannerToolStripMenuItem.Text = "Manual Scanner";
            this.ManualScannerToolStripMenuItem.Click += new System.EventHandler(this.ManualScannerToolStripMenuItem_Click);
            // 
            // TreeScannerToolStripMenuItem
            // 
            this.TreeScannerToolStripMenuItem.Name = "TreeScannerToolStripMenuItem";
            this.TreeScannerToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.TreeScannerToolStripMenuItem.Text = "Tree Scanner";
            this.TreeScannerToolStripMenuItem.Click += new System.EventHandler(this.TreeScannerToolStripMenuItem_Click);
            // 
            // LabelerToolStripMenuItem
            // 
            this.LabelerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InputCorrelatorToolStripMenuItem,
            this.ChangeCounterToolStripMenuItem});
            this.LabelerToolStripMenuItem.Name = "LabelerToolStripMenuItem";
            this.LabelerToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.LabelerToolStripMenuItem.Text = "Labeler";
            // 
            // InputCorrelatorToolStripMenuItem
            // 
            this.InputCorrelatorToolStripMenuItem.Name = "InputCorrelatorToolStripMenuItem";
            this.InputCorrelatorToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.InputCorrelatorToolStripMenuItem.Text = "Input Correlator";
            this.InputCorrelatorToolStripMenuItem.Click += new System.EventHandler(this.InputCorrelatorToolStripMenuItem_Click);
            // 
            // ChangeCounterToolStripMenuItem
            // 
            this.ChangeCounterToolStripMenuItem.Name = "ChangeCounterToolStripMenuItem";
            this.ChangeCounterToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.ChangeCounterToolStripMenuItem.Text = "Change Counter";
            this.ChangeCounterToolStripMenuItem.Click += new System.EventHandler(this.ChangeCounterToolStripMenuItem_Click);
            // 
            // SnapshotsToolStripMenuItem
            // 
            this.SnapshotsToolStripMenuItem.Name = "SnapshotsToolStripMenuItem";
            this.SnapshotsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.SnapshotsToolStripMenuItem.Text = "Snapshots";
            this.SnapshotsToolStripMenuItem.Click += new System.EventHandler(this.SnapshotsToolStripMenuItem_Click);
            // 
            // TableToolStripMenuItem
            // 
            this.TableToolStripMenuItem.Name = "TableToolStripMenuItem";
            this.TableToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.TableToolStripMenuItem.Text = "Table";
            this.TableToolStripMenuItem.Click += new System.EventHandler(this.TableToolStripMenuItem_Click);
            // 
            // ContentPanel
            // 
            this.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.DockBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ContentPanel.Location = new System.Drawing.Point(0, 49);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(944, 552);
            this.ContentPanel.TabIndex = 145;
            // 
            // GUIToolStrip
            // 
            this.GUIToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.GUIToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProcessSelectorButton,
            this.TargetProcessLabel,
            this.toolStripSeparator1});
            this.GUIToolStrip.Location = new System.Drawing.Point(0, 24);
            this.GUIToolStrip.Name = "GUIToolStrip";
            this.GUIToolStrip.Size = new System.Drawing.Size(944, 25);
            this.GUIToolStrip.TabIndex = 148;
            this.GUIToolStrip.Text = "toolStrip1";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // TargetProcessLabel
            // 
            this.TargetProcessLabel.Name = "TargetProcessLabel";
            this.TargetProcessLabel.Size = new System.Drawing.Size(113, 22);
            this.TargetProcessLabel.Text = "No Process Selected";
            // 
            // GUIMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 601);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.GUIToolStrip);
            this.Controls.Add(this.GUIMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "GUIMain";
            this.Text = "Anathema";
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
        private System.Windows.Forms.ToolStripMenuItem FilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LabelerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SnapshotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StateScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ManualScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TreeScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputCorrelatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeCounterToolStripMenuItem;
        private System.Windows.Forms.ToolStrip GUIToolStrip;
        private System.Windows.Forms.ToolStripButton ProcessSelectorButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ProcessSelectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel TargetProcessLabel;
    }
}

