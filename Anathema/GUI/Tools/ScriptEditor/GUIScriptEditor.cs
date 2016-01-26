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

            ScriptEditorTextBox.SetKeywords(0, "and break do else elseif end false for function if" +
                " in local nil not or repeat return then true until while" +
                " _VERSION assert collectgarbage dofile error gcinfo loadfile loadstring" +
                " print rawget rawset require tonumber tostring type unpack" +
                " assert collectgarbage dofile error gcinfo loadfile loadstring" +
                " print rawget rawset require tonumber tostring type unpack" +
                " abs acos asin atan atan2 ceil cos deg exp" +
                " floor format frexp gsub ldexp log log10 max min mod rad random randomseed" +
                " sin sqrt strbyte strchar strfind strlen strlower strrep strsub strupper tan" +
                " string.byte string.char string.dump string.find string.len");

            ScriptEditorTextBox.SetKeywords(1, "rax rbx rcx rdx rbp rsi rdi rsp r8 r9 r10 r11 r12 r13 r14 r15 rip" +
                " eax ebx ecx edx ebp esi edi esp r8d r9d r10d r11d r12d r13d r14d r15d eip" +
                " ax bx cx dx bp si di sp r8w r9w r10w r11w r12w r13w r14w r15w" +
                " al bl cl dl bpl sil dil spl r8b r9b r10b r11b r12b r13b r14b r15b" +
                " ah bh ch dh" +
                " fpr0 fpr1 fpr2 fpr3 fpr4 fpr5 fpr6 fpr7 mmx0 mmx1 mmx2 mmx3 mmx4 mmx5 mmx6 mmx7" +
                " xmm0 xmm1 xmm2 xmm3 xmm4 xmm5 xmm6 xmm7 xmm8 xmm9 xmm10 xmm11 xmm12 xmm13 xmm14 xmm15");

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