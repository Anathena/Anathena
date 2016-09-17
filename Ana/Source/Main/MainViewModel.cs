namespace Ana.Source.Main
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// This class contains properties that the main View can data bind to
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Int32 exampleValue;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class
        /// </summary>
        public MainViewModel()
        {
            this.ShowPopUp = new RelayCommand(() => this.ShowPopUpExecute(), () => true);
            this.IncrementValue = new RelayCommand(() => this.IncrementValueExecute(), () => true);
            this.ExampleValue = 0;
        }

        public ICommand ShowPopUp { get; private set; }

        public ICommand IncrementValue { get; private set; }

        public Int32 ExampleValue
        {
            get
            {
                return this.exampleValue;
            }

            set
            {
                if (this.exampleValue == value)
                {
                    return;
                }

                this.exampleValue = value;
                this.RaisePropertyChanged("ExampleValue");
            }
        }

        private void ShowPopUpExecute()
        {
            MessageBox.Show("Hello World!");
        }

        private void IncrementValueExecute()
        {
            this.ExampleValue += 1;
        }
    }
    //// End class
}
//// End namespace