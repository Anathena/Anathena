

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoFieldSetElement : GeckoHtmlElement
	{
		nsIDOMHTMLFieldSetElement DOMHTMLElement;
		internal GeckoFieldSetElement(nsIDOMHTMLFieldSetElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoFieldSetElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLFieldSetElement;
		}
		public GeckoFormElement Form {
			get { return new GeckoFormElement(DOMHTMLElement.GetFormAttribute()); }
		}

	}
}

