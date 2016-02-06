using System;
using System.Windows.Forms;

namespace Anathema
{
    class FlickerFreeListView : ListView
    {
        public FlickerFreeListView()
        {
            // Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Enable the OnNotifyMessage event so we get a chance to filter out  Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
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
                    if (Index - this.TopItem.Index > BoundsLimit)
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
            if (Message.Msg != 0x14)
            {
                base.OnNotifyMessage(Message);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.VirtualMode = true;
            this.ResumeLayout(false);
        }

    } // End class

} // End namespace