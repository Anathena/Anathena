using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using Gecko.Interop;

namespace Gecko
{
	partial class GeckoWebBrowser
	{
		#region nsIWebBrowserChrome

		void nsIWebBrowserChrome.SetStatus(uint statusType, string status)
		{
			Status = status;
		}

		nsIWebBrowser nsIWebBrowserChrome.GetWebBrowserAttribute()
		{
			return _webBrowser.Instance;
		}

		void nsIWebBrowserChrome.SetWebBrowserAttribute(nsIWebBrowser aWebBrowser)
		{

		}



		uint nsIWebBrowserChrome.GetChromeFlagsAttribute()
		{
			return _chromeFlags;
		}

		void nsIWebBrowserChrome.SetChromeFlagsAttribute(uint aChromeFlags)
		{
			_chromeFlags = aChromeFlags;
		}

		void nsIWebBrowserChrome.DestroyBrowserWindow()
		{

		}

		void nsIWebBrowserChrome.SizeBrowserTo(int aCX, int aCY)
		{

		}

		void nsIWebBrowserChrome.ShowAsModal()
		{

		}

		bool nsIWebBrowserChrome.IsWindowModal()
		{
			return false;
		}

		void nsIWebBrowserChrome.ExitModalEventLoop(int aStatus)
		{

		}
		#endregion

		#region nsISupportsWeakReference
		public nsIWeakReference GetWeakReference()
		{
			return new nsWeakReference(this);
		}
		#endregion

		#region nsIEmbeddingSiteWindow Members

		void nsIEmbeddingSiteWindow.SetDimensions(uint flags, int x, int y, int cx, int cy)
		{
			//const int DIM_FLAGS_POSITION = 1;
			//const int DIM_FLAGS_SIZE_INNER = 2;
			//const int DIM_FLAGS_SIZE_OUTER = 4;

			//BoundsSpecified specified = 0;
			//if ((flags & DIM_FLAGS_POSITION) != 0)
			//{
			//    specified |= BoundsSpecified.Location;
			//}
			//if ((flags & DIM_FLAGS_SIZE_INNER) != 0 || (flags & DIM_FLAGS_SIZE_OUTER) != 0)
			//{
			//    specified |= BoundsSpecified.Size;
			//}

			//OnWindowSetBounds(new GeckoWindowSetBoundsEventArgs(new Rectangle(x, y, cx, cy), specified));
		}

		unsafe void nsIEmbeddingSiteWindow.GetDimensions(uint flags, int* x, int* y, int* cx, int* cy)
		{
			double localX = (x != (void*)0) ? *x : 0;
			double localY = (y != (void*)0) ? *y : 0;
			double localCX = 0;
			double localCY = 0;

			if ((flags & nsIEmbeddingSiteWindowConstants.DIM_FLAGS_POSITION) != 0)
			{
				Point pt = PointToScreen(new Point());
				localX = pt.X;
				localY = pt.Y;
			}
			localCX = ActualWidth;
			localCY = ActualHeight;

			int iLocalCX = 0, iLocalCY = 0;
			_baseWindow.GetSize(ref iLocalCX, ref iLocalCY);
			localCX = iLocalCX;
			localCY = iLocalCY;

			if (x != (void*)0) *x = (int)localX;
			if (y != (void*)0) *y = (int)localY;
			if (cx != (void*)0) *cx = (int)localCX;
			if (cy != (void*)0) *cy = (int)localCY;
		}

		void nsIEmbeddingSiteWindow.SetFocus()
		{
			Focus();
			if (_baseWindow != null)
			{
				_baseWindow.SetFocus();
			}
		}

		bool nsIEmbeddingSiteWindow.GetVisibilityAttribute()
		{
			return Visibility == Visibility.Visible;
		}

		void nsIEmbeddingSiteWindow.SetVisibilityAttribute(bool aVisibility)
		{
			Visibility = aVisibility ? Visibility.Visible : Visibility.Hidden;
		}

		string nsIEmbeddingSiteWindow.GetTitleAttribute()
		{
			return DocumentTitle;
		}

		void nsIEmbeddingSiteWindow.SetTitleAttribute(string aTitle)
		{
			DocumentTitle = aTitle;
		}

		IntPtr nsIEmbeddingSiteWindow.GetSiteWindowAttribute()
		{
			return Handle;
		}

		void nsIEmbeddingSiteWindow.Blur()
		{
			// TODO: implement.
		}
		#endregion

		#region nsIInterfaceRequestor
		IntPtr nsIInterfaceRequestor.GetInterface(ref Guid uuid)
		{
			return Xpcom.WebBrowserGetInterface(this, _webBrowser.Instance, ref uuid );
		}
		#endregion
	}
}
