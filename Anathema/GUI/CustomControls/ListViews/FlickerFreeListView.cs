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

        public event ScrollEventHandler Scroll;
        protected virtual void OnScroll(ScrollEventArgs e)
        {
            ScrollEventHandler handler = this.Scroll;
            if (handler != null) handler(this, e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x115)
            {
                // Trap WM_VSCROLL
                OnScroll(new ScrollEventArgs((ScrollEventType)(m.WParam.ToInt32() & 0xffff), 0));
            }
        }

    } // End class

} // End namespace