namespace Gecko.DOM
{
	public class DocumentFragment
		:GeckoNode
	{
		private nsIDOMDocumentFragment _documentFragment;

		protected DocumentFragment(nsIDOMDocumentFragment documentFragment)
			: base(documentFragment)
		{
			_documentFragment = documentFragment;
		}


		public static DocumentFragment CreateDocumentFragmentWrapper(nsIDOMDocumentFragment documentFragment)
		{
			return documentFragment == null ? null : new DocumentFragment( documentFragment );
		}
	}
}