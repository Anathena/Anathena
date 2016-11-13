﻿namespace Ana.Source.Project.ProjectItems
{
    using Content;
    using Controls;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Media.Imaging;
    using Utils.Extensions;

    internal class ProjectItemViewModel : TreeViewItemViewModel
    {
        private readonly ProjectItem projectItem;
        private ObservableCollection<TreeViewItemViewModel> children;

        public ProjectItemViewModel(ProjectItem projectItem, TreeViewItemViewModel parent = null) : base(parent)
        {
            this.projectItem = projectItem;
            this.RebuildChildrenFacade();
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
            if (!(this.ProjectItem is FolderItem))
            {
                return;
            }

            (this.ProjectItem as FolderItem).AddChild(child.ProjectItem);
            this.RebuildChildrenFacade();
        }

        public void RemoveChild(ProjectItemViewModel child)
        {
            if (!(this.ProjectItem is FolderItem))
            {
                return;
            }

            (this.ProjectItem as FolderItem).RemoveChild(child.ProjectItem);
            this.RebuildChildrenFacade();
        }

        public void RemoveRecursive(ProjectItemViewModel removeTarget)
        {
            if (this.ProjectItem.Parent == null)
            {
                ProjectExplorerViewModel.GetInstance().ProjectItems.Where(x => x == removeTarget).ForEach(x => ProjectExplorerViewModel.GetInstance().RemoveProjectItem(x));
            }

            this.Children.ForEach(x => this.RemoveRecursive(x as ProjectItemViewModel));
            this.RebuildChildrenFacade();
        }

        public void AddSibling(ProjectItemViewModel projectItemViewModel, Boolean after)
        {
            if (!(this.ProjectItem is FolderItem))
            {
                return;
            }

            projectItemViewModel.ProjectItem.Parent = this.ProjectItem.Parent;

            if (after)
            {
                if (this.Parent != null)
                {
                    ((this.Parent as ProjectItemViewModel)?.ProjectItem as FolderItem)?.Children?.Insert(Parent.Children.IndexOf(this) + 1, projectItemViewModel.ProjectItem);
                }
                else
                {
                    ProjectExplorerViewModel.GetInstance().InsertProjectItem(projectItemViewModel, ProjectExplorerViewModel.GetInstance().ProjectItems.IndexOf(this) + 1);
                }
            }
            else
            {
                if (this.Parent != null)
                {
                    ((this.Parent as ProjectItemViewModel)?.ProjectItem as FolderItem)?.Children?.Insert(Parent.Children.IndexOf(this), projectItemViewModel.ProjectItem);
                }
                else
                {
                    ProjectExplorerViewModel.GetInstance().InsertProjectItem(projectItemViewModel, ProjectExplorerViewModel.GetInstance().ProjectItems.IndexOf(this));
                }
            }

            this.RebuildChildrenFacade();
        }

        protected override void OnSelected()
        {
            ProjectExplorerViewModel.GetInstance().SelectedProjectItem = this;
        }

        protected override void LoadChildren()
        {
            if (!(this.ProjectItem is FolderItem))
            {
                return;
            }

            foreach (ProjectItem child in (this.ProjectItem as FolderItem).Children)
            {
                this.Children.Add(new ProjectItemViewModel(child, this));
            }
        }

        private void RebuildChildrenFacade()
        {
            if (!(this.ProjectItem is FolderItem))
            {
                return;
            }

            this.children = new ObservableCollection<TreeViewItemViewModel>((this.ProjectItem as FolderItem).Children.Select(x => new ProjectItemViewModel(x)));
            (this.ProjectItem as FolderItem).BuildParents(this.ProjectItem.Parent);
            this.RaisePropertyChanged(nameof(this.Children));
        }
    }
    //// End class
}
//// End namespace