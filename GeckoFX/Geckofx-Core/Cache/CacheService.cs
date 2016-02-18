using System;
using System.Collections.Generic;
using Gecko.Interop;

namespace Gecko.Cache
{
	public static class CacheService
	{
		private static ComPtr<nsICacheService> _cacheService;

		static CacheService()
		{
			_cacheService = Xpcom.GetService2<nsICacheService>( Contracts.CacheService );
		}

		public static CacheSession CreateSession( string clientID, CacheStoragePolicy storagePolicy, bool streamBased )
		{
			// typedef long nsCacheStoragePolicy in nsICache.idl
			// In WINDOWS long is 4 bytes int
			return
				new CacheSession( _cacheService.Instance.CreateSession( clientID, new IntPtr( (int) storagePolicy ), streamBased ) );
		}

		public static string[] Search( string deviceID, Predicate<CacheEntryInfo> predicate )
		{
			string[] ret = null;
			using (var searcher = new CacheSearcher( deviceID, predicate ))
			{
				_cacheService.Instance.VisitEntries( searcher );
				ret = searcher.GetResult();
			}
			return ret;
		}

		/// <summary>
		/// Returns key's of founded entries
		/// 
		/// WARNING. We can't return all CacheEntryInfo entity because after search in becomes invalid :(
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static string[] Search( Predicate<CacheEntryInfo> predicate )
		{
			string[] ret = null;
			using (var searcher = new CacheSearcher( predicate ))
			{
				_cacheService.Instance.VisitEntries( searcher );
				ret = searcher.GetResult();
			}
			return ret;
		}

		public static void ProcessEntries( Action<CacheEntryInfo> entryProcessor )
		{
			CacheVisitor visitor = new CacheVisitor( entryProcessor );
			_cacheService.Instance.VisitEntries( visitor );
		}

		/// <summary>
		/// This method evicts all entries in all devices implied by the storage policy.
		/// </summary>
		/// <param name="storagePolicy">The cache storage policy.</param>
		/// <remarks>This function may evict some items but will throw if it fails to evict everything.</remarks>
		/// <exception cref="System.Runtime.InteropServices.COMException"></exception>
		public static void Clear( CacheStoragePolicy storagePolicy )
		{
			_cacheService.Instance.EvictEntries( new IntPtr( (int) storagePolicy ) );
		}

		public static readonly string MemoryCacheDevice = "memory";
		public static readonly string DiskCacheDevice = "disk";
	}

	internal sealed class CacheSearcher
		: nsICacheVisitor, IDisposable
	{
		private Predicate<CacheEntryInfo> _predicate;
		private string _deviceID;

		private List<string> _foundEntries = new List<string>();

		internal CacheSearcher( Predicate<CacheEntryInfo> predicate )
			: this( null, predicate )
		{

		}

		internal CacheSearcher( string deviceID, Predicate<CacheEntryInfo> predicate )
		{
			_deviceID = deviceID;
			_predicate = predicate;
		}

		~CacheSearcher()
		{
			_foundEntries.Clear();
		}

		public void Dispose()
		{
			_foundEntries.Clear();
			GC.SuppressFinalize( this );
		}

		public bool VisitDevice( string deviceID, nsICacheDeviceInfo deviceInfo )
		{
			if (string.IsNullOrEmpty( _deviceID )) return true;
			return string.Equals( _deviceID, deviceID );
		}

		public bool VisitEntry( string deviceID, nsICacheEntryInfo entryInfo )
		{
			if (entryInfo == null) return true;

			using (var entry = CacheEntryInfo.Create( entryInfo ))
			{
				if (_predicate(entry))
				{				
					var key = entry.Key;
					_foundEntries.Add( key );
				}
			}
			return true;
		}

		public string[] GetResult()
		{
			return _foundEntries.ToArray();
		}

	}


	internal sealed class CacheVisitor
		: nsICacheVisitor
	{
		private string _deviceID;
		private Action<CacheEntryInfo> _entryProcessor;

		internal CacheVisitor( Action<CacheEntryInfo> entryProcessor )
			: this( null, entryProcessor )
		{

		}

		internal CacheVisitor( string deviceID, Action<CacheEntryInfo> entryProcessor )
		{
			_deviceID = deviceID;
			_entryProcessor = entryProcessor;
		}

		public bool VisitDevice( string deviceID, nsICacheDeviceInfo deviceInfo )
		{
			if (string.IsNullOrEmpty( _deviceID )) return true;
			return string.Equals( _deviceID, deviceID );
		}

		public bool VisitEntry( string deviceID, nsICacheEntryInfo entryInfo )
		{
			if (entryInfo == null) return true;
			using (var entry = CacheEntryInfo.Create( entryInfo ))
			{
				_entryProcessor( entry );
			}
			return true;
		}
	}

	public enum CacheStoragePolicy
	{
		/// <summary>
		/// nsCacheStoragePolicy STORE_ANYWHERE
		/// </summary>
		Anywhere = 0,

		/// <summary>
		/// nsCacheStoragePolicy STORE_IN_MEMORY
		/// </summary>
		InMemory = 1,

		/// <summary>
		/// nsCacheStoragePolicy STORE_ON_DISK
		/// </summary>
		OnDisk = 2,

		[Obsolete( "value 3 was used by STORE_ON_DISK_AS_FILE which was removed", true )]
		OnDiskAsFile = 3,

		/// <summary>
		/// nsCacheStoragePolicy STORE_OFFLINE
		/// </summary>
		Offline = 4
	}
}