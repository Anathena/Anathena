using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Gecko.Interop;


namespace Gecko.Events
{
	public class GeckoNSSErrorEventArgs : HandledEventArgs 
	{
		private ComPtr<nsINSSErrorsService> _nssErrorSvc;
		private int _statusCode;
		private Uri _uri;

		public GeckoNSSErrorEventArgs(Uri uri, int status)
		{			
			_statusCode = status;
			_uri = uri;
		}

		private nsINSSErrorsService NSSErrorSvc
		{
			get
			{
				if(_nssErrorSvc == null)
					_nssErrorSvc = Xpcom.GetService2<nsINSSErrorsService>(Contracts.NSSErrorsService);
				return _nssErrorSvc.Instance;
			}
		}


		#region public

		public Uri Uri
		{
			get { return _uri; }
		}

		public int StatusCode
		{
			get { return _statusCode; }
		}

		public NSSErrors ErrorCode
		{
			get
			{
				return (NSSErrors)(-(_statusCode & 0xffff));
			}
		}

		public string Message
		{
			get
			{
				return nsString.Get(NSSErrorSvc.GetErrorMessage, _statusCode);
			}
		}

		public int ErrorClass
		{
			get
			{
				try
				{
					return (int)NSSErrorSvc.GetErrorClass(_statusCode);
				}
				catch(COMException ex)
				{
					if(ex.ErrorCode == GeckoError.NS_ERROR_UNEXPECTED)
						return (int)nsINSSErrorsServiceConsts.ERROR_CLASS_SSL_PROTOCOL;
					throw;
				}
			}
		}

		#endregion

	}
}
