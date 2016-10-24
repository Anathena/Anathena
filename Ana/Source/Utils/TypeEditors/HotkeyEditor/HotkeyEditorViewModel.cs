namespace Ana.Source.Utils.HotkeyEditor
{
    using Docking;
    using Main;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// View model for the Script Editor
    /// </summary>
    internal class HotkeyEditorViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(HotkeyEditorViewModel);

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
            this.ContentId = ToolContentId;

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="HotkeyEditorViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static HotkeyEditorViewModel GetInstance()
        {
            return HotkeyEditorViewModel.hotkeyEditorViewModelInstance.Value;
        }
    }
    //// End class
}
//// End namespace