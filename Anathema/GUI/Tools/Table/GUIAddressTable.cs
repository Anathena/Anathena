using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Anathema.Source.Tables.Addresses;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIAddressTable : DockContent, IAddressTableView
    {
        private AddressTablePresenter AddressTablePresenter;
        private Dictionary<AddressItem, AddressNode> Cache;
        private static Node DummyNode = new Node();
        private Object AccessLock;

        private TreeModel Model;

        public GUIAddressTable()
        {
            InitializeComponent();

            FrozenCheckBox.IsEditEnabledValueNeeded += CheckIndex;

            Cache = new Dictionary<AddressItem, AddressNode>();
            Model = new TreeModel();
            AccessLock = new Object();

            AddressTableTreeView.Model = Model;

            AddressTablePresenter = new AddressTablePresenter(this, AddressTable.GetInstance());
        }

        void CheckIndex(Object Sender, NodeControlValueEventArgs E)
        {
            E.Value = true;
        }

        public void UpdateAddressTableItemCount(Int32 ItemCount)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(AddressTableTreeView, () =>
                {
                    AddressTableTreeView.BeginUpdate();
                    Model.Nodes.Clear();
                    Cache.Clear();
                    for (Int32 Index = 0; Index < ItemCount; Index++)
                    {
                        AddressItem AddressItem = AddressTablePresenter.GetAddressItemAt(Index);
                        AddressNode AddressNode = new AddressNode(AddressItem.Description, AddressItem.GetAddressString(), AddressItem.ElementType?.Name, AddressItem.GetValueString());
                        Model.Nodes.Add(AddressNode);
                        Cache.Add(AddressItem, AddressNode);
                    }
                    AddressTableTreeView.EndUpdate();
                });
            }
        }

        public void ReadValues()
        {
            using (TimedLock.Lock(AccessLock))
            {
                // Update read bounds
                ControlThreadingHelper.InvokeControlAction(AddressTableTreeView, () =>
                {
                    Tuple<Int32, Int32> ReadBounds = new Tuple<Int32, Int32>(0, AddressTableTreeView.AllNodes.Count()); // AddressTableTreeView.GetReadBounds();
                    AddressTablePresenter.UpdateReadBounds(ReadBounds.Item1, ReadBounds.Item2);
                });

                ControlThreadingHelper.InvokeControlAction(AddressTableTreeView, () =>
                {
                    // Perform updates
                    AddressTableTreeView.BeginUpdate();
                    for (Int32 Index = 0; Index < AddressTablePresenter.GetAddressItemsCount(); Index++)
                    {
                        AddressItem AddressItem = AddressTablePresenter.GetAddressItemAt(Index);

                        // Update existing
                        if (Cache.ContainsKey(AddressItem))
                        {
                            Cache[AddressItem].EntryAddress = AddressItem.GetAddressString();
                            Cache[AddressItem].EntryValue = AddressItem.GetValueString();
                            Cache[AddressItem].IsChecked = AddressItem.GetActivationState();
                        }
                        // Otherwise create new
                        else
                        {
                            AddressNode AddressNode = new AddressNode(AddressItem.Description, AddressItem.GetAddressString(), AddressItem.ElementType?.Name, AddressItem.GetValueString());
                            Model.Nodes.Add(AddressNode);
                            Cache.Add(AddressItem, AddressNode);
                        }

                        Model.OnNodesChanged(new TreeModelEventArgs(Model.GetPath(Cache[AddressItem]), new Object[] { }));
                    }
                    AddressTableTreeView.EndUpdate();
                });
            }
        }

        private void EditAddressTableEntry(Int32 SelectedItemIndex, Int32 ColumnIndex)
        {
            GUIAddressTableEntryEditor GUIAddressTableEntryEditor;

            using (TimedLock.Lock(AccessLock))
            {
                List<Int32> Indicies = new List<Int32>();

                foreach (TreeNodeAdv Index in AddressTableTreeView.SelectedNodes)
                    Indicies.Add(Index.Index);

                if (Indicies.Count == 0)
                    return;

                // Determine the current column selection based on column index
                AddressTable.TableColumnEnum ColumnSelection = AddressTable.TableColumnEnum.Frozen;
                if (ColumnIndex == AddressTableTreeView.Columns.IndexOf(EntryDescriptionColumn))
                    ColumnSelection = AddressTable.TableColumnEnum.Description;
                else if (ColumnIndex == AddressTableTreeView.Columns.IndexOf(EntryAddressColumn))
                    ColumnSelection = AddressTable.TableColumnEnum.Address;
                else if (ColumnIndex == AddressTableTreeView.Columns.IndexOf(EntryTypeColumn))
                    ColumnSelection = AddressTable.TableColumnEnum.ValueType;
                else if (ColumnIndex == AddressTableTreeView.Columns.IndexOf(EntryValueColumn))
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
                if (AddressTableTreeView.SelectedNodes == null || AddressTableTreeView.SelectedNodes.Count <= 0)
                    return;

                List<Int32> Nodes = new List<Int32>();
                AddressTableTreeView.SelectedNodes.ForEach(X => Nodes.Add(X.Index));
                AddressTablePresenter.DeleteTableItems(Nodes);
            }
        }

        #region Events

        private void AddAddressButton_Click(Object Sender, EventArgs E)
        {
            AddNewAddressItem();
        }

        private Point LastRightClickLocation = Point.Empty;

        private void AddressTableListView_MouseClick(Object Sender, MouseEventArgs E)
        {
            if (E.Button == MouseButtons.Right)
                LastRightClickLocation = E.Location;

            TreeNodeAdv ListViewItem = AddressTableTreeView.GetNodeAt(E.Location);

            if (ListViewItem == null)
                return;

            // if (E.X < (ListViewItem.Bounds.Left + 16))
            //     AddressTablePresenter.SetAddressFrozen(ListViewItem.Index, !ListViewItem.Checked);  // (Has to be negated, click happens before check change)
        }

        private void AddressTableListView_KeyPress(Object Sender, KeyPressEventArgs E)
        {
            if (E.KeyChar != ' ')
                return;

            /*Boolean FreezeState = AddressTableListView.SelectedIndices == null ? false : !AddressTableListView.Items[AddressTableListView.SelectedIndices[0]].Checked;
            foreach (Int32 Index in AddressTableListView.SelectedIndices)
                AddressTablePresenter.SetAddressFrozen(Index, FreezeState);*/
        }

        private void ToggleFreezeToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            /*Boolean FreezeState = AddressTableListView.SelectedIndices == null ? false : !AddressTableListView.Items[AddressTableListView.SelectedIndices[0]].Checked;
            foreach (Int32 Index in AddressTableListView.SelectedIndices)
                AddressTablePresenter.SetAddressFrozen(Index, FreezeState);*/
        }

        private void AddressTableListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
        {
            /*
            ListViewItem SelectedItem;
            Int32 ColumnIndex;

            ListViewHitTestInfo HitTest = AddressTableTreeView.HitTest(E.Location);
            SelectedItem = HitTest.Item;
            ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            // Do not bring up edit menu on double clicks to checkbox
            if (ColumnIndex == AddressTableListView.Columns.IndexOf(FrozenHeader))
                return;

            if (SelectedItem == null)
                return;

            EditAddressTableEntry(SelectedItem.Index, ColumnIndex);
            */
        }

        private void EditAddressEntryToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            /*
                ListViewItem SelectedItem;
                Int32 ColumnIndex;

                ListViewHitTestInfo HitTest = AddressTableListView.HitTest(LastRightClickLocation);
                SelectedItem = HitTest.Item;
                ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

                if (SelectedItem == null)
                    return;

                EditAddressTableEntry(SelectedItem.Index, ColumnIndex);
            */
        }

        private void DeleteSelectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            /*
            ListViewItem SelectedItem;
            Int32 ColumnIndex;

            ListViewHitTestInfo HitTest = AddressTableListView.HitTest(LastRightClickLocation);
            SelectedItem = HitTest.Item;
            ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            if (SelectedItem == null)
                return;

            DeleteAddressTableEntries(SelectedItem.Index, SelectedItem.Index);

            AddressTableListView.SelectedIndices.Clear();
            */
        }

        private void AddNewAddressToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewAddressItem();
        }

        private void AddressTableContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
        {
            // using (TimedLock.Lock(AccessLock))
            {
                /*
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
                */
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
                /*
                ListViewHitTestInfo HitTest = AddressTableListView.HitTest(AddressTableListView.PointToClient(new Point(E.X, E.Y)));
                ListViewItem SelectedItem = HitTest.Item;

                if (DraggedItem == null || DraggedItem == SelectedItem)
                    return;

                if ((SelectedItem != null && SelectedItem.GetType() != typeof(ListViewItem)) || DraggedItem.GetType() != typeof(ListViewItem))
                    return;

                AddressTablePresenter.ReorderItem(DraggedItem.Index, SelectedItem == null ? AddressTableListView.Items.Count : SelectedItem.Index);
                */
            }
        }

        #endregion

    } // End class

} // End namespace