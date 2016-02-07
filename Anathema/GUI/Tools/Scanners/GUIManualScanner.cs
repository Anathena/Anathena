using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using System.Linq;

namespace Anathema
{
    public partial class GUIManualScanner : DockContent, IManualScannerView
    {
        private ManualScannerPresenter ManualScannerPresenter;

        public GUIManualScanner()
        {
            InitializeComponent();

            ManualScannerPresenter = new ManualScannerPresenter(this, new ManualScanner());
            
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
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ScanToolStrip.Items[ScanToolStrip.Items.IndexOf(StartScanButton)].Enabled = true;
            });
        }

        private void DisableGUI()
        {
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ScanToolStrip.Items[ScanToolStrip.Items.IndexOf(StartScanButton)].Enabled = false;
            });
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