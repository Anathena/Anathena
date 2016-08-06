using Anathema.GUI.Tools;
using Anathema.GUI.Tools.Scanners;
using Anathema.GUI.Tools.TypeEditors;
using Anathema.Source.Controller;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.MVP;
using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        private GUIPropertyViewer GUIPropertyViewer;
        private GUIDotNetExplorer GUIDotNetExplorer;
        private GUIProjectExplorer GUIProjectExplorer;

        // EDIT MENU ITEMS
        private GUISettings GUISettings;

        // Variables
        private String Title;
        private Object AccessLock;

        public GUIMain()
        {
            InitializeComponent();

            MainPresenter = new MainPresenter(this, Main.GetInstance());
            AccessLock = new Object();

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
                using (TimedLock.Lock(AccessLock))
                {
                    if (ProgressItem == null)
                    {
                        this.ActionProgressBar.ProgressBar.Value = 0;
                        this.ActionLabel.Text = String.Empty;
                        return;
                    }

                    this.ActionProgressBar.ProgressBar.Value = ProgressItem.GetProgress();
                    this.ActionLabel.Text = ProgressItem.GetProgressLabel();
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
            PrimitiveTypes.GetScannablePrimitiveTypes().ForEach(X => ValueTypeComboBox.Items.Add(X.Name));
            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        private void InitializeTheme()
        {
            String Version;

            if (!Debugger.IsAttached && ApplicationDeployment.IsNetworkDeployed)
                Version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            else
                Version = ProductVersion;

            this.Text += " " + Version + " " + "Beta";
            Title = this.Text;

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
            ContentPanel.SaveAsXml(ConfigFile);
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
            CreateManualScanner();
            CreateInputCorrelator();
            CreateSnapshotManager();
            CreateResults();
            CreateProjectExplorer();
            CreatePropertyViewer();

            // Force focus preferred windows with shared GUI tabs
            GUIResults.Show();
            GUIProjectExplorer.Show();
        }

        private void CreateCheatBrowser()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUICheatBrowser == null || GUICheatBrowser.IsDisposed)
                        GUICheatBrowser = new GUICheatBrowser();

                    GUICheatBrowser.Show(ContentPanel, new Rectangle(PointToScreen(ContentPanel.Location), GUICheatBrowser.Size));
                }
            });
        }

        private void CreateCodeView()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUICodeView == null || GUICodeView.IsDisposed)
                        GUICodeView = new GUICodeView();

                    GUICodeView.Show(ContentPanel);
                }
            });
        }

        private void CreateMemoryView()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIMemoryView == null || GUIMemoryView.IsDisposed)
                        GUIMemoryView = new GUIMemoryView();

                    GUIMemoryView.Show(ContentPanel);
                }
            });
        }

        private void CreateFiniteStateScanner()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIFiniteStateScanner == null || GUIFiniteStateScanner.IsDisposed)
                        GUIFiniteStateScanner = new GUIFiniteStateScanner();

                    GUIFiniteStateScanner.Show(ContentPanel);
                }
            });
        }

        private void CreateManualScanner()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIManualScanner == null || GUIManualScanner.IsDisposed)
                        GUIManualScanner = new GUIManualScanner();

                    GUIManualScanner.Show(ContentPanel);
                }
            });
        }

        private void CreateTreeScanner()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUITreeScanner == null || GUITreeScanner.IsDisposed)
                        GUITreeScanner = new GUITreeScanner();

                    GUITreeScanner.Show(ContentPanel);
                }
            });
        }

        private void CreateChunkScanner()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIChunkScanner == null || GUIChunkScanner.IsDisposed)
                        GUIChunkScanner = new GUIChunkScanner();

                    GUIChunkScanner.Show(ContentPanel);
                }
            });
        }

        private void CreateInputCorrelator()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIInputCorrelator == null || GUIInputCorrelator.IsDisposed)
                        GUIInputCorrelator = new GUIInputCorrelator();

                    GUIInputCorrelator.Show(ContentPanel);
                }
            });
        }

        private void CreateChangeCounter()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIChangeCounter == null || GUIChangeCounter.IsDisposed)
                        GUIChangeCounter = new GUIChangeCounter();

                    GUIChangeCounter.Show(ContentPanel);
                }
            });
        }

        private void CreateLabelThresholder()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUILabelThresholder == null || GUILabelThresholder.IsDisposed)
                        GUILabelThresholder = new GUILabelThresholder();

                    GUILabelThresholder.Show(ContentPanel);
                }
            });
        }

        private void CreatePointerScanner()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIPointerScanner == null || GUIPointerScanner.IsDisposed)
                        GUIPointerScanner = new GUIPointerScanner();

                    GUIPointerScanner.Show(ContentPanel);
                }
            });
        }

        private void CreateSnapshotManager()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUISnapshotManager == null || GUISnapshotManager.IsDisposed)
                        GUISnapshotManager = new GUISnapshotManager();

                    GUISnapshotManager.Show(ContentPanel, DockState.DockRight);
                }
            });
        }

        private void CreatePropertyViewer()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIPropertyViewer == null || GUIPropertyViewer.IsDisposed)
                        GUIPropertyViewer = new GUIPropertyViewer();

                    if (GUIResults != null)
                        GUIPropertyViewer.Show(GUIResults.Pane, DockAlignment.Bottom, 0.5);
                    else
                        GUIPropertyViewer.Show(ContentPanel, DockState.DockBottom);
                }
            });
        }

        private void CreateResults()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIResults == null || GUIResults.IsDisposed)
                        GUIResults = new GUIResults();

                    GUIResults.Show(ContentPanel, DockState.DockRight);
                }
            });
        }

        private void CreateDotNetExplorer()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIDotNetExplorer == null || GUIDotNetExplorer.IsDisposed)
                        GUIDotNetExplorer = new GUIDotNetExplorer();

                    GUIDotNetExplorer.Show(ContentPanel);
                }
            });
        }

        private void CreateProjectExplorer()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIProjectExplorer == null || GUIProjectExplorer.IsDisposed)
                        GUIProjectExplorer = new GUIProjectExplorer();

                    if (GUIManualScanner != null)
                        GUIProjectExplorer.Show(GUIManualScanner.Pane, DockAlignment.Bottom, 0.5);
                    else
                        GUIProjectExplorer.Show(ContentPanel, DockState.DockRight);
                }
            });
        }

        private void CreateProcessSelector()
        {
            ControlThreadingHelper.InvokeControlAction(ContentPanel, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUIProcessSelector == null || GUIProcessSelector.IsDisposed)
                        GUIProcessSelector = new GUIProcessSelector();

                    GUIProcessSelector.Show(ContentPanel);
                }
            });
        }

        private void CreateSettings()
        {
            ControlThreadingHelper.InvokeControlAction(GUISettings, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (GUISettings == null || GUISettings.IsDisposed)
                        GUISettings = new GUISettings();

                    GUISettings.ShowDialog(this);
                }
            });
        }

        public void BeginSaveProject()
        {
            if (MainPresenter.GetProjectFilePath() == null || MainPresenter.GetProjectFilePath() == String.Empty)
            {
                BeginSaveAsProject();
                return;
            }

            MainPresenter.RequestSaveProject(MainPresenter.GetProjectFilePath());
        }

        public void BeginSaveAsProject()
        {
            SaveFileDialog SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.Filter = "Cheat File (*.Hax)|*.hax|All files (*.*)|*.*";
            SaveFileDialog.Title = "Save Cheat File";
            SaveFileDialog.ShowDialog();

            MainPresenter.SetProjectFilePath(SaveFileDialog.FileName);

            MainPresenter.RequestSaveProject(SaveFileDialog.FileName);
        }

        public void BeginOpenProject()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "Cheat File (*.Hax)|*.hax|All files (*.*)|*.*";
            OpenFileDialog.Title = "Open Cheat File";
            OpenFileDialog.ShowDialog();

            MainPresenter.SetProjectFilePath(OpenFileDialog.FileName);

            MainPresenter.RequestOpenProject(OpenFileDialog.FileName);
        }

        public void BeginImportProject()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "Cheat File (*.Hax)|*.hax|All files (*.*)|*.*";
            OpenFileDialog.Title = "Open and Merge Cheat File";
            OpenFileDialog.ShowDialog();

            // Prioritize whatever is open already. If nothing, use the merge filename.
            if (MainPresenter.GetProjectFilePath() == String.Empty)
                MainPresenter.SetProjectFilePath(OpenFileDialog.FileName);

            MainPresenter.RequestImportProject(OpenFileDialog.FileName);
        }

        private Boolean AskSaveChanges()
        {
            // Check if there are even changes to save
            if (!MainPresenter.RequestHasChanges())
                return false;

            DialogResult Result = MessageBoxEx.Show(this, "This table has not been saved. Save the changes before closing?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            switch (Result)
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

        public void UpdateHasChanges(Boolean HasChanges)
        {
            ControlThreadingHelper.InvokeControlAction(this, () =>
            {
                this.Text = Title;

                if (HasChanges)
                    this.Text += "*";

                this.Text += " - " + MainPresenter.GetProjectFilePath();
            });
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

        private void PropertiesToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreatePropertyViewer();
        }

        private void ResultsToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateResults();
        }

        private void DotNetExplorerToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateDotNetExplorer();
        }

        private void ProjectExplorerToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateProjectExplorer();
        }

        private void ProcessSelectorToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateProcessSelector();
        }

        private void ProcessSelectorButton_Click(Object Sender, EventArgs E)
        {
            CreateProcessSelector();
        }

        private void SettingsToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            CreateSettings();
        }

        private void OpenToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            BeginOpenProject();
        }

        private void SaveToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            BeginSaveProject();
        }

        private void OpenButton_Click(Object Sender, EventArgs E)
        {
            BeginOpenProject();
        }

        private void SaveButton_Click(Object Sender, EventArgs E)
        {
            BeginSaveProject();
        }

        private void ImportProjectToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            BeginImportProject();
        }

        private void SaveAsToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            BeginSaveAsProject();
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

        private void ExitToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            this.Close();
        }

        private void GUIMenuStrip_MenuActivate(Object Sender, EventArgs E)
        {
            // Check / uncheck items if the windows are open
            using (TimedLock.Lock(AccessLock))
            {
                CheatBrowserToolStripMenuItem.Checked = (GUICheatBrowser == null || GUICheatBrowser.IsDisposed) ? false : true;
                ProcessSelectorToolStripMenuItem.Checked = (GUIProcessSelector == null || GUIProcessSelector.IsDisposed) ? false : true;
                SnapshotManagerToolStripMenuItem.Checked = (GUISnapshotManager == null || GUISnapshotManager.IsDisposed) ? false : true;
                PropertiesToolStripMenuItem.Checked = (GUIPropertyViewer == null || GUIPropertyViewer.IsDisposed) ? false : true;
                ResultsToolStripMenuItem.Checked = (GUIResults == null || GUIResults.IsDisposed) ? false : true;
                DotNetExplorerToolStripMenuItem.Checked = (GUIDotNetExplorer == null || GUIDotNetExplorer.IsDisposed) ? false : true;
                ProjectExplorerToolStripMenuItem.Checked = (GUIProjectExplorer == null || GUIProjectExplorer.IsDisposed) ? false : true;

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
        }

        private void GUIMain_FormClosing(Object Sender, FormClosingEventArgs E)
        {
            // Give the table a chance to ask to save changes
            using (TimedLock.Lock(AccessLock))
            {
                if (GUIProjectExplorer != null && !GUIProjectExplorer.IsDisposed)
                    GUIProjectExplorer.Close();

                try
                {
                    if (GUIProjectExplorer != null && !GUIProjectExplorer.IsDisposed)
                    {
                        E.Cancel = true;
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