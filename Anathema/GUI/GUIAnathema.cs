using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIAnathema : Form
    {
        private Anathema Anathema { get; set; }
        private const Int32 MarginSize = 12;
        private const Int32 MaximumDisplayed = 4000;

        public GUIAnathema()
        {
            InitializeComponent();
            Anathema = Anathema.GetAnathemaInstance();
            HandleGUIResize();
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
            for (Int32 Index = 0; Index < FilterPanel.Controls.Count; Index++)
            {
                if (FilterPanel.Controls[Index] != FilterToolStrip)
                    FilterPanel.Controls.RemoveAt(Index--);
            }
            FilterPanel.Controls.Add(UserControl);
            HandleGUIResize();
        }

        /// <summary>
        /// Properly places and resizes necessary controls when the GUI is resized
        /// </summary>
        private void HandleGUIResize()
        {
            // This code assumes that all controls are properly docked and are already placed
            // the default margin size away from the side of the screen.

            FilterPanel.Width = AddressListView.Location.X - FilterPanel.Location.X - MarginSize;
            TableListView.Width = this.ClientRectangle.Width - MarginSize * 2;

            for (Int32 Index = 0; Index < FilterPanel.Controls.Count; Index++)
            {
                FilterPanel.Controls[Index].Width = FilterPanel.Width;
                FilterPanel.Controls[Index].Height = FilterPanel.Height;
            }
        }

        /// <summary>
        /// Informs that a new process has been selected
        /// </summary>
        /// <param name="TargetProcess"></param>
        private void ProcessSelected(Process TargetProcess)
        {
            Anathema.UpdateTargetProcess(TargetProcess);
            SelectedProcessLabel.Text = TargetProcess.ProcessName;
            UpdateFilterPanelDisplay(new GUIMemoryTreeFilter());
        }

        #region Event Handlers
        private void GUIAnathema_Load(object sender, EventArgs e)
        {
            // I will not use this load event, but I usually double click my forms to quickly
            // get to my code, which will auto generate this function, so I will keep it here
        }

        private void GUIAnathema_Resize(object sender, EventArgs e)
        {
            HandleGUIResize();
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

        #endregion

        private void LabelSelectorButton_Click(object sender, EventArgs e)
        {
            UpdateMemoryLabels(Anathema.GetMemoryLabels());
        }

        private void MemoryViewerButton_Click(object sender, EventArgs e)
        {
            GUIMemoryViewer GUIMemoryViewer = new GUIMemoryViewer();
            GUIMemoryViewer.Show();
        }
    }
}
