namespace Anathema
{
    partial class GUIFiniteStateScanner
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
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.ValueTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.StartScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.UnchangedButton = new System.Windows.Forms.ToolStripButton();
            this.ChangedButton = new System.Windows.Forms.ToolStripButton();
            this.IncreasedButton = new System.Windows.Forms.ToolStripButton();
            this.DecreasedButton = new System.Windows.Forms.ToolStripButton();
            this.NotEqualButton = new System.Windows.Forms.ToolStripButton();
            this.EqualButton = new System.Windows.Forms.ToolStripButton();
            this.GreaterThanButton = new System.Windows.Forms.ToolStripButton();
            this.LessThanButton = new System.Windows.Forms.ToolStripButton();
            this.IncreasedByXButton = new System.Windows.Forms.ToolStripButton();
            this.DecreasedByXButton = new System.Windows.Forms.ToolStripButton();
            this.StateContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NoEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkValidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkInvalidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EndScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FSMBuilderPanel = new Anathema.FlickerFreePanel();
            this.StopScanButton = new System.Windows.Forms.ToolStripButton();
            this.ScanToolStrip.SuspendLayout();
            this.StateContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTextBox.Location = new System.Drawing.Point(121, 28);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(336, 20);
            this.ValueTextBox.TabIndex = 159;
            // 
            // ValueTypeComboBox
            // 
            this.ValueTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueTypeComboBox.FormattingEnabled = true;
            this.ValueTypeComboBox.Location = new System.Drawing.Point(12, 28);
            this.ValueTypeComboBox.Name = "ValueTypeComboBox";
            this.ValueTypeComboBox.Size = new System.Drawing.Size(94, 21);
            this.ValueTypeComboBox.TabIndex = 160;
            this.ValueTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ValueTypeComboBox_SelectedIndexChanged);
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartScanButton,
            this.StopScanButton,
            this.toolStripSeparator7,
            this.UnchangedButton,
            this.ChangedButton,
            this.IncreasedButton,
            this.DecreasedButton,
            this.NotEqualButton,
            this.EqualButton,
            this.GreaterThanButton,
            this.LessThanButton,
            this.IncreasedByXButton,
            this.DecreasedByXButton});
            this.ScanToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(469, 25);
            this.ScanToolStrip.TabIndex = 162;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // StartScanButton
            // 
            this.StartScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartScanButton.Image = global::Anathema.Properties.Resources.RightArrow;
            this.StartScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartScanButton.Name = "StartScanButton";
            this.StartScanButton.Size = new System.Drawing.Size(23, 22);
            this.StartScanButton.Text = "Start Scan";
            this.StartScanButton.Click += new System.EventHandler(this.StartScanButton_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // UnchangedButton
            // 
            this.UnchangedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UnchangedButton.Image = global::Anathema.Properties.Resources.Unchanged;
            this.UnchangedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UnchangedButton.Name = "UnchangedButton";
            this.UnchangedButton.Size = new System.Drawing.Size(23, 22);
            this.UnchangedButton.Text = "Unchanged";
            this.UnchangedButton.Click += new System.EventHandler(this.UnchangedButton_Click);
            // 
            // ChangedButton
            // 
            this.ChangedButton.CheckOnClick = true;
            this.ChangedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ChangedButton.Image = global::Anathema.Properties.Resources.Changed;
            this.ChangedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChangedButton.Name = "ChangedButton";
            this.ChangedButton.Size = new System.Drawing.Size(23, 22);
            this.ChangedButton.Text = "Changed Value";
            this.ChangedButton.Click += new System.EventHandler(this.ChangedButton_Click);
            // 
            // IncreasedButton
            // 
            this.IncreasedButton.CheckOnClick = true;
            this.IncreasedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IncreasedButton.Image = global::Anathema.Properties.Resources.Increased;
            this.IncreasedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedButton.Name = "IncreasedButton";
            this.IncreasedButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedButton.Text = "Increased Value";
            this.IncreasedButton.Click += new System.EventHandler(this.IncreasedButton_Click);
            // 
            // DecreasedButton
            // 
            this.DecreasedButton.CheckOnClick = true;
            this.DecreasedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecreasedButton.Image = global::Anathema.Properties.Resources.Decreased;
            this.DecreasedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecreasedButton.Name = "DecreasedButton";
            this.DecreasedButton.Size = new System.Drawing.Size(23, 22);
            this.DecreasedButton.Text = "Decreased Value";
            this.DecreasedButton.Click += new System.EventHandler(this.DecreasedButton_Click);
            // 
            // NotEqualButton
            // 
            this.NotEqualButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NotEqualButton.Image = global::Anathema.Properties.Resources.NotEqual;
            this.NotEqualButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NotEqualButton.Name = "NotEqualButton";
            this.NotEqualButton.Size = new System.Drawing.Size(23, 22);
            this.NotEqualButton.Text = "Not Equal";
            this.NotEqualButton.Click += new System.EventHandler(this.NotEqualButton_Click);
            // 
            // EqualButton
            // 
            this.EqualButton.CheckOnClick = true;
            this.EqualButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EqualButton.Image = global::Anathema.Properties.Resources.Equal;
            this.EqualButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EqualButton.Name = "EqualButton";
            this.EqualButton.Size = new System.Drawing.Size(23, 22);
            this.EqualButton.Text = "Exact Value";
            this.EqualButton.Click += new System.EventHandler(this.EqualButton_Click);
            // 
            // GreaterThanButton
            // 
            this.GreaterThanButton.CheckOnClick = true;
            this.GreaterThanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GreaterThanButton.Image = global::Anathema.Properties.Resources.GreaterThan;
            this.GreaterThanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GreaterThanButton.Name = "GreaterThanButton";
            this.GreaterThanButton.Size = new System.Drawing.Size(23, 22);
            this.GreaterThanButton.Text = "Greater Than Value";
            this.GreaterThanButton.Click += new System.EventHandler(this.GreaterThanButton_Click);
            // 
            // LessThanButton
            // 
            this.LessThanButton.CheckOnClick = true;
            this.LessThanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LessThanButton.Image = global::Anathema.Properties.Resources.LessThan;
            this.LessThanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LessThanButton.Name = "LessThanButton";
            this.LessThanButton.Size = new System.Drawing.Size(23, 22);
            this.LessThanButton.Text = "Less Than Value";
            this.LessThanButton.Click += new System.EventHandler(this.LessThanButton_Click);
            // 
            // IncreasedByXButton
            // 
            this.IncreasedByXButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IncreasedByXButton.Image = global::Anathema.Properties.Resources.PlusX;
            this.IncreasedByXButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedByXButton.Name = "IncreasedByXButton";
            this.IncreasedByXButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedByXButton.Text = "Increase By X";
            this.IncreasedByXButton.Click += new System.EventHandler(this.IncreasedByXButton_Click);
            // 
            // DecreasedByXButton
            // 
            this.DecreasedByXButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecreasedByXButton.Image = global::Anathema.Properties.Resources.MinusX;
            this.DecreasedByXButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecreasedByXButton.Name = "DecreasedByXButton";
            this.DecreasedByXButton.Size = new System.Drawing.Size(23, 22);
            this.DecreasedByXButton.Text = "Decrease By X";
            this.DecreasedByXButton.Click += new System.EventHandler(this.DecreasedByXButton_Click);
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
            this.StateContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.StateContextMenuStrip_Opening);
            // 
            // NoEventToolStripMenuItem
            // 
            this.NoEventToolStripMenuItem.Name = "NoEventToolStripMenuItem";
            this.NoEventToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.NoEventToolStripMenuItem.Text = "No Event";
            this.NoEventToolStripMenuItem.Click += new System.EventHandler(this.NoEventToolStripMenuItem_Click);
            // 
            // StartStateToolStripMenuItem
            // 
            this.StartStateToolStripMenuItem.Name = "StartStateToolStripMenuItem";
            this.StartStateToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.StartStateToolStripMenuItem.Text = "Start State";
            this.StartStateToolStripMenuItem.Click += new System.EventHandler(this.StartStateToolStripMenuItem_Click);
            // 
            // MarkValidToolStripMenuItem
            // 
            this.MarkValidToolStripMenuItem.Name = "MarkValidToolStripMenuItem";
            this.MarkValidToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.MarkValidToolStripMenuItem.Text = "Mark Valid";
            this.MarkValidToolStripMenuItem.Click += new System.EventHandler(this.MarkValidToolStripMenuItem_Click);
            // 
            // MarkInvalidToolStripMenuItem
            // 
            this.MarkInvalidToolStripMenuItem.Name = "MarkInvalidToolStripMenuItem";
            this.MarkInvalidToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.MarkInvalidToolStripMenuItem.Text = "Mark Invalid";
            this.MarkInvalidToolStripMenuItem.Click += new System.EventHandler(this.MarkInvalidToolStripMenuItem_Click);
            // 
            // EndScanToolStripMenuItem
            // 
            this.EndScanToolStripMenuItem.Name = "EndScanToolStripMenuItem";
            this.EndScanToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.EndScanToolStripMenuItem.Text = "End Scan";
            this.EndScanToolStripMenuItem.Click += new System.EventHandler(this.EndScanToolStripMenuItem_Click);
            // 
            // DeleteStateToolStripMenuItem
            // 
            this.DeleteStateToolStripMenuItem.Name = "DeleteStateToolStripMenuItem";
            this.DeleteStateToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.DeleteStateToolStripMenuItem.Text = "Delete State";
            this.DeleteStateToolStripMenuItem.Click += new System.EventHandler(this.DeleteStateToolStripMenuItem_Click);
            // 
            // FSMBuilderPanel
            // 
            this.FSMBuilderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FSMBuilderPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FSMBuilderPanel.ContextMenuStrip = this.StateContextMenuStrip;
            this.FSMBuilderPanel.Location = new System.Drawing.Point(12, 55);
            this.FSMBuilderPanel.Name = "FSMBuilderPanel";
            this.FSMBuilderPanel.Size = new System.Drawing.Size(445, 203);
            this.FSMBuilderPanel.TabIndex = 163;
            this.FSMBuilderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseDown);
            this.FSMBuilderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseMove);
            this.FSMBuilderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FSMBuilderPanel_MouseUp);
            // 
            // StopScanButton
            // 
            this.StopScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopScanButton.Image = global::Anathema.Properties.Resources.Stop;
            this.StopScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopScanButton.Name = "StopScanButton";
            this.StopScanButton.Size = new System.Drawing.Size(23, 22);
            this.StopScanButton.Text = "Stop Scan";
            this.StopScanButton.Click += new System.EventHandler(this.StopScanButton_Click);
            // 
            // GUIFiniteStateScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 270);
            this.Controls.Add(this.FSMBuilderPanel);
            this.Controls.Add(this.ValueTextBox);
            this.Controls.Add(this.ValueTypeComboBox);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIFiniteStateScanner";
            this.Text = "Finite State Scanner";
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.StateContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.ComboBox ValueTypeComboBox;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton StartScanButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton UnchangedButton;
        private System.Windows.Forms.ToolStripButton ChangedButton;
        private System.Windows.Forms.ToolStripButton IncreasedButton;
        private System.Windows.Forms.ToolStripButton DecreasedButton;
        private System.Windows.Forms.ToolStripButton NotEqualButton;
        private System.Windows.Forms.ToolStripButton EqualButton;
        private System.Windows.Forms.ToolStripButton GreaterThanButton;
        private System.Windows.Forms.ToolStripButton LessThanButton;
        private System.Windows.Forms.ToolStripButton IncreasedByXButton;
        private System.Windows.Forms.ToolStripButton DecreasedByXButton;
        private FlickerFreePanel FSMBuilderPanel;
        private System.Windows.Forms.ContextMenuStrip StateContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem StartStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NoEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkValidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkInvalidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EndScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton StopScanButton;
    }
}