namespace Ana.Source.UserSettings
{
    using Docking;
    using Main;
    using System;
    using System.Threading;

    /// <summary>
    /// View model for the Settings
    /// </summary>
    internal class SettingsViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(SettingsViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="SettingsViewModel" /> class
        /// </summary>
        private static Lazy<SettingsViewModel> settingsViewModelInstance = new Lazy<SettingsViewModel>(
                () => { return new SettingsViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="SettingsViewModel" /> class from being created
        /// </summary>
        private SettingsViewModel() : base("Settings")
        {
            this.ContentId = ToolContentId;

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="SettingsViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static SettingsViewModel GetInstance()
        {
            return settingsViewModelInstance.Value;
        }
    }
    //// End class
}
//// End namespace