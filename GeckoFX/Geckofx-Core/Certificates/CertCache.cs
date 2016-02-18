using Gecko.Interop;

namespace Gecko.Certificates
{
	public sealed class CertCache
	{
		internal ComPtr<nsINSSCertCache> _certCache;

		internal CertCache(nsINSSCertCache certCache)
		{
			_certCache = new ComPtr<nsINSSCertCache>(certCache);
		}

		/// <summary>
		/// cacheAllCerts
		///
		/// Creates a cache of all certificates currently known to NSS.
		/// </summary>
		public void CacheAllCerts()
		{
			_certCache.Instance.CacheAllCerts();
		}

		/// <summary>Member CacheCertList </summary>
		/// <param name='list'> </param>
		public void CacheCertList(CertificateList list)
		{
			_certCache.Instance.CacheCertList(list._list.Instance);
		}


		/// <summary>
		/// getCachedCerts
		///
		/// Returns the cached CERTCertList*
		/// </summary>
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//System.IntPtr GetCachedCerts();

		/// <summary>
		/// get an X509CertList
		/// </summary>
		public CertificateList GetX509CachedCerts()
		{
			return new CertificateList( _certCache.Instance.GetX509CachedCerts() );
		}

	}
}