using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Anathema.Source.Project;
using Anathema.Source.Project.ProjectItems;
using Anathema.Source.Utils.Caches;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIProjectExplorer : DockContent, IProjectExplorerView
    {
        private ProjectExplorerPresenter ProjectExplorerPresenter;

        private BiDictionary<ProjectItem, ProjectNode> NodeCache;
        private TreeModel ProjectTree;
        private Object AccessLock;

        private ProjectItem ProjectRootTEMPORARY_WORKAROUND;

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

        public void RefreshStructure(ProjectItem ProjectRoot)
        {
            if (ProjectExplorerPresenter == null)
                return;

            ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
            {
                ProjectRootTEMPORARY_WORKAROUND = ProjectRoot;

                ProjectExplorerTreeView.BeginUpdate();
                ProjectTree.Nodes.Clear();
                NodeCache.Clear();

                if (ProjectRoot != null)
                {
                    foreach (ProjectItem Child in ProjectRoot)
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

            foreach (ProjectItem Child in ProjectItem)
                BuildNodes(Child, ProjectItem);
        }

        private ProjectItem GetProjectItemFromNode(TreeNodeAdv TreeNodeAdv)
        {
            Node Node = ProjectTree.FindNode(ProjectExplorerTreeView.GetPath(TreeNodeAdv));

            if (Node == null || !typeof(ProjectNode).IsAssignableFrom(Node.GetType()))
                return null;

            ProjectNode ProjectNode = Node as ProjectNode;
            ProjectItem ProjectItem = ProjectNode.ProjectItem;

            return ProjectItem;
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

                RefreshStructure(ProjectRootTEMPORARY_WORKAROUND);
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

                RefreshStructure(ProjectRootTEMPORARY_WORKAROUND);
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

            foreach (ProjectItem Child in ProjectItem)
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

                RefreshStructure(ProjectRootTEMPORARY_WORKAROUND);
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

        #endregion

        #region Events(DEPRECATED - PLEASE REIMPLEMENT THIS SHIT SO I CAN DELETE IT)

        private ListViewItem DraggedItem;
        private void AddressTableListView_ItemDrag(Object Sender, ItemDragEventArgs E)
        {
            DraggedItem = (ListViewItem)E.Item;
            DoDragDrop(E.Item, DragDropEffects.All);
        }

        private void AddressTableListView_DragOver(Object Sender, DragEventArgs E)
        {
            E.Effect = DragDropEffects.All;
        }

        private void AddressTableListView_DragDrop(Object Sender, DragEventArgs E)
        {
            // using (TimedLock.Lock(AccessLock))
            {
                /*
                ListViewHitTestInfo HitTest = AddressTableListView.HitTest(AddressTableListView.PointToClient(new Point(E.X, E.Y)));
                ListViewItem SelectedItem = HitTest.Item;

                if (DraggedItem == null || DraggedItem == SelectedItem)
                    return;

                if ((SelectedItem != null && SelectedItem.GetType() != typeof(ListViewItem)) || DraggedItem.GetType() != typeof(ListViewItem))
                    return;

                AddressTablePresenter.ReorderItem(DraggedItem.Index, SelectedItem == null ? AddressTableListView.Items.Count : SelectedItem.Index);
                */
            }
        }

        #endregion

    } // End class

} // End namespace