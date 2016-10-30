namespace Ana.View
{
    using Source.Project;
    using Source.Project.ProjectItems;
    using System;
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

        private void projectExplorerTreeView_MouseDown(Object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && !(e.OriginalSource is Image) && !(e.OriginalSource is TextBlock))
            {
                ProjectExplorerViewModel projectExplorerViewModel = this.DataContext as ProjectExplorerViewModel;
                ProjectItemViewModel item = projectExplorerViewModel.SelectedProjectItem as ProjectItemViewModel;
                if (item != null)
                {
                    projectExplorerTreeView.Focus();
                    item.IsSelected = false;
                }
            }
        }
    }
    //// End class
}
//// End namespace