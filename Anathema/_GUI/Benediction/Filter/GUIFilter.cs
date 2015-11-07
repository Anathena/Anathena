using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Binarysharp.MemoryManagement;

namespace Anathema
{
    public partial class GUIFilter : UserControl, IProcessObserver
    {
        // Filter GUI options
        private GUIFilterManualScan GUIFilterManualScan;
        private GUIFilterTreeScan GUIFilterTreeScan;
        private GUIFilterFSM GUIFilterFSM;
        
        public GUIFilter()
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
        }

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            //GUIFilterManualScan.UpdateProcess(MemoryEditor);
        }

        /// <summary>
        /// Updates the filter display panel to show the given user control
        /// </summary>
        /// <param name="UserControl"></param>
        public void UpdateFilterPanelDisplay(UserControl UserControl)
        {
            foreach (Object Next in this.Controls)
            {
                if (Next.Equals(FilterToolStrip))
                    continue;

                this.Controls.Remove((UserControl)Next);
            }

            this.Controls.Add(UserControl);
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
