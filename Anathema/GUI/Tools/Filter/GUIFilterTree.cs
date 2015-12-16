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
            UpdateMinChanges();
            UpdateMaxChanges();
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
            UInt64 Value = (UInt64)Math.Pow(2, VariableSizeTrackBar.Value);
            String LabelText = Conversions.ByteCountToMetricSize(Value).ToString();

            VariableSizeValueLabel.Text = LabelText;

            FilterHashTreesPresenter.SetVariableSize(Value);
        }

        private void UpdateMinChanges()
        {
            UInt64 Value = (UInt64)MinChangesTrackBar.Value;
            MinChangesValueLabel.Text = Value.ToString();
        }

        private void UpdateMaxChanges()
        {
            Int32 Value = MaxChangesTrackBar.Value;
            if (Value == MaxChangesTrackBar.Maximum)
            {
                Value = Int32.MaxValue;
                MaxChangesValueLabel.Text = "Infinity";
            }
            else
                MaxChangesValueLabel.Text = Value.ToString();
        }

        private void HandleResize()
        {
            MaxChangesTrackBar.Location = new Point(this.Width / 2, MaxChangesTrackBar.Location.Y);
            MaxChangesTrackBar.Width = this.Width - MaxChangesTrackBar.Location.X;
            MinChangesTrackBar.Width = MaxChangesTrackBar.Location.X - MinChangesTrackBar.Location.X;

            VariableSizeTrackBar.Width = (this.Width - VariableSizeTrackBar.Location.X) / 2;
        }

        private void DisableGUI()
        {
            //AdvancedSettingsGroupBox.Enabled = false;
        }

        private void EnableGUI()
        {
            //AdvancedSettingsGroupBox.Enabled = true;
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

        private void MinChangesTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateMinChanges();

            if (MinChangesTrackBar.Value > MaxChangesTrackBar.Value)
            {
                MaxChangesTrackBar.Value = MinChangesTrackBar.Value;
                MaxChangesTrackBar_Scroll(sender, e);
            }
        }

        private void MaxChangesTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateMaxChanges();

            if (MaxChangesTrackBar.Value < MinChangesTrackBar.Value)
            {
                MinChangesTrackBar.Value = MaxChangesTrackBar.Value;
                MinChangesTrackBar_Scroll(sender, e);
            }
        }
        #endregion

    }
}
