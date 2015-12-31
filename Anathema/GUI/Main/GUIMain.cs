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
    partial class GUIMain : Form, IMainView
    {
        // All GUI components that can be created
        private GUIProcessSelector GUIProcessSelector;
        private GUIDebugger GUIDebugger;
        private GUIFilterFSM GUIFilterFSM;
        private GUIFilterManual GUIFilterManual;
        private GUIFilterTree GUIFilterTree;
        private GUIFilterChunks GUIFilterChunks;
        private GUILabelerChangeCounter GUILabelerChangeCounter;
        private GUILabelerInputCorrelator GUILabelerInputCorrelator;
        private GUISnapshotManager GUISnapshotManager;
        private GUIResults GUIResults;
        private GUITable GUITable;

        public GUIMain()
        {
            InitializeComponent();

            MainPresenter MainPresenter = new MainPresenter(this, new Main());

            // Update theme so that everything looks cool
            this.ContentPanel.Theme = new VS2013BlueTheme();

            // Initialize tools that are commonly used
            CreateDefaultTools();
        }

        /// <summary>
        /// Update the target process 
        /// </summary>
        /// <param name="ProcessTitle"></param>
        public void UpdateProcessTitle(String ProcessTitle)
        {
            // Update process text
            ProcessTitleLabel.Text = ProcessTitle;
        }

        #region Methods

        private void CreateDefaultTools()
        {
            CreateInputCorrelator();
            CreateDebugger();
            CreateChunkScanner();
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

        private void CreateChunkScanner()
        {
          if (GUIFilterChunks == null || GUIFilterChunks.IsDisposed)
                GUIFilterChunks = new GUIFilterChunks();
            GUIFilterChunks.Show(ContentPanel, DockState.DockLeft);
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

        private void ChunkScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateChunkScanner();
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
