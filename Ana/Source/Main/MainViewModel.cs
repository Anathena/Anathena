namespace Ana.Source.Main
{
    using Docking;
    using Mvvm;
    using Mvvm.Command;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Input;

    /// <summary>
    /// Main view model
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Singleton instance of the <see cref="MainViewModel" /> class
        /// </summary>
        private static Lazy<MainViewModel> mainViewModelInstance = new Lazy<MainViewModel>(
                () => { return new MainViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Collection of tools contained in the main docking panel
        /// </summary>
        private HashSet<ToolViewModel> tools;

        /// <summary>
        /// Prevents a default instance of the <see cref="MainViewModel" /> class from being created
        /// </summary>
        private MainViewModel()
        {
            this.tools = new HashSet<ToolViewModel>();
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
                    this.tools = new HashSet<ToolViewModel>();
                }

                return this.tools;
            }
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="MainViewModel" /> class
        /// </summary>
        /// <returns>The singleton instance of the <see cref="MainViewModel" /> class</returns>
        public static MainViewModel GetInstance()
        {
            return mainViewModelInstance.Value;
        }

        /// <summary>
        /// Adds a tool to the list of tools controlled by the main view model
        /// </summary>
        /// <param name="toolViewModel">The tool to be added</param>
        public void Subscribe(ToolViewModel toolViewModel)
        {
            if (toolViewModel != null && !this.tools.Contains(toolViewModel))
            {
                this.tools?.Add(toolViewModel);
            }
        }

        /// <summary>
        /// Removes a tool from the list of tools controlled by the main view model
        /// </summary>
        /// <param name="toolViewModel">The tool to be added</param>
        public void Unsubscribe(ToolViewModel toolViewModel)
        {
            if (toolViewModel != null && this.tools.Contains(toolViewModel))
            {
                this.tools?.Remove(toolViewModel);
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