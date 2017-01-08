namespace Ana.Source.Utils.ScriptEditor
{
    using Ana.Content.Templates;
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Script Editor.
    /// </summary>
    internal class ScriptEditorViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(ScriptEditorViewModel);

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptEditorViewModel" /> class.
        /// </summary>
        public ScriptEditorViewModel() : base("Script Editor")
        {
            this.ContentId = ScriptEditorViewModel.ToolContentId;
            this.UpdateScriptCommand = new RelayCommand<String>((script) => this.UpdateScript(script), (script) => true);
            this.SaveScriptCommand = new RelayCommand<String>((script) => this.SaveScript(script), (script) => true);

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets a command to save the active script.
        /// </summary>
        public ICommand SaveScriptCommand { get; private set; }

        /// <summary>
        /// Gets a command to update the active script text.
        /// </summary>
        public ICommand UpdateScriptCommand { get; private set; }

        /// <summary>
        /// Gets the active script text.
        /// </summary>
        public String Script { get; private set; }

        /// <summary>
        /// Gets the code injection script template.
        /// </summary>
        /// <returns>The code injection script template.</returns>
        public String GetCodeInjectionTemplate()
        {
            CodeInjectionTemplate codeInjectionTemplate = new CodeInjectionTemplate();

            return codeInjectionTemplate.TransformText();
        }

        /// <summary>
        /// Gets the graphics injection script template.
        /// </summary>
        /// <returns>The graphics injection script template.</returns>
        public String GetGraphicsInjectionTemplate()
        {
            GraphicsInjectionTemplate graphicsInjectionTemplate = new GraphicsInjectionTemplate();

            return graphicsInjectionTemplate.TransformText();
        }

        /// <summary>
        /// Updates the active script.
        /// </summary>
        /// <param name="script">The raw script text.</param>
        private void UpdateScript(String script)
        {
            this.Script = script;
        }

        /// <summary>
        /// Saves the provided script.
        /// </summary>
        /// <param name="script">The raw script to save.</param>
        private void SaveScript(String script)
        {
            this.UpdateScript(script);
        }
    }
    //// End class
}
//// End namespace