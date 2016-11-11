namespace Ana.View
{
    using Controls;
    using Source.Utils.ScriptEditor;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for OffsetEditor.xaml
    /// </summary>
    internal partial class OffsetEditor : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OffsetEditor" /> class
        /// </summary>
        /// <param name="offsets">The initial offsets to edit</param>
        public OffsetEditor(IList<Int32> offsets)
        {
            this.InitializeComponent();

            // Windows Forms hosting -- TODO: Phase this out
            this.OffsetHexDecBox = new HexDecTextBox();
            this.OffsetHexDecBox.TextChanged += this.ValueUpdated;
            this.offsetHexDecBox.Children.Add(WinformsHostingHelper.CreateHostedControl(this.OffsetHexDecBox));
            this.OffsetEditorViewModel.Offsets = offsets == null ? null : new ObservableCollection<Int32>(offsets);
        }

        /// <summary>
        /// Gets the view model associated with this view.
        /// </summary>
        public OffsetEditorViewModel OffsetEditorViewModel
        {
            get
            {
                return this.DataContext as OffsetEditorViewModel;
            }
        }

        /// <summary>
        /// Gets or sets the offset hex dec box used to display the edited hotkey.
        /// </summary>
        private HexDecTextBox OffsetHexDecBox { get; set; }

        /// <summary>
        /// Invoked when the added offsets are canceled. Closes the view.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void CancelButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// Invoked when the added offsets are accepted. Closes the view.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void AcceptButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Invoked when the current offset is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void ValueUpdated(Object sender, EventArgs e)
        {
            this.OffsetEditorViewModel.UpdateActiveValueCommand.Execute(this.OffsetHexDecBox.GetValue());
        }

        /// <summary>
        /// Invoked when the selected offset is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Selection event args</param>
        private void ListViewSelectionChanged(Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.OffsetEditorViewModel.SelectedOffsetIndex = this.offsetsListView.SelectedIndex;
        }
    }
    //// End class
}
//// End namespace