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
            this.SnapshotTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // SnapshotTreeView
            // 
            this.SnapshotTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SnapshotTreeView.Location = new System.Drawing.Point(0, 0);
            this.SnapshotTreeView.Name = "SnapshotTreeView";
            this.SnapshotTreeView.Size = new System.Drawing.Size(248, 199);
            this.SnapshotTreeView.TabIndex = 142;
            // 
            // DockableWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 199);
            this.Controls.Add(this.SnapshotTreeView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockableWindow";
            this.Text = "Snapshot Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView SnapshotTreeView;
    }
}