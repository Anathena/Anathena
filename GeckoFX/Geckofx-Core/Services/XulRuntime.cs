using Gecko.Interop;

namespace Gecko
{
    public static class XulRuntime
	{
		private static ComPtr<nsIXULRuntime> _xulRuntime;

		static XulRuntime()
		{
			_xulRuntime = Xpcom.GetService2<nsIXULRuntime>(Contracts.XulRuntime);
		}

		public static string OS
		{
			get { return nsString.Get( _xulRuntime.Instance.GetOSAttribute ); }
		}
	}
}
