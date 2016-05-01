using System;
using System.Runtime.InteropServices;

namespace Gecko
{
    /// <summary>
    /// Implements nsIXULAppInfo for geckofx.
    /// Object "@mozilla.org/xre/app-info;1" in XULRunner embedded in geckofx only implements nsIXULRuntime, but not nsIXULAppInfo.
    /// This can cause a few issues, such as AddonManager refuses to start, which in turn affects remote debugging and about:plugins.
    /// So we replace the original "@mozilla.org/xre/app-info;1" with an instance of XULAppInfo, which implements nsIXULAppInfo's 
    /// methods, and delegate nsIXULRuntime's methods to the original object.
    /// 
    /// P.S., To start AddonManager, run
    ///   Components.utils.import("resource://gre/modules/AddonManager.jsm");
    ///   AddonManagerPrivate.startup();
    /// in some chrome JS code.
    /// </summary>
    [Guid("8E4AABE2-B832-4cff-B213-2174DE2B839F")]
	[ContractID(XULAppInfoFactory.ContractID)]
	class XULAppInfoFactory
		: GenericOneClassNsFactory<XULAppInfoFactory, XULAppInfo>
	{
		public const string ContractID = Contracts.XulRuntime;

		public static void Init()
		{
			XULAppInfo.Init();
			Register();
		}
	}

	class XULAppInfo : nsIXULAppInfo, nsIXULRuntime
	{
		private static nsIXULRuntime backXulRuntime;

		public static void Init()
		{
			backXulRuntime = Xpcom.GetService<nsIXULRuntime>(Contracts.XulRuntime);
		}

		public void GetVendorAttribute(nsACStringBase aVendor)
		{
			aVendor.SetData("Mozilla and geckofx contributors");
		}

		public void GetNameAttribute(nsACStringBase aName)
		{
			aName.SetData("geckofx");
		}

		public void GetIDAttribute(nsACStringBase aID)
		{
			aID.SetData("unknown_id");
		}

		public void GetVersionAttribute(nsACStringBase aVersion)
		{
			aVersion.SetData("29");
		}

		public void GetAppBuildIDAttribute(nsACStringBase aAppBuildID)
		{
			aAppBuildID.SetData("unknown_id");
		}

		public void GetPlatformVersionAttribute(nsACStringBase aPlatformVersion)
		{
			aPlatformVersion.SetData("29");
		}

		public void GetPlatformBuildIDAttribute(nsACStringBase aPlatformBuildID)
		{
			aPlatformBuildID.SetData("0");
		}

		public void GetUANameAttribute(nsACStringBase aUAName)
		{
			aUAName.SetData("geckofx");
		}

		public bool GetInSafeModeAttribute()
		{
			return backXulRuntime.GetInSafeModeAttribute();
		}

		public bool GetLogConsoleErrorsAttribute()
		{
			return backXulRuntime.GetLogConsoleErrorsAttribute();
		}

		public void SetLogConsoleErrorsAttribute(bool aLogConsoleErrors)
		{
			backXulRuntime.SetLogConsoleErrorsAttribute(aLogConsoleErrors);
		}

		public void GetOSAttribute(nsAUTF8StringBase aOS)
		{
			backXulRuntime.GetOSAttribute(aOS);
		}

		public void GetXPCOMABIAttribute(nsAUTF8StringBase aXPCOMABI)
		{
			backXulRuntime.GetXPCOMABIAttribute(aXPCOMABI);
		}

		public void GetWidgetToolkitAttribute(nsAUTF8StringBase aWidgetToolkit)
		{
			backXulRuntime.GetWidgetToolkitAttribute(aWidgetToolkit);
		}

		public uint GetProcessTypeAttribute()
		{
			return backXulRuntime.GetProcessTypeAttribute();
		}

	    public uint GetProcessIDAttribute()
	    {
	        throw new NotImplementedException();
	    }

	    public bool GetBrowserTabsRemoteAttribute()
		{
			return backXulRuntime.GetBrowserTabsRemoteAttribute();
		}

		public void InvalidateCachesOnRestart()
		{
			backXulRuntime.InvalidateCachesOnRestart();
		}

		public void EnsureContentProcess()
		{
			backXulRuntime.EnsureContentProcess();
		}

		public long GetReplacedLockTimeAttribute()
		{
			return backXulRuntime.GetReplacedLockTimeAttribute();
		}

		public void GetLastRunCrashIDAttribute(nsAStringBase aLastRunCrashID)
		{
			backXulRuntime.GetLastRunCrashIDAttribute(aLastRunCrashID);
		}

	    public bool GetIsReleaseBuildAttribute()
	    {
	        throw new NotImplementedException();
	    }

	    public bool GetIsOfficialBrandingAttribute()
	    {
	        throw new NotImplementedException();
	    }

	    public void GetDefaultUpdateChannelAttribute(nsAUTF8StringBase aDefaultUpdateChannel)
	    {
	        throw new NotImplementedException();
	    }

	    public void GetDistributionIDAttribute(nsAUTF8StringBase aDistributionID)
	    {
	        throw new NotImplementedException();
	    }
	}
}
