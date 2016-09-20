using System.Windows.Forms;

namespace Ana.GUI.CustomControls.Panels
{
    class FlickerFreePanel : Panel
    {
        public FlickerFreePanel()
        {
            // Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Enable the OnNotifyMessage event so we get a chance to filter out  Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message message)
        {
            // Filter out the WM_ERASEBKGND message
            if (message.Msg != 0x14)
                base.OnNotifyMessage(message);
        }

    } // End class

} // End namespace