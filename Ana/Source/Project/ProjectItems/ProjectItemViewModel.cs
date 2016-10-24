namespace Ana.Source.Project.ProjectItems
{
    using Controls;

    internal class ProjectItemViewModel : TreeViewItemViewModel
    {
        private readonly ProjectItem projectItem;

        public ProjectItemViewModel(ProjectItem projectItem, TreeViewItemViewModel parentRegion = null) : base(parentRegion)
        {
            this.projectItem = projectItem;
        }

        public ProjectItem ProjectItem
        {
            get
            {
                return this.projectItem;
            }
        }

        public void AddChild(ProjectItem child)
        {
            this.ProjectItem.AddChild(child);
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