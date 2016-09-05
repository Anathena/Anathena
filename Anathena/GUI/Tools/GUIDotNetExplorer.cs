using Aga.Controls.Tree;
using Anathena.GUI.CustomControls.TreeViews;
using Anathena.Source.DotNetExplorer;
using Anathena.Source.Engine.AddressResolver.DotNet;
using Anathena.Source.Utils.DataStructures;
using Anathena.Source.Utils.Extensions;
using Anathena.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools
{
    public partial class GUIDotNetExplorer : DockContent, IDotNetExplorerView
    {
        private DotNetExplorerPresenter DotNetExplorerPresenter;
        private BiDictionary<DotNetObject, DotNetNode> NodeCache;
        private TreeModel ProjectTree;

        public GUIDotNetExplorer()
        {
            InitializeComponent();

            NodeCache = new BiDictionary<DotNetObject, DotNetNode>();
            ProjectTree = new TreeModel();

            // Initialize presenter
            DotNetExplorerPresenter = new DotNetExplorerPresenter(this, new DotNetExplorer());

            ObjectExplorerTreeView.Model = ProjectTree;
            DotNetExplorerPresenter.RefreshObjectTrees();
        }


        public void UpdateObjectTrees(List<DotNetObject> ObjectTrees)
        {
            if (DotNetExplorerPresenter == null)
                return;

            ControlThreadingHelper.InvokeControlAction(ObjectExplorerTreeView, () =>
            {
                ObjectExplorerTreeView.BeginUpdate();
                ProjectTree.Nodes.Clear();
                NodeCache.Clear();

                if (ObjectTrees != null)
                {
                    foreach (DotNetObject DotNetObject in ObjectTrees)
                        BuildNodes(DotNetObject);
                }

                ObjectExplorerTreeView.EndUpdate();
            });
        }

        private void BuildNodes(DotNetObject DotNetObject, DotNetObject Parent = null)
        {
            if (DotNetObject == null)
                return;

            // Create new node to insert
            DotNetNode ProjectNode = new DotNetNode(DotNetObject.GetName());
            ProjectNode.DotNetObject = DotNetObject;

            if (Parent != null && NodeCache.ContainsKey(Parent))
                NodeCache[Parent].Nodes.Add(ProjectNode);
            else
                ProjectTree.Nodes.Add(ProjectNode);

            NodeCache.Add(DotNetObject, ProjectNode);

            foreach (DotNetObject Child in DotNetObject.GetChildren())
                BuildNodes(Child, DotNetObject);
        }

        private DotNetNode GetDotNetNodeFromTreeNodeAdv(TreeNodeAdv TreeNodeAdv)
        {
            Node Node = ProjectTree.FindNode(ObjectExplorerTreeView.GetPath(TreeNodeAdv));

            if (Node == null || !typeof(DotNetNode).IsAssignableFrom(Node.GetType()))
                return null;

            DotNetNode DotNetNode = Node as DotNetNode;

            return DotNetNode;
        }

        private DotNetObject GetProjectItemFromNode(TreeNodeAdv TreeNodeAdv)
        {
            return GetDotNetNodeFromTreeNodeAdv(TreeNodeAdv)?.DotNetObject;
        }

        #region Events

        private void RefreshButton_Click(Object Sender, EventArgs E)
        {
            DotNetExplorerPresenter.RefreshObjectTrees();
        }

        private void ObjectExplorerTreeView_NodeMouseDoubleClick(Object Sender, TreeNodeAdvMouseEventArgs E)
        {
            DotNetNode Node = GetDotNetNodeFromTreeNodeAdv(E.Node);

            if (Node == null)
                return;

            if (!NodeCache.Reverse.ContainsKey(Node))
                return;

            DotNetExplorerPresenter.AddToTable(NodeCache.Reverse[Node]);
        }

        private void ObjectExplorerTreeView_SelectionChanged(Object Sender, EventArgs E)
        {
            List<TreeNodeAdv> TreeNodes = new List<TreeNodeAdv>();
            List<DotNetObject> DotNetObjects = new List<DotNetObject>();

            ObjectExplorerTreeView.SelectedNodes.ForEach(X => TreeNodes.Add(X));
            TreeNodes.ForEach(X => DotNetObjects.Add(GetProjectItemFromNode(X)));

            DotNetExplorerPresenter.UpdateSelection(DotNetObjects);
        }

        #endregion

    } // End class

} // End namespace