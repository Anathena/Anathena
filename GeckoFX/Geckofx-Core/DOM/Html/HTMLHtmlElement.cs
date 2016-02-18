

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoHtmlHtmlElement : GeckoHtmlElement
	{
		nsIDOMHTMLHtmlElement DOMHTMLElement;
		internal GeckoHtmlHtmlElement(nsIDOMHTMLHtmlElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoHtmlHtmlElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLHtmlElement;
		}
		public string Version {
			get { return nsString.Get(DOMHTMLElement.GetVersionAttribute); }
			set { DOMHTMLElement.SetVersionAttribute(new nsAString(value)); }
		}

	}
}

