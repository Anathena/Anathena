namespace Anathema
{
    partial class ProcessSelector
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
            this.ProcessListView = new System.Windows.Forms.ListView();
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseProcessButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.AcceptProcessButton = new System.Windows.Forms.Button();
            this.GUIToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.RightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessListView
            // 
            this.ProcessListView.AutoArrange = false;
            this.ProcessListView.ContextMenuStrip = this.RightClickMenu;
            this.ProcessListView.FullRowSelect = true;
            this.ProcessListView.LabelWrap = false;
            this.ProcessListView.Location = new System.Drawing.Point(0, 0);
            this.ProcessListView.MultiSelect = false;
            this.ProcessListView.Name = "ProcessListView";
            this.ProcessListView.ShowGroups = false;
            this.ProcessListView.Size = new System.Drawing.Size(240, 241);
            this.ProcessListView.TabIndex = 20;
            this.ProcessListView.TileSize = new System.Drawing.Size(16, 16);
            this.ProcessListView.UseCompatibleStateImageBehavior = false;
            this.ProcessListView.View = System.Windows.Forms.View.SmallIcon;
            this.ProcessListView.DoubleClick += new System.EventHandler(this.ProcessListView_DoubleClick);
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenProcessToolStripMenuItem,
            this.RefreshToolStripMenuItem});
            this.RightClickMenu.Name = "RightClickMenu";
            this.RightClickMenu.Size = new System.Drawing.Size(147, 48);
            // 
            // OpenProcessToolStripMenuItem
            // 
            this.OpenProcessToolStripMenuItem.Name = "OpenProcessToolStripMenuItem";
            this.OpenProcessToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.OpenProcessToolStripMenuItem.Text = "Open Process";
            this.OpenProcessToolStripMenuItem.Click += new System.EventHandler(this.OpenProcessToolStripMenuItem_Click);
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.RefreshToolStripMenuItem.Text = "Refresh";
            this.RefreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // CloseProcessButton
            // 
            this.CloseProcessButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseProcessButton.Location = new System.Drawing.Point(160, 247);
            this.CloseProcessButton.Name = "CloseProcessButton";
            this.CloseProcessButton.Size = new System.Drawing.Size(68, 23);
            this.CloseProcessButton.TabIndex = 19;
            this.CloseProcessButton.Text = "Close";
            this.CloseProcessButton.UseVisualStyleBackColor = true;
            this.CloseProcessButton.Click += new System.EventHandler(this.CloseProcessSelect_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.BackColor = System.Drawing.SystemColors.Control;
            this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RefreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RefreshButton.Location = new System.Drawing.Point(86, 247);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(68, 23);
            this.RefreshButton.TabIndex = 18;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // AcceptProcessButton
            // 
            this.AcceptProcessButton.BackColor = System.Drawing.SystemColors.Control;
            this.AcceptProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AcceptProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptProcessButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AcceptProcessButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AcceptProcessButton.Location = new System.Drawing.Point(12, 247);
            this.AcceptProcessButton.Name = "AcceptProcessButton";
            this.AcceptProcessButton.Size = new System.Drawing.Size(68, 23);
            this.AcceptProcessButton.TabIndex = 17;
            this.AcceptProcessButton.Text = "Accept";
            this.AcceptProcessButton.UseVisualStyleBackColor = false;
            this.AcceptProcessButton.Click += new System.EventHandler(this.AcceptProcessButton_Click);
            // 
            // ProcessSelector
            // 
            this.AcceptButton = this.AcceptProcessButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseProcessButton;
            this.ClientSize = new System.Drawing.Size(240, 282);
            this.Controls.Add(this.ProcessListView);
            this.Controls.Add(this.CloseProcessButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.AcceptProcessButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProcessSelector";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProcessSelector";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProcessSelector_Load);
            this.RightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ProcessListView;
        private System.Windows.Forms.ContextMenuStrip RightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
        private System.Windows.Forms.Button CloseProcessButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button AcceptProcessButton;
        private System.Windows.Forms.ToolTip GUIToolTip;
    }
}