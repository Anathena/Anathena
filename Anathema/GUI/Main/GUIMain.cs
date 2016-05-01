﻿using Anathema.Source.Utils;
using Anathema.User.Registration;
using Anathema.Utils;
using Anathema.Utils.MVP;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    partial class GUIMain : Form, IMainView
    {
        private static readonly String ConfigFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AnathemaLayout.config");

        private DeserializeDockContent DockContentDeserializer;

        private MainPresenter MainPresenter;

        // VIEW MENU ITEMS
        private GUICheatBrowser GUICheatBrowser;
        private GUIProcessSelector GUIProcessSelector;
        private GUICodeView GUICodeView;
        private GUIMemoryView GUIMemoryView;
        private GUIScriptEditor GUIScriptEditor;

        private GUIFiniteStateScanner GUIFiniteStateScanner;
        private GUIManualScanner GUIManualScanner;
        private GUITreeScanner GUITreeScanner;
        private GUIChunkScanner GUIChunkScanner;
        private GUIChangeCounter GUIChangeCounter;
        private GUILabelThresholder GUILabelThresholder;
        private GUIInputCorrelator GUIInputCorrelator;
        private GUIPointerScanner GUIPointerScanner;

        private GUISnapshotManager GUISnapshotManager;
        private GUIResults GUIResults;
        private GUITable GUITable;

        // EDIT MENU ITEMS
        private GUISettings GUISettings;

        // HELP ITEMS
        private GUIRegistration GUIRegistration;

        public GUIMain()
        {
            InitializeComponent();

            MainPresenter = new MainPresenter(this, Main.GetInstance());

            InitializeTheme();
            InitializeStatus();

            // CheckRegistration();
            CreateTools();
            CheckNewVersion();

            this.Show();
        }

        private void CheckNewVersion()
        {
            Task.Run(() =>
            {
                Assembly Assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo FileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.Location);
                String CurrentVersion = FileVersionInfo.ProductVersion;

                try
                {
                    String PublicVersion = (new VersionChecker()).DownloadString("http://www.anathemaengine.com/release/version.txt");

                    if (PublicVersion != CurrentVersion)
                        MessageBoxEx.Show(this, "New Version Available at http://www.anethemaengine.com/" + Environment.NewLine +
                            "Current Version: " + CurrentVersion + Environment.NewLine +
                            "New Version: " + PublicVersion + Environment.NewLine +
                            "Anathema is still in beta, so this update will likely provide critical performance and feature changes.",
                            "New Version Available");
                }
                catch { }
            });
        }

        public class VersionChecker : WebClient
        {
            protected override WebRequest GetWebRequest(Uri Address)
            {
                WebRequest WebRequest = base.GetWebRequest(Address);
                WebRequest.Timeout = 5000;
                return WebRequest;
            }
        }

        #region Public Methods

        /// <summary>
        /// Update the target process 
        /// </summary>
        /// <param name="ProcessTitle"></param>
        public void UpdateProcessTitle(String ProcessTitle)
        {
            // Update process text
            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                this.ProcessTitleLabel.Text = ProcessTitle;
            });
        }

        public void UpdateProgress(ProgressItem ProgressItem)
        {
            ControlThreadingHelper.InvokeControlAction(GUIStatusStrip, () =>
            {
                if (ProgressItem == null)
                {
                    this.ActionProgressBar.ProgressBar.Value = 0;
                    this.ActionLabel.Text = String.Empty;
                    return;
                }

                this.ActionProgressBar.ProgressBar.Value = ProgressItem.GetProgress();
                this.ActionLabel.Text = ProgressItem.GetProgressLabel();
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

        #endregion

        #region Private Methods

        private void InitializeTheme()
        {
            Assembly Assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo FileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.Location);
            String[] CurrentVersion = FileVersionInfo.ProductVersion.Split('.');

            this.Text += " " + CurrentVersion[0] + "." + CurrentVersion[1] + " " + "Beta";

            // Update theme so that everything looks cool
            this.ContentPanel.Theme = new VS2013BlueTheme();

            // Set default dock space sizes
            ContentPanel.DockRightPortion = 0.4;
            ContentPanel.DockBottomPortion = 0.4;
        }

        private void InitializeStatus()
        {
            // Initialize progress
            UpdateProgress(null);
        }

        private void SaveConfiguration()
        {
            return; // DISABLED FOR NOW
            ContentPanel.SaveAsXml(ConfigFile);
        }

        private void CheckRegistration()
        {
            if (RegistrationManager.GetInstance().IsRegistered())
                return;

            if (RegistrationManager.GetInstance().IsTrialMode())
            {
                TimeSpan RemainingTime = RegistrationManager.GetInstance().GetRemainingTime();

                // Append trial mode remaining time
                this.Text += " - Trial Mode";
                MessageBoxEx.Show(this, RemainingTime.ToString("%d") + " days, " + RemainingTime.ToString("%h") + " hours remaining!\nPlease buy this I am broke and live with my parents.", "Trial mode");

                return;
            }

            MessageBoxEx.Show(this, "Trial has expired! Please purchase Anathema to continue" + Environment.NewLine + Environment.NewLine +
                "Buy this if you enjoy it so I can move out of my parents' house");
            CreateRegistration();
            Application.Exit();
        }

        private void CreateTools()
        {
            if (File.Exists(ConfigFile))
            {
                try
                {
                    // DISABLED FOR NOW
                    if (false)
                    {
                        ContentPanel.LoadFromXml(ConfigFile, DockContentDeserializer);
                        return;
                    }
                }
                catch { }
            }

            CreateDefaultTools();
        }

        private void CreateDefaultTools()
        {
            // CreateChunkScanner();
            CreateManualScanner();
            CreateInputCorrelator();
            CreateSnapshotManager();
            CreateResults();
            CreateTable();
        }

        private void CreateCheatBrowser()
        {
            if (GUICheatBrowser == null || GUICheatBrowser.IsDisposed)
                GUICheatBrowser = new GUICheatBrowser();
            GUICheatBrowser.Show(ContentPanel);
        }

        private void CreateCodeView()
        {
            if (GUICodeView == null || GUICodeView.IsDisposed)
                GUICodeView = new GUICodeView();
            GUICodeView.Show(ContentPanel);
        }

        private void CreateMemoryView()
        {
            if (GUIMemoryView == null || GUIMemoryView.IsDisposed)
                GUIMemoryView = new GUIMemoryView();
            GUIMemoryView.Show(ContentPanel);
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
            if (GUILabelThresholder == null || GUILabelThresholder.IsDisposed)
                GUILabelThresholder = new GUILabelThresholder();
            GUILabelThresholder.Show(ContentPanel);
        }

        private void CreatePointerScanner()
        {
            if (GUIPointerScanner == null || GUIPointerScanner.IsDisposed)
                GUIPointerScanner = new GUIPointerScanner();
            GUIPointerScanner.Show(ContentPanel);
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

        private void CreateRegistration()
        {
            if (GUIRegistration == null || GUIRegistration.IsDisposed)
                GUIRegistration = new GUIRegistration();
            GUIRegistration.ShowDialog(this);
        }

        private void CreateSettings()
        {
            if (GUISettings == null || GUISettings.IsDisposed)
                GUISettings = new GUISettings();
            GUISettings.ShowDialog(this);
        }

        #endregion

        #region Events

        private void CheatBrowserToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateCheatBrowser();
        }

        private void CodeViewToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateCodeView();
        }

        private void MemoryViewToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateMemoryView();
        }

        private void FiniteStateScannerToolStripMenuItem_Click(Object Sender, EventArgs E)
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

        private void LabelThresholderToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateLabelThresholder();
        }

        private void PointerScannerToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreatePointerScanner();
        }

        private void SnapshotManagerToolStripMenuItem_Click(Object Sender, EventArgs E)
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

        private void RegisterToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateRegistration();
        }

        private void CollectValuesButton_Click(Object Sender, EventArgs E)
        {
            MainPresenter.RequestCollectValues();
        }

        private void NewScanButton_Click(Object Sender, EventArgs E)
        {
            MainPresenter.RequestNewScan();
        }

        private void UndoScanButton_Click(Object Sender, EventArgs E)
        {
            MainPresenter.RequestUndoScan();
        }

        private void OpenToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateTable();
            GUITable.BeginOpenTable();
        }

        private void SaveToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateTable();
            GUITable.BeginSaveTable();
        }

        private void ExitToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            this.Close();
        }

        private void GUIMenuStrip_MenuActivate(Object Sender, EventArgs E)
        {
            // Check / uncheck items if the windows are open
            CheatBrowserToolStripMenuItem.Checked = (GUICheatBrowser == null || GUICheatBrowser.IsDisposed) ? false : true;
            ProcessSelectorToolStripMenuItem.Checked = (GUIProcessSelector == null || GUIProcessSelector.IsDisposed) ? false : true;
            ScriptEditorToolStripMenuItem.Checked = (GUIScriptEditor == null || GUIScriptEditor.IsDisposed) ? false : true;
            SnapshotManagerToolStripMenuItem.Checked = (GUISnapshotManager == null || GUISnapshotManager.IsDisposed) ? false : true;
            ResultsToolStripMenuItem.Checked = (GUIResults == null || GUIResults.IsDisposed) ? false : true;
            TableToolStripMenuItem.Checked = (GUITable == null || GUITable.IsDisposed) ? false : true;

            CodeViewToolStripMenuItem.Checked = (GUICodeView == null || GUICodeView.IsDisposed) ? false : true;
            MemoryViewToolStripMenuItem.Checked = (GUIMemoryView == null || GUIMemoryView.IsDisposed) ? false : true;

            FiniteStateScannerToolStripMenuItem.Checked = (GUIFiniteStateScanner == null || GUIFiniteStateScanner.IsDisposed) ? false : true;
            ManualScannerToolStripMenuItem.Checked = (GUIManualScanner == null || GUIManualScanner.IsDisposed) ? false : true;
            TreeScannerToolStripMenuItem.Checked = (GUITreeScanner == null || GUITreeScanner.IsDisposed) ? false : true;
            ChunkScannerToolStripMenuItem.Checked = (GUIChunkScanner == null || GUIChunkScanner.IsDisposed) ? false : true;
            ChangeCounterToolStripMenuItem.Checked = (GUIChangeCounter == null || GUIChangeCounter.IsDisposed) ? false : true;
            LabelThresholderToolStripMenuItem.Checked = (GUILabelThresholder == null || GUILabelThresholder.IsDisposed) ? false : true;
            InputCorrelatorToolStripMenuItem.Checked = (GUIInputCorrelator == null || GUIInputCorrelator.IsDisposed) ? false : true;
            PointerScannerToolStripMenuItem.Checked = (GUIPointerScanner == null || GUIPointerScanner.IsDisposed) ? false : true;

        }

        private void GUIMain_FormClosing(Object Sender, FormClosingEventArgs E)
        {
            // Give the table a chance to ask to save changes
            if (GUITable != null && !GUITable.IsDisposed)
                GUITable.Close();

            try
            {
                if (GUITable != null && !GUITable.IsDisposed)
                {
                    E.Cancel = true;
                    return;
                }
            }
            catch { }

            SaveConfiguration();

            Application.Exit();
        }

        #endregion

    } // End namespace

} // End class