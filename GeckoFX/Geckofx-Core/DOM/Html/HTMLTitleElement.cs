

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoTitleElement : GeckoHtmlElement
	{
		nsIDOMHTMLTitleElement DOMHTMLElement;
		internal GeckoTitleElement(nsIDOMHTMLTitleElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoTitleElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLTitleElement;
		}
		public string Text {
			get { return nsString.Get(DOMHTMLElement.GetTextAttribute); }
			set { DOMHTMLElement.SetTextAttribute(new nsAString(value)); }
		}

	}
}

