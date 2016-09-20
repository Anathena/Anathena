using Ana.Source.Main;
using Ana.Source.ProcessSelector;

namespace Ana.View
{
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
            // TODO: Figure out how to remove these without WPF glitching out like a piece of shit
            mainViewModel = MainViewModel.GetInstance();
            processSelectorViewModel = new ProcessSelectorViewModel();
        }

        /// <summary>
        /// Gets the Main property which defines the main view model
        /// </summary>
        public MainViewModel MainViewModel
        {
            get
            {
                if (mainViewModel == null)
                {
                    mainViewModel = MainViewModel.GetInstance();
                }

                return mainViewModel;
            }
        }

        /// <summary>
        /// Gets the Main property which defines the main view model
        /// </summary>
        public ProcessSelectorViewModel ProcessSelectorViewModel
        {
            get
            {
                if (processSelectorViewModel == null)
                {
                    processSelectorViewModel = new ProcessSelectorViewModel();
                }

                return processSelectorViewModel;
            }
        }
    }
    //// End class
}
//// End namespace