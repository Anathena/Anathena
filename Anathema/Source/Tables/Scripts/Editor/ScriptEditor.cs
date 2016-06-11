using Anathema.Source.SystemInternals.OperatingSystems;
using Anathema.Source.SystemInternals.Processes;
using Anathema.Source.Utils.LUA;
using System;

namespace Anathema.Source.Tables.Scripts.Editor
{
    class ScriptEditor : IScriptEditorModel
    {
        // Singleton instance of Script Editor
        private static Lazy<ScriptEditor> ScriptEditorInstance = new Lazy<ScriptEditor>(() => { return new ScriptEditor(); });

        private Engine OSInterface;

        public event ScriptEditorEventHandler EventOpenScript;
        public event ScriptEditorEventHandler EventSetScriptText;

        private ScriptItem ScriptItem;

        private ScriptEditor()
        {
            InitializeProcessObserver();

            ScriptItem = new ScriptItem();
        }

        public void OnGUIOpen() { }

        public static ScriptEditor GetInstance()
        {
            return ScriptEditorInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateOSInterface(Engine OSInterface)
        {
            this.OSInterface = OSInterface;
        }

        public void OpenScript(ScriptItem ScriptItem)
        {
            this.ScriptItem = ScriptItem;

            ScriptEditorEventArgs ScriptEditorEventArgs = new ScriptEditorEventArgs();
            ScriptEditorEventArgs.ScriptItem = ScriptItem;
            EventOpenScript?.Invoke(this, ScriptEditorEventArgs);
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
            EventSetScriptText?.Invoke(this, ScriptEditorEventArgs);
        }

    } // End class

} // End namespace