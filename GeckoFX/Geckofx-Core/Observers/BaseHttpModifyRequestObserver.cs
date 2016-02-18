using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Gecko.Net;

namespace Gecko.Observers
{
	public class BaseHttpModifyRequestObserver
		: NsSupportsBase, nsIObserver
	{
		internal bool _isRegistered;

		void nsIObserver.Observe(nsISupports aSubject, string aTopic, string aData)
		{
			if (aTopic != ObserverNotifications.HttpRequests.HttpOnModifyRequest) return;
			using (var channel = HttpChannel.Create(aSubject))
			{
				ObserveRequest(channel);
			}
			
		}

		protected virtual void ObserveRequest(HttpChannel channel)
		{
			
		}

	}
}
