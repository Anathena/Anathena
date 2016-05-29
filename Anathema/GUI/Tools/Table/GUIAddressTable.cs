using Anathema.Source.Tables.Addresses;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Caches;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema
{
    public partial class GUIAddressTable : DockContent, IAddressTableView
    {
        private AddressTablePresenter AddressTablePresenter;
        private ListViewCache AddressTableCache;
        private Object AccessLock;

        public GUIAddressTable()
        {
            InitializeComponent();

            AddressTableCache = new ListViewCache();
            AccessLock = new Object();

            AddressTablePresenter = new AddressTablePresenter(this, AddressTable.GetInstance());
        }

        public void UpdateAddressTableItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
            {
                AddressTableListView.BeginUpdate();
                AddressTableListView.SetItemCount(ItemCount);
                AddressTableCache.FlushCache();
                AddressTableListView.EndUpdate();
            });
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
            {
                AddressTableListView.BeginUpdate();
                AddressTableListView.EndUpdate();
            });
        }

        private void UpdateReadBounds()
        {
            ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
            {
                Tuple<Int32, Int32> ReadBounds = AddressTableListView.GetReadBounds();
                AddressTablePresenter.UpdateReadBounds(ReadBounds.Item1, ReadBounds.Item2);
            });
        }

        private void EditAddressTableEntry(Int32 SelectedItemIndex, Int32 ColumnIndex)
        {
            GUIAddressTableEntryEditor GUIAddressTableEntryEditor;

            using (TimedLock.Lock(AccessLock))
            {
                List<Int32> Indicies = new List<Int32>();

                foreach (Int32 Index in AddressTableListView.SelectedIndices)
                    Indicies.Add(Index);

                if (Indicies.Count == 0)
                    return;

                // Determine the current column selection based on column index
                AddressTable.TableColumnEnum ColumnSelection = AddressTable.TableColumnEnum.Frozen;
                if (ColumnIndex == AddressTableListView.Columns.IndexOf(FrozenHeader))
                    ColumnSelection = AddressTable.TableColumnEnum.Frozen;
                else if (ColumnIndex == AddressTableListView.Columns.IndexOf(DescriptionHeader))
                    ColumnSelection = AddressTable.TableColumnEnum.Description;
                else if (ColumnIndex == AddressTableListView.Columns.IndexOf(AddressHeader))
                    ColumnSelection = AddressTable.TableColumnEnum.Address;
                else if (ColumnIndex == AddressTableListView.Columns.IndexOf(TypeHeader))
                    ColumnSelection = AddressTable.TableColumnEnum.ValueType;
                else if (ColumnIndex == AddressTableListView.Columns.IndexOf(ValueHeader))
                    ColumnSelection = AddressTable.TableColumnEnum.Value;

                GUIAddressTableEntryEditor = new GUIAddressTableEntryEditor(SelectedItemIndex, Indicies, ColumnSelection);
            }

            // Create editor for this entry
            GUIAddressTableEntryEditor.ShowDialog(this);
        }

        public void AddNewAddressItem()
        {
            AddressTablePresenter.AddNewAddressItem();
        }

        private void DeleteAddressTableEntries(Int32 StartIndex, Int32 EndIndex)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (AddressTableListView.SelectedIndices == null || AddressTableListView.SelectedIndices.Count <= 0)
                    return;

                AddressTablePresenter.DeleteTableItems(AddressTableListView.SelectedIndices.Cast<Int32>());
            }
        }

        #region Events

        private void AddAddressButton_Click(Object Sender, EventArgs E)
        {
            AddNewAddressItem();
        }

        private Point LastRightClickLocation = Point.Empty;

        private void AddressTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            ListViewItem Item = AddressTableCache.Get((UInt64)E.ItemIndex);
            AddressItem AddressItem = AddressTablePresenter.GetAddressItemAt(E.ItemIndex);

            // Try to update and return the item if it is a valid item
            if (Item != null &&
                AddressTableCache.TryUpdateSubItem(E.ItemIndex, AddressTableListView.Columns.IndexOf(ValueHeader), AddressItem.GetValueString()) &&
                AddressTableCache.TryUpdateSubItem(E.ItemIndex, AddressTableListView.Columns.IndexOf(AddressHeader), AddressItem.GetAddressString()))
            {
                Item.Checked = AddressItem.GetActivationState();
                E.Item = Item;
                return;
            }

            // Add the properties to the manager and get the list view item back
            Item = AddressTableCache.Add(E.ItemIndex, new String[AddressTableListView.Columns.Count]);

            Item.ForeColor = AddressItem.IsHex ? Color.Green : SystemColors.ControlText;

            Item.SubItems[AddressTableListView.Columns.IndexOf(FrozenHeader)].Text = String.Empty;
            Item.SubItems[AddressTableListView.Columns.IndexOf(DescriptionHeader)].Text = (AddressItem.Description == null ? String.Empty : AddressItem.Description);
            Item.SubItems[AddressTableListView.Columns.IndexOf(AddressHeader)].Text = Conversions.ToAddress(AddressItem.BaseAddress);
            Item.SubItems[AddressTableListView.Columns.IndexOf(TypeHeader)].Text = AddressItem.ElementType == null ? String.Empty : AddressItem.ElementType.Name;
            Item.SubItems[AddressTableListView.Columns.IndexOf(ValueHeader)].Text = AddressItem.GetValueString();

            Item.Checked = AddressItem.GetActivationState();

            // AddressTablePresenter.GetAddressTableItemAt(E.ItemIndex);
            E.Item = Item;
        }

        private void AddressTableListView_MouseClick(Object Sender, MouseEventArgs E)
        {
            if (E.Button == MouseButtons.Right)
                LastRightClickLocation = E.Location;

            ListViewItem ListViewItem = AddressTableListView.GetItemAt(E.X, E.Y);

            if (ListViewItem == null)
                return;

            if (E.X < (ListViewItem.Bounds.Left + 16))
                AddressTablePresenter.SetAddressFrozen(ListViewItem.Index, !ListViewItem.Checked);  // (Has to be negated, click happens before check change)
        }

        private void AddressTableListView_KeyPress(Object Sender, KeyPressEventArgs E)
        {
            if (E.KeyChar != ' ')
                return;

            Boolean FreezeState = AddressTableListView.SelectedIndices == null ? false : !AddressTableListView.Items[AddressTableListView.SelectedIndices[0]].Checked;
            foreach (Int32 Index in AddressTableListView.SelectedIndices)
                AddressTablePresenter.SetAddressFrozen(Index, FreezeState);
        }

        private void ToggleFreezeToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            Boolean FreezeState = AddressTableListView.SelectedIndices == null ? false : !AddressTableListView.Items[AddressTableListView.SelectedIndices[0]].Checked;
            foreach (Int32 Index in AddressTableListView.SelectedIndices)
                AddressTablePresenter.SetAddressFrozen(Index, FreezeState);
        }

        private void AddressTableListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
        {
            ListViewItem SelectedItem;
            Int32 ColumnIndex;

            ListViewHitTestInfo HitTest = AddressTableListView.HitTest(E.Location);
            SelectedItem = HitTest.Item;
            ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            // Do not bring up edit menu on double clicks to checkbox
            if (ColumnIndex == AddressTableListView.Columns.IndexOf(FrozenHeader))
                return;

            if (SelectedItem == null)
                return;

            EditAddressTableEntry(SelectedItem.Index, ColumnIndex);
        }


        private void EditAddressEntryToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ListViewItem SelectedItem;
            Int32 ColumnIndex;

            ListViewHitTestInfo HitTest = AddressTableListView.HitTest(LastRightClickLocation);
            SelectedItem = HitTest.Item;
            ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            if (SelectedItem == null)
                return;

            EditAddressTableEntry(SelectedItem.Index, ColumnIndex);
        }

        private void DeleteSelectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ListViewItem SelectedItem;
            Int32 ColumnIndex;

            ListViewHitTestInfo HitTest = AddressTableListView.HitTest(LastRightClickLocation);
            SelectedItem = HitTest.Item;
            ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            if (SelectedItem == null)
                return;

            DeleteAddressTableEntries(SelectedItem.Index, SelectedItem.Index);

            AddressTableListView.SelectedIndices.Clear();
        }

        private void AddNewAddressToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewAddressItem();
        }

        private void AddressTableContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
        {
            // using (TimedLock.Lock(AccessLock))
            {
                ListViewHitTestInfo HitTest = AddressTableListView.HitTest(AddressTableListView.PointToClient(MousePosition));
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                {
                    ToggleFreezeToolStripMenuItem.Enabled = false;
                    EditAddressEntryToolStripMenuItem.Enabled = false;
                    DeleteSelectionToolStripMenuItem.Enabled = false;
                    AddNewAddressToolStripMenuItem.Enabled = true;
                }
                else
                {
                    ToggleFreezeToolStripMenuItem.Enabled = true;
                    EditAddressEntryToolStripMenuItem.Enabled = true;
                    DeleteSelectionToolStripMenuItem.Enabled = true;
                    AddNewAddressToolStripMenuItem.Enabled = true;
                }
            }
        }

        private ListViewItem DraggedItem;
        private void AddressTableListView_ItemDrag(Object Sender, ItemDragEventArgs E)
        {
            DraggedItem = (ListViewItem)E.Item;
            DoDragDrop(E.Item, DragDropEffects.All);
        }

        private void AddressTableListView_DragOver(Object Sender, DragEventArgs E)
        {
            E.Effect = DragDropEffects.All;
        }

        private void AddressTableListView_DragDrop(Object Sender, DragEventArgs E)
        {
            // using (TimedLock.Lock(AccessLock))
            {
                ListViewHitTestInfo HitTest = AddressTableListView.HitTest(AddressTableListView.PointToClient(new Point(E.X, E.Y)));
                ListViewItem SelectedItem = HitTest.Item;

                if (DraggedItem == null || DraggedItem == SelectedItem)
                    return;

                if ((SelectedItem != null && SelectedItem.GetType() != typeof(ListViewItem)) || DraggedItem.GetType() != typeof(ListViewItem))
                    return;

                AddressTablePresenter.ReorderItem(DraggedItem.Index, SelectedItem == null ? AddressTableListView.Items.Count : SelectedItem.Index);
            }
        }

        #endregion

    } // End class

} // End namespace