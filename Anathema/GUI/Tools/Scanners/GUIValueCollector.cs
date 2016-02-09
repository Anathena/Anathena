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
    public partial class GUIValueCollector : DockContent, IValueCollectorView
    {
        private ValueCollectorPresenter ValueCollectorPresenter;

        public GUIValueCollector()
        {
            InitializeComponent();
            ValueCollectorPresenter = new ValueCollectorPresenter(this, new ValueCollector());
            
            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount) { }

        private void DisableGUI()
        {
            StartScanButton.Enabled = false;
        }

        private void EnableGUI()
        {
            StartScanButton.Enabled = true;
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            ValueCollectorPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            ValueCollectorPresenter.EndScan();
            EnableGUI();
        }

        #endregion

    } // End class

} // End namespace