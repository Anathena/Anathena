using System;
using System.Drawing;
using System.Windows.Forms;
using Anathema.Utils.MVP;

namespace Anathema
{
    public partial class GUIScriptTable : UserControl, IScriptTableView
    {
        private ScriptTablePresenter ScriptTablePresenter;

        public GUIScriptTable()
        {
            InitializeComponent();

            ScriptTablePresenter = new ScriptTablePresenter(this, ScriptTable.GetInstance());
        }

        public void UpdateScriptTableItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptTableListView, () =>
            {
                ScriptTableListView.BeginUpdate();
                ScriptTableListView.SetItemCount(ItemCount);
                ScriptTableListView.EndUpdate();
            });
        }

        #region Events

        private Point LastRightClickLocation = Point.Empty;

        private void ScriptTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = ScriptTablePresenter.GetScriptTableItemAt(E.ItemIndex);
        }

        private void ScriptTableListView_MouseClick(Object Sender, MouseEventArgs E)
        {
            if (E.Button == MouseButtons.Right)
                LastRightClickLocation = E.Location;

            ListViewItem ListViewItem = ScriptTableListView.GetItemAt(E.X, E.Y);

            if (ListViewItem == null)
                return;

            if (E.X < (ListViewItem.Bounds.Left + 16))
                ScriptTablePresenter.SetScriptActivation(ListViewItem.Index, !ListViewItem.Checked); // (Has to be negated, click happens before check change)
        }

        private void ScriptTableListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
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
            ListViewHitTestInfo HitTest = ScriptTableListView.HitTest(ScriptTableListView.PointToClient(new Point(E.X, E.Y)));
            ListViewItem SelectedItem = HitTest.Item;

            if (DraggedItem == null || DraggedItem == SelectedItem)
                return;

            if ((SelectedItem != null && SelectedItem.GetType() != typeof(ListViewItem)) || DraggedItem.GetType() != typeof(ListViewItem))
                return;

            ScriptTablePresenter.ReorderScript(DraggedItem.Index, SelectedItem == null ? ScriptTableListView.Items.Count : SelectedItem.Index);
        }

        #endregion

    } // End class

} // End namespace