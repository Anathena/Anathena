using Anathena.Source.Results.ScanResults;
using Anathena.Source.Utils;
using Anathena.Source.Utils.DataStructures;
using Anathena.Source.Utils.MVP;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools
{
    public partial class GUIResults : DockContent, IScanResultsView
    {
        private ScanResultsPresenter scanResultsPresenter;
        private ListViewCache listViewCache;
        private Object accessLock;

        private const String noValueString = "-";

        public GUIResults()
        {
            InitializeComponent();
            scanResultsPresenter = new ScanResultsPresenter(this, ScanResults.GetInstance());
            listViewCache = new ListViewCache();
            accessLock = new Object();
        }

        public void UpdateMemorySizeLabel(String memorySize, String itemCount)
        {
            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                // FIXME: Odd bug where the ControlThreadingHelper is not doing it's job. Workaround for now.
                try
                {
                    SnapshotSizeValueLabel.Text = memorySize + " - (" + itemCount + ")";
                }
                catch { }
            });
        }

        public void UpdateItemCount(Int32 itemCount)
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    ResultsListView.BeginUpdate();
                    ResultsListView.SetItemCount(itemCount);
                    listViewCache.FlushCache();
                    ResultsListView.EndUpdate();
                }
            });
        }

        private void UpdateReadBounds()
        {
            Tuple<Int32, Int32> readBounds = null;

            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    readBounds = ResultsListView.GetReadBounds();
                }
            });

            if (readBounds != null)
                scanResultsPresenter.UpdateReadBounds(readBounds.Item1, readBounds.Item2);
        }

        public void SetEnabled(Boolean isEnabled)
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
                {
                    ResultsListView.Enabled = isEnabled;
                });

                ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
                {
                    GUIToolStrip.Enabled = isEnabled;
                });
            }
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            // Force the list view to retrieve items again by signaling an update
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    ResultsListView.BeginUpdate();
                    ResultsListView.EndUpdate();
                }
            });
        }

        private void AddSelectedElements()
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (ResultsListView.SelectedIndices.Count <= 0)
                        return;

                    scanResultsPresenter.AddSelectionToTable(ResultsListView.SelectedIndices[0], ResultsListView.SelectedIndices[ResultsListView.SelectedIndices.Count - 1]);
                }
            });
        }

        #region Events

        private void ByteToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            scanResultsPresenter.UpdateScanType(typeof(SByte));
        }

        private void Int16ToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            scanResultsPresenter.UpdateScanType(typeof(Int16));
        }

        private void Int32ToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            scanResultsPresenter.UpdateScanType(typeof(Int32));
        }

        private void Int64ToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            scanResultsPresenter.UpdateScanType(typeof(Int64));
        }

        private void SingleToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            scanResultsPresenter.UpdateScanType(typeof(Single));
        }

        private void DoubleToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            scanResultsPresenter.UpdateScanType(typeof(Double));
        }

        private void ChangeSignToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            scanResultsPresenter.ChangeSign();
        }

        private void ResultsListView_RetrieveVirtualItem(Object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem Item = listViewCache.Get((UInt64)e.ItemIndex);

            // Try to update value and return the item if it is a valid item
            if (Item != null && listViewCache.TryUpdateSubItem(e.ItemIndex,
                ResultsListView.Columns.IndexOf(ValueHeader), scanResultsPresenter.GetValueAtIndex(e.ItemIndex)))
            {
                e.Item = Item;
                return;
            }

            // Add the properties to the cache and get the list view item back
            Item = listViewCache.Add(e.ItemIndex, new String[ResultsListView.Columns.Count]);

            Item.SubItems[ResultsListView.Columns.IndexOf(AddressHeader)].Text = scanResultsPresenter.GetAddressAtIndex(e.ItemIndex);
            Item.SubItems[ResultsListView.Columns.IndexOf(ValueHeader)].Text = noValueString;
            Item.SubItems[ResultsListView.Columns.IndexOf(LabelHeader)].Text = scanResultsPresenter.GetLabelAtIndex(e.ItemIndex);

            e.Item = Item;
        }

        private void AddSelectedResultsButton_Click(Object sender, EventArgs e)
        {
            AddSelectedElements();
        }

        private void ResultsListView_DoubleClick(Object sender, EventArgs e)
        {
            AddSelectedElements();
        }

        private void AddToCheatsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            AddSelectedElements();
        }

        #endregion

    } // End class

} // End mamespace