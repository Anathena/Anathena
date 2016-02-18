using System;
using System.Runtime.InteropServices;
using Gecko.Interop;

namespace Gecko.Net
{
	public class Channel
		:Request
	{
		private nsIChannel _channel;

		protected Channel(nsIChannel channel)
			:base(channel)
		{
			_channel = channel;
		}

		public static Channel CreateChannel( nsIChannel channel )
		{
			if ( channel is nsIHttpChannel )
			{
				return new HttpChannel( ( nsIHttpChannel ) channel );
			}
			if ( channel is nsIJARChannel )
			{

			}
			if ( channel is nsIViewSourceChannel )
			{

			}
			if ( channel is nsIWyciwygChannel )
			{

			}
			return new Channel( channel );
		}


		public Uri OriginalUri
		{
			get
			{
				return Xpcom.TranslateUriAttribute( _channel.GetOriginalURIAttribute );
			}
			set { _channel.SetOriginalURIAttribute( IOService.CreateNsIUri( value.ToString() ) ); }
		}

		public Uri Uri
		{
			get
			{
				return Xpcom.TranslateUriAttribute(_channel.GetURIAttribute);
			}
		}

		public nsISupports Owner
		{
			get { return _channel.GetOwnerAttribute(); }
			set { _channel.SetOwnerAttribute( value ); }
		}

		public nsIInterfaceRequestor NotificationCallbacks
		{
			get { return _channel.GetNotificationCallbacksAttribute(); }
			set{_channel.SetNotificationCallbacksAttribute( value );}
		}

		public nsISupports SecurityInfo
		{
			get { return _channel.GetSecurityInfoAttribute(); }
		}

		public string ContentType
		{
			get { return nsString.Get( _channel.GetContentTypeAttribute ); }
			set{nsString.Set( _channel.SetContentTypeAttribute,value );}
		}

		public string ContentCharset
		{
			get { return nsString.Get(_channel.GetContentCharsetAttribute); }
			set { nsString.Set(_channel.SetContentCharsetAttribute, value); }
		}

		public long ContentLength
		{
			get { return _channel.GetContentLengthAttribute(); }
			set { _channel.SetContentLengthAttribute( value ); }
		}

		public IO.InputStream Open()
		{
			return IO.InputStream.Create( _channel.Open() );
		}

		//void AsyncOpen([MarshalAs(UnmanagedType.Interface)] nsIStreamListener aListener, [MarshalAs(UnmanagedType.Interface)] nsISupports aContext);

		public uint ContentDisposition
		{
			get { return _channel.GetContentDispositionAttribute(); }
		}

		public string ContentDispositionFilename
		{
			get { return nsString.Get( _channel.GetContentDispositionFilenameAttribute ); }
		}

		public string ContentDispositionHeader
		{
			get { return nsString.Get(_channel.GetContentDispositionHeaderAttribute); }
		}
	}
}