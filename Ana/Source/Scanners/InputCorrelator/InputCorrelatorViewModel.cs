namespace Ana.Source.ChangeCounter
{
    using Docking;
    using Main;
    using System;
    using System.Threading;

    /// <summary>
    /// View model for the Input Correlator
    /// </summary>
    internal class InputCorrelatorViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(InputCorrelatorViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="InputCorrelatorViewModel" /> class
        /// </summary>
        private static Lazy<InputCorrelatorViewModel> inputCorrelatorViewModelInstance = new Lazy<InputCorrelatorViewModel>(
                () => { return new InputCorrelatorViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="InputCorrelatorViewModel" /> class from being created
        /// </summary>
        private InputCorrelatorViewModel() : base("Input Correlator")
        {
            this.ContentId = ToolContentId;

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ChangeCounterViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static InputCorrelatorViewModel GetInstance()
        {
            return inputCorrelatorViewModelInstance.Value;
        }
    }
    //// End class
}
//// End namespace