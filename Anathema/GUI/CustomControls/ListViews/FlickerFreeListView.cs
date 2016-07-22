using Anathema.Source.Utils.MVP;
using System;
using System.Windows.Forms;

namespace Anathema.GUI
{
    class FlickerFreeListView : ListView
    {
        private const Int32 WM_ERASEBKGND = 0x14;
        private const Int32 WM_HSCROLL = 0x114;
        private const Int32 WM_VSCROLL = 0x115;

        private const Int32 MaximumItems = 100000000;

        public FlickerFreeListView()
        {
            // Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Enable the OnNotifyMessage event so we get a chance to filter out  Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        public void SetItemCount(Int32 ItemCount)
        {
            this.VirtualListSize = Math.Min(MaximumItems, ItemCount);
        }

        public Tuple<Int32, Int32> GetReadBounds()
        {
            const Int32 BoundsLimit = 128;
            const Int32 OverRead = 48;

            Int32 StartReadIndex = 0;
            Int32 EndReadIndex = 0;

            ControlThreadingHelper.InvokeControlAction(this, () =>
            {
                StartReadIndex = this.TopItem == null ? 0 : this.TopItem.Index;

                ListViewItem LastVisibleItem = this.TopItem;
                for (Int32 Index = StartReadIndex; Index < this.Items.Count; Index++)
                {
                    if (Index - StartReadIndex > BoundsLimit)
                        break;

                    if (this.ClientRectangle.IntersectsWith(this.Items[Index].Bounds))
                        LastVisibleItem = this.Items[Index];
                    else
                        break;
                }

                StartReadIndex -= OverRead;
                EndReadIndex = LastVisibleItem == null ? 0 : LastVisibleItem.Index + 1 + OverRead;
            });

            return new Tuple<Int32, Int32>(StartReadIndex, EndReadIndex);
        }

        protected override void OnNotifyMessage(Message Message)
        {
            // Filter out the WM_ERASEBKGND message
            if (Message.Msg != WM_ERASEBKGND)
                base.OnNotifyMessage(Message);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.VirtualMode = true;
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Override to help with embedded combo box support
        /// </summary>
        /// <param name="Message"></param>
        protected override void WndProc(ref Message Message)
        {
            // Look for the WM_VSCROLL or the WM_HSCROLL messages.
            if ((Message.Msg == WM_VSCROLL) || (Message.Msg == WM_HSCROLL))
            {
                // Move focus to the ListView to cause ComboBox to lose focus.
                this.Focus();
            }

            // Pass message to default handler.
            base.WndProc(ref Message);
        }

    } // End class

} // End namespace