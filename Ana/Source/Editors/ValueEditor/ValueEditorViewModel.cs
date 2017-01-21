namespace Ana.Source.Editors.ValueEditor
{
    using Docking;
    using Main;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// View model for the Value Editor.
    /// </summary>
    internal class ValueEditorViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(ValueEditorViewModel);

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueEditorViewModel" /> class.
        /// </summary>
        public ValueEditorViewModel() : base("Value Editor")
        {
            this.ContentId = ValueEditorViewModel.ToolContentId;

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets or sets the value being edited.
        /// </summary>
        public dynamic Value { get; set; }
    }
    //// End class
}
//// End namespace