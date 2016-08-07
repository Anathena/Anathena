using Anathema.Source.Engine;
using Anathema.Source.Engine.InputCapture;
using Anathema.Source.Engine.InputCapture.Keyboard;
using Anathema.Source.Engine.Processes;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Anathema.Source.Project.ProjectItems.TypeEditors
{
    class HotKeyEditor : UITypeEditor, IProcessObserver, IKeyboardObserver, IHotKeyEditorModel
    {
        public event HotKeyEditorEventHandler EventUpdateHotKeys;
        public event HotKeyEditorEventHandler EventUpdatePendingKeys;

        private EngineCore EngineCore;

        private InputRequest.InputRequestDelegate InputRequest;
        private HashSet<Key> PendingKeys;
        private HotKeys HotKeys;

        public HotKeyEditor()
        {
            HotKeys = new HotKeys();
            PendingKeys = new HashSet<Key>();

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
            Args.PendingKeys = PendingKeys;
            EventUpdatePendingKeys?.Invoke(this, Args);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext Context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext Context, IServiceProvider Provider, Object Value)
        {
            if (InputRequest == null || (Value != null && !Value.GetType().IsAssignableFrom(typeof(HotKeys))))
                return Value;

            HotKeys = Value == null ? new HotKeys() : (Value as HotKeys);

            OnUpdateHotKeys();
            OnUpdatePendingKeys();

            // Call delegate function to request the hotkeys be edited by the user
            if (InputRequest != null && InputRequest() == DialogResult.OK)
                return HotKeys;

            return Value;
        }

        public void AddHotKey()
        {
            HotKeys.SetActivationKeys(PendingKeys);

            ClearInput();
            OnUpdateHotKeys();
            OnUpdatePendingKeys();
        }

        public void ClearInput()
        {
            PendingKeys.Clear();
            OnUpdatePendingKeys();
        }

        public void OnKeyRelease(Key Key) { }

        public void OnKeyDown(Key Key) { }

        public void OnKeyPress(Key Key)
        {
            PendingKeys.Add(Key);

            OnUpdatePendingKeys();
        }

    } // End class

} // End namespace