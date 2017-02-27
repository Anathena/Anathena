namespace Ana.Source.HotkeyManager
{
    using Docking;
    using Editors.HotkeyEditor;
    using Engine.Input.HotKeys;
    using Main;
    using Mvvm.Command;
    using Project.ProjectItems;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Hotkey Manager.
    /// </summary>
    internal class HotkeyManagerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(HotkeyManagerViewModel);

        private List<ProjectItemHotkey> hotKeys;

        /// <summary>
        /// Singleton instance of the <see cref="HotkeyManagerViewModel" /> class.
        /// </summary>
        private static Lazy<HotkeyManagerViewModel> inputCorrelatorViewModelInstance = new Lazy<HotkeyManagerViewModel>(
                () => { return new HotkeyManagerViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="HotkeyManagerViewModel" /> class from being created.
        /// </summary>
        private HotkeyManagerViewModel() : base("Hotkey Manager")
        {
            this.ContentId = HotkeyManagerViewModel.ToolContentId;
            this.hotKeys = new List<ProjectItemHotkey>();

            this.NewHotkeyCommand = new RelayCommand(() => this.NewHotkey(), () => true);

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
    }
    //// End class
}
//// End namespace