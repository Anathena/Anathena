using Anathema.Scanners.ChunkScanner;
using Anathema.Source.Utils;
using Anathema.Utils.MVP;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIChunkScanner : DockContent, IChunkScannerView
    {
        private ChunkScannerPresenter ChunkScannerPresenter;
        private Object AccessLock;

        public GUIChunkScanner()
        {
            InitializeComponent();
            ChunkScannerPresenter = new ChunkScannerPresenter(this, new ChunkScanner());
            AccessLock = new Object();

            SetMinChanges();
            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
                {
                    ScanCountLabel.Text = "Scan Count: " + ScanCount.ToString();
                });
            }
        }

        private void SetMinChanges()
        {
            using (TimedLock.Lock(AccessLock))
            {
                Int32 MinChanges = MinChangesTrackBar.Value;
                MinChangesValueLabel.Text = MinChanges.ToString();

                ChunkScannerPresenter.SetMinChanges(MinChanges);
            }
        }

        private void HandleResize()
        {
            using (TimedLock.Lock(AccessLock))
            {
                MinChangesTrackBar.Width = (this.Width - MinChangesTrackBar.Location.X) / 2;
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(AccessLock))
            {
                StartScanButton.Enabled = false;
                StopScanButton.Enabled = true;
            }
        }

        private void EnableGUI()
        {
            using (TimedLock.Lock(AccessLock))
            {
                StartScanButton.Enabled = true;
                StopScanButton.Enabled = false;
            }
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