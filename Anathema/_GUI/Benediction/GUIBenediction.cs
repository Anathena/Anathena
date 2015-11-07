using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Binarysharp.MemoryManagement;

namespace Anathema
{
    partial class GUIBenediction : UserControl, IBenedictionView, IProcessObserver
    {
        // Display constants
        private const Int32 MarginSize = 4;
        private const Int32 MaximumDisplayed = 4000;
        
        // Filter GUI options
        private GUIFilterManualScan GUIFilterManualScan;
        private GUIFilterTreeScan GUIFilterTreeScan;
        private GUIFilterFSM GUIFilterFSM;
        
        // Presenter
        private BenedictionPresenter BenedictionPresenter;

        public GUIBenediction()
        {
            InitializeComponent();

            // Create filter panel options
            GUIFilterManualScan = new GUIFilterManualScan();
            GUIFilterTreeScan = new GUIFilterTreeScan();
            GUIFilterFSM = new GUIFilterFSM();

            // Set the dock properties to fill their panel
            GUIFilterManualScan.Dock = DockStyle.Fill;
            GUIFilterTreeScan.Dock = DockStyle.Fill;
            GUIFilterFSM.Dock = DockStyle.Fill;

            // Initialize presenter
            BenedictionPresenter = new BenedictionPresenter(this, Benediction.GetBenedictionInstance());
        }

        public void EventCallbackTest()
        {
            int i = 0;
        }

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            BenedictionPresenter.UpdateProcess(MemoryEditor);
            GUIFilterTreeScan.UpdateProcess(MemoryEditor);
        }

        public void UpdateMemoryLabels(List<Tuple<IntPtr, Object>> MemoryLabels)
        {
            for (Int32 Index = 0; Index < Math.Min(MemoryLabels.Count, MaximumDisplayed); Index++)
            {
                ListViewItem NextEntry = new ListViewItem(MemoryLabels[Index].Item1.ToString());
                NextEntry.SubItems.Add(MemoryLabels[Index].Item2.ToString());
                NextEntry.SubItems.Add("");
                AddressListView.Items.Add(NextEntry);
            }
        }

        /// <summary>
        /// Updates the filter display panel to show the given user control
        /// </summary>
        /// <param name="UserControl"></param>
        private void UpdateFilterPanelDisplay(UserControl UserControl)
        {
            foreach (Object Next in FilterPanel.Controls)
            {
                if (Next.Equals(FilterToolStrip))
                    continue;

                FilterPanel.Controls.Remove((UserControl)Next);
            }

            FilterPanel.Controls.Add(UserControl);
            HandleGUIResize();
        }

        private void UpdateSnapshotManagerDisplay()
        {

        }

        /// <summary>
        /// Properly places and resizes necessary controls when the GUI is resized
        /// </summary>
        private void HandleGUIResize()
        {
            // Position the snapshot tree view
            SnapshotTreeView.Location = new Point(MarginSize, MarginSize);
            SnapshotTreeView.Height = TableListView.Location.Y - SnapshotTreeView.Location.Y - MarginSize;

            // Position the labeler panel
            LabelerPanel.Location = new Point(this.Width - LabelerPanel.Width - MarginSize, MarginSize);
            LabelerPanel.Height = TableListView.Location.Y - LabelerPanel.Location.Y - MarginSize;

            // Position the filter panel
            FilterPanel.Location = new Point(SnapshotTreeView.Location.X + SnapshotTreeView.Width + MarginSize, MarginSize);
            FilterPanel.Width = LabelerPanel.Location.X - FilterPanel.Location.X - MarginSize;
            FilterPanel.Height = TableListView.Location.Y - FilterPanel.Location.Y - MarginSize;

            // Position the display panel
            DisplayPanel.Location = new Point(MarginSize, DisplayPanel.Location.Y);

            // Position the table list view
            TableListView.Location = new Point(DisplayPanel.Location.X + DisplayPanel.Width + MarginSize,
                TableListView.Location.Y);
            TableListView.Width = this.Width - TableListView.Location.X - MarginSize;
            TableListView.Height = this.Height - TableListView.Location.Y - MarginSize;
        }

        private void GUIBenediction_Resize(object sender, EventArgs e)
        {
            HandleGUIResize();
        }

        private void SearchSpaceAnalysisButton_Click(object sender, EventArgs e)
        {
            UpdateFilterPanelDisplay(GUIFilterTreeScan);
        }

        private void FiniteStateMachineButton_Click(object sender, EventArgs e)
        {
            UpdateFilterPanelDisplay(GUIFilterFSM);
        }

        private void ManualScanButton_Click(object sender, EventArgs e)
        {
            UpdateFilterPanelDisplay(GUIFilterManualScan);
        }
        
    }
}
