using Anathema.GUI.Tools.TypeEditors;
using Anathema.Source.Engine.InputCapture;
using Anathema.Source.Project.ProjectItems.TypeEditors;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Anathema.Source.Project
{
    delegate void HotKeyEditorEventHandler(Object Sender, HotKeyEditorEventArgs Args);
    class HotKeyEditorEventArgs : EventArgs
    {
        public HotKeys HotKeys;
    }

    interface IHotKeyEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void SetHotKeyList(IEnumerable<String> HotKeyList);
    }

    interface IHotKeyEditorModel : IModel
    {
        // Events triggered by the model (upstream)
        event HotKeyEditorEventHandler EventUpdateHotKeys;

        // Functions invoked by presenter (downstream)
        void BeginRecordInput();
        void ApplyCurrentSet();
        void EndRecordInput();
    }

    class HotKeyEditorPresenter : Presenter<IHotKeyEditorView, IHotKeyEditorModel>
    {
        private GUIHotKeyEditor GUIHotKeyEditor;
        private new IHotKeyEditorView View { get; set; }
        private new IHotKeyEditorModel Model { get; set; }

        public HotKeyEditorPresenter(IHotKeyEditorView View, IHotKeyEditorModel Model) : base(View, Model)
        {
            GUIHotKeyEditor = new GUIHotKeyEditor(this);
            this.View = GUIHotKeyEditor;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateHotKeys += EventUpdateHotKeys;

            Model.OnGUIOpen();
        }

        public InputRequest.InputRequestDelegate GetInputRequestCallBack()
        {
            return InputRequest;
        }

        #region Method definitions called by the view (downstream)

        public void BeginRecordInput()
        {
            Model.BeginRecordInput();
        }

        void ApplyCurrentSet()
        {
            Model.ApplyCurrentSet();
        }

        void EndRecordInput()
        {
            Model.EndRecordInput();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private DialogResult InputRequest()
        {
            return GUIHotKeyEditor.ShowDialog();
        }

        private void EventUpdateHotKeys(Object Sender, HotKeyEditorEventArgs E)
        {
            View?.SetHotKeyList(E?.HotKeys?.GetActivationKeys()?.Select(X => X.ToString())?.ToList());
        }

        #endregion

    } // End class

} // End namespace