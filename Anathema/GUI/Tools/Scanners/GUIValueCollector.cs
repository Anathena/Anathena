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

            InitializeValueTypeComboBox();
            EnableGUI();
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);

            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
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

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            Type ElementType = Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString());
            ValueCollectorPresenter.SetElementType(ElementType);
        }

        #endregion

    } // End class

} // End namespace