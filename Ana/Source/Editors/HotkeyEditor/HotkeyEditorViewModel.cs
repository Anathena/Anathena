namespace Ana.Source.Editors.HotkeyEditor
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
    /// View model for the Script Editor.
    /// </summary>
    internal class HotkeyEditorViewModel : ToolViewModel, IKeyboardObserver, IControllerObserver, IMouseObserver
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(HotkeyEditorViewModel);

        /// <summary>
        /// The collection of hotkeys.
        /// </summary>
        private List<IHotkey> hotkeys;

        /// <summary>
        /// The active hotkey being edited.
        /// </summary>
        private IHotkey activeHotkey;

        /// <summary>
        /// The keyboard hotkey being constructed.
        /// </summary>
        private KeyboardHotkey keyboardHotkey;

        /// <summary>
        /// The controller hotkey being constructed.
        /// TODO: Unused right now.
        /// </summary>
        private ControllerHotkey controllerHotkey;

        /// <summary>
        /// The mouse hotkey being constructed.
        /// TODO: Unused right now.
        /// </summary>
        private MouseHotKey mouseHotkey;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyEditorViewModel" /> class.
        /// </summary>
        public HotkeyEditorViewModel() : base("Hotkey Editor")
        {
            this.ContentId = HotkeyEditorViewModel.ToolContentId;
            this.AddHotkeyCommand = new RelayCommand(() => this.AddHotkey(), () => true);
            this.RemoveHotkeyCommand = new RelayCommand(() => this.RemoveSelectedHotkey(), () => true);
            this.ClearHotkeysCommand = new RelayCommand(() => this.ClearActiveHotkey(), () => true);
            this.keyboardHotkey = new KeyboardHotkey();
            this.AccessLock = new Object();

            Task.Run(() => EngineCore.GetInstance()?.Input?.GetKeyboardCapture().Subscribe(this));
            Task.Run(() => EngineCore.GetInstance()?.Input?.GetControllerCapture().Subscribe(this));
            Task.Run(() => EngineCore.GetInstance()?.Input?.GetMouseCapture().Subscribe(this));
            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets a command to add the active hotkey.
        /// </summary>
        public ICommand AddHotkeyCommand { get; private set; }

        /// <summary>
        /// Gets a command to remove the selected hotkey.
        /// </summary>
        public ICommand RemoveHotkeyCommand { get; private set; }

        /// <summary>
        /// Gets a command to clear the hotkeys collection.
        /// </summary>
        public ICommand ClearHotkeysCommand { get; private set; }

        /// <summary>
        /// Gets a command to update the active hotkey value.
        /// </summary>
        public ICommand UpdateActiveValueCommand { get; private set; }

        /// <summary>
        /// Gets or sets the active hotkey being edited.
        /// </summary>
        public IHotkey ActiveHotkey
        {
            get
            {
                return this.activeHotkey;
            }

            set
            {
                this.activeHotkey = value;
                this.RaisePropertyChanged(nameof(this.ActiveHotkey));
            }
        }

        /// <summary>
        /// Gets or sets the collection of hotkeys.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the index of the selected hotkey.
        /// </summary>
        public Int32 SelectedHotkeyIndex { get; set; }

        /// <summary>
        /// Gets or sets the lock for the hotkey collection access.
        /// </summary>
        private Object AccessLock { get; set; }

        /// <summary>
        /// Event received when a key is pressed.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        public void OnKeyPress(SharpDX.DirectInput.Key key)
        {
            this.keyboardHotkey.ActivationKeys.Add(key);
            this.ActiveHotkey = this.keyboardHotkey;
        }

        /// <summary>
        /// Event received when a key is released.
        /// </summary>
        /// <param name="key">The key that was released.</param>
        public void OnKeyRelease(SharpDX.DirectInput.Key key)
        {
        }

        /// <summary>
        /// Event received when a key is down.
        /// </summary>
        /// <param name="key">The key that is down.</param>
        public void OnKeyDown(SharpDX.DirectInput.Key key)
        {
        }

        /// <summary>
        /// Event received when a set of keys are down.
        /// </summary>
        /// <param name="pressedKeys">The down keys.</param>
        public void OnUpdateAllDownKeys(HashSet<SharpDX.DirectInput.Key> pressedKeys)
        {
        }

        /// <summary>
        /// Adds the active hotkey to the hotkey collection.
        /// </summary>
        private void AddHotkey()
        {
            lock (this.AccessLock)
            {
                if (this.ActiveHotkey != null && this.ActiveHotkey.HasHotkey())
                {
                    this.hotkeys.Add(this.ActiveHotkey);
                    this.ClearActiveHotkey();
                }
            }

            this.RaisePropertyChanged(nameof(this.Hotkeys));
        }

        /// <summary>
        /// Removes the selected hotkey from the hotkey collection.
        /// </summary>
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

        /// <summary>
        /// Clears the active hotkey value.
        /// </summary>
        private void ClearActiveHotkey()
        {
            lock (this.AccessLock)
            {
                this.keyboardHotkey = new KeyboardHotkey();
                this.ActiveHotkey = this.keyboardHotkey;
            }

            this.RaisePropertyChanged(nameof(this.Hotkeys));
        }

        /// <summary>
        /// Updates the active hotkey value.
        /// </summary>
        /// <param name="hotkey">The new active hotkey.</param>
        private void UpdateActiveValue(IHotkey hotkey)
        {
            this.ActiveHotkey = hotkey;
        }
    }
    //// End class
}
//// End namespace