using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Gecko.Interop;

namespace Gecko.Net
{
	public class HttpChannel
		: Channel
	{
		private nsIHttpChannel _httpChannel;

		public HttpChannel( nsIHttpChannel httpChannel )
			: base( httpChannel )
		{
			_httpChannel = httpChannel;
		}

		public static HttpChannel Create( nsIHttpChannel httpChannel )
		{
			return new HttpChannel( httpChannel );
		}

		public nsIHttpChannel Instance
		{
			get
			{
				return _httpChannel;
			}
		}

		public string RequestMethod
		{
			get { return nsString.Get( _httpChannel.GetRequestMethodAttribute ); }
			set { nsString.Set( _httpChannel.SetRequestMethodAttribute, value ); }
		}

		public Uri Referrer
		{
			get { return Xpcom.TranslateUriAttribute( _httpChannel.GetReferrerAttribute ); }
			set { _httpChannel.SetReferrerAttribute( IOService.CreateNsIUri( value.ToString() ) ); }
		}

		public string GetRequestHeader( string header )
		{
			string ret = null;
			try
			{
				ret = nsString.Get( _httpChannel.GetRequestHeader, header );
			}
			catch ( Exception )
			{

			}
			return ret;
		}

		public void SetRequestHeader( string header, string value, bool merge )
		{
			nsString.Set( ( x, y ) => _httpChannel.SetRequestHeader( x, y, merge ), header, value );
		}

		[Obsolete( "use GetRequestHeaders<T>", false )]
		public List<KeyValuePair<string, string>> GetRequestHeaders()
		{
			var ret = new List<KeyValuePair<string, string>>();
			var visitor = new HttpHeaderVisitor( ( x, y ) => ret.Add( new KeyValuePair<string, string>( x, y ) ) );
			_httpChannel.VisitRequestHeaders( visitor );
			return ret;
		}

		public void GetRequestHeaders<T>( T accumulator, Action<T, string, string> func )
		{
			HttpHeaderVisitor visitor = new HttpHeaderVisitor( ( x, y ) => func( accumulator, x, y ) );
			_httpChannel.VisitRequestHeaders( visitor );
		}

		public Dictionary<string, List<string>> GetRequestHeadersDict()
		{
			// name -> list of values
			var ret = new Dictionary<string, List<string>>();

			HttpHeaderVisitor visitor = new HttpHeaderVisitor( ( x, y ) =>
			{
				List<string> tmp = null;
				if ( ret.TryGetValue( x, out tmp ) )
				{
					tmp.Add( y );
				}
				else
				{
					ret.Add( x, new List<string>() {y} );
				}
			} );
			_httpChannel.VisitRequestHeaders( visitor );

			return ret;
		}


		public bool AllowPipelining
		{
			get { return _httpChannel.GetAllowPipeliningAttribute(); }
			set { _httpChannel.SetAllowPipeliningAttribute( value ); }
		}

		public uint RedirectionLimit
		{
			get { return _httpChannel.GetRedirectionLimitAttribute(); }
			set { _httpChannel.SetRedirectionLimitAttribute( value ); }
		}

		public uint ResponseStatus
		{
			get { return _httpChannel.GetResponseStatusAttribute(); }
		}

		public string ResponseStatusText
		{
			get { return nsString.Get( _httpChannel.GetResponseStatusTextAttribute ); }
		}

		public bool RequestSucceeded
		{
			get { return _httpChannel.GetRequestSucceededAttribute(); }
		}

		public string GetResponseHeader( string header )
		{
			return nsString.Get( _httpChannel.GetResponseHeader, header );
		}

		public void SetResponseHeader( string header, string value, bool merge )
		{
			nsString.Set( ( x, y ) => _httpChannel.SetResponseHeader( x, y, merge ), header, value );
		}

		[Obsolete( "use GetResponseHeaders<T>", false )]
		public List<KeyValuePair<string, string>> GetResponseHeaders()
		{
			var ret = new List<KeyValuePair<string, string>>();
			var visitor = new HttpHeaderVisitor( ( x, y ) => ret.Add( new KeyValuePair<string, string>( x, y ) ) );
			_httpChannel.VisitResponseHeaders( visitor );
			return ret;
		}

		public void GetResponseHeaders<T>( T accumulator, Action<T, string, string> func )
		{
			HttpHeaderVisitor visitor = new HttpHeaderVisitor( ( x, y ) => func( accumulator, x, y ) );
			_httpChannel.VisitResponseHeaders( visitor );
		}

		public Dictionary<string, List<string>> GetResponseHeadersDict()
		{
			// name -> list of values
			var ret = new Dictionary<string, List<string>>();

			HttpHeaderVisitor visitor = new HttpHeaderVisitor( ( x, y ) =>
			{
				List<string> tmp = null;
				if ( ret.TryGetValue( x, out tmp ) )
				{
					tmp.Add( y );
				}
				else
				{
					ret.Add( x, new List<string>() {y} );
				}
			} );
			_httpChannel.VisitResponseHeaders( visitor );

			return ret;
		}


		public bool IsNoStoreResponse
		{
			get { return _httpChannel.IsNoStoreResponse(); }
		}

		public bool IsNoCacheResponse
		{
			get { return _httpChannel.IsNoCacheResponse(); }
		}

		#region Casts

		#endregion

		public UploadChannel CastToUploadChannel()
		{
			var channel = Xpcom.QueryInterface<nsIUploadChannel>( _httpChannel );
			return channel.Wrap( x => new UploadChannel( x ) );
		}

		public TraceableChannel CastToTraceableChannel()
		{
			var channel = Xpcom.QueryInterface<nsITraceableChannel>( _httpChannel );
			return channel == null ? null : new TraceableChannel( channel );
		}

		/// <summary>
		/// Creates HttpChannel directly from nsISupports
		/// </summary>
		/// <param name="supports"></param>
		/// <returns></returns>
		public static HttpChannel Create( nsISupports supports )
		{
			//int count = Interop.ComDebug.GetRcwRefCount(supports);

			var channel = Xpcom.QueryInterface<nsIHttpChannel>( supports );
			if ( channel == null ) return null;
			Marshal.ReleaseComObject( channel );
			var ret = new HttpChannel( ( nsIHttpChannel ) supports );

			//var count2=Interop.ComDebug.GetRcwRefCount( supports );

			return ret;
		}


		/// <summary>
		/// Universal visitor
		/// </summary>
		private sealed class HttpHeaderVisitor
			: nsIHttpHeaderVisitor
		{
			private Action<string, string> _func;

			public HttpHeaderVisitor( Action<string, string> func )
			{
				_func = func;
			}

			void nsIHttpHeaderVisitor.VisitHeader( nsACStringBase aHeader, nsACStringBase aValue )
			{
				_func( aHeader.ToString(), aValue.ToString() );
			}
		}

	}


	public sealed class TraceableChannel
	{
		private ComPtr<nsITraceableChannel> _traceableChannel;
		internal TraceableChannel(nsITraceableChannel traceableChannel)
		{
			_traceableChannel = new ComPtr<nsITraceableChannel>(traceableChannel);
		}


		public void SetNewListener(StreamListenerTee streamListener)
		{
			var old = _traceableChannel.Instance.SetNewListener( streamListener._streamListenerTee.Instance );
			streamListener.IntInit( old );
		}
	}



}