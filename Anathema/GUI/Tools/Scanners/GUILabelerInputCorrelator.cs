using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace Anathema
{
    public partial class GUILabelerInputCorrelator : DockContent, ILabelerInputCorrelatorView
    {
        LabelerInputCorrelatorPresenter LabelerInputCorrelatorPresenter;
       
        public GUILabelerInputCorrelator()
        {
            InitializeComponent();

            LabelerInputCorrelatorPresenter = new LabelerInputCorrelatorPresenter(this, new LabelerInputCorrelator());

            SetVariableSize();
            EnableGUI();
        }

        private void SetVariableSize()
        {
            Int32 VariableSize = (Int32)Math.Pow(2, VariableSizeTrackBar.Value);
            VariableSizeValueLabel.Text = Conversions.BytesToMetric((UInt64)VariableSize).ToString();

            LabelerInputCorrelatorPresenter.SetVariableSize(VariableSize);
        }

        private void EnableGUI()
        {
            StartScanButton.Enabled = true;
            StopScanButton.Enabled = false;
            VariableSizeTrackBar.Enabled = true;
        }

        private void DisableGUI()
        {
            StartScanButton.Enabled = false;
            StopScanButton.Enabled = true;
            VariableSizeTrackBar.Enabled = false;
        }

        #region Events

        private void StartScanButton_Click(object sender, EventArgs e)
        {
            DisableGUI();
            LabelerInputCorrelatorPresenter.BeginScan();
        }

        private void StopScanButton_Click(object sender, EventArgs e)
        {
            EnableGUI();
            LabelerInputCorrelatorPresenter.EndScan();
        }

        private void HandleResize()
        {
            VariableSizeTrackBar.Width = this.Width / 2 - VariableSizeTrackBar.Location.X;
        }

        private void VariableSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            SetVariableSize();
        }

        private void VariableToUserRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UserToVariableRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void EitherRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GUILabelerInputCorrelator_Resize(object sender, EventArgs e)
        {
            HandleResize();
        }

        #endregion

    }
}
