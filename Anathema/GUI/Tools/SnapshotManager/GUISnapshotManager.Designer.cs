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
            this.UndoSnapshotButton = new System.Windows.Forms.ToolStripButton();
            this.RedoSnapshotButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearSnapshotsButton = new System.Windows.Forms.ToolStripButton();
            this.SnapshotListView = new Anathema.FlickerFreeListView();
            this.ScanMethodHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TimeStampHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScanToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSnapshotButton,
            this.UndoSnapshotButton,
            this.RedoSnapshotButton,
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
            this.NewSnapshotButton.Text = "New Snapshot";
            this.NewSnapshotButton.Click += new System.EventHandler(this.NewSnapshotButton_Click);
            // 
            // UndoSnapshotButton
            // 
            this.UndoSnapshotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoSnapshotButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoSnapshotButton.Image")));
            this.UndoSnapshotButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoSnapshotButton.Name = "UndoSnapshotButton";
            this.UndoSnapshotButton.Size = new System.Drawing.Size(21, 20);
            this.UndoSnapshotButton.Text = "Undo";
            this.UndoSnapshotButton.Click += new System.EventHandler(this.UndoSnapshotButton_Click);
            // 
            // RedoSnapshotButton
            // 
            this.RedoSnapshotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RedoSnapshotButton.Image = ((System.Drawing.Image)(resources.GetObject("RedoSnapshotButton.Image")));
            this.RedoSnapshotButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RedoSnapshotButton.Name = "RedoSnapshotButton";
            this.RedoSnapshotButton.Size = new System.Drawing.Size(21, 20);
            this.RedoSnapshotButton.Text = "Redo";
            this.RedoSnapshotButton.Click += new System.EventHandler(this.RedoSnapshotButton_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(21, 6);
            // 
            // ClearSnapshotsButton
            // 
            this.ClearSnapshotsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearSnapshotsButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearSnapshotsButton.Image")));
            this.ClearSnapshotsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearSnapshotsButton.Name = "ClearSnapshotsButton";
            this.ClearSnapshotsButton.Size = new System.Drawing.Size(21, 20);
            this.ClearSnapshotsButton.Text = "Clear Snapshots";
            this.ClearSnapshotsButton.Click += new System.EventHandler(this.ClearSnapshotsButton_Click);
            // 
            // SnapshotListView
            // 
            this.SnapshotListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ScanMethodHeader,
            this.SizeHeader,
            this.TimeStampHeader});
            this.SnapshotListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SnapshotListView.FullRowSelect = true;
            this.SnapshotListView.Location = new System.Drawing.Point(0, 0);
            this.SnapshotListView.Name = "SnapshotListView";
            this.SnapshotListView.Size = new System.Drawing.Size(224, 199);
            this.SnapshotListView.TabIndex = 152;
            this.SnapshotListView.UseCompatibleStateImageBehavior = false;
            this.SnapshotListView.View = System.Windows.Forms.View.Details;
            this.SnapshotListView.VirtualMode = true;
            this.SnapshotListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.SnapshotListView_RetrieveVirtualItem);
            // 
            // ScanMethodHeader
            // 
            this.ScanMethodHeader.Text = "Scan Method";
            this.ScanMethodHeader.Width = 86;
            // 
            // SizeHeader
            // 
            this.SizeHeader.Text = "Size";
            this.SizeHeader.Width = 45;
            // 
            // TimeStampHeader
            // 
            this.TimeStampHeader.Text = "Time Stamp";
            this.TimeStampHeader.Width = 76;
            // 
            // GUISnapshotManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 199);
            this.Controls.Add(this.SnapshotListView);
            this.Controls.Add(this.ScanToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUISnapshotManager";
            this.Text = "Snapshot Manager";
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
        private System.Windows.Forms.ToolStripButton UndoSnapshotButton;
        private System.Windows.Forms.ToolStripButton RedoSnapshotButton;
        private Anathema.FlickerFreeListView SnapshotListView;
        private System.Windows.Forms.ColumnHeader ScanMethodHeader;
        private System.Windows.Forms.ColumnHeader SizeHeader;
        private System.Windows.Forms.ColumnHeader TimeStampHeader;
    }
}