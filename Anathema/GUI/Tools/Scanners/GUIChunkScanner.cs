using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Anathema.MemoryManagement;
using Anathema.MemoryManagement.Memory;

namespace Anathema
{
    public partial class GUIChunkScanner : DockContent, IChunkScannerView
    {
        private ChunkScannerPresenter ChunkScannerPresenter;

        public GUIChunkScanner()
        {
            InitializeComponent();
            ChunkScannerPresenter = new ChunkScannerPresenter(this, new ChunkScanner());
            
            SetMinChanges();
            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount)
        {
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ScanCountLabel.Text = "Scan Count: " + ScanCount.ToString();
            });
        }

        private void SetMinChanges()
        {
            Int32 MinChanges = MinChangesTrackBar.Value;
            MinChangesValueLabel.Text = MinChanges.ToString();

            ChunkScannerPresenter.SetMinChanges(MinChanges);
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
            ChunkScannerPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            ChunkScannerPresenter.EndScan();
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

    } // End class

} // End namespace