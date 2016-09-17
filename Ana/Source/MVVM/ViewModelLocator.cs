namespace Ana.Source.Mvvm
{
    using Main;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Main view model
        /// </summary>
        private static MainViewModel mainViewModel;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            mainViewModel = new MainViewModel();
        }

        /// <summary>
        /// Gets the Main property which defines the main view model
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return mainViewModel;
            }
        }
    }
    //// End class
}
//// End namespace