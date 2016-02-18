

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoMetaElement : GeckoHtmlElement
	{
		nsIDOMHTMLMetaElement DOMHTMLElement;
		internal GeckoMetaElement(nsIDOMHTMLMetaElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoMetaElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLMetaElement;
		}
		public string Content {
			get { return nsString.Get(DOMHTMLElement.GetContentAttribute); }
			set { DOMHTMLElement.SetContentAttribute(new nsAString(value)); }
		}

		public string HttpEquiv {
			get { return nsString.Get(DOMHTMLElement.GetHttpEquivAttribute); }
			set { DOMHTMLElement.SetHttpEquivAttribute(new nsAString(value)); }
		}

		public string Name {
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { DOMHTMLElement.SetNameAttribute(new nsAString(value)); }
		}

		public string Scheme {
			get { return nsString.Get(DOMHTMLElement.GetSchemeAttribute); }
			set { DOMHTMLElement.SetSchemeAttribute(new nsAString(value)); }
		}

	}
}

