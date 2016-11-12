namespace Ana.Source.Engine.AddressResolver.DotNet
{
    using Controls;
    using PropertyViewer;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Media.Imaging;
    internal class DotNetObjectViewModel : TreeViewItemViewModel
    {
        private readonly DotNetObject dotNetObject;
        private ObservableCollection<TreeViewItemViewModel> children;

        public DotNetObjectViewModel(DotNetObject projectItem, TreeViewItemViewModel parent = null) : base(parent)
        {
            this.dotNetObject = projectItem;
            this.RebuildChildrenFacade();
        }

        public override ObservableCollection<TreeViewItemViewModel> Children
        {
            get
            {
                return this.children;
            }
        }

        public DotNetObject DotNetObject
        {
            get
            {
                return this.dotNetObject;
            }
        }

        public BitmapImage Icon
        {
            get
            {
                // TODO: It would be cool to have an icon based on object type
                return null;
            }
        }

        protected override void OnSelected()
        {
            PropertyViewerViewModel.GetInstance().SetTargetObjects(this.DotNetObject);
        }

        protected override void LoadChildren()
        {
            foreach (DotNetObject child in this.DotNetObject.GetChildren())
            {
                this.Children.Add(new DotNetObjectViewModel(child, this));
            }
        }

        private void RebuildChildrenFacade()
        {
            this.children = new ObservableCollection<TreeViewItemViewModel>(this.DotNetObject.GetChildren().Select(x => new DotNetObjectViewModel(x)));
            this.RaisePropertyChanged(nameof(this.Children));
        }
    }
    //// End class
}
//// End namespace