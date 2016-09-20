using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ana.GUI.CustomControls.TreeViews
{
    public partial class HighlightPreservingTreeView : TreeView
    {
        private TreeNode previouslySelectedNode = null;

        public HighlightPreservingTreeView()
        {
            InitializeComponent();

            this.Validating += HighlightPreservingTreeView_Validating;
            this.AfterSelect += HighlightPreservingTreeView_AfterSelect;
        }

        private void HighlightPreservingTreeView_Validating(Object sender, EventArgs e)
        {
            if (SelectedNode == null)
                return;

            this.SelectedNode.BackColor = SystemColors.Highlight;
            this.SelectedNode.ForeColor = Color.White;
            previouslySelectedNode = this.SelectedNode;
        }

        private void HighlightPreservingTreeView_AfterSelect(Object sender, TreeViewEventArgs e)
        {
            if (previouslySelectedNode != null)
            {
                previouslySelectedNode.BackColor = this.BackColor;
                previouslySelectedNode.ForeColor = this.ForeColor;
            }
        }

        public void SetSelection(TreeNode node)
        {
            this.SelectedNode = node;
            HighlightPreservingTreeView_AfterSelect(null, null);
            HighlightPreservingTreeView_Validating(null, null);
        }
    }
}
