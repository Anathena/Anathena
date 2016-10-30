namespace Ana.Source.Scanners.ValueCollector
{
    using Mvvm;
    using Mvvm.Command;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Collect values for the current snapshot, or a new one if none exists. The values are then assigned to a new snapshot.
    /// </summary>
    internal class ValueCollectorViewModel : ViewModelBase
    {
        /// <summary>
        /// Singleton instance of the <see cref="ValueCollectorViewModel" /> class
        /// </summary>
        private static Lazy<ValueCollectorViewModel> valueCollectorViewModelInstance = new Lazy<ValueCollectorViewModel>(
                () => { return new ValueCollectorViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueCollectorViewModel" /> class
        /// </summary>
        public ValueCollectorViewModel()
        {
            this.ValueCollectorModel = new ValueCollectorModel();
            this.CollectValuesCommand = new RelayCommand(() => Task.Run(() => this.CollectValues()), () => true);
        }

        /// <summary>
        /// Gets the command to collect values
        /// </summary>
        public ICommand CollectValuesCommand { get; private set; }

        /// <summary>
        /// Gets or sets the model for the value collector scan
        /// </summary>
        private ValueCollectorModel ValueCollectorModel { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ValueCollectorViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ValueCollectorViewModel GetInstance()
        {
            return valueCollectorViewModelInstance.Value;
        }

        /// <summary>
        /// Begins the value collection
        /// </summary>
        private void CollectValues()
        {
            this.ValueCollectorModel.Begin();
        }
    }
    //// End class
}
//// End namespace