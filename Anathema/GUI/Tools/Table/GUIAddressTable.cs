using Anathema.Source.Utils;
using Anathema.User.UserAddressTable;
using Anathema.Utils.Cache;
using Anathema.Utils.MVP;
using Anathema.Utils.Validation;
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

            AddressTablePresenter = new AddressTablePresenter(this, AddressTable.GetInstance());
            AddressTableCache = new ListViewCache();
            AccessLock = new Object();
        }

        public void UpdateAddressTableItemCount(Int32 ItemCount)
        {
            // using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
                {
                    AddressTableListView.BeginUpdate();
                    AddressTableListView.SetItemCount(ItemCount);
                    AddressTableCache.FlushCache();
                    AddressTableListView.EndUpdate();
                });
            }
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            // using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
                {
                    AddressTableListView.BeginUpdate();
                    AddressTableListView.EndUpdate();
                });
            }
        }

        private void UpdateReadBounds()
        {
            // using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
                {
                    Tuple<Int32, Int32> ReadBounds = AddressTableListView.GetReadBounds();
                    AddressTablePresenter.UpdateReadBounds(ReadBounds.Item1, ReadBounds.Item2);
                });
            }
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
            // using (TimedLock.Lock(AccessLock))
            {
                if (E.Button == MouseButtons.Right)
                    LastRightClickLocation = E.Location;

                ListViewItem ListViewItem = AddressTableListView.GetItemAt(E.X, E.Y);

                if (ListViewItem == null)
                    return;

                if (E.X < (ListViewItem.Bounds.Left + 16))
                    AddressTablePresenter.SetAddressFrozen(ListViewItem.Index, !ListViewItem.Checked);  // (Has to be negated, click happens before check change)
            }
        }

        private void AddressTableListView_KeyPress(Object Sender, KeyPressEventArgs E)
        {
            // using (TimedLock.Lock(AccessLock))
            {
                if (E.KeyChar != ' ')
                    return;

                Boolean FreezeState = AddressTableListView.SelectedIndices == null ? false : !AddressTableListView.Items[AddressTableListView.SelectedIndices[0]].Checked;
                foreach (Int32 Index in AddressTableListView.SelectedIndices)
                    AddressTablePresenter.SetAddressFrozen(Index, FreezeState);
            }
        }

        private void ToggleFreezeToolStripMenuItem_Click(Object Sender, EventArgs E)
        {

        }

        private void AddressTableListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
        {
            ListViewItem SelectedItem;
            Int32 ColumnIndex;

            // using (TimedLock.Lock(AccessLock))
            {
                ListViewHitTestInfo HitTest = AddressTableListView.HitTest(E.Location);
                SelectedItem = HitTest.Item;
                ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

                // Do not bring up edit menu on double clicks to checkbox
                if (ColumnIndex == AddressTableListView.Columns.IndexOf(FrozenHeader))
                    return;

                if (SelectedItem == null)
                    return;
            }

            EditAddressTableEntry(SelectedItem.Index, ColumnIndex);
        }


        private void EditAddressEntryToolStripMenuItem_Click(Object Sender, EventArgs E)
        {

        }

        private void DeleteSelectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {

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
    }
}
/*
public partial class GUITable : DockContent, ITableView
{
    private TablePresenter TablePresenter;
    private Object AccessLock;

    private String Title;
    private String ActiveTablePath;

    public GUITable()
    {
        InitializeComponent();
        Title = this.Text;

        TablePresenter = new TablePresenter(this, Table.GetInstance());
        AccessLock = new Object();

        ActiveTablePath = String.Empty;

        ViewCheatTable();
    }

    private void ViewCheatTable()
    {
        using (TimedLock.Lock(AccessLock))
        {
            CheatTableButton.Checked = true;
            FSMTableButton.Checked = false;
        }
    }

    private void ViewFSMTable()
    {
        using (TimedLock.Lock(AccessLock))
        {
            CheatTableButton.Checked = false;
            FSMTableButton.Checked = true;
        }
    }

    public void UpdateHasChanges(Boolean HasChanges)
    {
        using (TimedLock.Lock(AccessLock))
        {
            ControlThreadingHelper.InvokeControlAction(this, () =>
            {
                this.Text = Title + " - " + ActiveTablePath;
                if (HasChanges)
                    this.Text += "*";
            });
        }
    }

    public void BeginSaveTable()
    {
        if (ActiveTablePath == String.Empty)
        {
            BeginSaveAsTable();
            return;
        }

        TablePresenter.SaveTable(ActiveTablePath);
    }

    public void BeginSaveAsTable()
    {
        SaveFileDialog SaveFileDialog = new SaveFileDialog();
        SaveFileDialog.Filter = "Anathema Table | *.ana";
        SaveFileDialog.Title = "Save Cheat Table";
        SaveFileDialog.ShowDialog();

        ActiveTablePath = SaveFileDialog.FileName;

        TablePresenter.SaveTable(SaveFileDialog.FileName);
    }

    public void BeginOpenTable()
    {
        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        OpenFileDialog.Filter = "Anathema Table | *.ana";
        OpenFileDialog.Title = "Open Cheat Table";
        OpenFileDialog.ShowDialog();

        ActiveTablePath = OpenFileDialog.FileName;

        TablePresenter.OpenTable(OpenFileDialog.FileName);
    }

    public void BeginMergeTable()
    {
        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        OpenFileDialog.Filter = "Anathema Table | *.ana";
        OpenFileDialog.Title = "Open and Merge Cheat Table";
        OpenFileDialog.ShowDialog();

        // Prioritize whatever is open already. If nothing, use the merge filename.
        if (ActiveTablePath == String.Empty)
            ActiveTablePath = OpenFileDialog.FileName;

        TablePresenter.MergeTable(OpenFileDialog.FileName);
    }

    private Boolean AskSaveChanges()
    {
        // Check if there are even changes to save
        if (!TablePresenter.HasChanges())
            return false;

        DialogResult Result = MessageBoxEx.Show(this, "This table has not been saved. Save the changes before closing?", "Save Changes?", MessageBoxButtons.YesNoCancel);

        switch (Result)
        {
            case DialogResult.Yes:
                BeginSaveTable();
                return false;
            case DialogResult.No:
                return false;
            case DialogResult.Cancel:
                break;
        }

        // User wishes to cancel
        return true;
    }

    #region Events

    private Point LastRightClickLocation = Point.Empty;

    private void SaveTableButton_Click(Object Sender, EventArgs E)
    {
        BeginSaveTable();
    }

    private void OpenTableButton_Click(Object Sender, EventArgs E)
    {
        BeginOpenTable();
    }

    private void OpenAndMergeTableButton_Click(Object Sender, EventArgs E)
    {
        BeginMergeTable();
    }

    private void CheatTableButton_Click(Object Sender, EventArgs E)
    {
        ViewCheatTable();
    }

    private void FSMTableButton_Click(Object Sender, EventArgs E)
    {
        ViewFSMTable();
    }

    private void AddAddressButton_Click(Object Sender, EventArgs E)
    {

    }

    private void GUITable_FormClosing(Object Sender, FormClosingEventArgs E)
    {
        if (AskSaveChanges())
            E.Cancel = true;
    }

    #endregion

} // End class

} // End namespace*/
