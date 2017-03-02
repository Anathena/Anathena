namespace Ana.View.Editors
{
    using Source.Editors.HotkeyEditor;
    using Source.Engine.Input.HotKeys;
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for HotkeyEditor.xaml.
    /// </summary>
    internal partial class HotkeyEditor : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyEditor" /> class.
        /// </summary>
        /// <param name="hotkey">The initial hotkeys to edit.</param>
        public HotkeyEditor(IHotkey hotkey = null)
        {
            this.InitializeComponent();
            this.HotkeyEditorViewModel.ActiveHotkey = hotkey == null ? null : hotkey;
        }

        /// <summary>
        /// Gets the view model associated with this view.
        /// </summary>
        public HotkeyEditorViewModel HotkeyEditorViewModel
        {
            get
            {
                return this.DataContext as HotkeyEditorViewModel;
            }
        }

        /// <summary>
        /// Invoked when the added hotkeys are canceled. Closes the view.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void CancelButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// Invoked when the added hotkeys are accepted. Closes the view.
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