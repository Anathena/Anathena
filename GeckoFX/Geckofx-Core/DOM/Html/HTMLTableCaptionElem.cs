

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoTableCaptionElement : GeckoHtmlElement
	{
		nsIDOMHTMLTableCaptionElement DOMHTMLElement;
		internal GeckoTableCaptionElement(nsIDOMHTMLTableCaptionElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoTableCaptionElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLTableCaptionElement;
		}
		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

	}
}
