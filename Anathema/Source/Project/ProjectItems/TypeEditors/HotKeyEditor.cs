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
using System.Windows.Forms.Design;

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
            RefreshGUI();
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext Context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override Object EditValue(ITypeDescriptorContext Context, System.IServiceProvider Provider, Object Value)
        {
            IWindowsFormsEditorService Service = Provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            HotKeys HotKeys = Value as HotKeys;

            if (InputRequest == null || Service == null)
                return Value;

            if (InputRequest() == DialogResult.OK)
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

        private void RefreshGUI()
        {
            HotKeyEditorEventArgs Args = new HotKeyEditorEventArgs();
            Args.HotKeys = HotKeys;
            EventUpdateHotKeys?.Invoke(this, Args);
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