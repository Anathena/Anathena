namespace Ana.Source.Utils.OffsetEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Linq;

    internal class OffsetEditorModel : UITypeEditor
    {
        public OffsetEditorModel()
        {
        }

        private List<Int32> Offsets { get; set; }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext context, IServiceProvider provider, Object value)
        {
            View.OffsetEditor offsetEditor = new View.OffsetEditor();

            Offsets = new List<Int32>();

            if ((value != null && !value.GetType().IsAssignableFrom(typeof(IEnumerable<Int32>))))
            {
                return value;
            }

            Offsets = value == null ? new List<Int32>() : (value as IEnumerable<Int32>).ToList();

            // Call delegate function to request the offsets be edited by the user
            if (offsetEditor.ShowDialog() == true)
            {
                return Offsets;
            }

            return value;
        }

        public void DeleteOffsets(IEnumerable<Int32> indicies)
        {
            foreach (Int32 Index in indicies?.OrderByDescending(x => x))
            {
                if (Index < Offsets.Count)
                {
                    Offsets.RemoveAt(Index);
                }
            }
        }

        public void AddOffset(Int32 Offset)
        {
            Offsets.Add(Offset);
        }
    }
    //// End class
}
//// End namespace