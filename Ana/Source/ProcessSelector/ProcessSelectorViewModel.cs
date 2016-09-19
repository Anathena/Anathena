namespace Ana.Source.Docking
{
    using System;

    internal class ProcessSelectorViewModel : ToolViewModel
    {
        public const String ToolContentId = "FileStatsTool";
        private Int64 fileSize;
        private DateTime lastModified;

        public ProcessSelectorViewModel() : base("Process Selector")
        {
            this.ContentId = ToolContentId;

            /*
             * BitmapImage bi = new BitmapImage();
             * bi.BeginInit();
             * bi.UriSource = new Uri("pack://application:,,/Images/property-blue.png");
             * bi.EndInit();
             * IconSource = bi;
            */
        }

        public Int64 FileSize
        {
            get
            {
                return this.fileSize;
            }

            set
            {
                if (this.fileSize != value)
                {
                    this.fileSize = value;
                    this.RaisePropertyChanged(nameof(this.FileSize));
                }
            }
        }

        public DateTime LastModified
        {
            get
            {
                return this.lastModified;
            }

            set
            {
                if (this.lastModified != value)
                {
                    this.lastModified = value;
                    this.RaisePropertyChanged(nameof(this.LastModified));
                }
            }
        }
    }
    //// End class
}
//// End namespace