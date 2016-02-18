using System;

// Mozilla error lookup utility is located here
// http://james-ross.co.uk/mozilla/misc/nserror

namespace Gecko
{
	/// <summary>
	/// Base exception class for all xullrunner exceptions
	/// General errors may be mapped here
	/// </summary>
	public class GeckoException
		:Exception
	{
		internal GeckoException()
			:base()
		{
			
		}

		internal GeckoException(string message)
			:base(message)
		{
			
		}

		internal GeckoException(string message,Exception innerException)
			:base(message,innerException)
		{
			
		}
	}

	/// <summary>
	/// nsIException wrapper
	/// </summary>
	public sealed class GeckoNativeException
		:GeckoException
	{
		private nsIException _exception;

		private GeckoNativeException(nsIException exception)
			:base(nsString.Get(exception.GetMessageAttribute))
		{
			_exception = exception;

			
		}

		public string Name
		{
			get { return nsString.Get(_exception.GetNameAttribute); }
		}


		public GeckoNativeException Inner
		{
			get { return Create(_exception.GetInnerAttribute()); }
		}

		internal static GeckoNativeException Create(nsIException exception)
		{
			return exception == null ? null : new GeckoNativeException( exception );
		}
	}

	/// <summary>
	/// File Errors
	/// </summary>
	public class GeckoFileException
		: GeckoException
	{
		
	}

	/// <summary>
	/// Stream Errors
	/// </summary>
	public class GeckoStreamException
		: GeckoException
	{

	}

	/// <summary>
	/// Document and Node Errors
	/// </summary>
	public class GeckoDomException
		: GeckoException
	{
		public GeckoDomException(string message)
			:base(message)
		{
			
		}
	}

	/// <summary>
	/// Network Errors
	/// </summary>
	public class GeckoNetworkException
		: GeckoException
	{

	}

	/// <summary>
	/// JavaScript Errors
	/// </summary>
	public class GeckoJavaScriptException
		: GeckoException
	{
		public GeckoJavaScriptException(string msg) : base(msg)
		{
			
		}
	}

	/// <summary>
	/// XPath Errors
	/// </summary>
	public class GeckoXPathException
		: GeckoException
	{

	}

	/// <summary>
	/// XSLT Errors
	/// </summary>
	public class GeckoXsltException
		: GeckoException
	{

	}

	/// <summary>
	/// Miscellaneous Errors
	/// </summary>
	public class GeckoMiscellaneousException
		: GeckoException
	{

	}

}