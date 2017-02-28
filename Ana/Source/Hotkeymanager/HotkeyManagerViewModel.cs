namespace Ana.Source.HotkeyManager
{
    using Docking;
    using Editors.HotkeyEditor;
    using Engine;
    using Engine.Input.Controller;
    using Engine.Input.HotKeys;
    using Engine.Input.Keyboard;
    using Engine.Input.Mouse;
    using Main;
    using Mvvm.Command;
    using Project.ProjectItems;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Hotkey Manager.
    /// </summary>
    internal class HotkeyManagerViewModel : ToolViewModel, IKeyboardObserver, IControllerObserver, IMouseObserver
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(HotkeyManagerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="HotkeyManagerViewModel" /> class.
        /// </summary>
        private static Lazy<HotkeyManagerViewModel> inputCorrelatorViewModelInstance = new Lazy<HotkeyManagerViewModel>(
                () => { return new HotkeyManagerViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        private List<ProjectItemHotkey> hotKeys;

        /// <summary>
        /// Prevents a default instance of the <see cref="HotkeyManagerViewModel" /> class from being created.
        /// </summary>
        private HotkeyManagerViewModel() : base("Hotkey Manager")
        {
            this.ContentId = HotkeyManagerViewModel.ToolContentId;
            this.NewHotkeyCommand = new RelayCommand(() => this.NewHotkey(), () => true);
            this.hotKeys = new List<ProjectItemHotkey>();

            EngineCore.GetInstance().Input?.GetKeyboardCapture().Subscribe(this);
            MainViewModel.GetInstance().Subscribe(this);
        }

        public ICommand NewHotkeyCommand { get; private set; }

        public ObservableCollection<ProjectItemHotkey> Hotkeys
        {
            get
            {
                return new ObservableCollection<ProjectItemHotkey>(this.hotKeys == null ? new List<ProjectItemHotkey>() : this.hotKeys);
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="HotkeyManagerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static HotkeyManagerViewModel GetInstance()
        {
            return inputCorrelatorViewModelInstance.Value;
        }

        private void NewHotkey()
        {
            HotkeyEditorModel hotkeyEditor = new HotkeyEditorModel();
            IHotkey newHotkey = hotkeyEditor.EditValue(context: null, provider: null, value: null) as IHotkey;

            if (newHotkey != null)
            {
                this.hotKeys.Add(new ProjectItemHotkey(newHotkey));
                this.RaisePropertyChanged(nameof(this.Hotkeys));
            }
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
            foreach (ProjectItemHotkey projectItemHotkey in this.hotKeys)
            {
                if (projectItemHotkey?.Hotkey is KeyboardHotkey)
                {
                    KeyboardHotkey keyboardHotkey = projectItemHotkey.Hotkey as KeyboardHotkey;

                    if (keyboardHotkey.ActivationKeys.All(x => pressedKeys.Contains(x)))
                    {
                        projectItemHotkey.Activate();
                    }
                }
            }
        }
    }
    //// End class
}
//// End namespace