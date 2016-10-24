namespace Ana.View
{
    using Controls;
    using Source.Utils.ScriptEditor;
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for OffsetEditor.xaml
    /// </summary>
    internal partial class OffsetEditor : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OffsetEditor" /> class
        /// </summary>
        public OffsetEditor()
        {
            this.InitializeComponent();

            // Windows Forms hosting -- TODO: Phase this out
            OffsetHexDecBox = new HexDecTextBox();
            OffsetHexDecBox.TextChanged += ValueUpdated;
            this.offsetHexDecBox.Children.Add(WinformsHostingHelper.CreateHostedControl(OffsetHexDecBox));
        }

        private HexDecTextBox OffsetHexDecBox { get; set; }

        public OffsetEditorViewModel OffsetEditorViewModel
        {
            get
            {
                return this.DataContext as OffsetEditorViewModel;
            }
        }

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
            this.OffsetEditorViewModel.UpdateActiveValueCommand.Execute(OffsetHexDecBox.GetValue());
        }
    }
    //// End class
}
//// End namespace