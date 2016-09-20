using System;
using System.Windows.Forms;

namespace Ana.GUI.CustomControls.ListViews
{
    /// <summary>
    /// Virtual list views are not checkable by default, so we can fix that.
    /// NOTE: For some unknown reason, the listview items must not be passed from temporary variables, or unchecked boxes will not be drawn.
    /// Store displayed items in some sort of data structure (ie a list or dictionary) and cache them before passing.
    /// </summary>
    class CheckableListView : FlickerFreeListView
    {
        const Int32 checkBoxSize = 16;

        public CheckableListView()
        {
            this.OwnerDraw = true;

            // Bind events
            this.DrawItem += new DrawListViewItemEventHandler(CheckableListView_DrawItem);
            this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(CheckableListView_DrawColumnHeader);
            this.MouseClick += new MouseEventHandler(CheckableListView_MouseClick);
            this.MouseDoubleClick += new MouseEventHandler(CheckableListView_MouseDoubleClick);
        }

        private void CheckableListView_DrawColumnHeader(Object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        void CheckableListView_DrawItem(Object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;

            if (!e.Item.Checked)
            {
                e.Item.Checked = true;
                e.Item.Checked = false;
            }
        }

        void CheckableListView_MouseClick(Object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = this.GetItemAt(e.X, e.Y);

            if (listViewItem == null)
                return;

            if (e.X < (listViewItem.Bounds.Left + checkBoxSize))
            {
                listViewItem.Checked = !listViewItem.Checked;
                this.Invalidate(listViewItem.Bounds);
            }
        }

        void CheckableListView_MouseDoubleClick(Object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = this.GetItemAt(e.X, e.Y);
            if (listViewItem != null)
                this.Invalidate(listViewItem.Bounds);
        }

    } // End class

} // End namespace