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

                ObjectExplorerTreeView.Nodes.AddRange(Nodes);
            });
        }

        private void AddAllItems()
        {

        }

        #region Events

        private void RefreshButton_Click(Object Sender, EventArgs Ee)
        {
            GDotNetExplorerPresenter.RefreshObjectTrees();
        }

        #endregion

    } // End class

} // End namespace