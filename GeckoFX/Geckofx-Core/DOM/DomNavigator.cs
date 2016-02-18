using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.DOM
{
	public sealed class DomNavigator
	{
		private nsIDOMNavigator _navigator;

		internal DomNavigator(nsIDOMNavigator navigator)
		{
			_navigator = navigator;
		}

		public string AppCodeName
		{
			get { return nsString.Get( _navigator.GetAppCodeNameAttribute ); }
		}

		public string AppName
		{
			get { return nsString.Get(_navigator.GetAppNameAttribute); }
		}

		public string AppVersion
		{
			get { return nsString.Get(_navigator.GetAppVersionAttribute); }
		}

		public string BuildID
		{
			get { return nsString.Get(_navigator.GetBuildIDAttribute); }
		}

		public string DoNotTrack
		{
			get { return nsString.Get(_navigator.GetDoNotTrackAttribute); }
		}

		public string Language
		{
			get { return nsString.Get(_navigator.GetLanguageAttribute); }
		}

		public string Oscpu
		{
			get { return nsString.Get(_navigator.GetOscpuAttribute); }
		}

		public string Platform
		{
			get { return nsString.Get(_navigator.GetPlatformAttribute); }
		}

		//_navigator.GetPluginsAttribute(  );

		public string Product
		{
			get { return nsString.Get(_navigator.GetProductAttribute); }
		}

		public string ProductSub
		{
			get { return nsString.Get(_navigator.GetProductSubAttribute); }
		}

		public string UserAgent
		{
			get { return nsString.Get(_navigator.GetUserAgentAttribute); }
		}

		public string Vendor
		{
			get { return nsString.Get(_navigator.GetVendorAttribute); }
		}

		public string GetVendorSub
		{
			get { return nsString.Get(_navigator.GetVendorSubAttribute); }
		}
	}
}
