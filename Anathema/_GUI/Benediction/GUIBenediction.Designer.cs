namespace Anathema
{
    partial class GUIBenediction
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
            this.GUITable = new Anathema.GUITable();
            this.GUIDisplay = new Anathema.GUIDisplay();
            this.GUILabeler = new Anathema.GUILabeler();
            this.GUIFilter = new Anathema.GUIFilter();
            this.GUISnapshotManager = new Anathema.GUISnapshotManager();
            this.SuspendLayout();
            // 
            // GUITable
            // 
            this.GUITable.Location = new System.Drawing.Point(249, 213);
            this.GUITable.Name = "GUITable";
            this.GUITable.Size = new System.Drawing.Size(550, 194);
            this.GUITable.TabIndex = 149;
            // 
            // GUIDisplay
            // 
            this.GUIDisplay.Location = new System.Drawing.Point(3, 213);
            this.GUIDisplay.Name = "GUIDisplay";
            this.GUIDisplay.Size = new System.Drawing.Size(240, 194);
            this.GUIDisplay.TabIndex = 148;
            // 
            // GUILabeler
            // 
            this.GUILabeler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GUILabeler.Location = new System.Drawing.Point(483, 4);
            this.GUILabeler.Name = "GUILabeler";
            this.GUILabeler.Size = new System.Drawing.Size(316, 203);
            this.GUILabeler.TabIndex = 147;
            // 
            // GUIFilter
            // 
            this.GUIFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GUIFilter.Location = new System.Drawing.Point(148, 4);
            this.GUIFilter.Name = "GUIFilter";
            this.GUIFilter.Size = new System.Drawing.Size(329, 203);
            this.GUIFilter.TabIndex = 146;
            // 
            // GUISnapshotManager
            // 
            this.GUISnapshotManager.Location = new System.Drawing.Point(3, 4);
            this.GUISnapshotManager.Name = "GUISnapshotManager";
            this.GUISnapshotManager.Size = new System.Drawing.Size(139, 203);
            this.GUISnapshotManager.TabIndex = 145;
            // 
            // GUIBenediction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GUITable);
            this.Controls.Add(this.GUIDisplay);
            this.Controls.Add(this.GUILabeler);
            this.Controls.Add(this.GUIFilter);
            this.Controls.Add(this.GUISnapshotManager);
            this.Name = "GUIBenediction";
            this.Size = new System.Drawing.Size(802, 410);
            this.Resize += new System.EventHandler(this.GUIBenediction_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private GUISnapshotManager GUISnapshotManager;
        private GUIFilter GUIFilter;
        private GUILabeler GUILabeler;
        private GUIDisplay GUIDisplay;
        private GUITable GUITable;
    }
}
