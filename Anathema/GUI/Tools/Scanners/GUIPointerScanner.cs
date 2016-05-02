using Anathema.Scanners.PointerScanner;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Anathema.Utils;
using Anathema.Utils.Cache;
using Anathema.Utils.MVP;
using Anathema.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIPointerScanner : DockContent, IPointerScannerView
    {
        private PointerScannerPresenter PointerScannerPresenter;
        private ListViewCache ListViewCache;
        private Object AccessLock;

        private const Int32 DefaultLevel = 3;
        private const Int32 DefaultOffset = 2048;


        private const String EmptyValue = "-";

        public GUIPointerScanner()
        {
            InitializeComponent();

            PointerScannerPresenter = new PointerScannerPresenter(this, new PointerScanner());
            ListViewCache = new ListViewCache();
            AccessLock = new Object();

            GUIConstraintEditor.RemoveRelativeScans();

            InitializeValueTypeComboBox();
            InitializeDefaults();
            UpdateRescanMode();
            EnableGUI();
        }

        private void InitializeDefaults()
        {
            using (TimedLock.Lock(AccessLock))
            {
                MaxOffsetTextBox.SetElementType(typeof(Int32));
                MaxLevelTextBox.SetElementType(typeof(Int32));

                MaxLevelTextBox.SetValue(DefaultLevel);
                MaxOffsetTextBox.SetValue(DefaultOffset);
            }
        }

        private void InitializeValueTypeComboBox()
        {
            using (TimedLock.Lock(AccessLock))
            {
                foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                    ValueTypeComboBox.Items.Add(Primitive.Name);

                ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
            }
        }

        public void DisplayScanCount(Int32 ScanCount) { }

        public void ScanFinished(Int32 ItemCount, Int32 MaxPointerLevel)
        {
            using (TimedLock.Lock(AccessLock))
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
            }
            EnableGUI();
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            using (TimedLock.Lock(AccessLock))
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
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(PointerListView, () =>
                {
                    Tuple<Int32, Int32> ReadBounds = PointerListView.GetReadBounds();
                    PointerScannerPresenter.UpdateReadBounds(ReadBounds.Item1, ReadBounds.Item2);
                });
            }
        }

        private void AddSelectedElements()
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (PointerListView.SelectedIndices.Count <= 0)
                    return;

                PointerScannerPresenter.AddSelectionToTable(PointerListView.SelectedIndices[0], PointerListView.SelectedIndices[PointerListView.SelectedIndices.Count - 1]);
            }
        }

        private void DisableGUI()
        {
            using (TimedLock.Lock(AccessLock))
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
            using (TimedLock.Lock(AccessLock))
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
            using (TimedLock.Lock(AccessLock))
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
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
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
            }

            // Start scan
            DisableGUI();
            PointerScannerPresenter.BeginPointerScan();
        }

        private void RebuildPointersButton_Click(Object Sender, EventArgs E)
        {
            DisableGUI();

            using (TimedLock.Lock(AccessLock))
            {
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
        }

        private void PointerListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            ListViewItem Item = ListViewCache.Get((UInt64)E.ItemIndex);

            // Try to update and return the item if it is a valid item
            if (Item != null && ListViewCache.TryUpdateSubItem(E.ItemIndex, PointerListView.Columns.IndexOf(ValueHeader), PointerScannerPresenter.GetValueAtIndex(E.ItemIndex)))
            {
                E.Item = Item;
                return;
            }

            IEnumerable<String> Offsets = PointerScannerPresenter.GetOffsetsAtIndex(E.ItemIndex);
            Int32 OffsetStartIndex = PointerListView.Columns.IndexOf(BaseHeader) + 1;

            // Add the properties to the cache and get the list view item back
            Item = ListViewCache.Add(E.ItemIndex, new String[OffsetStartIndex + PointerScannerPresenter.GetMaxPointerLevel()]);

            Item.SubItems[PointerListView.Columns.IndexOf(ValueHeader)].Text = EmptyValue;
            Item.SubItems[PointerListView.Columns.IndexOf(BaseHeader)].Text = PointerScannerPresenter.GetAddressAtIndex(E.ItemIndex);
            Offsets?.ForEach(X => Item.SubItems[OffsetStartIndex++].Text = X);

            E.Item = Item;
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                Type ElementType = Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString());
                PointerScannerPresenter.SetElementType(ElementType);
                GUIConstraintEditor.SetElementType(ElementType);
            }
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
            using (TimedLock.Lock(AccessLock))
            {
                const Int32 TabBoarderOffset = 3;
                PointerScanTabControl.ItemSize = new Size(Math.Max(0, (PointerScanTabControl.Width - TabBoarderOffset)) / Math.Max(1, PointerScanTabControl.TabCount), 0);
            }
        }

        #endregion

    } // End class

} // End namespace