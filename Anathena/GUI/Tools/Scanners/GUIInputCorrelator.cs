using Anathena.Source.Scanners.InputCorrelator;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using Anathena.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools.Scanners
{
    public partial class GUIInputCorrelator : DockContent, IInputCorrelatorView
    {
        private InputCorrelatorPresenter inputCorrelatorPresenter;
        private Object accessLock;

        public GUIInputCorrelator()
        {
            InitializeComponent();

            inputCorrelatorPresenter = new InputCorrelatorPresenter(this, new InputCorrelator());
            accessLock = new Object();

            SetVariableSize();
            EnableGUI();
        }


        public void SetHotKeyList(IEnumerable<String> hotKeyList)
        {
            ControlThreadingHelper.InvokeControlAction(HotKeyListView, () =>
            {
                HotKeyListView.Items.Clear();

                foreach (String hotKey in hotKeyList)
                    HotKeyListView.Items.Add(hotKey);
            });
        }

        public void DisplayScanCount(Int32 scanCount)
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(scanToolStrip, () =>
                {
                    ScanCountLabel.Text = "Scan Count: " + scanCount.ToString();
                });
            }
        }

        private void SetVariableSize()
        {
            using (TimedLock.Lock(accessLock))
            {
                Int32 variableSize = (Int32)Math.Pow(2, VariableSizeTrackBar.Value);
                VariableSizeValueLabel.Text = Conversions.BytesToMetric(variableSize).ToString();

                inputCorrelatorPresenter.SetVariableSize(variableSize);
            }
        }

        private void EnableGUI()
        {
            using (TimedLock.Lock(accessLock))
            {
                StartScanButton.Enabled = true;
                StopScanButton.Enabled = false;
                VariableSizeTrackBar.Enabled = true;
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(accessLock))
            {
                StartScanButton.Enabled = false;
                StopScanButton.Enabled = true;
                VariableSizeTrackBar.Enabled = false;
            }
        }

        #region Events

        private void EditKeysButton_Click(Object sender, EventArgs e)
        {
            inputCorrelatorPresenter.EditKeys();
        }

        private void StartScanButton_Click(Object sender, EventArgs e)
        {
            DisableGUI();
            inputCorrelatorPresenter.BeginScan();
        }

        private void StopScanButton_Click(Object sender, EventArgs e)
        {
            EnableGUI();
            inputCorrelatorPresenter.EndScan();
        }

        private void VariableSizeTrackBar_Scroll(Object sender, EventArgs e)
        {
            SetVariableSize();
        }

        #endregion

    } // End class

} // End namespace