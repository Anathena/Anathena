namespace Ana.Source.ProcessSelector
{
    using Docking;
    using Engine.Processes;
    using Main;
    using System;
    using System.Collections.Generic;
    using Utils;

    /// <summary>
    /// View model for the Process Selector
    /// </summary>
    internal class ProcessSelectorViewModel : ToolViewModel
    {
        public const String ToolContentId = "FileStatsTool";
        private DateTime lastModified;
        private Int64 fileSize;

        public ProcessSelectorViewModel() : base("Process Selector")
        {
            this.ContentId = ToolContentId;
            this.IconSource = ImageLoader.LoadImage("pack://application:,,/Content/Icons/SelectProcess.png");

            MainViewModel.GetInstance().Subscribe(this);
        }

        public IEnumerable<NormalizedProcess> ProcessObjects
        {
            get
            {
                return ProcessCollector.GetProcesses();
            }

            set
            {
            }
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