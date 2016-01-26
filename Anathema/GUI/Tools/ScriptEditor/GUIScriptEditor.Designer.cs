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
            this.ScriptEditorMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CodeInjectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScriptEditorTextBox = new ScintillaNET.Scintilla();
            this.ScriptEditorMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScriptEditorMenuStrip
            // 
            this.ScriptEditorMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.TemplatesToolStripMenuItem});
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
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.NewToolStripMenuItem.Text = "New";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
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
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddEntryToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.ViewToolStripMenuItem.Text = "View";
            // 
            // AddEntryToolStripMenuItem
            // 
            this.AddEntryToolStripMenuItem.Name = "AddEntryToolStripMenuItem";
            this.AddEntryToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.AddEntryToolStripMenuItem.Text = "Add Entry";
            // 
            // TemplatesToolStripMenuItem
            // 
            this.TemplatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CodeInjectionToolStripMenuItem});
            this.TemplatesToolStripMenuItem.Name = "TemplatesToolStripMenuItem";
            this.TemplatesToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.TemplatesToolStripMenuItem.Text = "Templates";
            // 
            // CodeInjectionToolStripMenuItem
            // 
            this.CodeInjectionToolStripMenuItem.Name = "CodeInjectionToolStripMenuItem";
            this.CodeInjectionToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.CodeInjectionToolStripMenuItem.Text = "Code Injection";
            this.CodeInjectionToolStripMenuItem.Click += new System.EventHandler(this.CodeInjectionToolStripMenuItem_Click);
            // 
            // ScriptEditorTextBox
            // 
            this.ScriptEditorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptEditorTextBox.Location = new System.Drawing.Point(0, 24);
            this.ScriptEditorTextBox.Name = "ScriptEditorTextBox";
            this.ScriptEditorTextBox.Size = new System.Drawing.Size(339, 254);
            this.ScriptEditorTextBox.TabIndex = 2;
            // 
            // GUIScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 278);
            this.Controls.Add(this.ScriptEditorTextBox);
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
        private System.Windows.Forms.MenuStrip ScriptEditorMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TemplatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CodeInjectionToolStripMenuItem;
        private ScintillaNET.Scintilla ScriptEditorTextBox;
    }
}