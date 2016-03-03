using Anathema.Services.ProcessManager;
using Anathema.Utils.MVP;
using System;

namespace Anathema
{
    delegate void ScriptEditorEventHandler(Object Sender, ScriptEditorEventArgs Args);
    class ScriptEditorEventArgs : EventArgs
    {
        public ScriptItem ScriptItem;
        public String NewScript;
    }

    interface IScriptEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void OpenScript(String ScriptText);
        void SetScriptText(String ScriptText);
    }

    interface IScriptEditorModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event ScriptEditorEventHandler EventOpenScript;
        event ScriptEditorEventHandler EventSetScriptText;

        // Functions invoked by presenter (downstream)
        void SaveScript(String ScriptText);
        void OpenNewScript();
        Boolean HasChanges(String Script);

        void InsertCodeInjectionTemplate();
    }

    class ScriptEditorPresenter : Presenter<IScriptEditorView, IScriptEditorModel>
    {
        public ScriptEditorPresenter(IScriptEditorView View, IScriptEditorModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventOpenScript += EventOpenScript;
            Model.EventSetScriptText += EventSetScriptText;
        }

        #region Method definitions called by the view (downstream)
        
        public void SaveScript(String ScriptText)
        {
            Model.SaveScript(ScriptText);
        }

        public void OpenNewScript()
        {
            Model.OpenNewScript();
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

        void EventOpenScript(Object Sender, ScriptEditorEventArgs E)
        {
            View.OpenScript(E.ScriptItem.Script);
        }

        void EventSetScriptText(Object Sender, ScriptEditorEventArgs E)
        {
            View.SetScriptText(E.NewScript);
        }

        #endregion

    } // End class

} // End namespace