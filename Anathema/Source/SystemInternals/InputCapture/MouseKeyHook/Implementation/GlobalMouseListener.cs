using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.WinApi;
using System;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.Implementation
{
    internal class GlobalMouseListener : MouseListener
    {
        private readonly Int32 MouseSystemDoubleClickTime;
        private MouseButtons MousePreviousClicked;
        private Point MousePreviousClickedPosition;
        private Int32 MousePreviousClickedTime;

        public GlobalMouseListener() : base(HookHelper.HookGlobalMouse)
        {
            MouseSystemDoubleClickTime = MouseNativeMethods.GetDoubleClickTime();
        }

        protected override void ProcessDown(ref MouseEventExtArgs E)
        {
            if (IsDoubleClick(E))
                E = E.ToDoubleClickEventArgs();

            base.ProcessDown(ref E);
        }

        protected override void ProcessUp(ref MouseEventExtArgs E)
        {
            base.ProcessUp(ref E);

            if (E.Clicks == 2)
                StopDoubleClickWaiting();

            if (E.Clicks == 1)
                StartDoubleClickWaiting(E);
        }

        private void StartDoubleClickWaiting(MouseEventExtArgs E)
        {
            MousePreviousClicked = E.Button;
            MousePreviousClickedTime = E.Timestamp;
            MousePreviousClickedPosition = E.Point;
        }

        private void StopDoubleClickWaiting()
        {
            MousePreviousClicked = MouseButtons.None;
            MousePreviousClickedTime = 0;
            MousePreviousClickedPosition = new Point(0, 0);
        }

        private bool IsDoubleClick(MouseEventExtArgs E)
        {
            return E.Button == MousePreviousClicked
               && E.Point == MousePreviousClickedPosition // Click-move-click exception, see Patch 11222
               && E.Timestamp - MousePreviousClickedTime <= MouseSystemDoubleClickTime;
        }

        protected override MouseEventExtArgs GetEventArgs(CallbackData Data)
        {
            return MouseEventExtArgs.FromRawDataGlobal(Data);
        }

    } // End class

} // End namespace