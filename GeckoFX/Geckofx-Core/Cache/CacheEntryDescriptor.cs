using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Gecko.IO;
using Gecko.Interop;

namespace Gecko.Cache
{
	public sealed class CacheEntryDescriptor
		: CacheEntryInfo
	{
		private nsICacheEntryDescriptor _cacheEntryDescriptor;

		private CacheEntryDescriptor( nsICacheEntryDescriptor cacheEntryDescriptor )
			: base( cacheEntryDescriptor )
		{
			_cacheEntryDescriptor = cacheEntryDescriptor;
		}

		public static CacheEntryDescriptor Create( nsICacheEntryDescriptor cacheEntryDescriptor )
		{
			return new CacheEntryDescriptor( cacheEntryDescriptor );
		}

		public void Close()
		{
			_cacheEntryDescriptor.Close();
		}

		public void Doom()
		{
			_cacheEntryDescriptor.Doom();
		}

		public void DoomAndFailPendingRequests( int status )
		{
			_cacheEntryDescriptor.DoomAndFailPendingRequests( status );

		}

		public IntPtr AccessGranted
		{
			get { return _cacheEntryDescriptor.GetAccessGrantedAttribute(); }
		}

		public nsISupports CacheElement
		{
			get { return _cacheEntryDescriptor.GetCacheElementAttribute(); }
			set { _cacheEntryDescriptor.SetCacheElementAttribute( value ); }
		}

		public new uint ExpirationTimeNative
		{
			get { return _cacheEntryDescriptor.GetExpirationTimeAttribute(); }
			set { _cacheEntryDescriptor.SetExpirationTime(value); }
		}

		public new DateTime ExpirationTime
		{
			get { return Xpcom.Time.FromSecondsSinceEpoch(ExpirationTimeNative); }
			set { ExpirationTimeNative = Xpcom.Time.ToSecondsSinceEpoch(value); }
		}


		public long PredictedDataSize
		{
			get { return _cacheEntryDescriptor.GetPredictedDataSizeAttribute(); }
			set { _cacheEntryDescriptor.SetPredictedDataSizeAttribute(value); }
		}

		public CacheStoragePolicy StoragePolicy
		{
			get { return (CacheStoragePolicy)_cacheEntryDescriptor.GetStoragePolicyAttribute(); }
			set { _cacheEntryDescriptor.SetStoragePolicyAttribute((IntPtr)(int)value); }
		}

		public nsIFile File
		{
			get { return _cacheEntryDescriptor.GetFileAttribute(); }
		}

		public nsISupports SecurityInfo
		{
			get { return _cacheEntryDescriptor.GetSecurityInfoAttribute(); }
			set { _cacheEntryDescriptor.SetSecurityInfoAttribute(value); }
		}

		public new uint DataSize
		{
			get { return _cacheEntryDescriptor.GetDataSizeAttribute(); }
			set { _cacheEntryDescriptor.SetDataSize(value); }
		}

		#region Functions
		
		public InputStream OpenInputStream(uint offset)
		{
			return InputStream.Create(_cacheEntryDescriptor.OpenInputStream(offset));
		}

		public IO.OutputStream OpenOutputStream( uint offset )
		{
			return OutputStream.Create( _cacheEntryDescriptor.OpenOutputStream( offset ) );
		}

		public void MarkValid()
		{
			_cacheEntryDescriptor.MarkValid();
		}


		public string GetMetaDataElement( string key )
		{
			return _cacheEntryDescriptor.GetMetaDataElement( key );
		}

		public void SetMetaDataElement( string key, string value )
		{
			_cacheEntryDescriptor.SetMetaDataElement( key, value );
		}

		public KeyValuePair<string, string>[] GetAllMetadata()
		{
			KeyValuePair<string, string>[] ret = null;
			using (var searcher = new CacheEntryMetadataSearcher((x, y) => true))
			{
				_cacheEntryDescriptor.VisitMetaData(searcher);
				ret = searcher.GetResult();
			}
			return ret;
		}

		public KeyValuePair<string,string>[] SearchInMetadata(Func<string,string,bool> predicate)
		{
			KeyValuePair<string, string>[] ret = null;
			using (var searcher = new CacheEntryMetadataSearcher(predicate))
			{
				_cacheEntryDescriptor.VisitMetaData(searcher);
				ret = searcher.GetResult();
			}
			return ret;
		}
		#endregion

		// void VisitMetaData([MarshalAs(UnmanagedType.Interface)] nsICacheMetaDataVisitor visitor);

	}

	internal sealed class CacheEntryMetadataSearcher
		: nsICacheMetaDataVisitor, IDisposable
	{
		private Func<string,string,bool> _predicate;

		private List<KeyValuePair<string, string>> _foundEntries=new List<KeyValuePair<string, string>>();  

		internal CacheEntryMetadataSearcher(Func<string,string,bool> predicate)

		{
			_predicate = predicate;
		}


		~CacheEntryMetadataSearcher()
		{
			_foundEntries.Clear();
		}

		public bool VisitMetaDataElement( string key, string value )
		{
			if (_predicate(key, value))
			{
				_foundEntries.Add( new KeyValuePair<string, string>( key, value ) );
			}
			return true;
		}

		public void Dispose()
		{
			_foundEntries.Clear();
			GC.SuppressFinalize(this);
		}


		public KeyValuePair<string, string>[] GetResult()
		{
			return _foundEntries.ToArray();
		}

		

	}
}