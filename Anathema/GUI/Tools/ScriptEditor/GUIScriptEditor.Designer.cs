namespace Anathema
{
    partial class GUIScriptEditor
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
            this.ScriptEditorRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ScriptEditorMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScriptEditorMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScriptEditorRichTextBox
            // 
            this.ScriptEditorRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptEditorRichTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScriptEditorRichTextBox.Location = new System.Drawing.Point(0, 24);
            this.ScriptEditorRichTextBox.Name = "ScriptEditorRichTextBox";
            this.ScriptEditorRichTextBox.Size = new System.Drawing.Size(339, 254);
            this.ScriptEditorRichTextBox.TabIndex = 0;
            this.ScriptEditorRichTextBox.Text = "";
            this.ScriptEditorRichTextBox.WordWrap = false;
            this.ScriptEditorRichTextBox.TextChanged += new System.EventHandler(this.ScriptEditorRichTextBox_TextChanged);
            // 
            // ScriptEditorMenuStrip
            // 
            this.ScriptEditorMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ViewToolStripMenuItem});
            this.ScriptEditorMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.ScriptEditorMenuStrip.Name = "ScriptEditorMenuStrip";
            this.ScriptEditorMenuStrip.Size = new System.Drawing.Size(339, 24);
            this.ScriptEditorMenuStrip.TabIndex = 1;
            this.ScriptEditorMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.SaveToTableToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.NewToolStripMenuItem.Text = "New";
            // 
            // SaveToTableToolStripMenuItem
            // 
            this.SaveToTableToolStripMenuItem.Name = "SaveToTableToolStripMenuItem";
            this.SaveToTableToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.SaveToTableToolStripMenuItem.Text = "Save to Table";
            this.SaveToTableToolStripMenuItem.Click += new System.EventHandler(this.SaveToTableToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.ViewToolStripMenuItem.Text = "View";
            // 
            // GUIScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 278);
            this.Controls.Add(this.ScriptEditorRichTextBox);
            this.Controls.Add(this.ScriptEditorMenuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.ScriptEditorMenuStrip;
            this.Name = "GUIScriptEditor";
            this.Text = "Script Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUIScriptEditor_FormClosing);
            this.ScriptEditorMenuStrip.ResumeLayout(false);
            this.ScriptEditorMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ScriptEditorRichTextBox;
        private System.Windows.Forms.MenuStrip ScriptEditorMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
    }
}