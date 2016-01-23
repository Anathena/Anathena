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

        public GUIResults()
        {
            InitializeComponent();
            ResultsPresenter = new ResultsPresenter(this, Results.GetInstance());
        }

        public void UpdateMemorySizeLabel(String MemorySize)
        {
            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                SnapshotSizeValueLabel.Text = MemorySize;
            });
        }

        public void UpdateItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                ResultsListView.VirtualListSize = ItemCount;
            });
        }

        private void UpdateReadBounds()
        {
            const Int32 BoundsLimit = 128;
            const Int32 OverRead = 8;

            Int32 StartReadIndex = 0;
            Int32 EndReadIndex = 0;

            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                StartReadIndex = ResultsListView.TopItem == null ? 0 : ResultsListView.TopItem.Index ;

                ListViewItem LastVisibleItem = ResultsListView.TopItem;
                for (Int32 Index = StartReadIndex; Index < ResultsListView.Items.Count; Index++)
                {
                    if (Index - ResultsListView.TopItem.Index > BoundsLimit)
                        break;

                    if (ResultsListView.ClientRectangle.IntersectsWith(ResultsListView.Items[Index].Bounds))
                        LastVisibleItem = ResultsListView.Items[Index];
                    else
                        break;
                }

                StartReadIndex -= OverRead;
                EndReadIndex = LastVisibleItem == null ? 0 : LastVisibleItem.Index + 1 + OverRead;
            });
            ResultsPresenter.UpdateReadBounds(StartReadIndex, EndReadIndex);
        }

        public void EnableResults()
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                ResultsListView.Enabled = true;
            });
            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                GUIToolStrip.Enabled = true;
            });
        }

        public void DisableResults()
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                ResultsListView.Enabled = false;
            });
            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                GUIToolStrip.Enabled = false;
            });
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            // Force the list view to retrieve items again by signaling an update
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                ResultsListView.BeginUpdate();
                ResultsListView.EndUpdate();
            });
        }

        private void AddSelectedElements()
        {
            const Int32 AddMaximum = 4096;

            Int32 Count = 0;
            foreach (Int32 Index in ResultsListView.SelectedIndices)
            { 
                ResultsPresenter.AddSelectionToTable(Index);

                // Prevent adding too many elements. No user would do this intentionally.
                if (++Count > AddMaximum)
                    break;
            }
        }

        #region Events
        
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

        private void AddSelectedResultsButton_Click(Object Sender, EventArgs E)
        {
            AddSelectedElements();
        }

        private void ResultsListView_DoubleClick(Object Sender, EventArgs E)
        {
            AddSelectedElements();
        }

        private void AddToCheatsToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddSelectedElements();
        }

        #endregion

    } // End class

} // End mamespace