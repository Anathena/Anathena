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
    public partial class GUIResults : DockContent, IResultsView
    {
        private ResultsPresenter ResultsPresenter;

        /*
        ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                // Pause drawing and updating (removes flicker)
                ResultsListView.SuspendLayout();
                ResultsListView.BeginUpdate();

                // Save the top item in the list
                Int32 TopItemIndex = 0;
                if (ResultsListView.TopItem != null)
                    TopItemIndex = ResultsListView.TopItem.Index;

                // Update current results
                ResultsListView.Items.Clear();
                ResultsListView.Items.AddRange(Items);

                // Restore scroll location
                if (TopItemIndex >= 0 && TopItemIndex < ResultsListView.Items.Count)
                    ResultsListView.TopItem = ResultsListView.Items[TopItemIndex];

                // Resume drawing and updating
                ResultsListView.EndUpdate();
                ResultsListView.ResumeLayout();

            });
        */

        public GUIResults()
        {
            InitializeComponent();

            ResultsPresenter = new ResultsPresenter(this, Results.GetInstance());
        }

        public void UpdateMemorySize(String MemorySize)
        {
            ControlThreadingHelper.InvokeControlAction(SnapshotSizeValueLabel, () =>
            {
                this.SnapshotSizeValueLabel.Text = MemorySize;
            });
        }

        public void UpdateItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                this.ResultsListView.VirtualListSize = ItemCount;
            });
        }

        public void RefreshResults()
        {
            // Force the list view to retrieve items again by signaling an update
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                ResultsListView.BeginUpdate();
                ResultsListView.EndUpdate();
            });
        }
        
        private void ByteToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(SByte));
        }

        private void Int16ToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Int16));
        }

        private void Int32ToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Int32));
        }

        private void Int64ToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Int64));
        }

        private void SingleToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Single));
        }

        private void DoubleToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Double));
        }

        private void ChangeSignToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.ChangeSign();
        }

        private void ResultsListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = ResultsPresenter.GetItemAt(E.ItemIndex);
        }
    }
}
