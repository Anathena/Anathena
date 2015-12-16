using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace Anathema
{
    public partial class GUISnapshotManager : DockContent, ISnapshotManagerView
    {
        public GUISnapshotManager()
        {
            InitializeComponent();

            // Initialize presenter
            SnapshotManagerPresenter SnapshotManagerPresenter = new SnapshotManagerPresenter(this, SnapshotManager.GetSnapshotManagerInstance());
        }

        public void UpdateSnapshotDisplay(TreeNode[] Snapshots)
        {
            SnapshotTreeView.Nodes.Clear();

            SnapshotTreeView.Nodes.AddRange(Snapshots);
        }
    }
}
