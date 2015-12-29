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
    public partial class GUIFilterTree : DockContent, IFilterTreeScanView
    {
        private FilterTreeScanPresenter FilterTreeScanPresenter;

        public GUIFilterTree()
        {
            InitializeComponent();

            FilterTreeScanPresenter = new FilterTreeScanPresenter(this, new FilterTreeScan());
            EnableGUI();
        }

        public void EventFilterFinished(List<RemoteRegion> MemoryRegions)
        {

        }

        public void DisplayResultSize(UInt64 TreeSize)
        {
            ControlThreadingHelper.InvokeControlAction(MemorySizeValueLabel, () =>
            {
                MemorySizeValueLabel.Text = Conversions.ByteCountToMetricSize(TreeSize).ToString();
            });
        }

        private void UpdateTreeSplits(Int32 Splits)
        {
            // TreeSplitsValueLabel.Text = Splits.ToString();
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
            FilterTreeScanPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(object sender, EventArgs e)
        {
            FilterTreeScanPresenter.EndScan();
            EnableGUI();
        }

        private void GUIFilterTree_Resize(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
