namespace Ana.Source.Main
{
    using Docking;
    using Mvvm;
    using Mvvm.Command;
    using ProcessSelector;
    using System.Collections.Generic;
    using System.Windows.Input;

    /// <summary>
    /// Main view model
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// View model for the Process Selector
        /// </summary>
        private ProcessSelectorViewModel processSelectorViewModel;

        /// <summary>
        /// Collection of tools contained in the main docking panel
        /// </summary>
        private ToolViewModel[] tools;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class
        /// </summary>
        public MainViewModel()
        {
            this.tools = null;
            this.processSelectorViewModel = null;

            this.OpenProject = new RelayCommand(() => this.OpenProjectExecute(), () => true);
        }

        /// <summary>
        /// Gets the command to create a pop up
        /// </summary>
        public ICommand OpenProject { get; private set; }

        /// <summary>
        /// Gets the tools contained in the main docking panel
        /// </summary>
        public IEnumerable<ToolViewModel> Tools
        {
            get
            {
                if (this.tools == null)
                {
                    this.tools = new ToolViewModel[] { this.ProcessSelectorViewModel };
                }

                return this.tools;
            }
        }

        /// <summary>
        /// Gets the view model for the Process Selector
        /// </summary>
        public ProcessSelectorViewModel ProcessSelectorViewModel
        {
            get
            {
                if (this.processSelectorViewModel == null)
                {
                    this.processSelectorViewModel = new ProcessSelectorViewModel();
                }

                return this.processSelectorViewModel;
            }
        }

        /// <summary>
        /// Method to open a project from disk
        /// </summary>
        private void OpenProjectExecute()
        {
        }
    }
    //// End class
}
//// End namesapce