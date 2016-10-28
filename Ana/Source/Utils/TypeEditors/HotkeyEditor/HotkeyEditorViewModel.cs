namespace Ana.Source.Utils.HotkeyEditor
{
    using Docking;
    using Engine;
    using Engine.Input.Controller;
    using Engine.Input.HotKeys;
    using Engine.Input.Keyboard;
    using Engine.Input.Mouse;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Script Editor
    /// </summary>
    internal class HotkeyEditorViewModel : ToolViewModel, IKeyboardObserver, IControllerObserver, IMouseObserver
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(HotkeyEditorViewModel);

        private List<IHotkey> hotkeys;

        /// <summary>
        /// 
        /// </summary>
        public HotkeyEditorViewModel() : base("Hotkey Editor")
        {
            this.ContentId = HotkeyEditorViewModel.ToolContentId;
            this.AddHotkeyCommand = new RelayCommand(() => AddHotkey(), () => true);
            this.RemoveHotkeyCommand = new RelayCommand(() => RemoveSelectedHotkey(), () => true);
            this.ClearHotkeysCommand = new RelayCommand(() => ClearHotkeys(), () => true);
            this.AccessLock = new Object();
            this.InitializeListeners();

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        public ICommand AddHotkeyCommand { get; private set; }

        public ICommand RemoveHotkeyCommand { get; private set; }

        public ICommand ClearHotkeysCommand { get; private set; }

        public ICommand UpdateActiveValueCommand { get; private set; }

        public ObservableCollection<IHotkey> Hotkeys
        {
            get
            {
                lock (this.AccessLock)
                {
                    if (this.hotkeys == null)
                    {
                        this.hotkeys = new List<IHotkey>();
                    }

                    return new ObservableCollection<IHotkey>(this.hotkeys);
                }
            }

            set
            {
                lock (this.AccessLock)
                {
                    this.hotkeys = value == null ? new List<IHotkey>() : new List<IHotkey>(value);
                    this.RaisePropertyChanged(nameof(this.Hotkeys));
                }
            }
        }

        private Object AccessLock { get; set; }

        public Int32 SelectedHotkeyIndex { get; set; }

        private IHotkey ActiveHotkeyValue { get; set; }

        public void OnKeyPress(SharpDX.DirectInput.Key key)
        {
            this.ActiveHotkeyValue = new KeyboardHotkey(key);
        }

        public void OnKeyRelease(SharpDX.DirectInput.Key key)
        {
        }

        public void OnKeyDown(SharpDX.DirectInput.Key key)
        {
        }

        public void OnUpdateAllDownKeys(HashSet<SharpDX.DirectInput.Key> pressedKeys)
        {
        }

        private void InitializeListeners()
        {
            EngineCore.GetInstance()?.Input?.GetKeyboardCapture().Subscribe(this);
            EngineCore.GetInstance()?.Input?.GetControllerCapture().Subscribe(this);
            EngineCore.GetInstance()?.Input?.GetMouseCapture().Subscribe(this);
        }

        private void AddHotkey()
        {
            lock (this.AccessLock)
            {
                if (this.ActiveHotkeyValue != null)
                {
                    this.hotkeys.Add(this.ActiveHotkeyValue);
                }
            }

            this.RaisePropertyChanged(nameof(this.Hotkeys));
        }

        private void RemoveSelectedHotkey()
        {
            Int32 removalIndex = this.SelectedHotkeyIndex;

            lock (this.AccessLock)
            {
                if (removalIndex < 0)
                {
                    removalIndex = 0;
                }

                if (removalIndex < this.hotkeys.Count)
                {
                    this.hotkeys.RemoveAt(removalIndex);
                }
            }

            this.RaisePropertyChanged(nameof(this.Hotkeys));
        }

        private void ClearHotkeys()
        {
            lock (this.AccessLock)
            {
                this.hotkeys.Clear();
            }

            this.RaisePropertyChanged(nameof(this.Hotkeys));
        }

        private void UpdateActiveValue(IHotkey hotkey)
        {
            this.ActiveHotkeyValue = hotkey;
        }
    }
    //// End class
}
//// End namespace