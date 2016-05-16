using Anathema.Source.Utils;
using Anathema.User.UserScriptEditor;
using Anathema.Utils;
using Anathema.Utils.LUA;
using Anathema.Utils.MVP;
using ScintillaNET;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema
{
    public partial class GUIScriptEditor : DockContent, IScriptEditorView
    {
        private static Scintilla StaticEditor = new Scintilla();
        private ScriptEditorPresenter ScriptEditorPresenter;
        private Object AccessLock;

        private String DocumentTitle;

        public GUIScriptEditor()
        {
            InitializeComponent();

            ScriptEditorPresenter = new ScriptEditorPresenter(this, ScriptEditor.GetInstance());
            AccessLock = new Object();

            DocumentTitle = this.Text;

            FixScintilla();
            InitializeScriptEditor();

            ScriptEditorTextBox.TextChanged += ScriptEditorTextBox_TextChanged;
            ScriptEditorTextBox.CharAdded += ScriptEditorTextBox_CharAdded;
        }

        private void FixScintilla()
        {
            // Work around to a fatal bug in scintilla where the handle of the editor is changed, and scintilla does not expect it.
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    this.Controls.Remove(ScriptEditorTextBox);
                    ScriptEditorTextBox = StaticEditor;
                    ScriptEditorTextBox.Dock = DockStyle.Fill;
                });

                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    this.Controls.Add(ScriptEditorTextBox);
                    this.Controls.SetChildIndex(ScriptEditorTextBox, 0);
                });
            }

            InitializeScriptEditor();
        }

        private void InitializeScriptEditor()
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    ScriptEditorTextBox.Lexer = ScintillaNET.Lexer.Lua;
                    ScriptEditorTextBox.IndentWidth = 4;
                    ScriptEditorTextBox.Margins[0].Width = 16;
                    ScriptEditorTextBox.AutoCIgnoreCase = true;

                    ScriptEditorTextBox.Styles[Style.Lua.Comment].ForeColor = Color.DarkGreen;
                    ScriptEditorTextBox.Styles[Style.Lua.CommentDoc].ForeColor = Color.DarkGreen;
                    ScriptEditorTextBox.Styles[Style.Lua.CommentLine].ForeColor = Color.DarkGreen;
                    ScriptEditorTextBox.Styles[Style.Lua.Preprocessor].ForeColor = Color.DarkGreen;

                    ScriptEditorTextBox.Styles[Style.Lua.Character].ForeColor = Color.Firebrick;
                    ScriptEditorTextBox.Styles[Style.Lua.String].ForeColor = Color.Firebrick;
                    ScriptEditorTextBox.Styles[Style.Lua.StringEol].ForeColor = Color.Firebrick;
                    ScriptEditorTextBox.Styles[Style.Lua.LiteralString].ForeColor = Color.Firebrick;

                    ScriptEditorTextBox.Styles[Style.Lua.Number].ForeColor = Color.Black;
                    ScriptEditorTextBox.Styles[Style.Lua.Operator].ForeColor = Color.Black;
                    ScriptEditorTextBox.Styles[Style.Lua.Identifier].ForeColor = Color.Black;

                    ScriptEditorTextBox.Styles[Style.Lua.Label].ForeColor = Color.Blue;
                });
            }
            EnableLuaHighlighting();
        }

        private void EnableLuaHighlighting()
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    // Reprioritize keywords to handle keyword conflicts from LUA and ASM (like 'and')
                    ScriptEditorTextBox.SetKeywords(0, LuaKeywordManager.LuaKeywords);
                    ScriptEditorTextBox.SetKeywords(1, LuaKeywordManager.AnathemaKeywords);
                    ScriptEditorTextBox.SetKeywords(2, LuaKeywordManager.AsmRegisterKeywords);
                    ScriptEditorTextBox.SetKeywords(3, LuaKeywordManager.AsmInstructionKeywords);

                    ScriptEditorTextBox.Styles[Style.Lua.Word].ForeColor = Color.Blue;
                    ScriptEditorTextBox.Styles[Style.Lua.Word2].ForeColor = Color.CadetBlue;
                    ScriptEditorTextBox.Styles[Style.Lua.Word3].ForeColor = Color.Firebrick;
                    ScriptEditorTextBox.Styles[Style.Lua.Word4].ForeColor = Color.Blue;
                });
            }
        }

        private void EnableAsmHighlighting()
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    // Reprioritize keywords to handle keyword conflicts from LUA and ASM (like 'and')
                    ScriptEditorTextBox.SetKeywords(0, LuaKeywordManager.AsmRegisterKeywords);
                    ScriptEditorTextBox.SetKeywords(1, LuaKeywordManager.AsmInstructionKeywords);
                    ScriptEditorTextBox.SetKeywords(2, LuaKeywordManager.LuaKeywords);
                    ScriptEditorTextBox.SetKeywords(3, LuaKeywordManager.AnathemaKeywords);

                    ScriptEditorTextBox.Styles[Style.Lua.Word].ForeColor = Color.Firebrick;
                    ScriptEditorTextBox.Styles[Style.Lua.Word2].ForeColor = Color.Blue;
                    ScriptEditorTextBox.Styles[Style.Lua.Word3].ForeColor = Color.Blue;
                    ScriptEditorTextBox.Styles[Style.Lua.Word4].ForeColor = Color.CadetBlue;
                });
            }
        }

        public void OpenScript(String ScriptText)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
            {
                ScriptEditorTextBox.Text = ScriptText;
            });

            ControlThreadingHelper.InvokeControlAction(this, () =>
            {
                this.Text = DocumentTitle;

                if (!this.Visible)
                    this.Show();
            });
        }

        public void SetScriptText(String ScriptText)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
            {
                ScriptEditorTextBox.Text = ScriptText;
            });
        }

        private void SaveChanges()
        {
            using (TimedLock.Lock(AccessLock))
            {
                ScriptEditorPresenter.SaveScript(ScriptEditorTextBox.Text);
                this.Text = DocumentTitle;
            }
        }

        private Boolean AskSaveChanges()
        {
            // Check if there are even changes to save
            if (!ScriptEditorPresenter.HasChanges(ScriptEditorTextBox.Text))
                return false;

            DialogResult Result = MessageBoxEx.Show(this, "This script has not been saved, save the changes to the table before closing?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            switch (Result)
            {
                case DialogResult.Yes:
                    SaveChanges();
                    return false;
                case DialogResult.No:
                    return false;
                case DialogResult.Cancel:
                    break;
            }

            // User wishes to cancel
            return true;
        }

        private void CleanUp()
        {
            using (TimedLock.Lock(AccessLock))
            {
                // Remove the static editor so that it does not get disposed
                if (this.Controls.Contains(StaticEditor))
                    this.Controls.Remove(StaticEditor);
                StaticEditor.ClearAll();
            }
        }

        #region Events

        private void ScriptEditorTextBox_CharAdded(Object Sender, CharAddedEventArgs E)
        {
            Boolean AsmMode = false;
            Int32 Length;

            using (TimedLock.Lock(AccessLock))
            {
                if (E.Char == '\n')
                {
                    Int32 Indentation = (ScriptEditorTextBox.Lines[ScriptEditorTextBox.CurrentLine == 0 ? 0 : ScriptEditorTextBox.CurrentLine - 1].Indentation) / ScriptEditorTextBox.IndentWidth;

                    ScriptEditorTextBox.InsertText(ScriptEditorTextBox.CurrentPosition, String.Join("", Enumerable.Repeat("\t", Indentation)));
                    ScriptEditorTextBox.CurrentPosition = Math.Min(ScriptEditorTextBox.CurrentPosition + Indentation, ScriptEditorTextBox.TextLength);
                    ScriptEditorTextBox.AnchorPosition = ScriptEditorTextBox.CurrentPosition;
                }

                Length = ScriptEditorTextBox.CurrentPosition - ScriptEditorTextBox.WordStartPosition(ScriptEditorTextBox.CurrentPosition, true);

                if (Length <= 0)
                    return;

                ScriptEditorTextBox.SearchFlags = SearchFlags.None;

                // Search for closest preceeding [asm] tag to determine if we are writing inside the tag
                ScriptEditorTextBox.TargetStart = 0;
                ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.CurrentPosition;
                Int32 StartTagPosition = -1;
                while (true)
                {
                    Int32 Next = ScriptEditorTextBox.SearchInTarget("[asm]");

                    if (Next == -1)
                        break;

                    StartTagPosition = Next;

                    ScriptEditorTextBox.TargetStart = ScriptEditorTextBox.TargetEnd;
                    ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.CurrentPosition;
                }

                // Search for next [asm] tag to ensure the [/asm] does not pass it
                ScriptEditorTextBox.TargetStart = ScriptEditorTextBox.CurrentPosition;
                ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.TextLength;
                Int32 NextTagPosition = ScriptEditorTextBox.SearchInTarget("[asm]");

                // Search for next [/asm] tag and see if we are inside it
                ScriptEditorTextBox.TargetStart = ScriptEditorTextBox.CurrentPosition;
                ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.TextLength;

                if (StartTagPosition != -1)
                {
                    Int32 EndTagPositon = ScriptEditorTextBox.SearchInTarget("[/asm]");
                    if (EndTagPositon != -1 && (NextTagPosition == -1 || EndTagPositon < NextTagPosition))
                        AsmMode = true;
                }
            }

            if (AsmMode)
            {
                EnableAsmHighlighting();
                //ScriptEditorTextBox.AutoCShow(Length, LuaKeywordManager.AllAsmKeywords);
            }
            else
            {
                EnableLuaHighlighting();
                ScriptEditorTextBox.AutoCShow(Length, LuaKeywordManager.AllLuaKeywords);
            }

        }

        private void ScriptEditorTextBox_TextChanged(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (ScriptEditorPresenter.HasChanges(ScriptEditorTextBox.Text))
                    this.Text = DocumentTitle + "*";
                else
                    this.Text = DocumentTitle;
            }
        }

        private void CodeInjectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ScriptEditorPresenter.InsertCodeInjectionTemplate();
        }

        protected override Boolean ProcessCmdKey(ref Message Message, Keys Keys)
        {
            if (Keys == (Keys.Control | Keys.S))
            {
                SaveChanges();
                return true;
            }
            else if (Keys == (Keys.Control | Keys.W))
            {
                Close();
                return true;
            }

            /* ScintillaNet will insert garbage with certain command keys, this filters those out */
            else if (Keys == (Keys.Control | Keys.B)) return true;
            else if (Keys == (Keys.Control | Keys.D)) return true;
            else if (Keys == (Keys.Control | Keys.E)) return true;
            else if (Keys == (Keys.Control | Keys.F)) return true;
            else if (Keys == (Keys.Control | Keys.G)) return true;
            else if (Keys == (Keys.Control | Keys.H)) return true;
            else if (Keys == (Keys.Control | Keys.K)) return true;
            else if (Keys == (Keys.Control | Keys.N)) return true;
            else if (Keys == (Keys.Control | Keys.O)) return true;
            else if (Keys == (Keys.Control | Keys.P)) return true;
            else if (Keys == (Keys.Control | Keys.Q)) return true;
            else if (Keys == (Keys.Control | Keys.R)) return true;

            return base.ProcessCmdKey(ref Message, Keys);
        }

        private void NewToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            // Ask to save changes or cancel
            if (AskSaveChanges())
                return;

            ScriptEditorPresenter.OpenNewScript();
        }

        private void SaveToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            SaveChanges();
        }

        private void GUIScriptEditor_FormClosing(Object Sender, FormClosingEventArgs E)
        {
            if (AskSaveChanges())
            {
                E.Cancel = true;
                return;
            }

            CleanUp();
        }

        #endregion

    } // End class

} // End namespace