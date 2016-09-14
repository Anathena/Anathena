using Anathena.Source.Scanners.FiniteStateScanner;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools.Scanners
{
    public partial class GUIFiniteStateScanner : DockContent, IFiniteStateScannerView
    {
        private FiniteStateScannerPresenter finiteStateScannerPresenter;
        private Object accessLock;

        private FiniteStateMachine finiteStateMachine;

        public GUIFiniteStateScanner()
        {
            InitializeComponent();

            finiteStateScannerPresenter = new FiniteStateScannerPresenter(this, new FiniteStateScanner());
            accessLock = new Object();

            EnableGUI();
        }

        public void DisplayScanCount(Int32 scanCount)
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
                {
                    ScanCountLabel.Text = "Scan Count: " + scanCount.ToString();
                });
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(accessLock))
            {
                StartScanButton.Enabled = false;
                StopScanButton.Enabled = true;
            }
        }

        private void EnableGUI()
        {
            using (TimedLock.Lock(accessLock))
            {
                StartScanButton.Enabled = true;
                StopScanButton.Enabled = false;
            }
        }

        #region Events

        private void StartScanButton_Click(Object sender, EventArgs e)
        {
            finiteStateScannerPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(Object sender, EventArgs e)
        {
            finiteStateScannerPresenter.EndScan();
            EnableGUI();
        }

        #endregion

    } // End class

} // End namespace