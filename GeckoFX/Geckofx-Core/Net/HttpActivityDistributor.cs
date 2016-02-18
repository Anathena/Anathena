using Gecko.Interop;

namespace Gecko.Net
{
	public static class HttpActivityDistributor
	{
		private static ComPtr<nsIHttpActivityDistributor> _httpActivityDistributor;

		static HttpActivityDistributor()
		{
			_httpActivityDistributor = Xpcom.GetService2<nsIHttpActivityDistributor>(Contracts.HttpActivityDistributor);
		}

		public static bool IsActive
		{
			get { return _httpActivityDistributor.Instance.GetIsActiveAttribute(); }
		}

		public static void AddObserver( nsIHttpActivityObserver observer )
		{
			_httpActivityDistributor.Instance.AddObserver(observer);
		}

		public static void AddObserver(BaseHttpActivityObserver observer)
		{
			if (observer._isRegistered) return;
			_httpActivityDistributor.Instance.AddObserver( observer );
			observer._isRegistered = true;
		}

		public static void RemoveObserver(BaseHttpActivityObserver observer)
		{
			if (!observer._isRegistered) return;
			_httpActivityDistributor.Instance.RemoveObserver(observer);
			observer._isRegistered = false;
		}
	}

	public class BaseHttpActivityObserver
		: nsIHttpActivityObserver
	{
		internal bool _isRegistered;

		void nsIHttpActivityObserver.ObserveActivity(nsISupports httpChannel, uint activityType, uint activitySubtype, long timestamp, ulong extraSizeData, nsACStringBase extraStringData)
		{
			switch (activityType)
			{
				case nsIHttpActivityObserverConstants.ACTIVITY_TYPE_SOCKET_TRANSPORT:
					SocketTransport( httpChannel, activitySubtype, timestamp, extraSizeData, extraStringData );
					break;
				case nsIHttpActivityObserverConstants.ACTIVITY_TYPE_HTTP_TRANSACTION:
					HttpTransaction(httpChannel, activitySubtype, timestamp, extraSizeData, extraStringData);
					break;
			}
		}

		protected virtual void SocketTransport(nsISupports httpChannel,uint activitySubtype, long timestamp, ulong extraSizeData, nsACStringBase extraStringData)
		{
			
		}

		protected virtual void HttpTransaction(nsISupports httpChannel,uint activitySubtype, long timestamp, ulong extraSizeData, nsACStringBase extraStringData)
		{
			
		}

		bool nsIHttpActivityObserver.GetIsActiveAttribute()
		{
			return true;
		}
	}
}