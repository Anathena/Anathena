using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anathena.GUI.CustomControls.TreeViews
{
    public partial class HighlightPreservingTreeView : TreeView
    {
        private TreeNode PreviouslySelectedNode = null;

        public HighlightPreservingTreeView()
        {
            InitializeComponent();

            this.Validating += HighlightPreservingTreeView_Validating;
            this.AfterSelect += HighlightPreservingTreeView_AfterSelect;
        }

        private void HighlightPreservingTreeView_Validating(Object Sender, EventArgs E)
        {
            if (SelectedNode == null)
                return;

            this.SelectedNode.BackColor = SystemColors.Highlight;
            this.SelectedNode.ForeColor = Color.White;
            PreviouslySelectedNode = this.SelectedNode;
        }

        private void HighlightPreservingTreeView_AfterSelect(Object Sender, TreeViewEventArgs E)
        {
            if (PreviouslySelectedNode != null)
            {
                PreviouslySelectedNode.BackColor = this.BackColor;
                PreviouslySelectedNode.ForeColor = this.ForeColor;
            }
        }

        public void SetSelection(TreeNode Node)
        {
            this.SelectedNode = Node;
            HighlightPreservingTreeView_AfterSelect(null, null);
            HighlightPreservingTreeView_Validating(null, null);
        }
    }
}
