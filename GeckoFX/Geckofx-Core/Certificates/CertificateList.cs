using System;
using System.Collections;
using System.Collections.Generic;
using Gecko.Collections;
using Gecko.Interop;

namespace Gecko.Certificates
{
	public sealed class CertificateList
		:IEnumerable<Certificate>
	{
		internal ComPtr<nsIX509CertList> _list;

		internal CertificateList( nsIX509CertList list )
		{
			_list = new ComPtr<nsIX509CertList>( list );
        }

		public IntPtr RawCertList
		{
			get { return _list.Instance.GetRawCertList(); }
		}

		public IEnumerator<Certificate> GetEnumerator()
		{
			return new GeckoEnumerator<Certificate, nsIX509Cert3>( _list.Instance.GetEnumerator(), Certificate.Create );
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void AddCert(Certificate certificate)
		{
			_list.Instance.AddCert(certificate._cert.Instance);
		}

		public void DeleteCert(Certificate certificate)
		{
			_list.Instance.DeleteCert(certificate._cert.Instance);
		}
	}
}