﻿namespace Ana.Source.Utils.ScriptEditor
{
    using ScriptEngine;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows;

    /// <summary>
    /// Type editor for scripts.
    /// </summary>
    internal class ScriptEditorModel : UITypeEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptEditorModel" /> class.
        /// </summary>
        public ScriptEditorModel()
        {
        }

        /// <summary>
        /// Gets the editor style. This will be Modal, as it launches a custom editor.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <returns>Modal type editor.</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// Launches the editor for this type.
        /// </summary>
        /// <param name="context">Type descriptor context.</param>
        /// <param name="provider">Service provider.</param>
        /// <param name="value">The current value.</param>
        /// <returns>The updated values.</returns>
        public override Object EditValue(ITypeDescriptorContext context, IServiceProvider provider, Object value)
        {
            View.ScriptEditor scriptEditor = new View.ScriptEditor((value as Script)?.Payload);

            scriptEditor.Owner = Application.Current.MainWindow;
            if (scriptEditor.ShowDialog() == true)
            {
                String script = scriptEditor.ScriptEditorViewModel.Script;

                if (script != null && script != String.Empty)
                {
                    return new Script(scriptEditor.ScriptEditorViewModel.Script);
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