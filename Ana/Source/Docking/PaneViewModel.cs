namespace Ana.Source.Docking
{
    using Mvvm;
    using System;
    using System.Windows.Media;

    internal class PaneViewModel : ViewModelBase
    {
        private String title = null;
        private String contentId = null;
        private Boolean isSelected = false;
        private Boolean isActive = false;

        public PaneViewModel()
        {
        }

        public String Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.RaisePropertyChanged(nameof(this.Title));
                }
            }
        }

        public ImageSource IconSource { get; protected set; }

        public String ContentId
        {
            get
            {
                return this.contentId;
            }

            set
            {
                if (this.contentId != value)
                {
                    this.contentId = value;
                    this.RaisePropertyChanged(nameof(this.ContentId));
                }
            }
        }

        public Boolean IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.RaisePropertyChanged(nameof(this.IsSelected));
                }
            }
        }

        public Boolean IsActive
        {
            get
            {
                return this.isActive;
            }

            set
            {
                if (this.isActive != value)
                {
                    this.isActive = value;
                    this.RaisePropertyChanged(nameof(this.IsActive));
                }
            }
        }
    }
    //// End class
}
//// End namespace