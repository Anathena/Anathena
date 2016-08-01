using Anathema.Source.DotNetExplorer;
using Anathema.Source.Engine.AddressResolver.DotNet;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIDotNetExplorer : DockContent, IDotNetExplorerView
    {
        private DotNetExplorerPresenter DotNetExplorerPresenter;
        private Dictionary<TreeNode, DotNetObject> TreeObjectMapping;

        public GUIDotNetExplorer()
        {
            InitializeComponent();

            // Initialize presenter
            DotNetExplorerPresenter = new DotNetExplorerPresenter(this, new DotNetExplorer());
            TreeObjectMapping = new Dictionary<TreeNode, DotNetObject>();

            DotNetExplorerPresenter.RefreshObjectTrees();
        }

        public void UpdateObjectTrees(List<DotNetObject> ObjectTrees)
        {
            ControlThreadingHelper.InvokeControlAction(ObjectExplorerTreeView, () =>
            {
                TreeObjectMapping.Clear();
                ObjectExplorerTreeView.Nodes.Clear();

                TreeNode[] Nodes = new TreeNode[ObjectTrees.Count];
                ObjectTrees.ForEach(X => Nodes[ObjectTrees.IndexOf(X)] = CreateNodeFromDotNetObject(X));
                ObjectTrees.ForEach(X => AddChildren(Nodes[ObjectTrees.IndexOf(X)], X));

                ObjectExplorerTreeView.Nodes.AddRange(Nodes);
            });
        }

        private void AddChildren(TreeNode TreeNode, DotNetObject DotNetObject)
        {
            if (DotNetObject.GetChildren().Count == 0)
                return;

            TreeNode[] Children = new TreeNode[DotNetObject.GetChildren().Count];
            DotNetObject.GetChildren().ForEach(X => Children[DotNetObject.GetChildren().IndexOf(X)] = CreateNodeFromDotNetObject(X));
            DotNetObject.GetChildren().ForEach(X => AddChildren(Children[DotNetObject.GetChildren().IndexOf(X)], X));
            TreeNode.Nodes.AddRange(Children);
        }

        private TreeNode CreateNodeFromDotNetObject(DotNetObject DotNetObject)
        {
            TreeNode TreeNode = new TreeNode(DotNetObject?.GetName());
            TreeObjectMapping[TreeNode] = DotNetObject;
            return TreeNode;
        }

        #region Events

        private void RefreshButton_Click(Object Sender, EventArgs E)
        {
            DotNetExplorerPresenter.RefreshObjectTrees();
        }

        #endregion

        private void ObjectExplorerTreeView_NodeMouseDoubleClick(Object Sender, TreeNodeMouseClickEventArgs E)
        {
            DotNetObject DotNetObject;
            if (!TreeObjectMapping.TryGetValue(E.Node, out DotNetObject))
                return;

            DotNetExplorerPresenter.AddToTable(DotNetObject);
        }

    } // End class

} // End namespace