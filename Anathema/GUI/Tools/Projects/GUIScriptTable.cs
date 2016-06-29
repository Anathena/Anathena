using Anathema.Source.Project.Deprecating;
using Anathema.Source.Project.ProjectItems;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Caches;
using Anathema.Source.Utils.MVP;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema
{
    public partial class GUIScriptTable : DockContent, IScriptTableView
    {
        private ScriptTablePresenter ScriptTablePresenter;
        private ListViewCache ScriptTableCache;
        private Object AccessLock;

        public GUIScriptTable()
        {
            InitializeComponent();

            ScriptTableCache = new ListViewCache();
            AccessLock = new Object();

            ScriptTablePresenter = new ScriptTablePresenter(this, ScriptTable.GetInstance());
        }

        public void UpdateScriptTableItemCount(Int32 ItemCount)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ScriptTableListView, () =>
                {
                    ScriptTableListView.BeginUpdate();
                    ScriptTableListView.SetItemCount(ItemCount);
                    ScriptTableCache.FlushCache();
                    ScriptTableListView.EndUpdate();
                });
            }
        }

        #region Events

        private Point LastRightClickLocation = Point.Empty;

        private void ScriptTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            ListViewItem Item = ScriptTableCache.Get((UInt64)E.ItemIndex);
            ScriptItem ScriptItem = ScriptTablePresenter.GetScriptTableItemAt(E.ItemIndex);

            // Try to update and return the item if it is a valid item
            if (Item != null)
            {
                Item.Checked = ScriptItem.GetActivationState();
                E.Item = Item;
                return;
            }

            // Add the properties to the manager and get the list view item back
            Item = ScriptTableCache.Add(E.ItemIndex, new String[ScriptTableListView.Columns.Count]);

            Item.SubItems[ScriptTableListView.Columns.IndexOf(ScriptActiveHeader)].Text = String.Empty;
            Item.SubItems[ScriptTableListView.Columns.IndexOf(ScriptDescriptionHeader)].Text = ScriptItem.GetDescription();
            Item.Checked = ScriptItem.GetActivationState();

            E.Item = Item;
        }

        private void AddScriptButton_Click(Object Sender, EventArgs E)
        {
            ScriptTablePresenter.AddNewScript();
        }

        private void ScriptTableListView_MouseClick(Object Sender, MouseEventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (E.Button == MouseButtons.Right)
                    LastRightClickLocation = E.Location;

                ListViewItem ListViewItem = ScriptTableListView.GetItemAt(E.X, E.Y);

                if (ListViewItem == null)
                    return;

                if (E.X < (ListViewItem.Bounds.Left + 16))
                    ScriptTablePresenter.SetScriptActivation(ListViewItem.Index, !ListViewItem.Checked); // (Has to be negated, click happens before check change)
            }
        }

        private void ScriptTableListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ListViewHitTestInfo HitTest = ScriptTableListView.HitTest(E.Location);
                ListViewItem SelectedItem = HitTest.Item;
                Int32 ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

                // Do not bring up edit menu on double clicks to checkbox
                if (ColumnIndex == ScriptTableListView.Columns.IndexOf(ScriptActiveHeader))
                    return;

                if (SelectedItem == null)
                    return;

                ScriptTablePresenter.OpenScript(SelectedItem.Index);
            }
        }
        private void NewScriptToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ScriptTablePresenter.AddNewScript();
        }

        private void OpenScriptToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ListViewHitTestInfo HitTest = ScriptTableListView.HitTest(LastRightClickLocation);
            ListViewItem SelectedItem = HitTest.Item;
            Int32 ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            if (SelectedItem == null)
                return;

            ScriptTablePresenter.OpenScript(SelectedItem.Index);
        }

        private void DeleteScriptToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ListViewHitTestInfo HitTest = ScriptTableListView.HitTest(LastRightClickLocation);
            ListViewItem SelectedItem = HitTest.Item;
            Int32 ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            if (SelectedItem == null)
                return;

            ScriptTablePresenter.DeleteScript(SelectedItem.Index);
        }

        private void ScriptTableContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
        {
            ListViewHitTestInfo HitTest = ScriptTableListView.HitTest(ScriptTableListView.PointToClient(MousePosition));
            ListViewItem SelectedItem = HitTest.Item;

            if (SelectedItem == null)
                E.Cancel = true;
        }

        private ListViewItem DraggedItem;
        private void ScriptTableListView_ItemDrag(Object Sender, ItemDragEventArgs E)
        {
            DraggedItem = (ListViewItem)E.Item;
            DoDragDrop(E.Item, DragDropEffects.All);
        }

        private void ScriptTableListView_DragOver(Object Sender, DragEventArgs E)
        {
            E.Effect = DragDropEffects.All;
        }

        private void ScriptTableListView_DragDrop(Object Sender, DragEventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ListViewHitTestInfo HitTest = ScriptTableListView.HitTest(ScriptTableListView.PointToClient(new Point(E.X, E.Y)));
                ListViewItem SelectedItem = HitTest.Item;

                if (DraggedItem == null || DraggedItem == SelectedItem)
                    return;

                if ((SelectedItem != null && SelectedItem.GetType() != typeof(ListViewItem)) || DraggedItem.GetType() != typeof(ListViewItem))
                    return;

                ScriptTablePresenter.ReorderScript(DraggedItem.Index, SelectedItem == null ? ScriptTableListView.Items.Count : SelectedItem.Index);
            }
        }

        #endregion

    } // End class

} // End namespace