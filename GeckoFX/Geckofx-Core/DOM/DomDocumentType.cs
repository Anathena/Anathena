namespace Gecko.DOM
{
	public class DomDocumentType
		:GeckoNode
	{
		private nsIDOMDocumentType _documentType;

		private DomDocumentType(nsIDOMDocumentType documentType)
			:base(documentType)
		{
			_documentType = documentType;
		}

		public static DomDocumentType Create(nsIDOMDocumentType documentType)
		{
			return new DomDocumentType( documentType );
		}

		/// <summary>
		/// Each Document has a doctype attribute whose value is either null
		/// or a DocumentType object.
		/// The nsIDOMDocumentType interface in the DOM Core provides an
		/// interface to the list of entities that are defined for the document.
		///
		/// For more information on this interface please see
		/// http://www.w3.org/TR/DOM-Level-2-Core/
		/// </summary>
		public string Name
		{
			get { return nsString.Get( _documentType.GetNameAttribute ); }
		}

		public string PublicID
		{
			get { return nsString.Get( _documentType.GetPublicIdAttribute ); }
		}

		public string SystemID
		{
			get { return nsString.Get(_documentType.GetSystemIdAttribute); }
		}

		public string InternalSubset
		{
			get { return nsString.Get(_documentType.GetInternalSubsetAttribute); }
		}
	}
}