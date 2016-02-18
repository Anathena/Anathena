using System;
using System.Runtime.InteropServices;
using Gecko.Net;

namespace Gecko.Observers
{
	public class BaseHttpRequestResponseObserver
		: NsSupportsBase, nsIObserver
	{
		internal bool _isRegistered;

		void nsIObserver.Observe( nsISupports aSubject, string aTopic, string aData )
		{
			switch (aTopic)
			{
				case ObserverNotifications.HttpRequests.HttpOnModifyRequest:
					using (var req = HttpChannel.Create( aSubject ))
					{
						// error handling will help in debug
#if DEBUG
						try
						{
							Request( req );
						}
						catch ( Exception e )
						{
							Console.WriteLine("Exception in observer implementation");
						}
#else
						Request( req );
#endif

					}
					break;
				case ObserverNotifications.HttpRequests.HttpOnExamineResponse:
					using (var res = HttpChannel.Create( aSubject ))
					{
						// error handling will help in debug
#if DEBUG
						try
						{
							Response( res );
						}
						catch ( Exception e)
						{
							Console.WriteLine( "Exception in observer implementation" );
						}
#else
						Response( res );
#endif
					}
					break;
			}
		}

		protected virtual void Request(HttpChannel channel)
		{

		}

		protected virtual void Response(HttpChannel channel)
		{

		}

	}
}