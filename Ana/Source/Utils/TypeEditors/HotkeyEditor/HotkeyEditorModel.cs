namespace Ana.Source.Utils.HotkeyEditor
{
    using Engine.Input.HotKeys;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Linq;
    using System.Windows;

    internal class HotkeyEditorModel : UITypeEditor
    {
        public HotkeyEditorModel()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext context, IServiceProvider provider, Object value)
        {
            View.HotkeyEditor hotkeyEditor = new View.HotkeyEditor(value == null ? null : (value as IEnumerable<IHotkey>)?.ToList());

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