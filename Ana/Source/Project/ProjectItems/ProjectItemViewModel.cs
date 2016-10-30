namespace Ana.Source.Project.ProjectItems
{
    using Content;
    using Controls;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Media.Imaging;
    internal class ProjectItemViewModel : TreeViewItemViewModel
    {
        private readonly ProjectItem projectItem;
        private ObservableCollection<TreeViewItemViewModel> children;

        public ProjectItemViewModel(ProjectItem projectItem, TreeViewItemViewModel parentRegion = null) : base(parentRegion)
        {
            this.projectItem = projectItem;
            this.children = new ObservableCollection<TreeViewItemViewModel>(this.ProjectItem.Children.Select(x => new ProjectItemViewModel(x)));
        }

        public override ObservableCollection<TreeViewItemViewModel> Children
        {
            get
            {
                return this.children;
            }
        }

        public ProjectItem ProjectItem
        {
            get
            {
                return this.projectItem;
            }
        }

        public BitmapImage Icon
        {
            get
            {
                if (this.ProjectItem is FolderItem)
                {
                    return Images.Open;
                }
                else if (this.ProjectItem is AddressItem)
                {
                    return Images.CollectValues;
                }
                else if (this.ProjectItem is ScriptItem)
                {
                    return Images.CollectValues;
                }
                else
                {
                    return null;
                }
            }
        }

        public void AddChild(ProjectItemViewModel child)
        {
            this.ProjectItem.AddChild(child.ProjectItem);
            this.children = new ObservableCollection<TreeViewItemViewModel>(this.ProjectItem.Children.Select(x => new ProjectItemViewModel(x)));
            this.RaisePropertyChanged(nameof(this.Children));
        }

        protected override void OnSelected()
        {
            ProjectExplorerViewModel.GetInstance().SelectedProjectItem = this;
        }

        protected override void LoadChildren()
        {
            foreach (ProjectItem child in this.ProjectItem.Children)
            {
                this.Children.Add(new ProjectItemViewModel(child, this));
            }
        }
    }
    //// End class
}
//// End namespace