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

namespace Anathema
{
    public partial class GUIAnathema : UserControl
    {
        const Int32 MarginSize = 4;
        const Int32 MaximumDisplayed = 4000;

        public GUIAnathema()
        {
            InitializeComponent();
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

        private void GUIAnathema_Resize(object sender, EventArgs e)
        {
            HandleGUIResize();
        }

        /*
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
            for (Int32 Index = 0; Index < FilterPanel.Controls.Count; Index++)
            {
                if (FilterPanel.Controls[Index] != FilterToolStrip)
                    FilterPanel.Controls.RemoveAt(Index--);
            }
            FilterPanel.Controls.Add(UserControl);
            HandleGUIResize();
        }

        private void UpdateSnapshotManagerDisplay()
        {

        }

        

        /// <summary>
        /// Informs that a new process has been selected
        /// </summary>
        /// <param name="TargetProcess"></param>
        private void ProcessSelected(Process TargetProcess)
        {
            //Anathema.UpdateTargetProcess(TargetProcess);
            //SelectedProcessLabel.Text = TargetProcess.ProcessName;
            //UpdateFilterPanelDisplay(new GUIMemoryTreeFilter());
        }

        private void GUIMain_Load(object sender, EventArgs e)
        {
            // I will not use this load event, but I usually double click my forms to quickly
            // get to my code, which will auto generate this function, so I will keep it here
        }

        private void SelectProcessButton_Click(object sender, EventArgs e)
        {
            UpdateFilterPanelDisplay(new GUIProcessSelector(ProcessSelected));
        }

        private void SearchSpaceAnalysisButton_Click(object sender, EventArgs e)
        {
            UpdateFilterPanelDisplay(new GUIMemoryTreeFilter());
        }

        private void FiniteStateMachineButton_Click(object sender, EventArgs e)
        {
            UpdateFilterPanelDisplay(new GUIFiniteStateMachinePanel());
        }

        private void InputCorrelatorButton_Click(object sender, EventArgs e)
        {
            UpdateFilterPanelDisplay(new GUIInputCorrelator());
        }

        private void ManualScanButton_Click(object sender, EventArgs e)
        {
            UpdateFilterPanelDisplay(new GUIManualScan());
        }
        */
    }
}
