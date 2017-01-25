namespace Ana.Source.CustomControls
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// A class for displaying a dialog box centered to the calling parent.
    /// </summary>
    internal class CenteredDialogBox
    {
        public const Int32 WH_CALLWNDPROCRET = 12;

        private static IntPtr ownerPtr;

        private static HookProc hookProc;

        private static IntPtr windowHook;

        /// <summary>
        /// Shows a dialog box with the specified parameters.
        /// </summary>
        /// <param name="text">The body text.</param>
        /// <param name="caption">The window caption.</param>
        /// <param name="buttons">The buttons choices to display</param>
        /// <param name="icon">The icon to display.</param>
        /// <returns>The result based on the button pressed.</returns>
        public static MessageBoxResult Show(String text, String caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            CenteredDialogBox.Initialize();
            return MessageBox.Show(text, caption, buttons, icon);
        }

        /// <summary>
        /// Shows a dialog box with the specified parameters.
        /// </summary>
        /// <param name="owner">The creator of this messagebox, on which we will center this.</param>
        /// <param name="text">The body text.</param>
        /// <param name="caption">The window caption.</param>
        /// <param name="buttons">The buttons choices to display</param>
        /// <param name="icon">The icon to display.</param>
        /// <returns>The result based on the button pressed.</returns>
        public static MessageBoxResult Show(Window owner, String text, String caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            ownerPtr = new WindowInteropHelper(owner).Handle;
            CenteredDialogBox.Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon);
        }

        public delegate IntPtr HookProc(Int32 nCode, IntPtr wParam, IntPtr lParam);

        public delegate void TimerProc(IntPtr hWnd, UInt32 uMsg, UIntPtr nIDEvent, UInt32 dwTime);

        private enum CbtHookAction : Int32
        {
            HCBT_MOVESIZE = 0,

            HCBT_MINMAX = 1,

            HCBT_QS = 2,

            HCBT_CREATEWND = 3,

            HCBT_DESTROYWND = 4,

            HCBT_ACTIVATE = 5,

            HCBT_CLICKSKIPPED = 6,

            HCBT_KEYSKIPPED = 7,

            HCBT_SYSCOMMAND = 8,

            HCBT_SETFOCUS = 9
        }

        [DllImport("user32.dll")]
        public static extern Int32 UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr idHook, Int32 nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern Boolean GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern Int32 MoveWindow(IntPtr hWnd, Int32 X, Int32 Y, Int32 nWidth, Int32 nHeight, Boolean bRepaint);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(Int32 idHook, HookProc lpfn, IntPtr hInstance, Int32 threadId);

        static CenteredDialogBox()
        {
            hookProc = new HookProc(MessageBoxHookProc);
            windowHook = IntPtr.Zero;
        }

        private static void Initialize()
        {
            if (windowHook != IntPtr.Zero)
            {
                throw new NotSupportedException("Multiple calls are not supported.");
            }

            if (ownerPtr != null)
            {
                windowHook = CenteredDialogBox.SetWindowsHookEx(WH_CALLWNDPROCRET, hookProc, IntPtr.Zero, Thread.CurrentThread.ManagedThreadId);
            }
        }

        private static IntPtr MessageBoxHookProc(Int32 nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CenteredDialogBox.CallNextHookEx(windowHook, nCode, wParam, lParam);
            }

            CWPRETSTRUCT msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
            IntPtr hook = windowHook;

            if (msg.Message == (Int32)CbtHookAction.HCBT_ACTIVATE)
            {
                try
                {
                    CenteredDialogBox.CenterWindow(msg.Hwnd);
                }
                finally
                {
                    CenteredDialogBox.UnhookWindowsHookEx(windowHook);
                    windowHook = IntPtr.Zero;
                }
            }

            return CenteredDialogBox.CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static void CenterWindow(IntPtr childWindowHandle)
        {
            Rectangle recChild = new Rectangle(0, 0, 0, 0);
            Boolean success = GetWindowRect(childWindowHandle, ref recChild);

            Int32 width = recChild.Width - recChild.X;
            Int32 height = recChild.Height - recChild.Y;

            Rectangle parentRectangle = new Rectangle(0, 0, 0, 0);
            success = CenteredDialogBox.GetWindowRect(ownerPtr, ref parentRectangle);

            System.Drawing.Point centerPoint = new System.Drawing.Point(0, 0);
            centerPoint.X = parentRectangle.X + ((parentRectangle.Width - parentRectangle.X) / 2);
            centerPoint.Y = parentRectangle.Y + ((parentRectangle.Height - parentRectangle.Y) / 2);

            System.Drawing.Point startPoint = new System.Drawing.Point(0, 0);
            startPoint.X = centerPoint.X - (width / 2);
            startPoint.Y = centerPoint.Y - (height / 2);

            startPoint.X = (startPoint.X < 0) ? 0 : startPoint.X;
            startPoint.Y = (startPoint.Y < 0) ? 0 : startPoint.Y;

            Int32 result = CenteredDialogBox.MoveWindow(childWindowHandle, startPoint.X, startPoint.Y, width, height, false);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CWPRETSTRUCT
        {
            public IntPtr LResult;

            public IntPtr LParam;

            public IntPtr WParam;

            public UInt32 Message;

            public IntPtr Hwnd;
        }
    }
    //// End class
}
//// End namespace