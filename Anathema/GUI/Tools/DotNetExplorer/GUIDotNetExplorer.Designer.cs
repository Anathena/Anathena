namespace Anathema.GUI
{
    partial class GUIDotNetExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIDotNetExplorer));
            this.ObjectExplorer = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // ObjectExplorer
            // 
            this.ObjectExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectExplorer.Location = new System.Drawing.Point(0, 0);
            this.ObjectExplorer.Name = "ObjectExplorer";
            this.ObjectExplorer.Size = new System.Drawing.Size(284, 261);
            this.ObjectExplorer.TabIndex = 0;
            // 
            // GUIDotNetExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ObjectExplorer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIDotNetExplorer";
            this.Text = ".Net Explorer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ObjectExplorer;
    }
}