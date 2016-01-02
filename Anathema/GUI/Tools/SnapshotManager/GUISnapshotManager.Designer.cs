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
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewSnapshotButton = new System.Windows.Forms.ToolStripButton();
            this.SelectPreviousSnapshotButton = new System.Windows.Forms.ToolStripButton();
            this.SelectNextSnapshotButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearSnapshotsButton = new System.Windows.Forms.ToolStripButton();
            this.SnapshotListView = new System.Windows.Forms.ListView();
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSnapshotButton,
            this.SelectPreviousSnapshotButton,
            this.SelectNextSnapshotButton,
            this.ToolStripSeparator1,
            this.ClearSnapshotsButton});
            this.ScanToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ScanToolStrip.Location = new System.Drawing.Point(216, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(32, 199);
            this.ScanToolStrip.TabIndex = 151;
            // 
            // NewSnapshotButton
            // 
            this.NewSnapshotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewSnapshotButton.Image = ((System.Drawing.Image)(resources.GetObject("NewSnapshotButton.Image")));
            this.NewSnapshotButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewSnapshotButton.Name = "NewSnapshotButton";
            this.NewSnapshotButton.Size = new System.Drawing.Size(29, 20);
            this.NewSnapshotButton.Text = "New Snapshot";
            // 
            // SelectPreviousSnapshotButton
            // 
            this.SelectPreviousSnapshotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectPreviousSnapshotButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectPreviousSnapshotButton.Image")));
            this.SelectPreviousSnapshotButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectPreviousSnapshotButton.Name = "SelectPreviousSnapshotButton";
            this.SelectPreviousSnapshotButton.Size = new System.Drawing.Size(29, 20);
            this.SelectPreviousSnapshotButton.Text = "Select Previous";
            // 
            // SelectNextSnapshotButton
            // 
            this.SelectNextSnapshotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectNextSnapshotButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectNextSnapshotButton.Image")));
            this.SelectNextSnapshotButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectNextSnapshotButton.Name = "SelectNextSnapshotButton";
            this.SelectNextSnapshotButton.Size = new System.Drawing.Size(29, 20);
            this.SelectNextSnapshotButton.Text = "Select Next";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(29, 6);
            // 
            // ClearSnapshotsButton
            // 
            this.ClearSnapshotsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearSnapshotsButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearSnapshotsButton.Image")));
            this.ClearSnapshotsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearSnapshotsButton.Name = "ClearSnapshotsButton";
            this.ClearSnapshotsButton.Size = new System.Drawing.Size(29, 20);
            this.ClearSnapshotsButton.Text = "Open Table";
            this.ClearSnapshotsButton.ToolTipText = "Stop Tree Scan";
            this.ClearSnapshotsButton.Click += new System.EventHandler(this.ClearSnapshotsButton_Click);
            // 
            // SnapshotListView
            // 
            this.SnapshotListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SnapshotListView.Location = new System.Drawing.Point(0, 0);
            this.SnapshotListView.Name = "SnapshotListView";
            this.SnapshotListView.Size = new System.Drawing.Size(216, 199);
            this.SnapshotListView.TabIndex = 152;
            this.SnapshotListView.UseCompatibleStateImageBehavior = false;
            // 
            // GUISnapshotManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 199);
            this.Controls.Add(this.SnapshotListView);
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
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton NewSnapshotButton;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ClearSnapshotsButton;
        private System.Windows.Forms.ToolStripButton SelectPreviousSnapshotButton;
        private System.Windows.Forms.ToolStripButton SelectNextSnapshotButton;
        private System.Windows.Forms.ListView SnapshotListView;
    }
}