using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace Gecko
{
	static class nsIHelperAppLauncherConstants
	{
		public const int NS_BINDING_ABORTED = unchecked((int)0x804b0002);
	}

	public class LauncherDialogEvent : EventArgs
	{
		public string Url
		{
			get
			{
				nsIURI uri = HelperAppLauncher.GetSourceAttribute();
				return nsString.Get(uri.GetSpecAttribute);
			}
		}
		public string Filename
		{
			get
			{
				nsAString o = new nsAString();
				HelperAppLauncher.GetSuggestedFileNameAttribute(o);
				return o.ToString();
			}
		}
		public nsIMIMEInfo Mime
		{
			get
			{
				return HelperAppLauncher.GetMIMEInfoAttribute();
			}
		}

		public void Cancel()
		{
			HelperAppLauncher.Cancel(nsIHelperAppLauncherConstants.NS_BINDING_ABORTED);
		}
		public void Navigate(string Url)
		{
			WebNavigation.LoadURI(Url, 0, null, null, null);
		}

		/// <summary>
		/// Gets the nsIWebNavigation object. May be null for popup windows.
		/// </summary>
		public nsIWebNavigation WebNavigation { get; private set; }
		public nsIHelperAppLauncher HelperAppLauncher { get; private set; }

		public LauncherDialogEvent(nsIHelperAppLauncher aLauncher, nsISupports aWindowContext, uint aReason)
		{
			HelperAppLauncher = aLauncher;
			WebNavigation = aWindowContext as nsIWebNavigation;
		}
	}

	public class LauncherDialog : nsIHelperAppLauncherDialog
	{
		public static event EventHandler<LauncherDialogEvent> Download;

		[DllImport("xul", CharSet = CharSet.Ansi)]
		private static extern int NS_NewNativeLocalFile(nsACString path, bool followLinks, [MarshalAs(UnmanagedType.IUnknown)] out object result);
		public nsILocalFile PromptForSaveToFile(nsIHelperAppLauncher aLauncher, nsISupports aWindowContext, string aDefaultFileName, string aSuggestedFileExtension, bool aForcePrompt)
		{
			aLauncher.Cancel(nsIHelperAppLauncherConstants.NS_BINDING_ABORTED);
			return null;
		}

		public void PromptForSaveToFileAsync(nsIHelperAppLauncher aLauncher, nsISupports aWindowContext, string aDefaultFileName,
			string aSuggestedFileExtension, bool aForcePrompt)
		{
			throw new NotImplementedException();
		}

		public void Show(nsIHelperAppLauncher aLauncher, nsISupports aWindowContext, uint aReason)
		{
			if (Download != null)
			{
				Download(this, new LauncherDialogEvent(aLauncher, aWindowContext, aReason));
			}
			else
			{
				aLauncher.Cancel(nsIHelperAppLauncherConstants.NS_BINDING_ABORTED);
			}
		}

		#region nsIHelperAppLauncherDialog Members


		nsIFile nsIHelperAppLauncherDialog.PromptForSaveToFile(nsIHelperAppLauncher aLauncher, nsISupports aWindowContext, string aDefaultFileName, string aSuggestedFileExtension, bool aForcePrompt)
		{
			throw new NotImplementedException();
		}

		#endregion
	}

	[ContractID(LauncherDialogFactory.ContractID)]
	internal class LauncherDialogFactory
		: GenericOneClassNsFactory<LauncherDialogFactory, LauncherDialog>
	{
		//IntPtr nsIFactory.CreateInstance(nsISupports aOuter, ref Guid iid)
		//{
		//    IntPtr result = IntPtr.Zero;
		//    IntPtr iUnknownForObject = Marshal.GetIUnknownForObject(new LauncherDialog());
		//    Marshal.QueryInterface(iUnknownForObject, ref iid, out result);
		//    Marshal.Release(iUnknownForObject);
		//    return result;
		//}

		//void nsIFactory.LockFactory(bool @lock)
		//{
		//}

		public const string ContractID = "@mozilla.org/helperapplauncherdialog;1";
	}
}