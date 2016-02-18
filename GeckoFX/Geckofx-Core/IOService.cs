using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Gecko.Interop;

namespace Gecko
{
	public static class IOService
	{
		private static ComPtr<nsIIOService2> _service;

		static IOService()
		{
			_service = Xpcom.GetService2<nsIIOService2>( Contracts.NetworkIOService );
		}

		public static bool Offline
		{
			get { return _service.Instance.GetOfflineAttribute(); }
			set{_service.Instance.SetOfflineAttribute( value );}
		}
		public static nsIURI CreateNsIUri(string url)
		{
			nsIURI ret;
			using (var str = new nsAUTF8String(url))
			{
				ret = _service.Instance.NewURI(str, null, null);
			}
			return ret;
		}

		internal static nsIURL CreateNsIUrl(string url)
		{
			var uri = CreateNsIUri(url);
			var ret = Xpcom.QueryInterface<nsIURL>(uri);
			Marshal.ReleaseComObject(uri);
			return ret;
		}

		//public static nsURI Create(string url)
		//{
		//    return new nsURI(CreateNsIUri(url));
		//}

		public static nsIChannel NewChannelFromUri(nsIURI uri)
		{
			return _service.Instance.NewChannelFromURI(uri);
		
		}

		public static nsIChannel NewChannelFromUriWithProxyFlags(nsIURI uri,nsIURI proxyUri,uint proxyFlags)
		{
			return _service.Instance.NewChannelFromURIWithProxyFlags( uri, proxyUri, proxyFlags );
		}


		public static bool ManageOfflineStatus
		{
			get { return _service.Instance.GetManageOfflineStatusAttribute(); }
			set { _service.Instance.SetManageOfflineStatusAttribute( value ); }
		}

	}
}
