using System;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;
using Anathema.Utils.Validation;
using Anathema.Utils.MVP;
using Anathema.Scanners.ChangeCounter;

namespace Anathema.GUI
{
    public partial class GUIChangeCounter : DockContent, IChangeCounterView
    {
        ChangeCounterPresenter ChangeCounterPresenter;

        public GUIChangeCounter()
        {
            InitializeComponent();

            ChangeCounterPresenter = new ChangeCounterPresenter(this, new ChangeCounter());

            SetMinChanges();
            SetMaxChanges();
            SetVariableSize();

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
            UInt16 MinChanges = (UInt16)MinChangesTrackBar.Value;
            MinChangesValueLabel.Text = MinChanges.ToString();
            ChangeCounterPresenter.SetMinChanges(MinChanges);
        }

        private void SetMaxChanges()
        {
            UInt16 MaxChanges = (UInt16)MaxChangesTrackBar.Value;
            String MaxChangesString = MaxChanges.ToString();
            
            if (MaxChanges == MaxChangesTrackBar.Maximum)
            {
                MaxChanges = UInt16.MaxValue;
                MaxChangesString = "Inf";
            }

            MaxChangesValueLabel.Text = MaxChangesString;
            ChangeCounterPresenter.SetMaxChanges(MaxChanges);
        }

        private void SetVariableSize()
        {
            Int32 VariableSize = (Int32)Math.Pow(2, VariableSizeTrackBar.Value);
            VariableSizeValueLabel.Text = Conversions.BytesToMetric(VariableSize);
            ChangeCounterPresenter.SetVariableSize(VariableSize);
        }

        private void EnableGUI()
        {
            StartScanButton.Enabled = true;
            StopScanButton.Enabled = false;
            MinChangesTrackBar.Enabled = true;
            MaxChangesTrackBar.Enabled = true;
            VariableSizeTrackBar.Enabled = true;
        }

        private void DisableGUI()
        {
            StartScanButton.Enabled = false;
            StopScanButton.Enabled = true;
            MinChangesTrackBar.Enabled = false;
            MaxChangesTrackBar.Enabled = false;
            VariableSizeTrackBar.Enabled = false;
        }

        private void HandleResize()
        {
            MinChangesTrackBar.Width = (this.Width - MinChangesTrackBar.Location.X) / 2;
            MaxChangesTrackBar.Location = new Point(MinChangesTrackBar.Location.X + MinChangesTrackBar.Width, MaxChangesTrackBar.Location.Y);
            MaxChangesTrackBar.Width = MinChangesTrackBar.Width;

            VariableSizeTrackBar.Width = (this.Width - VariableSizeTrackBar.Location.X) / 2;
        }

        #region Events

        private void MinChangesTrackBar_Scroll(Object Sender, EventArgs E)
        {
            if (MinChangesTrackBar.Value > MaxChangesTrackBar.Value)
            {
                MaxChangesTrackBar.Value = MinChangesTrackBar.Value;
                SetMaxChanges();
            }

            SetMinChanges();
        }

        private void MaxChangesTrackBar_Scroll(Object Sender, EventArgs E)
        {
            if (MaxChangesTrackBar.Value < MinChangesTrackBar.Value)
            {
                MinChangesTrackBar.Value = MaxChangesTrackBar.Value;
                SetMinChanges();
            }
            SetMaxChanges();
        }

        private void VariableSizeTrackBar_Scroll(Object Sender, EventArgs E)
        {
            SetVariableSize();
        }

        private void GUILabelerChangeCounter_Resize(Object Sender, EventArgs E)
        {
            HandleResize();
        }

        private void StartScanButton_Click(object sender, EventArgs e)
        {
            ChangeCounterPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(object sender, EventArgs e)
        {
            ChangeCounterPresenter.EndScan();
            EnableGUI();
        }

        #endregion

    } // End class

} // End namespace