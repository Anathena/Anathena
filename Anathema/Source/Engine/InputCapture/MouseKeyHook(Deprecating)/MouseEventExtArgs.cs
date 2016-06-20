using Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook
{
    /// <summary>
    /// Provides extended data for the MouseClickExt and MouseMoveExt events.
    /// </summary>
    public class MouseEventExtArgs : MouseEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventExtArgs" /> class.
        /// </summary>
        /// <param name="Buttons">One of the MouseButtons values indicating which mouse button was pressed.</param>
        /// <param name="Clicks">The number of times a mouse button was pressed.</param>
        /// <param name="Point">The x and y coordinate of a mouse click, in pixels.</param>
        /// <param name="Delta">A signed count of the number of detents the wheel has rotated.</param>
        /// <param name="Timestamp">The system tick count when the event occurred.</param>
        /// <param name="isMouseButtonDown">True if event signals mouse button down.</param>
        /// <param name="isMouseButtonUp">True if event signals mouse button up.</param>
        internal MouseEventExtArgs(MouseButtons Buttons, Int32 Clicks, Point Point, Int32 Delta, Int32 Timestamp,
            Boolean isMouseButtonDown, Boolean isMouseButtonUp) : base(Buttons, Clicks, Point.X, Point.Y, Delta)
        {
            IsMouseButtonDown = isMouseButtonDown;
            IsMouseButtonUp = isMouseButtonUp;
            this.Timestamp = Timestamp;
        }

        /// <summary>
        /// Set this property to <b>true</b> inside your event handler to prevent further processing of the event in other applications.
        /// </summary>
        public Boolean Handled { get; set; }

        /// <summary>
        /// True if event contains information about wheel scroll.
        /// </summary>
        public Boolean WheelScrolled
        {
            get { return Delta != 0; }
        }

        /// <summary>
        /// True if event signals a click. False if it was only a move or wheel scroll.
        /// </summary>
        public Boolean Clicked
        {
            get { return Clicks > 0; }
        }

        /// <summary>
        /// True if event signals mouse button down.
        /// </summary>
        public Boolean IsMouseButtonDown { get; private set; }

        /// <summary>
        /// True if event signals mouse button up.
        /// </summary>
        public Boolean IsMouseButtonUp { get; private set; }

        /// <summary>
        /// The system tick count of when the event occurred.
        /// </summary>
        public Int32 Timestamp { get; private set; }

        /// <summary>
        /// </summary>
        internal Point Point
        {
            get { return new Point(X, Y); }
        }

        internal static MouseEventExtArgs FromRawDataApp(CallbackData Data)
        {
            IntPtr WParam = Data.WParam;
            IntPtr LParam = Data.LParam;

            AppMouseStruct MarshalledMouseStruct =
                (AppMouseStruct)Marshal.PtrToStructure(LParam, typeof(AppMouseStruct));

            return FromRawDataUniversal(WParam, MarshalledMouseStruct.ToMouseStruct());
        }

        internal static MouseEventExtArgs FromRawDataGlobal(CallbackData Data)
        {
            IntPtr WParam = Data.WParam;
            IntPtr LParam = Data.LParam;

            MouseStruct marshalledMouseStruct = (MouseStruct)Marshal.PtrToStructure(LParam, typeof(MouseStruct));

            return FromRawDataUniversal(WParam, marshalledMouseStruct);
        }

        /// <summary>
        /// Creates <see cref="MouseEventExtArgs" /> from relevant mouse data.
        /// </summary>
        /// <param name="WParam">First Windows Message parameter.</param>
        /// <param name="MouseInfo">A MouseStruct containing information from which to construct MouseEventExtArgs.</param>
        /// <returns>A new MouseEventExtArgs object.</returns>
        private static MouseEventExtArgs FromRawDataUniversal(IntPtr WParam, MouseStruct MouseInfo)
        {
            MouseButtons Button = MouseButtons.None;
            Int16 MouseDelta = 0;
            Int32 ClickCount = 0;

            Boolean IsMouseButtonDown = false;
            Boolean IsMouseButtonUp = false;


            switch ((Int64)WParam)
            {
                case Messages.WM_LBUTTONDOWN:
                    IsMouseButtonDown = true;
                    Button = MouseButtons.Left;
                    ClickCount = 1;
                    break;
                case Messages.WM_LBUTTONUP:
                    IsMouseButtonUp = true;
                    Button = MouseButtons.Left;
                    ClickCount = 1;
                    break;
                case Messages.WM_LBUTTONDBLCLK:
                    IsMouseButtonDown = true;
                    Button = MouseButtons.Left;
                    ClickCount = 2;
                    break;
                case Messages.WM_RBUTTONDOWN:
                    IsMouseButtonDown = true;
                    Button = MouseButtons.Right;
                    ClickCount = 1;
                    break;
                case Messages.WM_RBUTTONUP:
                    IsMouseButtonUp = true;
                    Button = MouseButtons.Right;
                    ClickCount = 1;
                    break;
                case Messages.WM_RBUTTONDBLCLK:
                    IsMouseButtonDown = true;
                    Button = MouseButtons.Right;
                    ClickCount = 2;
                    break;
                case Messages.WM_MBUTTONDOWN:
                    IsMouseButtonDown = true;
                    Button = MouseButtons.Middle;
                    ClickCount = 1;
                    break;
                case Messages.WM_MBUTTONUP:
                    IsMouseButtonUp = true;
                    Button = MouseButtons.Middle;
                    ClickCount = 1;
                    break;
                case Messages.WM_MBUTTONDBLCLK:
                    IsMouseButtonDown = true;
                    Button = MouseButtons.Middle;
                    ClickCount = 2;
                    break;
                case Messages.WM_MOUSEWHEEL:
                    MouseDelta = MouseInfo.MouseData;
                    break;
                case Messages.WM_XBUTTONDOWN:
                    Button = MouseInfo.MouseData == 1 ? MouseButtons.XButton1 : MouseButtons.XButton2;
                    IsMouseButtonDown = true;
                    ClickCount = 1;
                    break;

                case Messages.WM_XBUTTONUP:
                    Button = MouseInfo.MouseData == 1 ? MouseButtons.XButton1 : MouseButtons.XButton2;
                    IsMouseButtonUp = true;
                    ClickCount = 1;
                    break;

                case Messages.WM_XBUTTONDBLCLK:
                    IsMouseButtonDown = true;
                    Button = MouseInfo.MouseData == 1 ? MouseButtons.XButton1 : MouseButtons.XButton2;
                    ClickCount = 2;
                    break;

                case Messages.WM_MOUSEHWHEEL:
                    MouseDelta = MouseInfo.MouseData;
                    break;
            }

            MouseEventExtArgs Args = new MouseEventExtArgs(Button, ClickCount, MouseInfo.Point,
                MouseDelta, MouseInfo.Timestamp, IsMouseButtonDown, IsMouseButtonUp);

            return Args;
        }

        internal MouseEventExtArgs ToDoubleClickEventArgs()
        {
            return new MouseEventExtArgs(Button, 2, Point, Delta, Timestamp, IsMouseButtonDown, IsMouseButtonUp);
        }

    } // End class

} // End namespace