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
using ScintillaNET;

namespace Anathema
{
    public partial class GUIScriptEditor : DockContent, IScriptEditorView
    {
        private ScriptEditorPresenter ScriptEditorPresenter;
        private String DocumentTitle;

        public GUIScriptEditor()
        {
            InitializeComponent();

            ScriptEditorPresenter = new ScriptEditorPresenter(this, ScriptEditor.GetInstance());

            DocumentTitle = this.Text;

            InitializeLuaHighlighting();
        }

        private void InitializeLuaHighlighting()
        {
            ScriptEditorTextBox.Lexer = ScintillaNET.Lexer.Lua;
            ScriptEditorTextBox.Margins[0].Width = 16;

            ScriptEditorTextBox.SetKeywords(0, LuaEngine.LuaKeywords);
            ScriptEditorTextBox.SetKeywords(1, LuaEngine.AsmKeywords);

            ScriptEditorTextBox.Styles[Style.Lua.Comment].ForeColor = Color.Green;
            ScriptEditorTextBox.Styles[Style.Lua.CommentDoc].ForeColor = Color.Green;
            ScriptEditorTextBox.Styles[Style.Lua.CommentLine].ForeColor = Color.Green;

            ScriptEditorTextBox.Styles[Style.Lua.Character].ForeColor = Color.DarkOrange;
            ScriptEditorTextBox.Styles[Style.Lua.String].ForeColor = Color.DarkOrange;
            ScriptEditorTextBox.Styles[Style.Lua.StringEol].ForeColor = Color.DarkOrange;
            ScriptEditorTextBox.Styles[Style.Lua.LiteralString].ForeColor = Color.DarkOrange;

            ScriptEditorTextBox.Styles[Style.Lua.Identifier].ForeColor = Color.Black;
            ScriptEditorTextBox.Styles[Style.Lua.Label].ForeColor = Color.Blue;
            ScriptEditorTextBox.Styles[Style.Lua.Number].ForeColor = Color.Orange;
            ScriptEditorTextBox.Styles[Style.Lua.Operator].ForeColor = Color.Black;

            ScriptEditorTextBox.Styles[Style.Lua.Preprocessor].ForeColor = Color.Gray;

            ScriptEditorTextBox.Styles[Style.Lua.Word].ForeColor = Color.Blue;
            ScriptEditorTextBox.Styles[Style.Lua.Word2].ForeColor = Color.Orange;

        }

        public void DisplayScript(String ScriptText)
        {
            ScriptEditorTextBox.Text = ScriptText;
            this.Text = DocumentTitle;

            if (!this.Visible)
                this.Show();
        }

        private void SaveChanges()
        {
            ScriptEditorPresenter.SaveScript(ScriptEditorTextBox.Text);
            this.Text = DocumentTitle;
        }

        #region Events
        
        private void ScriptEditorTextBox_CharAdded(Object Sender, CharAddedEventArgs E)
        {
            Int32 Length = ScriptEditorTextBox.CurrentPosition - ScriptEditorTextBox.WordStartPosition(ScriptEditorTextBox.CurrentPosition, true);

            if (Length > 0)
            {
                ScriptEditorTextBox.AutoCShow(Length, LuaEngine.LuaKeywords);
            }
        }

        private void CodeInjectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ScriptEditorPresenter.InsertCodeInjectionTemplate();
        }

        private void SaveToTableToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            SaveChanges();
        }

        private void ScriptEditorRichTextBox_TextChanged(Object Sender, EventArgs E)
        {
            if (ScriptEditorPresenter.HasChanges(ScriptEditorTextBox.Text))
                this.Text = DocumentTitle + "*";
        }

        private void GUIScriptEditor_FormClosing(Object Sender, FormClosingEventArgs E)
        {
            if (!ScriptEditorPresenter.HasChanges(ScriptEditorTextBox.Text))
                return;

            DialogResult Result = MessageBox.Show("This script has not been saved, save the changes to the table before closing?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            switch (Result)
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

        #endregion

    } // End class

} // End namespace