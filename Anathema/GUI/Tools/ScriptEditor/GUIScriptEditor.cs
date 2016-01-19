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

        public GUIScriptEditor()
        {
            InitializeComponent();

            ScriptEditorPresenter = new ScriptEditorPresenter(this, new ScriptEditor());
        }

        #region Events

        #endregion

        private void SaveToTableToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ScriptEditorPresenter.SaveScript(ScriptEditorRichTextBox.Text);
        }

    } // End class

} // End namespace