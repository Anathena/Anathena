namespace Ana.Source.Utils.ScriptEditor
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows;

    internal class ScriptEditorModel : UITypeEditor
    {
        public ScriptEditorModel()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext context, IServiceProvider provider, Object value)
        {
            View.ScriptEditor offsetEditor = new View.ScriptEditor();

            offsetEditor.Owner = Application.Current.MainWindow;
            if (offsetEditor.ShowDialog() == true)
            {
                if ("true".Equals("true"))
                {

                }
                else
                {
                    return null;
                }
            }

            return value;
        }
    }
    //// End class
}
//// End namespace