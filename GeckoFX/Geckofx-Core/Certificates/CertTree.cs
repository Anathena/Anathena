namespace Gecko.Certificates
{
	public sealed class CertTree
		:TreeView
	{
		private nsICertTree _certTree;

		internal CertTree(nsICertTree certTree)
			:base(certTree)
		{
			_certTree = certTree;
		}

		/// <summary>Member LoadCerts </summary>
		/// <param name='type'> </param>
		public void LoadCerts(uint type)
		{
			_certTree.LoadCerts( type );
		}
		

		/// <summary>Member LoadCertsFromCache </summary>
		/// <param name='cache'> </param>
		/// <param name='type'> </param>
		public void LoadCertsFromCache(CertCache cache, uint type)
		{
			_certTree.LoadCertsFromCache( cache._certCache.Instance, type );
		}

		/// <summary>Member GetCert </summary>
		/// <param name='index'> </param>
		/// <returns>A nsIX509Cert</returns>
		public Certificate GetCert(uint index)
		{
			return Certificate.Create( (nsIX509Cert3) _certTree.GetCert( index ) );
		}

		/// <summary>Member GetTreeItem </summary>
		/// <param name='index'> </param>
		/// <returns>A nsICertTreeItem</returns>
		public CertTreeItem GetTreeItem(uint index)
		{
			return new CertTreeItem( _certTree.GetTreeItem( index ) );
		}

		/// <summary>Member IsHostPortOverride </summary>
		/// <param name='index'> </param>
		/// <returns>A System.Boolean</returns>
		public bool IsHostPortOverride(uint index)
		{
			return _certTree.IsHostPortOverride( index );
		}

		/// <summary>Member DeleteEntryObject </summary>
		/// <param name='index'> </param>
		public void DeleteEntryObject(uint index)
		{
			_certTree.DeleteEntryObject( index );
		}
	}
}