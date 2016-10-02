namespace Ana.Source.Docking
{
    using Mvvm.Command;
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Generic view model for all tool panes
    /// </summary>
    internal class ToolViewModel : PaneViewModel
    {
        /// <summary>
        /// Value indicating if tool pane is visible
        /// </summary>
        private Boolean isVisible;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolViewModel" /> class
        /// </summary>
        /// <param name="title">The title to display for the tool pane</param>
        public ToolViewModel(String title)
        {
            this.Title = title;
            this.ToggleVisibility = new RelayCommand(() => this.ToggleVisibilityExecute(), () => true);
        }

        /// <summary>
        /// Gets a command that toggles the visibility of this tool
        /// </summary>
        public ICommand ToggleVisibility { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the tool pane is visible
        /// </summary>
        public Boolean IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                if (this.isVisible != value)
                {
                    this.isVisible = value;
                    this.RaisePropertyChanged(nameof(this.IsVisible));
                }
            }
        }

        /// <summary>
        /// Toggles the visibility of this tool
        /// </summary>
        public void ToggleVisibilityExecute()
        {
            this.IsVisible = !this.IsVisible;
        }
    }
    //// End class
}
//// End namespace