using System;
using System.Windows.Forms;

namespace Anathema
{
    /// <summary>
    /// Virtual list views are not checkable by default, so we can fix that.
    /// </summary>
    class CheckableListView : FlickerFreeListView
    {
        public CheckableListView() : base()
        {
            this.DrawItem += new DrawListViewItemEventHandler(CheckableListView_DrawItem);

            // Redraw when checked or doubleclicked
            this.MouseClick += new MouseEventHandler(CheckableListView_MouseClick);
            this.MouseDoubleClick += new MouseEventHandler(CheckableListView_MouseDoubleClick);
        }
        
        void CheckableListView_DrawItem(Object Sender, DrawListViewItemEventArgs E)
        {
            E.DrawDefault = true;
            if (!E.Item.Checked)
            {
                E.Item.Checked = true;
                E.Item.Checked = false;
            }
        }

        void CheckableListView_MouseClick(Object Sender, MouseEventArgs E)
        {
            ListViewItem ListViewItem = this.GetItemAt(E.X, E.Y);
            if (ListViewItem != null)
            {
                if (E.X < (ListViewItem.Bounds.Left + 16))
                {
                    ListViewItem.Checked = !ListViewItem.Checked;
                    this.Invalidate(ListViewItem.Bounds);
                }
            }
        }

        void CheckableListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
        {
            ListViewItem ListViewItem = this.GetItemAt(E.X, E.Y);
            if (ListViewItem != null)
                this.Invalidate(ListViewItem.Bounds);
        }
    }
}
