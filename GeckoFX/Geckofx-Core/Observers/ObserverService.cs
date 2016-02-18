using System;
using System.Runtime.InteropServices;
using Gecko.Interop;

namespace Gecko
{
	public static class ObserverService
	{
		private static ComPtr<nsIObserverService> _observerService;

		static ObserverService()
		{
			_observerService = Xpcom.GetService2<nsIObserverService>( Contracts.ObserverService );
		}

		public static void AddObserver(nsIObserver observer, string topic, bool ownsWeak)
		{
			_observerService.Instance.AddObserver(observer, topic, ownsWeak);
		}

		public static void RemoveObserver(nsIObserver observer, string topic)
		{
			_observerService.Instance.RemoveObserver(observer, topic);
		}

		public static void AddObserver(Observers.BaseHttpModifyRequestObserver observer)
		{
			if (observer._isRegistered) return;
			_observerService.Instance.AddObserver(observer, ObserverNotifications.HttpRequests.HttpOnModifyRequest, false);
		}

		public static void RemoveObserver(Observers.BaseHttpModifyRequestObserver observer)
		{
			if (!observer._isRegistered) return;
			_observerService.Instance.RemoveObserver( observer, ObserverNotifications.HttpRequests.HttpOnModifyRequest );
		}

		public static void AddObserver(Observers.BaseHttpRequestResponseObserver observer)
		{
			if (observer._isRegistered) return;
			_observerService.Instance.AddObserver(observer, ObserverNotifications.HttpRequests.HttpOnModifyRequest, false);
			_observerService.Instance.AddObserver(observer, ObserverNotifications.HttpRequests.HttpOnExamineResponse, false);
		}

		public static void RemoveObserver(Observers.BaseHttpRequestResponseObserver observer)
		{
			if (!observer._isRegistered) return;
			_observerService.Instance.RemoveObserver(observer, ObserverNotifications.HttpRequests.HttpOnModifyRequest);
			_observerService.Instance.RemoveObserver(observer, ObserverNotifications.HttpRequests.HttpOnExamineResponse);
		}
	}
}