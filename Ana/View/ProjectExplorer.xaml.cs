namespace Ana.View
{
    using Source.Project;
    using Source.Project.ProjectItems;
    using Source.Utils.Extensions;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for ProjectExplorer.xaml
    /// </summary>
    internal partial class ProjectExplorer : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectExplorer" /> class
        /// </summary>
        public ProjectExplorer()
        {
            this.InitializeComponent();
        }

        private Point LastMouseDownLocation { get; set; }

        private ProjectItemViewModel DraggedItem { get; set; }

        private ProjectItemViewModel Target { get; set; }

        private void ProjectExplorerTreeViewMouseDown(Object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.LastMouseDownLocation = e.GetPosition(this.projectExplorerTreeView);
            }

            if (e.ChangedButton == MouseButton.Left && !(e.OriginalSource is Image) && !(e.OriginalSource is TextBlock))
            {
                ProjectExplorerViewModel projectExplorerViewModel = this.DataContext as ProjectExplorerViewModel;
                ProjectItemViewModel item = projectExplorerViewModel.SelectedProjectItem as ProjectItemViewModel;
                if (item != null)
                {
                    this.projectExplorerTreeView.Focus();
                    item.IsSelected = false;
                }
            }
        }

        private void TreeViewMouseMove(Object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point currentPosition = e.GetPosition(this.projectExplorerTreeView);

                    if ((Math.Abs(currentPosition.X - this.LastMouseDownLocation.X) > 10.0) || (Math.Abs(currentPosition.Y - this.LastMouseDownLocation.Y) > 10.0))
                    {
                        this.DraggedItem = (ProjectItemViewModel)this.projectExplorerTreeView.SelectedItem;
                        if (this.DraggedItem != null)
                        {
                            DragDropEffects finalDropEffect = DragDrop.DoDragDrop(
                                this.projectExplorerTreeView,
                                this.projectExplorerTreeView.SelectedValue,
                                DragDropEffects.Move);

                            // Checking target is not null and item is dragging(moving)
                            if ((finalDropEffect == DragDropEffects.Move) && this.Target != null)
                            {
                                // A Move drop was accepted
                                if (this.DraggedItem != this.Target)
                                {
                                    ProjectExplorerViewModel.GetInstance().ProjectItems.ForEach(x => x.RemoveRecursive(this.DraggedItem));
                                    this.Target.AddSibling(this.DraggedItem, true);

                                    this.Target = null;
                                    this.DraggedItem = null;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void TreeViewDragOver(Object sender, DragEventArgs e)
        {
            try
            {
                Point currentPosition = e.GetPosition(this.projectExplorerTreeView);

                if ((Math.Abs(currentPosition.X - this.LastMouseDownLocation.X) > 10.0) || (Math.Abs(currentPosition.Y - this.LastMouseDownLocation.Y) > 10.0))
                {
                    // Verify that this is a valid drop and then store the drop target
                    ProjectItemViewModel item = ((sender as TreeViewItem).Header as ProjectItemViewModel) as ProjectItemViewModel;
                    if (this.CheckDropTarget(this.DraggedItem, item))
                    {
                        e.Effects = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.None;
                    }
                }

                e.Handled = true;
            }
            catch
            {
            }
        }

        private void TreeViewDrop(Object sender, DragEventArgs e)
        {
            try
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;

                // Verify that this is a valid drop and then store the drop target
                ProjectItemViewModel targetItem = ((sender as TreeViewItem).Header as ProjectItemViewModel) as ProjectItemViewModel;
                if (targetItem != null && this.DraggedItem != null)
                {
                    this.Target = targetItem;
                    e.Effects = DragDropEffects.Move;
                }
            }
            catch
            {
            }
        }

        private Boolean CheckDropTarget(ProjectItemViewModel sourceItem, ProjectItemViewModel targetItem)
        {
            return sourceItem != targetItem;
        }

        private void CopyItem(ProjectItemViewModel sourceItem, ProjectItemViewModel targetItem)
        {
            try
            {
                // Adding dragged TreeViewItem in target ProjectItemViewModel
                this.AddChild(sourceItem, targetItem);

                // Finding Parent ProjectItemViewModel of dragged ProjectItemViewModel 
                ProjectItemViewModel parentItem = sourceItem.Parent as ProjectItemViewModel;

                // If parent is null then remove from TreeView else remove from Parent ProjectItemViewModel
                if (parentItem == null)
                {
                    this.projectExplorerTreeView.Items.Remove(sourceItem);
                }
                else
                {
                    parentItem.RemoveChild(sourceItem);
                }
            }
            catch
            {
            }
        }

        public void AddChild(ProjectItemViewModel sourceItem, ProjectItemViewModel targetItem)
        {
            // Add item in target ProjectItemViewModel 
            ProjectItemViewModel newItem = new ProjectItemViewModel(sourceItem.ProjectItem);
            newItem = sourceItem;
            targetItem.AddChild(newItem);
            foreach (ProjectItemViewModel item in sourceItem.Children)
            {
                this.AddChild(item, newItem);
            }
        }
    }
    //// End class
}
//// End namespace