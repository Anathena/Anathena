namespace Ana.Source.Main
{
    using Mvvm;
    using Mvvm.Command;
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// This class contains properties that the main View can data bind to
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Trash or whatever
        /// </summary>
        private MainModel mainModel;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class
        /// </summary>
        public MainViewModel()
        {
            this.mainModel = new MainModel();
            this.ShowPopUp = new RelayCommand(() => this.ShowPopUpExecute(), () => true);
            this.IncrementValue = new RelayCommand(() => this.IncrementValueExecute(), () => true);
        }

        /// <summary>
        /// Gets the command to create a pop up
        /// </summary>
        public ICommand ShowPopUp { get; private set; }

        /// <summary>
        /// Gets the command to increment display value
        /// </summary>
        public ICommand IncrementValue { get; private set; }

        /// <summary>
        /// Gets or sets value of whatever
        /// </summary>
        public Int32 ExampleValue
        {
            get
            {
                return this.mainModel.ExampleValue;
            }

            set
            {
                if (this.mainModel.ExampleValue == value)
                {
                    return;
                }

                this.mainModel.ExampleValue = value;
                this.RaisePropertyChanged(nameof(ExampleValue));
            }
        }

        /// <summary>
        /// Command to show le popup
        /// </summary>
        private void ShowPopUpExecute()
        {
            MessageBox.Show("Hello World!");
        }

        /// <summary>
        /// Command to increment le value
        /// </summary>
        private void IncrementValueExecute()
        {
            this.ExampleValue += 1;
        }
    }
    //// End class
}
//// End namespace