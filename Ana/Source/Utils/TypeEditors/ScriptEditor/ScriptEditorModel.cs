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
            View.ScriptEditor scriptEditor = new View.ScriptEditor();

            scriptEditor.Owner = Application.Current.MainWindow;
            if (scriptEditor.ShowDialog() == true)
            {
                return scriptEditor.ScriptEditorViewModel.Script;
            }

            return value;
        }
    }
    //// End class
}
//// End namespace