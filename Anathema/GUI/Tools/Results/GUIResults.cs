using Anathema.Services.ScanResults;
using Anathema.Source.Utils;
using Anathema.Utils.Cache;
using Anathema.Utils.MVP;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIResults : DockContent, IResultsView
    {
        private ResultsPresenter ResultsPresenter;
        private ListViewCache ListViewCache;
        private Object AccessLock;

        private const String NoValueString = "-";

        public GUIResults()
        {
            InitializeComponent();
            ResultsPresenter = new ResultsPresenter(this, Results.GetInstance());
            ListViewCache = new ListViewCache();
            AccessLock = new Object();
        }

        public void UpdateMemorySizeLabel(String MemorySize, String ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(SnapshotSizeValueLabel.GetCurrentParent(), () =>
            {
                SnapshotSizeValueLabel.Text = MemorySize + " - (" + ItemCount + ")";
            });
        }

        public void UpdateItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    ResultsListView.BeginUpdate();
                    ResultsListView.SetItemCount(ItemCount);
                    ListViewCache.FlushCache();
                    ResultsListView.EndUpdate();
                }
            });
        }

        private void UpdateReadBounds()
        {
            Tuple<Int32, Int32> ReadBounds = null;

            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    ReadBounds = ResultsListView.GetReadBounds();
                }
            });

            if (ReadBounds != null)
                ResultsPresenter.UpdateReadBounds(ReadBounds.Item1, ReadBounds.Item2);
        }

        public void SetEnabled(Boolean IsEnabled)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
                {
                    ResultsListView.Enabled = IsEnabled;
                });

                ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
                {
                    GUIToolStrip.Enabled = IsEnabled;
                });
            }
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            // Force the list view to retrieve items again by signaling an update
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                using (TimedLock.Lock(AccessLock))
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
                using (TimedLock.Lock(AccessLock))
                {
                    if (ResultsListView.SelectedIndices.Count <= 0)
                        return;

                    ResultsPresenter.AddSelectionToTable(ResultsListView.SelectedIndices[0], ResultsListView.SelectedIndices[ResultsListView.SelectedIndices.Count - 1]);
                }
            });
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
            ListViewItem Item = ListViewCache.Get((UInt64)E.ItemIndex);

            // Try to update value and return the item if it is a valid item
            if (Item != null && ListViewCache.TryUpdateSubItem(E.ItemIndex,
                ResultsListView.Columns.IndexOf(ValueHeader), ResultsPresenter.GetValueAtIndex(E.ItemIndex)))
            {
                E.Item = Item;
                return;
            }

            // Add the properties to the cache and get the list view item back
            Item = ListViewCache.Add(E.ItemIndex, new String[ResultsListView.Columns.Count]);

            Item.SubItems[ResultsListView.Columns.IndexOf(AddressHeader)].Text = ResultsPresenter.GetAddressAtIndex(E.ItemIndex);
            Item.SubItems[ResultsListView.Columns.IndexOf(ValueHeader)].Text = NoValueString;
            Item.SubItems[ResultsListView.Columns.IndexOf(LabelHeader)].Text = ResultsPresenter.GetLabelAtIndex(E.ItemIndex);

            E.Item = Item;
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