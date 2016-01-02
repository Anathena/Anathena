namespace Anathema
{
    partial class GUISnapshotManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUISnapshotManager));
            this.SnapshotTreeView = new System.Windows.Forms.TreeView();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewSnapshotButton = new System.Windows.Forms.ToolStripButton();
            this.ClearSnapshotsButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SnapshotTreeView
            // 
            this.SnapshotTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SnapshotTreeView.Location = new System.Drawing.Point(0, 0);
            this.SnapshotTreeView.Name = "SnapshotTreeView";
            this.SnapshotTreeView.Size = new System.Drawing.Size(224, 199);
            this.SnapshotTreeView.TabIndex = 142;
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSnapshotButton,
            this.toolStripButton1,
            this.toolStripButton2,
            this.ToolStripSeparator1,
            this.ClearSnapshotsButton});
            this.ScanToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ScanToolStrip.Location = new System.Drawing.Point(224, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(24, 199);
            this.ScanToolStrip.TabIndex = 151;
            // 
            // NewSnapshotButton
            // 
            this.NewSnapshotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewSnapshotButton.Image = ((System.Drawing.Image)(resources.GetObject("NewSnapshotButton.Image")));
            this.NewSnapshotButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewSnapshotButton.Name = "NewSnapshotButton";
            this.NewSnapshotButton.Size = new System.Drawing.Size(21, 20);
            this.NewSnapshotButton.Text = "Save Table";
            // 
            // ClearSnapshotsButton
            // 
            this.ClearSnapshotsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearSnapshotsButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearSnapshotsButton.Image")));
            this.ClearSnapshotsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearSnapshotsButton.Name = "ClearSnapshotsButton";
            this.ClearSnapshotsButton.Size = new System.Drawing.Size(21, 20);
            this.ClearSnapshotsButton.Text = "Open Table";
            this.ClearSnapshotsButton.ToolTipText = "Stop Tree Scan";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(21, 6);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 20);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(29, 20);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // GUISnapshotManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 199);
            this.Controls.Add(this.SnapshotTreeView);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUISnapshotManager";
            this.Text = "Snapshot Manager";
            this.Load += new System.EventHandler(this.GUISnapshotManager_Load);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView SnapshotTreeView;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton NewSnapshotButton;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ClearSnapshotsButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}