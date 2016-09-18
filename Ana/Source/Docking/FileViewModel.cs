namespace Ana.Source.Docking
{
    using Mvvm.Command;
    using System;
    using System.IO;
    using System.Windows.Input;
    using System.Windows.Media;

    internal class FileViewModel : PaneViewModel
    {
        private static ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
        private String filePath;
        private String textContent;
        private Boolean isDirty;
        private RelayCommand saveCommand;
        private RelayCommand saveAsCommand;
        private RelayCommand closeCommand;

        public FileViewModel(String filePath)
        {
            this.FilePath = filePath;
            this.Title = FileName;

            this.textContent = String.Empty;
            this.isDirty = false;
            this.saveCommand = null;
            this.saveAsCommand = null;
            this.closeCommand = null;

            // Set the icon only for open documents (just a test)
            this.IconSource = imageSourceConverter.ConvertFromInvariantString(@"pack://application:,,/Images/document.png") as ImageSource;
        }

        public FileViewModel()
        {
            IsDirty = true;
            Title = FileName;
        }

        public String FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                if (filePath != value)
                {
                    filePath = value;
                    RaisePropertyChanged("FilePath");
                    RaisePropertyChanged("FileName");
                    RaisePropertyChanged("Title");

                    if (File.Exists(filePath))
                    {
                        textContent = File.ReadAllText(filePath);
                        ContentId = filePath;
                    }
                }
            }
        }

        public String FileName
        {
            get
            {
                if (FilePath == null)
                    return "Noname" + (IsDirty ? "*" : String.Empty);

                return Path.GetFileName(FilePath) + (IsDirty ? "*" : String.Empty);
            }
        }

        public String TextContent
        {
            get
            {
                return textContent;
            }

            set
            {
                if (textContent != value)
                {
                    textContent = value;
                    RaisePropertyChanged("TextContent");
                    IsDirty = true;
                }
            }
        }

        public Boolean IsDirty
        {
            get
            {
                return isDirty;
            }

            set
            {
                if (isDirty != value)
                {
                    isDirty = value;
                    RaisePropertyChanged("IsDirty");
                    RaisePropertyChanged("FileName");
                }
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(OnSave, CanSave);
                }

                return saveCommand;
            }
        }

        public ICommand SaveAsCommand
        {
            get
            {
                if (saveAsCommand == null)
                {
                    saveAsCommand = new RelayCommand(OnSaveAs, CanSaveAs);
                }

                return saveAsCommand;
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(OnClose, CanClose);
                }

                return closeCommand;
            }
        }

        private Boolean CanSave()
        {
            return IsDirty;
        }

        private void OnSave()
        {
            Workspace.GetInstance().Save(this, false);
        }

        private Boolean CanSaveAs()
        {
            return IsDirty;
        }

        private void OnSaveAs()
        {
            Workspace.GetInstance().Save(this, true);
        }

        private Boolean CanClose()
        {
            return true;
        }

        private void OnClose()
        {
            Workspace.GetInstance().Close(this);
        }
    }
    //// End class
}
//// End namespace