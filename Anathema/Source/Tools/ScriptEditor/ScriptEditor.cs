using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class ScriptEditor : IScriptEditorModel
    {
        private MemorySharp MemoryEditor;
        private static ScriptEditor _ScriptEditor;

        public event ScriptEditorEventHandler EventDisplayScript;

        private ScriptItem ScriptItem;

        private ScriptEditor()
        {
            InitializeProcessObserver();

            ScriptItem = new ScriptItem();
        }

        public static ScriptEditor GetInstance()
        {
            if (_ScriptEditor == null)
                _ScriptEditor = new ScriptEditor();
            return _ScriptEditor;
        }
        
        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        private void UpdateDisplayText()
        {
            ScriptEditorEventArgs ScriptEditorEventArgs = new ScriptEditorEventArgs();
            ScriptEditorEventArgs.ScriptItem = ScriptItem;
            EventDisplayScript(this, ScriptEditorEventArgs);
        }

        public void OpenScript(ScriptItem ScriptItem)
        {
            this.ScriptItem = ScriptItem;

            UpdateDisplayText();
        }

        public Boolean HasChanges(String ScriptText)
        {
            if (ScriptItem.Script != ScriptText)
                return true;
            return false;
        }

        public void NewScript()
        {
            ScriptItem = new ScriptItem();
        }

        public void SaveScript(String ScriptText)
        {
            ScriptItem.Script = ScriptText;
            Table.GetInstance().SaveScript(ScriptItem);
        }

        public void InsertCodeInjectionTemplate()
        {
            ScriptItem.Script = ScriptItem.CodeInjection;
            UpdateDisplayText();
        }

    } // End class

} // End namespace