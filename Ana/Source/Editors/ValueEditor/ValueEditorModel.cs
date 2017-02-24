namespace Ana.Source.Editors.ValueEditor
{
    using Project.ProjectItems;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows;

    /// <summary>
    /// Type editor for scripts.
    /// </summary>
    internal class ValueEditorModel : UITypeEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueEditorModel" /> class.
        /// </summary>
        public ValueEditorModel()
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
            View.Editors.ValueEditor valueEditor = new View.Editors.ValueEditor(value as AddressItem);

            valueEditor.Owner = Application.Current.MainWindow;
            if (valueEditor.ShowDialog() == true)
            {
                return valueEditor.ValueEditorViewModel.Value;
            }

            return (value as AddressItem)?.Value;
        }
    }
    //// End class
}
//// End namespace