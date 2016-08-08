using Anathena.Source.Scanners.TreeScanner;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools.Scanners
{
    public partial class GUITreeScanner : DockContent, ITreeScannerView
    {
        private TreeScannerPresenter TreeScannerPresenter;
        private Object AccessLock;

        public GUITreeScanner()
        {
            InitializeComponent();

            TreeScannerPresenter = new TreeScannerPresenter(this, new TreeScanner());
            AccessLock = new Object();

            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
                {
                    ScanCountLabel.Text = "Scan Count: " + ScanCount.ToString();
                });
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(AccessLock))
            {
                StartScanButton.Enabled = false;
                StopScanButton.Enabled = true;
            }
        }

        private void EnableGUI()
        {
            using (TimedLock.Lock(AccessLock))
            {
                StartScanButton.Enabled = true;
                StopScanButton.Enabled = false;
            }
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