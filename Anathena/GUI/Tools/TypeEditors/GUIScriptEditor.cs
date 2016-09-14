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
        private static Scintilla staticEditor = new Scintilla();
        private ScriptEditorPresenter scriptEditorPresenter;
        private Object accessLock;
        private String documentTitle;

        public GUIScriptEditor(ScriptEditorPresenter scriptEditorPresenter)
        {
            InitializeComponent();

            accessLock = new Object();
            documentTitle = this.Text;

            this.scriptEditorPresenter = scriptEditorPresenter;

            FixScintilla();
            InitializeScriptEditor();
        }

        private void FixScintilla()
        {
            // Disabled due to current implementation, where the editor is no longer dockable. If this changes, this fix will need to be reinstated.
            return;

            // Work around to a fatal bug in scintilla where the handle of the editor is changed, and scintilla does not expect it.
            /* using (TimedLock.Lock(accessLock))
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
            }*/
        }

        private void InitializeScriptEditor()
        {
            using (TimedLock.Lock(accessLock))
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
            using (TimedLock.Lock(accessLock))
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
            using (TimedLock.Lock(accessLock))
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

        public void RefreshScript(String newScript)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
            {
                ScriptEditorTextBox.Text = newScript;
            });
        }

        private void SaveChanges()
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    scriptEditorPresenter.SaveChanges(ScriptEditorTextBox.Text);

                    this.Text = documentTitle;
                });
            }
        }

        private Boolean AskSaveChanges()
        {
            // Check if there are even changes to save
            if (!scriptEditorPresenter.HasChanges(ScriptEditorTextBox.Text))
                return false;

            DialogResult result = MessageBoxEx.Show(this, "This script has not been saved, save the changes to the table before closing?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            switch (result)
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
            using (TimedLock.Lock(accessLock))
            {
                staticEditor.ClearAll();

                // Remove the static editor so that it does not get disposed
                if (this.Controls.Contains(staticEditor))
                    this.Controls.Remove(staticEditor);
            }
        }

        #region Events

        private void ScriptEditorTextBox_CharAdded(Object sender, CharAddedEventArgs e)
        {
            Boolean asmMode = false;
            Int32 length;

            using (TimedLock.Lock(accessLock))
            {
                if (e.Char == '\n')
                {
                    Int32 Indentation = (ScriptEditorTextBox.Lines[ScriptEditorTextBox.CurrentLine == 0 ? 0 : ScriptEditorTextBox.CurrentLine - 1].Indentation) / ScriptEditorTextBox.IndentWidth;

                    ScriptEditorTextBox.InsertText(ScriptEditorTextBox.CurrentPosition, String.Join("", Enumerable.Repeat("\t", Indentation)));
                    ScriptEditorTextBox.CurrentPosition = Math.Min(ScriptEditorTextBox.CurrentPosition + Indentation, ScriptEditorTextBox.TextLength);
                    ScriptEditorTextBox.AnchorPosition = ScriptEditorTextBox.CurrentPosition;
                }

                length = ScriptEditorTextBox.CurrentPosition - ScriptEditorTextBox.WordStartPosition(ScriptEditorTextBox.CurrentPosition, true);

                if (length <= 0)
                    return;

                ScriptEditorTextBox.SearchFlags = SearchFlags.None;

                // Search for closest preceeding [fasm] tag to determine if we are writing inside the tag
                ScriptEditorTextBox.TargetStart = 0;
                ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.CurrentPosition;
                Int32 startTagPosition = -1;
                while (true)
                {
                    Int32 next = ScriptEditorTextBox.SearchInTarget("[fasm]");

                    if (next == -1)
                        break;

                    startTagPosition = next;

                    ScriptEditorTextBox.TargetStart = ScriptEditorTextBox.TargetEnd;
                    ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.CurrentPosition;
                }

                // Search for next [fasm] tag to ensure the [/fasm] does not pass it
                ScriptEditorTextBox.TargetStart = ScriptEditorTextBox.CurrentPosition;
                ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.TextLength;
                Int32 nextTagPosition = ScriptEditorTextBox.SearchInTarget("[fasm]");

                // Search for next [/fasm] tag and see if we are inside it
                ScriptEditorTextBox.TargetStart = ScriptEditorTextBox.CurrentPosition;
                ScriptEditorTextBox.TargetEnd = ScriptEditorTextBox.TextLength;

                if (startTagPosition != -1)
                {
                    Int32 endTagPositon = ScriptEditorTextBox.SearchInTarget("[/fasm]");
                    if (endTagPositon != -1 && (nextTagPosition == -1 || endTagPositon < nextTagPosition))
                        asmMode = true;
                }
            }

            if (asmMode)
            {
                EnableAsmHighlighting();
                // ScriptEditorTextBox.AutoCShow(length, LuaKeywordManager.AllAsmKeywords);
            }
            else
            {
                EnableLuaHighlighting();
                ScriptEditorTextBox.AutoCShow(length, LuaKeywordManager.AllLuaKeywords);
            }

        }

        private void ScriptEditorTextBox_TextChanged(Object sender, EventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
                {
                    if (scriptEditorPresenter.HasChanges(ScriptEditorTextBox?.Text))
                        this.Text = documentTitle + "*";
                    else
                        this.Text = documentTitle;
                });
            }
        }

        private void CodeInjectionToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
            {
                ScriptEditorTextBox.Text = LuaTemplates.AddCodeInjectionTemplate(ScriptEditorTextBox.Text, "module.exe", new IntPtr(0xabcde));
            });
        }
        private void GraphicsOverlayToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptEditorTextBox, () =>
            {
                ScriptEditorTextBox.Text = LuaTemplates.AddGraphicsOverlayTemplate(ScriptEditorTextBox.Text);
            });
        }

        protected override Boolean ProcessCmdKey(ref Message message, Keys keys)
        {
            if (keys == (Keys.Control | Keys.S))
            {
                SaveChanges();
                return true;
            }
            else if (keys == (Keys.Control | Keys.W))
            {
                Close();
                return true;
            }

            // ScintillaNet will insert garbage with certain command keys, this filters those out
            else if (keys == (Keys.Control | Keys.B)) return true;
            else if (keys == (Keys.Control | Keys.D)) return true;
            else if (keys == (Keys.Control | Keys.E)) return true;
            else if (keys == (Keys.Control | Keys.F)) return true;
            else if (keys == (Keys.Control | Keys.G)) return true;
            else if (keys == (Keys.Control | Keys.H)) return true;
            else if (keys == (Keys.Control | Keys.K)) return true;
            else if (keys == (Keys.Control | Keys.N)) return true;
            else if (keys == (Keys.Control | Keys.O)) return true;
            else if (keys == (Keys.Control | Keys.P)) return true;
            else if (keys == (Keys.Control | Keys.Q)) return true;
            else if (keys == (Keys.Control | Keys.R)) return true;

            return base.ProcessCmdKey(ref message, keys);
        }

        private void SaveToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void GUIScriptEditor_FormClosing(Object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;

            if (AskSaveChanges())
            {
                e.Cancel = true;
                return;
            }

            CleanUp();
        }

        #endregion

    } // End class

} // End namespace