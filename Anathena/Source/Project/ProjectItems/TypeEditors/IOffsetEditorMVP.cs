using Ana.GUI.Tools.TypeEditors;
using Ana.Source.Utils;
using Ana.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ana.Source.Project
{
    delegate void OffsetEditorEventHandler(Object Sender, OffsetEditorEventArgs Args);
    class OffsetEditorEventArgs : EventArgs
    {
        public List<Int32> Offsets;
    }

    interface IOffsetEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void SetOffsets(IEnumerable<Tuple<String, String>> HexDecOffsets);
    }

    interface IOffsetEditorModel : IModel
    {
        // Events triggered by the model (upstream)
        event OffsetEditorEventHandler EventUpdateOffsets;

        // Functions invoked by presenter (downstream)
        void DeleteOffsets(IEnumerable<Int32> Indicies);
        void AddOffset(Int32 Offset);
    }

    class OffsetEditorPresenter : Presenter<IOffsetEditorView, IOffsetEditorModel>
    {
        private GUIOffsetEditor GUIOffsetEditor;
        private new IOffsetEditorView view { get; set; }
        private new IOffsetEditorModel model { get; set; }

        public OffsetEditorPresenter(IOffsetEditorView view, IOffsetEditorModel model) : base(view, model)
        {
            GUIOffsetEditor = new GUIOffsetEditor(this);
            this.view = GUIOffsetEditor;
            this.model = model;

            // Bind events triggered by the model
            model.EventUpdateOffsets += EventUpdateOffsets;

            model.OnGUIOpen();
        }

        public InputRequest.InputRequestDelegate GetInputRequestCallBack()
        {
            return InputRequest;
        }

        #region Method definitions called by the view (downstream)

        public void DeleteOffsets(IEnumerable<Int32> Indicies)
        {
            model.DeleteOffsets(Indicies);
        }

        public void AddOffset(Int32 Offset)
        {
            model.AddOffset(Offset);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private DialogResult InputRequest()
        {
            model.OnGUIOpen();
            return GUIOffsetEditor.ShowDialog();
        }

        private void EventUpdateOffsets(Object Sender, OffsetEditorEventArgs E)
        {
            List<Tuple<String, String>> HexDecOffsets = new List<Tuple<String, String>>();

            foreach (Int32 Offset in E?.Offsets)
            {
                String Decimal = Offset.ToString();
                String Hexadecimal = Offset < 0 ? "-" + Math.Abs(Offset).ToString("X") : Offset.ToString("X");
                HexDecOffsets.Add(new Tuple<String, String>(Hexadecimal, Decimal));
            }

            view?.SetOffsets(HexDecOffsets);
        }

        #endregion

    } // End class

} // End namespace
