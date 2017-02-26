namespace Ana.Source.HotkeyManager
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
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
            this.NewHotkeyCommand = new RelayCommand(() => this.NewHotkey(), () => true);

            MainViewModel.GetInstance().Subscribe(this);
        }

        public ICommand NewHotkeyCommand { get; private set; }

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

        }
    }
    //// End class
}
//// End namespace