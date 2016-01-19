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

        public event ScriptEditorEventHandler EventUpdateProcessTitle;

        private ScriptItem ScriptItem;

        public ScriptEditor()
        {
            InitializeObserver();

            ScriptItem = new ScriptItem();
        }
        
        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
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
    }
}