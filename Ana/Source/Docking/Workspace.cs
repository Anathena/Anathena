namespace Ana.Source.Docking
{
    using Microsoft.Win32;
    using Mvvm;
    using Mvvm.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    internal class Workspace : ViewModelBase
    {
        public event EventHandler ActiveDocumentChanged;

        private static Workspace Instance = new Workspace();

        private ObservableCollection<FileViewModel> files;
        private ReadOnlyObservableCollection<FileViewModel> readonyFiles;
        private ToolViewModel[] tools;
        private FileStatsViewModel fileStats;
        private RelayCommand openCommand;
        private RelayCommand newCommand;
        private FileViewModel activeDocument;

        private Workspace()
        {
            files = new ObservableCollection<FileViewModel>();
            readonyFiles = null;
            tools = null;
            fileStats = null;
            openCommand = null;
            newCommand = null;
            activeDocument = null;
        }

        public static Workspace GetInstance()
        {
            return Instance;
        }

        public ReadOnlyObservableCollection<FileViewModel> Files
        {
            get
            {
                if (readonyFiles == null)
                    readonyFiles = new ReadOnlyObservableCollection<FileViewModel>(files);

                return readonyFiles;
            }
        }

        public IEnumerable<ToolViewModel> Tools
        {
            get
            {
                if (tools == null)
                {
                    tools = new ToolViewModel[] { FileStats };
                }

                return tools;
            }
        }

        public FileStatsViewModel FileStats
        {
            get
            {
                if (fileStats == null)
                    fileStats = new FileStatsViewModel();

                return fileStats;
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                if (openCommand == null)
                {
                    openCommand = new RelayCommand(OnOpen, CanOpen);
                }

                return openCommand;
            }
        }

        private Boolean CanOpen()
        {
            return true;
        }

        private void OnOpen()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                FileViewModel fileViewModel = Open(openFileDialog.FileName);
                ActiveDocument = fileViewModel;
            }
        }

        public FileViewModel Open(string filepath)
        {
            FileViewModel fileViewModel = files.FirstOrDefault(file => file.FilePath == filepath);
            if (fileViewModel != null)
            {
                return fileViewModel;
            }

            fileViewModel = new FileViewModel(filepath);
            files.Add(fileViewModel);
            return fileViewModel;
        }

        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(OnNew, CanNew);
                }

                return newCommand;
            }
        }

        private Boolean CanNew()
        {
            return true;
        }

        private void OnNew()
        {
            files.Add(new FileViewModel());
            ActiveDocument = files.Last();
        }

        public FileViewModel ActiveDocument
        {
            get
            {
                return activeDocument;
            }

            set
            {
                if (activeDocument != value)
                {
                    activeDocument = value;
                    RaisePropertyChanged("ActiveDocument");
                    ActiveDocumentChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        internal void Close(FileViewModel fileToClose)
        {
            if (fileToClose.IsDirty)
            {
                var res = MessageBox.Show(string.Format("Save changes for file '{0}'?", fileToClose.FileName), "AvalonDock Test App", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Cancel)
                {
                    return;
                }

                if (res == MessageBoxResult.Yes)
                {
                    Save(fileToClose);
                }
            }

            files.Remove(fileToClose);
        }

        internal void Save(FileViewModel fileToSave, Boolean saveAsFlag = false)
        {
            if (fileToSave.FilePath == null || saveAsFlag)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog().GetValueOrDefault())
                    fileToSave.FilePath = saveFileDialog.SafeFileName;
            }

            File.WriteAllText(fileToSave.FilePath, fileToSave.TextContent);
            ActiveDocument.IsDirty = false;
        }
    }
    //// End class
}
//// End namesapce