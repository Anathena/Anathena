using Ana.Source.Engine.InputCapture.HotKeys;
using System;
using System.Collections.Generic;

namespace Ana.Source.Scanners.InputCorrelator
{
    delegate void InputCorrelatorEventHandler(Object Sender, InputCorrelatorEventArgs Args);
    class InputCorrelatorEventArgs : EventArgs
    {
        public IEnumerable<IHotKey> HotKeys;
    }

    interface IInputCorrelatorView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void SetHotKeyList(IEnumerable<String> HotKeyList);
    }

    abstract class IInputCorrelatorModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event InputCorrelatorEventHandler EventUpdateHotKeys;
        protected virtual void OnEventUpdateHotKeys(InputCorrelatorEventArgs E)
        {
            EventUpdateHotKeys?.Invoke(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void EditKeys();
        public abstract void SetVariableSize(Int32 VariableSize);
    }

    class InputCorrelatorPresenter : ScannerPresenter
    {
        private new IInputCorrelatorView view { get; set; }
        private new IInputCorrelatorModel model { get; set; }

        public InputCorrelatorPresenter(IInputCorrelatorView view, IInputCorrelatorModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventUpdateHotKeys += EventUpdateHotKeys;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void EditKeys()
        {
            model.EditKeys();
        }

        public void SetVariableSize(Int32 VariableSize)
        {
            if (VariableSize <= 0)
                return;

            model.SetVariableSize(VariableSize);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateHotKeys(Object Sender, InputCorrelatorEventArgs E)
        {
            List<String> HotKeyStrings = new List<String>();

            if (E == null || E.HotKeys == null)
            {
                view?.SetHotKeyList(HotKeyStrings);
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

                    HotKeyStrings.Add(ControllerHotKey.ToString());
                }
                else if (HotKey.GetType().IsAssignableFrom(typeof(MouseHotKey)))
                {
                    MouseHotKey MouseHotKey = HotKey as MouseHotKey;

                    HotKeyStrings.Add(MouseHotKey.ToString());
                }
            }

            view?.SetHotKeyList(HotKeyStrings);
        }

        #endregion

    } // End class

} // End namespace