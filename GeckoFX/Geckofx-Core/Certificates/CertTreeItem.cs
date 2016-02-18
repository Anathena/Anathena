using System.Runtime.CompilerServices;
using Gecko.Interop;

namespace Gecko.Certificates
{
	public sealed class CertTreeItem
	{
		private ComPtr<nsICertTreeItem> _certTreeItem;

		internal CertTreeItem(nsICertTreeItem certTreeItem)
		{
			_certTreeItem = new ComPtr<nsICertTreeItem>( certTreeItem );
		}

		public Certificate Certificate
		{
			get { return Certificate.Create( (nsIX509Cert3) _certTreeItem.Instance.GetCertAttribute() ); }
		}

		public string HostPort
		{
			get { return nsString.Get(_certTreeItem.Instance.GetHostPortAttribute); }
		}
	}
}