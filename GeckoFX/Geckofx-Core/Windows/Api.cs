using System;
using System.Runtime.InteropServices;

namespace Gecko.Windows
{
	internal static class Gdi32
	{
		public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
		[DllImport("gdi32.dll")]
		public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
			int nWidth, int nHeight, IntPtr hObjectSource,
			int nXSrc, int nYSrc, int dwRop);
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
			int nHeight);
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
		[DllImport("gdi32.dll")]
		public static extern bool DeleteDC(IntPtr hDC);
		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);
		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
	}

	public static class User32
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;

			internal int width()
			{
				return right - left;
			}

			internal int height()
			{
				return bottom - top;
			}
		}
		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);
		[DllImport("user32.dll")]
		public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hdc);
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

		[DllImport( "user32.dll" )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern bool PrintWindow( IntPtr hWnd, IntPtr hdc, int nFlags );

		[DllImport("user32")]
		public static extern bool IsChild(IntPtr hWndParent, IntPtr hwnd);

		[DllImport( "user32" )]
		public static extern IntPtr GetParent(IntPtr hwnd);

		[DllImport("user32")]
		public static extern IntPtr GetFocus();
	}

	internal static class Kernel32
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetDllDirectory(string lpPathName);
	}
}
