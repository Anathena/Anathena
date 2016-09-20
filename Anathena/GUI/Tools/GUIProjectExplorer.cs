using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Ana.GUI.CustomControls.TreeViews;
using Ana.Source.Project;
using Ana.Source.Project.ProjectItems;
using Ana.Source.Utils.DataStructures;
using Ana.Source.Utils.Extensions;
using Ana.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Ana.GUI.Tools
{
    public partial class GUIProjectExplorer : DockContent, IProjectExplorerView
    {
        private ProjectExplorerPresenter projectExplorerPresenter;

        private BiDictionary<ProjectItem, ProjectNode> nodeCache;
        private TreeModel projectTree;
        private ProjectItem projectRoot;
        private TreeNodeAdv draggedItem;
        private Object accessLock;

        public GUIProjectExplorer()
        {
            InitializeComponent();

            EntryCheckBox.IsEditEnabledValueNeeded += CheckIndex;

            nodeCache = new BiDictionary<ProjectItem, ProjectNode>();
            projectTree = new TreeModel();
            accessLock = new Object();

            (ProjectExplorerTreeView.NodeControls[ProjectExplorerTreeView.NodeControls.IndexOf(EntryDescription)] as BaseTextControl).DrawText += new EventHandler<DrawEventArgs>(FolderBrowser_DrawText);

            ProjectExplorerTreeView.Model = projectTree;
            projectExplorerPresenter = new ProjectExplorerPresenter(this, ProjectExplorer.GetInstance());
        }

        public void EventRefreshProjectStructure(ProjectItem projectRoot)
        {
            if (projectExplorerPresenter == null)
                return;

            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                this.projectRoot = projectRoot;

                ProjectExplorerTreeView.BeginUpdate();
                projectTree.Nodes.Clear();
                nodeCache.Clear();

                if (projectRoot != null)
                {
                    foreach (ProjectItem Child in projectRoot.Children)
                        BuildNodes(Child);
                }

                ProjectExplorerTreeView.EndUpdate();
                ProjectExplorerTreeView.ExpandAll();
            });
        }

        private void BuildNodes(ProjectItem projectItem, ProjectItem parent = null)
        {
            if (projectItem == null)
                return;

            Image image = null;

            if (projectItem is AddressItem)
                image = new Bitmap(Properties.Resources.CollectValues);
            else if (projectItem is ScriptItem)
                image = new Bitmap(Properties.Resources.CollectValues);
            else if (projectItem is FolderItem)
                image = new Bitmap(Properties.Resources.Open);

            // Create new node to insert
            ProjectNode ProjectNode = new ProjectNode(projectItem.Description);
            ProjectNode.ProjectItem = projectItem;
            ProjectNode.EntryIcon = image;
            ProjectNode.IsChecked = projectItem.GetActivationState();

            if (parent != null && nodeCache.ContainsKey(parent))
                nodeCache[parent].Nodes.Add(ProjectNode);
            else
                projectTree.Nodes.Add(ProjectNode);

            nodeCache.Add(projectItem, ProjectNode);

            foreach (ProjectItem Child in projectItem.Children)
                BuildNodes(Child, projectItem);
        }

        private ProjectNode GetProjectNodeFromTreeNodeAdv(TreeNodeAdv treeNodeAdv)
        {
            Node node = projectTree.FindNode(ProjectExplorerTreeView.GetPath(treeNodeAdv));

            if (node == null || !typeof(ProjectNode).IsAssignableFrom(node.GetType()))
                return null;

            ProjectNode projectNode = node as ProjectNode;

            return projectNode;
        }

        private ProjectItem GetProjectItemFromNode(TreeNodeAdv treeNodeAdv)
        {
            return GetProjectNodeFromTreeNodeAdv(treeNodeAdv)?.ProjectItem;
        }

        public void AddNewAddressItem()
        {
            projectExplorerPresenter.AddNewAddressItem(GetSelectedItem());
        }

        public void AddNewScriptItem()
        {
            projectExplorerPresenter.AddNewScriptItem(GetSelectedItem());
        }

        public void AddNewFolderItem()
        {
            projectExplorerPresenter.AddNewFolderItem(GetSelectedItem());
        }

        private ProjectItem GetSelectedItem()
        {
            Node selectedNode = projectTree.FindNode(ProjectExplorerTreeView.GetPath(ProjectExplorerTreeView.SelectedNode));
            ProjectItem SelectedItem = null;

            if (selectedNode != null && typeof(ProjectNode).IsAssignableFrom(selectedNode.GetType()))
            {
                if (nodeCache.Reverse.ContainsKey((ProjectNode)selectedNode))
                    SelectedItem = nodeCache.Reverse[(ProjectNode)selectedNode];
            }

            return SelectedItem;
        }

        private void ActivateSelectedItems()
        {
            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                if (ProjectExplorerTreeView.SelectedNodes == null || ProjectExplorerTreeView.SelectedNodes.Count <= 0)
                    return;

                List<ProjectItem> nodes = new List<ProjectItem>();
                ProjectExplorerTreeView.SelectedNodes.ForEach(x => nodes.Add(GetProjectItemFromNode(x)));

                if (nodes.Count <= 0)
                    return;

                // Behavior here is undefined, we could check only the selected items, or enforce the recursive rules of folders
                // Nodes.ForEach(x => DoCheck(x, !x.GetActivationState()));
                projectExplorerPresenter.ActivateProjectItems(nodes, !nodes.First().GetActivationState());
                ProjectExplorerTreeView.SelectedNodes.ForEach(x => nodeCache[GetProjectItemFromNode(x)].IsChecked = GetProjectItemFromNode(x).GetActivationState());

                EventRefreshProjectStructure(projectRoot);
            });
        }

        private void DeleteSelectedItems()
        {
            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                if (ProjectExplorerTreeView.SelectedNodes == null || ProjectExplorerTreeView.SelectedNodes.Count <= 0)
                    return;

                List<ProjectItem> Nodes = new List<ProjectItem>();
                ProjectExplorerTreeView.SelectedNodes.ForEach(x => Nodes.Add(GetProjectItemFromNode(x)));
                projectExplorerPresenter.DeleteProjectItems(Nodes);

                EventRefreshProjectStructure(projectRoot);
            });
        }

        private void DoCheck(ProjectItem projectItem, Boolean activated)
        {
            if (projectItem == null)
                return;

            projectExplorerPresenter.ActivateProjectItem(projectItem, activated);
            nodeCache[projectItem].IsChecked = projectItem.GetActivationState();

            if (!(projectItem is FolderItem))
                return;

            foreach (ProjectItem child in projectItem.Children)
                DoCheck(child, activated);
        }

        #region Events

        private void AddressToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            AddNewAddressItem();
        }

        private void AddNewScriptToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            AddNewScriptItem();
        }

        private void FolderToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            AddNewFolderItem();
        }

        private void AddressRightClickToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            AddNewAddressItem();
        }

        private void ScriptRightClickToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            AddNewScriptItem();
        }

        private void FolderRightClickToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            AddNewFolderItem();
        }

        private void DeleteSelectionToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        private void ToggleFreezeToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            ActivateSelectedItems();
        }

        private void ProjectExplorerTreeView_KeyPress(Object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != ' ')
                return;

            ActivateSelectedItems();
        }

        private void ProjectContextMenuStrip_Opening(Object sender, CancelEventArgs e)
        {
            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                if (ProjectExplorerTreeView.SelectedNodes == null || ProjectExplorerTreeView.SelectedNodes.Count <= 0)
                {
                    ToggleActivationToolStripMenuItem.Enabled = false;
                    DeleteSelectionToolStripMenuItem.Enabled = false;
                    AddNewItemToolStripMenuItem.Enabled = true;
                }
                else
                {
                    ToggleActivationToolStripMenuItem.Enabled = true;
                    DeleteSelectionToolStripMenuItem.Enabled = true;
                    AddNewItemToolStripMenuItem.Enabled = true;
                }
            });
        }

        private void CheckIndex(Object sender, NodeControlValueEventArgs e)
        {
            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                if (e == null || e.Node == null)
                    return;

                ProjectItem projectItem = GetProjectItemFromNode(e.Node);

                if (projectItem == null)
                    return;

                DoCheck(projectItem, !projectItem.GetActivationState());

                EventRefreshProjectStructure(projectRoot);
            });
        }

        private void ProjectExplorerTreeView_NodeMouseDoubleClick(Object sender, TreeNodeAdvMouseEventArgs e)
        {
            if (e == null || e.Node == null)
                return;

            ProjectItem projectItem = GetProjectItemFromNode(e.Node);
            projectExplorerPresenter.PerformDefaultAction(projectItem);
        }

        private void ProjectExplorerTreeView_SelectionChanged(Object sender, EventArgs e)
        {
            List<TreeNodeAdv> treeNodes = new List<TreeNodeAdv>();
            List<ProjectItem> projectItems = new List<ProjectItem>();

            ProjectExplorerTreeView.SelectedNodes.ForEach(X => treeNodes.Add(X));
            treeNodes.ForEach(x => projectItems.Add(GetProjectItemFromNode(x)));

            projectExplorerPresenter.UpdateSelection(projectItems);
        }

        private void FolderBrowser_DrawText(Object sender, DrawEventArgs e)
        {
            ProjectItem projectItem = GetProjectItemFromNode(e.Node);

            if (projectItem != null)
                e.TextColor = projectItem.TextColor;
            else
                e.TextColor = SystemColors.ControlText;
        }

        private void ProjectExplorerTreeView_ItemDrag(Object sender, ItemDragEventArgs e)
        {
            draggedItem = e.Item as TreeNodeAdv;
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void ProjectExplorerTreeView_DragOver(Object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void ProjectExplorerTreeView_DragEnter(Object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void ProjectExplorerTreeView_DragDrop(Object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.
            Point targetPoint = ProjectExplorerTreeView.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNodeAdv targetNodeAdv = ProjectExplorerTreeView.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNodeAdv[] draggedNodesAdv = e.Data.GetData(typeof(TreeNodeAdv[])) as TreeNodeAdv[];

            if (draggedNodesAdv.Count() <= 0)
                return;

            TreeNodeAdv draggedNodeAdv = draggedNodesAdv[0];

            if (draggedNodeAdv != null && targetNodeAdv != null && draggedNodeAdv != targetNodeAdv)
            {
                ProjectItem draggedItem = GetProjectItemFromNode(draggedNodeAdv);
                ProjectItem targetItem = GetProjectItemFromNode(targetNodeAdv);

                switch (ProjectExplorerTreeView.DropPosition.Position)
                {
                    case NodePosition.Before:
                        if (!draggedItem.HasNode(targetItem))
                        {
                            projectRoot.RemoveNode(draggedItem);
                            targetItem.AddSibling(draggedItem, false);
                        }
                        break;
                    case NodePosition.Inside:
                        if (!draggedItem.HasNode(targetItem))
                        {
                            projectRoot.RemoveNode(draggedItem);
                            targetItem.AddChild(draggedItem);
                        }
                        break;
                    case NodePosition.After:
                        if (!draggedItem.HasNode(targetItem))
                        {
                            projectRoot.RemoveNode(draggedItem);
                            targetItem.AddSibling(draggedItem, true);
                        }
                        break;
                }

                EventRefreshProjectStructure(projectRoot);
            }
        }

        #endregion

    } // End class

} // End namespace