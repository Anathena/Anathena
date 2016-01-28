namespace Anathema
{
    partial class GUIMemoryView
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
            this.HexEditorBox = new Be.Windows.Forms.HexBox();
            this.GUIToolStrip = new System.Windows.Forms.ToolStrip();
            this.ViewLabel = new System.Windows.Forms.ToolStripLabel();
            this.ViewStyleComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.QuickNavComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.RefreshNavigationButton = new System.Windows.Forms.ToolStripButton();
            this.GUIToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // HexEditorBox
            // 
            this.HexEditorBox.AllowDrop = true;
            this.HexEditorBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // 
            // 
            // 
            this.HexEditorBox.BuiltInContextMenu.CopyMenuItemText = "Copy ";
            this.HexEditorBox.BuiltInContextMenu.CutMenuItemText = "Cut ";
            this.HexEditorBox.BuiltInContextMenu.PasteMenuItemText = "Paste ";
            this.HexEditorBox.BuiltInContextMenu.SelectAllMenuItemText = "Select All ";
            this.HexEditorBox.ColumnInfoVisible = true;
            this.HexEditorBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HexEditorBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.HexEditorBox.HexCasing = Be.Windows.Forms.HexCasing.Lower;
            this.HexEditorBox.LineInfoVisible = true;
            this.HexEditorBox.Location = new System.Drawing.Point(0, 25);
            this.HexEditorBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.HexEditorBox.Name = "HexEditorBox";
            this.HexEditorBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.HexEditorBox.Size = new System.Drawing.Size(574, 262);
            this.HexEditorBox.StringViewVisible = true;
            this.HexEditorBox.TabIndex = 119;
            this.HexEditorBox.VScrollBarVisible = true;
            // 
            // GUIToolStrip
            // 
            this.GUIToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.GUIToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLabel,
            this.ViewStyleComboBox,
            this.QuickNavComboBox,
            this.RefreshNavigationButton});
            this.GUIToolStrip.Location = new System.Drawing.Point(0, 0);
            this.GUIToolStrip.Name = "GUIToolStrip";
            this.GUIToolStrip.Size = new System.Drawing.Size(574, 25);
            this.GUIToolStrip.TabIndex = 151;
            this.GUIToolStrip.Text = "Main Tool Strip";
            // 
            // ViewLabel
            // 
            this.ViewLabel.Name = "ViewLabel";
            this.ViewLabel.Size = new System.Drawing.Size(35, 22);
            this.ViewLabel.Text = "View:";
            // 
            // ViewStyleComboBox
            // 
            this.ViewStyleComboBox.Name = "ViewStyleComboBox";
            this.ViewStyleComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // QuickNavComboBox
            // 
            this.QuickNavComboBox.Name = "QuickNavComboBox";
            this.QuickNavComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // RefreshNavigationButton
            // 
            this.RefreshNavigationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshNavigationButton.Image = global::Anathema.Properties.Resources.Undo;
            this.RefreshNavigationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshNavigationButton.Name = "RefreshNavigationButton";
            this.RefreshNavigationButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshNavigationButton.Text = "Refresh";
            // 
            // GUIMemoryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 287);
            this.Controls.Add(this.HexEditorBox);
            this.Controls.Add(this.GUIToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIMemoryViewer";
            this.Text = "Memory View";
            this.GUIToolStrip.ResumeLayout(false);
            this.GUIToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Be.Windows.Forms.HexBox HexEditorBox;
        private System.Windows.Forms.ToolStrip GUIToolStrip;
        private System.Windows.Forms.ToolStripLabel ViewLabel;
        private System.Windows.Forms.ToolStripComboBox ViewStyleComboBox;
        private System.Windows.Forms.ToolStripComboBox QuickNavComboBox;
        private System.Windows.Forms.ToolStripButton RefreshNavigationButton;
    }
}