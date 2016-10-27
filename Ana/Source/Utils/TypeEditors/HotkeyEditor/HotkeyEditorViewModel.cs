namespace Ana.Source.Utils.HotkeyEditor
{
    using Docking;
    using Engine.Input.Controller;
    using Engine.Input.HotKeys;
    using Engine.Input.Keyboard;
    using Engine.Input.Mouse;
    using Main;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
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
        /// Singleton instance of the <see cref="HotkeyEditorViewModel" /> class
        /// </summary>
        private static Lazy<HotkeyEditorViewModel> hotkeyEditorViewModelInstance = new Lazy<HotkeyEditorViewModel>(
                () => { return new HotkeyEditorViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="HotkeyEditorViewModel" /> class from being created
        /// </summary>
        private HotkeyEditorViewModel() : base("Hotkey Editor")
        {
            this.ContentId = HotkeyEditorViewModel.ToolContentId;
            this.AccessLock = new Object();

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        public ICommand AddHotkeyCommand { get; private set; }

        public ICommand RemoveHotkeyCommand { get; private set; }

        public ICommand UpdateActiveValueCommand { get; private set; }

        public ObservableCollection<IHotkey> Hotkeys
        {
            get
            {
                lock (this.AccessLock)
                {
                    return new ObservableCollection<IHotkey>(this.hotkeys);
                }
            }

            set
            {
                this.hotkeys = value == null ? new List<IHotkey>() : new List<IHotkey>(value);
                this.RaisePropertyChanged(nameof(this.Hotkeys));
            }
        }

        private Object AccessLock { get; set; }

        public Int32 SelectedHotkeyIndex { get; set; }

        private IHotkey ActiveHotkeyValue { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="HotkeyEditorViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static HotkeyEditorViewModel GetInstance()
        {
            return HotkeyEditorViewModel.hotkeyEditorViewModelInstance.Value;
        }

        public void OnKeyPress(SharpDX.DirectInput.Key key)
        {
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

        private void AddHotkey()
        {
            lock (this.AccessLock)
            {
                this.hotkeys.Add(this.ActiveHotkeyValue);
            }

            this.RaisePropertyChanged(nameof(this.Hotkeys));
        }

        private void RemoveSelectedOffset()
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

        private void UpdateActiveValue(IHotkey hotkey)
        {
            this.ActiveHotkeyValue = hotkey;
        }
    }
    //// End class
}
//// End namespace