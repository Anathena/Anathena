using System;
using System.Runtime.InteropServices;
using System.Threading;
using Gecko.Interop;

namespace Gecko.Cache
{
	public class CacheEntryInfo
		:IDisposable
	{
		private ComPtr<nsICacheEntryInfo> _cacheEntryInfo;

		protected CacheEntryInfo( nsICacheEntryInfo cacheEntryInfo )
		{
			_cacheEntryInfo = new ComPtr<nsICacheEntryInfo>( cacheEntryInfo );
		}

		public void Dispose()
		{
			_cacheEntryInfo.Dispose();
			GC.SuppressFinalize( this );
		}

		public nsICacheEntryInfo NativeCacheEntryInfo
		{
			get { return _cacheEntryInfo.Instance; }
		}


		public string ClientID
		{
			get { return _cacheEntryInfo.Instance.GetClientIDAttribute(); }
		}

		public uint DataSize
		{
			get { return _cacheEntryInfo.Instance.GetDataSizeAttribute(); }
		}

		public string DeviceID
		{
			get { return _cacheEntryInfo.Instance.GetDeviceIDAttribute(); }
		}

		public uint ExpirationTimeNative
		{
			get { return _cacheEntryInfo.Instance.GetExpirationTimeAttribute(); }
		}

		public DateTime ExpirationTime
		{
			get { return Xpcom.Time.FromSecondsSinceEpoch( ExpirationTimeNative ); }
		}


		public int FetchCount
		{
			get { return _cacheEntryInfo.Instance.GetFetchCountAttribute(); }
		}

		public string Key
		{
			get { return nsString.Get( _cacheEntryInfo.Instance.GetKeyAttribute ); }
		}

		public uint LastFetched
		{
			get { return _cacheEntryInfo.Instance.GetLastFetchedAttribute(); }
		}

		public uint LastModified
		{
			get { return _cacheEntryInfo.Instance.GetLastModifiedAttribute(); }
		}

		public bool IsStreamBased
		{
			get { return _cacheEntryInfo.Instance.IsStreamBased(); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <param name="incrementRefCounter">for visitor api OLNY</param>
		/// <returns></returns>
		public static CacheEntryInfo Create( nsICacheEntryInfo info)
		{
			if (info is nsICacheEntryDescriptor)
			{
				return CacheEntryDescriptor.Create( (nsICacheEntryDescriptor) info );
			}
			return new CacheEntryInfo( info );
		}
	}
}