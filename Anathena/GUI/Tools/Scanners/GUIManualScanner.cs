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
        private ManualScannerPresenter ManualScannerPresenter;
        private Object AccessLock;

        public GUIManualScanner()
        {
            InitializeComponent();

            ManualScannerPresenter = new ManualScannerPresenter(this, new ManualScanner());
            AccessLock = new Object();

            ToolStripManager.Merge(GUIConstraintEditor.AcquireToolStrip(), ScanToolStrip);
            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount) { /* Manual scan will always have 1 scan so we need not implement this */ }

        public void ScanFinished()
        {
            EnableGUI();
        }

        private void EnableGUI()
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
                {
                    ScanToolStrip.Items[ScanToolStrip.Items.IndexOf(StartScanButton)].Enabled = true;
                });
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
                {
                    ScanToolStrip.Items[ScanToolStrip.Items.IndexOf(StartScanButton)].Enabled = false;
                });
            }
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            DisableGUI();
            ManualScannerPresenter.SetScanConstraintManager(GUIConstraintEditor.GetScanConstraintManager());
            ManualScannerPresenter.BeginScan();
        }

        #endregion

    } // End class

} // End namespace