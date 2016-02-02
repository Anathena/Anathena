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

        private const Int32 DefaultLevel = 5;
        private const Int32 DefaultOffset = 2048;

        public GUIPointerScanner()
        {
            InitializeComponent();

            PointerScannerPresenter = new PointerScannerPresenter(this, new PointerScanner());

            InitializeDefaults();
            EnableGUI();
        }

        private void InitializeDefaults()
        {
            MaxLevelTextBox.Text = DefaultLevel.ToString();
            MaxOffsetTextBox.Text = DefaultOffset.ToString();
        }

        public void DisplayScanCount(Int32 ScanCount) { }

        public void ScanFinished(Int32 ItemCount, Int32 MaxPointerLevel)
        {
            ControlThreadingHelper.InvokeControlAction<Control>(PointerListView, () =>
            {
                PointerListView.Items.Clear();

                // Remove offset columns
                while (PointerListView.Columns.Count > 2)
                    PointerListView.Columns.RemoveAt(2);
                
                // Create offset columns based on max level
                for (Int32 OffsetIndex = 0; OffsetIndex < MaxPointerLevel; OffsetIndex++)
                    PointerListView.Columns.Add("Offset " + OffsetIndex.ToString());

                PointerListView.VirtualListSize = ItemCount;
            });

            EnableGUI();
        }

        public void ReadValues()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(PointerListView, () =>
            {
                PointerListView.BeginUpdate();
                PointerListView.EndUpdate();
            });
        }

        private void DisableGUI()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(PointerListView, () =>
            {
                PointerListView.Enabled = false;
            });

            ControlThreadingHelper.InvokeControlAction<Control>(ScanToolStrip, () =>
            {
                StartScanButton.Enabled = false;
                StopScanButton.Enabled = true;
            });
        }

        private void EnableGUI()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(PointerListView, () =>
            {
                PointerListView.Enabled = true;
            });

            ControlThreadingHelper.InvokeControlAction<Control>(ScanToolStrip, () =>
            {
                StartScanButton.Enabled = true;
                StopScanButton.Enabled = false;
            });
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            DisableGUI();

            if (!PointerScannerPresenter.TrySetTargetAddress(TargetAddressTextBox.Text))
                return;

            if (!PointerScannerPresenter.TrySetMaxPointerLevel(MaxLevelTextBox.Text))
                return;

            if (!PointerScannerPresenter.TrySetMaxPointerOffset(MaxOffsetTextBox.Text))
                return;

            PointerScannerPresenter.BeginPointerScan();
        }

        private void RebuildPointersButton_Click(Object Sender, EventArgs E)
        {
            DisableGUI();

            if (!PointerScannerPresenter.TrySetTargetAddress(TargetAddressTextBox.Text))
                return;

            PointerScannerPresenter.BeginPointerRescan();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            EnableGUI();
        }

        private void PointerListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = PointerScannerPresenter.GetItemAt(E.ItemIndex);
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
            if (CheckSyntax.IsInt32(MaxOffsetTextBox.Text))
                MaxOffsetTextBox.ForeColor = SystemColors.ControlText;
            else
                MaxOffsetTextBox.ForeColor = Color.Red;
        }

        #endregion

    } // End class

} // End namespace