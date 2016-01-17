using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
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
