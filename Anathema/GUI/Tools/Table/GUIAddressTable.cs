using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anathema.Properties;

namespace Anathema
{
    public partial class GUIAddressTable : UserControl, IAddressTableView
    {
        private AddressTablePresenter AddressTablePresenter;
        
        public GUIAddressTable()
        {
            InitializeComponent();

            AddressTablePresenter = new AddressTablePresenter(this, AddressTable.GetInstance());
        }

        public void UpdateAddressTableItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
            {
                AddressTableListView.BeginUpdate();
                AddressTableListView.VirtualListSize = ItemCount;
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
            List<Int32> Indicies = new List<Int32>();
            foreach (Int32 Index in AddressTableListView.SelectedIndices)
                Indicies.Add(Index);

            if (Indicies.Count == 0)
                return;

            // Determine the current column selection based on column index
            AddressTable.TableColumnEnum ColumnSelection = AddressTable.TableColumnEnum.Frozen;
            if (ColumnIndex == AddressTableListView.Columns.IndexOf(FrozenHeader))
                ColumnSelection = AddressTable.TableColumnEnum.Frozen;
            else if (ColumnIndex == AddressTableListView.Columns.IndexOf(AddressDescriptionHeader))
                ColumnSelection = AddressTable.TableColumnEnum.Description;
            else if (ColumnIndex == AddressTableListView.Columns.IndexOf(AddressHeader))
                ColumnSelection = AddressTable.TableColumnEnum.Address;
            else if (ColumnIndex == AddressTableListView.Columns.IndexOf(TypeHeader))
                ColumnSelection = AddressTable.TableColumnEnum.ValueType;
            else if (ColumnIndex == AddressTableListView.Columns.IndexOf(ValueHeader))
                ColumnSelection = AddressTable.TableColumnEnum.Value;

            // Create editor for this entry
            GUIAddressTableEntryEditor GUIAddressTableEntryEditor = new GUIAddressTableEntryEditor(SelectedItemIndex, Indicies.ToArray(), ColumnSelection);
            GUIAddressTableEntryEditor.ShowDialog(this);
        }

        private void DeleteAddressTableEntries(Int32 StartIndex, Int32 EndIndex)
        {

        }

        #region Events

        private Point LastRightClickLocation = Point.Empty;
        
        private void AddressTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = AddressTablePresenter.GetAddressTableItemAt(E.ItemIndex);
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
            ListViewHitTestInfo HitTest = AddressTableListView.HitTest(E.Location);
            ListViewItem SelectedItem = HitTest.Item;
            Int32 ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            if (SelectedItem == null)
                return;

            EditAddressTableEntry(SelectedItem.Index, ColumnIndex);
        }


        private void EditAddressEntryToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ListViewHitTestInfo HitTest = AddressTableListView.HitTest(LastRightClickLocation);
            ListViewItem SelectedItem = HitTest.Item;
            Int32 ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            if (SelectedItem == null)
                return;

            EditAddressTableEntry(SelectedItem.Index, ColumnIndex);
        }

        private void DeleteSelectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ListViewHitTestInfo HitTest = AddressTableListView.HitTest(LastRightClickLocation);
            ListViewItem SelectedItem = HitTest.Item;
            Int32 ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            if (SelectedItem == null)
                return;

            DeleteAddressTableEntries(SelectedItem.Index, SelectedItem.Index);
        }

        private void AddNewAddressToolStripMenuItem_Click(Object Sender, EventArgs E)
        {

        }

        #endregion

    } // End class

} // End namespace