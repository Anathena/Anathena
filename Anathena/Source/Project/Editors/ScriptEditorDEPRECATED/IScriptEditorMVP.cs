using Anathena.Source.Engine.Processes;
using Anathena.Source.LuaEngine;
using Anathena.Source.Project.ProjectItems;
using Anathena.Source.Utils.MVP;
using System;

namespace Anathena.Source.Project.Editors.ScriptEditor
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
        void OpenScript(LuaScript ScriptText);
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
        void InsertGraphicsOverlayTemplate();
    }

    class ScriptEditorPresenter : Presenter<IScriptEditorView, IScriptEditorModel>
    {
        private new IScriptEditorView View { get; set; }
        private new IScriptEditorModel Model { get; set; }

        public ScriptEditorPresenter(IScriptEditorView View, IScriptEditorModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventOpenScript += EventOpenScript;
            Model.EventSetScriptText += EventSetScriptText;

            Model.OnGUIOpen();
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

        public void InsertGraphicsOverlayTemplate()
        {
            Model.InsertGraphicsOverlayTemplate();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        void EventOpenScript(Object Sender, ScriptEditorEventArgs E)
        {
            View.OpenScript(E.ScriptItem.LuaScript);
        }

        void EventSetScriptText(Object Sender, ScriptEditorEventArgs E)
        {
            View.SetScriptText(E.NewScript);
        }

        #endregion

    } // End class

} // End namespace