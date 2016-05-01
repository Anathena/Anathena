using Anathema.Scanners.TreeScanner;
using Anathema.Utils.MVP;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUITreeScanner : DockContent, ITreeScannerView
    {
        private TreeScannerPresenter TreeScannerPresenter;

        public GUITreeScanner()
        {
            InitializeComponent();

            TreeScannerPresenter = new TreeScannerPresenter(this, new TreeScanner());
            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount)
        {
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ScanCountLabel.Text = "Scan Count: " + ScanCount.ToString();
            });
        }

        private void DisableGUI()
        {
            StartScanButton.Enabled = false;
            StopScanButton.Enabled = true;
        }

        private void EnableGUI()
        {
            StartScanButton.Enabled = true;
            StopScanButton.Enabled = false;
        }

        #region Events

        private void StartScanButton_Click(object sender, EventArgs e)
        {
            TreeScannerPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(object sender, EventArgs e)
        {
            TreeScannerPresenter.EndScan();
            EnableGUI();
        }

        #endregion

    } // End calss

} // End namespace