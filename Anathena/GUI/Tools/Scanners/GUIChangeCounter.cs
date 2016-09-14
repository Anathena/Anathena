using Anathena.Source.Scanners.ChangeCounter;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using Anathena.Source.Utils.Validation;
using System;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools.Scanners
{
    public partial class GUIChangeCounter : DockContent, IChangeCounterView
    {
        private ChangeCounterPresenter changeCounterPresenter;
        private Object accessLock;

        public GUIChangeCounter()
        {
            InitializeComponent();

            changeCounterPresenter = new ChangeCounterPresenter(this, new ChangeCounter());
            accessLock = new Object();

            SetMinChanges();
            SetMaxChanges();
            SetVariableSize();

            EnableGUI();
        }

        public void DisplayScanCount(Int32 scanCount)
        {
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ScanCountLabel.Text = "Scan Count: " + scanCount.ToString();
            });
        }

        private void SetMinChanges()
        {
            using (TimedLock.Lock(accessLock))
            {
                if (MaxChangesTrackBar.Value < MinChangesTrackBar.Value)
                    MinChangesTrackBar.Value = MaxChangesTrackBar.Value;

                UInt16 MinChanges = (UInt16)MinChangesTrackBar.Value;
                MinChangesValueLabel.Text = MinChanges.ToString();
                changeCounterPresenter.SetMinChanges(MinChanges);
            }
        }

        private void SetMaxChanges()
        {
            using (TimedLock.Lock(accessLock))
            {
                if (MinChangesTrackBar.Value > MaxChangesTrackBar.Value)
                    MaxChangesTrackBar.Value = MinChangesTrackBar.Value;

                UInt16 maxChanges = (UInt16)MaxChangesTrackBar.Value;
                String maxChangesString = maxChanges.ToString();

                if (maxChanges == MaxChangesTrackBar.Maximum)
                {
                    maxChanges = UInt16.MaxValue;
                    maxChangesString = "Inf";
                }

                MaxChangesValueLabel.Text = maxChangesString;
                changeCounterPresenter.SetMaxChanges(maxChanges);
            }
        }

        private void SetVariableSize()
        {
            using (TimedLock.Lock(accessLock))
            {
                Int32 VariableSize = (Int32)Math.Pow(2, VariableSizeTrackBar.Value);
                VariableSizeValueLabel.Text = Conversions.BytesToMetric(VariableSize);
                changeCounterPresenter.SetVariableSize(VariableSize);
            }
        }

        private void EnableGUI()
        {
            using (TimedLock.Lock(accessLock))
            {
                StartScanButton.Enabled = true;
                StopScanButton.Enabled = false;
                MinChangesTrackBar.Enabled = true;
                MaxChangesTrackBar.Enabled = true;
                VariableSizeTrackBar.Enabled = true;
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(accessLock))
            {
                StartScanButton.Enabled = false;
                StopScanButton.Enabled = true;
                MinChangesTrackBar.Enabled = false;
                MaxChangesTrackBar.Enabled = false;
                VariableSizeTrackBar.Enabled = false;
            }
        }

        private void HandleResize()
        {
            MinChangesTrackBar.Width = (this.Width - MinChangesTrackBar.Location.X) / 2;
            MaxChangesTrackBar.Location = new Point(MinChangesTrackBar.Location.X + MinChangesTrackBar.Width, MaxChangesTrackBar.Location.Y);
            MaxChangesTrackBar.Width = MinChangesTrackBar.Width;

            VariableSizeTrackBar.Width = (this.Width - VariableSizeTrackBar.Location.X) / 2;
        }

        #region Events

        private void MinChangesTrackBar_Scroll(Object sender, EventArgs e)
        {
            SetMaxChanges();
            SetMinChanges();
        }

        private void MaxChangesTrackBar_Scroll(Object sender, EventArgs e)
        {
            SetMinChanges();
            SetMaxChanges();
        }

        private void VariableSizeTrackBar_Scroll(Object sender, EventArgs e)
        {
            SetVariableSize();
        }

        private void GUILabelerChangeCounter_Resize(Object sender, EventArgs e)
        {
            HandleResize();
        }

        private void StartScanButton_Click(Object sender, EventArgs e)
        {
            changeCounterPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(Object sender, EventArgs e)
        {
            changeCounterPresenter.EndScan();
            EnableGUI();
        }

        #endregion

    } // End class

} // End namespace