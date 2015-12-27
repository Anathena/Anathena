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
    public partial class GUIFilterChunks : DockContent, IFilterChunkScanView
    {
        private FilterChunkScanPresenter FilterChunkScanPresenter;

        public GUIFilterChunks()
        {
            InitializeComponent();
            FilterChunkScanPresenter = new FilterChunkScanPresenter(this, new FilterChunkScan());
            
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

        private void SetChunkSize(Int32 Value)
        {
            MemorySizeValueLabel.Text = Conversions.ByteCountToMetricSize((UInt64)Value).ToString();
            FilterChunkScanPresenter.SetChunkSize(Value);
        }

        private void HandleResize()
        { 
            ChunkSizeTrackBar.Width = (this.Width - ChunkSizeTrackBar.Location.X) / 2;
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
            FilterChunkScanPresenter.BeginFilter();
            DisableGUI();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            Snapshot Result = FilterChunkScanPresenter.EndFilter();
            SnapshotManager.GetSnapshotManagerInstance().SaveSnapshot(Result);
            EnableGUI();
        }

        private void GUIFilterChunks_Resize(object sender, EventArgs e)
        {
            HandleResize();
        }
        
        private void ChunkSizeTrackBar_Scroll(Object Sender, EventArgs E)
        {
            SetChunkSize((Int32)Math.Pow(2, ChunkSizeTrackBar.Value));
        }

        #endregion
    }
}
