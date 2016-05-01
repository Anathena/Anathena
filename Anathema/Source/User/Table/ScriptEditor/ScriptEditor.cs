using Anathema.Utils.OS;
using Anathema.Utils.LUA;
using System;
using Anathema.User.UserScriptTable;
using System.Runtime.CompilerServices;

namespace Anathema.User.UserScriptEditor
{
    class ScriptEditor : IScriptEditorModel
    {
        // Singleton instance of Script Editor
        private static Lazy<ScriptEditor> ScriptEditorInstance = new Lazy<ScriptEditor>(() => { return new ScriptEditor(); });

        private OSInterface OSInterface;

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
            return ScriptEditorInstance.Value;
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