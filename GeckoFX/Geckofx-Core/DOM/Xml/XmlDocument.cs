using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.DOM.Xml
{
	public sealed class XmlDocument
		:GeckoDomDocument
	{
		private nsIDOMXMLDocument _xmlDocument;

		internal XmlDocument(nsIDOMXMLDocument document)
			: base( document )
		{
			_xmlDocument = document;
		}

		public bool Async
		{
			get { return _xmlDocument.GetAsyncAttribute(); }
			set{_xmlDocument.SetAsyncAttribute( value );}
		}

		public bool Load(string url)
		{
			bool ret=nsString.Pass( _xmlDocument.Load, url );
			return ret;
		}


	}
}
