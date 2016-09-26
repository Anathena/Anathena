namespace Ana.Source.Project.ProjectItems
{
    using Controls;
    using System;

    internal class ProjectItemViewModel : TreeViewItemViewModel
    {
        private readonly ProjectItem projectItem;

        public ProjectItemViewModel(ProjectItem projectItem, TreeViewItemViewModel parentRegion = null) : base(parentRegion, true)
        {
            this.projectItem = projectItem;
        }

        public String Description
        {
            get
            {
                return projectItem.Description;
            }
        }

        public void AddChild(ProjectItem child)
        {
            projectItem.AddChild(child);
        }

        protected override void LoadChildren()
        {
            foreach (ProjectItem child in projectItem.Children)
            {
                base.Children.Add(new ProjectItemViewModel(child, this));
            }
        }
    }
    //// End class
}
//// End namespace