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
        }

        private void GUISnapshotManager_Load(object sender, EventArgs e)
        {
            // Initialize presenter
            SnapshotManagerPresenter SnapshotManagerPresenter = new SnapshotManagerPresenter(this, SnapshotManager.GetInstance());
        }

        public void UpdateSnapshotDisplay(ListViewItem[] Snapshots)
        {
            ControlThreadingHelper.InvokeControlAction(SnapshotListView, () =>
            {
                SnapshotListView.Items.Clear();
                SnapshotListView.Items.AddRange(Snapshots);
            });
        }

        private void ClearSnapshotsButton_Click(object sender, EventArgs e)
        {

        }
    }
}
