using System.Diagnostics;

namespace Gecko
{

	// Implement nsIXULWindow interface on GeckoWebBrowser, so that nsIWindowMediator can find GeckoWebBrowser.
	// It seems that only GetDocShellAttribute() is required by nsIWindowMediator, so we leave other method not implemented.
	// 
	// It was a bad idea to throw NotImplementedException, because this methods are called in XUL specific urls like about:config
	public partial class GeckoWebBrowser : nsIXULWindow
	{

		public nsIDocShell GetDocShellAttribute()
		{
			return Xpcom.QueryInterface<nsIDocShell>(this.WebBrowser);
		}

		public bool GetIntrinsicallySizedAttribute()
		{
			Debug.WriteLine("GetIntrinsicallySizedAttribute called");
			return false;
		}

		public void SetIntrinsicallySizedAttribute(bool aIntrinsicallySized)
		{
			Debug.WriteLine("SetIntrinsicallySizedAttribute called");
		}

		public nsIDocShellTreeItem GetPrimaryContentShellAttribute()
		{
			Debug.WriteLine("GetPrimaryContentShellAttribute called");
			return null;
		}

		public nsIDocShellTreeItem GetContentShellById(string ID)
		{
			Debug.WriteLine("GetContentShellById called");
			return null;
		}

		public void AddChildWindow(nsIXULWindow aChild)
		{
			Debug.WriteLine("AddChildWindow called");
		}

		public void RemoveChildWindow(nsIXULWindow aChild)
		{
			Debug.WriteLine("RemoveChildWindow called");
		}

		public void Center(nsIXULWindow aRelative, bool aScreen, bool aAlert)
		{
			Debug.WriteLine("Center called");
		}

		public void ShowModal()
		{
			Debug.WriteLine("ShowModal called");
		}

		public uint GetZLevelAttribute()
		{
			Debug.WriteLine("GetZLevelAttribute called");
			return 0;
		}

		public void SetZLevelAttribute(uint aZLevel)
		{
			Debug.WriteLine("SetZLevelAttribute called");
		}

		public uint GetContextFlagsAttribute()
		{
			Debug.WriteLine("GetContextFlagsAttribute called");
			return 0;
		}

		public void SetContextFlagsAttribute(uint aContextFlags)
		{
			Debug.WriteLine("SetContextFlagsAttribute called");
		}

		uint nsIXULWindow.GetChromeFlagsAttribute()
		{
			Debug.WriteLine("GetChromeFlagsAttribute called");
			return ChromeFlags;
		}

		void nsIXULWindow.SetChromeFlagsAttribute(uint aChromeFlags)
		{
			Debug.WriteLine("SetChromeFlagsAttribute called");
			ChromeFlags = aChromeFlags;
		}

		public void AssumeChromeFlagsAreFrozen()
		{
			Debug.WriteLine("AssumeChromeFlagsAreFrozen called");
		}

		public nsIXULWindow CreateNewWindow(int aChromeFlags)
		{
			Debug.WriteLine("CreateNewWindow called");
			return null;
		}

		public nsIXULBrowserWindow GetXULBrowserWindowAttribute()
		{
			Debug.WriteLine("GetXULBrowserWindowAttribute called");
			return null;
		}

		public void SetXULBrowserWindowAttribute(nsIXULBrowserWindow aXULBrowserWindow)
		{
			Debug.WriteLine("SetXULBrowserWindowAttribute called");
		}

		public void ApplyChromeFlags()
		{
			// this method is called in about:config !!!
			Debug.WriteLine("ApplyChromeFlags called");
		}
	}
}