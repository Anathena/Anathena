using Anathema.GUI.Tools.TypeEditors;
using Anathema.Source.Engine.InputCapture;
using Anathema.Source.Project.ProjectItems.TypeEditors;
using Anathema.Source.Utils.MVP;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anathema.Source.Project
{
    delegate void HotKeyEditorEventHandler(Object Sender, HotKeyEditorEventArgs Args);
    class HotKeyEditorEventArgs : EventArgs
    {
        public HotKeys HotKeys;
        public HashSet<Key> PendingKeys;
    }

    interface IHotKeyEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void SetPendingKeys(String PendingKeys);
        void SetHotKeyList(IEnumerable<Tuple<String, String>> HotKeyList);
    }

    interface IHotKeyEditorModel : IModel
    {
        // Events triggered by the model (upstream)
        event HotKeyEditorEventHandler EventUpdatePendingKeys;
        event HotKeyEditorEventHandler EventUpdateHotKeys;

        // Functions invoked by presenter (downstream)
        void AddHotKey();
        void ClearInput();
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
            Model.EventUpdatePendingKeys += EventUpdatePendingKeys;

            Model.OnGUIOpen();
        }

        public InputRequest.InputRequestDelegate GetInputRequestCallBack()
        {
            return InputRequest;
        }

        #region Method definitions called by the view (downstream)

        public void AddHotKey()
        {
            Model.AddHotKey();
        }

        public void ClearInput()
        {
            Model.ClearInput();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private DialogResult InputRequest()
        {
            return GUIHotKeyEditor.ShowDialog();
        }

        private void EventUpdatePendingKeys(Object Sender, HotKeyEditorEventArgs E)
        {
            String PendingKeys = String.Empty;

            if (E == null)
                return;

            foreach (Key Key in E.PendingKeys)
            {
                PendingKeys += Key.ToString() + "+";
            }

            View?.SetPendingKeys(PendingKeys.Trim('+'));
        }

        private void EventUpdateHotKeys(Object Sender, HotKeyEditorEventArgs E)
        {
            List<Tuple<String, String>> HotKeyActionPairs = new List<Tuple<String, String>>();

            if (E == null || E.HotKeys == null)
                return;

            foreach (Key Key in E.HotKeys.GetActivationKeys())
            {
                HotKeyActionPairs.Add(new Tuple<String, String>(Key.ToString(), "Action"));
            }

            View?.SetHotKeyList(HotKeyActionPairs);
        }

        #endregion

    } // End class

} // End namespace