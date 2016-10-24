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

        public OffsetEditorViewModel OffsetEditorViewModel
        {
            get
            {
                return this.DataContext as OffsetEditorViewModel;
            }
        }

        private HexDecTextBox OffsetHexDecBox { get; set; }

        private void CancelButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void AcceptButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ValueUpdated(Object sender, EventArgs e)
        {
            this.OffsetEditorViewModel.UpdateActiveValueCommand.Execute(this.OffsetHexDecBox.GetValue());
        }

        private void ListViewSelectionChanged(Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.OffsetEditorViewModel.SelectedOffsetIndex = this.offsetsListView.SelectedIndex;
        }
    }
    //// End class
}
//// End namespace