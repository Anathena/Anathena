using System.Collections.Generic;
using Gecko.Interop;

namespace Gecko
{
	public static class CookieManager
	{
		private static ComPtr<nsICookieManager2> _cookieManager;

		static CookieManager()
		{
			_cookieManager = Xpcom.GetService2<nsICookieManager2>(Contracts.CookieManager);		
		}

		public static void Add(string host,string path,string name,string value,bool isSecure,bool isHttpOnly,bool isSession,long expiry)
		{
			_cookieManager.Instance.Add(
				new nsAUTF8String( host ),
				new nsAUTF8String( path ),
				new nsACString( name ),
				new nsACString( value ),
				isSecure,
				isHttpOnly,
				isSession,
				expiry );
		}

		public static bool CookieExists(Cookie cookie)
		{
			return _cookieManager.Instance.CookieExists(cookie._cookie);
		}

		public static int CountCookiesFromHost(string host)
		{
			// int is big for cookie count :)
			return (int)_cookieManager.Instance.CountCookiesFromHost(new nsAUTF8String(host));
		}

		public static IEnumerator<Cookie> GetCookiesFromHost(string host)
		{
			return new Collections.GeckoEnumerator<Cookie, nsICookie2>(_cookieManager.Instance.GetCookiesFromHost(new nsAUTF8String(host)),
																	   x => new Cookie(x));
		}

		public static IEnumerator<Cookie> GetEnumerator()
		{
			return new Collections.GeckoEnumerator<Cookie, nsICookie2>(_cookieManager.Instance.GetEnumeratorAttribute(),
																	   x => new Cookie(x));
		}

		public static void ImportCookies(string filename)
		{
			_cookieManager.Instance.ImportCookies((nsIFile)Xpcom.NewNativeLocalFile(filename));
		}

		public static void Remove(string host, string name, string path, bool blocked)
		{
			_cookieManager.Instance.Remove(new nsAUTF8String(host),
			                       new nsACString( name ),
			                       new nsAUTF8String( path ),
			                       blocked );
		}

		public static void RemoveAll()
		{
			_cookieManager.Instance.RemoveAll();
		}
	}


	public sealed class Cookie
	{
		internal nsICookie2 _cookie;

		internal Cookie(nsICookie2 cookie)
		{
			_cookie = cookie;
		}

		public long CreationTime
		{
			get { return _cookie.GetCreationTimeAttribute(); }
		}

		public long Expiry
		{
			get { return _cookie.GetExpiryAttribute(); }
		}

		public string Host
		{
			get { return nsString.Get( _cookie.GetHostAttribute ); }
		}

		public bool IsDomain
		{
			get { return _cookie.GetIsDomainAttribute(); }
		}

		public bool IsHttpOnly
		{
			get { return _cookie.GetIsHttpOnlyAttribute(); }
		}

		public bool IsSecure
		{
			get { return _cookie.GetIsSecureAttribute(); }
		}

		public bool IsSession
		{
			get { return _cookie.GetIsSessionAttribute(); }
		}

		public long LastAccessed
		{
			get { return _cookie.GetLastAccessedAttribute(); }
		}

		public string Name
		{
			get { return nsString.Get(_cookie.GetNameAttribute); }
		}

		public string Path
		{
			get { return nsString.Get(_cookie.GetPathAttribute); }
		}

		public string RawHost
		{
			get { return nsString.Get(_cookie.GetRawHostAttribute); }
		}

		public string Value
		{
			get { return nsString.Get(_cookie.GetValueAttribute); }
		}
	}
}
