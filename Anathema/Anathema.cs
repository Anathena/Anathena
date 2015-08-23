﻿using System;
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
            SearchSpaceAnalyzer.Begin(SearchSpaceAnalyzer.AnalysisModeEnum.SearchSpaceReduction, 0x40); // 0x400 good for size, 0x40 good for reduction (64 bytes)
        }

        private void EndSSAButton_Click(object sender, EventArgs e)
        {
            SearchSpaceAnalyzer.EndScan();
        }

        private void StartInputCorrelationButton_Click(object sender, EventArgs e)
        {
            SearchSpaceAnalyzer.Begin(SearchSpaceAnalyzer.AnalysisModeEnum.InputCorrelator);
        }

        private void EndInputCorrelationButton_Click(object sender, EventArgs e)
        {
            SearchSpaceAnalyzer.EndScan();
        }
    }
}
