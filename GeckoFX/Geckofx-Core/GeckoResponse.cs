using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Gecko
{
	/// <summary>
	/// Represents a response to a Gecko web request.
	/// </summary>
	public class GeckoResponse
	{			
		internal GeckoResponse(nsIRequest request)
		{
			Channel = Xpcom.QueryInterface<nsIChannel>(request);
			HttpChannel = Xpcom.QueryInterface<nsIHttpChannel>(request);
		}
		
		nsIChannel Channel;
		nsIHttpChannel HttpChannel;
		
		/// <summary>
		/// Gets the MIME type of the channel's content if available.
		/// </summary>
		public string ContentType
		{
			get { return (Channel == null) ? null : nsString.Get(Channel.GetContentTypeAttribute);
			}
		}
		
		/// <summary>
		/// Gets the character set of the channel's content if available and if applicable.
		/// </summary>
		public string ContentCharset
		{
			get { return (Channel == null) ? null : nsString.Get(Channel.GetContentCharsetAttribute);
			}
		}
		
		/// <summary>
		/// Gets the length of the data associated with the channel if available. A value of -1 indicates that the content length is unknown.
		/// </summary>
		public long ContentLength
		{
			get { return (Channel == null) ? -1 : Channel.GetContentLengthAttribute(); }
		}
		
		/// <summary>
		/// Gets the HTTP request method.
		/// </summary>
		public string HttpRequestMethod
		{
			get { return (HttpChannel == null) ? null : nsString.Get(HttpChannel.GetRequestMethodAttribute); }
		}
		
		/// <summary>
		/// Returns true if the HTTP response code indicates success. This value will be true even when processing a 404 response because a 404 response
		/// may include a message body that (in some cases) should be shown to the user.
		/// </summary>
		public bool HttpRequestSucceeded
		{
			get { return (HttpChannel == null) || HttpChannel.GetRequestSucceededAttribute(); }
		}
		
		/// <summary>
		/// Gets the HTTP response code (a value of 200 indicates success).
		/// </summary>
		public int HttpResponseStatus
		{
			get { return (HttpChannel == null) ? 0 : (int)HttpChannel.GetResponseStatusAttribute(); }
		}
		
		/// <summary>
		/// Gets the HTTP response status text.
		/// </summary>
		public string HttpResponseStatusText
		{
			get { return (HttpChannel == null) ? null : nsString.Get(HttpChannel.GetResponseStatusTextAttribute); }
		}
	}
}
