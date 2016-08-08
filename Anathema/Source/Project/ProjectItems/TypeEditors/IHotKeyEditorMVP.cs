using Anathema.GUI.Tools.TypeEditors;
using Anathema.Source.Engine.InputCapture.HotKeys;
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
        public IEnumerable<IHotKey> HotKeys;
        public IHotKey PendingHotKey;
    }

    interface IHotKeyEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void SetPendingKeys(String PendingKeys);
        void SetHotKeyList(IEnumerable<String> HotKeyList);
    }

    interface IHotKeyEditorModel : IModel
    {
        // Events triggered by the model (upstream)
        event HotKeyEditorEventHandler EventUpdatePendingKeys;
        event HotKeyEditorEventHandler EventUpdateHotKeys;

        // Functions invoked by presenter (downstream)
        void OnClose();
        void AddHotKey();
        void DeleteHotKeys(IEnumerable<Int32> Indicies);
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

        public void OnClose()
        {
            Model.OnClose();
        }

        public void AddHotKey()
        {
            Model.AddHotKey();
        }

        public void DeleteHotKeys(IEnumerable<Int32> Indicies)
        {
            if (Indicies == null || Indicies.Count() <= 0)
                return;

            Model.DeleteHotKeys(Indicies);
        }

        public void ClearInput()
        {
            Model.ClearInput();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private DialogResult InputRequest()
        {
            Model.OnGUIOpen();
            return GUIHotKeyEditor.ShowDialog();
        }

        private void EventUpdatePendingKeys(Object Sender, HotKeyEditorEventArgs E)
        {
            String HotKeyString = String.Empty;

            if (E == null || E.PendingHotKey == null)
            {
                View?.SetPendingKeys(HotKeyString);
                return;
            }

            if (E.PendingHotKey.GetType().IsAssignableFrom(typeof(KeyboardHotKey)))
            {
                KeyboardHotKey KeyboardHotKey = E.PendingHotKey as KeyboardHotKey;

                HotKeyString = KeyboardHotKey.ToString();
            }
            else if (E.PendingHotKey.GetType().IsAssignableFrom(typeof(ControllerHotKey)))
            {
                ControllerHotKey ControllerHotKey = E.PendingHotKey as ControllerHotKey;
            }
            else if (E.PendingHotKey.GetType().IsAssignableFrom(typeof(MouseHotKey)))
            {
                MouseHotKey MouseHotKey = E.PendingHotKey as MouseHotKey;
            }

            View?.SetPendingKeys(HotKeyString);
        }

        private void EventUpdateHotKeys(Object Sender, HotKeyEditorEventArgs E)
        {
            List<String> HotKeyStrings = new List<String>();

            if (E == null || E.HotKeys == null)
            {
                View?.SetHotKeyList(HotKeyStrings);
                return;
            }

            foreach (IHotKey HotKey in E.HotKeys)
            {
                if (HotKey.GetType().IsAssignableFrom(typeof(KeyboardHotKey)))
                {
                    KeyboardHotKey KeyboardHotKey = HotKey as KeyboardHotKey;

                    HotKeyStrings.Add(KeyboardHotKey.ToString());
                }
                else if (HotKey.GetType().IsAssignableFrom(typeof(ControllerHotKey)))
                {
                    ControllerHotKey ControllerHotKey = HotKey as ControllerHotKey;
                }
                else if (HotKey.GetType().IsAssignableFrom(typeof(MouseHotKey)))
                {
                    MouseHotKey MouseHotKey = HotKey as MouseHotKey;
                }
            }

            View?.SetHotKeyList(HotKeyStrings);
        }

        #endregion

    } // End class

} // End namespace