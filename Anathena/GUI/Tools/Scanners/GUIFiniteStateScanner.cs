using Anathena.Source.Scanners.FiniteStateScanner;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools.Scanners
{
    public partial class GUIFiniteStateScanner : DockContent, IFiniteStateScannerView
    {
        private FiniteStateScannerPresenter FiniteStateScannerPresenter;
        private Object AccessLock;

        private FiniteStateMachine FiniteStateMachine;

        public GUIFiniteStateScanner()
        {
            InitializeComponent();

            FiniteStateScannerPresenter = new FiniteStateScannerPresenter(this, new FiniteStateScanner());
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

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            FiniteStateScannerPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            FiniteStateScannerPresenter.EndScan();
            EnableGUI();
        }

        #endregion

    } // End class

} // End namespace