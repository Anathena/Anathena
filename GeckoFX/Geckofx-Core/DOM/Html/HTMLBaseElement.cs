

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoBaseElement : GeckoHtmlElement
	{
		nsIDOMHTMLBaseElement DOMHTMLElement;
		internal GeckoBaseElement(nsIDOMHTMLBaseElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoBaseElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLBaseElement;
		}
		public string Href {
			get { return nsString.Get(DOMHTMLElement.GetHrefAttribute); }
			set { DOMHTMLElement.SetHrefAttribute(new nsAString(value)); }
		}

		public string Target {
			get { return nsString.Get(DOMHTMLElement.GetTargetAttribute); }
			set { DOMHTMLElement.SetTargetAttribute(new nsAString(value)); }
		}

	}
}

