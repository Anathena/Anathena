namespace Ana.Source.Docking
{
    using System;
    using System.IO;

    internal class FileStatsViewModel : ToolViewModel
    {
        public FileStatsViewModel() : base("File Stats")
        {
            Workspace.GetInstance().ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);
            ContentId = ToolContentId;

            /*
             * BitmapImage bi = new BitmapImage();
             * bi.BeginInit();
             * bi.UriSource = new Uri("pack://application:,,/Images/property-blue.png");
             * bi.EndInit();
             * IconSource = bi;
            */
        }

        public const String ToolContentId = "FileStatsTool";

        void OnActiveDocumentChanged(Object sender, EventArgs e)
        {
            if (Workspace.GetInstance().ActiveDocument != null &&
                Workspace.GetInstance().ActiveDocument.FilePath != null &&
                File.Exists(Workspace.GetInstance().ActiveDocument.FilePath))
            {
                FileInfo fileInfo = new FileInfo(Workspace.GetInstance().ActiveDocument.FilePath);
                FileSize = fileInfo.Length;
                LastModified = fileInfo.LastWriteTime;
            }
            else
            {
                FileSize = 0;
                LastModified = DateTime.MinValue;
            }
        }

        private Int64 fileSize;
        public Int64 FileSize
        {
            get { return fileSize; }
            set
            {
                if (fileSize != value)
                {
                    fileSize = value;
                    RaisePropertyChanged("FileSize");
                }
            }
        }

        private DateTime lastModified;
        public DateTime LastModified
        {
            get { return lastModified; }
            set
            {
                if (lastModified != value)
                {
                    lastModified = value;
                    RaisePropertyChanged("LastModified");
                }
            }
        }
    }
    //// End class
}
//// End namespace