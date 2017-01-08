namespace Ana.View
{
    using Source.ChangeLog;
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for ChangeLog.xaml.
    /// </summary>
    internal partial class ChangeLog : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeLog" /> class.
        /// </summary>
        public ChangeLog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the view model associated with this view.
        /// </summary>
        public ChangeLogViewModel ChangeLogViewModel
        {
            get
            {
                return this.DataContext as ChangeLogViewModel;
            }
        }

        /// <summary>
        /// Invoked when the added offsets are accepted. Closes the view.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void AcceptButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
    //// End class
}
//// End namespace