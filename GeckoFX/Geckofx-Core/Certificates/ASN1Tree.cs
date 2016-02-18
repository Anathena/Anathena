namespace Gecko.Certificates
{
	public sealed class ASN1Tree
		:TreeView
	{
		private nsIASN1Tree _asnTree;

		internal ASN1Tree(nsIASN1Tree asnTree)
			:base(asnTree)
		{
			_asnTree = asnTree;
		}

		public void LoadASN1Structure(ASN1Object asn1Object)
		{
			_asnTree.LoadASN1Structure( asn1Object._object );
		}

		public string GetDisplayData(uint index)
		{
			return nsString.Get( _asnTree.GetDisplayData, index );
		}
	}
}