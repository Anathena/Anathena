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
        private InputCorrelatorPresenter InputCorrelatorPresenter;
        private Object AccessLock;

        public GUIInputCorrelator()
        {
            InitializeComponent();

            InputCorrelatorPresenter = new InputCorrelatorPresenter(this, new InputCorrelator());
            AccessLock = new Object();

            SetVariableSize();
            EnableGUI();
        }


        public void SetHotKeyList(IEnumerable<String> HotKeyList)
        {
            ControlThreadingHelper.InvokeControlAction(HotKeyListView, () =>
            {
                HotKeyListView.Items.Clear();

                foreach (String HotKey in HotKeyList)
                    HotKeyListView.Items.Add(HotKey);
            });
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

        private void SetVariableSize()
        {
            using (TimedLock.Lock(AccessLock))
            {
                Int32 VariableSize = (Int32)Math.Pow(2, VariableSizeTrackBar.Value);
                VariableSizeValueLabel.Text = Conversions.BytesToMetric(VariableSize).ToString();

                InputCorrelatorPresenter.SetVariableSize(VariableSize);
            }
        }

        private void EnableGUI()
        {
            using (TimedLock.Lock(AccessLock))
            {
                StartScanButton.Enabled = true;
                StopScanButton.Enabled = false;
                VariableSizeTrackBar.Enabled = true;
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(AccessLock))
            {
                StartScanButton.Enabled = false;
                StopScanButton.Enabled = true;
                VariableSizeTrackBar.Enabled = false;
            }
        }

        #region Events

        private void EditKeysButton_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.EditKeys();
        }

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            DisableGUI();
            InputCorrelatorPresenter.BeginScan();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            EnableGUI();
            InputCorrelatorPresenter.EndScan();
        }

        private void VariableSizeTrackBar_Scroll(Object Sender, EventArgs E)
        {
            SetVariableSize();
        }

        #endregion

    } // End class

} // End namespace