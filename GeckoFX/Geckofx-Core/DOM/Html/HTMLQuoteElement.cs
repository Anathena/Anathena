

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoQuoteElement : GeckoHtmlElement
	{
		nsIDOMHTMLQuoteElement DOMHTMLElement;
		internal GeckoQuoteElement(nsIDOMHTMLQuoteElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoQuoteElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLQuoteElement;
		}
		public string Cite {
			get { return nsString.Get(DOMHTMLElement.GetCiteAttribute); }
			set { DOMHTMLElement.SetCiteAttribute(new nsAString(value)); }
		}

	}
}

