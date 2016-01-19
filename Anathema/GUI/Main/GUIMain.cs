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
        // VIEW MENU ITEMS
        private GUIProcessSelector GUIProcessSelector;
        private GUIDebugger GUIDebugger;
        private GUIScriptEditor GUIScriptEditor;
        private GUIFiniteStateScanner GUIFiniteStateScanner;
        private GUIManualScanner GUIManualScanner;
        private GUITreeScanner GUITreeScanner;
        private GUIChunkScanner GUIChunkScanner;
        private GUIChangeCounter GUIChangeCounter;
        private GUIInputCorrelator GUIInputCorrelator;
        private GUISnapshotManager GUISnapshotManager;
        private GUIResults GUIResults;
        private GUITable GUITable;

        // EDIT MENU ITEMS
        private GUISettings GUISettings;

        public GUIMain()
        {
            InitializeComponent();

            MainPresenter MainPresenter = new MainPresenter(this, Main.GetInstance());

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
            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                ProcessTitleLabel.Text = ProcessTitle;
            });
        }

        public void OpenScriptEditor()
        {
            CreateScriptEditor();
        }

        public void OpenLabelThresholder()
        {
           CreateLabelThresholder();
        }

        #region Private Methods

        private void CreateDefaultTools()
        {
            CreateChunkScanner();
            CreateFiniteStateScanner();
            CreateInputCorrelator();
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

        private void CreateFiniteStateScanner()
        {
            if (GUIFiniteStateScanner == null || GUIFiniteStateScanner.IsDisposed)
                GUIFiniteStateScanner = new GUIFiniteStateScanner();
            GUIFiniteStateScanner.Show(ContentPanel);
        }

        private void CreateManualScanner()
        {
            if (GUIManualScanner == null || GUIManualScanner.IsDisposed)
                GUIManualScanner = new GUIManualScanner();
            GUIManualScanner.Show(ContentPanel);
        }

        private void CreateTreeScanner()
        {
            if (GUITreeScanner == null || GUITreeScanner.IsDisposed)
                GUITreeScanner = new GUITreeScanner();
            GUITreeScanner.Show(ContentPanel);
        }

        private void CreateChunkScanner()
        {
            if (GUIChunkScanner == null || GUIChunkScanner.IsDisposed)
                GUIChunkScanner = new GUIChunkScanner();
            GUIChunkScanner.Show(ContentPanel);
        }

        private void CreateInputCorrelator()
        {
            if (GUIInputCorrelator == null || GUIInputCorrelator.IsDisposed)
                GUIInputCorrelator = new GUIInputCorrelator();
            GUIInputCorrelator.Show(ContentPanel);
        }

        private void CreateChangeCounter()
        {
            if (GUIChangeCounter == null || GUIChangeCounter.IsDisposed)
                GUIChangeCounter = new GUIChangeCounter();
            GUIChangeCounter.Show(ContentPanel);
        }

        private void CreateLabelThresholder()
        {

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

        private void CreateScriptEditor()
        {
            if (GUIScriptEditor == null || GUIScriptEditor.IsDisposed)
                GUIScriptEditor = new GUIScriptEditor();
            GUIScriptEditor.Show(ContentPanel);
        }

        private void CreateSettings()
        {
            if (GUISettings == null || GUISettings.IsDisposed)
                GUISettings = new GUISettings();
            GUISettings.ShowDialog();
        }

        #endregion

        #region Events

        private void DebuggerToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateDebugger();
        }

        private void StateScannerToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateFiniteStateScanner();
        }

        private void ManualScannerToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateManualScanner();
        }

        private void TreeScannerToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateTreeScanner();
        }

        private void ChunkScannerToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateChunkScanner();
        }

        private void InputCorrelatorToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateInputCorrelator();
        }

        private void ChangeCounterToolStripMenuItem_Click(Object Sender, EventArgs E)
        {

            CreateChangeCounter();
        }

        private void SnapshotsToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateSnapshotManager();
        }

        private void ResultsToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateResults();
        }

        private void TableToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateTable();
        }

        private void ProcessSelectorToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateProcessSelector();
        }

        private void ProcessSelectorButton_Click(Object Sender, EventArgs E)
        {
            CreateProcessSelector();
        }

        private void ScriptEditorToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateScriptEditor();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateSettings();
        }

        private void NewScanButton_Click(Object Sender, EventArgs E)
        {
            GUISnapshotManager.CreateNewSnapshot();
        }

        private void UndoScanButton_Click(Object Sender, EventArgs E)
        {
            GUISnapshotManager.UndoSnapshot();
        }

        #endregion

    }
}
