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
        private IList<Int32> offsets;

        public OffsetEditorViewModel()
        {
            this.AddOffsetCommand = new RelayCommand(() => Task.Run(() => AddOffset()), () => true);
            this.RemoveOffsetCommand = new RelayCommand(() => Task.Run(() => RemoveSelectedOffset()), () => true);
            this.UpdateActiveValueCommand = new RelayCommand<Int32>((offset) => Task.Run(() => UpdateActiveValue(offset)), (offset) => true);
            this.AccessLock = new Object();
            this.offsets = new List<Int32>();
        }

        public ICommand AddOffsetCommand { get; private set; }

        public ICommand RemoveOffsetCommand { get; private set; }

        public ICommand UpdateActiveValueCommand { get; private set; }

        public ObservableCollection<Int32> Offsets
        {
            get
            {
                lock (AccessLock)
                {
                    return new ObservableCollection<Int32>(offsets);
                }
            }

            set
            {
                this.offsets = value == null ? new List<Int32>() : new List<Int32>(value);
                this.RaisePropertyChanged(nameof(this.Offsets));
            }
        }

        public Int32 SelectedOffsetIndex { get; set; }

        private Object AccessLock { get; set; }

        private Int32 ActiveOffsetValue { get; set; }

        private void AddOffset()
        {
            lock (AccessLock)
            {
                offsets.Add(ActiveOffsetValue);
            }

            this.RaisePropertyChanged(nameof(this.Offsets));
        }

        private void RemoveSelectedOffset()
        {
            Int32 removalIndex = SelectedOffsetIndex;

            lock (AccessLock)
            {
                if (removalIndex < 0)
                {
                    removalIndex = 0;
                }

                if (removalIndex < offsets.Count)
                {
                    offsets.RemoveAt(removalIndex);
                }
            }

            this.RaisePropertyChanged(nameof(this.Offsets));
        }

        private void UpdateActiveValue(Int32 offset)
        {
            this.ActiveOffsetValue = offset;
        }
    }
    //// End class
}
//// End namespace