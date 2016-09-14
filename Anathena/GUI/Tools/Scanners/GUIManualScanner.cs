using Anathena.Source.Scanners.ManualScanner;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools.Scanners
{
    public partial class GUIManualScanner : DockContent, IManualScannerView
    {
        private ManualScannerPresenter manualScannerPresenter;
        private Object accessLock;

        public GUIManualScanner()
        {
            InitializeComponent();

            manualScannerPresenter = new ManualScannerPresenter(this, new ManualScanner());
            accessLock = new Object();

            ToolStripManager.Merge(GUIConstraintEditor.AcquireToolStrip(), ScanToolStrip);
            EnableGUI();
        }

        public void DisplayScanCount(Int32 scanCount) { /* Manual scan will always have 1 scan so we need not implement this */ }

        public void ScanFinished()
        {
            EnableGUI();
        }

        private void EnableGUI()
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
                {
                    ScanToolStrip.Items[ScanToolStrip.Items.IndexOf(StartScanButton)].Enabled = true;
                });
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
                {
                    ScanToolStrip.Items[ScanToolStrip.Items.IndexOf(StartScanButton)].Enabled = false;
                });
            }
        }

        #region Events

        private void StartScanButton_Click(Object sender, EventArgs e)
        {
            DisableGUI();
            manualScannerPresenter.SetScanConstraintManager(GUIConstraintEditor.GetScanConstraintManager());
            manualScannerPresenter.BeginScan();
        }

        #endregion

    } // End class

} // End namespace