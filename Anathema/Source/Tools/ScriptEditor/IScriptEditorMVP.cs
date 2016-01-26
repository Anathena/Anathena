using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void ScriptEditorEventHandler(Object Sender, ScriptEditorEventArgs Args);
    class ScriptEditorEventArgs : EventArgs
    {
        public ScriptItem ScriptItem;
        public Boolean Loaded;
    }

    interface IScriptEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void DisplayScript(String ScriptText, Boolean Loaded);
    }

    interface IScriptEditorModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event ScriptEditorEventHandler EventDisplayScript;

        // Functions invoked by presenter (downstream)
        void SaveScript(String ScriptText);
        Boolean HasChanges(String Script);

        void InsertCodeInjectionTemplate();
    }

    class ScriptEditorPresenter : Presenter<IScriptEditorView, IScriptEditorModel>
    {
        public ScriptEditorPresenter(IScriptEditorView View, IScriptEditorModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventDisplayScript += EventDisplayScript;
        }

        #region Method definitions called by the view (downstream)
        
        public void SaveScript(String ScriptText)
        {
            Model.SaveScript(ScriptText);
        }
        
        public Boolean HasChanges(String Script)
        {
            return Model.HasChanges(Script);
        }

        public void InsertCodeInjectionTemplate()
        {
            Model.InsertCodeInjectionTemplate();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        void EventDisplayScript(Object Sender, ScriptEditorEventArgs E)
        {
            View.DisplayScript(E.ScriptItem.Script, E.Loaded);
        }

        #endregion
    }
}
