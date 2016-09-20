using Ana.GUI.Tools;
using Ana.GUI.Tools.Scanners;
using Ana.Source.Controller;
using Ana.Source.Utils;
using Ana.Source.Utils.Extensions;
using Ana.Source.Utils.MVP;
using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Ana.GUI
{
    partial class GUIMain : Form, IMainView
    {
        private static readonly String configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AnathenaLayout.config");

        private DeserializeDockContent cockContentDeserializer;

        private MainPresenter mainPresenter;

        // VIEW MENU ITEMS
        private GUICheatBrowser guiCheatBrowser;
        private GUIProcessSelector guiProcessSelector;
        private GUICodeView guiCodeView;
        private GUIMemoryView guiMemoryView;

        private GUIFiniteStateScanner guiFiniteStateScanner;
        private GUIManualScanner guiManualScanner;
        private GUIChangeCounter guiChangeCounter;
        private GUILabelThresholder guiLabelThresholder;
        private GUIInputCorrelator guiInputCorrelator;
        private GUIPointerScanner guiPointerScanner;

        private GUISnapshotManager guiSnapshotManager;
        private GUIResults guiResults;
        private GUIPropertyViewer guiPropertyViewer;
        private GUIDotNetExplorer guiDotNetExplorer;
        private GUIProjectExplorer guiProjectExplorer;

        // EDIT MENU ITEMS
        private GUISettings guiSettings;

        // Variables
        private String title;
        private Object accessLock;

        public GUIMain()
        {
            InitializeComponent();

            mainPresenter = new MainPresenter(this, Main.GetInstance());
            accessLock = new Object();

            InitializeTheme();
            InitializeControls();
            InitializeStatus();

            CreateTools();

            this.Show();
        }

        #region Public Methods

        /// <summary>
        /// Update the target process 
        /// </summary>
        /// <param name="processTitle"></param>
        public void UpdateProcessTitle(String processTitle)
        {
            // Update process text
            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                this.ProcessTitleLabel.Text = processTitle;
            });
        }

        public void UpdateProgress(ProgressItem progressItem)
        {
            ControlThreadingHelper.InvokeControlAction(GUIStatusStrip, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (progressItem == null)
                    {
                        this.ActionProgressBar.ProgressBar.Value = 0;
                        this.ActionLabel.Text = String.Empty;
                        return;
                    }

                    this.ActionProgressBar.ProgressBar.Value = progressItem.GetProgress();
                    this.ActionLabel.Text = progressItem.GetProgressLabel();
                }
            });
        }

        public void OpenLabelThresholder()
        {
            CreateLabelThresholder();
        }

        #endregion

        #region Private Methods

        private void InitializeControls()
        {
            PrimitiveTypes.GetScannablePrimitiveTypes().ForEach(x => ValueTypeComboBox.Items.Add(x.Name));
            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        private void InitializeTheme()
        {
            String version;

            if (!Debugger.IsAttached && ApplicationDeployment.IsNetworkDeployed)
                version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            else
                version = ProductVersion;

            this.Text += " " + version + " " + "Beta";
            title = this.Text;

            // Update theme so that everything looks cool
            this.ContentPanel.Theme = new VS2013BlueTheme();

            // Set default dock space sizes
            ContentPanel.DockRightPortion = 288;
            ContentPanel.DockBottomPortion = 0.5;
        }

        private void InitializeStatus()
        {
            // Initialize progress
            UpdateProgress(null);
        }

        private void SaveConfiguration()
        {
            return; // DISABLED FOR NOW
            // ContentPanel.SaveAsXml(ConfigFile);
        }

        private void CreateTools()
        {
            if (File.Exists(configFile))
            {
                try
                {
                    // DISABLED FOR NOW
                    if (false)
                    {
                        // ContentPanel.LoadFromXml(ConfigFile, DockContentDeserializer);
                        // return;
                    }
                }
                catch { }
            }

            CreateDefaultTools();
        }

        private void CreateDefaultTools()
        {
            CreateManualScanner();
            CreateInputCorrelator();
            CreateSnapshotManager();
            CreateResults();
            CreateProjectExplorer();
            CreatePropertyViewer();

            // Force focus preferred windows with shared GUI tabs
            guiResults.Show();
            guiProjectExplorer.Show();
        }

        private void CreateCheatBrowser()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiCheatBrowser == null || guiCheatBrowser.IsDisposed)
                        guiCheatBrowser = new GUICheatBrowser();

                    guiCheatBrowser.Show(ContentPanel, new Rectangle(PointToScreen(ContentPanel.Location), guiCheatBrowser.Size));
                }
            });
        }

        private void CreateCodeView()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiCodeView == null || guiCodeView.IsDisposed)
                        guiCodeView = new GUICodeView();

                    guiCodeView.Show(ContentPanel);
                }
            });
        }

        private void CreateMemoryView()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiMemoryView == null || guiMemoryView.IsDisposed)
                        guiMemoryView = new GUIMemoryView();

                    guiMemoryView.Show(ContentPanel);
                }
            });
        }

        private void CreateFiniteStateScanner()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiFiniteStateScanner == null || guiFiniteStateScanner.IsDisposed)
                        guiFiniteStateScanner = new GUIFiniteStateScanner();

                    guiFiniteStateScanner.Show(ContentPanel);
                }
            });
        }

        private void CreateManualScanner()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiManualScanner == null || guiManualScanner.IsDisposed)
                        guiManualScanner = new GUIManualScanner();

                    guiManualScanner.Show(ContentPanel);
                }
            });
        }

        private void CreateInputCorrelator()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiInputCorrelator == null || guiInputCorrelator.IsDisposed)
                        guiInputCorrelator = new GUIInputCorrelator();

                    guiInputCorrelator.Show(ContentPanel);
                }
            });
        }

        private void CreateChangeCounter()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiChangeCounter == null || guiChangeCounter.IsDisposed)
                        guiChangeCounter = new GUIChangeCounter();

                    guiChangeCounter.Show(ContentPanel);
                }
            });
        }

        private void CreateLabelThresholder()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiLabelThresholder == null || guiLabelThresholder.IsDisposed)
                        guiLabelThresholder = new GUILabelThresholder();

                    guiLabelThresholder.Show(ContentPanel);
                }
            });
        }

        private void CreatePointerScanner()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiPointerScanner == null || guiPointerScanner.IsDisposed)
                        guiPointerScanner = new GUIPointerScanner();

                    guiPointerScanner.Show(ContentPanel);
                }
            });
        }

        private void CreateSnapshotManager()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiSnapshotManager == null || guiSnapshotManager.IsDisposed)
                        guiSnapshotManager = new GUISnapshotManager();

                    guiSnapshotManager.Show(ContentPanel, DockState.DockRight);
                }
            });
        }

        private void CreatePropertyViewer()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiPropertyViewer == null || guiPropertyViewer.IsDisposed)
                        guiPropertyViewer = new GUIPropertyViewer();

                    if (guiResults != null)
                        guiPropertyViewer.Show(guiResults.Pane, DockAlignment.Bottom, 0.5);
                    else
                        guiPropertyViewer.Show(ContentPanel, DockState.DockBottom);
                }
            });
        }

        private void CreateResults()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiResults == null || guiResults.IsDisposed)
                        guiResults = new GUIResults();

                    guiResults.Show(ContentPanel, DockState.DockRight);
                }
            });
        }

        private void CreateDotNetExplorer()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiDotNetExplorer == null || guiDotNetExplorer.IsDisposed)
                        guiDotNetExplorer = new GUIDotNetExplorer();

                    guiDotNetExplorer.Show(ContentPanel);
                }
            });
        }

        private void CreateProjectExplorer()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiProjectExplorer == null || guiProjectExplorer.IsDisposed)
                        guiProjectExplorer = new GUIProjectExplorer();

                    if (guiManualScanner != null)
                        guiProjectExplorer.Show(guiManualScanner.Pane, DockAlignment.Bottom, 0.5);
                    else
                        guiProjectExplorer.Show(ContentPanel, DockState.DockRight);
                }
            });
        }

        private void CreateProcessSelector()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiProcessSelector == null || guiProcessSelector.IsDisposed)
                        guiProcessSelector = new GUIProcessSelector();

                    guiProcessSelector.Show(ContentPanel);
                }
            });
        }

        private void CreateSettings()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (guiSettings == null || guiSettings.IsDisposed)
                        guiSettings = new GUISettings();

                    guiSettings.ShowDialog(this);
                }
            });
        }

        public void BeginSaveProject()
        {
            if (mainPresenter.GetProjectFilePath() == null || mainPresenter.GetProjectFilePath() == String.Empty)
            {
                BeginSaveAsProject();
                return;
            }

            mainPresenter.RequestSaveProject(mainPresenter.GetProjectFilePath());
        }

        public void BeginSaveAsProject()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Cheat File (*.Hax)|*.hax|All files (*.*)|*.*";
            saveFileDialog.Title = "Save Cheat File";
            saveFileDialog.ShowDialog();

            mainPresenter.SetProjectFilePath(saveFileDialog.FileName);

            mainPresenter.RequestSaveProject(saveFileDialog.FileName);
        }

        public void BeginOpenProject()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Cheat File (*.Hax)|*.hax|All files (*.*)|*.*";
            openFileDialog.Title = "Open Cheat File";
            openFileDialog.ShowDialog();

            mainPresenter.SetProjectFilePath(openFileDialog.FileName);

            mainPresenter.RequestOpenProject(openFileDialog.FileName);
        }

        public void BeginImportProject()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Cheat File (*.Hax)|*.hax|All files (*.*)|*.*";
            openFileDialog.Title = "Open and Merge Cheat File";
            openFileDialog.ShowDialog();

            // Prioritize whatever is open already. If nothing, use the merge filename.
            if (mainPresenter.GetProjectFilePath() == String.Empty)
                mainPresenter.SetProjectFilePath(openFileDialog.FileName);

            mainPresenter.RequestImportProject(openFileDialog.FileName);
        }

        private Boolean AskSaveChanges()
        {
            // Check if there are even changes to save
            if (!mainPresenter.RequestHasChanges())
                return false;

            DialogResult result = MessageBoxEx.Show(this, "This table has not been saved. Save the changes before closing?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            switch (result)
            {
                case DialogResult.Yes:
                    BeginSaveProject();
                    return false;
                case DialogResult.No:
                    return false;
                case DialogResult.Cancel:
                    break;
            }

            // User wishes to cancel
            return true;
        }

        public void UpdateHasChanges(Boolean hasChanges)
        {
            ControlThreadingHelper.InvokeControlAction(this, () =>
            {
                this.Text = title;

                if (hasChanges)
                    this.Text += "*";

                this.Text += " - " + mainPresenter.GetProjectFilePath();
            });
        }

        #endregion

        #region Events

        private void CheatBrowserToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateCheatBrowser();
        }

        private void CodeViewToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateCodeView();
        }

        private void MemoryViewToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateMemoryView();
        }

        private void FiniteStateScannerToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateFiniteStateScanner();
        }

        private void ManualScannerToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateManualScanner();
        }

        private void InputCorrelatorToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateInputCorrelator();
        }

        private void ChangeCounterToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateChangeCounter();
        }

        private void LabelThresholderToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateLabelThresholder();
        }

        private void PointerScannerToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreatePointerScanner();
        }

        private void SnapshotManagerToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateSnapshotManager();
        }

        private void PropertiesToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreatePropertyViewer();
        }

        private void ResultsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateResults();
        }

        private void DotNetExplorerToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateDotNetExplorer();
        }

        private void ProjectExplorerToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateProjectExplorer();
        }

        private void ProcessSelectorToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateProcessSelector();
        }

        private void ProcessSelectorButton_Click(Object sender, EventArgs e)
        {
            CreateProcessSelector();
        }

        private void SettingsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            CreateSettings();
        }

        private void OpenToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            BeginOpenProject();
        }

        private void SaveToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            BeginSaveProject();
        }

        private void OpenButton_Click(Object sender, EventArgs e)
        {
            BeginOpenProject();
        }

        private void SaveButton_Click(Object sender, EventArgs e)
        {
            BeginSaveProject();
        }

        private void ImportProjectToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            BeginImportProject();
        }

        private void SaveAsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            BeginSaveAsProject();
        }

        private void CollectValuesButton_Click(Object sender, EventArgs e)
        {
            mainPresenter.RequestCollectValues();
        }

        private void NewScanButton_Click(Object sender, EventArgs e)
        {
            mainPresenter.RequestNewScan();
        }

        private void UndoScanButton_Click(Object sender, EventArgs e)
        {
            mainPresenter.RequestUndoScan();
        }

        private void ExitToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            this.Close();
        }

        private void GUIMenuStrip_MenuActivate(Object sender, EventArgs e)
        {
            // Check / uncheck items if the windows are open
            using (TimedLock.Lock(accessLock))
            {
                CheatBrowserToolStripMenuItem.Checked = (guiCheatBrowser == null || guiCheatBrowser.IsDisposed) ? false : true;
                ProcessSelectorToolStripMenuItem.Checked = (guiProcessSelector == null || guiProcessSelector.IsDisposed) ? false : true;
                SnapshotManagerToolStripMenuItem.Checked = (guiSnapshotManager == null || guiSnapshotManager.IsDisposed) ? false : true;
                PropertiesToolStripMenuItem.Checked = (guiPropertyViewer == null || guiPropertyViewer.IsDisposed) ? false : true;
                ResultsToolStripMenuItem.Checked = (guiResults == null || guiResults.IsDisposed) ? false : true;
                DotNetExplorerToolStripMenuItem.Checked = (guiDotNetExplorer == null || guiDotNetExplorer.IsDisposed) ? false : true;
                ProjectExplorerToolStripMenuItem.Checked = (guiProjectExplorer == null || guiProjectExplorer.IsDisposed) ? false : true;

                CodeViewToolStripMenuItem.Checked = (guiCodeView == null || guiCodeView.IsDisposed) ? false : true;
                MemoryViewToolStripMenuItem.Checked = (guiMemoryView == null || guiMemoryView.IsDisposed) ? false : true;

                FiniteStateScannerToolStripMenuItem.Checked = (guiFiniteStateScanner == null || guiFiniteStateScanner.IsDisposed) ? false : true;
                ManualScannerToolStripMenuItem.Checked = (guiManualScanner == null || guiManualScanner.IsDisposed) ? false : true;
                ChangeCounterToolStripMenuItem.Checked = (guiChangeCounter == null || guiChangeCounter.IsDisposed) ? false : true;
                LabelThresholderToolStripMenuItem.Checked = (guiLabelThresholder == null || guiLabelThresholder.IsDisposed) ? false : true;
                InputCorrelatorToolStripMenuItem.Checked = (guiInputCorrelator == null || guiInputCorrelator.IsDisposed) ? false : true;
                PointerScannerToolStripMenuItem.Checked = (guiPointerScanner == null || guiPointerScanner.IsDisposed) ? false : true;
            }
        }

        private void GUIMain_FormClosing(Object sender, FormClosingEventArgs e)
        {
            // Give the table a chance to ask to save changes
            using (TimedLock.Lock(accessLock))
            {
                if (guiProjectExplorer != null && !guiProjectExplorer.IsDisposed)
                    guiProjectExplorer.Close();

                try
                {
                    if (guiProjectExplorer != null && !guiProjectExplorer.IsDisposed)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch { }
            }

            SaveConfiguration();

            Application.Exit();
        }

        #endregion

    } // End namespace

} // End class