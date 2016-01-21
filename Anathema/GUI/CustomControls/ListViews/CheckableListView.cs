using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anathema
{
    /// <summary>
    /// Virtual list views are not checkable by default, so we can fix that.
    /// NOTE: For some unknown reason, the listview items must not be passed from temporary variables, or unchecked boxes will not be drawn.
    /// Store displayed items in some sort of data structure (ie a list or dictionary) and cache them before passing.
    /// </summary>
    class CheckableListView : FlickerFreeListView
    {
        const Int32 CheckBoxSize = 16;

        public CheckableListView()
        {
            this.OwnerDraw = true;

            // Bind events
            this.DrawItem += new DrawListViewItemEventHandler(CheckableListView_DrawItem);
            this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(CheckableListView_DrawColumnHeader);
            this.MouseClick += new MouseEventHandler(CheckableListView_MouseClick);
            this.MouseDoubleClick += new MouseEventHandler(CheckableListView_MouseDoubleClick);
        }

        private void CheckableListView_DrawColumnHeader(Object Sender, DrawListViewColumnHeaderEventArgs E)
        {
            E.DrawDefault = true;
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

            if (ListViewItem == null)
                return;

            if (E.X < (ListViewItem.Bounds.Left + CheckBoxSize))
            {
                ListViewItem.Checked = !ListViewItem.Checked;
                this.Invalidate(ListViewItem.Bounds);
            }
        }

        void CheckableListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
        {
            ListViewItem ListViewItem = this.GetItemAt(E.X, E.Y);
            if (ListViewItem != null)
                this.Invalidate(ListViewItem.Bounds);
        }

    } // End class

} // End namespace