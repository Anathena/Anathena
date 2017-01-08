namespace Ana.View.Editors
{
    using Source.Engine.Input.HotKeys;
    using Source.Utils.HotkeyEditor;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for HotkeyEditor.xaml
    /// </summary>
    internal partial class HotkeyEditor : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyEditor" /> class
        /// </summary>
        /// <param name="hotkeys">The initial hotkeys to edit</param>
        public HotkeyEditor(IList<IHotkey> hotkeys)
        {
            this.InitializeComponent();
            this.HotkeyEditorViewModel.Hotkeys = hotkeys == null ? null : new ObservableCollection<IHotkey>(hotkeys);
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
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void CancelButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// Invoked when the added hotkeys are accepted. Closes the view.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void AcceptButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Invoked when the selected hotkey is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Selection event args</param>
        private void ListViewSelectionChanged(Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.HotkeyEditorViewModel.SelectedHotkeyIndex = this.hotkeysListView.SelectedIndex;
        }
    }
    //// End class
}
//// End namespace