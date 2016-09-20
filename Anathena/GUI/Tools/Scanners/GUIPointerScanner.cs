using Ana.Source.Scanners.PointerScanner;
using Ana.Source.Utils;
using Ana.Source.Utils.DataStructures;
using Ana.Source.Utils.Extensions;
using Ana.Source.Utils.MVP;
using Ana.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Ana.GUI.Tools.Scanners
{
    public partial class GUIPointerScanner : DockContent, IPointerScannerView
    {
        private PointerScannerPresenter pointerScannerPresenter;
        private ListViewCache listViewCache;
        private Object accessLock;

        private const Int32 defaultLevel = 3;
        private const Int32 defaultOffset = 2048;
        private const String emptyValue = "-";

        public GUIPointerScanner()
        {
            InitializeComponent();

            pointerScannerPresenter = new PointerScannerPresenter(this, new PointerScanner());
            listViewCache = new ListViewCache();
            accessLock = new Object();

            GUIConstraintEditor.RemoveRelativeScans();

            InitializeValueTypeComboBox();
            InitializeDefaults();
            UpdateRescanMode();
            EnableGUI();
        }

        private void InitializeDefaults()
        {
            using (TimedLock.Lock(accessLock))
            {
                MaxOffsetTextBox.SetElementType(typeof(Int32));
                MaxLevelTextBox.SetElementType(typeof(Int32));

                MaxLevelTextBox.SetValue(defaultLevel);
                MaxOffsetTextBox.SetValue(defaultOffset);
            }
        }

        private void InitializeValueTypeComboBox()
        {
            using (TimedLock.Lock(accessLock))
            {
                foreach (Type Primitive in PrimitiveTypes.GetScannablePrimitiveTypes())
                    ValueTypeComboBox.Items.Add(Primitive.Name);

                ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
            }
        }

        public void DisplayScanCount(Int32 scanCount) { }

        public void ScanFinished(Int32 itemCount, Int32 maxPointerLevel)
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction<Control>(PointerListView, () =>
                {
                    PointerListView.Items.Clear();

                    // Remove offset columns
                    while (PointerListView.Columns.Count > 2)
                        PointerListView.Columns.RemoveAt(2);

                    // Create offset columns based on max level
                    for (Int32 OffsetIndex = 0; OffsetIndex < maxPointerLevel; OffsetIndex++)
                        PointerListView.Columns.Add("Offset " + OffsetIndex.ToString());

                    PointerListView.SetItemCount(itemCount);
                });
            }
            EnableGUI();
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction<Control>(PointerListView, () =>
                {
                    PointerListView.BeginUpdate();
                    PointerListView.EndUpdate();
                });
            }
        }
        private void UpdateReadBounds()
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(PointerListView, () =>
                {
                    Tuple<Int32, Int32> ReadBounds = PointerListView.GetReadBounds();
                    pointerScannerPresenter.UpdateReadBounds(ReadBounds.Item1, ReadBounds.Item2);
                });
            }
        }

        private void AddSelectedElements()
        {
            using (TimedLock.Lock(accessLock))
            {
                if (PointerListView.SelectedIndices.Count <= 0)
                    return;

                pointerScannerPresenter.AddSelectionToTable(PointerListView.SelectedIndices[0], PointerListView.SelectedIndices[PointerListView.SelectedIndices.Count - 1]);
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(accessLock))
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
        }

        private void EnableGUI()
        {
            using (TimedLock.Lock(accessLock))
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
        }

        private void UpdateRescanMode()
        {
            using (TimedLock.Lock(accessLock))
            {
                if (AddressModeRadioButton.Checked)
                {
                    AddressTextBox.Enabled = true;
                    GUIConstraintEditor.Enabled = false;
                    pointerScannerPresenter.SetRescanMode(true);
                }
                else if (ValueModeRadioButton.Checked)
                {
                    AddressTextBox.Enabled = false;
                    GUIConstraintEditor.Enabled = true;
                    pointerScannerPresenter.SetRescanMode(false);
                }
            }
        }

        #region Events

        private void StartScanButton_Click(Object sender, EventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                if (ValueModeRadioButton.Checked)
                    return;

                // Validate input
                if (!AddressTextBox.IsValid() || !MaxLevelTextBox.IsValid() || !MaxOffsetTextBox.IsValid())
                    return;

                // Apply settings
                pointerScannerPresenter.SetTargetAddress(AddressTextBox.GetValueAsHexidecimal());
                pointerScannerPresenter.SetMaxPointerLevel(MaxLevelTextBox.GetValueAsDecimal());
                pointerScannerPresenter.SetMaxPointerOffset(MaxOffsetTextBox.GetValueAsDecimal());
            }

            // Start scan
            DisableGUI();
            pointerScannerPresenter.BeginPointerScan();
        }

        private void RebuildPointersButton_Click(Object sender, EventArgs e)
        {
            DisableGUI();

            using (TimedLock.Lock(accessLock))
            {
                if (AddressModeRadioButton.Checked)
                {
                    // Address mode -- update address
                    if (!AddressTextBox.IsValid())
                        return;

                    pointerScannerPresenter.SetTargetAddress(AddressTextBox.GetValueAsHexidecimal());
                }
                else
                {
                    // Value mode -- gather scan constraints
                    pointerScannerPresenter.SetScanConstraintManager(GUIConstraintEditor.GetScanConstraintManager());
                }

                pointerScannerPresenter.BeginPointerRescan();
            }
        }

        private void PointerListView_RetrieveVirtualItem(Object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem item = listViewCache.Get((UInt64)e.ItemIndex);

            // Try to update and return the item if it is a valid item
            if (item != null && listViewCache.TryUpdateSubItem(e.ItemIndex, PointerListView.Columns.IndexOf(ValueHeader), pointerScannerPresenter.GetValueAtIndex(e.ItemIndex)))
            {
                e.Item = item;
                return;
            }

            IEnumerable<String> offsets = pointerScannerPresenter.GetOffsetsAtIndex(e.ItemIndex);
            Int32 offsetStartIndex = PointerListView.Columns.IndexOf(BaseHeader) + 1;

            // Add the properties to the cache and get the list view item back
            item = listViewCache.Add(e.ItemIndex, new String[offsetStartIndex + pointerScannerPresenter.GetMaxPointerLevel()]);

            item.SubItems[PointerListView.Columns.IndexOf(ValueHeader)].Text = emptyValue;
            item.SubItems[PointerListView.Columns.IndexOf(BaseHeader)].Text = pointerScannerPresenter.GetAddressAtIndex(e.ItemIndex);
            offsets?.ForEach(X => item.SubItems[offsetStartIndex++].Text = X);

            e.Item = item;
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object sender, EventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                Type elementType = Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString());
                pointerScannerPresenter.SetElementType(elementType);
                GUIConstraintEditor.SetElementType(elementType);
            }
        }

        private void AddSelectedResultsButton_Click(Object sender, EventArgs e)
        {
            AddSelectedElements();
        }

        private void PointerListView_DoubleClick(Object sender, EventArgs e)
        {
            AddSelectedElements();
        }

        private void AddressModeRadioButton_CheckedChanged(Object sender, EventArgs e)
        {
            UpdateRescanMode();
        }

        private void ValueModeRadioButton_CheckedChanged(Object sender, EventArgs e)
        {
            UpdateRescanMode();
        }

        private void GUIPointerScanner_Resize(Object sender, EventArgs e)
        {
            // Ensure tabs take up the entire width of the control
            const Int32 tabBoarderOffset = 3;
            PointerScanTabControl.ItemSize = new Size(Math.Max(0, (PointerScanTabControl.Width - tabBoarderOffset)) / Math.Max(1, PointerScanTabControl.TabCount), 0);
        }

        #endregion

    } // End class

} // End namespace