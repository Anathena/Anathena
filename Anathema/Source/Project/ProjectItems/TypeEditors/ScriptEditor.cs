using Anathema.GUI.Tools.TypeEditors;
using Anathema.Source.LuaEngine;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Anathema.Source.Project.ProjectItems.TypeEditors
{
    class ScriptEditor : UITypeEditor
    {
        public ScriptEditor() { }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext Context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext Context, System.IServiceProvider Provider, Object Value)
        {
            IWindowsFormsEditorService Service = Provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            LuaScript LuaScript = Value as LuaScript;

            if (Service == null)
                return Value;

            using (GUIScriptEditor Form = new GUIScriptEditor(LuaScript))
            {
                if (Service.ShowDialog(Form) == DialogResult.OK)
                    return Form.GetScript();
            }

            return Value;
        }

    } // End class

} // End namespace