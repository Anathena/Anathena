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

        private EngineCore EngineCore;

        private InputRequest.InputRequestDelegate InputRequest;
        private HashSet<Key> PendingKeys;
        private HotKeys HotKeys;
        private Boolean IsRecording;

        public HotKeyEditor()
        {
            HotKeys = new HotKeys();
            PendingKeys = new HashSet<Key>();
            IsRecording = false;

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
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            HotKeyEditorEventArgs Args = new HotKeyEditorEventArgs();
            Args.HotKeys = HotKeys;
            EventUpdateHotKeys?.Invoke(this, Args);
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

            UpdateGUI();

            // Call delegate function to request the hotkeys be edited by the user
            if (InputRequest != null && InputRequest() == DialogResult.OK)
                return HotKeys;

            return Value;
        }

        public void BeginRecordInput()
        {
            IsRecording = true;
        }

        public void ApplyCurrentSet()
        {
            HotKeys.SetActivationKeys(PendingKeys);

            EndRecordInput();
        }

        public void EndRecordInput()
        {
            IsRecording = false;
        }

        public void OnKeyDown(Key Key) { }

        public void OnKeyUp(Key Key) { }

        public void OnKeyPress(Key Key)
        {
            if (!IsRecording)
                return;

            PendingKeys.Add(Key);
        }

    } // End class

} // End namespace