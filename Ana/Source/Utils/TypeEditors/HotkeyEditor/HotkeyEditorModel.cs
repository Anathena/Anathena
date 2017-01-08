namespace Ana.Source.Utils.HotkeyEditor
{
    using Engine.Input.HotKeys;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Type editor for hot keys.
    /// </summary>
    internal class HotkeyEditorModel : UITypeEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyEditorModel" /> class.
        /// </summary>
        public HotkeyEditorModel()
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
            View.Editors.HotkeyEditor hotkeyEditor = new View.Editors.HotkeyEditor(value == null ? null : (value as IEnumerable<IHotkey>)?.ToList());

            hotkeyEditor.Owner = Application.Current.MainWindow;
            if (hotkeyEditor.ShowDialog() == true)
            {
                List<IHotkey> newOffsets = hotkeyEditor.HotkeyEditorViewModel.Hotkeys.ToList();

                if (newOffsets != null && newOffsets.Count > 0)
                {
                    return newOffsets;
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