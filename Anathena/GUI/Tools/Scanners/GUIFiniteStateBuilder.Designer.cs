using Anathena.GUI.CustomControls.Panels;

namespace Anathena.GUI.Tools.Scanners
{
    partial class GUIFiniteStateBuilder
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
            this.components = new System.ComponentModel.Container();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.DragModeButton = new System.Windows.Forms.ToolStripButton();
            this.StateContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StartStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkValidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkInvalidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EndScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.GUIConstraintEditor = new GUIConstraintEditor();
            this.FSMBuilderPanel = new FlickerFreePanel();
            this.ScanToolStrip.SuspendLayout();
            this.StateContextMenuStrip.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.AutoSize = false;
            this.ScanToolStrip.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DragModeButton});
            this.ScanToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ScanToolStrip.Location = new System.Drawing.Point(397, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(33, 333);
            this.ScanToolStrip.TabIndex = 163;
            this.ScanToolStrip.Text = "Tools";
            // 
            // DragModeButton
            // 
            this.DragModeButton.CheckOnClick = true;
            this.DragModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DragModeButton.Image = global::Anathena.Properties.Resources.Valid;
            this.DragModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DragModeButton.Name = "DragModeButton";
            this.DragModeButton.Size = new System.Drawing.Size(31, 20);
            this.DragModeButton.Text = "Drag Mode";
            // 
            // StateContextMenuStrip
            // 
            this.StateContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartStateToolStripMenuItem,
            this.NoEventToolStripMenuItem,
            this.MarkValidToolStripMenuItem,
            this.MarkInvalidToolStripMenuItem,
            this.EndScanToolStripMenuItem,
            this.DeleteStateToolStripMenuItem});
            this.StateContextMenuStrip.Name = "StateMenuStrip";
            this.StateContextMenuStrip.Size = new System.Drawing.Size(140, 136);
            // 
            // StartStateToolStripMenuItem
            // 
            this.StartStateToolStripMenuItem.Name = "StartStateToolStripMenuItem";
            this.StartStateToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.StartStateToolStripMenuItem.Text = "Start State";
            // 
            // NoEventToolStripMenuItem
            // 
            this.NoEventToolStripMenuItem.Name = "NoEventToolStripMenuItem";
            this.NoEventToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.NoEventToolStripMenuItem.Text = "No Event";
            // 
            // MarkValidToolStripMenuItem
            // 
            this.MarkValidToolStripMenuItem.Name = "MarkValidToolStripMenuItem";
            this.MarkValidToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.MarkValidToolStripMenuItem.Text = "Mark Valid";
            // 
            // MarkInvalidToolStripMenuItem
            // 
            this.MarkInvalidToolStripMenuItem.Name = "MarkInvalidToolStripMenuItem";
            this.MarkInvalidToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.MarkInvalidToolStripMenuItem.Text = "Mark Invalid";
            // 
            // EndScanToolStripMenuItem
            // 
            this.EndScanToolStripMenuItem.Name = "EndScanToolStripMenuItem";
            this.EndScanToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.EndScanToolStripMenuItem.Text = "End Scan";
            // 
            // DeleteStateToolStripMenuItem
            // 
            this.DeleteStateToolStripMenuItem.Name = "DeleteStateToolStripMenuItem";
            this.DeleteStateToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.DeleteStateToolStripMenuItem.Text = "Delete State";
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.GUIConstraintEditor);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ControlPanel.Location = new System.Drawing.Point(0, 233);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(397, 100);
            this.ControlPanel.TabIndex = 161;
            // 
            // GUIConstraintEditor
            // 
            this.GUIConstraintEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GUIConstraintEditor.Location = new System.Drawing.Point(0, 0);
            this.GUIConstraintEditor.Name = "GUIConstraintEditor";
            this.GUIConstraintEditor.Size = new System.Drawing.Size(397, 100);
            this.GUIConstraintEditor.TabIndex = 0;
            // 
            // FSMBuilderPanel
            // 
            this.FSMBuilderPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FSMBuilderPanel.ContextMenuStrip = this.StateContextMenuStrip;
            this.FSMBuilderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FSMBuilderPanel.Location = new System.Drawing.Point(0, 0);
            this.FSMBuilderPanel.Name = "FSMBuilderPanel";
            this.FSMBuilderPanel.Size = new System.Drawing.Size(430, 333);
            this.FSMBuilderPanel.TabIndex = 164;
            this.FSMBuilderPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FSMBuilderPanel_Paint);
            this.FSMBuilderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseDown);
            this.FSMBuilderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseMove);
            this.FSMBuilderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseUp);
            // 
            // GUIFiniteStateBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.ScanToolStrip);
            this.Controls.Add(this.FSMBuilderPanel);
            this.Name = "GUIFiniteStateBuilder";
            this.Size = new System.Drawing.Size(430, 333);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.StateContextMenuStrip.ResumeLayout(false);
            this.ControlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton DragModeButton;
        private FlickerFreePanel FSMBuilderPanel;
        private System.Windows.Forms.ContextMenuStrip StateContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem StartStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NoEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkValidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkInvalidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EndScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteStateToolStripMenuItem;
        private System.Windows.Forms.Panel ControlPanel;
        private GUIConstraintEditor GUIConstraintEditor;
    }
}
