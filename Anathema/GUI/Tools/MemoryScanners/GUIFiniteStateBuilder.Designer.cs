namespace Anathema.GUI.Tools.MemoryScanners
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
            this.StateContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StartStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkValidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkInvalidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EndScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FSMBuilderPanel = new Anathema.FlickerFreePanel();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ScanToolStrip.SuspendLayout();
            this.StateContextMenuStrip.SuspendLayout();
            this.FSMBuilderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.AutoSize = false;
            this.ScanToolStrip.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DragModeButton,
            this.ScanOptionsToolStripDropDownButton});
            this.ScanToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ScanToolStrip.Location = new System.Drawing.Point(473, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(33, 301);
            this.ScanToolStrip.TabIndex = 163;
            this.ScanToolStrip.Text = "Tools";
            // 
            // DragModeButton
            // 
            this.DragModeButton.CheckOnClick = true;
            this.DragModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DragModeButton.Image = global::Anathema.Properties.Resources.AnathemaIcon;
            this.DragModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DragModeButton.Name = "DragModeButton";
            this.DragModeButton.Size = new System.Drawing.Size(31, 20);
            this.DragModeButton.Text = "Drag Mode";
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
            this.LessThanOrEqualToToolStripMenuItem});
            this.ScanOptionsToolStripDropDownButton.Image = global::Anathema.Properties.Resources.Changed;
            this.ScanOptionsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanOptionsToolStripDropDownButton.Name = "ScanOptionsToolStripDropDownButton";
            this.ScanOptionsToolStripDropDownButton.Size = new System.Drawing.Size(31, 20);
            this.ScanOptionsToolStripDropDownButton.Text = "toolStripDropDownButton1";
            // 
            // ChangedToolStripMenuItem
            // 
            this.ChangedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Changed;
            this.ChangedToolStripMenuItem.Name = "ChangedToolStripMenuItem";
            this.ChangedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.ChangedToolStripMenuItem.Text = "Changed";
            this.ChangedToolStripMenuItem.Click += new System.EventHandler(this.ChangedToolStripMenuItem_Click);
            // 
            // UnchangedToolStripMenuItem
            // 
            this.UnchangedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Unchanged;
            this.UnchangedToolStripMenuItem.Name = "UnchangedToolStripMenuItem";
            this.UnchangedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.UnchangedToolStripMenuItem.Text = "Unchanged";
            this.UnchangedToolStripMenuItem.Click += new System.EventHandler(this.UnchangedToolStripMenuItem_Click);
            // 
            // IncreasedToolStripMenuItem
            // 
            this.IncreasedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Increased;
            this.IncreasedToolStripMenuItem.Name = "IncreasedToolStripMenuItem";
            this.IncreasedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.IncreasedToolStripMenuItem.Text = "Increased";
            this.IncreasedToolStripMenuItem.Click += new System.EventHandler(this.IncreasedToolStripMenuItem_Click);
            // 
            // DecreasedToolStripMenuItem
            // 
            this.DecreasedToolStripMenuItem.Image = global::Anathema.Properties.Resources.Decreased;
            this.DecreasedToolStripMenuItem.Name = "DecreasedToolStripMenuItem";
            this.DecreasedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.DecreasedToolStripMenuItem.Text = "Decreased";
            this.DecreasedToolStripMenuItem.Click += new System.EventHandler(this.DecreasedToolStripMenuItem_Click);
            // 
            // EqualToToolStripMenuItem
            // 
            this.EqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.Equal;
            this.EqualToToolStripMenuItem.Name = "EqualToToolStripMenuItem";
            this.EqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.EqualToToolStripMenuItem.Text = "Equal to";
            this.EqualToToolStripMenuItem.Click += new System.EventHandler(this.EqualToToolStripMenuItem_Click);
            // 
            // NotEqualToToolStripMenuItem
            // 
            this.NotEqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.NotEqual;
            this.NotEqualToToolStripMenuItem.Name = "NotEqualToToolStripMenuItem";
            this.NotEqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.NotEqualToToolStripMenuItem.Text = "Not Equal to";
            this.NotEqualToToolStripMenuItem.Click += new System.EventHandler(this.NotEqualToToolStripMenuItem_Click);
            // 
            // IncreasedByToolStripMenuItem
            // 
            this.IncreasedByToolStripMenuItem.Image = global::Anathema.Properties.Resources.PlusX;
            this.IncreasedByToolStripMenuItem.Name = "IncreasedByToolStripMenuItem";
            this.IncreasedByToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.IncreasedByToolStripMenuItem.Text = "Increased by";
            this.IncreasedByToolStripMenuItem.Click += new System.EventHandler(this.IncreasedByToolStripMenuItem_Click);
            // 
            // DecreasedByToolStripMenuItem
            // 
            this.DecreasedByToolStripMenuItem.Image = global::Anathema.Properties.Resources.MinusX;
            this.DecreasedByToolStripMenuItem.Name = "DecreasedByToolStripMenuItem";
            this.DecreasedByToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.DecreasedByToolStripMenuItem.Text = "Decreased by";
            this.DecreasedByToolStripMenuItem.Click += new System.EventHandler(this.DecreasedByToolStripMenuItem_Click);
            // 
            // GreaterThanToolStripMenuItem
            // 
            this.GreaterThanToolStripMenuItem.Image = global::Anathema.Properties.Resources.GreaterThan;
            this.GreaterThanToolStripMenuItem.Name = "GreaterThanToolStripMenuItem";
            this.GreaterThanToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.GreaterThanToolStripMenuItem.Text = "Greater Than";
            this.GreaterThanToolStripMenuItem.Click += new System.EventHandler(this.GreaterThanToolStripMenuItem_Click);
            // 
            // GreaterThanOrEqualToToolStripMenuItem
            // 
            this.GreaterThanOrEqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.GreaterThanOrEqual;
            this.GreaterThanOrEqualToToolStripMenuItem.Name = "GreaterThanOrEqualToToolStripMenuItem";
            this.GreaterThanOrEqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.GreaterThanOrEqualToToolStripMenuItem.Text = "Greater Than or Equal to";
            this.GreaterThanOrEqualToToolStripMenuItem.Click += new System.EventHandler(this.GreaterThanOrEqualToToolStripMenuItem_Click);
            // 
            // LessThanToolStripMenuItem
            // 
            this.LessThanToolStripMenuItem.Image = global::Anathema.Properties.Resources.LessThan;
            this.LessThanToolStripMenuItem.Name = "LessThanToolStripMenuItem";
            this.LessThanToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.LessThanToolStripMenuItem.Text = "Less Than";
            this.LessThanToolStripMenuItem.Click += new System.EventHandler(this.LessThanToolStripMenuItem_Click);
            // 
            // LessThanOrEqualToToolStripMenuItem
            // 
            this.LessThanOrEqualToToolStripMenuItem.Image = global::Anathema.Properties.Resources.LessThanOrEqual;
            this.LessThanOrEqualToToolStripMenuItem.Name = "LessThanOrEqualToToolStripMenuItem";
            this.LessThanOrEqualToToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.LessThanOrEqualToToolStripMenuItem.Text = "Less Than or Equal to";
            this.LessThanOrEqualToToolStripMenuItem.Click += new System.EventHandler(this.LessThanOrEqualToToolStripMenuItem_Click);
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
            // FSMBuilderPanel
            // 
            this.FSMBuilderPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FSMBuilderPanel.Controls.Add(this.ValueTextBox);
            this.FSMBuilderPanel.Controls.Add(this.ValueTypeComboBox);
            this.FSMBuilderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FSMBuilderPanel.Location = new System.Drawing.Point(0, 0);
            this.FSMBuilderPanel.Name = "FSMBuilderPanel";
            this.FSMBuilderPanel.Size = new System.Drawing.Size(473, 301);
            this.FSMBuilderPanel.TabIndex = 164;
            this.FSMBuilderPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FSMBuilderPanel_Paint);
            this.FSMBuilderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseDown);
            this.FSMBuilderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseMove);
            this.FSMBuilderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseUp);
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTextBox.Location = new System.Drawing.Point(3, 4);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(367, 20);
            this.ValueTextBox.TabIndex = 160;
            this.ValueTextBox.TextChanged += new System.EventHandler(this.ValueTextBox_TextChanged);
            // 
            // ValueTypeComboBox
            // 
            this.ValueTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueTypeComboBox.FormattingEnabled = true;
            this.ValueTypeComboBox.Location = new System.Drawing.Point(376, 3);
            this.ValueTypeComboBox.Name = "ValueTypeComboBox";
            this.ValueTypeComboBox.Size = new System.Drawing.Size(94, 21);
            this.ValueTypeComboBox.TabIndex = 154;
            // 
            // GUIFiniteStateBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FSMBuilderPanel);
            this.Controls.Add(this.ScanToolStrip);
            this.Name = "GUIFiniteStateBuilder";
            this.Size = new System.Drawing.Size(506, 301);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.StateContextMenuStrip.ResumeLayout(false);
            this.FSMBuilderPanel.ResumeLayout(false);
            this.FSMBuilderPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton DragModeButton;
        private FlickerFreePanel FSMBuilderPanel;
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
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.ContextMenuStrip StateContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem StartStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NoEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkValidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkInvalidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EndScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteStateToolStripMenuItem;
    }
}
