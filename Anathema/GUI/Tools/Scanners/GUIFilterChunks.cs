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
            
            SetMinChanges();
            EnableGUI();
        }

        private void SetMinChanges()
        {
            Int32 MinChanges = MinChangesTrackBar.Value;
            MinChangesValueLabel.Text = MinChanges.ToString();

            FilterChunkScanPresenter.SetMinChanges(MinChanges);
        }

        private void HandleResize()
        {
            MinChangesTrackBar.Width = (this.Width - MinChangesTrackBar.Location.X) / 2;
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
            FilterChunkScanPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            FilterChunkScanPresenter.EndScan();
            EnableGUI();
        }

        private void GUIFilterChunks_Resize(object sender, EventArgs e)
        {
            HandleResize();
        }

        private void MinChangesTrackBar_Scroll(object sender, EventArgs e)
        {
            SetMinChanges();
        }

        #endregion

    }
}
