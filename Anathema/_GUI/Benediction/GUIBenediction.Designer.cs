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
            this.GUIFilterLabeler = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GUIFilter = new Anathema.GUIFilter();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.GUILabeler = new Anathema.GUILabeler();
            this.GUITable = new Anathema.GUITable();
            this.GUIDisplay = new Anathema.GUIDisplay();
            this.GUISnapshotManager = new Anathema.GUISnapshotManager();
            this.GUIFilterLabeler.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GUIFilterLabeler
            // 
            this.GUIFilterLabeler.Controls.Add(this.tabPage1);
            this.GUIFilterLabeler.Controls.Add(this.tabPage2);
            this.GUIFilterLabeler.Location = new System.Drawing.Point(3, 4);
            this.GUIFilterLabeler.Name = "GUIFilterLabeler";
            this.GUIFilterLabeler.SelectedIndex = 0;
            this.GUIFilterLabeler.Size = new System.Drawing.Size(388, 403);
            this.GUIFilterLabeler.TabIndex = 150;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.GUIFilter);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(380, 377);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Filter";
            // 
            // GUIFilter
            // 
            this.GUIFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GUIFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GUIFilter.Location = new System.Drawing.Point(3, 3);
            this.GUIFilter.Name = "GUIFilter";
            this.GUIFilter.Size = new System.Drawing.Size(374, 371);
            this.GUIFilter.TabIndex = 146;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.GUILabeler);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(408, 377);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Labeler";
            // 
            // GUILabeler
            // 
            this.GUILabeler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GUILabeler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GUILabeler.Location = new System.Drawing.Point(3, 3);
            this.GUILabeler.Name = "GUILabeler";
            this.GUILabeler.Size = new System.Drawing.Size(402, 371);
            this.GUILabeler.TabIndex = 147;
            // 
            // GUITable
            // 
            this.GUITable.Location = new System.Drawing.Point(393, 229);
            this.GUITable.Name = "GUITable";
            this.GUITable.Size = new System.Drawing.Size(406, 178);
            this.GUITable.TabIndex = 149;
            // 
            // GUIDisplay
            // 
            this.GUIDisplay.Location = new System.Drawing.Point(393, 4);
            this.GUIDisplay.Name = "GUIDisplay";
            this.GUIDisplay.Size = new System.Drawing.Size(272, 219);
            this.GUIDisplay.TabIndex = 148;
            // 
            // GUISnapshotManager
            // 
            this.GUISnapshotManager.Location = new System.Drawing.Point(671, 4);
            this.GUISnapshotManager.Name = "GUISnapshotManager";
            this.GUISnapshotManager.Size = new System.Drawing.Size(128, 219);
            this.GUISnapshotManager.TabIndex = 145;
            // 
            // GUIBenediction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GUIFilterLabeler);
            this.Controls.Add(this.GUITable);
            this.Controls.Add(this.GUIDisplay);
            this.Controls.Add(this.GUISnapshotManager);
            this.Name = "GUIBenediction";
            this.Size = new System.Drawing.Size(802, 410);
            this.Resize += new System.EventHandler(this.GUIBenediction_Resize);
            this.GUIFilterLabeler.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private GUISnapshotManager GUISnapshotManager;
        private GUIFilter GUIFilter;
        private GUILabeler GUILabeler;
        private GUIDisplay GUIDisplay;
        private GUITable GUITable;
        private System.Windows.Forms.TabControl GUIFilterLabeler;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
