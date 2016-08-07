using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

namespace Anathema.Source.Project.ProjectItems.TypeEditors
{
    class OffsetEditor : UITypeEditor, IOffsetEditorModel
    {
        public event OffsetEditorEventHandler EventUpdateOffsets;

        private InputRequest.InputRequestDelegate InputRequest;
        private List<Int32> Offsets;

        public OffsetEditor()
        {
            Offsets = new List<Int32>();

            // Rare exception to our MVP where the presenter is created from the base rather than the GUI
            OffsetEditorPresenter OffsetEditorPresenter = new OffsetEditorPresenter(null, this);
            InputRequest = OffsetEditorPresenter.GetInputRequestCallBack();
        }

        public void OnGUIOpen()
        {
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            OffsetEditorEventArgs Args = new OffsetEditorEventArgs();
            Args.Offsets = Offsets;
            EventUpdateOffsets?.Invoke(this, Args);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext Context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext Context, IServiceProvider Provider, Object Value)
        {
            if (InputRequest == null || (Value != null && !Value.GetType().IsAssignableFrom(typeof(IEnumerable<Int32>))))
                return Value;

            Offsets = Value == null ? new List<Int32>() : (Value as IEnumerable<Int32>).ToList();

            UpdateGUI();

            // Call delegate function to request the offsets be edited by the user
            if (InputRequest() == DialogResult.OK)
                return Offsets;

            return Value;
        }

        public void DeleteOffsets(IEnumerable<Int32> Indicies)
        {
            foreach (Int32 Index in Indicies?.OrderByDescending(X => X))
            {
                if (Index < Offsets.Count)
                    Offsets.RemoveAt(Index);
            }

            UpdateGUI();
        }

        public void AddOffset(Int32 Offset)
        {
            Offsets.Add(Offset);

            UpdateGUI();
        }

    } // End class

} // End namespace