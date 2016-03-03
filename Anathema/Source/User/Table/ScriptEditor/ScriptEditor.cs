using Anathema.Utils.OS;
using Anathema.Utils.LUA;
using System;

namespace Anathema
{
    class ScriptEditor : IScriptEditorModel
    {
        private OSInterface OSInterface;
        private static ScriptEditor _ScriptEditor;

        public event ScriptEditorEventHandler EventOpenScript;
        public event ScriptEditorEventHandler EventSetScriptText;

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

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;
        }

        public void OpenScript(ScriptItem ScriptItem)
        {
            this.ScriptItem = ScriptItem;

            ScriptEditorEventArgs ScriptEditorEventArgs = new ScriptEditorEventArgs();
            ScriptEditorEventArgs.ScriptItem = ScriptItem;
            EventOpenScript(this, ScriptEditorEventArgs);
        }

        public void OpenNewScript()
        {
            ScriptItem = new ScriptItem();
            OpenScript(ScriptItem);
        }

        public void SaveScript(String ScriptText)
        {
            ScriptItem.Script = ScriptText;
            ScriptTable.GetInstance().SaveScript(ScriptItem);
        }

        public Boolean HasChanges(String Script)
        {
            if (ScriptItem.Script == Script)
                return false;
            return true;
        }

        public void InsertCodeInjectionTemplate()
        {
            String NewScript = LuaEngine.AddCodeInjectionTemplate(ScriptItem.Script, "module.exe", new IntPtr(0x1abcd));
            ScriptEditorEventArgs ScriptEditorEventArgs = new ScriptEditorEventArgs();
            ScriptEditorEventArgs.NewScript = NewScript;
            EventSetScriptText(this, ScriptEditorEventArgs);
        }

    } // End class

} // End namespace