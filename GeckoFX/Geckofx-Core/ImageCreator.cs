using System;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using Gecko.DOM;

namespace Gecko
{
	/// <summary>
	/// Creates Png images of webbrowser
	/// </summary>
	public class ImageCreator
	{
		IGeckoWebBrowser m_browser;

		public ImageCreator(IGeckoWebBrowser browser)
		{
			m_browser = browser;
		}

		[Obsolete("Use CanvasGetPngImage",false)]
		public Bitmap CanvasGetBitmap(uint width, uint height)
		{
			return CanvasGetBitmap(0, 0, width, height);
		}
		
		/// <summary>
		/// Get a bitmap of the current browsers Window.
		/// Works on Visible or InVisible windows.
		/// Not captures plugin (Flash,etc...) windows
		/// </summary>
		/// <param name="xOffset"></param>
		/// <param name="yOffset"></param>
		/// <param name="width">Width length of returned bitmap in pixels</param>
		/// <param name="height">Height length of returned bitmap in pixels</param>
		/// <returns></returns>
		/// <throws>ArgumentException if width or height is zero</throws>
		[Obsolete("Use CanvasGetPngImage", false)]
		public Bitmap CanvasGetBitmap(uint xOffset, uint yOffset, uint width, uint height)
		{
			var bytes = CanvasGetPngImage(xOffset, yOffset, width, height);
			return (Bitmap)Image.FromStream(new System.IO.MemoryStream(bytes));
		}

		/// <summary>
		/// Get byte array with png image of the current browsers Window.
		/// Wpf methods on windows platform don't use a Bitmap :-/
		/// Not captures plugin (Flash,etc...) windows
		/// </summary>
		/// <param name="xOffset"></param>
		/// <param name="yOffset"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public byte[] CanvasGetPngImage(uint xOffset, uint yOffset, uint width, uint height)
		{
			if (width == 0)
				throw new ArgumentException("width");

			if (height == 0)
				throw new ArgumentException("height");

			return Xpcom.ChromeContext.DrawWindow(m_browser.Window.DomWindow, xOffset, yOffset, width, height);
		}

		public byte[] CanvasGetPngImage(uint width, uint height)
		{
			return CanvasGetPngImage(0, 0, width, height);
		}

#if false // TODO move these method this into Geckofx-Winforms.
		public byte[] GraphicsGetBitmapPng(int width, int height)
		{
			return GraphicsGetBitmapPng(0, 0, width, height);
		}

		public byte[] GraphicsGetBitmapPng(int xOffset, int yOffset, int width, int height)
		{
			Bitmap bmp = new Bitmap(width, height);
			using (Graphics g = m_browser.CreateGraphics())
			{       
				m_browser.DrawToBitmap(bmp, new Rectangle(xOffset, yOffset, width, height));
			}
			byte[] ret = null;
			using (var m = new MemoryStream())
			{
				bmp.Save(m, ImageFormat.Png);
				ret = m.ToArray();
			}
			return ret;
		}
#endif
	}
}
