using Ana.Source.Engine;
using Ana.Source.Engine.InputCapture.HotKeys;
using Ana.Source.Engine.InputCapture.Keyboard;
using Ana.Source.Engine.Processes;
using Ana.Source.Utils;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

namespace Ana.Source.Project.ProjectItems.TypeEditors
{
    class HotKeyEditor : UITypeEditor, IProcessObserver, IKeyboardObserver, IHotKeyEditorModel
    {
        public event HotKeyEditorEventHandler EventUpdateHotKeys;
        public event HotKeyEditorEventHandler EventUpdatePendingKeys;
        private InputRequest.InputRequestDelegate InputRequest;

        private EngineCore EngineCore;

        private List<IHotKey> HotKeys;
        private IHotKey PendingHotKey;

        public HotKeyEditor()
        {
            HotKeys = new List<IHotKey>();

            InitializeProcessObserver();

            // Rare exception to our MVP where the presenter is created from the base rather than the GUI
            HotKeyEditorPresenter HotKeyEditorPresenter = new HotKeyEditorPresenter(null, this);
            InputRequest = HotKeyEditorPresenter.GetInputRequestCallBack();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
            EngineCore.InputManager.GetKeyboardCapture().Subscribe(this);
        }

        public void OnGUIOpen()
        {
            OnUpdateHotKeys();
            OnUpdatePendingKeys();

            if (EngineCore != null)
                EngineCore.InputManager.GetKeyboardCapture().Subscribe(this);
        }

        public void OnClose()
        {
            if (EngineCore != null)
                EngineCore.InputManager.GetKeyboardCapture().Unsubscribe(this);
        }

        private void OnUpdateHotKeys()
        {
            HotKeyEditorEventArgs Args = new HotKeyEditorEventArgs();
            Args.HotKeys = HotKeys;
            EventUpdateHotKeys?.Invoke(this, Args);
        }

        private void OnUpdatePendingKeys()
        {
            HotKeyEditorEventArgs Args = new HotKeyEditorEventArgs();
            Args.PendingHotKey = PendingHotKey;
            EventUpdatePendingKeys?.Invoke(this, Args);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext Context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext Context, IServiceProvider Provider, Object Value)
        {
            if (InputRequest == null || (Value != null && !Value.GetType().GetInterfaces().Any(X => X.IsAssignableFrom(typeof(IEnumerable<IHotKey>)))))
                return Value;

            if (Value == null)
                HotKeys = new List<IHotKey>();
            else
                HotKeys = (Value as IEnumerable<IHotKey>).ToList();

            if (ShowDialog() == DialogResult.OK)
                return HotKeys;

            return Value;
        }

        public DialogResult ShowDialog()
        {
            ClearInput();
            OnUpdateHotKeys();
            OnUpdatePendingKeys();

            // Call delegate function to request the hotkeys be edited by the user
            if (InputRequest == null)
                return DialogResult.Cancel;

            return InputRequest();
        }

        public void SetHotKeys(IEnumerable<IHotKey> HotKeys)
        {
            if (HotKeys == null)
                this.HotKeys = new List<IHotKey>();
            else
                this.HotKeys = new List<IHotKey>(HotKeys);

            OnUpdateHotKeys();
        }

        public IEnumerable<IHotKey> GetHotKeys()
        {
            return HotKeys;
        }

        public void AddHotKey()
        {
            if (PendingHotKey == null)
                return;

            HotKeys.Add(PendingHotKey);

            ClearInput();
            OnUpdateHotKeys();
            OnUpdatePendingKeys();
        }

        public void DeleteHotKeys(IEnumerable<Int32> Indicies)
        {
            foreach (Int32 Index in Indicies.OrderByDescending(X => X))
                HotKeys.RemoveAt(Index);

            OnUpdateHotKeys();
        }

        public void ClearInput()
        {
            PendingHotKey = null;
            OnUpdatePendingKeys();
        }

        public void OnUpdateAllDownKeys(HashSet<Key> DownKeys) { }

        public void OnKeyRelease(Key Key) { }

        public void OnKeyDown(Key Key) { }

        public void OnKeyPress(Key Key)
        {
            if (PendingHotKey == null || !PendingHotKey.GetType().IsAssignableFrom(typeof(KeyboardHotKey)))
                PendingHotKey = new KeyboardHotKey();

            KeyboardHotKey KeyboardHotKey = PendingHotKey as KeyboardHotKey;

            // Update hotkey to contain new pressed key
            HashSet<Key> ActivationKeys = KeyboardHotKey.GetActivationKeys();
            ActivationKeys.Add(Key);
            KeyboardHotKey.SetActivationKeys(ActivationKeys);

            OnUpdatePendingKeys();
        }

    } // End class

} // End namespace