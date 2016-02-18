using System;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.Net
{
	public class Request
		:IDisposable
	{
		private ComPtr<nsIRequest> _request;

		protected Request(nsIRequest request)
		{
			_request = new ComPtr<nsIRequest>( request );
		}

		~Request()
		{
			Xpcom.DisposeObject( ref _request );
		}

		public void Dispose()
		{
			Xpcom.DisposeObject( ref _request );
			GC.SuppressFinalize( this );
		}


		public nsIRequest NativeRequest
		{
			get { return _request.Instance; }
		}

		#region Wrapper functions and properties
		/// <summary>
		/// The name of the request.  Often this is the URI of the request.
		/// </summary>
		public string Name
		{
			get { return nsString.Get( _request.Instance.GetNameAttribute ); }
		}
			
		public bool IsPending
		{
			get
			{
				// Some subtype (ImgRequest) may throw 
				try
				{
					return _request.Instance.IsPending();
				}
				catch (NotImplementedException)
				{
					return false;
				}
			}
		}

		public int Status
		{
			get
			{
				// Some subtype (ImgRequest) may throw 
				try
				{
					return _request.Instance.GetStatusAttribute();
				}
				catch (NotImplementedException)
				{
					return 0;
				}
			}
		}

		public void Cancel(int aStatus)
		{
			_request.Instance.Cancel( aStatus );
		}

		public void Suspend()
		{
			_request.Instance.Suspend();
		}

		public void Resume()
		{
			_request.Instance.Resume();
		}

		public LoadGroup LoadGroup
		{
			get { return  _request.Instance.GetLoadGroupAttribute().Wrap( x=>new LoadGroup(x) ); }
			set { _request.Instance.SetLoadGroupAttribute( value == null ? null : value._loadGroup ); }
		}

		public uint LoadFlags
		{
			get { return _request.Instance.GetLoadFlagsAttribute(); }
			set { _request.Instance.SetLoadFlagsAttribute( value ); }
		}
		#endregion

		public override int GetHashCode()
		{
			return _request.Instance.GetHashCode();
		}



		public static Request CreateRequest( nsIRequest request )
		{
			if ( request is nsIChannel )
			{
				return Channel.CreateChannel( ( nsIChannel ) request );
			}

			if ( request is nsIAsyncStreamCopier )
			{
				return new AsyncStreamCopier( ( nsIAsyncStreamCopier ) request );
			}
			if ( request is nsILoadGroup )
			{
				return new LoadGroup( ( nsILoadGroup ) request );
			}
			if ( request is nsIIncrementalDownload )
			{
				return new IncrementalDownload( ( nsIIncrementalDownload ) request );
			}
			if ( request is imgIRequest )
			{
				return new ImgRequest( ( imgIRequest ) request );
			}
			if ( request is nsIInputStreamPump )
			{

			}
			if ( request is nsIURIChecker )
			{
				return new UriChecker( ( nsIURIChecker ) request );
			}
			return new Request( request );
		}
	}
}
