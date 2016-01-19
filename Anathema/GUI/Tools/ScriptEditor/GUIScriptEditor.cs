using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    public partial class GUIScriptEditor : DockContent, IScriptEditorView
    {
        private ScriptEditorPresenter ScriptEditorPresenter;
        private String DocumentTitle;

        public GUIScriptEditor()
        {
            InitializeComponent();

            DocumentTitle = this.Text;

            ScriptEditorPresenter = new ScriptEditorPresenter(this, ScriptEditor.GetInstance());
        }

        public void DisplayScript(String ScriptText)
        {
            ScriptEditorRichTextBox.Text = ScriptText;
            this.Text = DocumentTitle;

            if (!this.Visible)
                this.Show();
        }

        private void SaveChanges()
        {
            ScriptEditorPresenter.SaveScript(ScriptEditorRichTextBox.Text);
            this.Text = DocumentTitle;
        }

        #region Events

        private void SaveToTableToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            SaveChanges();
        }

        private void ScriptEditorRichTextBox_TextChanged(Object Sender, EventArgs E)
        {
            if (ScriptEditorPresenter.HasChanges(ScriptEditorRichTextBox.Text))
                this.Text = DocumentTitle + "*";
        }

        #endregion

        private void GUIScriptEditor_FormClosing(Object Sender, FormClosingEventArgs E)
        {
            if (!ScriptEditorPresenter.HasChanges(ScriptEditorRichTextBox.Text))
                return;

            DialogResult Result = MessageBox.Show("This script has not been saved, save the changes to the table before closing?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            switch(Result)
            {
                case DialogResult.Yes:
                    SaveChanges();
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    E.Cancel = true;
                    break;
            }
        }

    } // End class

} // End namespace