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
    public partial class Anathema : Form
    {
        private SearchSpaceAnalyzer SearchSpaceAnalyzer = new SearchSpaceAnalyzer();

        public Anathema()
        {
            InitializeComponent();
        }

        private void ProcessSelected(Process TargetProcess)
        {
            SelectedProcessLabel.Text = TargetProcess.ProcessName;

            // Pass the target process through to all components
            SearchSpaceAnalyzer.SetTargetProcess(TargetProcess);
        }

        private void SelectProcessButton_Click(object sender, EventArgs e)
        {
            ProcessSelector SelectProcess = new ProcessSelector(ProcessSelected);
            SelectProcess.ShowDialog();
        }

        private void StartSSAButton_Click(object sender, EventArgs e)
        {
            SearchSpaceAnalyzer.Begin();
        }

        private void EndSSAButton_Click(object sender, EventArgs e)
        {
            SearchSpaceAnalyzer.EndScan();
        }
        
        private void StartRSSAButton_Click(object sender, EventArgs e)
        {
            SearchSpaceAnalyzer.Begin(0x400); // 0x400 found experimentally to be very good
        }

        private void EndRSSAButton_Click(object sender, EventArgs e)
        {
            SearchSpaceAnalyzer.BeginScan();
        }

        private void PageVisualizerButton_Click(object sender, EventArgs e)
        {
            PageVisualizer PageVisualizer = new PageVisualizer(SearchSpaceAnalyzer.GetHistory());
            PageVisualizer.ShowDialog();
        }
    }
}
