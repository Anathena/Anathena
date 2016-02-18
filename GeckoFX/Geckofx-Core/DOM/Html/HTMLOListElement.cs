

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoOListElement : GeckoHtmlElement
	{
		nsIDOMHTMLOListElement DOMHTMLElement;
		internal GeckoOListElement(nsIDOMHTMLOListElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoOListElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLOListElement;
		}
		public bool Compact {
			get { return DOMHTMLElement.GetCompactAttribute(); }
			set { DOMHTMLElement.SetCompactAttribute(value); }
		}

		public int Start {
			get { return DOMHTMLElement.GetStartAttribute(); }
			set { DOMHTMLElement.SetStartAttribute(value); }
		}

		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
			set { DOMHTMLElement.SetTypeAttribute(new nsAString(value)); }
		}

	}
}

