using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gecko.Windows;

namespace Gecko.Utils
{
#if GTK
#else
	public static class WindowsImageCreator
	{
		private static byte[] SaveBmpAsPng(Bitmap bmp)
		{
			byte[] ret = null;
			// Saving to stream
			using (var m = new MemoryStream(500))
			{
				bmp.Save(m, ImageFormat.Png);
				ret = m.ToArray();
			}
			return ret;
		}

		private static byte[] HbitmapToPng(IntPtr hBitmap)
		{
			byte[] ret = null;
			// calling  Image.FromHbitmap cause error when hBitmap is not exists
			try
			{
				// get a .NET image object for it
				using (Bitmap img = Image.FromHbitmap(hBitmap))
				{
					ret = SaveBmpAsPng( img );
				}
			}
			catch (Exception)
			{

			}
			return ret;
		}

		/// <summary>
		/// Windows only window capture by handle
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		public static byte[] WindowsCapturePng(IntPtr handle)
		{
			// get te hDC of the target window
			IntPtr hdcSrc = User32.GetWindowDC(handle);
			// get the size
			User32.RECT windowRect = new User32.RECT();
			User32.GetWindowRect(handle, ref windowRect);
			int width = windowRect.right - windowRect.left;
			int height = windowRect.bottom - windowRect.top;
			// create a device context we can copy to
			IntPtr hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);
			// create a bitmap we can copy it to,
			// using GetDeviceCaps to get the width/height
			IntPtr hBitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);
			// select the bitmap object
			IntPtr hOld = Gdi32.SelectObject(hdcDest, hBitmap);
			// bitblt over
			Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Gdi32.SRCCOPY);
			// restore selection
			Gdi32.SelectObject(hdcDest, hOld);
			// clean up 
			Gdi32.DeleteDC(hdcDest);
			User32.ReleaseDC(handle, hdcSrc);
			byte[] ret = HbitmapToPng( hBitmap );
			// free up the Bitmap object
			Gdi32.DeleteObject( hBitmap );
			return ret;
		}



		/// <summary>
		/// Capture whole WebBrowser window (Only on MS Windows Platform)
		/// </summary>
		/// <returns></returns>
		public static byte[] CapturePng(this GeckoWebBrowser browser)
		{
			return WindowsCapturePng(browser.Handle);
		}


		/// <summary>
		/// Return a Bitmap Image of the current WebBrowsers Rendered page.
		/// Not supported on Linux - use OffScreenGeckoWebBrowser.
		/// </summary>
		/// <param name="width">Width of the bimap</param>
		/// <param name="height">Height of the bitmap</param>
		/// <returns></returns>
		public static Bitmap GetBitmap(this GeckoWebBrowser browser,uint width, uint height)
		{
			return GetBitmap(browser,0, 0, width, height);
		}

		/// <summary>
		/// Return a Bitmap Image of the current WebBrowsers Rendered page.
		/// </summary>
		/// <param name="xOffset"></param>
		/// <param name="yOffset"></param>
		/// <param name="width">Width of the bitmap</param>
		/// <param name="height">Height of the bitmap</param>
		/// <returns></returns>
		public static Bitmap GetBitmap(this GeckoWebBrowser browser, uint xOffset, uint yOffset, uint width, uint height)
		{
			return new ImageCreator(browser).CanvasGetBitmap(xOffset, yOffset, width, height);
		}
	}

#endif
}
