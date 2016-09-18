namespace Ana.Source.Mvvm
{
    using Docking;
    using Main;
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
        /// Main view model
        /// </summary>
        private static Workspace workspace;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            workspace = Workspace.This;
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

        /// <summary>
        /// Gets the Main property which defines the main view model
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public Workspace Workspace
        {
            get
            {
                return workspace;
            }
        }
    }
    //// End class
}
//// End namespace