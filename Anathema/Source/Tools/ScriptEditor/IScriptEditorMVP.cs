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
    }

    interface IScriptEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void DisplayScript(String ScriptText);
    }

    interface IScriptEditorModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event ScriptEditorEventHandler EventDisplayScript;

        // Functions invoked by presenter (downstream)
        void SaveScript(String ScriptText);
        Boolean HasChanges(String ScriptText);
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

        public Boolean HasChanges(String ScriptText)
        {
            return Model.HasChanges(ScriptText);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        void EventDisplayScript(Object Sender, ScriptEditorEventArgs E)
        {
            View.DisplayScript(E.ScriptItem.Script);
        }

        #endregion
    }
}
