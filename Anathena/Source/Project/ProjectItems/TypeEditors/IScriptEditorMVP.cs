using Anathena.GUI.Tools.TypeEditors;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using System;
using System.Windows.Forms;

namespace Anathena.Source.Project
{
    delegate void ScriptEditorEventHandler(Object Sender, ScriptEditorEventArgs Args);
    class ScriptEditorEventArgs : EventArgs
    {
        public String Script;
    }

    interface IScriptEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshScript(String NewScript);
    }

    interface IScriptEditorModel : IModel
    {
        // Events triggered by the model (upstream)
        event ScriptEditorEventHandler EventUpdateScript;

        // Functions invoked by presenter (downstream)
        void SaveChanges(String NewScript);
        Boolean HasChanges(String NewScript);
    }

    class ScriptEditorPresenter : Presenter<IScriptEditorView, IScriptEditorModel>
    {
        private GUIScriptEditor GUIScriptEditor;
        private new IScriptEditorView view { get; set; }
        private new IScriptEditorModel model { get; set; }

        public ScriptEditorPresenter(IScriptEditorView view, IScriptEditorModel model) : base(view, model)
        {
            GUIScriptEditor = new GUIScriptEditor(this);
            this.view = GUIScriptEditor;
            this.model = model;

            // Bind events triggered by the model
            model.EventUpdateScript += EventUpdateScript;

            model.OnGUIOpen();
        }

        public InputRequest.InputRequestDelegate GetInputRequestCallBack()
        {
            return InputRequest;
        }

        #region Method definitions called by the view (downstream)

        public void SaveChanges(String NewScript)
        {
            model.SaveChanges(NewScript == null ? String.Empty : NewScript);
        }

        public Boolean HasChanges(String NewScript)
        {
            return model.HasChanges(NewScript == null ? String.Empty : NewScript);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private DialogResult InputRequest()
        {
            model.OnGUIOpen();
            return GUIScriptEditor.ShowDialog();
        }

        private void EventUpdateScript(Object Sender, ScriptEditorEventArgs E)
        {
            view.RefreshScript(E?.Script == null ? String.Empty : E.Script);
        }

        #endregion

    } // End class

} // End namespace
