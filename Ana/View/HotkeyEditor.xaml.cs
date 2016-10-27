namespace Ana.View
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

        public HotkeyEditorViewModel HotkeyEditorViewModel
        {
            get
            {
                return this.DataContext as HotkeyEditorViewModel;
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

        private void ListViewSelectionChanged(Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.HotkeyEditorViewModel.SelectedHotkeyIndex = this.hotkeysListView.SelectedIndex;
        }
    }
    //// End class
}
//// End namespace