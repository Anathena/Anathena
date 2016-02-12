using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Anathema.MemoryManagement;
using Anathema.MemoryManagement.Memory;
using System.Linq;

namespace Anathema
{
    public partial class GUIPointerScanner : DockContent, IPointerScannerView
    {
        private PointerScannerPresenter PointerScannerPresenter;

        private const Int32 DefaultLevel = 3;
        private const Int32 DefaultOffset = 2048;

        public GUIPointerScanner()
        {
            InitializeComponent();

            PointerScannerPresenter = new PointerScannerPresenter(this, new PointerScanner());

            GUIConstraintEditor.RemoveRelativeScans();
            
            InitializeValueTypeComboBox();
            InitializeDefaults();
            UpdateRescanMode();
            EnableGUI();
        }

        private void InitializeDefaults()
        {
            MaxOffsetTextBox.SetElementType(typeof(Int32));
            MaxLevelTextBox.SetElementType(typeof(Int32));

            MaxLevelTextBox.SetValue(DefaultLevel);
            MaxOffsetTextBox.SetValue(DefaultOffset);
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);

            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        public void DisplayScanCount(Int32 ScanCount) { }

        private void UpdateReadBounds()
        {
            ControlThreadingHelper.InvokeControlAction(PointerListView, () =>
            {
                Tuple<Int32, Int32> ReadBounds = PointerListView.GetReadBounds();
                PointerScannerPresenter.UpdateReadBounds(ReadBounds.Item1, ReadBounds.Item2);
            });
        }

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

                PointerListView.SetItemCount(ItemCount);
            });

            EnableGUI();
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            ControlThreadingHelper.InvokeControlAction<Control>(PointerListView, () =>
            {
                PointerListView.BeginUpdate();
                PointerListView.EndUpdate();
            });
        }

        private void AddSelectedElements()
        {
            if (PointerListView.SelectedIndices.Count <= 0)
                return;

            PointerScannerPresenter.AddSelectionToTable(PointerListView.SelectedIndices[0], PointerListView.SelectedIndices[PointerListView.SelectedIndices.Count - 1]);
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
                RebuildPointersButton.Enabled = false;
                AddSelectedResultsButton.Enabled = false;
                // StopScanButton.Enabled = true;
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
                RebuildPointersButton.Enabled = true;
                AddSelectedResultsButton.Enabled = true;
                // StopScanButton.Enabled = false;
            });
        }

        private void UpdateRescanMode()
        {
            if (AddressModeRadioButton.Checked)
            {
                AddressTextBox.Enabled = true;
                GUIConstraintEditor.Enabled = false;
                PointerScannerPresenter.SetRescanMode(true);
            }
            else if (ValueModeRadioButton.Checked)
            {
                AddressTextBox.Enabled = false;
                GUIConstraintEditor.Enabled = true;
                PointerScannerPresenter.SetRescanMode(false);
            }
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            if (ValueModeRadioButton.Checked)
                return;

            // Validate input
            if (!AddressTextBox.IsValid() || !MaxLevelTextBox.IsValid() || !MaxOffsetTextBox.IsValid())
                return;

            // Apply settings
            PointerScannerPresenter.SetTargetAddress(AddressTextBox.GetValueAsHexidecimal());
            PointerScannerPresenter.SetMaxPointerLevel(MaxLevelTextBox.GetValueAsDecimal());
            PointerScannerPresenter.SetMaxPointerOffset(MaxOffsetTextBox.GetValueAsDecimal());

            // Start scan
            DisableGUI();
            PointerScannerPresenter.BeginPointerScan();
        }

        private void RebuildPointersButton_Click(Object Sender, EventArgs E)
        {
            DisableGUI();

            if (AddressModeRadioButton.Checked)
            {
                // Address mode -- update address
                if (!AddressTextBox.IsValid())
                    return;
                PointerScannerPresenter.SetTargetAddress(AddressTextBox.GetValueAsHexidecimal());
            }
            else
            {
                // Value mode -- gather scan constraints
                PointerScannerPresenter.SetScanConstraintManager(GUIConstraintEditor.GetScanConstraintManager());
            }

            PointerScannerPresenter.BeginPointerRescan();
        }

        private void PointerListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = PointerScannerPresenter.GetItemAt(E.ItemIndex);
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            Type ElementType = Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString());
            PointerScannerPresenter.SetElementType(ElementType);
            GUIConstraintEditor.SetElementType(ElementType);
        }

        private void AddSelectedResultsButton_Click(Object Sender, EventArgs E)
        {
            AddSelectedElements();
        }

        private void PointerListView_DoubleClick(Object Sender, EventArgs E)
        {
            AddSelectedElements();
        }

        private void AddressModeRadioButton_CheckedChanged(Object Sender, EventArgs E)
        {
            UpdateRescanMode();
        }

        private void ValueModeRadioButton_CheckedChanged(Object Sender, EventArgs E)
        {
            UpdateRescanMode();
        }

        private void GUIPointerScanner_Resize(Object Sender, EventArgs E)
        {
            // Ensure tabs take up the entire width of the control
            const Int32 TabBoarderOffset = 3;
            PointerScanTabControl.ItemSize = new Size(Math.Max(0, (PointerScanTabControl.Width - TabBoarderOffset)) / Math.Max(1, PointerScanTabControl.TabCount), 0);
        }

        #endregion

    } // End class

} // End namespace