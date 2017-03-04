namespace Ana.View
{
    using System;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for HotkeyManager.xaml.
    /// </summary>
    internal partial class HotkeyManager : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyManager" /> class.
        /// </summary>
        public HotkeyManager()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the selected offset is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Selection event args.</param>
        private void DataGridSelectionChanged(Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // this.HotkeyManagerViewModel.SelectedOffsetIndex = this.offsetsDataGrid.SelectedIndex;
        }
    }
    //// End class
}
//// End namespace