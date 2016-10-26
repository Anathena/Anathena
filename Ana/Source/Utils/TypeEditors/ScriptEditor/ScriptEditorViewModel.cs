namespace Ana.Source.Utils.ScriptEditor
{
    using Docking;
    using Main;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// View model for the Script Editor
    /// </summary>
    internal class ScriptEditorViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(ScriptEditorViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="ScriptEditorViewModel" /> class
        /// </summary>
        private static Lazy<ScriptEditorViewModel> scriptEditorViewModelInstance = new Lazy<ScriptEditorViewModel>(
                () => { return new ScriptEditorViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="ScriptEditorViewModel" /> class from being created
        /// </summary>
        private ScriptEditorViewModel() : base("Script Editor")
        {
            this.ContentId = ScriptEditorViewModel.ToolContentId;

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ScriptEditorViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ScriptEditorViewModel GetInstance()
        {
            return ScriptEditorViewModel.scriptEditorViewModelInstance.Value;
        }
    }
    //// End class
}
//// End namespace