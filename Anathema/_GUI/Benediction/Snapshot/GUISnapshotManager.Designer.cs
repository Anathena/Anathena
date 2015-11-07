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

        #region Component Designer generated code

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
            this.SnapshotTreeView.Size = new System.Drawing.Size(146, 187);
            this.SnapshotTreeView.TabIndex = 141;
            // 
            // GUISnapshotManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SnapshotTreeView);
            this.Name = "GUISnapshotManager";
            this.Size = new System.Drawing.Size(146, 187);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView SnapshotTreeView;
    }
}
