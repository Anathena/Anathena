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
    public partial class GUIPointerScanner : DockContent, IPointerScannerView
    {
        private PointerScannerPresenter PointerScannerPresenter;

        public GUIPointerScanner()
        {
            InitializeComponent();

            PointerScannerPresenter = new PointerScannerPresenter(this, new PointerScanner());
            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount) { }

        public void EventFilterFinished(List<RemoteRegion> MemoryRegions)
        {

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
            DisableGUI();

            if (!PointerScannerPresenter.TrySetTargetAddress(TargetAddressTextBox.Text))
                return;

            if (!PointerScannerPresenter.TrySetTargetAddress(TargetAddressTextBox.Text))
                return;

            if (!PointerScannerPresenter.TrySetTargetAddress(TargetAddressTextBox.Text))
                return;

            PointerScannerPresenter.BeginScan();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            EnableGUI();
        }

        private void TargetAddressTextBox_TextChanged(Object Sender, EventArgs E)
        {
            if (CheckSyntax.Address(TargetAddressTextBox.Text))
                TargetAddressTextBox.ForeColor = SystemColors.ControlText;
            else
                TargetAddressTextBox.ForeColor = Color.Red;
        }

        private void MaxLevelTextBox_TextChanged(Object Sender, EventArgs E)
        {
            if (CheckSyntax.IsInt32(MaxLevelTextBox.Text))
                MaxLevelTextBox.ForeColor = SystemColors.ControlText;
            else
                MaxLevelTextBox.ForeColor = Color.Red;
        }

        private void MaxOffsetTextBox_TextChanged(Object Sender, EventArgs E)
        {
            if (CheckSyntax.Address(MaxOffsetTextBox.Text))
                MaxOffsetTextBox.ForeColor = SystemColors.ControlText;
            else
                MaxOffsetTextBox.ForeColor = Color.Red;
        }

        #endregion

    } // End class

} // End namespace