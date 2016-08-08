using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Anathena.GUI.CustomControls.TreeViews;
using Anathena.Source.Project;
using Anathena.Source.Project.ProjectItems;
using Anathena.Source.Utils.Caches;
using Anathena.Source.Utils.Extensions;
using Anathena.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools
{
    public partial class GUIProjectExplorer : DockContent, IProjectExplorerView
    {
        private ProjectExplorerPresenter ProjectExplorerPresenter;

        private BiDictionary<ProjectItem, ProjectNode> NodeCache;
        private TreeModel ProjectTree;
        private ProjectItem ProjectRoot;
        private TreeNodeAdv DraggedItem;
        private Object AccessLock;

        public GUIProjectExplorer()
        {
            InitializeComponent();

            EntryCheckBox.IsEditEnabledValueNeeded += CheckIndex;

            NodeCache = new BiDictionary<ProjectItem, ProjectNode>();
            ProjectTree = new TreeModel();
            AccessLock = new Object();

            (ProjectExplorerTreeView.NodeControls[ProjectExplorerTreeView.NodeControls.IndexOf(EntryDescription)] as BaseTextControl).DrawText += new EventHandler<DrawEventArgs>(FolderBrowser_DrawText);

            ProjectExplorerTreeView.Model = ProjectTree;
            ProjectExplorerPresenter = new ProjectExplorerPresenter(this, ProjectExplorer.GetInstance());
        }

        public void EventRefreshProjectStructure(ProjectItem ProjectRoot)
        {
            if (ProjectExplorerPresenter == null)
                return;

            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                this.ProjectRoot = ProjectRoot;

                ProjectExplorerTreeView.BeginUpdate();
                ProjectTree.Nodes.Clear();
                NodeCache.Clear();

                if (ProjectRoot != null)
                {
                    foreach (ProjectItem Child in ProjectRoot.Children)
                        BuildNodes(Child);
                }

                ProjectExplorerTreeView.EndUpdate();
                ProjectExplorerTreeView.ExpandAll();
            });
        }

        private void BuildNodes(ProjectItem ProjectItem, ProjectItem Parent = null)
        {
            if (ProjectItem == null)
                return;

            Image Image = null;

            if (ProjectItem is AddressItem)
                Image = new Bitmap(Properties.Resources.CollectValues);
            else if (ProjectItem is ScriptItem)
                Image = new Bitmap(Properties.Resources.CollectValues);
            else if (ProjectItem is FolderItem)
                Image = new Bitmap(Properties.Resources.Open);

            // Create new node to insert
            ProjectNode ProjectNode = new ProjectNode(ProjectItem.Description);
            ProjectNode.ProjectItem = ProjectItem;
            ProjectNode.EntryIcon = Image;
            ProjectNode.IsChecked = ProjectItem.GetActivationState();

            if (Parent != null && NodeCache.ContainsKey(Parent))
                NodeCache[Parent].Nodes.Add(ProjectNode);
            else
                ProjectTree.Nodes.Add(ProjectNode);

            NodeCache.Add(ProjectItem, ProjectNode);

            foreach (ProjectItem Child in ProjectItem.Children)
                BuildNodes(Child, ProjectItem);
        }

        private ProjectNode GetProjectNodeFromTreeNodeAdv(TreeNodeAdv TreeNodeAdv)
        {
            Node Node = ProjectTree.FindNode(ProjectExplorerTreeView.GetPath(TreeNodeAdv));

            if (Node == null || !typeof(ProjectNode).IsAssignableFrom(Node.GetType()))
                return null;

            ProjectNode ProjectNode = Node as ProjectNode;

            return ProjectNode;
        }

        private ProjectItem GetProjectItemFromNode(TreeNodeAdv TreeNodeAdv)
        {
            return GetProjectNodeFromTreeNodeAdv(TreeNodeAdv)?.ProjectItem;
        }

        public void AddNewAddressItem()
        {
            ProjectExplorerPresenter.AddNewAddressItem(GetSelectedItem());
        }

        public void AddNewScriptItem()
        {
            ProjectExplorerPresenter.AddNewScriptItem(GetSelectedItem());
        }

        public void AddNewFolderItem()
        {
            ProjectExplorerPresenter.AddNewFolderItem(GetSelectedItem());
        }

        private ProjectItem GetSelectedItem()
        {
            Node SelectedNode = ProjectTree.FindNode(ProjectExplorerTreeView.GetPath(ProjectExplorerTreeView.SelectedNode));
            ProjectItem SelectedItem = null;

            if (SelectedNode != null && typeof(ProjectNode).IsAssignableFrom(SelectedNode.GetType()))
            {
                if (NodeCache.Reverse.ContainsKey((ProjectNode)SelectedNode))
                    SelectedItem = NodeCache.Reverse[(ProjectNode)SelectedNode];
            }

            return SelectedItem;
        }

        private void ActivateSelectedItems()
        {
            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                if (ProjectExplorerTreeView.SelectedNodes == null || ProjectExplorerTreeView.SelectedNodes.Count <= 0)
                    return;

                List<ProjectItem> Nodes = new List<ProjectItem>();
                ProjectExplorerTreeView.SelectedNodes.ForEach(X => Nodes.Add(GetProjectItemFromNode(X)));

                if (Nodes.Count <= 0)
                    return;

                // Behavior here is undefined, we could check only the selected items, or enforce the recursive rules of folders
                // Nodes.ForEach(X => DoCheck(X, !X.GetActivationState()));
                ProjectExplorerPresenter.ActivateProjectItems(Nodes, !Nodes.First().GetActivationState());
                ProjectExplorerTreeView.SelectedNodes.ForEach(X => NodeCache[GetProjectItemFromNode(X)].IsChecked = GetProjectItemFromNode(X).GetActivationState());

                EventRefreshProjectStructure(ProjectRoot);
            });
        }

        private void DeleteSelectedItems()
        {
            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                if (ProjectExplorerTreeView.SelectedNodes == null || ProjectExplorerTreeView.SelectedNodes.Count <= 0)
                    return;

                List<ProjectItem> Nodes = new List<ProjectItem>();
                ProjectExplorerTreeView.SelectedNodes.ForEach(X => Nodes.Add(GetProjectItemFromNode(X)));
                ProjectExplorerPresenter.DeleteProjectItems(Nodes);

                EventRefreshProjectStructure(ProjectRoot);
            });
        }

        private void DoCheck(ProjectItem ProjectItem, Boolean Activated)
        {
            if (ProjectItem == null)
                return;

            ProjectExplorerPresenter.ActivateProjectItem(ProjectItem, Activated);
            NodeCache[ProjectItem].IsChecked = ProjectItem.GetActivationState();

            if (!(ProjectItem is FolderItem))
                return;

            foreach (ProjectItem Child in ProjectItem.Children)
                DoCheck(Child, Activated);
        }

        #region Events

        private void AddressToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewAddressItem();
        }

        private void AddNewScriptToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewScriptItem();
        }

        private void FolderToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewFolderItem();
        }

        private void AddressRightClickToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewAddressItem();
        }

        private void ScriptRightClickToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewScriptItem();
        }

        private void FolderRightClickToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewFolderItem();
        }

        private void DeleteSelectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            DeleteSelectedItems();
        }

        private void ToggleFreezeToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ActivateSelectedItems();
        }

        private void ProjectExplorerTreeView_KeyPress(Object Sender, KeyPressEventArgs E)
        {
            if (E.KeyChar != ' ')
                return;

            ActivateSelectedItems();
        }

        private void ProjectContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
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

        private void CheckIndex(Object Sender, NodeControlValueEventArgs E)
        {
            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                if (E.Node == null)
                    return;

                ProjectItem ProjectItem = GetProjectItemFromNode(E.Node);

                if (ProjectItem == null)
                    return;

                DoCheck(ProjectItem, !ProjectItem.GetActivationState());

                EventRefreshProjectStructure(ProjectRoot);
            });
        }

        private void ProjectExplorerTreeView_SelectionChanged(Object Sender, EventArgs E)
        {
            List<TreeNodeAdv> TreeNodes = new List<TreeNodeAdv>();
            List<ProjectItem> ProjectItems = new List<ProjectItem>();

            ProjectExplorerTreeView.SelectedNodes.ForEach(X => TreeNodes.Add(X));
            TreeNodes.ForEach(X => ProjectItems.Add(GetProjectItemFromNode(X)));

            ProjectExplorerPresenter.UpdateSelection(ProjectItems);
        }

        private void FolderBrowser_DrawText(Object Sender, DrawEventArgs E)
        {
            ProjectItem ProjectItem = GetProjectItemFromNode(E.Node);

            if (ProjectItem != null)
                E.TextColor = ProjectItem.TextColor;
            else
                E.TextColor = SystemColors.ControlText;
        }

        private void ProjectExplorerTreeView_ItemDrag(Object Sender, ItemDragEventArgs E)
        {
            DraggedItem = E.Item as TreeNodeAdv;
            DoDragDrop(E.Item, DragDropEffects.Move);
        }

        private void ProjectExplorerTreeView_DragOver(Object Sender, DragEventArgs E)
        {
            E.Effect = DragDropEffects.Move;
        }

        private void ProjectExplorerTreeView_DragEnter(Object Sender, DragEventArgs E)
        {
            E.Effect = DragDropEffects.Move;
        }

        private void ProjectExplorerTreeView_DragDrop(Object Sender, DragEventArgs E)
        {
            // Retrieve the client coordinates of the drop location.
            Point TargetPoint = ProjectExplorerTreeView.PointToClient(new Point(E.X, E.Y));

            // Retrieve the node at the drop location.
            TreeNodeAdv TargetNodeAdv = ProjectExplorerTreeView.GetNodeAt(TargetPoint);

            // Retrieve the node that was dragged.
            TreeNodeAdv[] DraggedNodesAdv = E.Data.GetData(typeof(TreeNodeAdv[])) as TreeNodeAdv[];

            if (DraggedNodesAdv.Count() <= 0)
                return;

            TreeNodeAdv DraggedNodeAdv = DraggedNodesAdv[0];

            if (DraggedNodeAdv != null && TargetNodeAdv != null && DraggedNodeAdv != TargetNodeAdv)
            {
                ProjectItem DraggedItem = GetProjectItemFromNode(DraggedNodeAdv);
                ProjectItem TargetItem = GetProjectItemFromNode(TargetNodeAdv);

                switch (ProjectExplorerTreeView.DropPosition.Position)
                {
                    case NodePosition.Before:
                        if (!DraggedItem.HasNode(TargetItem))
                        {
                            ProjectRoot.RemoveNode(DraggedItem);
                            TargetItem.AddSibling(DraggedItem, false);
                        }
                        break;
                    case NodePosition.Inside:
                        if (!DraggedItem.HasNode(TargetItem))
                        {
                            ProjectRoot.RemoveNode(DraggedItem);
                            TargetItem.AddChild(DraggedItem);
                        }
                        break;
                    case NodePosition.After:
                        if (!DraggedItem.HasNode(TargetItem))
                        {
                            ProjectRoot.RemoveNode(DraggedItem);
                            TargetItem.AddSibling(DraggedItem, true);
                        }
                        break;
                }

                EventRefreshProjectStructure(ProjectRoot);
            }
        }

        #endregion

    } // End class

} // End namespace