#if GTK
using System.Drawing;
using System;

namespace Gecko
{
	/// <summary>
	/// Used for offscreen capture of a web page.
	/// </summary>
	public class OffScreenGeckoWebBrowser : GeckoWebBrowser
	{		
		public OffScreenGeckoWebBrowser()
		{
			if (!Xpcom.IsLinux)
				throw new NotImplementedException("Not currently implemented on Windows");
		
			// Create a special form of the GtkWrapper which doesn't reparent Gtk.Window into
			// a winform Control.
			m_wrapper = new GtkDotNet.GtkWrapperNoThread(new Gtk.Window(Gtk.WindowType.Popup));
		}

        protected override void OnHandleCreated(EventArgs e)
        {
            Xpcom.InitChromeContext();

            base.OnHandleCreated(e);
        }
		
		/// <summary>
		/// Get a bitmap image, of given size, for the current web page. 
		/// </summary>		
		public Bitmap GetBitmap(int width, int height)
		{
            ImageCreator creator = new ImageCreator(this);
            byte[] mBytes = creator.CanvasGetPngImage((uint)0, (uint)0, (uint)this.Width, (uint)this.Height);
            return (Bitmap)Bitmap.FromStream(new System.IO.MemoryStream(mBytes));
		}
	}
}
#endif