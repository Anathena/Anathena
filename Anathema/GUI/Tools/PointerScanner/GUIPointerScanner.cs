using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    public partial class GUIPointerScanner : DockContent, IPointerScannerView
    {
        private PointerScannerPresenter PointerScannerPresenter;

        public GUIPointerScanner()
        {
            InitializeComponent();

            PointerScannerPresenter = new PointerScannerPresenter(this, new PointerScanner());
            EnableGUI();
        }

        public void EventFilterFinished(List<RemoteRegion> MemoryRegions)
        {

        }

        public void DisplayResultSize(UInt64 TreeSize)
        {
            /*ControlThreadingHelper.InvokeControlAction(MemorySizeValueLabel, () =>
            {
                MemorySizeValueLabel.Text = Conversions.ByteCountToMetricSize(TreeSize).ToString();
            });*/
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
            DisableGUI();
        }

        private void StopScanButton_Click(object sender, EventArgs e)
        {
            EnableGUI();
        }

        private void GUIFilterTree_Resize(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
