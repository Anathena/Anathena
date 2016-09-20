using Ana.Source.Utils.MVP;
using System;
using System.Windows.Forms;

namespace Ana.GUI.CustomControls.ListViews
{
    class FlickerFreeListView : ListView
    {
        private const Int32 WM_ERASEBKGND = 0x14;
        private const Int32 WM_HSCROLL = 0x114;
        private const Int32 WM_VSCROLL = 0x115;

        private const Int32 maximumItems = 100000000;

        public FlickerFreeListView()
        {
            // Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Enable the OnNotifyMessage event so we get a chance to filter out  Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        public void SetItemCount(Int32 itemCount)
        {
            this.VirtualListSize = Math.Min(maximumItems, itemCount);
        }

        public Tuple<Int32, Int32> GetReadBounds()
        {
            const Int32 boundsLimit = 128;
            const Int32 overRead = 48;

            Int32 startReadIndex = 0;
            Int32 endReadIndex = 0;

            ControlThreadingHelper.InvokeControlAction(this, () =>
            {
                startReadIndex = this.TopItem == null ? 0 : this.TopItem.Index;

                ListViewItem lastVisibleItem = this.TopItem;
                for (Int32 Index = startReadIndex; Index < this.Items.Count; Index++)
                {
                    if (Index - startReadIndex > boundsLimit)
                        break;

                    if (this.ClientRectangle.IntersectsWith(this.Items[Index].Bounds))
                        lastVisibleItem = this.Items[Index];
                    else
                        break;
                }

                startReadIndex -= overRead;
                endReadIndex = lastVisibleItem == null ? 0 : lastVisibleItem.Index + 1 + overRead;
            });

            return new Tuple<Int32, Int32>(startReadIndex, endReadIndex);
        }

        protected override void OnNotifyMessage(Message message)
        {
            // Filter out the WM_ERASEBKGND message
            if (message.Msg != WM_ERASEBKGND)
                base.OnNotifyMessage(message);
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
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            // Look for the WM_VSCROLL or the WM_HSCROLL messages.
            if ((message.Msg == WM_VSCROLL) || (message.Msg == WM_HSCROLL))
            {
                // Move focus to the ListView to cause ComboBox to lose focus.
                this.Focus();
            }

            // Pass message to default handler.
            base.WndProc(ref message);
        }

    } // End class

} // End namespace