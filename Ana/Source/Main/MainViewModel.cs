namespace Ana.Source.Main
{
    using Docking;
    using Mvvm;
    using Mvvm.Command;
    using System.Collections.Generic;
    using System.Windows.Input;

    internal class MainViewModel : ViewModelBase
    {
        private ProcessSelectorViewModel processSelectorViewModel;
        private ToolViewModel[] tools;

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

        private void OpenProjectExecute()
        {

        }
    }
    //// End class
}
//// End namesapce