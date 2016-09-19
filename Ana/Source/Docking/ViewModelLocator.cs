namespace Ana.Source.Docking
{
    using Main;
    using ProcessSelector;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    internal class ViewModelLocator
    {
        /// <summary>
        /// Main view model
        /// </summary>
        private static MainViewModel mainViewModel;

        /// <summary>
        /// Process selector view model
        /// </summary>
        private static ProcessSelectorViewModel processSelectorViewModel;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            mainViewModel = new MainViewModel();
            processSelectorViewModel = new ProcessSelectorViewModel();
        }

        /// <summary>
        /// Gets the Main property which defines the main view model
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel MainViewModel
        {
            get
            {
                return mainViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property which defines the main view model
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public ProcessSelectorViewModel ProcessSelectorViewModel
        {
            get
            {
                return processSelectorViewModel;
            }
        }
    }
    //// End class
}
//// End namespace