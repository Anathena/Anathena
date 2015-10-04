using System;
using System.Runtime.InteropServices;

namespace Be.HexEditor
{
    // Win32 API for P/Invoke
    class W32
    {
        // Get handle to monitor that has the largest intersection with a specified window.
        [DllImport("User32.dll", SetLastError = true)]
        internal static extern IntPtr MonitorFromWindow(IntPtr hwnd,
                                                        int dwFlags);

        // Get handle to monitor that has the largest intersection with a specified rectangle.
        [DllImport("User32.dll", SetLastError = true)]
        internal static extern IntPtr MonitorFromRect([In] ref RECT lprc,
                                                      int dwFlags);

        // Get handle to monitor that contains a specified point.
        [DllImport("User32.dll", SetLastError = true)]
        internal static extern IntPtr MonitorFromPoint(POINT pt, int dwFlags);

        internal const int MONITORINFOF_PRIMARY = 0x00000001;
        internal const int MONITOR_DEFAULTTONEAREST = 0x00000002;
        internal const int MONITOR_DEFAULTTONULL = 0x00000000;
        internal const int MONITOR_DEFAULTTOPRIMARY = 0x00000001;

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            internal int x;
            internal int y;

            internal POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            internal int left;
            internal int top;
            internal int right;
            internal int bottom;
        }

        // Get DPI from handle to a specified monitor (Windows 8.1 or newer is required).
        [DllImport("Shcore.dll", SetLastError = true)]
        internal static extern int GetDpiForMonitor(IntPtr hmonitor,
                                                    Monitor_DPI_Type dpiType,
                                                    out uint dpiX,
                                                    out uint dpiY);

        internal enum Monitor_DPI_Type : int
        {
            MDT_Effective_DPI = 0,
            MDT_Angular_DPI = 1,
            MDT_Raw_DPI = 2,
            MDT_Default = MDT_Effective_DPI
        }

        // Equivalent to LOWORD macro
        internal static short GetLoWord(int dword)
        {
            return (short)(dword & 0xffff);
        }

        // Get device-specific information.
        [DllImport("Gdi32.dll", SetLastError = true)]
        internal static extern int GetDeviceCaps(IntPtr hdc,
                                                 int nIndex);

        internal const int LOGPIXELSX = 88;

        [DllImport("User32.dll", SetLastError = true)]
        internal static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReleaseDC(IntPtr hWnd,
                                              IntPtr hDC);

    }
}
