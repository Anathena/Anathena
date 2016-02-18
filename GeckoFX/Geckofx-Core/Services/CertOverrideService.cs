using System;
using Gecko.Interop;

namespace Gecko
{
	public static class CertOverrideService
	{
		private static ComPtr<nsICertOverrideService> GetService()
		{
			return Xpcom.GetService2<nsICertOverrideService>(Contracts.CertOverride);
		}

		public static bool RememberRecentBadCert(Uri url)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			ComPtr<nsISSLStatus> aSSLStatus = null;
			try
			{
				int port = url.Port;
				if (port == -1) port = 443;
				using (var hostWithPort = new nsAString(url.Host + ":" + port.ToString()))
				{
					using (var certDbSvc = Xpcom.GetService2<nsIX509CertDB>(Contracts.X509CertDb))
					{
						if (certDbSvc != null)
						{
							using (var recentBadCerts = certDbSvc.Instance.GetRecentBadCerts(false).AsComPtr())
							{
								if (recentBadCerts != null)
									aSSLStatus = recentBadCerts.Instance.GetRecentBadCert(hostWithPort).AsComPtr();
							}
						}
					}
				}
				using (var cert = aSSLStatus.Instance.GetServerCertAttribute().AsComPtr())
				{
					if (aSSLStatus == null
						|| HasMatchingOverride(url, cert))
					{
						return false;
					}

					int flags = 0;
					if (aSSLStatus.Instance.GetIsUntrustedAttribute())
						flags |= nsICertOverrideServiceConsts.ERROR_UNTRUSTED;
					if (aSSLStatus.Instance.GetIsDomainMismatchAttribute())
						flags |= nsICertOverrideServiceConsts.ERROR_MISMATCH;
					if (aSSLStatus.Instance.GetIsNotValidAtThisTimeAttribute())
						flags |= nsICertOverrideServiceConsts.ERROR_TIME;

					RememberValidityOverride(url, cert, flags);
				}
			}
			finally
			{
				if (aSSLStatus != null)
					aSSLStatus.Dispose();
			}
			return true;
		}

		public static bool HasMatchingOverride(Uri url, ComPtr<nsIX509Cert> cert)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			using (var aHostName = new nsACString(url.Host))
			{
				uint flags = 0;
				bool isTemp = false;
				using (var overrideSvc = GetService())
				{
					return overrideSvc.Instance.HasMatchingOverride(aHostName, url.Port, cert.Instance, ref flags, ref isTemp);
				}
			}
		}

		/// <param name="flags">see nsICertOverrideServiceConsts</param>
		public static void RememberValidityOverride(Uri url, ComPtr<nsIX509Cert> cert, int flags)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			using (var aHostName = new nsACString(url.Host))
			{
				using (var svc = GetService())
				{
					svc.Instance.RememberValidityOverride(aHostName, url.Port, cert.Instance, (uint)flags, true);
				}
			}
		}

		public static void ClearValidityOverride(Uri url)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			using (var aHostName = new nsACString(url.Host))
			{
				using (var svc = GetService())
				{
					svc.Instance.ClearValidityOverride(aHostName, url.Port);
				}
			}
		}

	}
}
