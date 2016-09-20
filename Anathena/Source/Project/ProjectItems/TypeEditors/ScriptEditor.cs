using Ana.Source.LuaEngine;
using Ana.Source.Utils;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Ana.Source.Project.ProjectItems.TypeEditors
{
    class ScriptEditor : UITypeEditor, IScriptEditorModel
    {
        private LuaScript LuaScript;
        private InputRequest.InputRequestDelegate InputRequest;

        public event ScriptEditorEventHandler EventUpdateScript;

        public ScriptEditor()
        {
            LuaScript = new LuaScript();

            ScriptEditorPresenter ScriptEditorPresenter = new ScriptEditorPresenter(null, this);
            InputRequest = ScriptEditorPresenter.GetInputRequestCallBack();
        }

        public void OnGUIOpen()
        {
            OnUpdateScript();
        }

        private void OnUpdateScript()
        {
            ScriptEditorEventArgs Args = new ScriptEditorEventArgs();
            Args.Script = LuaScript.Script;
            EventUpdateScript?.Invoke(this, Args);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext Context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext Context, IServiceProvider Provider, Object Value)
        {
            if (InputRequest == null || (Value != null && !Value.GetType().IsAssignableFrom(typeof(LuaScript))))
                return Value;

            LuaScript = Value == null ? new LuaScript() : (Value as LuaScript);

            OnUpdateScript();

            // Call delegate function to request the script be edited by the user
            if (InputRequest != null && InputRequest() == DialogResult.OK)
                return LuaScript;

            return Value;
        }

        public LuaScript GetScript()
        {
            return LuaScript;
        }

        public void SaveChanges(String NewScript)
        {
            if (!HasChanges(NewScript))
                return;

            LuaScript.Script = NewScript;

            ProjectExplorer.GetInstance().ProjectChanged();
        }

        public Boolean HasChanges(String NewScript)
        {
            if (NewScript != LuaScript.Script)
                return true;

            return false;
        }

    } // End class

} // End namespace