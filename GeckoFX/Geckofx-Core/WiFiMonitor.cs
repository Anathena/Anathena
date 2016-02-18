using System;
using Gecko.Interop;

namespace Gecko
{
	/// <summary>
	/// This code is not crashes (cool :) ) but always return zero length array of access points :(
	/// 
	/// https://developer.mozilla.org/En/Monitoring_WiFi_access_points
	/// </summary>
	public static class WiFiMonitor
	{
		private static ComPtr<nsIWifiMonitor> _wifiMonitor;

		static WiFiMonitor()
		{
			_wifiMonitor = Xpcom.GetService2<nsIWifiMonitor>(Contracts.WiFiMonitor);

			
		}

		public static void StartWatching(WiFiListener wifiListener)
		{
			_wifiMonitor.Instance.StartWatching(wifiListener);
		}

		public static void StopWatching(WiFiListener wifiListener)
		{
			_wifiMonitor.Instance.StopWatching(wifiListener);
		}
	}


	public sealed class WiFiListener
		:nsIWifiListener
	{
		void nsIWifiListener.OnChange( nsIWifiAccessPoint[] accessPoints, uint aLen )
		{
			if (AccessPointsChanged == null) return;
			WifiAccessPoint[] array = new WifiAccessPoint[aLen];
			for (int i = 0; i < accessPoints.Length; i++)
			{
				array[i]=new WifiAccessPoint( accessPoints[i] );
			}
			AccessPointsChanged( array );
		}

		void nsIWifiListener.OnError( int error )
		{
			
		}

		public event Action<WifiAccessPoint[]> AccessPointsChanged;
	}


	public sealed class WifiAccessPoint
	{
		private nsIWifiAccessPoint _wifiAccessPoint;

		public WifiAccessPoint(nsIWifiAccessPoint wifiAccessPoint)
		{
			_wifiAccessPoint = wifiAccessPoint;
		}


		public string Mac
		{
			get { return nsString.Get( _wifiAccessPoint.GetMacAttribute ); }
		}

		public string Ssid
		{
			get { return nsString.Get(_wifiAccessPoint.GetSsidAttribute); }
		}
	}
}