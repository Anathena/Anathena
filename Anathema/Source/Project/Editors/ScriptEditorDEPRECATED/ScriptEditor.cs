using Anathema.Source.Engine;
using Anathema.Source.Engine.Processes;
using Anathema.Source.LuaEngine;
using Anathema.Source.Project.ProjectItems;
using Anathema.Source.Project.ProjectItems.ScriptTemplates;
using System;
using System.Threading;

namespace Anathema.Source.Project.Editors.ScriptEditor
{
    class ScriptEditor : IScriptEditorModel
    {
        // Singleton instance of Script Editor
        private static Lazy<ScriptEditor> ScriptEditorInstance = new Lazy<ScriptEditor>(() => { return new ScriptEditor(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore EngineCore;

        public event ScriptEditorEventHandler EventOpenScript;
        public event ScriptEditorEventHandler EventSetScriptText;

        private ScriptItem ScriptItem;
        private LuaScript LuaScript;

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

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
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
            ScriptItem.LuaScript.Script = ScriptText;
            // TODO: DEPRECATED: ScriptTable.GetInstance().SaveScript(ScriptItem);
        }

        public Boolean HasChanges(String Script)
        {
            if (ScriptItem.LuaScript.Script == Script)
                return false;

            return true;
        }

        public void InsertCodeInjectionTemplate()
        {
            String NewScript = LuaTemplates.AddCodeInjectionTemplate(ScriptItem.LuaScript.Script, "module.exe", new IntPtr(0x1abcd));
            ScriptEditorEventArgs ScriptEditorEventArgs = new ScriptEditorEventArgs();
            ScriptEditorEventArgs.NewScript = NewScript;
            EventSetScriptText?.Invoke(this, ScriptEditorEventArgs);
        }

        public void InsertGraphicsOverlayTemplate()
        {
            String NewScript = LuaTemplates.AddGraphicsOverlayTemplate(ScriptItem.LuaScript.Script);
            ScriptEditorEventArgs ScriptEditorEventArgs = new ScriptEditorEventArgs();
            ScriptEditorEventArgs.NewScript = NewScript;
            EventSetScriptText?.Invoke(this, ScriptEditorEventArgs);
        }

    } // End class

} // End namespace