namespace Ana.Source.Utils.TextEditor
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Text Editor.
    /// </summary>
    internal class TextEditorViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(TextEditorViewModel);

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditorViewModel" /> class.
        /// </summary>
        public TextEditorViewModel() : base("Text Editor")
        {
            this.ContentId = TextEditorViewModel.ToolContentId;
            this.UpdateTextCommand = new RelayCommand<String>((text) => this.UpdateText(text), (text) => true);
            this.SaveTextCommand = new RelayCommand<String>((text) => this.SaveText(text), (text) => true);

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets a command to save the active text.
        /// </summary>
        public ICommand SaveTextCommand { get; private set; }

        /// <summary>
        /// Gets a command to update the active text.
        /// </summary>
        public ICommand UpdateTextCommand { get; private set; }

        /// <summary>
        /// Gets the active text.
        /// </summary>
        public String Text { get; private set; }

        /// <summary>
        /// Updates the active text.
        /// </summary>
        /// <param name="text">The raw text.</param>
        private void UpdateText(String text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Saves the provided text.
        /// </summary>
        /// <param name="text">The raw text to save.</param>
        private void SaveText(String text)
        {
            this.UpdateText(text);
        }
    }
    //// End class
}
//// End namespace