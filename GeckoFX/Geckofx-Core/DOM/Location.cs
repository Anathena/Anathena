using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class Location
	{
		private ComPtr<nsIDOMLocation> _location;

		private Location( nsIDOMLocation location )
		{
			_location = new ComPtr<nsIDOMLocation>(  location );
		}

		public static Location Create(nsIDOMLocation location)
		{
			return new Location( location );
		}

		public string Hash
		{
			get { return nsString.Get( _location.Instance.GetHashAttribute ); }
			set { nsString.Set(_location.Instance.SetHashAttribute, value); }
		}

		public string Host
		{
			get { return nsString.Get(_location.Instance.GetHostAttribute); }
			set { nsString.Set(_location.Instance.SetHostAttribute, value); }
		}

		public string Hostname
		{
			get { return nsString.Get(_location.Instance.GetHostnameAttribute); }
			set { nsString.Set(_location.Instance.GetHostnameAttribute, value); }
		}

		public string Href
		{
			get { return nsString.Get(_location.Instance.GetHrefAttribute); }
			set { nsString.Set(_location.Instance.SetHrefAttribute, value); }
		}

		public string Pathname
		{
			get { return nsString.Get(_location.Instance.GetPathnameAttribute); }
			set { nsString.Set(_location.Instance.SetPathnameAttribute, value); }
		}

		public string Port
		{
			get { return nsString.Get(_location.Instance.GetPortAttribute); }
			set { nsString.Set(_location.Instance.SetPortAttribute, value); }
		}

		public string Protocol
		{
			get { return nsString.Get(_location.Instance.GetProtocolAttribute); }
			set { nsString.Set(_location.Instance.SetProtocolAttribute, value); }
		}

		public string Search
		{
			get { return nsString.Get(_location.Instance.GetSearchAttribute); }
			set { nsString.Set(_location.Instance.SetSearchAttribute, value); }
		}

		public void Reload(bool forceget)
		{
			_location.Instance.Reload(forceget);
		}

		
		public void Replace(string url)
		{
			nsString.Set(_location.Instance.Replace, url);
		}

		public void Assign(string url)
		{
			nsString.Set(_location.Instance.Assign, url);
		}

		public override string ToString()
		{
			return nsString.Get(_location.Instance.ToString);
		}
	}
}
