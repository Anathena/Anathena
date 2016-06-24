using Anathema.Source.Engine.DotNetObjectCollector;
using Anathema.Source.Utils.DotNetExplorer;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIDotNetExplorer : DockContent, IDotNetExplorerView
    {
        private DotNetExplorerPresenter GDotNetExplorerPresenter;

        public GUIDotNetExplorer()
        {
            InitializeComponent();

            // Initialize presenter
            GDotNetExplorerPresenter = new DotNetExplorerPresenter(this, new DotNetExplorer());

            GDotNetExplorerPresenter.RefreshObjectTrees();
        }

        public void UpdateObjectTrees(List<DotNetObject> ObjectTrees)
        {
            ControlThreadingHelper.InvokeControlAction(ObjectExplorerTreeView, () =>
            {
                ObjectExplorerTreeView.Nodes.Clear();

                TreeNode[] Nodes = new TreeNode[ObjectTrees.Count];
                ObjectTrees.ForEach(X => Nodes[ObjectTrees.IndexOf(X)] = new TreeNode(X.GetObjectType()));
                ObjectTrees.ForEach(X => AddChildren(Nodes[ObjectTrees.IndexOf(X)], X));

                ObjectExplorerTreeView.Nodes.AddRange(Nodes);
            });
        }

        private void AddChildren(TreeNode TreeNode, DotNetObject DotNetObject)
        {
            if (DotNetObject.GetChildren().Count == 0)
                return;

            TreeNode[] Children = new TreeNode[DotNetObject.GetChildren().Count];
            DotNetObject.GetChildren().ForEach(X => Children[DotNetObject.GetChildren().IndexOf(X)] = new TreeNode(X.GetObjectType()));
            DotNetObject.GetChildren().ForEach(X => AddChildren(Children[DotNetObject.GetChildren().IndexOf(X)], X));
            TreeNode.Nodes.AddRange(Children);
        }

        #region Events

        private void RefreshButton_Click(Object Sender, EventArgs E)
        {
            GDotNetExplorerPresenter.RefreshObjectTrees();
        }

        #endregion

    } // End class

} // End namespace