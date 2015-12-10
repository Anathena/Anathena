using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema
{
    partial class GUIMain : Form
    {
        private MemorySharp MemoryEditor;

        // All GUI Components
        private GUIProcessSelector GUIProcessSelector;
        private GUIDebugger GUIDebugger;
        private GUIFilterFSM GUIFilterFSM;
        private GUIFilterManual GUIFilterManual;
        private GUIFilterTree GUIFilterTree;
        private GUILabelerChangeCounter GUILabelerChangeCounter;
        private GUILabelerInputCorrelator GUILabelerInputCorrelator;
        private GUISnapshotManager GUISnapshotManager;
        private GUIResults GUIResults;
        private GUITable GUITable;

        public GUIMain()
        {
            InitializeComponent();

            // Update theme so that everything looks cool
            this.ContentPanel.Theme = new VS2013BlueTheme();

            // Instantiate all primary GUI components
            // GUIProcessSelector = new GUIProcessSelector(UpdateTargetProcess);

            CreateDefaultTools();
        }

        /// <summary>
        /// Update the target process 
        /// </summary>
        /// <param name="TargetProcess"></param>
        public void UpdateTargetProcess(Process TargetProcess)
        {
            // Instantiate a new memory editor with the new target process
            MemoryEditor = new MemorySharp(TargetProcess);

            GUIProcessSelector.Hide();

            // Update components with new process

            // Switch selection to benediction
            //MakeSelection(ViewBenedictionButton, GUIBenediction);

            // Update process text
            TargetProcessLabel.Text = TargetProcess.ProcessName;
        }

        private void MakeSelection(ToolStripButton Sender, UserControl NewView)
        {
            // Clear GUI elements in main panel
            //foreach (Control NextControl in ComponentPanel.Controls)
            //    ComponentPanel.Controls.Remove(NextControl);

            // Uncheck all GUI options
            //SelectProcessButton.Checked = false;
            //ViewAnathemaButton.Checked = false;
            //ViewBenedictionButton.Checked = false;
            //ViewCelestialButton.Checked = false;

            // Check selection
            if (Sender != null)
                Sender.Checked = true;

            // Add the new view
            //if (NewView != null)
            //    ComponentPanel.Controls.Add(NewView);
        }


        private void SelectProcessButton_Click(object sender, EventArgs e)
        {
            //MakeSelection((ToolStripButton)sender, GUIProcessSelector);
        }

        #region Methods

        private void CreateDefaultTools()
        {
            CreateInputCorrelator();
            CreateDebugger();
            CreateTreeScanner();
            CreateSnapshotManager();
            CreateResults();
            CreateTable();
        }

        private void CreateDebugger()
        {
            if (GUIDebugger == null || GUIDebugger.IsDisposed)
                GUIDebugger = new GUIDebugger();
            GUIDebugger.Show(ContentPanel);
        }

        private void CreateStateScanner()
        {
            if (GUIFilterFSM == null || GUIFilterFSM.IsDisposed)
                GUIFilterFSM = new GUIFilterFSM();
            GUIFilterFSM.Show(ContentPanel);
        }

        private void CreateManualScanner()
        {
            if (GUIFilterManual == null || GUIFilterManual.IsDisposed)
                GUIFilterManual = new GUIFilterManual();
            GUIFilterManual.Show(ContentPanel);
        }

        private void CreateTreeScanner()
        {
            if (GUIFilterTree == null || GUIFilterTree.IsDisposed)
                GUIFilterTree = new GUIFilterTree();
            GUIFilterTree.Show(ContentPanel, DockState.DockLeft);
        }

        private void CreateInputCorrelator()
        {
            if (GUILabelerInputCorrelator == null || GUILabelerInputCorrelator.IsDisposed)
                GUILabelerInputCorrelator = new GUILabelerInputCorrelator();
            GUILabelerInputCorrelator.Show(ContentPanel);
        }

        private void CreateChangeCounter()
        {
            if (GUILabelerChangeCounter == null || GUILabelerChangeCounter.IsDisposed)
                GUILabelerChangeCounter = new GUILabelerChangeCounter();
            GUILabelerChangeCounter.Show(ContentPanel);
        }

        private void CreateSnapshotManager()
        {
            if (GUISnapshotManager == null || GUISnapshotManager.IsDisposed)
                GUISnapshotManager = new GUISnapshotManager();
            GUISnapshotManager.Show(ContentPanel, DockState.DockRight);
        }

        private void CreateResults()
        {
            if (GUIResults == null || GUIResults.IsDisposed)
                GUIResults = new GUIResults();
            GUIResults.Show(ContentPanel, DockState.DockRight);
        }

        private void CreateTable()
        {
            if (GUITable == null || GUITable.IsDisposed)
                GUITable = new GUITable();
            GUITable.Show(ContentPanel, DockState.DockBottom);
        }

        private void CreateProcessSelector()
        {
            if (GUIProcessSelector == null || GUIProcessSelector.IsDisposed)
                GUIProcessSelector = new GUIProcessSelector();
            
            GUIProcessSelector.Show(ContentPanel);
        }

        #endregion

        #region Events
        private void DebuggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDebugger();
        }

        private void StateScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateStateScanner();
        }

        private void ManualScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateManualScanner();
        }

        private void TreeScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTreeScanner();
        }
        
        private void InputCorrelatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateInputCorrelator();
        }

        private void ChangeCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateChangeCounter();
        }

        private void SnapshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateSnapshotManager();
        }

        private void ResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateResults();
        }

        private void TableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTable();
        }

        private void ProcessSelectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProcessSelector();
        }


        private void ProcessSelectorButton_Click(object sender, EventArgs e)
        {
            CreateProcessSelector();
        }

        #endregion

        
    }
}
