using Anathena.Source.LuaEngine;
using Anathena.Source.Project;
using Anathena.Source.Project.ProjectItems.ScriptTemplates;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using ScintillaNET;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools.TypeEditors
{
    partial class GUIScriptEditor : DockContent, IScriptEditorView
    {
        private static Scintilla StaticEditor = new Scintilla();
        private ScriptEditorPresenter ScriptEditorPresenter;
        private Object AccessLock;
        private String DocumentTitle;

        public GUIScriptEditor(ScriptEditorPresenter ScriptEditorPresenter)
        {
            InitializeComponent();

            AccessLock = new Object();
            DocumentTitle = this.Text;

            this.ScriptEditorPresenter = ScriptEditorPresenter;

            FixScintilla();
            InitializeScriptEditor();
        }

        private void FixScintilla()
        {
            // Disabled due to current implementation, where the editor is no longer dockable. If this changes, this fix will need to be reinstated.
            return;

            // Work around to a fatal bug in scintilla where the handle of the editor is changed, and scintilla does not expect it.
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    if (this.Controls.Contains(ScriptEditorTextBox))
                        this.Controls.Remove(ScriptEditorTextBox);
                    ScriptEditorTextBox = StaticEditor;
                    ScriptEditorTextBox.Dock = DockStyle.Fill;
                });

                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    if (!this.Controls.Contains(ScriptEditorTextBox))
                        this.Controls.Add(ScriptEditorTextBox);
                    this.Controls.SetChildIndex(ScriptEditorTextBox, 0);
                });
            }
        }

        private void InitializeScriptEditor()
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    ScriptEditorTextBox.Lexer = Lexer.Lua;
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

                    // Bind events
                    ScriptEditorTextBox.TextChanged += ScriptEditorTextBox_TextChanged;
                    ScriptEditorTextBox.CharAdded += ScriptEditorTextBox_CharAdded;
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
                    ScriptEditorTextBox.SetKeywords(1, LuaKeywordManager.AnathenaKeywords);
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
                    ScriptEditorTextBox.SetKeywords(3, LuaKeywordManager.AnathenaKeywords);

                    ScriptEditorTextBox.Styles[Style.Lua.Word].ForeColor = Color.Firebrick;
                    ScriptEditorTextBox.Styles[Style.Lua.Word2].ForeColor = Color.Blue;
                    ScriptEditorTextBox.Styles[Style.Lua.Word3].ForeColor = Color.Blue;
                    ScriptEditorTextBox.Styles[Style.Lua.Word4].ForeColor = Color.CadetBlue;
                });
            }
        }

        public void RefreshScript(String NewScript)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
            {
                ScriptEditorTextBox.Text = NewScript;
            });
        }

        private void SaveChanges()
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    ScriptEditorPresenter.SaveChanges(ScriptEditorTextBox.Text);

                    this.Text = DocumentTitle;
                });
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
                StaticEditor.ClearAll();

                // Remove the static editor so that it does not get disposed
                if (this.Controls.Contains(StaticEditor))
                    this.Controls.Remove(StaticEditor);
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

                // Search for closest preceeding [fasm] tag to determine if we are writing inside the tag
                ScriptEditorTextBox.TargetStart = 0;
                ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.CurrentPosition;
                Int32 StartTagPosition = -1;
                while (true)
                {
                    Int32 Next = ScriptEditorTextBox.SearchInTarget("[fasm]");

                    if (Next == -1)
                        break;

                    StartTagPosition = Next;

                    ScriptEditorTextBox.TargetStart = ScriptEditorTextBox.TargetEnd;
                    ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.CurrentPosition;
                }

                // Search for next [fasm] tag to ensure the [/fasm] does not pass it
                ScriptEditorTextBox.TargetStart = ScriptEditorTextBox.CurrentPosition;
                ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.TextLength;
                Int32 NextTagPosition = ScriptEditorTextBox.SearchInTarget("[fasm]");

                // Search for next [/fasm] tag and see if we are inside it
                ScriptEditorTextBox.TargetStart = ScriptEditorTextBox.CurrentPosition;
                ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.TextLength;

                if (StartTagPosition != -1)
                {
                    Int32 EndTagPositon = ScriptEditorTextBox.SearchInTarget("[/fasm]");
                    if (EndTagPositon != -1 && (NextTagPosition == -1 || EndTagPositon < NextTagPosition))
                        AsmMode = true;
                }
            }

            if (AsmMode)
            {
                EnableAsmHighlighting();
                // ScriptEditorTextBox.AutoCShow(Length, LuaKeywordManager.AllAsmKeywords);
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
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    if (ScriptEditorPresenter.HasChanges(ScriptEditorTextBox?.Text))
                        this.Text = DocumentTitle + "*";
                    else
                        this.Text = DocumentTitle;
                });
            }
        }

        private void CodeInjectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
            {
                ScriptEditorTextBox.Text = LuaTemplates.AddCodeInjectionTemplate(ScriptEditorTextBox.Text, "module.exe", new IntPtr(0xabcde));
            });
        }
        private void GraphicsOverlayToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
            {
                ScriptEditorTextBox.Text = LuaTemplates.AddGraphicsOverlayTemplate(ScriptEditorTextBox.Text);
            });
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

            // ScintillaNet will insert garbage with certain command keys, this filters those out
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

        private void SaveToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            SaveChanges();
        }

        private void GUIScriptEditor_FormClosing(Object Sender, FormClosingEventArgs E)
        {
            DialogResult = DialogResult.OK;

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