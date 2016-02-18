using System;
using Gecko.Interop;

namespace Gecko.Certificates
{
	//https://bugzilla.mozilla.org/show_bug.cgi?id=643041
	// nsIX509Cert nsIX509Cert2 nsIX509Cert3 - are merged !

	public sealed class Certificate
		: System.IEquatable<Certificate>
	{
		internal ComPtr<nsIX509Cert3> _cert;

		private Certificate(nsIX509Cert3 cert)
		{
			_cert = new ComPtr<nsIX509Cert3>( cert );
		}

		public string Nickname
		{
			get { return nsString.Get(_cert.Instance.GetNicknameAttribute); }
		}

		public string EmailAddress
		{
			get { return nsString.Get(_cert.Instance.GetEmailAddressAttribute); }
		}

		public void GetEmailAddresses(ref uint length,ref IntPtr[] addresses)
		{
			_cert.Instance.GetEmailAddresses( ref length, ref addresses );
		}

		public bool ContainsEmailAddress(string email)
		{
			return nsString.Pass<bool>(_cert.Instance.ContainsEmailAddress, email);
		}

		public string SubjectName
		{
			get { return nsString.Get(_cert.Instance.GetSubjectNameAttribute); }
		}

		public string CommonName
		{
			get { return nsString.Get(_cert.Instance.GetCommonNameAttribute); }
		}

		public string Organization
		{
			get { return nsString.Get(_cert.Instance.GetOrganizationAttribute); }
		}

		public string OrganizationalUnit
		{
			get { return nsString.Get(_cert.Instance.GetOrganizationalUnitAttribute); }
		}

		public string Sha1Fingerprint
		{
			get { return nsString.Get(_cert.Instance.GetSha1FingerprintAttribute); }
		}

		public string Md5Fingerprint
		{
			get { return nsString.Get(_cert.Instance.GetMd5FingerprintAttribute); }
		}

		public string TokenName
		{
			get { return nsString.Get(_cert.Instance.GetTokenNameAttribute); }
		}

		public string IssuerName
		{
			get { return nsString.Get(_cert.Instance.GetIssuerNameAttribute); }
		}

		public string SerialNumber
		{
			get { return nsString.Get(_cert.Instance.GetSerialNumberAttribute); }
		}

		public string IssuerCommonName
		{
			get { return nsString.Get(_cert.Instance.GetIssuerCommonNameAttribute); }
		}

		public string IssuerOrganization
		{
			get { return nsString.Get(_cert.Instance.GetIssuerOrganizationAttribute); }
		}

		public string IssuerOrganizationUnit
		{
			get { return nsString.Get(_cert.Instance.GetIssuerOrganizationUnitAttribute ); }
		}

		public Certificate Issuer
		{
			get
			{
				return ( (nsIX509Cert3) _cert.Instance.GetIssuerAttribute() ).Wrap( Create );
			}
		}

		public CertificateValidity Validity
		{
			get { return new CertificateValidity(_cert.Instance.GetValidityAttribute() ); }
		}

		public string DbKey
		{
			get { return _cert.Instance.GetDbKeyAttribute(); }
		}

		public string WindowTitle
		{
			get { return _cert.Instance.GetWindowTitleAttribute(); }
		}

		public Collections.IGeckoArray<Certificate> Chain
		{
			get { return new Collections.GeckoArray<Certificate, nsIX509Cert3>(_cert.Instance.GetChain(), Certificate.Create ); }
		}

		public void GetUsagesArray(bool localOnly,ref uint verified,ref uint count,ref System.IntPtr[] usages)
		{
			_cert.Instance.GetUsagesArray( localOnly, ref verified, ref count, ref usages );
		}

		public string GetUsagesString(bool localOnly,ref uint verified)
		{
			string ret = null;
			using (nsAString str = new nsAString())
			{
				_cert.Instance.GetUsagesString( localOnly, ref verified, str );
				ret = str.ToString();
			}
			return ret;
		}

		public ASN1Object ASN1Structure
		{
			get { return new ASN1Object(_cert.Instance.GetASN1StructureAttribute()); }
		}

		public void GetRawDER(ref uint length,ref byte[] data)
		{
			_cert.Instance.GetRawDER( ref length, ref data );
		}

		public uint CertType
		{
			get { return _cert.Instance.GetCertTypeAttribute(); }
		}

		public void MarkForPermDeletion()
		{
			_cert.Instance.MarkForPermDeletion();
		}

		public IntPtr GetCert()
		{
			return _cert.Instance.GetCert();
		}

		//void RequestUsagesArrayAsync([MarshalAs(UnmanagedType.Interface)] nsICertVerificationListener cvl);


		public void ExportAsCMS(uint chainMode, ref uint length, ref byte[] data)
		{
			_cert.Instance.ExportAsCMS( chainMode, ref length, ref data );
		}

		public bool IsSelfSigned
		{
			get { return _cert.Instance.GetIsSelfSignedAttribute(); }
		}

		public void GetAllTokenNames(ref uint length, ref System.IntPtr[] tokenNames)
		{
			_cert.Instance.GetAllTokenNames( ref length, ref tokenNames );
		}

		public bool Equals( Certificate other )
		{
			return _cert.Instance.Equals( other._cert.Instance);
		}

		public override bool Equals(object obj)
		{
			if (obj is Certificate)
			{
				return Equals( ( Certificate ) obj );
			}
			if (obj is nsIX509Cert3)
			{
				return _cert.Instance.Equals( ( nsIX509Cert3 ) obj );
			}
			return false;
		}

		public override int GetHashCode() {
			return _cert.Instance.GetHashCode();
		}


		public static Certificate Create(nsIX509Cert3 certificate)
		{
			return new Certificate(certificate);
		}
	}
}