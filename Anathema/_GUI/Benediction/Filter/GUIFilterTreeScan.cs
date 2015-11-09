using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIFilterTreeScan : UserControl, IFilterTreeScanView, IProcessObserver
    {
        private FilterTreeScanPresenter FilterHashTreesPresenter;
        private const Int32 MarginSize = 4;

        public GUIFilterTreeScan()
        {
            InitializeComponent();

            FilterHashTreesPresenter = new FilterTreeScanPresenter(this, new FilterTreeScan());

            UpdateFragmentSizeLabel();
            UpdateVariableSizeLabel();
        }

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            FilterHashTreesPresenter.UpdateProcess(MemoryEditor);
        }

        public void EventFilterFinished(List<RemoteRegion> MemoryRegions)
        {

        }

        public void DisplaySplitCount(UInt64 SplitCount)
        {
            ControlThreadingHelper.InvokeControlAction(TreeSplitsValueLabel, () =>
            {
                TreeSplitsValueLabel.Text = SplitCount.ToString();
            });
        }

        public void DisplayTreeSize(UInt64 TreeSize)
        {
            ControlThreadingHelper.InvokeControlAction(MemorySizeValueLabel, () =>
            {
                MemorySizeValueLabel.Text = Conversions.ByteCountToMetricSize(TreeSize).ToString();
            });
        }

        private void UpdateTreeSplits(Int32 Splits)
        {
            TreeSplitsValueLabel.Text = Splits.ToString();
        }

        private void UpdateFragmentSizeLabel()
        {
            UInt64 Value = (UInt64)Math.Pow(2, FragmentSizeTrackBar.Value);
            string LabelText = Conversions.ByteCountToMetricSize(Value).ToString();

            FragmentSizeValueLabel.Text = LabelText;

            FilterHashTreesPresenter.SetLeafSize(Value);
        }

        private void UpdateVariableSizeLabel()
        {
            UInt64 Value = (UInt64)Math.Pow(2, VariableSizeTrackBar.Value);
            string LabelText = Conversions.ByteCountToMetricSize(Value).ToString();

            VariableSizeValueLabel.Text = LabelText;

            FilterHashTreesPresenter.SetVariableSize(Value);
        }

        private void GUIMemoryTreeFilter_Resize(object sender, EventArgs e)
        {
            //AdvancedSettingsGroupBox.SetBounds(MarginSize, this.Height / 2 + MarginSize,
            //    this.Width - MarginSize * 2, this.Height / 2 - MarginSize * 2);
        }

        private void DisableGUI()
        {
            AdvancedSettingsGroupBox.Enabled = false;
        }

        private void EnableGUI()
        {
            AdvancedSettingsGroupBox.Enabled = true;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            FilterHashTreesPresenter.BeginFilter();
            DisableGUI();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            FilterHashTreesPresenter.EndFilter();
            EnableGUI();
        }

        private void GranularityTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateFragmentSizeLabel();
        }

        private void VariableSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateVariableSizeLabel();
        }
    }
}
