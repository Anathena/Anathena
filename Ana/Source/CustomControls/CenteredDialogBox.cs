namespace Ana.Source.CustomControls
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Interop;

    public class MessageBoxEx
    {
        private static IntPtr ownerPtr;

        private static HookProc hookProc;

        private static IntPtr hHook;

        public static MessageBoxResult Show(String text, String caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            MessageBoxEx.Initialize();
            return MessageBox.Show(text, caption, buttons, icon);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            ownerPtr = new WindowInteropHelper(owner).Handle;
            MessageBoxEx.Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon);
        }

        public delegate IntPtr HookProc(Int32 nCode, IntPtr wParam, IntPtr lParam);

        public delegate void TimerProc(IntPtr hWnd, UInt32 uMsg, UIntPtr nIDEvent, UInt32 dwTime);

        public const Int32 WH_CALLWNDPROCRET = 12;

        public enum CbtHookAction : Int32
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
        private static extern Boolean GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern Int32 MoveWindow(IntPtr hWnd, Int32 X, Int32 Y, Int32 nWidth, Int32 nHeight, Boolean bRepaint);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(Int32 idHook, HookProc lpfn, IntPtr hInstance, Int32 threadId);

        [DllImport("user32.dll")]
        public static extern Int32 UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr idHook, Int32 nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public UInt32 message;
            public IntPtr hwnd;
        };

        static MessageBoxEx()
        {
            hookProc = new HookProc(MessageBoxHookProc);
            hHook = IntPtr.Zero;
        }

        private static void Initialize()
        {
            if (hHook != IntPtr.Zero)
            {
                throw new NotSupportedException("Multiple calls are not supported.");
            }

            if (ownerPtr != null)
            {
                hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, hookProc, IntPtr.Zero, Thread.CurrentThread.ManagedThreadId);
            }
        }

        private static IntPtr MessageBoxHookProc(Int32 nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }

            CWPRETSTRUCT msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
            IntPtr hook = hHook;

            if (msg.message == (Int32)CbtHookAction.HCBT_ACTIVATE)
            {
                try
                {
                    CenterWindow(msg.hwnd);
                }
                finally
                {
                    UnhookWindowsHookEx(hHook);
                    hHook = IntPtr.Zero;
                }
            }

            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static void CenterWindow(IntPtr hChildWnd)
        {
            Rectangle recChild = new Rectangle(0, 0, 0, 0);
            Boolean success = GetWindowRect(hChildWnd, ref recChild);

            Int32 width = recChild.Width - recChild.X;
            Int32 height = recChild.Height - recChild.Y;

            Rectangle recParent = new Rectangle(0, 0, 0, 0);
            success = GetWindowRect(ownerPtr, ref recParent);

            System.Drawing.Point ptCenter = new System.Drawing.Point(0, 0);
            ptCenter.X = recParent.X + ((recParent.Width - recParent.X) / 2);
            ptCenter.Y = recParent.Y + ((recParent.Height - recParent.Y) / 2);


            System.Drawing.Point ptStart = new System.Drawing.Point(0, 0);
            ptStart.X = (ptCenter.X - (width / 2));
            ptStart.Y = (ptCenter.Y - (height / 2));

            ptStart.X = (ptStart.X < 0) ? 0 : ptStart.X;
            ptStart.Y = (ptStart.Y < 0) ? 0 : ptStart.Y;

            Int32 result = MoveWindow(hChildWnd, ptStart.X, ptStart.Y, width, height, false);
        }
    }
    //// End class
}
//// End namespace