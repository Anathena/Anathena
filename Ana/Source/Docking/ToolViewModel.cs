namespace Ana.Source.Docking
{
    using System;

    internal class ToolViewModel : PaneViewModel
    {
        private Boolean isVisible = true;

        public ToolViewModel(String name)
        {
            this.Name = name;
            this.Title = name;
        }

        public String Name { get; private set; }

        public Boolean IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                if (this.isVisible != value)
                {
                    this.isVisible = value;
                    this.RaisePropertyChanged("IsVisible");
                }
            }
        }
    }
    //// End class
}
//// End namespace