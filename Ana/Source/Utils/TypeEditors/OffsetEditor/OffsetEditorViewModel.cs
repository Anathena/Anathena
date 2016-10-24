namespace Ana.Source.Utils.ScriptEditor
{
    using Mvvm;
    using Mvvm.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Offset Editor
    /// </summary>
    internal class OffsetEditorViewModel : ViewModelBase
    {
        private List<Int32> offsets;

        public OffsetEditorViewModel()
        {
            this.AddOffsetCommand = new RelayCommand(() => Task.Run(() => AddOffset()), () => true);
            this.RemoveOffsetCommand = new RelayCommand(() => Task.Run(() => RemoveSelectedOffset()), () => true);
            this.UpdateActiveValueCommand = new RelayCommand<Int32>((offset) => Task.Run(() => UpdateActiveValue(offset)), (offset) => true);
            this.offsets = new List<int>();
        }

        public ICommand AddOffsetCommand { get; private set; }

        public ICommand RemoveOffsetCommand { get; private set; }

        public ICommand UpdateActiveValueCommand { get; private set; }

        public ObservableCollection<Int32> Offsets
        {
            get
            {
                return new ObservableCollection<Int32>(offsets);
            }
        }

        public Int32 SelectedOffsetIndex { get; set; }

        private Int32 ActiveOffsetValue { get; set; }

        private void AddOffset()
        {
            offsets.Add(ActiveOffsetValue);
        }

        private void RemoveSelectedOffset()
        {
            offsets.RemoveAt(SelectedOffsetIndex);
        }

        private void UpdateActiveValue(Int32 offset)
        {
            this.ActiveOffsetValue = offset;
        }
    }
    //// End class
}
//// End namespace