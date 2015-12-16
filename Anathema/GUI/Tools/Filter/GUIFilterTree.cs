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
        private FilterTreeScanPresenter FilterHashTreesPresenter;

        public GUIFilterTree()
        {
            InitializeComponent();

            FilterHashTreesPresenter = new FilterTreeScanPresenter(this, new FilterTreeScan());

            UpdateVariableSize();

            EnableGUI();
        }

        public void EventFilterFinished(List<RemoteRegion> MemoryRegions)
        {

        }

        public void DisplaySplitCount(UInt64 SplitCount)
        {
            /*
            ControlThreadingHelper.InvokeControlAction(TreeSplitsValueLabel, () =>
            {
                TreeSplitsValueLabel.Text = SplitCount.ToString();
            });*/
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

        private void UpdateVariableSize()
        {
            Int32 Value = (Int32)Math.Pow(2, VariableSizeTrackBar.Value);
            String LabelText = Conversions.ByteCountToMetricSize((UInt64)Value).ToString();

            VariableSizeValueLabel.Text = LabelText;

            FilterHashTreesPresenter.SetVariableSize(Value);
        }

        private void HandleResize()
        {
            VariableSizeTrackBar.Width = (this.Width - VariableSizeTrackBar.Location.X) / 2;
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
            FilterHashTreesPresenter.BeginFilter();
            DisableGUI();
        }

        private void StopScanButton_Click(object sender, EventArgs e)
        {
            Snapshot Result = FilterHashTreesPresenter.EndFilter();
            SnapshotManager.GetSnapshotManagerInstance().SaveSnapshot(Result);
            EnableGUI();
        }

        private void GUIFilterTree_Resize(object sender, EventArgs e)
        {
            HandleResize();
        }

        private void VariableSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateVariableSize();
        }
        #endregion

    }
}
