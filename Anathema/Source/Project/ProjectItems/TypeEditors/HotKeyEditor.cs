using Anathema.GUI.Tools.TypeEditors;
using Anathema.Source.Engine.InputCapture;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Anathema.Source.Project.ProjectItems.TypeEditors
{
    class HotKeyEditor : UITypeEditor
    {
        public HotKeyEditor() { }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext Context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext Context, System.IServiceProvider Provider, Object Value)
        {
            IWindowsFormsEditorService Service = Provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            InputBinding HotKeys = Value as InputBinding;

            if (Service == null)
                return Value;

            using (GUIHotKeyEditor Form = new GUIHotKeyEditor(HotKeys))
            {
                if (Service.ShowDialog(Form) == DialogResult.OK)
                    return Form.HotKeys;
            }

            return Value;
        }

    } // End class

} // End namespace