using Anathema.GUI.Tools.TypeEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Anathema.Source.Project.ProjectItems.TypeEditors
{
    class ArrayEditor : UITypeEditor
    {
        public ArrayEditor() { }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext Context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext Context, System.IServiceProvider Provider, Object Value)
        {
            IWindowsFormsEditorService Service = Provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            List<Int32> Array = Value as List<Int32>;

            if (Service == null)
                return Value;

            using (GUIArrayEditor Form = new GUIArrayEditor(Array))
            {
                if (Service.ShowDialog(Form) == DialogResult.OK)
                    return Form.Offsets;
            }

            return Value;
        }

    } // End class

} // End namespace