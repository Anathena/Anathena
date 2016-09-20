using Aga.Controls.Tree;
using Ana.GUI.CustomControls.TreeViews;
using Ana.Source.DotNetExplorer;
using Ana.Source.Engine.AddressResolver.DotNet;
using Ana.Source.Utils.DataStructures;
using Ana.Source.Utils.Extensions;
using Ana.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace Ana.GUI.Tools
{
    public partial class GUIDotNetExplorer : DockContent, IDotNetExplorerView
    {
        private DotNetExplorerPresenter dotNetExplorerPresenter;
        private BiDictionary<DotNetObject, DotNetNode> nodeCache;
        private TreeModel projectTree;

        public GUIDotNetExplorer()
        {
            InitializeComponent();

            nodeCache = new BiDictionary<DotNetObject, DotNetNode>();
            projectTree = new TreeModel();

            // Initialize presenter
            dotNetExplorerPresenter = new DotNetExplorerPresenter(this, new DotNetExplorer());

            ObjectExplorerTreeView.Model = projectTree;
            dotNetExplorerPresenter.RefreshObjectTrees();
        }


        public void UpdateObjectTrees(List<DotNetObject> objectTrees)
        {
            if (dotNetExplorerPresenter == null)
                return;

            ControlThreadingHelper.InvokeControlAction(ObjectExplorerTreeView, () =>
            {
                ObjectExplorerTreeView.BeginUpdate();
                projectTree.Nodes.Clear();
                nodeCache.Clear();

                if (objectTrees != null)
                {
                    foreach (DotNetObject dotNetObject in objectTrees)
                        BuildNodes(dotNetObject);
                }

                ObjectExplorerTreeView.EndUpdate();
            });
        }

        private void BuildNodes(DotNetObject dotNetObject, DotNetObject parent = null)
        {
            if (dotNetObject == null)
                return;

            // Create new node to insert
            DotNetNode projectNode = new DotNetNode(dotNetObject.GetName());
            projectNode.DotNetObject = dotNetObject;

            if (parent != null && nodeCache.ContainsKey(parent))
                nodeCache[parent].Nodes.Add(projectNode);
            else
                projectTree.Nodes.Add(projectNode);

            nodeCache.Add(dotNetObject, projectNode);

            foreach (DotNetObject child in dotNetObject.GetChildren())
                BuildNodes(child, dotNetObject);
        }

        private DotNetNode GetDotNetNodeFromTreeNodeAdv(TreeNodeAdv treeNodeAdv)
        {
            Node node = projectTree.FindNode(ObjectExplorerTreeView.GetPath(treeNodeAdv));

            if (node == null || !typeof(DotNetNode).IsAssignableFrom(node.GetType()))
                return null;

            DotNetNode DotNetNode = node as DotNetNode;

            return DotNetNode;
        }

        private DotNetObject GetProjectItemFromNode(TreeNodeAdv treeNodeAdv)
        {
            return GetDotNetNodeFromTreeNodeAdv(treeNodeAdv)?.DotNetObject;
        }

        #region Events

        private void RefreshButton_Click(Object sender, EventArgs e)
        {
            dotNetExplorerPresenter.RefreshObjectTrees();
        }

        private void ObjectExplorerTreeView_NodeMouseDoubleClick(Object sender, TreeNodeAdvMouseEventArgs e)
        {
            DotNetNode node = GetDotNetNodeFromTreeNodeAdv(e.Node);

            if (node == null)
                return;

            if (!nodeCache.Reverse.ContainsKey(node))
                return;

            dotNetExplorerPresenter.AddToTable(nodeCache.Reverse[node]);
        }

        private void ObjectExplorerTreeView_SelectionChanged(Object sender, EventArgs e)
        {
            List<TreeNodeAdv> treeNodes = new List<TreeNodeAdv>();
            List<DotNetObject> dotNetObjects = new List<DotNetObject>();

            ObjectExplorerTreeView.SelectedNodes.ForEach(x => treeNodes.Add(x));
            treeNodes.ForEach(x => dotNetObjects.Add(GetProjectItemFromNode(x)));

            dotNetExplorerPresenter.UpdateSelection(dotNetObjects);
        }

        #endregion

    } // End class

} // End namespace