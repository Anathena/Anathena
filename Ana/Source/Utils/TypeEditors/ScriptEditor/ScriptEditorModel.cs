namespace Ana.Source.Utils.ScriptEditor
{
    using LuaEngine;
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
            View.ScriptEditor scriptEditor = new View.ScriptEditor((value as LuaScript)?.Script);

            scriptEditor.Owner = Application.Current.MainWindow;
            if (scriptEditor.ShowDialog() == true)
            {
                String script = scriptEditor.ScriptEditorViewModel.Script;

                if (script != null && script != String.Empty)
                {
                    return new LuaScript(scriptEditor.ScriptEditorViewModel.Script);
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