using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Gecko.Interop;

namespace Gecko.Cache
{
	public sealed class CacheSession
	{
		private ComPtr<nsICacheSession> _cacheSession;

		internal CacheSession(nsICacheSession cacheSession)
		{
			//ComDebug.WriteDebugInfo( cacheSession );
			_cacheSession = new ComPtr<nsICacheSession>(cacheSession);
		}

		public bool DoomEntriesIfExpired
		{
			get { return _cacheSession.Instance.GetDoomEntriesIfExpiredAttribute(); }
			set { _cacheSession.Instance.SetDoomEntriesIfExpiredAttribute(value); }
		}

		public bool IsStorageEnabled
		{

			get { return _cacheSession.Instance.IsStorageEnabled(); }
		}


		public void EvictEntries()
		{
			_cacheSession.Instance.EvictEntries();
		}


		[Obsolete("This method is not working in latest (>29) Xulrunner versions. Use AsyncOpenCacheEntry", true)]
		public CacheEntryDescriptor OpenCacheEntry( string key, CacheAccessMode accessRequested, bool blockingMode )
		{
			nsICacheEntryDescriptor descriptor = null;
			try
			{
				descriptor = _cacheSession.Instance.OpenCacheEntry( new nsACString( key ), new IntPtr((int)accessRequested), blockingMode );
			}
			catch (System.Runtime.InteropServices.COMException comEx)
			{
				Debug.WriteLine( string.Format( "COM Exception CODE={0},Message={1}", comEx.ErrorCode, comEx.Message ) );
			}
			catch (Exception e)
			{
				Debug.WriteLine( string.Format( "Exception Message={0}", e.Message ) );
			}
			return descriptor.Wrap( CacheEntryDescriptor.Create );
			
        }

		public void AsyncOpenCacheEntry( string key, CacheAccessMode accessRequested,Action<CacheEntryDescriptor, CacheAccessMode, CacheOpenEntryStatus> callback )
		{
			_cacheSession.Instance.AsyncOpenCacheEntry( new nsACString( key ),new IntPtr( (int) accessRequested), new CacheListener(callback), false );
		}
    }

	internal sealed class CacheListener
		: nsICacheListener
	{
		private Action<CacheEntryDescriptor, CacheAccessMode, CacheOpenEntryStatus> _callback;

		public CacheListener(Action<CacheEntryDescriptor, CacheAccessMode, CacheOpenEntryStatus> callback)
		{
			_callback = callback;
		}

		public void OnCacheEntryAvailable( nsICacheEntryDescriptor descriptor, IntPtr accessGranted, int status )
		{
			_callback(
				descriptor.Wrap( CacheEntryDescriptor.Create ),
				(CacheAccessMode) accessGranted.ToInt32(),
				(CacheOpenEntryStatus) status );
		}

		public void OnCacheEntryDoomed( int status )
		{
			
		}
	}

	public enum CacheAccessMode
	{
		None=0,
		Read=1,
		Write=2,
		ReadWrite=3
	}

	public enum CacheOpenEntryStatus
	{
		Ok=0,
		KeyNotFound = GeckoError.NS_ERROR_CACHE_KEY_NOT_FOUND,
		DataIsStream= GeckoError.NS_ERROR_CACHE_DATA_IS_STREAM,
		DataIsNotStream = GeckoError.NS_ERROR_CACHE_DATA_IS_NOT_STREAM,
		WaitForValidation = GeckoError.NS_ERROR_CACHE_WAIT_FOR_VALIDATION,
		EntryDoomed = GeckoError.NS_ERROR_CACHE_ENTRY_DOOMED,
		ReadAccessDenied = GeckoError.NS_ERROR_CACHE_READ_ACCESS_DENIED,
		WriteAccessDenied = GeckoError.NS_ERROR_CACHE_WRITE_ACCESS_DENIED,
		InUse = GeckoError.NS_ERROR_CACHE_IN_USE,
	}
}