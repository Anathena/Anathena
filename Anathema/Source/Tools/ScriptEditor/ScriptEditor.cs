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

        private void UpdateDisplayText(Boolean Loaded)
        {
            ScriptEditorEventArgs ScriptEditorEventArgs = new ScriptEditorEventArgs();
            ScriptEditorEventArgs.ScriptItem = ScriptItem;
            ScriptEditorEventArgs.Loaded = Loaded;
            EventDisplayScript(this, ScriptEditorEventArgs);
        }

        public void OpenScript(ScriptItem ScriptItem)
        {
            this.ScriptItem = ScriptItem;

            UpdateDisplayText(true);
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

        public Boolean HasChanges(String Script)
        {
            if (ScriptItem.Script == Script)
                return false;
            return true;
        }

        public void InsertCodeInjectionTemplate()
        {
            ScriptItem.Script = LuaEngine.AddCodeInjectionTemplate(ScriptItem.Script, "main.exe", 0x41c);
            UpdateDisplayText(false);
        }

    } // End class

} // End namespace