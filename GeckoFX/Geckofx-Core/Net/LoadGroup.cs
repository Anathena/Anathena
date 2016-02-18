using System.Collections.Generic;
using Gecko.Interop;

namespace Gecko.Net
{
	public sealed class LoadGroup
		:Request
	{
		internal nsILoadGroup _loadGroup;

		public LoadGroup(nsILoadGroup loadGroup)
			:base(loadGroup)
		{
			_loadGroup = loadGroup;
		}

		public nsIRequestObserver GroupObserver
		{
			get { return _loadGroup.GetGroupObserverAttribute(); }
			set { _loadGroup.SetGroupObserverAttribute( value ); }
		}

		public Request DefaultLoadRequest
		{
			get { return _loadGroup.GetDefaultLoadRequestAttribute().Wrap( CreateRequest ); }
			set{_loadGroup.SetDefaultLoadRequestAttribute( value.NativeRequest );}
		}

		public void AddRequest(Request request,nsISupports aContext)
		{
			_loadGroup.AddRequest( request.NativeRequest, aContext );
		}

		public void RemoveRequest( Request request, nsISupports aContext, int aStatus )
		{
			_loadGroup.RemoveRequest( request.NativeRequest, aContext, aStatus );
		}

		public IEnumerable<Request> Requests
		{
			get
			{
				return new Collections.GeckoEnumerableCollection<Request, nsIRequest>(
					_loadGroup.GetRequestsAttribute,
					CreateRequest );
			}
		}


		public uint ActiveCount
		{
			get { return _loadGroup.GetActiveCountAttribute(); }
		}

		public nsIInterfaceRequestor NotificationCallbacks
		{
			get { return _loadGroup.GetNotificationCallbacksAttribute(); }
			set{_loadGroup.SetNotificationCallbacksAttribute( value );}
		}
	}
}