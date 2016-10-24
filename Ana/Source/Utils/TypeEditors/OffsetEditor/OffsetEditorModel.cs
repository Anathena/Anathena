namespace Ana.Source.Utils.OffsetEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Linq;
    using System.Windows;

    internal class OffsetEditorModel : UITypeEditor
    {
        public OffsetEditorModel()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext context, IServiceProvider provider, Object value)
        {
            View.OffsetEditor offsetEditor = new View.OffsetEditor(value == null ? null : (value as IEnumerable<Int32>)?.ToList());

            offsetEditor.Owner = Application.Current.MainWindow;
            if (offsetEditor.ShowDialog() == true)
            {
                List<Int32> newOffsets = offsetEditor.OffsetEditorViewModel.Offsets.ToList();

                if (newOffsets != null && newOffsets.Count > 0)
                {
                    return offsetEditor.OffsetEditorViewModel.Offsets.ToList();
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