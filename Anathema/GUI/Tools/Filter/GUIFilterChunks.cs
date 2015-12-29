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

            SetChunkSize();
            SetMinChanges();
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

        private void SetChunkSize()
        {
            Int32 ChunkSize = (Int32)Math.Pow(2, ChunkSizeTrackBar.Value);

            ChunkSizeValueLabel.Text = Conversions.ByteCountToMetricSize((UInt64)ChunkSize).ToString();
            FilterChunkScanPresenter.SetChunkSize(ChunkSize);
        }

        private void SetMinChanges()
        {
            Int32 MinChanges = MinChangesTrackBar.Value;
            MinChangesValueLabel.Text = MinChanges.ToString();

            FilterChunkScanPresenter.SetMinChanges(MinChanges);
        }

        private void HandleResize()
        {
            ChunkSizeTrackBar.Width = (this.Width - ChunkSizeTrackBar.Location.X) / 2;
            MinChangesTrackBar.Location = new Point(ChunkSizeTrackBar.Location.X + ChunkSizeTrackBar.Width, MinChangesTrackBar.Location.Y);
            MinChangesTrackBar.Width = ChunkSizeTrackBar.Width;
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
            FilterChunkScanPresenter.EndFilter();
            EnableGUI();
        }

        private void GUIFilterChunks_Resize(object sender, EventArgs e)
        {
            HandleResize();
        }

        private void ChunkSizeTrackBar_Scroll(Object Sender, EventArgs E)
        {
            SetChunkSize();
        }

        private void MinChangesTrackBar_Scroll(object sender, EventArgs e)
        {
            SetMinChanges();
        }

        #endregion

    }
}
