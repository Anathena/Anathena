namespace Ana.Source.Editors.TextEditor
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows;

    /// <summary>
    /// Type editor for text.
    /// </summary>
    internal class TextEditorModel : UITypeEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditorModel" /> class.
        /// </summary>
        public TextEditorModel()
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
            View.Editors.TextEditor textEditor = new View.Editors.TextEditor(value as String);

            textEditor.Owner = Application.Current.MainWindow;
            if (textEditor.ShowDialog() == true)
            {
                return textEditor.TextEditorViewModel.Text ?? String.Empty;
            }

            return value;
        }
    }
    //// End class
}
//// End namespace