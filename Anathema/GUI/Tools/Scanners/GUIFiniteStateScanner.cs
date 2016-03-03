using System;
using WeifenLuo.WinFormsUI.Docking;
using Anathema.Utils.MVP;
using Anathema.Scanners.FiniteStateScanner;

namespace Anathema
{
    public partial class GUIFiniteStateScanner : DockContent, IFiniteStateScannerView
    {
        private FiniteStateScannerPresenter FiniteStateScannerPresenter;
        
        private FiniteStateMachine FiniteStateMachine;
        
        public GUIFiniteStateScanner()
        {
            InitializeComponent();

            FiniteStateScannerPresenter = new FiniteStateScannerPresenter(this, new FiniteStateScanner());
            
            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount)
        {
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ScanCountLabel.Text = "Scan Count: " + ScanCount.ToString();
            });
        }

        public void ScanFinished()
        {
            EnableGUI();
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
         
        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            FiniteStateScannerPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(object sender, EventArgs e)
        {
            FiniteStateScannerPresenter.EndScan();
            EnableGUI();
        }

        #endregion

    } // End class

} // End namespace