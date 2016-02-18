using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.Certificates
{
	public sealed class CertificateValidity
	{
		private ComPtr<nsIX509CertValidity> _validity;

		internal CertificateValidity( nsIX509CertValidity validity )
		{
			_validity = new ComPtr<nsIX509CertValidity>( validity );
		}

		public long NotBefore
		{
			get { return _validity.Instance.GetNotBeforeAttribute(); }
		}

		public string NotBeforeLocalTime
		{
			get { return nsString.Get(_validity.Instance.GetNotBeforeLocalTimeAttribute); }
		}

		public string NotBeforeLocalDay
		{
			get { return nsString.Get(_validity.Instance.GetNotBeforeLocalDayAttribute); }
		}

		public string NotBeforeGMT
		{
			get { return nsString.Get(_validity.Instance.GetNotBeforeGMTAttribute); }
		}
	
		public long NotAfter
		{
			get { return _validity.Instance.GetNotAfterAttribute(); }
		}

		public string NotAfterLocalTime
		{
			get { return nsString.Get(_validity.Instance.GetNotAfterLocalTimeAttribute); }
		}

		public string NotAfterLocalDay
		{
			get { return nsString.Get(_validity.Instance.GetNotAfterLocalDayAttribute); }
		}

		public string NotAfterGMT
		{
			get { return nsString.Get(_validity.Instance.GetNotAfterGMTAttribute); }
		}
	}
}
